using CSL.Encryption;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using BC = BCrypt.Net.BCrypt;

namespace Felix;

public record User(string username, string hash, Guid uuid)
{
    public object _id { get; set; }

    internal static MongoClient mdbClient = new MongoClient("mongodb://mongodb:27017");
    internal static string aesKey = Environment.GetEnvironmentVariable("aeskey") ?? throw new NullReferenceException();

    public static string HashPassword(string plaintext) => BC.HashPassword(plaintext);
    public static bool ValidatePassword(string plaintext, string hash) => BC.Verify(plaintext, hash);

    //mongo funcs
    public static async Task<string> Auth(string username, string plaintext)
    {
        IMongoDatabase db = mdbClient.GetDatabase("felix");
        User ret = db.GetCollection<User>("users").AsQueryable().First(x => x.username == username);
        
        if (User.ValidatePassword(plaintext, ret.hash))
        {
            string jsonString = JsonConvert.SerializeObject(ret);
            AES256KeyBasedProtector aes = new AES256KeyBasedProtector(System.Text.Encoding.UTF8.GetBytes(aesKey));

            return await aes.Protect(jsonString);
        }
        else
        {
            throw new Exception();
        }
    }

    public static async Task CreateUser(string username, string plaintext)
    {
        IMongoDatabase db = mdbClient.GetDatabase("felix");
        IMongoCollection<User> users = db.GetCollection<User>("users");

        await users.InsertOneAsync(new User(username, User.HashPassword(plaintext), Guid.NewGuid()));
    }
}

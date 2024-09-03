using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Felix;

public class Users
{
    public record RCreateUser(string username, string password);

    public static void MapUsers(WebApplication app)
    {
        app.MapPost("/api/user", async ([FromBody] RCreateUser data) =>
        {
            User? user = User.mdbClient
                .GetDatabase("felix")
                .GetCollection<User>("users")
                .AsQueryable()
                .FirstOrDefault(x => x.username == data.username);

            if (user == null)
            {
                User.CreateUser(data.username, data.password);
            }
            else
            {
                throw new Exception("User already exists!");
            }
        });
    }
}

using MongoDB.Driver;
using Newtonsoft.Json; //do i need this????
using CSL.Encryption;

namespace Felix;

public class UserChats
{
    public Guid uuid { get; set; }
    //public List<ChatConversation> conversations { get; set; }

    public UserChats(Guid userId)
    {
        uuid = userId;
        //conversations = new();
    }

    public UserChats GetUsersChats(User user)
    {
        UserChats chats = User.mdbClient
            .GetDatabase("felix")
            .GetCollection<UserChats>("chats")
            .AsQueryable()
            .First(x => x.uuid == user.uuid);

        return chats;
    }
}

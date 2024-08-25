using Microsoft.AspNetCore.Mvc;

namespace Felix;

public static class Auth
{
    public static void MapAuth(WebApplication app)
    {
        app.MapPost("/api/auth", async ([FromHeader] string username, [FromHeader] string password) =>
        {
            string token = await User.Auth(username, password);
            return token;
        });
    }
}

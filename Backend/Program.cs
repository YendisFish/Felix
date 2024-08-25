using Felix;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

CSL.DependencyInjection.AesGcmConstructor = (x) => new System.Security.Cryptography.AesGcm(x);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Console.WriteLine(Environment.GetEnvironmentVariable("aeskey"));

Dictionary<Guid, ChatConversation> userChats = new();
HttpClient client = new();

Auth.MapAuth(app);
Users.MapUsers(app);

app.MapGet("/create", () =>
{
    Guid chatId = Guid.NewGuid();
    userChats.Add(chatId, new ChatConversation("llama3.1", new()));
    return chatId;
});

app.MapGet("/chat", ([FromHeader] Guid chatId) =>
{
    return userChats[chatId];
});

app.MapPost("/chat", async ([FromHeader] Guid chatId, ChatMessage prompt) =>
{
    userChats[chatId].AddMessage(prompt);

    HttpResponseMessage msg = await client.PostAsJsonAsync<ChatConversation>("http://localhost:5678/api/chat", userChats[chatId]);
    ChatResponse jsonObj = await msg.Content.ReadFromJsonAsync<ChatResponse>() ?? throw new NullReferenceException();

    userChats[chatId].AddMessage(jsonObj.message);
});

app.Run();

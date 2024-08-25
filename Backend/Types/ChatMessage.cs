namespace Felix;

public record ChatConversation(string model, List<ChatMessage> messages, bool stream = false)
{
    public void AddMessage(ChatMessage message)
    {
        messages.Add(message);
    }
}


public record ChatMessage(string role, string content);

public record ChatResponse(string model, string created_at, ChatMessage message);

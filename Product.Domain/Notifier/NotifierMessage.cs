namespace Product.Domain.Notifier;

public class NotifierMessage : INotifierMessage
{
    private readonly List<string> _messages = new();

    public void Add(string message)
    {
        _messages.Add(message);
    }

    public void AddRange(List<string> messages)
    {
        _messages.AddRange(messages);
    }

    public List<string> GetMessages()
    {
        return _messages;
    }

    public bool IsValid()
    {
        return _messages.Count == 0;
    }
}
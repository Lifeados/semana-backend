namespace Product.Domain.Notifier;

public interface INotifierMessage
{
    void Add(string message);
    void AddRange(List<string> messages);
    List<string> GetMessages();
    bool IsValid();
}
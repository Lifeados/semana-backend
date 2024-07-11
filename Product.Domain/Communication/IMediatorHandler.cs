namespace Product.Domain.Communication;

public interface IMediatorHandler
{
    Task<bool> SendEvent<T>(T @event);
    Task PublishEvent<T>(T @event);
}
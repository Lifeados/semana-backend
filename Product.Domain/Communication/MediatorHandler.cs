using MediatR;

namespace Product.Domain.Communication;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    public async Task<bool> SendEvent<T>(T @event)
    {
        var res = await mediator.Send(@event);
        return res != null && (bool)res;
    }

    public async Task PublishEvent<T>(T @event)
    {
        await mediator.Publish(@event);
    }
}
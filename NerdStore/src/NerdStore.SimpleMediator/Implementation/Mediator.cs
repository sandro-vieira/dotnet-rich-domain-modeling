using Microsoft.Extensions.DependencyInjection;
using NerdStore.SimpleMediator.Interfaces;

namespace NerdStore.SimpleMediator.Implementation;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<TResponse> Send<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse));

        var handler = _serviceProvider.GetService(handlerType)
            ?? throw new InvalidOperationException(
                $"Handler for request type {request.GetType().Name} not found.");

        return await ((IRequestHandler<IRequest<TResponse>, TResponse>)handler)
            .Handle(request, cancellationToken);
    }

    public async Task Publish<TNotification>(
        TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        var handlers = _serviceProvider.GetServices<INotificationHandler<TNotification>>();

        if (handlers?.FirstOrDefault() == null)
        {
            return;
        }

        foreach (var handler in handlers)
        {
            await handler.Handle(notification, cancellationToken);
        }
    }
}

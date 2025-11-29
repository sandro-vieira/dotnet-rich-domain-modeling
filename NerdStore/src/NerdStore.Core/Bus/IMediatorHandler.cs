using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T @event) where T : Event;
}

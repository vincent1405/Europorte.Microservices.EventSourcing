using MediatR;

namespace Dewisme.Vincent.Microservices.EventSourcing.Common
{
    public class EventReceived<EventType> : INotification
    {
        public EventReceived(EventType @event)
        {
            Event = @event;
        }

        public EventType Event { get; }
    }
}
namespace Dewisme.Vincent.Microservices.EventSourcing.Common
{
    public static class EventReceivedFactory
    {
        public static EventReceived<EventType> Create<EventType>(EventType @event)
        {
            return new EventReceived<EventType>(@event);
        }
    }
}
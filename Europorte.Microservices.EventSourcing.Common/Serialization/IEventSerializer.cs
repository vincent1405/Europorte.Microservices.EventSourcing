using Europorte.Microservices.EventSourcing.Common.Events;

namespace Europorte.Microservices.EventSourcing.Common.Serialization
{
    public interface IEventSerializer
    {
        IDomainEvent<TKey> Deserialize<TKey>(string type, byte[] data);
        IDomainEvent<TKey> Deserialize<TKey>(string type, string data);
        byte[] Serialize<TKey>(IDomainEvent<TKey> @event);
    }
}

using Dewisme.Vincent.Microservices.EventSourcing.Common.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dewisme.Vincent.Microservices.EventSourcing.Common.Serialization
{
    public class JsonEventSerializer : IEventSerializer
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new PrivateSetterContractResolver()
        };

        private readonly IDictionary<string, Type> typeDictionaryByName;

        public JsonEventSerializer(IEnumerable<Assembly> assemblies)
        {
            typeDictionaryByName = new Dictionary<string, Type>();
            InitializeTypeDictionary(assemblies ?? new[] { Assembly.GetExecutingAssembly() });
        }

        private void InitializeTypeDictionary(IEnumerable<Assembly> assemblies)
        {
            foreach(var ass in assemblies)
            {
                foreach(var type in ass.GetTypes())
                {
                    _ = typeDictionaryByName.TryAdd(type.FullName, type);
                }
            }
        }

        public IDomainEvent<TKey> Deserialize<TKey>(string type, byte[] data)
        {
            string json = Encoding.UTF8.GetString(data);
            return Deserialize<TKey>(type, json);
        }

        public IDomainEvent<TKey> Deserialize<TKey>(string type, string data)
        {
            if(!typeDictionaryByName.TryGetValue(type,out Type foundType))
            {
                foundType = Type.GetType(type);
            }

            if(foundType == null)
            {
                throw new InvalidOperationException($"Cannot deserialize event of type '{type}' because it is not registered.");
            }

            var result = JsonConvert.DeserializeObject(data, foundType, JsonSerializerSettings);

            return (IDomainEvent<TKey>)result;
        }

        public byte[] Serialize<TKey>(IDomainEvent<TKey> @event)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
        }
    }
}

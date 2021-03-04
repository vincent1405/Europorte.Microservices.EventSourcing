using Dewisme.Vincent.Microservices.EventSourcing.Common.Events;
using System;

namespace Dewisme.Vincent.ShoppingList.Domain.Events
{
    public class StoreGroupRenamed : DomainEventBase<StoreGroup, Guid>
    {
        public string NewName { get; }

        private StoreGroupRenamed() { }

        public StoreGroupRenamed(StoreGroup storeGroup, string newName) : base(storeGroup)
        {
            NewName = newName;
        }
    }
}

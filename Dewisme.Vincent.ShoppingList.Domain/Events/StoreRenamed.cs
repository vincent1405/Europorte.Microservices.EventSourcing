using Dewisme.Vincent.Microservices.EventSourcing.Common.Events;
using System;

namespace Dewisme.Vincent.ShoppingList.Domain.Events
{
    public class StoreRenamed : DomainEventBase<Store, Guid>
    {
        public string NewName { get; }

        private StoreRenamed() { }

        public StoreRenamed(Store store, string newName) : base(store)
        {
            NewName = newName;
        }
    }
}

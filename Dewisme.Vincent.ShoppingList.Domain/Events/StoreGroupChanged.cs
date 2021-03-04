using Dewisme.Vincent.Microservices.EventSourcing.Common.Events;
using System;

namespace Dewisme.Vincent.ShoppingList.Domain.Events
{
    public class StoreGroupChanged : DomainEventBase<Store, Guid>
    {
        public Guid? NewStoreGroupId { get; }

        private StoreGroupChanged() { }

        public StoreGroupChanged(Store store, StoreGroup newStoreGroup) : base(store)
        {
            NewStoreGroupId = newStoreGroup?.Id;
        }
    }
}

using Dewisme.Vincent.Microservices.EventSourcing.Common.Events;
using System;

namespace Dewisme.Vincent.ShoppingList.Domain.Events
{
    public class StoreGroupCreated : DomainEventBase<StoreGroup, Guid>
    {
        public Guid StoreGroupId { get; }
        public string StoreGroupName { get; }

        /// <summary>
        /// For deserialization
        /// </summary>
        private StoreGroupCreated() { }

        public StoreGroupCreated(StoreGroup storeGroup) : base(storeGroup)
        {
            StoreGroupId = storeGroup.Id;
            StoreGroupName = storeGroup.Name;
        }
    }
}

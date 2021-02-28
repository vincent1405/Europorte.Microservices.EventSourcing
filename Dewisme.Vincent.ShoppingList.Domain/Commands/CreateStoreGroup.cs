using Europorte.Microservices.EventSourcing.Common.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dewisme.Vincent.ShoppingList.Domain.Commands
{
    public class CreateStoreGroup : INotification
    {
        public CreateStoreGroup(Guid storeGroupId, string name)
        {
            StoreGroupId = storeGroupId;
            Name = name;
        }

        public Guid StoreGroupId { get; }
        public string Name { get; }
    }

    public class CreateStoreGroupHandler : INotificationHandler<CreateStoreGroup>
    {
        private readonly IEventsService<StoreGroup, Guid> storeGroupEventsService;
        
        public CreateStoreGroupHandler(IEventsService<StoreGroup,Guid> storeGroupEventsService)
        {
            this.storeGroupEventsService = storeGroupEventsService;
        }

        public async Task Handle(CreateStoreGroup createStoreGroup, CancellationToken cancellationToken)
        {
            var storeGroup = new StoreGroup(createStoreGroup.StoreGroupId, createStoreGroup.Name);
            await storeGroupEventsService.PersistAsync(storeGroup);
        }
    }
}

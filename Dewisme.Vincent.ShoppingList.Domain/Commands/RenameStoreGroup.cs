using Dewisme.Vincent.Microservices.EventSourcing.Common.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dewisme.Vincent.ShoppingList.Domain.Commands
{
    public class RenameStoreGroup : INotification
    {
        public RenameStoreGroup(StoreGroup storeGroup, string storeGroupNewName)
        {
            StoreGroupId = storeGroup.Id;
            StoreGroupNewName = storeGroupNewName;
        }

        public Guid StoreGroupId { get; }
        public string StoreGroupNewName { get; }
    }

    public class RenameStoreGroupHandler : INotificationHandler<RenameStoreGroup>
    {
        private readonly IEventsService<StoreGroup, Guid> storeGroupEventsService;

        public RenameStoreGroupHandler(IEventsService<StoreGroup, Guid> storeGroupEventsService)
        {
            this.storeGroupEventsService = storeGroupEventsService;
        }

        public async Task Handle(RenameStoreGroup command, CancellationToken cancellationToken)
        {
            var foundStoreGroup = await storeGroupEventsService.RehydrateAsync(command.StoreGroupId);
            if (foundStoreGroup == null)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(command.StoreGroupId), message: $"Invalid store group id.");
            }

            foundStoreGroup.Rename(command.StoreGroupNewName);

            await storeGroupEventsService.PersistAsync(foundStoreGroup);
        }
    }
}

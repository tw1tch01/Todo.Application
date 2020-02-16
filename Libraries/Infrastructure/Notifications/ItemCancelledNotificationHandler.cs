using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Services.TodoItems.Events.CancelItem;

namespace Todo.Infrastructure.Notifications
{
    public class ItemCancelledNotificationHandler : INotificationHandler<ItemCancelledNotification>
    {
        public Task Handle(ItemCancelledNotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
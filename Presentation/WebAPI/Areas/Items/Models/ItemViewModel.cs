using Todo.Domain.Enums;
using Todo.DomainModels.TodoItems;

namespace Todo.WebAPI.Areas.Items.Models
{
    public class ItemViewModel
    {
        public ItemViewModel(TodoItemDetails item)
        {
            Item = item;
        }

        public TodoItemDetails Item { get; }

        public bool CanCancel => CanBeCancelled();

        public bool CanComplete => CanBeCompleted();

        public bool CanStart => CanBeStarted();

        public bool IsOverdue => IsItemOverdue();

        #region Methods

        private bool CanBeCancelled()
        {
            return !Item.CancelledOn.HasValue && !Item.CompletedOn.HasValue;
        }

        private bool CanBeCompleted()
        {
            return !Item.CancelledOn.HasValue && !Item.CompletedOn.HasValue;
        }

        private bool CanBeStarted()
        {
            return !Item.StartedOn.HasValue && !Item.CancelledOn.HasValue && !Item.CompletedOn.HasValue;
        }

        private bool IsItemOverdue()
        {
            return Item.Status == TodoItemStatus.Overdue;
        }

        #endregion Methods
    }
}
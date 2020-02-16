using System;

namespace Todo.WebAPI.Areas.Items.Models
{
    public class CreatedItemResponseModel
    {
        public CreatedItemResponseModel(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}
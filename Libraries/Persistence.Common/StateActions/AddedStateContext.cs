using System;
using Data.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todo.Domain.Common;

namespace Todo.Persistence.Common.StateActions
{
    public class AddedStateContext
    {
        public string CreatedBy { get; set; }
        public string CreatedProcess { get; set; }

        public void SetCreatedAuditFields(EntityEntry entity)
        {
            entity.Entity.TrySetProperty(nameof(ICreatedAudit.CreatedBy), CreatedBy)
                         .TrySetProperty(nameof(ICreatedAudit.CreatedOn), DateTime.UtcNow)
                         .TrySetProperty(nameof(ICreatedAudit.CreatedProcess), CreatedProcess);
        }
    }
}
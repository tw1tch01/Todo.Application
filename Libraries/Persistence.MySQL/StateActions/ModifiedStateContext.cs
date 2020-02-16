using System;
using Data.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todo.Domain.Common;

namespace Todo.Persistence.MySQL.StateActions
{
    public class ModifiedStateContext
    {
        public string ModifiedBy { get; set; }
        public string ModifiedProcess { get; set; }

        public void SetModifiedAuditFields(EntityEntry entity)
        {
            entity.Entity.TrySetProperty(nameof(IModifiedAudit.ModifiedBy), ModifiedBy)
                         .TrySetProperty(nameof(IModifiedAudit.ModifiedOn), DateTime.UtcNow)
                         .TrySetProperty(nameof(IModifiedAudit.ModifiedProcess), ModifiedProcess);
        }
    }
}
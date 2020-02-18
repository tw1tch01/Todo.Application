using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Persistence.Common.Configurations
{
    public class TodoItemConfiguration : BaseEntityConfiguration<TodoItem>
    {
        public override void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            #region Primary Key

            builder.HasKey(item => item.ItemId);

            #endregion Primary Key

            #region Foreign Keys

            builder.HasMany(parentItem => parentItem.ChildItems)
                       .WithOne(childItem => childItem.ParentItem)
                       .HasForeignKey(childItem => childItem.ParentItemId);

            #endregion Foreign Keys

            #region Properties

            builder.Property(item => item.PriorityLevel)
                       .IsRequired()
                       .HasConversion(priority => priority.ToString(), priorityString => (PriorityLevel)Enum.Parse(typeof(PriorityLevel), priorityString));

            builder.Property(item => item.ImportanceLevel)
                       .IsRequired()
                       .HasConversion(importance => importance.ToString(), priorityString => (ImportanceLevel)Enum.Parse(typeof(ImportanceLevel), priorityString));

            builder.Property(item => item.CancelledOn)
                   .IsRequired(false);

            builder.Property(item => item.CompletedOn)
                   .IsRequired(false);

            builder.Property(item => item.StartedOn)
                   .IsRequired(false);

            builder.Property(item => item.DueDate)
                   .IsRequired(false);

            builder.Property(item => item.Rank)
                   .IsRequired(true);

            builder.Property(item => item.Description)
                   .IsRequired();

            builder.Property(item => item.Name)
                   .IsRequired()
                   .HasMaxLength(64);

            #endregion Properties

            base.Configure(builder);
        }
    }
}
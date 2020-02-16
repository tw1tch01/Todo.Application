using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities;

namespace Todo.Persistence.MySQL.Configurations
{
    public class TodoItemNoteConfiguration : BaseEntityConfiguration<TodoItemNote>
    {
        public override void Configure(EntityTypeBuilder<TodoItemNote> builder)
        {
            #region Primary Key

            builder.HasKey(note => note.NoteId);

            #endregion Primary Key

            #region Foreign Keys

            builder.HasOne(note => note.Item)
                   .WithMany(item => item.Notes)
                   .HasForeignKey(note => note.ItemId);

            builder.HasOne(childNote => childNote.ParentNote)
                   .WithMany(parentNote => parentNote.Replies)
                   .HasForeignKey(childNote => childNote.ParentNoteId);

            #endregion Foreign Keys

            #region Properties

            builder.Property(a => a.Comment).IsRequired();

            #endregion Properties

            base.Configure(builder);
        }
    }
}
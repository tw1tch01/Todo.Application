using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Common;

namespace Todo.Persistence.MySQL.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(entity => entity.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(entity => entity.CreatedOn)
                   .IsRequired();

            builder.Property(entity => entity.CreatedProcess)
                   .IsRequired()
                   .HasMaxLength(1024);

            builder.Property(entity => entity.ModifiedBy)
                   .HasMaxLength(256);

            builder.Property(entity => entity.ModifiedProcess)
                   .HasMaxLength(1024);
        }
    }
}
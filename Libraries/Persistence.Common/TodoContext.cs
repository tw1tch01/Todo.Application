using Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todo.Domain.Entities;
using Todo.Persistence.Common.Configurations;
using Todo.Services.Common;

namespace Todo.Persistence.Common
{
    public class TodoContext : DbContext, ITodoContext
    {
        private readonly ILogger _logger;

        public TodoContext(DbContextOptions options, ContextScope contextScope, ILoggerFactory loggerFactory) : base(options)
        {
            ContextScope = contextScope;
            _logger = loggerFactory.CreateLogger(nameof(TodoContext));
        }

        #region DbSets

        public DbSet<TodoItem> Items { get; set; }

        public DbSet<TodoItemNote> Notes { get; set; }

        public ContextScope ContextScope { get; set; }

        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
            modelBuilder.ApplyConfiguration(new TodoItemNoteConfiguration());
        }
    }
}
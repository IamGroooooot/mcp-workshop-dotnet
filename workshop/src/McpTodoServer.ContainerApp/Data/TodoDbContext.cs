using Microsoft.EntityFrameworkCore;
using McpTodoServer.ContainerApp.Models;

namespace McpTodoServer.ContainerApp.Data;

/// <summary>
/// Database context for the Todo application using SQLite in-memory database.
/// </summary>
public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet for TodoItem entities.
    /// </summary>
    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure TodoItem entity
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.IsCompleted)
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("datetime('now')");

            entity.Property(e => e.UpdatedAt)
                .IsRequired(false);

            // Add index for performance on common queries
            entity.HasIndex(e => e.IsCompleted)
                .HasDatabaseName("IX_TodoItems_IsCompleted");

            entity.HasIndex(e => e.CreatedAt)
                .HasDatabaseName("IX_TodoItems_CreatedAt");
        });
    }
}

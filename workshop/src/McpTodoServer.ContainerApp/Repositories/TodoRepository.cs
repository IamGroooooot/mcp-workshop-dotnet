using Microsoft.EntityFrameworkCore;
using McpTodoServer.ContainerApp.Data;
using McpTodoServer.ContainerApp.Models;

namespace McpTodoServer.ContainerApp.Repositories;

/// <summary>
/// Repository implementation for TodoItem operations using Entity Framework Core.
/// </summary>
public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;
    private readonly ILogger<TodoRepository> _logger;

    public TodoRepository(TodoDbContext context, ILogger<TodoRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<TodoItem> CreateAsync(string text, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Todo text cannot be null or empty.", nameof(text));

        if (text.Length > 500)
            throw new ArgumentException("Todo text cannot exceed 500 characters.", nameof(text));

        var todoItem = new TodoItem
        {
            Text = text.Trim(),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created new todo item with ID {TodoId}: {TodoText}", 
            todoItem.Id, todoItem.Text);

        return todoItem;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TodoItem>> ListAsync(CancellationToken cancellationToken = default)
    {
        var todoItems = await _context.TodoItems
            .OrderBy(t => t.IsCompleted)
            .ThenByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Retrieved {Count} todo items", todoItems.Count);

        return todoItems;
    }

    /// <inheritdoc />
    public async Task<TodoItem?> UpdateAsync(int id, string newText, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(newText))
            throw new ArgumentException("Todo text cannot be null or empty.", nameof(newText));

        if (newText.Length > 500)
            throw new ArgumentException("Todo text cannot exceed 500 characters.", nameof(newText));

        var todoItem = await _context.TodoItems.FindAsync([id], cancellationToken);
        if (todoItem == null)
        {
            _logger.LogWarning("Todo item with ID {TodoId} not found for update", id);
            return null;
        }

        var oldText = todoItem.Text;
        todoItem.Text = newText.Trim();
        todoItem.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated todo item {TodoId}: '{OldText}' -> '{NewText}'", 
            id, oldText, todoItem.Text);

        return todoItem;
    }

    /// <inheritdoc />
    public async Task<TodoItem?> CompleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var todoItem = await _context.TodoItems.FindAsync([id], cancellationToken);
        if (todoItem == null)
        {
            _logger.LogWarning("Todo item with ID {TodoId} not found for completion", id);
            return null;
        }

        if (todoItem.IsCompleted)
        {
            _logger.LogInformation("Todo item {TodoId} is already completed", id);
            return todoItem;
        }

        todoItem.IsCompleted = true;
        todoItem.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Completed todo item {TodoId}: {TodoText}", 
            id, todoItem.Text);

        return todoItem;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var todoItem = await _context.TodoItems.FindAsync([id], cancellationToken);
        if (todoItem == null)
        {
            _logger.LogWarning("Todo item with ID {TodoId} not found for deletion", id);
            return false;
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted todo item {TodoId}: {TodoText}", 
            id, todoItem.Text);

        return true;
    }

    /// <inheritdoc />
    public async Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var todoItem = await _context.TodoItems.FindAsync([id], cancellationToken);
        
        if (todoItem == null)
        {
            _logger.LogDebug("Todo item with ID {TodoId} not found", id);
        }
        else
        {
            _logger.LogDebug("Retrieved todo item {TodoId}: {TodoText}", 
                id, todoItem.Text);
        }

        return todoItem;
    }
}

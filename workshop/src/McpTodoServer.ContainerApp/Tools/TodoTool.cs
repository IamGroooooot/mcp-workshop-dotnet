using Microsoft.EntityFrameworkCore;
using McpTodoServer.ContainerApp.Data;
using McpTodoServer.ContainerApp.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpTodoServer.ContainerApp.Tools;

/// <summary>
/// Provides direct methods for managing TodoItem entities: create, list, update, complete, and delete.
/// Not registered for dependency injection.
/// </summary>
[McpServerToolType]
public class TodoTool
{
    private readonly TodoDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoTool"/> class.
    /// </summary>
    /// <param name="context">The database context to use.</param>
    public TodoTool(TodoDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Creates a new todo item.
    /// </summary>
    /// <param name="text">The text content of the todo item.</param>
    /// <returns>The created todo item.</returns>
    [McpServerTool(Name = "add_todo_item", Title = "Add a to-do item")]
    [Description("Adds a to-do item to database.")]
    public async Task<TodoItem> CreateAsync(string text)
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
        await _context.SaveChangesAsync();
        return todoItem;
    }

    /// <summary>
    /// Lists all todo items.
    /// </summary>
    /// <returns>A list of all todo items.</returns>
    [McpServerTool(Name = "get_todo_items", Title = "Get a list of to-do items")]
    [Description("Gets a list of to-do items from database.")]
    public async Task<List<TodoItem>> ListAsync()
    {
        return await _context.TodoItems
            .OrderBy(t => t.IsCompleted)
            .ThenByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    /// <summary>
    /// Updates the text of a todo item.
    /// </summary>
    /// <param name="id">The ID of the todo item to update.</param>
    /// <param name="newText">The new text content.</param>
    /// <returns>The updated todo item, or null if not found.</returns>
    [McpServerTool(Name = "update_todo_item", Title = "Update a to-do item")]
    [Description("Updates a to-do item in the database.")]
    public async Task<TodoItem?> UpdateAsync(int id, string newText)
    {
        if (string.IsNullOrWhiteSpace(newText))
            throw new ArgumentException("Todo text cannot be null or empty.", nameof(newText));
        if (newText.Length > 500)
            throw new ArgumentException("Todo text cannot exceed 500 characters.", nameof(newText));

        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
            return null;
        todoItem.Text = newText.Trim();
        todoItem.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return todoItem;
    }

    /// <summary>
    /// Marks a todo item as completed.
    /// </summary>
    /// <param name="id">The ID of the todo item to complete.</param>
    /// <returns>The completed todo item, or null if not found.</returns>
    [McpServerTool(Name = "complete_todo_item", Title = "Complete a to-do item")]
    [Description("Completes a to-do item in the database.")]
    public async Task<TodoItem?> CompleteAsync(int id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
            return null;
        if (!todoItem.IsCompleted)
        {
            todoItem.IsCompleted = true;
            todoItem.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        return todoItem;
    }

    /// <summary>
    /// Deletes a todo item.
    /// </summary>
    /// <param name="id">The ID of the todo item to delete.</param>
    /// <returns>True if the item was deleted, false if not found.</returns>
    [McpServerTool(Name = "delete_todo_item", Title = "Delete a to-do item")]
    [Description("Deletes a to-do item from the database.")]
    public async Task<bool> DeleteAsync(int id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
            return false;
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return true;
    }
}

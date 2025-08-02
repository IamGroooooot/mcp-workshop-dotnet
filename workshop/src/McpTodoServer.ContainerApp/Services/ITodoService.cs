using McpTodoServer.ContainerApp.Models;
using McpTodoServer.ContainerApp.Repositories;

namespace McpTodoServer.ContainerApp.Services;

/// <summary>
/// Interface for TodoItem service operations.
/// </summary>
public interface ITodoService
{
    /// <summary>
    /// Creates a new todo item.
    /// </summary>
    /// <param name="text">The text content of the todo item.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created todo item.</returns>
    Task<TodoItem> CreateTodoAsync(string text, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all todo items.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of all todo items.</returns>
    Task<IEnumerable<TodoItem>> GetAllTodosAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the text of a todo item.
    /// </summary>
    /// <param name="id">The ID of the todo item to update.</param>
    /// <param name="newText">The new text content.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated todo item, or null if not found.</returns>
    Task<TodoItem?> UpdateTodoAsync(int id, string newText, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks a todo item as completed.
    /// </summary>
    /// <param name="id">The ID of the todo item to complete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completed todo item, or null if not found.</returns>
    Task<TodoItem?> CompleteTodoAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a todo item.
    /// </summary>
    /// <param name="id">The ID of the todo item to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the item was deleted, false if not found.</returns>
    Task<bool> DeleteTodoAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a todo item by ID.
    /// </summary>
    /// <param name="id">The ID of the todo item.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The todo item, or null if not found.</returns>
    Task<TodoItem?> GetTodoByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets statistics about todo items.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Todo statistics.</returns>
    Task<TodoStatistics> GetTodoStatisticsAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Statistics about todo items.
/// </summary>
public record TodoStatistics(
    int TotalTodos,
    int CompletedTodos,
    int PendingTodos,
    double CompletionPercentage);

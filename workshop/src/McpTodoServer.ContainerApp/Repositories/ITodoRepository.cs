using McpTodoServer.ContainerApp.Models;

namespace McpTodoServer.ContainerApp.Repositories;

/// <summary>
/// Interface for TodoItem repository operations.
/// </summary>
public interface ITodoRepository
{
    /// <summary>
    /// Creates a new todo item.
    /// </summary>
    /// <param name="text">The text content of the todo item.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created todo item.</returns>
    Task<TodoItem> CreateAsync(string text, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all todo items.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of all todo items.</returns>
    Task<IEnumerable<TodoItem>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the text of a todo item.
    /// </summary>
    /// <param name="id">The ID of the todo item to update.</param>
    /// <param name="newText">The new text content.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated todo item, or null if not found.</returns>
    Task<TodoItem?> UpdateAsync(int id, string newText, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks a todo item as completed.
    /// </summary>
    /// <param name="id">The ID of the todo item to complete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completed todo item, or null if not found.</returns>
    Task<TodoItem?> CompleteAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a todo item.
    /// </summary>
    /// <param name="id">The ID of the todo item to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the item was deleted, false if not found.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a todo item by ID.
    /// </summary>
    /// <param name="id">The ID of the todo item.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The todo item, or null if not found.</returns>
    Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}

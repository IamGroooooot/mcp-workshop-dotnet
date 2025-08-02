using System.ComponentModel.DataAnnotations;

namespace McpTodoServer.ContainerApp.Models;

/// <summary>
/// Represents a todo item with ID, text content, and completion status.
/// </summary>
public class TodoItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the todo item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the text content of the todo item.
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the todo item is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the todo item was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the todo item was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

using McpTodoServer.ContainerApp.Models;
using McpTodoServer.ContainerApp.Repositories;

namespace McpTodoServer.ContainerApp.Services;

/// <summary>
/// Service implementation for TodoItem business logic operations.
/// </summary>
public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly ILogger<TodoService> _logger;

    public TodoService(ITodoRepository todoRepository, ILogger<TodoService> logger)
    {
        _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<TodoItem> CreateTodoAsync(string text, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new todo with text: {TodoText}", text);
        
        try
        {
            var todoItem = await _todoRepository.CreateAsync(text, cancellationToken);
            _logger.LogInformation("Successfully created todo with ID {TodoId}", todoItem.Id);
            return todoItem;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create todo with text: {TodoText}", text);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TodoItem>> GetAllTodosAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Retrieving all todos");
        
        try
        {
            var todos = await _todoRepository.ListAsync(cancellationToken);
            _logger.LogDebug("Successfully retrieved {TodoCount} todos", todos.Count());
            return todos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve todos");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<TodoItem?> UpdateTodoAsync(int id, string newText, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating todo {TodoId} with new text: {NewText}", id, newText);
        
        try
        {
            var updatedTodo = await _todoRepository.UpdateAsync(id, newText, cancellationToken);
            if (updatedTodo != null)
            {
                _logger.LogInformation("Successfully updated todo {TodoId}", id);
            }
            else
            {
                _logger.LogWarning("Todo {TodoId} not found for update", id);
            }
            return updatedTodo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update todo {TodoId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<TodoItem?> CompleteTodoAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Completing todo {TodoId}", id);
        
        try
        {
            var completedTodo = await _todoRepository.CompleteAsync(id, cancellationToken);
            if (completedTodo != null)
            {
                _logger.LogInformation("Successfully completed todo {TodoId}", id);
            }
            else
            {
                _logger.LogWarning("Todo {TodoId} not found for completion", id);
            }
            return completedTodo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to complete todo {TodoId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteTodoAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting todo {TodoId}", id);
        
        try
        {
            var deleted = await _todoRepository.DeleteAsync(id, cancellationToken);
            if (deleted)
            {
                _logger.LogInformation("Successfully deleted todo {TodoId}", id);
            }
            else
            {
                _logger.LogWarning("Todo {TodoId} not found for deletion", id);
            }
            return deleted;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete todo {TodoId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<TodoItem?> GetTodoByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Retrieving todo {TodoId}", id);
        
        try
        {
            var todo = await _todoRepository.GetByIdAsync(id, cancellationToken);
            if (todo != null)
            {
                _logger.LogDebug("Successfully retrieved todo {TodoId}", id);
            }
            else
            {
                _logger.LogDebug("Todo {TodoId} not found", id);
            }
            return todo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve todo {TodoId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<TodoStatistics> GetTodoStatisticsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Calculating todo statistics");
        
        try
        {
            var todos = await _todoRepository.ListAsync(cancellationToken);
            var todoList = todos.ToList();
            
            var totalTodos = todoList.Count;
            var completedTodos = todoList.Count(t => t.IsCompleted);
            var pendingTodos = totalTodos - completedTodos;
            var completionPercentage = totalTodos > 0 ? (double)completedTodos / totalTodos * 100 : 0;

            var statistics = new TodoStatistics(totalTodos, completedTodos, pendingTodos, completionPercentage);
            
            _logger.LogDebug("Successfully calculated todo statistics: {TotalTodos} total, {CompletedTodos} completed, {PendingTodos} pending",
                totalTodos, completedTodos, pendingTodos);
            
            return statistics;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to calculate todo statistics");
            throw;
        }
    }
}

using ChatAPI.Models;

namespace ChatAPI.Interfaces;

/// <summary>
/// Provides methods to save and retrieve chat connections.
/// </summary>
public interface IChatConnectionService
{
    /// <summary>
    /// Saves a connection to the database.
    /// </summary>
    /// <param name="connectionId">SignalR connection.</param>
    /// <param name="userConnection">User's connection details(username and chat room).</param>
    /// <returns>Task</returns>
    Task SaveConnectionAsync(string connectionId, UserConnection userConnection);

    /// <summary>
    /// Retrieves a connection from the database.
    /// </summary>
    /// <param name="connectionId">SignalR connection Id</param>
    /// <returns>Task, or <c>null</c> if not found.</returns>
    Task<ChatConnection> GetConnectionAsync(string connectionId);
}
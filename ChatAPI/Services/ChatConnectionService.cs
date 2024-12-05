using ChatAPI.Data;
using ChatAPI.Interfaces;
using ChatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Services;

/// <summary>
/// Manages the persistence and retrieval of user chat connections.
/// </summary>
public class ChatConnectionService : IChatConnectionService
{
    private readonly ApplicationDbContext _dbContext;

    public ChatConnectionService(ApplicationDbContext dbContext) => _dbContext = dbContext;

    /// <summary>
    /// Stores a user connection in the database.
    /// </summary>
    public async Task SaveConnectionAsync(string connectionId, UserConnection userConnection)
    {
        if (string.IsNullOrEmpty(connectionId))
            throw new ArgumentException("Connection ID is required", nameof(connectionId));

        var connection = new ChatConnection
        {
            ConnectionId = connectionId,
            UserName = userConnection.UserName,
            ChatRoom = userConnection.ChatRoom,
            ConnectedAt = DateTimeOffset.UtcNow
        };

        _dbContext.ChatConnections.Add(connection);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves a chat connection by ID.
    /// </summary>
    public async Task<ChatConnection> GetConnectionAsync(string connectionId)
    {
        if (string.IsNullOrEmpty(connectionId))
            throw new ArgumentException("Connection ID is required", nameof(connectionId));

        return await _dbContext.ChatConnections
            .FirstOrDefaultAsync(c => c.ConnectionId == connectionId);
    }
}

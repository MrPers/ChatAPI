using ChatAPI.Data;
using ChatAPI.Interfaces;
using ChatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Services;

/// <summary>
/// Handles message persistence and retrieval for chat rooms.
/// </summary>
public class ChatMessageService : IChatMessageService
{
    private readonly ApplicationDbContext _dbContext;

    public ChatMessageService(ApplicationDbContext dbContext) => _dbContext = dbContext;

    /// <summary>
    /// Retrieves recent messages for a given chat room.
    /// </summary>
    public async Task<List<ChatMessage>> GetRecentMessagesAsync(string chatRoom)
    {
        if (string.IsNullOrWhiteSpace(chatRoom))
            throw new ArgumentException("Chat room is required", nameof(chatRoom));

        return await _dbContext.ChatMessages
            .Where(m => m.ChatRoom == chatRoom)
            .OrderByDescending(m => m.SentAt)
            .Take(50)
            .ToListAsync();
    }

    /// <summary>
    /// Saves a new message to the database and returns it.
    /// </summary>
    public async Task<ChatMessage> SaveMessageAsync(ChatConnection connection, string message, string sentiment)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message cannot be empty", nameof(message));

        var chatMessage = new ChatMessage
        {
            ChatRoom = connection.ChatRoom,
            UserName = connection.UserName,
            MessageText = message,
            Sentiment = sentiment,
            SentAt = DateTimeOffset.UtcNow
        };

        _dbContext.ChatMessages.Add(chatMessage);
        await _dbContext.SaveChangesAsync();

        return chatMessage;
    }
}

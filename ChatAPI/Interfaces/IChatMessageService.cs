using ChatAPI.Models;

namespace ChatAPI.Interfaces;

/// <summary>
/// Handles retrieving recent messages and saving new.
/// </summary>
public interface IChatMessageService
{
    /// <summary>
    /// Retrieves recent messages from the database.
    /// </summary>
    /// <param name="chatRoom">Name of the chat to retrieve messages from.</param>
    /// <returns>Task that returns list of recent messages</returns>
    Task<List<ChatMessage>> GetRecentMessagesAsync(string chatRoom);

    /// <summary>
    /// Saves a message to the database.
    /// </summary>
    /// <param name="connection">The connection details of the user sending the message.</param>
    /// <param name="message">Content of the chat message.</param>
    /// <param name="sentiment">The sentiment of the message (e.g., Positive, Neutral, Negative).</param>
    /// <returns>Task that returns saved chat messages</returns>
    Task<ChatMessage> SaveMessageAsync(ChatConnection connection, string message, string sentiment);
}
namespace ChatAPI.Interfaces;

/// <summary>
/// Defines methods for receiving messages.
/// </summary>
public interface IChatClient
{
    /// <summary>
    /// Send a message to the client.
    /// </summary>
    /// <param name="userName">Name of the user that sends the message.</param>
    /// <param name="message">Content of the chat message.</param>
    /// <returns>Task</returns>
    Task ReceiveMessage(string userName, string message);

    /// <summary>
    /// Sends a message to the client with sentiment analysis.
    /// </summary>
    /// <param name="userName">Name of the user that sends the message.</param>
    /// <param name="message">Content of the chat message.</param>
    /// <param name="sentiment">The sentiment of the message (e.g., Positive, Neutral, Negative).</param>
    /// <returns>Task</returns>
    Task ReceiveMessageWithSentiment(string userName, string message, string sentiment);
}
namespace ChatAPI.Models;

/// <summary>
/// Represents a message sent in a chat room.
/// Stores details about the message content, sender, sentiment analysis, and timestamp.
/// </summary>
public class ChatMessage
{
    /// <summary>
    /// Gets or sets the unique identifier for the chat message.
    /// This is typically used as the primary key in the database.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the chat room where the message was sent.
    /// </summary>
    public string ChatRoom { get; set; }

    /// <summary>
    /// Gets or sets the username of the user who sent the message.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the content of the chat message.
    /// </summary>
    public string MessageText { get; set; }

    /// <summary>
    /// Gets or sets the sentiment analysis result for the message.
    /// For example: "Positive", "Neutral", or "Negative".
    /// </summary>
    public string Sentiment { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the message was sent.
    /// Defaults to the current UTC time when the message is created.
    /// </summary>
    public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;
}
namespace ChatAPI.Models;

/// <summary>
/// Represents a user's connection to a chat system.
/// Stores details about the user's SignalR connection, associated chat room, and connection time.
/// </summary>
public class ChatConnection
{
    /// <summary>
    /// Gets or sets the unique identifier for the chat connection.
    /// This is typically used as the primary key in the database.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the SignalR connection ID associated with the user's session.
    /// This ID is used to uniquely identify the user's connection in the SignalR system.
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// Gets or sets the username of the user connected to the chat system.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the name of the chat room the user has joined.
    /// </summary>
    public string ChatRoom { get; set; }

    /// <summary>
    /// Gets or sets the timestamp indicating when the user connected to the chat room.
    /// </summary>
    public DateTimeOffset ConnectedAt { get; set; }
}
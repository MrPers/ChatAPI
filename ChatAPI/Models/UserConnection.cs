namespace ChatAPI.Models;

/// <summary>
/// Represents a user's connection to a specific chat room.
/// </summary>
/// <param name="UserName">The name of the user connected to the chat room.</param>
/// <param name="ChatRoom">The name of the chat room the user has joined.</param>
public record UserConnection(string UserName, string ChatRoom);
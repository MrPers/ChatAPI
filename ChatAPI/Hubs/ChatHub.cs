using ChatAPI.Data;
using ChatAPI.Interfaces;
using ChatAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs;

/// <summary>
/// SignalR hub for real-time chat functionality.
/// Manages user connections, messages, and sentiment analysis.
/// </summary>
/// <param name="logger"></param>
/// <param name="chatMessageService"></param>
/// <param name="textAnalyticsService"></param>
/// <param name="chatConnectionService"></param>
public class ChatHub(
    ILogger<ChatHub> logger,
    IChatMessageService chatMessageService,
    ITextAnalyticsService textAnalyticsService,
    IChatConnectionService chatConnectionService)
    : Hub<IChatClient>
{
    /// <summary>
    /// Allows user to join a chat room.
    /// </summary>
    /// <param name="userConnection">Details about user's connection</param>
    public async Task JoinChat(UserConnection userConnection)
    {
        try
        {
            logger.LogInformation("User {UserName} is joining chat room {ChatRoom}.", userConnection.UserName,
                userConnection.ChatRoom);

            // Add user to the specified SignalR group
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.ChatRoom);

            // Save user's connection details to the database
            await chatConnectionService.SaveConnectionAsync(Context.ConnectionId, userConnection);

            // Get recent messages from the database
            var recentMessages = await chatMessageService.GetRecentMessagesAsync(userConnection.ChatRoom);

            // Send the recent messages to the caller
            foreach (var message in recentMessages)
            {
                await Clients.Caller.ReceiveMessageWithSentiment(
                    message.UserName,
                    message.MessageText,
                    message.Sentiment
                );
            }

            logger.LogInformation("User {UserName} successfully joined chat room {ChatRoom}.", userConnection.UserName,
                userConnection.ChatRoom);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while user {UserName} was joining chat room {ChatRoom}.",
                userConnection.UserName, userConnection.ChatRoom);
            await Clients.Caller.ReceiveMessage("System", "An error occurred while joining the ChatAPI.");
        }
    }

    /// <summary>
    /// Allows user to send a message to the chat room.
    /// </summary>
    /// <param name="message">Message that user want to send</param>
    public async Task SendMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            await Clients.Caller.ReceiveMessage("System", "Message cannot be empty.");
            return;
        }

        var connection = await chatConnectionService.GetConnectionAsync(Context.ConnectionId);

        if (connection == null)
        {
            await Clients.Caller.ReceiveMessage("System", "Connection not found. Unable to send the message.");
            return;
        }

        try
        {
            var sentiment = await textAnalyticsService.AnalyzeSentimentAsync(message);
            var savedMessage = await chatMessageService.SaveMessageAsync(connection, message, sentiment);

            await Clients.Group(connection.ChatRoom).ReceiveMessageWithSentiment(
                savedMessage.UserName, savedMessage.MessageText, savedMessage.Sentiment);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while sending message from user {UserName} in room {ChatRoom}.",
                connection.UserName, connection.ChatRoom);

            await Clients.Caller.ReceiveMessage("System", "An error occurred while sending the message.");
        }
    }
}
namespace ChatAPI.Interfaces;

/// <summary>
/// Provides methods to analyze text sentiment.
/// </summary>
public interface ITextAnalyticsService
{
    /// <summary>
    /// Analyzes the sentiment of a message.
    /// </summary>
    /// <param name="message">Content of the chat message.</param>
    /// <returns>Task of the sentiment of the message (e.g., Positive, Neutral, Negative).</returns>
    public Task<string> AnalyzeSentimentAsync(string message);
}
using Azure;
using Azure.AI.TextAnalytics;
using ChatAPI.Interfaces;
using ChatAPI.Models;
using Microsoft.Extensions.Options;

namespace ChatAPI.Services;

/// <summary>
/// Service for performing text analysis, leveraging Azure Text Analytics.
/// </summary>
public class TextAnalyticsService : ITextAnalyticsService
{
    private readonly TextAnalyticsClient _textAnalyticsClient;

    /// <summary>
    /// Initializes the Text Analytics Service with configured credentials.
    /// </summary>
    /// <param name="settings">Settings for Azure Text Analytics, provided via dependency injection.</param>
    public TextAnalyticsService(IOptions<AzureTextAnalyticsSettings> settings)
    {
        var credentials = new AzureKeyCredential(settings.Value.ApiKey);
        _textAnalyticsClient = new TextAnalyticsClient(new Uri(settings.Value.Endpoint), credentials);
    }

    /// <summary>
    /// Analyzes the sentiment of the provided text message.
    /// </summary>
    /// <param name="message">The text to analyze.</param>
    /// <returns>Sentiment classification: Positive, Neutral, or Negative.</returns>
    public async Task<string> AnalyzeSentimentAsync(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message cannot be null or empty", nameof(message));

        var result = await _textAnalyticsClient.AnalyzeSentimentAsync(message);
        return result.Value.Sentiment.ToString();
    }
}

# Real-Time Chat

This is a real-time chat application built with ASP.NET Core, SignalR, and Azure Text Analytics. The application allows users to join chat rooms, send messages, and analyze the sentiment of messages in real-time.

## Features

- Real-time messaging using SignalR
- Sentiment analysis of messages using Azure Text Analytics
- User connection management
- Recent message retrieval

## Prerequisites

- .NET 6.0 SDK or later
- Azure account with Text Analytics and SignalR services
- SQL Server database

## Configuration

1. Update the `appsettings.json` file with your Azure and database connection details:
    ```json
    {
      "ConnectionStrings": {
        "SignalConnection": "Your SignalR connection string",
        "ChatDatabase": "Your SQL Database connection string",
        "AzureAppConfig": "Your Azure App Configuration connection string"
      },
      "AzureTextAnalytics": {
        "Endpoint": "Your Azure Text Analytics endpoint",
        "ApiKey": "Your Azure Text Analytics API key"
      }
    }
    ```

2. Ensure your Azure App Configuration is set up correctly and contains the necessary settings.

## Running the Application

1. Restore the dependencies:
    ```sh
    dotnet restore
    ```

2. Update the database:
    ```sh
    dotnet ef database update
    ```

3. Run the application:
    ```sh
    dotnet run
    ```

## Deployment

The application is deployed to Azure. You can access it [here](https://proud-sky-03c41bc03.4.azurestaticapps.net/).

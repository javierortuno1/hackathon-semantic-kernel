using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

// Populate values from your OpenAI deployment
var modelId = "";
var endpoint = "";
var apiKey = "";

// Create kernel builder and configure services
var builder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

// Add logging and HTTP client
builder.Services.AddLogging(c => c.AddConsole().SetMinimumLevel(LogLevel.Error))
               .AddHttpClient();

// Build the kernel
var kernel = builder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

var httpClientFactory = kernel.GetRequiredService<IHttpClientFactory>();
var myApiPlugin = new MyApiPlugin(httpClientFactory.CreateClient());
kernel.Plugins.AddFromObject(myApiPlugin, "SchufaApi");

// Enable planning
var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings 
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

// Create chat history
var history = new ChatHistory();
history.AddSystemMessage("I am an AI assistant that can help check customer Schufa data. You can ask me about customer information using their name.");

// Initiate chat loop
string? userInput;
do {
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (!string.IsNullOrEmpty(userInput))
    {
        // Add user input to history
        history.AddUserMessage(userInput);

        // Get AI response
        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            executionSettings: openAIPromptExecutionSettings,
            kernel: kernel);

        // Print results
        Console.WriteLine("Assistant > " + result);

        // Add assistant's message to history
        history.AddMessage(result.Role, result.Content ?? string.Empty);
    }
} while (userInput is not null);
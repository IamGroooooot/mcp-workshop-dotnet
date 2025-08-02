using System.ClientModel;

using McpTodoClient.BlazorApp.Components;

using Microsoft.Extensions.AI;

using OpenAI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

var config = builder.Configuration;

// Check for required configuration and provide helpful error messages
var token = config["GitHubModels:Token"];
var endpoint = config["GitHubModels:Endpoint"];
var modelId = config["GitHubModels:ModelId"];

if (string.IsNullOrEmpty(token))
{
    throw new InvalidOperationException(
        "Missing configuration: GitHubModels:Token. " +
        "Please set this value in user secrets using: " +
        "dotnet user-secrets set \"GitHubModels:Token\" \"your-github-token-here\"");
}

if (string.IsNullOrEmpty(endpoint))
{
    throw new InvalidOperationException("Missing configuration: GitHubModels:Endpoint.");
}

if (string.IsNullOrEmpty(modelId))
{
    throw new InvalidOperationException("Missing configuration: GitHubModels:ModelId.");
}

var credential = new ApiKeyCredential(token);
var options = new OpenAIClientOptions()
{
    Endpoint = new Uri(endpoint)
};

var openAIClient = new OpenAIClient(credential, options);
var chatClient = openAIClient.GetChatClient(modelId).AsIChatClient();

builder.Services.AddChatClient(chatClient)
                .UseFunctionInvocation()
                .UseLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.UseStaticFiles();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();

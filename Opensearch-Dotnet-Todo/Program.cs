using System.Text.Json;
using OpenSearch.Net;
using Opensearch_Dotnet_Todo.models;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Opensearch_Dotnet_Todo;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0,
        Opensearch_Dotnet_Todo.AppJsonSerializerContext.Default);
});

builder.Services.AddSingleton<IOpenSearchLowLevelClient>(sp =>
{
    var nodeAddress = new Uri("https://localhost:9200");
    var config = new ConnectionConfiguration(nodeAddress)
        .BasicAuthentication("admin", "C#abcd@1234")
        .DisableDirectStreaming()
        .EnableDebugMode()
        .RequestTimeout(TimeSpan.FromMinutes(2))
        .ServerCertificateValidationCallback((o, certificate, chain, errors) => true); // Accept any certificate
    return new OpenSearchLowLevelClient(config);
});

var app = builder.Build();

var opensearchApi = app.MapGroup("/opensearch");
opensearchApi.MapGet("/indices", async (IOpenSearchLowLevelClient client) =>
{
    var response = await client.Cat.IndicesAsync<StringResponse>();
    return response.Body;
});

var studentsApi = app.MapGroup("/students");
studentsApi.MapGet("/", async (IOpenSearchLowLevelClient client) =>
{
    try
    {
        // Create proper query to get all documents using our model classes
        var searchRequest = new SearchRequest
        {
            Query = new QueryContainer
            {
                MatchAll = new MatchAllQuery()
            },
            Size = 100
        };
        
        // Manually serialize the search request to avoid dynamic code generation
        var jsonString = JsonSerializer.Serialize(searchRequest, 
            AppJsonSerializerContext.Default.SearchRequest);
        
        var searchResponse = await client.SearchAsync<StringResponse>("students", 
            PostData.String(jsonString));
        
        // Check if the response is successful and contains data
        if (!searchResponse.Success)
        {
            return Results.Problem($"Failed to retrieve data from OpenSearch: {searchResponse.OriginalException?.Message}", 
                statusCode: 500);
        }
        
        // Log the raw response for debugging
        Console.WriteLine($"OpenSearch response: {searchResponse.Body}");
        
        try
        {
            var responseJson = JsonDocument.Parse(searchResponse.Body);
            var hits = responseJson.RootElement.GetProperty("hits").GetProperty("hits");
            return Results.Ok(hits.ToString());
        }
        catch (JsonException ex)
        {
            return Results.Problem($"Error parsing JSON response: {ex.Message}", 
                statusCode: 500);
        }
    }
    catch (Exception ex)
    {
        return Results.Problem($"OpenSearch request failed: {ex.Message}", 
            statusCode: 500);
    }
});

app.MapGet("/", () => { return "Hello World!"; });

app.Run();
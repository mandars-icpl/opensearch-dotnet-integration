using System.Text.Json.Serialization;

namespace Opensearch_Dotnet_Todo.models;

public class SearchRequest
{
    [JsonPropertyName("query")]
    public QueryContainer Query { get; set; } = new();
    
    [JsonPropertyName("size")]
    public int Size { get; set; } = 100;
}

public class QueryContainer
{
    [JsonPropertyName("match_all")]
    public MatchAllQuery MatchAll { get; set; } = new();
}

public class MatchAllQuery
{
    // Empty object for match_all query
}

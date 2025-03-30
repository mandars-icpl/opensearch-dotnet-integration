using System.Text.Json.Serialization;

namespace Opensearch_Dotnet_Todo.models;

public class Student
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    
    [JsonPropertyName("FirstName")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("LastName")]
    public string LastName { get; set; }
    
    [JsonPropertyName("GradYear")]
    public int GradYear { get; set; }
    
    [JsonPropertyName("Gpa")]
    public double Gpa { get; set; }
}
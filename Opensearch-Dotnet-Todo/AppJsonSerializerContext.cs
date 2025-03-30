using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Opensearch_Dotnet_Todo.models;

namespace Opensearch_Dotnet_Todo;

[JsonSerializable(typeof(Student))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(ProblemDetails))]
[JsonSerializable(typeof(SearchRequest))]
[JsonSerializable(typeof(QueryContainer))]
[JsonSerializable(typeof(MatchAllQuery))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
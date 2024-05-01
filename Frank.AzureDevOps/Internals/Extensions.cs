using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;

namespace Frank.AzureDevOps;

public static class Extensions
{
    public static Guid ToGuid(this string value) => Guid.Parse(value);
}

public static class BuidHttpClientExtensions
{
    public static async Task<IEnumerable<BuildDefinition>> GetBuildDefinitionsAsync(this BuildHttpClient client, params TeamProjectReference[] projects)
    {
        var definitions = new List<BuildDefinition>();
        foreach (var project in projects)
        {
            definitions.AddRange(await client.GetFullDefinitionsAsync(project.Id));
        }
        return definitions;
    }
    
    public static async Task<IEnumerable<BuildDefinitionReference>> GetBuildReferencesAsync(this BuildHttpClient client, params TeamProjectReference[] projects)
    {
        var definitions = new List<BuildDefinitionReference>();
        foreach (var project in projects)
        {
            definitions.AddRange(await client.GetDefinitionsAsync(project.Id));
        }
        return definitions;
    }
}
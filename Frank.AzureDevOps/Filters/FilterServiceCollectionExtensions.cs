using Microsoft.Extensions.DependencyInjection;

namespace Frank.AzureDevOps;

public static class FilterServiceCollectionExtensions
{
    public static IServiceCollection AddDevOpsClientProjectExclusionFilters(this IServiceCollection services, params ProjectFilterExclusionItem[] projectFilterExclusionItems)
    {
        foreach (var filterExclusionItem in projectFilterExclusionItems) 
            services.AddSingleton<IProjectFilterExclusionItem>(filterExclusionItem);
        return services;
    }
    
    public static IServiceCollection AddDevOpsClientRepositoryExclusionFilters(this IServiceCollection services, params RepositoryFilterExclusionItem[] repositoryFilterExclusionItems)
    {
        foreach (var filterExclusionItem in repositoryFilterExclusionItems) 
            services.AddSingleton<IRepositoryFilterExclusionItem>(filterExclusionItem);
        return services;
    }
    
    public static IServiceCollection AddDevOpsClientBuildDefinitionExclusionFilters(this IServiceCollection services, params BuildDefinitionFilterExclusionItem[] buildDefinitionFilterExclusionItems)
    {
        foreach (var filterExclusionItem in buildDefinitionFilterExclusionItems) 
            services.AddSingleton<IBuildDefinitionFilterExclusionItem>(filterExclusionItem);
        return services;
    }
}
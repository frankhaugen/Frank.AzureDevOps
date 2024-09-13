using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.AzureDevOps;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFrankDevOpsClient(this IServiceCollection services, Action<AzureDevOpsCredentials> configure)
    {
        services.Configure(configure);
        services.AddSingleton<IVssConnectionFactory, VssConnectionFactory>();
        services.AddSingleton<IDevOpsClient, DevOpsClient>();
        
        return services;
    }
    
    public static IServiceCollection AddFrankDevOpsClient(this IServiceCollection services, IConfiguration configuration) 
    {
        services.Configure<AzureDevOpsCredentials>(configuration.GetSection(nameof(AzureDevOpsCredentials)));
        services.AddSingleton<IVssConnectionFactory, VssConnectionFactory>();
        services.AddSingleton<IDevOpsClient, DevOpsClient>();
        
        return services;
    }
    
    public static IServiceCollection AddFrankDevOpsClient(this IServiceCollection services, string url, string personalAccessToken)
    {
        services.Configure<AzureDevOpsCredentials>(options =>
        {
            options.OrganizationUrl = new Uri(url);
            options.PersonalAccessToken = personalAccessToken;
        });
        services.AddSingleton<IVssConnectionFactory, VssConnectionFactory>();
        services.AddSingleton<IDevOpsClient, DevOpsClient>();
        
        return services;
    }
    
    public static IServiceCollection AddFrankDevOpsClient(this IServiceCollection services, AzureDevOpsCredentials credentials)
    {
        services.AddSingleton(credentials);
        services.AddSingleton<IVssConnectionFactory, VssConnectionFactory>();
        services.AddSingleton<IDevOpsClient, DevOpsClient>();
        
        return services;
    }
}
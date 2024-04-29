using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.Account.Client;
using Microsoft.VisualStudio.Services.Identity.Client;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.WebApi;

namespace Frank.AzureDevOps;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddDevOpsClient(this IServiceCollection services, string url, string personalAccessToken)
    {
        services.Configure<AzureDevOpsCredentials>(options =>
        {
            options.Url = url;
            options.PersonalAccessToken = personalAccessToken;
        });
        services.AddDevOpsClientInternal();
        return services;
    }

    public static IServiceCollection AddDevOpsClient(this IServiceCollection services, Action<AzureDevOpsCredentials> configure)
    {
        services.Configure(configure);
        services.AddDevOpsClientInternal();
        return services;
    }
    
    public static IServiceCollection AddDevOpsClient(this IServiceCollection services, IConfiguration configuration) 
    {
        services.Configure<AzureDevOpsCredentials>(configuration.GetSection(nameof(AzureDevOpsCredentials)));
        services.AddDevOpsClientInternal();
        return services;
    }

    private static IServiceCollection AddDevOpsClientInternal(this IServiceCollection services)
    {
        // Register clients
        services.AddSingleton<IDevOpsGitClient, DevOpsGitClient>();
        services.AddSingleton<IDevOpsProjectClient, DevOpsProjectClient>();
        services.AddSingleton<IDevOpsBuildClient, DevOpsBuildClient>();
        services.AddSingleton<IDevOpsReleaseClient, DevOpsReleaseClient>();
        services.AddSingleton<IDevOpsBuildPipelineClient, DevOpsBuildPipelineClient>();
        services.AddSingleton<IDevOpsPullRequestClient, DevOpsPullRequestClient>();

        // Register factories
        services.AddSingleton<ICredentialsFactory, CredentialsFactory>();
        services.AddSingleton<IVssConnectionFactory, VssConnectionFactory>();
        services.AddSingleton<IDevOpsClientFactory, DevOpsClientFactory>();

        // Factory pattern to in DI container
        services.AddSingleton<DevOpsPatCredentials>(provider => provider.GetRequiredService<ICredentialsFactory>().GetCredentials());
        services.AddSingleton<VssConnection>(provider => provider.GetRequiredService<IVssConnectionFactory>().GetConnection());
        services.AddSingleton<GitHttpClient>(provider => provider.GetRequiredService<IDevOpsClientFactory>().Git);
        services.AddSingleton<BuildHttpClient>(provider => provider.GetRequiredService<IDevOpsClientFactory>().Builds);
        services.AddSingleton<ProjectHttpClient>(provider => provider.GetRequiredService<IDevOpsClientFactory>().Projects);
        services.AddSingleton<ReleaseHttpClient>(provider => provider.GetRequiredService<IDevOpsClientFactory>().Releases);
        services.AddSingleton<WorkHttpClient>(provider => provider.GetRequiredService<IDevOpsClientFactory>().Work);
        services.AddSingleton<IIdentityHttpClient>(provider => provider.GetRequiredService<IDevOpsClientFactory>().Identity);
        services.AddSingleton<AccountHttpClient>(provider => provider.GetRequiredService<IDevOpsClientFactory>().Account);

        return services;
    }
}
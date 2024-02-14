using Microsoft.Extensions.Options;

namespace Frank.AzureDevOps;

public class CredentialsFactory : ICredentialsFactory
{
    private readonly IOptions<AzureDevOpsCredentials> _options;

    public DevOpsPatCredentials GetCredentials() => new DevOpsPatCredentials(new Uri(_options.Value.Url ?? throw new InvalidOperationException("Azure DevOps URL is not set")), _options.Value.PersonalAccessToken ?? throw new InvalidOperationException("Azure DevOps PAT is not set"));
}
using Azure.Core;
using Azure.Identity;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.OAuth;
using Microsoft.VisualStudio.Services.WebApi;

namespace Frank.AzureDevOps;

/// <summary>
/// Implementation of IVssConnectionFactory that selects the best authentication method.
/// </summary>
public class VssConnectionFactory : IVssConnectionFactory
{
    private readonly Uri _organizationUrl;
    private readonly string? _personalAccessToken;

    /// <summary>
    /// Initializes a new instance of the <see cref="VssConnectionFactory"/> class.
    /// </summary>
    /// <param name="organizationUrl">The Azure DevOps organization URL.</param>
    /// <param name="personalAccessToken">Optional personal access token.</param>
    public VssConnectionFactory(Uri organizationUrl, string? personalAccessToken = null)
    {
        _organizationUrl = organizationUrl;
        _personalAccessToken = personalAccessToken;
    }

    /// <summary>
    /// Gets a VssConnection instance using the best available authentication method.
    /// </summary>
    /// <returns>A VssConnection instance or null if unable to create one.</returns>
    public VssConnection? GetConnection()
    {
        // Try Azure AD authentication first
        try
        {
            var credential = new DefaultAzureCredential();
            var accessToken = credential.GetToken(
                new TokenRequestContext(new[] { "499b84ac-1321-427f-aa17-267ca6975798/.default" }));
            var vssCredentials = new VssOAuthAccessTokenCredential(accessToken.Token);

            return new VssConnection(_organizationUrl, vssCredentials);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Azure AD authentication failed: {ex.Message}");
        }

        // Fallback to Personal Access Token (PAT)
        if (!string.IsNullOrEmpty(_personalAccessToken))
        {
            try
            {
                var vssCredentials = new VssBasicCredential(string.Empty, _personalAccessToken);
                return new VssConnection(_organizationUrl, vssCredentials);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PAT authentication failed: {ex.Message}");
            }
        }

        // Unable to create a connection
        Console.WriteLine("Failed to create a VssConnection using available authentication methods.");
        return null;
    }
}
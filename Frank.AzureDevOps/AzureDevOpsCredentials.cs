namespace Frank.AzureDevOps;

/// <summary>
/// Provides Azure DevOps credentials for authentication.
/// </summary>
public class AzureDevOpsCredentials
{
    /// <summary>
    /// Gets or sets the Azure DevOps organization URL.
    /// </summary>
    public Uri OrganizationUrl { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the personal access token for authentication.
    /// </summary>
    /// <remarks>If not set, the default Azure AD authentication will be used.</remarks>
    public string? PersonalAccessToken { get; set; }
}
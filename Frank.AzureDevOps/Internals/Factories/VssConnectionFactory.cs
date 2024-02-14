using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace Frank.AzureDevOps;

public class VssConnectionFactory(DevOpsPatCredentials credentials) : IVssConnectionFactory
{
    public VssConnection GetConnection() => new(credentials.Instance, new VssBasicCredential(string.Empty, credentials.PAT));
}
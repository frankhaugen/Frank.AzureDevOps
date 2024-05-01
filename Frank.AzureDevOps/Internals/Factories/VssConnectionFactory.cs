using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace Frank.AzureDevOps;

public class VssConnectionFactory(DevOpsPatCredentials credentials) : IVssConnectionFactory
{
    public IVssConnection? GetConnection() => new VssConnection(credentials.Instance, new VssBasicCredential(string.Empty, credentials.PAT));
}
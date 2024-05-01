using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.Account.Client;
using Microsoft.VisualStudio.Services.Identity.Client;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.WebApi;

namespace Frank.AzureDevOps;

public class DevOpsClientFactory(IVssConnection connection) : IDevOpsClientFactory
{
    public GitHttpClient Git => connection.GetClient<GitHttpClient>();
    public BuildHttpClient Builds => connection.GetClient<BuildHttpClient>();
    public ProjectHttpClient Projects => connection.GetClient<ProjectHttpClient>();
    public ReleaseHttpClient Releases => connection.GetClient<ReleaseHttpClient>();
    public WorkHttpClient Work => connection.GetClient<WorkHttpClient>();
    public IdentityHttpClient Identity => connection.GetClient<IdentityHttpClient>();
    public AccountHttpClient Account => connection.GetClient<AccountHttpClient>();
}
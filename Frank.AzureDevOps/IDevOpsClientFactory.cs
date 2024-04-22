using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.Account.Client;
using Microsoft.VisualStudio.Services.Identity.Client;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace Frank.AzureDevOps;

public interface IDevOpsClientFactory
{
    GitHttpClient Git { get; }
    BuildHttpClient Builds { get; }
    ProjectHttpClient Projects { get; }
    ReleaseHttpClient Releases { get; }
    WorkHttpClient Work { get; }
    IdentityHttpClient Identity { get; }
    AccountHttpClient Account { get; }
}
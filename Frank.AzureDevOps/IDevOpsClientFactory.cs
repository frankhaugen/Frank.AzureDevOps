using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace Frank.AzureDevOps;

public interface IDevOpsClientFactory
{
    GitHttpClient Git { get; }
    BuildHttpClient Builds { get; }
    ProjectHttpClient Projects { get; }
    ReleaseHttpClient Releases { get; }
    WorkHttpClient Work { get; }
}
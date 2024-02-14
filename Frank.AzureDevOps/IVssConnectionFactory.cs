using Microsoft.VisualStudio.Services.WebApi;

namespace Frank.AzureDevOps;

public interface IVssConnectionFactory
{
    VssConnection GetConnection();
}
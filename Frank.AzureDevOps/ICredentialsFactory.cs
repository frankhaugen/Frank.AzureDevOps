namespace Frank.AzureDevOps;

public interface ICredentialsFactory
{
    DevOpsPatCredentials GetCredentials();
}
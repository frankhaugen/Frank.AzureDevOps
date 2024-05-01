namespace Frank.AzureDevOps;

public interface IFilterItemGuid
{
    Guid? Id { get; }
    
    string? Name { get; }
}
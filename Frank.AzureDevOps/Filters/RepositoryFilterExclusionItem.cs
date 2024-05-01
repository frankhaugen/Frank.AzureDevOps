namespace Frank.AzureDevOps;

public class RepositoryFilterExclusionItem : IRepositoryFilterExclusionItem
{
    public RepositoryFilterExclusionItem(Guid id)
    {
        Id = id;
        Name = null;
    }
    
    public RepositoryFilterExclusionItem(string name)
    {
        Id = null;
        Name = name;
    }
    
    public Guid? Id { get; }

    public string? Name { get; }
}
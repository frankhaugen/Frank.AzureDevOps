namespace Frank.AzureDevOps;

public class ProjectFilterExclusionItem : IProjectFilterExclusionItem
{
    public ProjectFilterExclusionItem(Guid id)
    {
        Id = id;
        Name = null;
    }
    
    public ProjectFilterExclusionItem(string name)
    {
        Id = null;
        Name = name;
    }
    
    public Guid? Id { get; }

    public string? Name { get; }
}
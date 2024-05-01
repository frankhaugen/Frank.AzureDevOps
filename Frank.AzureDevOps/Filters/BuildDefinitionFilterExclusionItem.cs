namespace Frank.AzureDevOps;

public class BuildDefinitionFilterExclusionItem : IBuildDefinitionFilterExclusionItem
{
    public BuildDefinitionFilterExclusionItem(int id)
    {
        Id = id;
        Name = null;
    }
    
    public BuildDefinitionFilterExclusionItem(string name)
    {
        Id = null;
        Name = name;
    }
    
    public int? Id { get; }

    public string? Name { get; }
}
namespace Frank.AzureDevOps;

public class FilterContainer
{
    public FilterContainer(
        IEnumerable<IProjectFilterExclusionItem> projectFilterExclusions, 
        IEnumerable<IRepositoryFilterExclusionItem> repositoryFilterExclusions, 
        IEnumerable<IBuildDefinitionFilterExclusionItem> buildDefinitionFilterExclusions, 
        IEnumerable<IReleaseDefinitionFilterExclusionItem> releaseDefinitionFilterExclusions)
    {
        ProjectFilterExclusions = projectFilterExclusions.ToList();
        RepositoryFilterExclusions = repositoryFilterExclusions.ToList();
        BuildDefinitionFilterExclusions = buildDefinitionFilterExclusions.ToList();
        ReleaseDefinitionFilterExclusions = releaseDefinitionFilterExclusions.ToList();
    }

    public List<IProjectFilterExclusionItem> ProjectFilterExclusions { get; }

    public List<IRepositoryFilterExclusionItem> RepositoryFilterExclusions { get; }
    
    public List<IBuildDefinitionFilterExclusionItem> BuildDefinitionFilterExclusions { get; }
    
    public List<IReleaseDefinitionFilterExclusionItem> ReleaseDefinitionFilterExclusions { get; }
}
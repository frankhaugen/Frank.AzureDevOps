namespace Frank.AzureDevOps;

public static class Extensions
{
    public static Guid ToGuid(this string value) => Guid.Parse(value);
}
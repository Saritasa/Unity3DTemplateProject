/// <summary>
/// Extension methods for GitVersionVariables
/// </summary>
public static class GitVersionVariablesExtensions
{
    /// <summary>
    /// Generate version string for application
    /// </summary>
    /// <param name="versionVariables"></param>
    /// <returns></returns>
    public static string GetVersionString(this GitVersionVariables versionVariables)
    {
        return string.Format("{0}+{1}",
            versionVariables.FullSemVer,
            versionVariables.ShortSha);
    }
}

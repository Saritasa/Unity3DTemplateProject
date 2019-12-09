using System;

/// <summary>
/// This structure holds input of GitVersion.exe tool (stripped)
/// </summary>
[Serializable]
public class GitVersionVariables
{
    /// <summary>
    /// Semantic Versioning string for current build/release http://semver.org/
    /// </summary>
    public string SemVer;

    /// <summary>
    /// Full version of SemVer
    /// </summary>
    public string FullSemVer;

    /// <summary>
    /// Legacy version of SemVer
    /// </summary>
    public string LegacySemVer;

    /// <summary>
    /// All info about build
    /// </summary>
    public string FullBuildMetaData;

    /// <summary>
    /// All info about version
    /// </summary>
    public string InformationalVersion;

    /// <summary>
    /// Date of commit
    /// </summary>
    public string CommitDate;

    /// <summary>
    /// ShortSha of commit
    /// </summary>
    public string ShortSha;
}

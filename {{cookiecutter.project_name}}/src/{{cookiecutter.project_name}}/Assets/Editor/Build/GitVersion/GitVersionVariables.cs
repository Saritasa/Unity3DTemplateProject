using System;

/// <summary>
/// This structure holds input of GitVersion.exe tool (stripped)
/// </summary>
[Serializable]
public class GitVersionVariables
{
    /// <summary>
    /// Number of commits since last version change
    /// https://github.com/GitTools/GitVersion/issues/1345#issuecomment-350932571
    /// </summary>
    public string BuildMetaData;

    /// <summary>
    /// Version in format Major.Minor.Patch 
    /// </summary>
    public string MajorMinorPatch;

    /// <summary>
    /// First number in version
    /// </summary>
    public int Major;
    /// <summary>
    /// Second number in version
    /// </summary>
    public int Minor;
    /// <summary>
    /// Third number in version
    /// </summary>
    public int Patch;

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

    /// <summary>
    /// CommitsSinceVersionSource of commit
    /// </summary>
    public int CommitsSinceVersionSource;
}

// C# example.

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Utils;
using YamlDotNet.Serialization;

[InitializeOnLoad]
public static partial class BuildProject
{
    private const string RelativeOutputPath = "Build";
    private const string KeyStorePassFieldName = "keystorePass";
    private const string KeyAliasNameFieldName = "keyaliasName";
    private const string KeyAliasPassFieldName = "keyaliasPass";

    static BuildProject()
    {
        var invokeConfigPath = GetCIFolder().CombineAsPath("invoke.yaml");
        if (!File.Exists(invokeConfigPath))
        {
            Debug.LogError(
                "invoke.yaml is not found. Please follow instructions in project's readme.md to setup it properly");
            return;
        }

        // Read keystore variables from invoke.yaml
        var deserializer = new Deserializer();
        var invokeYaml = deserializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(invokeConfigPath));

        if (invokeYaml.ContainsKey(KeyStorePassFieldName) &&
            invokeYaml.ContainsKey(KeyAliasNameFieldName) &&
            invokeYaml.ContainsKey(KeyAliasPassFieldName))
        {
            PlayerSettings.Android.keystorePass = invokeYaml[KeyStorePassFieldName];
            PlayerSettings.Android.keyaliasName = invokeYaml[KeyAliasNameFieldName];
            PlayerSettings.Android.keyaliasPass = invokeYaml[KeyAliasPassFieldName];
            Debug.Log("Android Keystore settings are set up");
        }
        else
        {
            Debug.LogError("Android Keystore config is not found");
        }
    }

    private static string GetCIFolder()
    {
        return BuildHelpers.GetSolutionPath() // scripts/CI/
            .CombineAsPath("..")
            .CombineAsPath("..")
            .CombineAsPath("scripts")
            .CombineAsPath("CI");
    }

    /// <summary>
    /// Path to version_info.txt file
    /// </summary>
    /// <returns></returns>
    private static string GetVersionInfoFilePath()
    {
        return GetAbsoluteOutputPath().CombineAsPath("version_info.txt");
    }

    /// <summary>
    /// Get path to output folder
    /// </summary>
    /// <returns></returns>
    public static string GetAbsoluteOutputPath()
    {
        return BuildHelpers.GetSolutionPath().CombineAsPath(RelativeOutputPath);
    }

    /// <summary>
    /// Generate version_info.txt and save it to build target location. Set bundleVersion.
    /// </summary>
    public static void GenerateGitVersion()
    {
        var gitVersionVariables = GenerateVersionInfo();
        WriteInfoVariablesToFile(gitVersionVariables, GetAbsoluteOutputPath().CombineAsPath("version_info.txt"));
        PlayerSettings.bundleVersion = gitVersionVariables.GetVersionString();
    }
    /// <summary>
    /// Return version build number for android and sets bundleVersionCode with specific formula.
    /// </summary>
    /// <param name="versionVariables"></param>
    /// <returns></returns>
    public static void GenerateGitVersionForAndroid()
    {
        var gitVersionVariables = GenerateVersionInfo();
        WriteInfoVariablesToFile(gitVersionVariables, GetAbsoluteOutputPath().CombineAsPath("version_info.txt"));
        PlayerSettings.bundleVersion = gitVersionVariables.GetVersionString();
        PlayerSettings.Android.bundleVersionCode = gitVersionVariables.Major * 100000000 + gitVersionVariables.Minor * 10000000 + gitVersionVariables.Patch * 10000 + int.Parse(gitVersionVariables.BuildMetaData);
    }

    /// <summary>
    /// Return version build number for iOs in format: major.minor.patch.commits_since_last_release.
    /// </summary>
    /// <param name="versionVariables"></param>
    /// <returns></returns>
    public static string GetIosBuildNumber(GitVersionVariables versionVariables)
    {
        return string.IsNullOrEmpty(versionVariables.BuildMetaData) ?
            versionVariables.MajorMinorPatch :
            $"{versionVariables.MajorMinorPatch}.{versionVariables.BuildMetaData}";
    }

    /// <summary>
    /// Set bundle version according to gitversion.
    /// </summary>
    public static void GenerateGitVersionForIos()
    {
        GitVersionVariables versionInfo = GenerateVersionInfo();
        PlayerSettings.bundleVersion = versionInfo.MajorMinorPatch;
        PlayerSettings.iOS.buildNumber = GetIosBuildNumber(versionInfo);
    }
}
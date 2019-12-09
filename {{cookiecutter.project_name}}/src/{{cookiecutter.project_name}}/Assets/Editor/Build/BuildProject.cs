// C# example.
using UnityEditor;
using Utils;
using System.IO;
using YamlDotNet.Serialization;
using System.Collections.Generic;

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
            UnityEngine.Debug.LogError("invoke.yaml is not found. Please follow instructions in project's readme.md to setup it properly");
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
            UnityEngine.Debug.Log("Android Keystore settings are set up");
        }
        else
        {
            UnityEngine.Debug.LogError("Android Keystore config is not found");
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
    /// Generate version string for application
    /// </summary>
    /// <param name="versionVariables"></param>
    /// <returns></returns>
    private static string GetVersionString(GitVersionVariables versionVariables)
    {
        return string.Format("{0}+{1}",
            versionVariables.FullSemVer,
            versionVariables.ShortSha);
    }

    /// <summary>
    /// Get path to output folder
    /// </summary>
    /// <returns></returns>
    private static string GetAbsoluteOutputPath()
    {
        return BuildHelpers.GetSolutionPath().CombineAsPath(RelativeOutputPath);
    }

    /// <summary>
    /// Generate version_info.txt and save it to build target location. Set bundleVersion
    /// </summary>
    public static void BuildVersionInfo()
    {
        var gitVersionVariables = GenerateVersionInfo();
        WriteInfoVariablesToFile(gitVersionVariables, GetAbsoluteOutputPath().CombineAsPath("version_info.txt"));
        PlayerSettings.bundleVersion = GetVersionString(gitVersionVariables);
    }

    [MenuItem( "Build Tools/Build Windows" )]
    public static void Windows()
    {
        Directory.CreateDirectory(GetAbsoluteOutputPath());
        BuildHelpers.RemoveDirectoryContent(GetAbsoluteOutputPath());
        BuildVersionInfo();

        // Build player.
        BuildPipeline.BuildPlayer(
            EditorBuildSettings.scenes,
            GetAbsoluteOutputPath().CombineAsPath(string.Format("{0}.exe", PlayerSettings.productName)),
            BuildTarget.StandaloneWindows64,
            BuildOptions.UncompressedAssetBundle);
    }

    [MenuItem( "Build Tools/Build Android" )]
    public static void Android()
    {
        Directory.CreateDirectory(GetAbsoluteOutputPath());
        BuildHelpers.RemoveDirectoryContent(GetAbsoluteOutputPath());
        BuildVersionInfo();

        PlayerSettings.Android.bundleVersionCode++;

        // Build player.
        BuildPipeline.BuildPlayer(
            EditorBuildSettings.scenes,
            GetAbsoluteOutputPath().CombineAsPath(string.Format("{0}.apk", PlayerSettings.productName)),
            BuildTarget.Android,
            BuildOptions.UncompressedAssetBundle);
    }

}
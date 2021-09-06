// C# example.

using System.IO;
using UnityEditor;
using Utils;

public static partial class BuildProject
{
    [MenuItem("Build Tools/Build Windows")]
    public static void Windows()
    {
        Directory.CreateDirectory(GetAbsoluteOutputPath());
        BuildHelpers.RemoveDirectoryContent(GetAbsoluteOutputPath());
        GenerateGitVersion();

        // Build player.
        BuildPipeline.BuildPlayer(
            EditorBuildSettings.scenes,
            GetAbsoluteOutputPath().CombineAsPath(string.Format("{0}.exe", PlayerSettings.productName)),
            BuildTarget.StandaloneWindows64,
            BuildOptions.UncompressedAssetBundle);
    }
}
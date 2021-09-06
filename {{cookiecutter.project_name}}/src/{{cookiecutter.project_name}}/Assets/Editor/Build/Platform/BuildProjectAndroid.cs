using UnityEditor;

#if BUILD_ANDROID_XR
using System.IO;
using UnityEditor.XR.Management;
using UnityEngine.XR.Management;
using Utils;
#endif

public static partial class BuildProjectSupportPlatform
{
    const string AndroidXRDefine = "BUILD_ANDROID_XR";

    const string EditorPathAndroidXR = "Build Tools/Supported Build Target/Android/Android XR";

    [MenuItem(EditorPathAndroidXR)]
    public static void AddAndroidXRSupport()
    {
        AddSupportedPlatform(BuildTargetGroup.Android, AndroidXRDefine);
    }

    [MenuItem(EditorPathAndroidXR, true)]
    public static bool IsAndroidXRDefined()
    {
        Menu.SetChecked(EditorPathAndroidXR,
            IsBuildSymbolSet(BuildTargetGroup.Android, AndroidXRDefine));
        return true;
    }
}

#if BUILD_ANDROID_XR

public static partial class BuildProject
{
    [MenuItem("Build Tools/Build Android")]
    public static void Android()
    {
        Directory.CreateDirectory(GetAbsoluteOutputPath());
        BuildHelpers.RemoveDirectoryContent(GetAbsoluteOutputPath());
        GenerateGitVersionForAndroid();

        XRGeneralSettingsPerBuildTarget buildTargetSettings = null;
        EditorBuildSettings.TryGetConfigObject(XRGeneralSettings.k_SettingsKey, out buildTargetSettings);
        XRGeneralSettings settings = buildTargetSettings.SettingsForBuildTarget(BuildTargetGroup.Android);
        settings.AssignedSettings.loaders.Clear();

        // Build player.
        EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
        BuildPipeline.BuildPlayer(
            EditorBuildSettings.scenes,
            GetAbsoluteOutputPath().CombineAsPath(string.Format("{0}.apk", PlayerSettings.productName)),
            BuildTarget.Android,
            BuildOptions.UncompressedAssetBundle);
    }
}
#endif
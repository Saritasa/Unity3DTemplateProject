using UnityEditor;

#if BUILD_OCULUS
using System.Collections.Generic;
using System.IO;
using Unity.XR.Oculus;
using UnityEditor;
using UnityEditor.XR.Management;
using UnityEditor.XR.Management.Metadata;
using UnityEngine;
using UnityEngine.XR.Management;
using Utils;
#endif

public static partial class BuildProjectSupportPlatform 
{
    const string OculusDefine = "BUILD_OCULUS";

    [MenuItem("Build Tools/Build Define Symbols/Android/Oculus")]
    public static void AddOculusSupport()
    {
        AddSupportBuildTool(BuildTargetGroup.Android, OculusDefine);
    }

    [MenuItem("Build Tools/Build Define Symbols/Android/Oculus", true)]
    public static bool IsOculusDefined()
    {
        Menu.SetChecked("Build Tools/Build Define Symbols/Android/Oculus",
            IsBuildSymbolSet(BuildTargetGroup.Android, OculusDefine));
        return true;
    }
}

#if BUILD_OCULUS //Need Oculus XR module.
public static partial class BuildProject
{
    [MenuItem("Build Tools/Build Oculus")]
    public static void Oculus()
    {
        Directory.CreateDirectory(GetAbsoluteOutputPath());
        BuildHelpers.RemoveDirectoryContent(GetAbsoluteOutputPath());
        GenerateGitVersionForAndroid();

        XRGeneralSettingsPerBuildTarget buildTargetSettings = null;
        EditorBuildSettings.TryGetConfigObject(XRGeneralSettings.k_SettingsKey, out buildTargetSettings);
        XRGeneralSettings settings = buildTargetSettings.SettingsForBuildTarget(BuildTargetGroup.Android);

        var activeLoaders = new List<XRLoader>(settings.Manager.activeLoaders);
        foreach (var loader in activeLoaders)
        {
            settings.Manager.TryRemoveLoader(loader);
        }

        XRPackageMetadataStore.AssignLoader(settings.Manager, typeof(OculusLoader).FullName,
            BuildTargetGroup.Android);

        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel25;

        // Build player.
        EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
        BuildPipeline.BuildPlayer(
            EditorBuildSettings.scenes,
            GetAbsoluteOutputPath().CombineAsPath($"{PlayerSettings.productName}_Oculus.apk"),
            BuildTarget.Android,
            BuildOptions.UncompressedAssetBundle);

        Debug.Log("Oculus build complete!");
    }
}
#endif
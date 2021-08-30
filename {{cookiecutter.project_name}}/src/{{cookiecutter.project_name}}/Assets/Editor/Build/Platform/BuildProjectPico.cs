using UnityEditor;

#if BUILD_PICO
using System.Collections.Generic;
using System.IO;
using Unity.XR.PXR;
using UnityEditor.XR.Management;
using UnityEditor.XR.Management.Metadata;
using UnityEngine.XR.Management;
using Utils;
#endif

public static partial class BuildProjectSupportPlatform
{
    const string PicoDefine = "BUILD_PICO";

    [MenuItem("Build Tools/Build Define Symbols/Android/Pico")]
    public static void AddPicoSupport()
    {
        AddSupportBuildTool(BuildTargetGroup.Android, PicoDefine);
    }

    [MenuItem("Build Tools/Build Define Symbols/Android/Pico", true)]
    public static bool IsPicoDefined()
    {
        Menu.SetChecked("Build Tools/Build Define Symbols/Android/Pico",
            IsBuildSymbolSet(BuildTargetGroup.Android, PicoDefine));
        return true;
    }
}

#if BUILD_PICO //Need Pico SDK.
public static partial class BuildProject
{
    [MenuItem("Build Tools/Build Pico")]
    public static void Pico()
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

        XRPackageMetadataStore.AssignLoader(settings.Manager, typeof(PXR_Loader).FullName,
            BuildTargetGroup.Android);

        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel26;

        var absoluteOutputPath = GetAbsoluteOutputPath().CombineAsPath($"{PlayerSettings.productName}_Pico.apk");
        PicoBuildExtention.BuildComplete += () =>
        {
            File.Copy(Path
                .Combine(Path.Combine(PicoBuildExtention.gradleExport, "launcher\\build\\outputs\\apk\\debug"),
                    "launcher-debug.apk")
                .Replace("/", "\\"), absoluteOutputPath);
        };

        PicoBuildExtention.Build();
    }
}
#endif
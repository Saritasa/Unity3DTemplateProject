#if BUILD_PICO
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PicoBuildExtention : PXR_BuildAndRunEW
{
    public static Action BuildComplete;

    const int NUM_BUILD_STEPS = 5;

    private static T GetPrivateFieldValue<T>(string fieldName)
    {
        FieldInfo fieldInfo = typeof(PXR_BuildAndRunEW).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
        T value = (T) fieldInfo.GetValue(null);
        return value;
    }

    private static void SetPrivateFieldValue<T>(string fieldName, T value)
    {
        FieldInfo fieldInfo = typeof(PXR_BuildAndRunEW).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
        fieldInfo.SetValue(null, value);
    }

    private static void GetPrivateMethod(string methodName, object[] methodParams)
    {
        MethodInfo methodInfo =
            typeof(PXR_BuildAndRunEW).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        methodInfo.Invoke(null, methodParams);
    }

    private static T GetPrivateMethod<T>(string methodName, object[] methodParams)
    {
        MethodInfo methodInfo =
            typeof(PXR_BuildAndRunEW).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        T methodValue = (T) methodInfo.Invoke(null, methodParams);
        return methodValue;
    }

    protected static bool showCancel
    {
        get { return GetPrivateFieldValue<bool>("showCancel"); }
        set { SetPrivateFieldValue("showCancel", value); }
    }

    protected static bool buildFailed
    {
        get { return GetPrivateFieldValue<bool>("buildFailed"); }
        set { SetPrivateFieldValue("buildFailed", value); }
    }

    protected static double totalBuildTime
    {
        get { return GetPrivateFieldValue<double>("totalBuildTime"); }
        set { SetPrivateFieldValue("totalBuildTime", value); }
    }

    protected static bool? apkOutputSuccessful
    {
        get { return GetPrivateFieldValue<bool?>("apkOutputSuccessful"); }
        set { SetPrivateFieldValue("apkOutputSuccessful", value); }
    }

    protected static PXR_DirectorySyncer.CancellationTokenSource syncCancelToken
    {
        get { return GetPrivateFieldValue<PXR_DirectorySyncer.CancellationTokenSource>("syncCancelToken"); }
        set { SetPrivateFieldValue("syncCancelToken", value); }
    }

    protected static Process gradleBuildProcess
    {
        get { return GetPrivateFieldValue<Process>("syncCancelToken"); }
        set { SetPrivateFieldValue("syncCancelToken", value); }
    }

    protected static string applicationIdentifier
    {
        get { return GetPrivateFieldValue<string>("applicationIdentifier"); }
        set { SetPrivateFieldValue("applicationIdentifier", value); }
    }

    protected static string productName
    {
        get { return GetPrivateFieldValue<string>("productName"); }
        set { SetPrivateFieldValue("productName", value); }
    }

    protected static Thread buildThread
    {
        get { return GetPrivateFieldValue<Thread>("buildThread"); }
        set { SetPrivateFieldValue("buildThread", value); }
    }

    public static string gradleExport
    {
        get { return GetPrivateFieldValue<string>("gradleExport"); }
        protected set { SetPrivateFieldValue("gradleExport", value); }
    }

    protected static void IncrementProgressBar(string message)
    {
        GetPrivateMethod("IncrementProgressBar", new object[] {message});
    }

    /// <summary>
    /// Build Apk file for PicoXR.
    /// </summary>
    public static void Build()
    {
        GetWindow(typeof(PXR_BuildAndRunEW));
        showCancel = false;
        buildFailed = false;
        totalBuildTime = 0;

        GetPrivateMethod("InitializeProgressBar", new object[] {NUM_BUILD_STEPS});

        //InitializeProgressBar(NUM_BUILD_STEPS);
        IncrementProgressBar("Exporting Unity Project . . .");

        apkOutputSuccessful = null;
        syncCancelToken = null;
        gradleBuildProcess = null;

        Debug.Log("PXRBuild: Starting Unity build ...");

        GetPrivateMethod("SetupDirectories", null);
        //SetupDirectories();

        // 1. Get scenes to build in Unity, and export gradle project
        BuildReport buildResult = GetPrivateMethod<BuildReport>("UnityBuildPlayer", null);
        //UnityBuildPlayer();

        if (buildResult.summary.result == BuildResult.Succeeded)
        {
            totalBuildTime += buildResult.summary.totalTime.TotalSeconds;

            // Set static variables so build thread has updated data
            showCancel = true;
            //gradlePath = PXR_ADBTool.GetInstance().GetGradlePath();
            //jdkPath = PXR_ADBTool.GetInstance().GetJDKPath();
            //androidSdkPath = PXR_ADBTool.GetInstance().GetAndroidSDKPath();
            applicationIdentifier = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
            productName = "launcher";

            BuildCheck();
            return;
        }
        else if (buildResult.summary.result == BuildResult.Cancelled)
        {
            Debug.Log("Build canceled.");
        }
        else
        {
            Debug.Log("Build failed.");
        }

        buildFailed = true;
    }

    private static void BuildCheck()
    {
        // 2. Process gradle project
        IncrementProgressBar("Processing gradle project . . .");
        if (GetPrivateMethod<bool>("ProcessGradleProject", null))
        {
            // 3. Build gradle project
            IncrementProgressBar("Starting gradle build . . .");
            if (GetPrivateMethod<bool>("BuildGradleProject", null))
            {
                IncrementProgressBar("Success!");
                BuildComplete?.Invoke();
                return;
            }
        }

        buildFailed = true;
    }
}
#endif
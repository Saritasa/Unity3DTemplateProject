using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using Utils;

public static partial class BuildProject
{
    /// <summary>
    /// Path to version GitVersion.exe tool
    /// </summary>
    /// <returns></returns>
    private static string GetPathToGitVersionTool()
    {
        return Application.dataPath.
            CombineAsPath(".."). // /src/vr-fire-extinguisher-app
            CombineAsPath(".."). // /src
            CombineAsPath(".."). // /
            CombineAsPath("scripts"). // /scripts
            CombineAsPath("GitVersion"). // /scripts/GitVersion
            CombineAsPath("GitVersion.exe"); // /scripts/GitVersion
    }

    /// <summary>
    /// Call GitVersion.exe for current project. Capture output, deserialize it to GitVersionVariables object
    /// </summary>
    /// <returns></returns>
    private static GitVersionVariables GenerateVersionInfo()
    {
        var ret = new GitVersionVariables();

        using (var process = new Process())
        {
            var platformDependentScripts = Utils.PlatformDependentScripts.ScriptsFactory.ScriptsForCurrentPlatform();
            var startInfo = platformDependentScripts.GetGitVersionProcessStartInfo(GetPathToGitVersionTool());
            process.StartInfo = startInfo;

            var versionInfo = new StringBuilder();
            process.OutputDataReceived += (sender, args) => versionInfo.Append(args.Data);
            process.ErrorDataReceived += (sender, args) => BuildHelpers.HandleCompilationError(args.Data);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            var succeed = process.ExitCode == 0;
            UnityEngine.Debug.Log(succeed ? "GitVersion succeed" : "GitVersion failed, output: " + versionInfo.ToString());

            try
            {
                // We don't need all that returns GitVersion.exe deserialize it with restricted set of properties
                ret = JsonUtility.FromJson<GitVersionVariables>(versionInfo.ToString());
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("GetVersion output:" + versionInfo.ToString());
                UnityEngine.Debug.LogError("Error during reading GitVersion tool output file. See the next message for error details");
                UnityEngine.Debug.LogException(e);
            }
        }
        return ret;
    }

    /// <summary>
    /// Serialize GitVersionVariables object to Json and write to file
    /// </summary>
    /// <param name="gitVersion"></param>
    /// <param name="filePath"></param>
    private static void WriteInfoVariablesToFile(GitVersionVariables gitVersion, string filePath)
    {
        File.WriteAllText(filePath, JsonUtility.ToJson(gitVersion, true));
    }

    /// <summary>
    /// Read GitVersionVariables from file.
    /// Return empty GitVersionVariables in case of problems
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private static GitVersionVariables ReadInfoVariablesFromFile(string filePath)
    {
        var ret = File.ReadAllText(filePath);

        try
        {
            return JsonUtility.FromJson<GitVersionVariables>(ret);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Error during reading info file. See next message for details");
            UnityEngine.Debug.LogException(e);
        }
        return new GitVersionVariables();
    }


}
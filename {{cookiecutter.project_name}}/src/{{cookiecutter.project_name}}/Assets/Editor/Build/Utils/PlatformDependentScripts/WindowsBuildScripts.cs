using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace Utils.PlatformDependentScripts
{
    /// <summary>
    /// Build scripts for Windows
    /// </summary>
    public class WindowsBuildScripts : IPlatformDependentScripts
    {
        public ProcessStartInfo GetGitVersionProcessStartInfo(string pathToGitVersion)
        {
            var startInfo = new ProcessStartInfo(pathToGitVersion);
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            return startInfo;
        }

        public string GetPathToMono()
        {
            return string.Format("\"{0}\"", GetUnityInstallationPath().
                CombineAsPath("Editor").
                CombineAsPath("Data").
                CombineAsPath("MonoBleedingEdge").
                CombineAsPath("bin").
                CombineAsPath("mono.exe"));
        }

        /// <summary>
        /// Returns Unity installation path for current running instance
        /// </summary>
        /// <returns></returns>
        public string GetUnityInstallationPath()
        {
            return EditorApplication.applicationPath.CombineAsPath("..").CombineAsPath("..");
        }
    }
}
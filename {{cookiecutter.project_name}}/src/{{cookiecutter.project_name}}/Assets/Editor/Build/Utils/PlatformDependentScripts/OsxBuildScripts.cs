using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace Utils.PlatformDependentScripts
{
    /// <summary>
    /// Build scripts for Osx
    /// </summary>
    public class OsxBuildScripts : IPlatformDependentScripts
    {
        public ProcessStartInfo GetGitVersionProcessStartInfo(string pathToGitVersion)
        {
            var startInfo = new ProcessStartInfo(GetPathToMono());
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.Arguments = pathToGitVersion;
            return startInfo;
        }

        /// <summary>
        /// Returns Unity installation path for current running instance
        /// </summary>
        /// <returns></returns>
        public string GetUnityInstallationPath()
        {
            return EditorApplication.applicationPath.CombineAsPath("..");
        }

        public string GetPathToMono()
        {
            return GetUnityInstallationPath().
                CombineAsPath("MonoDevelop.app").
                CombineAsPath("Contents").
                CombineAsPath("Frameworks").
                CombineAsPath("Mono.framework").
                CombineAsPath("Versions").
                CombineAsPath("Current").
                CombineAsPath("bin").
                CombineAsPath("mono");
        }
    }
}
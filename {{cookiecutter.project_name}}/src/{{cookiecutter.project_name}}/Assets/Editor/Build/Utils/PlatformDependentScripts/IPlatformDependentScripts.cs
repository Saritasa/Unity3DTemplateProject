using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.Xml.Linq;

namespace Utils.PlatformDependentScripts
{
    /// <summary>
    /// Interface for all platform dependent parts of build process
    /// </summary>
    public interface IPlatformDependentScripts
    {
        /// <summary>
        /// Returns ProcessStartInfo for GitVersion tool
        /// </summary>
        /// <returns></returns>
        ProcessStartInfo GetGitVersionProcessStartInfo(string pathToGitVersion);

        /// <summary>
        /// Returns Unity installation path for current running instance
        /// </summary>
        /// <returns></returns>
        string GetUnityInstallationPath();

        /// <summary>
        /// Return path to mono in system
        /// </summary>
        /// <returns></returns>
        string GetPathToMono();
    }
}
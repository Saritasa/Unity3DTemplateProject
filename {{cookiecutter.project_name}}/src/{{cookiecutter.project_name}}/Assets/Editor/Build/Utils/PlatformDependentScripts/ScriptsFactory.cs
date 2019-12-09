using UnityEngine;

namespace Utils.PlatformDependentScripts
{
    /// <summary>
    /// Factory class for platform dependent scripts
    /// </summary>
    public class ScriptsFactory
    {
        public static IPlatformDependentScripts ScriptsForCurrentPlatform()
        {
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                return new OsxBuildScripts();
            }
            else
            {
                return new WindowsBuildScripts();
            }
        }
    }
}
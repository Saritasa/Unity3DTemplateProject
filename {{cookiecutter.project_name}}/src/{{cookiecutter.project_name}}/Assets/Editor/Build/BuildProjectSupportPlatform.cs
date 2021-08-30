using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[InitializeOnLoad]
public static partial class BuildProjectSupportPlatform
{
    static BuildProjectSupportPlatform()
    {
    }

    private static void AddSupportBuildTool(BuildTargetGroup buildTargetGroup, string definesToAdd)
    {
        var symbols =
            new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';'));

        var newDefine = definesToAdd;
        if (symbols.Contains(newDefine))
        {
            symbols.Remove(newDefine);
        }
        else
        {
            symbols.Add(newDefine);
        }

        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, string.Join(";", symbols.ToArray()));
    }

    private static bool IsBuildSymbolSet(BuildTargetGroup buildTargetGroup, string symbol)
    {
        bool isDefineSet = false;
        var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');

        if (symbols.Contains(symbol))
        {
            isDefineSet = true;
        }

        return isDefineSet;
    }
}
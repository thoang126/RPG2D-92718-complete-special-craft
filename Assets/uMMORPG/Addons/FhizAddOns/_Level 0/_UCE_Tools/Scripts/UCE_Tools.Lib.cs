#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ToolsLib
{
	const string define = "_FHIZTOOLS";

	static ToolsLib() {AddLibrayDefineIfNeeded();}

	static void AddLibrayDefineIfNeeded() {
		BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
		string definestring = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
		string[] defines = definestring.Split (';');

#if !_FHIZTOOLS
		Debug.LogWarning("<b>uMMMORPG3d</b> only! I cannot give support for uMMORPG2d or uSurvival - Sorry!");
#endif

		if (Contains(defines, define))
			return;

		PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (definestring + ";" + define));
		Debug.LogWarning("<b>AddOn imported!</b> - to complete installation please refer to the included README and follow instructions.");
		Debug.Log("<b>" + define + "</b> added to <i>Scripting Define Symbols</i> for selected build target (" + EditorUserBuildSettings.activeBuildTarget.ToString() + ").");
	}

	static bool Contains(string[] defines, string define) {
		foreach (string def in defines) {
			if (def == define)
				return true;
		}
		return false;
	}
}

#endif
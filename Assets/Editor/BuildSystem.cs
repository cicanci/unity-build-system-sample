using System.IO;
using UnityEngine;
using UnityEditor;

public class BuildSystem
{
    private static string _buildPath = Path.Combine(Directory.GetCurrentDirectory(), "Builds");
    private static string[] _buildScenes = new string[] { "Assets/Scenes/SampleScene.unity" };
    private static string[] _buildDebugSymbols = new string[] { "DEBUG_MENU" };
    private static BuildOptions _buildDebugOptions = BuildOptions.Development | BuildOptions.AllowDebugging;

    [MenuItem("My Game/Build System/Build All")]
    public static void BuildAll()
    {
        if (Directory.Exists(_buildPath))
        {
            Directory.Delete(_buildPath, true);
        }

        BuildWindowsDebug();
        BuildWindowsRelease();
        BuildAndroidDebug();
    }

    [MenuItem("My Game/Build System/Build Windows Debug")]
    public static void BuildWindowsDebug() 
    {
        var buildReport = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            target = BuildTarget.StandaloneWindows,
            targetGroup = BuildTargetGroup.Standalone,
            extraScriptingDefines = _buildDebugSymbols,
            locationPathName = Path.Combine(_buildPath, "WindowsDebug/MyGame.exe"),
            options = _buildDebugOptions,
            scenes = _buildScenes
        });

        Debug.Log($"Windows build debug finished: {buildReport.summary.result}");
    }

    [MenuItem("My Game/Build System/Build Windows Release")]
    public static void BuildWindowsRelease()
    {
        var buildReport = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            target = BuildTarget.StandaloneWindows,
            targetGroup = BuildTargetGroup.Standalone,
            locationPathName = Path.Combine(_buildPath, "WindowsRelease/MyGame.exe"),
            scenes = _buildScenes
        });

        Debug.Log($"Windows build release finished: {buildReport.summary.result}");
    }

    [MenuItem("My Game/Build System/Build Android Debug")]
    public static void BuildAndroidDebug()
    {
        var buildReport = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            target = BuildTarget.Android,
            targetGroup = BuildTargetGroup.Android,
            extraScriptingDefines = _buildDebugSymbols,
            locationPathName = Path.Combine(_buildPath, "AndroidDebug/MyGame.apk"),
            options = _buildDebugOptions,
            scenes = _buildScenes
        });

        Debug.Log($"Android build debug finished: {buildReport.summary.result}");
    }
}
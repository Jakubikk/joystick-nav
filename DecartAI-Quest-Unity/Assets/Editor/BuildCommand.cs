using UnityEditor;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Unity Editor build script for automated Quest APK builds
/// Used by build_automation.py and manual builds
/// </summary>
public class BuildCommand
{
    /// <summary>
    /// Build APK for Meta Quest 3
    /// Can be called from command line with:
    /// Unity.exe -quit -batchmode -executeMethod BuildCommand.BuildAndroid
    /// </summary>
    public static void BuildAndroid()
    {
        string outputPath = Environment.GetEnvironmentVariable("BUILD_OUTPUT_PATH");
        if (string.IsNullOrEmpty(outputPath))
        {
            // Default output location
            outputPath = Path.Combine(Application.dataPath, "..", "Builds", "QuestAI.apk");
        }

        // Ensure Builds directory exists
        string buildDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(buildDir))
        {
            Directory.CreateDirectory(buildDir);
        }

        Debug.Log($"Building APK to: {outputPath}");

        // Configure build options
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = new[] { "Assets/Samples/DecartAI-Quest/DecartAI-Main.unity" },
            locationPathName = outputPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // Perform the build
        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"Build succeeded: {outputPath}");
            Debug.Log($"Build size: {report.summary.totalSize} bytes");
        }
        else
        {
            Debug.LogError($"Build failed with {report.summary.totalErrors} errors");
            EditorApplication.Exit(1);
        }
    }

    /// <summary>
    /// Build Development APK with debugging enabled
    /// </summary>
    public static void BuildAndroidDevelopment()
    {
        string outputPath = Environment.GetEnvironmentVariable("BUILD_OUTPUT_PATH");
        if (string.IsNullOrEmpty(outputPath))
        {
            outputPath = Path.Combine(Application.dataPath, "..", "Builds", "QuestAI_Development.apk");
        }

        string buildDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(buildDir))
        {
            Directory.CreateDirectory(buildDir);
        }

        Debug.Log($"Building Development APK to: {outputPath}");

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = new[] { "Assets/Samples/DecartAI-Quest/DecartAI-Main.unity" },
            locationPathName = outputPath,
            target = BuildTarget.Android,
            options = BuildOptions.Development | BuildOptions.AllowDebugging
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"Development build succeeded: {outputPath}");
        }
        else
        {
            Debug.LogError($"Development build failed with {report.summary.totalErrors} errors");
            EditorApplication.Exit(1);
        }
    }

    /// <summary>
    /// Menu item to build from Unity Editor
    /// </summary>
    [MenuItem("Build/Build Quest APK")]
    public static void BuildFromMenu()
    {
        string outputPath = EditorUtility.SaveFilePanel(
            "Save Quest APK",
            Path.Combine(Application.dataPath, "..", "Builds"),
            "QuestAI.apk",
            "apk"
        );

        if (!string.IsNullOrEmpty(outputPath))
        {
            Environment.SetEnvironmentVariable("BUILD_OUTPUT_PATH", outputPath);
            BuildAndroid();
        }
    }

    /// <summary>
    /// Menu item to build development version from Unity Editor
    /// </summary>
    [MenuItem("Build/Build Quest APK (Development)")]
    public static void BuildDevelopmentFromMenu()
    {
        string outputPath = EditorUtility.SaveFilePanel(
            "Save Development Quest APK",
            Path.Combine(Application.dataPath, "..", "Builds"),
            "QuestAI_Development.apk",
            "apk"
        );

        if (!string.IsNullOrEmpty(outputPath))
        {
            Environment.SetEnvironmentVariable("BUILD_OUTPUT_PATH", outputPath);
            BuildAndroidDevelopment();
        }
    }

    /// <summary>
    /// Menu item to quickly build and show output folder
    /// </summary>
    [MenuItem("Build/Quick Build Quest APK")]
    public static void QuickBuild()
    {
        string outputPath = Path.Combine(Application.dataPath, "..", "Builds", "QuestAI.apk");
        Environment.SetEnvironmentVariable("BUILD_OUTPUT_PATH", outputPath);
        BuildAndroid();
        
        // Open Builds folder
        EditorUtility.RevealInFinder(outputPath);
    }
}

using UnityEditor;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Build automation script for Quest 3 AI Transformation App
/// This script is called by the automated build scripts to perform Unity builds
/// </summary>
public class BuildAutomation
{
    private static readonly string BuildPath = Path.Combine(Application.dataPath, "..", "Builds");
    private static readonly string ApkName = "QuestAI.apk";

    [MenuItem("Build/Build Android APK")]
    public static void BuildAndroid()
    {
        Debug.Log("=== Starting Automated Build ===");
        
        // Ensure build directory exists
        if (!Directory.Exists(BuildPath))
        {
            Directory.CreateDirectory(BuildPath);
            Debug.Log($"Created build directory: {BuildPath}");
        }

        // Configure build settings
        string outputPath = Path.Combine(BuildPath, ApkName);
        Debug.Log($"Output path: {outputPath}");

        // Get scenes to build
        string[] scenes = GetScenesToBuild();
        if (scenes.Length == 0)
        {
            Debug.LogError("No scenes found to build!");
            EditorApplication.Exit(1);
            return;
        }

        Debug.Log($"Building {scenes.Length} scene(s):");
        foreach (string scene in scenes)
        {
            Debug.Log($"  - {scene}");
        }

        // Configure build options
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = outputPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // Optional: Enable development build if needed for debugging
        // buildPlayerOptions.options |= BuildOptions.Development;
        // buildPlayerOptions.options |= BuildOptions.AllowDebugging;

        Debug.Log("Starting Unity build pipeline...");
        
        // Perform the build
        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        // Check result
        var summary = report.summary;
        
        Debug.Log($"Build finished with result: {summary.result}");
        Debug.Log($"Total size: {summary.totalSize} bytes");
        Debug.Log($"Build time: {summary.totalTime.TotalSeconds:F2} seconds");
        
        if (summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log("=== BUILD SUCCESSFUL ===");
            Debug.Log($"APK saved to: {outputPath}");
            
            // Show file size in human-readable format
            FileInfo fileInfo = new FileInfo(outputPath);
            if (fileInfo.Exists)
            {
                double sizeMB = fileInfo.Length / (1024.0 * 1024.0);
                Debug.Log($"APK size: {sizeMB:F2} MB");
            }
            
            EditorApplication.Exit(0);
        }
        else
        {
            Debug.LogError("=== BUILD FAILED ===");
            Debug.LogError($"Result: {summary.result}");
            
            if (summary.totalErrors > 0)
            {
                Debug.LogError($"Total errors: {summary.totalErrors}");
            }
            
            if (summary.totalWarnings > 0)
            {
                Debug.LogWarning($"Total warnings: {summary.totalWarnings}");
            }
            
            EditorApplication.Exit(1);
        }
    }

    [MenuItem("Build/Build Android APK (Development)")]
    public static void BuildAndroidDevelopment()
    {
        Debug.Log("=== Starting Development Build ===");
        
        string outputPath = Path.Combine(BuildPath, "QuestAI-dev.apk");
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = GetScenesToBuild(),
            locationPathName = outputPath,
            target = BuildTarget.Android,
            options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.ConnectWithProfiler
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log("=== DEVELOPMENT BUILD SUCCESSFUL ===");
            EditorApplication.Exit(0);
        }
        else
        {
            Debug.LogError("=== DEVELOPMENT BUILD FAILED ===");
            EditorApplication.Exit(1);
        }
    }

    [MenuItem("Build/Configure Project for Quest 3")]
    public static void ConfigureProjectForQuest()
    {
        Debug.Log("=== Configuring Project for Quest 3 ===");

        try
        {
            // Player Settings
            PlayerSettings.companyName = "Decart";
            PlayerSettings.productName = "DecartAI Quest";
            
            // Android Settings
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel29;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel34;
            
            // Architecture
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            
            // Graphics
            PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { 
                UnityEngine.Rendering.GraphicsDeviceType.Vulkan,
                UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 
            });
            
            Debug.Log("✓ Player settings configured");
            Debug.Log("✓ Android settings configured");
            Debug.Log("✓ Architecture set to ARM64");
            Debug.Log("✓ Graphics APIs configured");
            
            // Save assets
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            Debug.Log("=== Configuration Complete ===");
        }
        catch (Exception e)
        {
            Debug.LogError($"Configuration failed: {e.Message}");
        }
    }

    private static string[] GetScenesToBuild()
    {
        // Get all scenes in build settings
        var scenes = new System.Collections.Generic.List<string>();
        
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                scenes.Add(scene.path);
            }
        }

        // If no scenes in build settings, try to find the main scene
        if (scenes.Count == 0)
        {
            string mainScenePath = "Assets/Samples/DecartAI-Quest/DecartAI-Main.unity";
            if (File.Exists(mainScenePath))
            {
                Debug.LogWarning($"No scenes in build settings, using: {mainScenePath}");
                scenes.Add(mainScenePath);
            }
        }

        return scenes.ToArray();
    }

    [MenuItem("Build/Clean Build Directory")]
    public static void CleanBuildDirectory()
    {
        if (Directory.Exists(BuildPath))
        {
            Debug.Log($"Cleaning build directory: {BuildPath}");
            Directory.Delete(BuildPath, true);
            Directory.CreateDirectory(BuildPath);
            Debug.Log("Build directory cleaned");
        }
        else
        {
            Debug.Log("Build directory doesn't exist, nothing to clean");
        }
    }

    [MenuItem("Build/Show Build Directory")]
    public static void ShowBuildDirectory()
    {
        if (Directory.Exists(BuildPath))
        {
            EditorUtility.RevealInFinder(BuildPath);
        }
        else
        {
            Debug.LogWarning("Build directory doesn't exist yet");
        }
    }
}

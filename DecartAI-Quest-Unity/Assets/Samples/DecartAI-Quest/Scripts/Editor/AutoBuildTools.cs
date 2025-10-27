using UnityEditor;
using UnityEngine;
using System;
using System.IO;

namespace QuestCameraKit.Editor
{
    /// <summary>
    /// Automated build tools for Meta Quest 3 application
    /// Accessible via Unity menu: DecartAI > Build Tools
    /// </summary>
    public class AutoBuildTools
    {
        private const string BuildsDirectory = "Builds";
        private const string PackageName = "com.decartai.quest";
        private const string ProductName = "DecartAI-Quest";
        
        [MenuItem("DecartAI/Build APK")]
        public static void BuildAPK()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string fileName = $"DecartQuest3-{timestamp}.apk";
            string outputPath = Path.Combine(BuildsDirectory, fileName);
            
            // Ensure build directory exists
            Directory.CreateDirectory(BuildsDirectory);
            
            Debug.Log($"Starting build: {fileName}");
            Debug.Log($"Output path: {Path.GetFullPath(outputPath)}");
            
            // Build
            BuildPlayerOptions buildOptions = CreateBuildOptions(outputPath);
            var report = BuildPipeline.BuildPlayer(buildOptions);
            
            HandleBuildResult(report, outputPath);
        }
        
        [MenuItem("DecartAI/Build and Install to Quest")]
        public static void BuildAndInstall()
        {
            // Check ADB availability first
            if (!IsADBAvailable())
            {
                EditorUtility.DisplayDialog(
                    "ADB Not Found",
                    "Android Debug Bridge (ADB) is not available in PATH.\n\n" +
                    "Please install Android Platform Tools and ensure 'adb' is in your system PATH.",
                    "OK"
                );
                return;
            }
            
            // Check device connection
            if (!IsQuestConnected())
            {
                EditorUtility.DisplayDialog(
                    "No Quest Device",
                    "No Quest device detected.\n\n" +
                    "Please:\n" +
                    "1. Connect Quest via USB\n" +
                    "2. Enable USB debugging\n" +
                    "3. Accept the authorization dialog in your headset",
                    "OK"
                );
                return;
            }
            
            string timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string fileName = $"DecartQuest3-{timestamp}.apk";
            string outputPath = Path.Combine(BuildsDirectory, fileName);
            
            // Ensure build directory exists
            Directory.CreateDirectory(BuildsDirectory);
            
            Debug.Log($"Starting build and install: {fileName}");
            
            // Build
            BuildPlayerOptions buildOptions = CreateBuildOptions(outputPath);
            var report = BuildPipeline.BuildPlayer(buildOptions);
            
            if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                Debug.Log("Build successful! Installing to Quest...");
                InstallAPK(Path.GetFullPath(outputPath));
            }
            else
            {
                Debug.LogError("Build failed! Installation cancelled.");
                EditorUtility.DisplayDialog("Build Failed", "Build failed. Check console for errors.", "OK");
            }
        }
        
        [MenuItem("DecartAI/Configure Project Settings")]
        public static void ConfigureProjectSettings()
        {
            Debug.Log("Configuring project settings for Quest 3...");
            
            // Company and product settings
            PlayerSettings.companyName = "DecartAI";
            PlayerSettings.productName = ProductName;
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, PackageName);
            
            // Android specific settings
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel29;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
            
            // Scripting settings
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetApiCompatibilityLevel(
                BuildTargetGroup.Android,
                ApiCompatibilityLevel.NET_Standard_2_1
            );
            
            // Architecture
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            
            // Graphics
            PlayerSettings.colorSpace = ColorSpace.Linear;
            
            // Rendering
            PlayerSettings.stereoRenderingPath = StereoRenderingPath.Instancing;
            
            Debug.Log("✅ Project settings configured!");
            
            EditorUtility.DisplayDialog(
                "Configuration Complete",
                "Project settings have been configured for Quest 3.\n\n" +
                "Please manually verify XR Plugin Management settings:\n" +
                "Edit > Project Settings > XR Plug-in Management > Oculus",
                "OK"
            );
        }
        
        [MenuItem("DecartAI/Open Builds Folder")]
        public static void OpenBuildsFolder()
        {
            Directory.CreateDirectory(BuildsDirectory);
            string fullPath = Path.GetFullPath(BuildsDirectory);
            EditorUtility.RevealInFinder(fullPath);
        }
        
        [MenuItem("DecartAI/Check Quest Connection")]
        public static void CheckQuestConnection()
        {
            if (!IsADBAvailable())
            {
                EditorUtility.DisplayDialog(
                    "ADB Not Available",
                    "ADB (Android Debug Bridge) is not available.\n\n" +
                    "Install Android Platform Tools and add to PATH.",
                    "OK"
                );
                return;
            }
            
            if (IsQuestConnected())
            {
                EditorUtility.DisplayDialog(
                    "Quest Connected",
                    "✅ Quest device is connected and ready!",
                    "OK"
                );
            }
            else
            {
                EditorUtility.DisplayDialog(
                    "Quest Not Connected",
                    "❌ No Quest device detected.\n\n" +
                    "Please connect your Quest and enable USB debugging.",
                    "OK"
                );
            }
        }
        
        private static BuildPlayerOptions CreateBuildOptions(string outputPath)
        {
            return new BuildPlayerOptions
            {
                scenes = new[] { "Assets/Samples/DecartAI-Quest/DecartAI-Main.unity" },
                locationPathName = outputPath,
                target = BuildTarget.Android,
                targetGroup = BuildTargetGroup.Android,
                options = BuildOptions.CompressWithLz4
            };
        }
        
        private static void HandleBuildResult(
            UnityEditor.Build.Reporting.BuildReport report, 
            string outputPath
        )
        {
            if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                string fullPath = Path.GetFullPath(outputPath);
                long sizeInMB = report.summary.totalSize / (1024 * 1024);
                
                Debug.Log($"✅ Build successful!");
                Debug.Log($"APK location: {fullPath}");
                Debug.Log($"APK size: {sizeInMB} MB");
                Debug.Log($"Build time: {report.summary.totalTime}");
                
                bool reveal = EditorUtility.DisplayDialog(
                    "Build Successful",
                    $"APK built successfully!\n\n" +
                    $"Location: {fullPath}\n" +
                    $"Size: {sizeInMB} MB\n" +
                    $"Time: {report.summary.totalTime}\n\n" +
                    $"Open build folder?",
                    "Open Folder",
                    "Close"
                );
                
                if (reveal)
                {
                    EditorUtility.RevealInFinder(fullPath);
                }
            }
            else
            {
                Debug.LogError($"❌ Build failed with result: {report.summary.result}");
                
                // Show errors from build report
                foreach (var step in report.steps)
                {
                    foreach (var message in step.messages)
                    {
                        if (message.type == LogType.Error || message.type == LogType.Exception)
                        {
                            Debug.LogError($"Build error: {message.content}");
                        }
                    }
                }
                
                EditorUtility.DisplayDialog(
                    "Build Failed",
                    "Build failed. Please check the Console for error details.",
                    "OK"
                );
            }
        }
        
        private static bool IsADBAvailable()
        {
            try
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = "version",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                
                using (var process = System.Diagnostics.Process.Start(startInfo))
                {
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        private static bool IsQuestConnected()
        {
            try
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = "devices",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                
                using (var process = System.Diagnostics.Process.Start(startInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    
                    // Check if there's a device listed (not just "List of devices attached")
                    string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    return lines.Length > 1; // More than just the header line
                }
            }
            catch
            {
                return false;
            }
        }
        
        private static void InstallAPK(string apkPath)
        {
            try
            {
                Debug.Log($"Installing APK: {apkPath}");
                
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = $"install -r \"{apkPath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                
                using (var process = System.Diagnostics.Process.Start(startInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    
                    Debug.Log($"ADB output: {output}");
                    if (!string.IsNullOrEmpty(error))
                    {
                        Debug.LogWarning($"ADB error output: {error}");
                    }
                    
                    if (process.ExitCode == 0)
                    {
                        Debug.Log("✅ APK installed successfully!");
                        EditorUtility.DisplayDialog(
                            "Installation Successful",
                            "APK installed to Quest!\n\n" +
                            "Put on your headset and launch 'DecartAI-Quest' from Unknown Sources.",
                            "OK"
                        );
                    }
                    else
                    {
                        Debug.LogError($"❌ Installation failed with exit code: {process.ExitCode}");
                        EditorUtility.DisplayDialog(
                            "Installation Failed",
                            $"Failed to install APK.\n\n{error}",
                            "OK"
                        );
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception during installation: {e.Message}");
                EditorUtility.DisplayDialog("Installation Error", e.Message, "OK");
            }
        }
    }
}

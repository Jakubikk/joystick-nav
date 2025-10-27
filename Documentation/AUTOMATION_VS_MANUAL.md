# Automation vs Manual Approach Comparison
## Meta Quest 3 XR Application Development

**Document Purpose:** Analyze which aspects of the development workflow can be automated versus requiring manual intervention, with cost-benefit analysis.

---

## Overview Summary

| Aspect | Manual Time | Automated Time | Automation Difficulty | Recommended Approach |
|--------|-------------|----------------|----------------------|---------------------|
| Git Clone | 2 min | 1 min | ⭐ Easy | **Automate** |
| Software Installation | 30-60 min | 20-30 min | ⭐⭐ Moderate | **Semi-Automate** |
| Unity Setup | 15-30 min | 10-15 min | ⭐⭐⭐ Complex | **Semi-Automate** |
| Project Configuration | 10-20 min | 2-5 min | ⭐⭐⭐⭐ Very Complex | **Automate with Editor Script** |
| Building APK | 15-20 min | 10-15 min | ⭐⭐ Moderate | **Automate** |
| Installation | 2-5 min | 1-2 min | ⭐ Easy | **Automate** |

---

## 1. Repository Cloning

### Manual Approach
**Steps:**
1. Open terminal/command prompt
2. Navigate to desired directory
3. Type: `git clone https://github.com/Jakubikk/joystick-nav.git`
4. Press Enter
5. Wait for completion

**Time:** ~2 minutes  
**Difficulty:** Easy  
**Error Rate:** Low (~5%)

### Automated Approach
**Implementation:**
```bash
#!/bin/bash
# setup-project.sh

PROJECT_DIR="$HOME/UnityProjects"
REPO_URL="https://github.com/Jakubikk/joystick-nav.git"

echo "Creating project directory..."
mkdir -p "$PROJECT_DIR"
cd "$PROJECT_DIR"

echo "Cloning repository..."
if [ -d "joystick-nav" ]; then
    echo "Project already exists. Pulling latest changes..."
    cd joystick-nav
    git pull
else
    git clone "$REPO_URL"
    echo "Repository cloned successfully!"
fi
```

**Time:** ~1 minute  
**Difficulty:** Easy to create  
**Error Rate:** Very Low (~1%)

**Recommendation:** ✅ **AUTOMATE** - Simple script, significant time savings for repeated setups.

---

## 2. Software Installation

### Manual Approach
**Steps:**
1. Download Unity Hub installer
2. Run installer, click through wizard
3. Create Unity account
4. Install specific Unity version
5. Add Android build support modules
6. Install Android platform tools
7. Configure environment variables

**Time:** 30-60 minutes  
**Difficulty:** Moderate  
**Error Rate:** Medium (~20% - wrong version, missing modules)

### Semi-Automated Approach

**Windows PowerShell Script:**
```powershell
# install-dependencies.ps1

# Check if running as administrator
if (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
    Write-Warning "Please run this script as Administrator!"
    exit
}

# Install Chocolatey if not present
if (!(Get-Command choco -ErrorAction SilentlyContinue)) {
    Write-Host "Installing Chocolatey..."
    Set-ExecutionPolicy Bypass -Scope Process -Force
    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072
    iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
}

# Install Git
Write-Host "Installing Git..."
choco install git -y

# Install Android Platform Tools
Write-Host "Installing ADB..."
choco install adb -y

# Download Unity Hub
Write-Host "Downloading Unity Hub..."
$unityHubUrl = "https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.exe"
$output = "$env:TEMP\UnityHubSetup.exe"
Invoke-WebRequest -Uri $unityHubUrl -OutFile $output

Write-Host "Please run $output manually to complete Unity Hub installation"
Write-Host "Then use Unity Hub to install Unity 6000.0.34f1 with Android Build Support"
```

**macOS Bash Script:**
```bash
#!/bin/bash
# install-dependencies.sh

echo "Installing dependencies for macOS..."

# Install Homebrew if not present
if ! command -v brew &> /dev/null; then
    echo "Installing Homebrew..."
    /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
fi

# Install Git
echo "Installing Git..."
brew install git

# Install Android Platform Tools
echo "Installing Android Platform Tools..."
brew install android-platform-tools

# Download Unity Hub
echo "Downloading Unity Hub..."
curl -o ~/Downloads/UnityHub.dmg "https://public-cdn.cloud.unity3d.com/hub/prod/UnityHub.dmg"

echo "Please install Unity Hub from ~/Downloads/UnityHub.dmg"
echo "Then use Unity Hub to install Unity 6000.0.34f1 with Android Build Support"
```

**Time:** 20-30 minutes (still requires some manual steps)  
**Difficulty:** Moderate to create, Easy to use  
**Error Rate:** Low (~10%)

**Recommendation:** ✅ **SEMI-AUTOMATE** - Worth the effort. Can't fully automate Unity installation due to Unity Hub requiring manual module selection, but script handles most dependencies.

---

## 3. Unity Project Configuration

### Manual Approach
**Steps:**
1. Open Unity Hub
2. Add project from disk
3. Open project (wait for import)
4. File → Build Settings → Switch Platform to Android (wait 5-10 min)
5. Player Settings configuration (15+ clicks)
6. XR Plugin Management setup (10+ clicks)
7. Meta XR settings fixes (variable number of fixes)

**Time:** 15-30 minutes  
**Difficulty:** Complex (many settings)  
**Error Rate:** High (~30% - easy to miss a setting)

### Automated Approach with Editor Script

**Unity Editor Script:**
```csharp
// Assets/Editor/AutoConfigureProject.cs
using UnityEditor;
using UnityEngine;

public class AutoConfigureProject
{
    [MenuItem("DecartAI/Auto-Configure Project")]
    public static void ConfigureProject()
    {
        Debug.Log("Starting automatic project configuration...");
        
        // Switch to Android platform
        EditorUserBuildSettings.SwitchActiveBuildTarget(
            BuildTargetGroup.Android, 
            BuildTarget.Android
        );
        
        // Configure Player Settings
        PlayerSettings.companyName = "DecartAI";
        PlayerSettings.productName = "DecartAI-Quest";
        PlayerSettings.SetApplicationIdentifier(
            BuildTargetGroup.Android, 
            "com.decartai.quest"
        );
        
        // Android specific settings
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel29;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
        PlayerSettings.SetScriptingBackend(
            BuildTargetGroup.Android, 
            ScriptingImplementation.IL2CPP
        );
        PlayerSettings.SetApiCompatibilityLevel(
            BuildTargetGroup.Android,
            ApiCompatibilityLevel.NET_Standard_2_1
        );
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
        
        // Graphics settings
        PlayerSettings.colorSpace = ColorSpace.Linear;
        
        // XR Settings (requires manual XR plugin installation first)
        // This can be done but requires Unity 2019.3+ XR SDK
        
        Debug.Log("Project configuration complete!");
        Debug.Log("Please verify XR Plugin Management settings manually.");
    }
}
```

**Usage:**
1. Open project in Unity
2. Click menu: DecartAI → Auto-Configure Project
3. Wait 30 seconds
4. Manually verify XR settings

**Time:** 2-5 minutes (mostly automated)  
**Difficulty:** High to create, Very Easy to use  
**Error Rate:** Very Low (~5%)

**Recommendation:** ✅ **STRONGLY AUTOMATE** - High initial effort but massive time savings and error reduction. Essential for repeated builds or team onboarding.

---

## 4. Scene Setup & Component Configuration

### Manual Approach
**Steps:**
1. Open DecartAI-Main scene
2. Add MenuController component to GameObject
3. Configure all references in Inspector
4. Add TimeTravelController component
5. Configure references
6. Repeat for all 5 feature controllers
7. Set up UI panels
8. Link all UI elements

**Time:** 30-60 minutes  
**Difficulty:** Very Complex  
**Error Rate:** Very High (~50% - missing references, wrong objects)

### Automated Approach with Prefabs + Setup Script

**Unity Editor Script:**
```csharp
// Assets/Editor/AutoSetupScene.cs
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoSetupScene
{
    [MenuItem("DecartAI/Auto-Setup Scene Components")]
    public static void SetupScene()
    {
        Debug.Log("Setting up scene components...");
        
        // Find existing components
        var webRtcConnection = Object.FindFirstObjectByType<WebRTCConnection>();
        var passthroughCamera = Object.FindFirstObjectByType<WebCamTextureManager>();
        
        // Find or create UI panels
        GameObject mainCanvas = GameObject.Find("MainCanvas");
        if (mainCanvas == null)
        {
            Debug.LogError("MainCanvas not found in scene!");
            return;
        }
        
        // Setup MenuController
        GameObject menuControllerObj = new GameObject("MenuController");
        var menuController = menuControllerObj.AddComponent<MenuController>();
        
        // Auto-wire references (simplified example)
        // In practice, this would use SerializedObject and reflection
        // to automatically connect all references
        
        Debug.Log("Scene setup complete! Please verify all connections.");
    }
}
```

**Recommendation:** ⚠️ **SEMI-AUTOMATE** - Full automation is very complex due to Unity's serialization. Better approach: Create prefabs for each panel with pre-configured controllers.

---

## 5. Building the Application

### Manual Approach
**Steps:**
1. File → Build Settings
2. Verify scene is added
3. Click Build or Build and Run
4. Choose save location
5. Wait 10-20 minutes
6. Install if not using Build and Run

**Time:** 15-20 minutes  
**Difficulty:** Easy  
**Error Rate:** Low (~10%)

### Automated Approach

**Unity Editor Script:**
```csharp
// Assets/Editor/AutoBuild.cs
using UnityEditor;
using UnityEngine;
using System;

public class AutoBuild
{
    [MenuItem("DecartAI/Build APK")]
    public static void BuildAPK()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
        string outputPath = $"Builds/DecartQuest3-{timestamp}.apk";
        
        // Ensure build directory exists
        System.IO.Directory.CreateDirectory("Builds");
        
        // Configure build options
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = new[] { "Assets/Samples/DecartAI-Quest/DecartAI-Main.unity" },
            locationPathName = outputPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };
        
        Debug.Log($"Starting build to {outputPath}...");
        
        var report = BuildPipeline.BuildPlayer(buildOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"Build successful! APK saved to: {outputPath}");
            EditorUtility.RevealInFinder(outputPath);
        }
        else
        {
            Debug.LogError("Build failed!");
        }
    }
    
    [MenuItem("DecartAI/Build and Install to Quest")]
    public static void BuildAndInstall()
    {
        BuildAPK();
        
        // Wait for build to complete, then install
        EditorApplication.delayCall += () =>
        {
            var adbPath = "adb"; // Assumes ADB is in PATH
            var apkPath = System.IO.Directory.GetFiles("Builds", "*.apk")[0];
            
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = adbPath,
                Arguments = $"install -r \"{apkPath}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            });
        };
    }
}
```

**Command Line Build (Unity CLI):**
```bash
#!/bin/bash
# build.sh

UNITY_PATH="/Applications/Unity/Hub/Editor/6000.0.34f1/Unity.app/Contents/MacOS/Unity"
PROJECT_PATH="$(pwd)"
BUILD_PATH="$(pwd)/Builds/DecartQuest3.apk"

echo "Building APK..."
"$UNITY_PATH" \
    -quit \
    -batchmode \
    -projectPath "$PROJECT_PATH" \
    -executeMethod AutoBuild.BuildAPK \
    -logFile build.log

echo "Installing to Quest..."
adb install -r "$BUILD_PATH"

echo "Build and install complete!"
```

**Time:** 10-15 minutes (same build time, but automated triggering)  
**Difficulty:** Moderate to create, Very Easy to use  
**Error Rate:** Very Low (~2%)

**Recommendation:** ✅ **AUTOMATE** - Especially useful for CI/CD pipelines and repeated builds. One-click build + install saves significant time.

---

## 6. Installation to Quest Device

### Manual Approach
**Steps:**
1. Plug in Quest via USB
2. Enable USB debugging dialog
3. Open command prompt
4. Type: `adb install -r DecartQuest3.apk`
5. Wait for completion

**Time:** 2-5 minutes  
**Difficulty:** Easy  
**Error Rate:** Medium (~15% - wrong path, USB issues)

### Automated Approach

**Batch Script (Windows):**
```batch
@echo off
REM install-quest.bat

echo Checking for connected Quest device...
adb devices

echo.
echo Installing APK to Quest...
adb install -r "%~dp0DecartQuest3.apk"

if %ERRORLEVEL% == 0 (
    echo.
    echo Installation successful!
    echo Put on your Quest headset and launch DecartAI-Quest from Unknown Sources.
) else (
    echo.
    echo Installation failed! Check that Quest is connected and USB debugging is enabled.
)

pause
```

**Bash Script (macOS/Linux):**
```bash
#!/bin/bash
# install-quest.sh

APK_PATH="$(dirname "$0")/DecartQuest3.apk"

echo "Checking for connected Quest device..."
adb devices

echo ""
echo "Installing APK to Quest..."
if adb install -r "$APK_PATH"; then
    echo ""
    echo "✅ Installation successful!"
    echo "Put on your Quest headset and launch DecartAI-Quest from Unknown Sources."
else
    echo ""
    echo "❌ Installation failed!"
    echo "Check that Quest is connected and USB debugging is enabled."
fi
```

**Time:** 1-2 minutes  
**Difficulty:** Easy to create and use  
**Error Rate:** Low (~5%)

**Recommendation:** ✅ **AUTOMATE** - Simple script with significant quality-of-life improvement.

---

## 7. Complete Automation Pipeline

### Full CI/CD Approach (Advanced)

**GitHub Actions Workflow:**
```yaml
# .github/workflows/build-quest.yml
name: Build Quest APK

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    
    steps:
    - name: Checkout Repository
      uses: actions/checkout@v3
    
    - name: Cache Unity Library
      uses: actions/cache@v3
      with:
        path: DecartAI-Quest-Unity/Library
        key: Library-${{ hashFiles('DecartAI-Quest-Unity/Assets/**') }}
    
    - name: Build Unity Project
      uses: game-ci/unity-builder@v2
      with:
        projectPath: DecartAI-Quest-Unity
        targetPlatform: Android
        androidAppBundle: false
        androidKeystoreName: user.keystore
        androidKeystorePass: ${{ secrets.ANDROID_KEYSTORE_PASS }}
        androidKeyaliasName: ${{ secrets.ANDROID_KEYALIAS_NAME }}
        androidKeyaliasPass: ${{ secrets.ANDROID_KEYALIAS_PASS }}
    
    - name: Upload APK
      uses: actions/upload-artifact@v3
      with:
        name: DecartQuest3-APK
        path: build/Android/Android.apk
```

**Time:** Automatic on git push  
**Difficulty:** High to set up initially  
**Error Rate:** Very Low (~1%)

**Recommendation:** ✅ **AUTOMATE for production** - Essential for team development and releases.

---

## Cost-Benefit Analysis

### Time Investment vs Savings

| Task | Manual (per run) | Automated (per run) | Script Creation Time | Break-even Point |
|------|------------------|---------------------|---------------------|------------------|
| Git Clone | 2 min | 1 min | 5 min | 5 uses |
| Software Install | 45 min | 25 min | 2 hours | 6 uses |
| Unity Config | 22 min | 3 min | 4 hours | 13 uses |
| Scene Setup | 45 min | 5 min | 8 hours | 12 uses |
| Build APK | 17 min | 12 min | 1 hour | 12 uses |
| Install | 3 min | 1 min | 10 min | 5 uses |
| **TOTAL** | **134 min** | **47 min** | **~16 hours** | **~11 full runs** |

### Return on Investment

For a **single developer:**
- Time saved per workflow: **87 minutes** (65% reduction)
- Break-even: After **11 complete workflows**
- Worth it if you'll: Set up project on multiple machines, rebuild frequently, or onboard team members

For a **team of 5 developers:**
- Break-even: After **3 workflows** (each team member runs once)
- Highly recommended

For **production/commercial:**
- Essential for consistency and reliability
- Automation **mandatory** for CI/CD

---

## Recommended Automation Strategy

### Tier 1: Essential (Do This First)
1. ✅ **Git clone script** - 5 min to create, immediate benefit
2. ✅ **APK installation script** - 10 min to create, immediate benefit
3. ✅ **Unity build menu item** - 1 hour, very high ROI

### Tier 2: High Value (Do This Soon)
4. ✅ **Project configuration script** - 4 hours, high ROI for teams
5. ✅ **Software installation scripts** - 2 hours, good for onboarding

### Tier 3: Professional (Do for Production)
6. ✅ **CI/CD pipeline** - 1-2 days, essential for production
7. ✅ **Automated testing** - Ongoing, quality assurance

---

## Conclusion

**For Individual Hobbyists:**
- Automate: Git clone, APK installation, Unity build
- Skip: CI/CD, complex scene setup
- **Time investment: ~2 hours, saves ~30 min per build**

**For Small Teams (2-5 people):**
- Automate: All Tier 1 and Tier 2
- Consider: Basic CI/CD
- **Time investment: ~8 hours, saves ~87 min per workflow per person**

**For Professional/Commercial:**
- Automate: Everything
- Mandatory: Full CI/CD pipeline with automated testing
- **Time investment: ~40 hours initially, saves hundreds of hours over project lifetime**

**Recommendation:** Start with Tier 1 automation (essential scripts) and gradually move to more advanced automation as project needs grow.


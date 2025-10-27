# Automation vs Manual Processes - Comparison Guide

## Overview

This document compares automated versus manual approaches for building, deploying, and managing the Quest 3 AI Transformation App. It helps developers understand where automation saves time and where manual intervention provides more control.

---

## Table of Contents
1. [Build Process](#build-process)
2. [Deployment Process](#deployment-process)
3. [Testing Process](#testing-process)
4. [Configuration Management](#configuration-management)
5. [Summary and Recommendations](#summary-and-recommendations)

---

## Build Process

### Manual Build Process

**Steps:**
1. Open Unity Hub manually
2. Launch Unity Editor
3. Open Build Settings from File menu
4. Select Android platform
5. Click "Build"
6. Choose save location
7. Wait for build to complete
8. Manually verify APK exists

**Time Required:** ~15-30 minutes (first build), 5-10 minutes (subsequent builds)

**Pros:**
- Full control over each step
- Easy to modify settings before build
- Can verify settings visually
- Good for learning the process

**Cons:**
- Repetitive manual steps
- Easy to forget steps
- Human error prone (wrong settings)
- Requires constant attention

---

### Automated Build Process

**Setup Required:**
```bash
# Install Unity Cloud Build CLI or use command line builds
# Create build script: build.sh or build.bat
```

**Automated Script Example (build.sh):**
```bash
#!/bin/bash

# Configuration
UNITY_PATH="/Applications/Unity/Hub/Editor/6000.0.34f1/Unity.app/Contents/MacOS/Unity"
PROJECT_PATH="$(pwd)/DecartAI-Quest-Unity"
BUILD_PATH="$(pwd)/Builds"
APP_NAME="QuestAI.apk"
LOG_FILE="build_log.txt"

# Create builds directory
mkdir -p "$BUILD_PATH"

# Run Unity build
echo "Starting automated build..."
"$UNITY_PATH" \
  -quit \
  -batchmode \
  -nographics \
  -projectPath "$PROJECT_PATH" \
  -buildTarget Android \
  -executeMethod BuildScript.BuildAndroid \
  -logFile "$LOG_FILE"

# Check if build succeeded
if [ -f "$BUILD_PATH/$APP_NAME" ]; then
    echo "✓ Build completed successfully!"
    echo "APK location: $BUILD_PATH/$APP_NAME"
    echo "APK size: $(du -h "$BUILD_PATH/$APP_NAME" | cut -f1)"
else
    echo "✗ Build failed. Check $LOG_FILE for details."
    exit 1
fi
```

**BuildScript.cs** (Unity Editor Script):
```csharp
using UnityEditor;
using UnityEngine;
using System;

public class BuildScript
{
    [MenuItem("Build/Build Android APK")]
    public static void BuildAndroid()
    {
        string buildPath = "Builds/QuestAI.apk";
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Samples/DecartAI-Quest/DecartAI-Main.unity" };
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        Debug.Log("Starting build...");
        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"Build succeeded: {report.summary.totalSize} bytes");
        }
        else
        {
            Debug.LogError($"Build failed: {report.summary.result}");
        }
    }
}
```

**Time Required:** 
- Setup: 30 minutes (one-time)
- Per build: 5-10 minutes (unattended)

**Pros:**
- Consistent builds every time
- Can run unattended
- Easy to integrate with CI/CD
- Automated error checking
- Build logs automatically saved

**Cons:**
- Initial setup time required
- Requires scripting knowledge
- Less visible feedback during build
- Harder to modify settings on-the-fly

---

## Deployment Process

### Manual Deployment

**Steps:**
1. Connect Quest 3 via USB
2. Open SideQuest
3. Wait for device to connect
4. Click "Install APK from folder"
5. Navigate to APK file
6. Click "Open"
7. Wait for installation
8. Verify installation in Quest

**Time Required:** 2-5 minutes

**Pros:**
- Visual confirmation at each step
- Easy to understand
- Works reliably
- Good for one-off deployments

**Cons:**
- Must be done manually each time
- Requires physical USB connection
- Need to navigate UI multiple times
- Can't deploy to multiple devices easily

---

### Automated Deployment

**Automated Script Example (deploy.sh):**
```bash
#!/bin/bash

# Configuration
ADB_PATH="$HOME/Library/Android/sdk/platform-tools/adb"
APK_PATH="./Builds/QuestAI.apk"
PACKAGE_NAME="com.decart.questai"

# Colors for output
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo "Quest 3 Deployment Automation"
echo "=============================="

# Check if ADB exists
if [ ! -f "$ADB_PATH" ]; then
    echo -e "${RED}✗ ADB not found at $ADB_PATH${NC}"
    exit 1
fi

# Check if APK exists
if [ ! -f "$APK_PATH" ]; then
    echo -e "${RED}✗ APK not found at $APK_PATH${NC}"
    echo "  Run ./build.sh first to create the APK"
    exit 1
fi

# Check device connection
echo "Checking device connection..."
DEVICES=$("$ADB_PATH" devices | grep -v "List" | grep "device$")

if [ -z "$DEVICES" ]; then
    echo -e "${RED}✗ No Quest device connected${NC}"
    echo "  Please connect Quest via USB and enable USB debugging"
    exit 1
fi

echo -e "${GREEN}✓ Quest device connected${NC}"

# Uninstall old version (optional, prevents conflicts)
echo "Uninstalling old version..."
"$ADB_PATH" uninstall "$PACKAGE_NAME" 2>/dev/null
echo -e "${YELLOW}  (OK if app wasn't installed)${NC}"

# Install new APK
echo "Installing APK..."
INSTALL_OUTPUT=$("$ADB_PATH" install "$APK_PATH" 2>&1)

if echo "$INSTALL_OUTPUT" | grep -q "Success"; then
    echo -e "${GREEN}✓ Installation successful!${NC}"
    
    # Launch app automatically
    echo "Launching app..."
    "$ADB_PATH" shell am start -n "$PACKAGE_NAME/com.unity3d.player.UnityPlayerActivity"
    echo -e "${GREEN}✓ App launched on Quest${NC}"
else
    echo -e "${RED}✗ Installation failed${NC}"
    echo "$INSTALL_OUTPUT"
    exit 1
fi

# Show app info
APP_SIZE=$(du -h "$APK_PATH" | cut -f1)
echo ""
echo "Deployment Summary:"
echo "  APK Size: $APP_SIZE"
echo "  Package: $PACKAGE_NAME"
echo "  Device: Connected"
echo -e "${GREEN}  Status: Running${NC}"
```

**Time Required:**
- Setup: 15 minutes (one-time)
- Per deployment: 30 seconds - 1 minute (unattended)

**Pros:**
- Very fast deployment
- Can deploy to multiple devices
- Automated error handling
- Auto-launch app after install
- Works from command line

**Cons:**
- Requires ADB knowledge
- Less visual feedback
- May need troubleshooting if script fails
- Requires USB still (or wireless ADB setup)

---

### Wireless Deployment (Advanced Automation)

**Setup Wireless ADB:**
```bash
#!/bin/bash
# setup_wireless_adb.sh

# First time: Connect via USB and run this
ADB_PATH="$HOME/Library/Android/sdk/platform-tools/adb"

echo "Setting up wireless ADB..."

# Enable TCP/IP mode on port 5555
"$ADB_PATH" tcpip 5555

# Get Quest IP address
QUEST_IP=$("$ADB_PATH" shell ip addr show wlan0 | grep "inet " | awk '{print $2}' | cut -d/ -f1)

echo "Quest IP: $QUEST_IP"
echo ""
echo "Now disconnect USB cable and run:"
echo "  adb connect $QUEST_IP:5555"
echo ""
echo "Save this IP for future wireless deployments"
```

**Wireless Deploy Script:**
```bash
#!/bin/bash
# wireless_deploy.sh

QUEST_IP="192.168.1.XXX"  # Your Quest IP
ADB_PATH="$HOME/Library/Android/sdk/platform-tools/adb"

# Connect wirelessly
"$ADB_PATH" connect "$QUEST_IP:5555"

# Wait for connection
sleep 2

# Run normal deployment
./deploy.sh
```

**Pros:**
- No USB cable needed
- Deploy from anywhere on same network
- Very convenient for rapid iteration

**Cons:**
- Initial setup more complex
- Requires same WiFi network
- Slightly slower than USB
- May disconnect if WiFi unstable

---

## Testing Process

### Manual Testing

**Process:**
1. Put on Quest headset
2. Launch app manually
3. Test each feature one by one
4. Take notes on issues
5. Remove headset
6. Update code
7. Repeat build and deploy
8. Put on headset again

**Time Required:** 10-30 minutes per test cycle

**Pros:**
- Direct experience of user interaction
- Can test UX and feel
- Catch visual issues easily
- Natural user perspective

**Cons:**
- Time consuming
- Hard to test edge cases
- Can miss subtle bugs
- Tiring (putting on/off headset repeatedly)
- No automated regression testing

---

### Automated Testing

**Unit Test Example:**
```csharp
using NUnit.Framework;
using UnityEngine;
using QuestCameraKit.WebRTC;

public class MenuControllerTests
{
    [Test]
    public void NavigateDown_WhenAtEnd_WrapsToStart()
    {
        // Arrange
        var menuController = new GameObject().AddComponent<MenuController>();
        menuController.CurrentSelectionIndex = 4; // Last item
        
        // Act
        menuController.NavigateDown();
        
        // Assert
        Assert.AreEqual(0, menuController.CurrentSelectionIndex);
    }
    
    [Test]
    public void ApplyTimeTravelPrompt_GeneratesCorrectPrompt()
    {
        // Arrange
        var controller = new GameObject().AddComponent<TimeTravelController>();
        
        // Act
        string prompt = controller.GenerateTimeTravelPrompt(1920);
        
        // Assert
        Assert.IsTrue(prompt.Contains("Art Nouveau"));
        Assert.IsTrue(prompt.Contains("1920"));
    }
}
```

**Integration Test Script:**
```bash
#!/bin/bash
# run_tests.sh

UNITY_PATH="/Applications/Unity/Hub/Editor/6000.0.34f1/Unity.app/Contents/MacOS/Unity"
PROJECT_PATH="$(pwd)/DecartAI-Quest-Unity"

echo "Running Unity Tests..."

"$UNITY_PATH" \
  -batchmode \
  -nographics \
  -projectPath "$PROJECT_PATH" \
  -runTests \
  -testPlatform Android \
  -testResults test_results.xml

# Parse results
if grep -q "failed=\"0\"" test_results.xml; then
    echo "✓ All tests passed"
    exit 0
else
    echo "✗ Some tests failed"
    grep "test-case" test_results.xml | grep "success=\"False\""
    exit 1
fi
```

**Time Required:**
- Setup: 2-3 hours (one-time)
- Per test run: 2-5 minutes (automated)

**Pros:**
- Fast regression testing
- Catches bugs early
- Consistent test coverage
- Can test edge cases easily
- Runs during CI/CD

**Cons:**
- Can't test real VR experience
- Requires test infrastructure
- Time to write tests
- May miss UX issues
- Not a replacement for manual testing

---

## Configuration Management

### Manual Configuration

**Process:**
1. Open Unity
2. Navigate to Project Settings
3. Change settings in Inspector
4. Save project
5. Document changes
6. Rebuild if needed

**Pros:**
- Visual interface
- See all options
- Easy to understand
- Can experiment freely

**Cons:**
- Settings can be forgotten
- Hard to replicate across machines
- Version control conflicts
- No audit trail

---

### Automated Configuration

**Configuration Script Example:**
```csharp
using UnityEditor;
using UnityEngine;

public class ProjectConfigAutomation
{
    [MenuItem("Tools/Configure Project for Quest")]
    public static void ConfigureProject()
    {
        Debug.Log("Configuring project for Quest 3...");
        
        // Player Settings
        PlayerSettings.companyName = "Decart";
        PlayerSettings.productName = "DecartAI Quest";
        PlayerSettings.Android.bundleVersionCode = 1;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel29;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel34;
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
        
        // Graphics Settings
        PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { 
            UnityEngine.Rendering.GraphicsDeviceType.Vulkan,
            UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 
        });
        
        // XR Settings
        UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        
        Debug.Log("✓ Project configured successfully!");
        
        AssetDatabase.SaveAssets();
    }
}
```

**JSON Configuration File:**
```json
{
  "project": {
    "companyName": "Decart",
    "productName": "DecartAI Quest",
    "packageName": "com.decart.questai",
    "version": "1.0.0"
  },
  "android": {
    "minSdkVersion": 29,
    "targetSdkVersion": 34,
    "architecture": "ARM64",
    "scriptingBackend": "IL2CPP"
  },
  "xr": {
    "provider": "Oculus",
    "initializeOnStartup": true,
    "lowOverheadMode": false
  },
  "build": {
    "development": false,
    "autoconnectProfiler": false,
    "compressionMethod": "LZ4"
  }
}
```

**Pros:**
- Consistent configuration
- Version controlled
- Easy to replicate
- Documented in code
- Can run on CI/CD

**Cons:**
- Requires scripting
- Less visual
- May need Unity API knowledge
- Can be over-engineered

---

## Complete CI/CD Pipeline (Full Automation)

### Setup GitHub Actions

**.github/workflows/build-and-deploy.yml:**
```yaml
name: Build and Deploy Quest App

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - uses: game-ci/unity-builder@v3
      env:
        UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
      with:
        targetPlatform: Android
        buildName: QuestAI
        buildsPath: builds
        
    - name: Upload APK
      uses: actions/upload-artifact@v3
      with:
        name: QuestAI-APK
        path: builds/Android/QuestAI.apk
        
    - name: Create Release
      if: startsWith(github.ref, 'refs/tags/v')
      uses: softprops/action-gh-release@v1
      with:
        files: builds/Android/QuestAI.apk
```

**Time Required:**
- Setup: 4-6 hours (one-time)
- Per commit: 15-25 minutes (automatic)

**Pros:**
- Fully automated builds on every commit
- Automatic testing
- Automatic releases
- Team collaboration friendly
- Professional workflow

**Cons:**
- Complex setup
- Requires GitHub/CI knowledge
- May cost money (CI minutes)
- Overkill for solo projects

---

## Summary and Recommendations

### Time Investment Comparison

| Task | Manual | Automated (Setup + Run) | Time Saved After 10 Iterations |
|------|--------|-------------------------|-------------------------------|
| First Build | 30 min | 30 min + 10 min | N/A |
| Build (subsequent) | 10 min × 10 = 100 min | 5 min × 10 = 50 min | 50 minutes |
| Deploy | 5 min × 10 = 50 min | 15 min + (1 min × 10) = 25 min | 25 minutes |
| Test | 20 min × 10 = 200 min | 180 min + (3 min × 10) = 210 min | -10 minutes (initially) |
| Configure | 15 min × 5 = 75 min | 60 min + (2 min × 5) = 70 min | 5 minutes |
| **TOTAL** | **425 minutes** | **355 minutes** | **70 minutes saved** |

**Break-even point**: After about 5-7 complete cycles, automation starts saving time.

---

### Recommended Approach by Experience Level

#### Complete Beginner (First Time Using Unity)
**Recommendation**: 100% Manual

- Follow the Complete Guide step-by-step
- Use SideQuest for deployment
- Manual testing only
- Learn the process first

**Why**: Understanding manual process is crucial for learning.

---

#### Intermediate (Some Unity Experience)
**Recommendation**: Hybrid Approach

**Automate**:
- ✅ Build script (saves time on rebuilds)
- ✅ Basic deployment script
- ✅ Configuration validation

**Keep Manual**:
- ❌ Testing (still learning)
- ❌ Project setup (need to understand settings)

**Scripts to Use**:
```bash
# Quick build
./scripts/build.sh

# Quick deploy
./scripts/deploy.sh

# Both together
./scripts/build-and-deploy.sh
```

---

#### Advanced (Professional Development)
**Recommendation**: Maximum Automation

**Automate**:
- ✅ Full CI/CD pipeline
- ✅ Automated testing
- ✅ Automated deployment
- ✅ Configuration management
- ✅ Wireless deployment

**Keep Manual**:
- ❌ Final UX testing
- ❌ Creative decisions

---

### Automation Scripts Provided

This project includes ready-to-use automation scripts:

```
scripts/
├── build.sh              # Automated Unity build
├── deploy.sh             # Automated Quest deployment
├── build-and-deploy.sh   # Combined workflow
├── wireless-setup.sh     # Setup wireless ADB
├── run-tests.sh          # Automated testing
└── configure-project.sh  # Project configuration
```

**To use**:
```bash
# Make scripts executable (first time only)
chmod +x scripts/*.sh

# Run build
./scripts/build.sh

# Run deployment
./scripts/deploy.sh

# Or both
./scripts/build-and-deploy.sh
```

---

## Conclusion

### When to Choose Manual
- First time learning
- One-off builds
- Complex troubleshooting
- Creative experimentation

### When to Choose Automation
- Repeated tasks
- Team collaboration
- CI/CD integration
- Professional deployment

### Best Practice
**Start manual, automate incrementally**:
1. Week 1-2: All manual, learn the process
2. Week 3-4: Automate build only
3. Month 2: Add deployment automation
4. Month 3+: Full automation with CI/CD

**Remember**: Automation is a tool, not a requirement. Choose what works for your project and skill level.

---

*For the automation scripts mentioned in this document, see the `/scripts` folder in the project repository.*

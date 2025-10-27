# Automation Scripts

This folder contains automation scripts for building and deploying the Quest 3 AI Transformation App.

## Available Scripts

### `build.sh`
Automates the Unity build process to create an Android APK.

**Usage:**
```bash
./scripts/build.sh
```

**What it does:**
- Finds Unity installation automatically
- Builds Android APK using Unity's batch mode
- Creates `Builds/QuestAI.apk`
- Generates build log for debugging

**Time:** 10-30 minutes (first build), 5-10 minutes (subsequent)

---

### `deploy.sh`
Deploys the built APK to a connected Quest 3 device.

**Usage:**
```bash
./scripts/deploy.sh
```

**Prerequisites:**
- Quest 3 connected via USB
- Developer mode enabled
- USB debugging allowed

**What it does:**
- Finds ADB installation
- Checks for connected Quest device
- Uninstalls old version (if exists)
- Installs new APK
- Launches app automatically

**Time:** 30 seconds - 1 minute

---

### `build-and-deploy.sh`
Combined workflow that builds and deploys in one command.

**Usage:**
```bash
./scripts/build-and-deploy.sh
```

**What it does:**
- Runs `build.sh`
- If successful, runs `deploy.sh`
- Complete pipeline from code to running app

**Time:** 10-30 minutes total

---

## First Time Setup

### Make Scripts Executable (Unix/Mac/Linux)
```bash
chmod +x scripts/*.sh
```

### Windows Users
Use Git Bash or WSL to run these scripts, or:
- Install Windows Subsystem for Linux (WSL)
- Use PowerShell equivalents (see below)

---

## Configuration

The scripts auto-detect most settings, but you may need to update paths in the scripts if your installation differs:

### `build.sh` - Unity Path
```bash
# macOS default
UNITY_PATH="/Applications/Unity/Hub/Editor/6000.0.34f1/Unity.app/Contents/MacOS/Unity"

# Windows default
UNITY_PATH="C:/Program Files/Unity/Hub/Editor/6000.0.34f1/Editor/Unity.exe"
```

### `deploy.sh` - ADB Path
```bash
# macOS default
ADB_PATH="$HOME/Library/Android/sdk/platform-tools/adb"

# Windows default
ADB_PATH="C:/Users/$USER/AppData/Local/Android/Sdk/platform-tools/adb.exe"
```

---

## Troubleshooting

### "Unity not found"
Update `UNITY_PATH` in `build.sh` to point to your Unity installation.

### "ADB not found"
Install Android SDK or update `ADB_PATH` in `deploy.sh`.

### "No Quest device connected"
1. Connect Quest via USB
2. Put on headset
3. Allow USB debugging when prompted
4. Run script again

### "Permission denied"
```bash
chmod +x scripts/*.sh
```

---

## Windows PowerShell Alternatives

If you prefer PowerShell over bash:

### Build (PowerShell)
```powershell
# Set paths
$UnityPath = "C:\Program Files\Unity\Hub\Editor\6000.0.34f1\Editor\Unity.exe"
$ProjectPath = "$PWD\DecartAI-Quest-Unity"
$BuildPath = "$PWD\Builds"

# Run build
& $UnityPath `
  -quit `
  -batchmode `
  -nographics `
  -projectPath $ProjectPath `
  -buildTarget Android `
  -executeMethod BuildAutomation.BuildAndroid `
  -logFile build_log.txt
```

### Deploy (PowerShell)
```powershell
# Set paths
$ADBPath = "$env:LOCALAPPDATA\Android\Sdk\platform-tools\adb.exe"
$APKPath = "$PWD\Builds\QuestAI.apk"
$PackageName = "com.decart.questai"

# Uninstall old version
& $ADBPath uninstall $PackageName 2>$null

# Install new APK
& $ADBPath install $APKPath

# Launch app
& $ADBPath shell am start -n "$PackageName/com.unity3d.player.UnityPlayerActivity"
```

---

## Advanced Usage

### Development Build
Edit `build.sh` and uncomment these lines in `BuildAutomation.cs`:
```csharp
buildPlayerOptions.options |= BuildOptions.Development;
buildPlayerOptions.options |= BuildOptions.AllowDebugging;
```

Or use the menu in Unity: `Build > Build Android APK (Development)`

### Clean Build
Remove old builds before building:
```bash
rm -rf Builds/
./scripts/build.sh
```

Or use Unity menu: `Build > Clean Build Directory`

### Build Specific Scene
Edit `BuildAutomation.cs` to change the scene path.

---

## CI/CD Integration

These scripts can be integrated into CI/CD pipelines:

### GitHub Actions
```yaml
- name: Build APK
  run: ./scripts/build.sh

- name: Upload Artifact
  uses: actions/upload-artifact@v3
  with:
    name: QuestAI-APK
    path: Builds/QuestAI.apk
```

### GitLab CI
```yaml
build:
  script:
    - ./scripts/build.sh
  artifacts:
    paths:
      - Builds/QuestAI.apk
```

---

## See Also

- [Complete Guide](../Documentation/COMPLETE_GUIDE.md) - Full setup instructions
- [Automation vs Manual](../Documentation/AUTOMATION_VS_MANUAL.md) - Detailed comparison
- [Unity Build Documentation](https://docs.unity3d.com/Manual/CommandLineArguments.html)

---

*These scripts are designed to work on macOS, Linux, and Windows (via Git Bash/WSL)*

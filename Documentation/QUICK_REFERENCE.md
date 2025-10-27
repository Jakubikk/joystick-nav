# Quick Reference Guide - DecartXR Quest 3

A quick reference for common tasks and information.

---

## Navigation Controls

| Button | Function |
|--------|----------|
| **Left Trigger** | Go back to previous menu |
| **Right Trigger** | Confirm selection / Apply |
| **Left Joystick ↑↓** | Navigate menu options |
| **Right Joystick ←→** | Adjust sliders (Time Travel) |
| **Start (≡)** | Show/Hide menu |

---

## Menu Structure

```
Main Menu
├─ Time Travel (12 periods: 1800-2200)
├─ Virtual Try-On (32+ items, 7 categories)
├─ Biome Transform (28 locations, 4 types)
├─ Video Game Style (34+ games)
└─ Custom Prompt (unlimited)
```

---

## Quick Start Workflow

### For Developers
1. Clone repo: `git clone https://github.com/Jakubikk/joystick-nav.git`
2. Open in Unity Hub → Unity 6
3. Follow: `Documentation/UNITY_SCENE_SETUP.md`
4. Build APK
5. Deploy to Quest 3

### For Users
1. Put on Quest 3
2. Launch app from Unknown Sources
3. Press **Start** to open menu
4. Navigate with **joystick**
5. Select with **right trigger**

---

## Feature Quick Reference

### Time Travel
- **Use**: View past/future aesthetics
- **Navigation**: Slider with right joystick
- **Apply**: Right trigger
- **Periods**: 12 (every 10-50 years)

### Virtual Try-On
- **Use**: Try on clothing
- **Best with**: Mirror in front of you
- **Navigate**: Joystick up/down
- **Items**: 32+ across 7 categories

### Biome Transform
- **Use**: Change environment
- **Navigate**: Joystick up/down
- **Locations**: 28 worldwide + fantasy
- **Categories**: Nature, Countries, Fantasy, Seasonal

### Video Game Style
- **Use**: Apply game aesthetics
- **Navigate**: Joystick up/down
- **Games**: 34+ from various genres
- **Types**: Retro, Modern, Realistic, Stylized

### Custom Prompt
- **Use**: Type any transformation
- **Open Keyboard**: Right trigger
- **Submit**: A button
- **Clear**: B button
- **Limit**: 500 characters

---

## File Locations

### Code
```
DecartAI-Quest-Unity/Assets/Samples/DecartAI-Quest/Scripts/
├─ MenuManager.cs
├─ TimeTravelFeature.cs
├─ TryOnFeature.cs
├─ BiomeFeature.cs
├─ VideoGameFeature.cs
└─ CustomPromptFeature.cs
```

### Documentation
```
Documentation/
├─ README.md (this guide's index)
├─ COMPLETE_SETUP_GUIDE.md (beginner setup)
├─ FEATURES_GUIDE.md (all features)
├─ AUTOMATION_VS_MANUAL.md (workflows)
├─ UNITY_SCENE_SETUP.md (Unity Editor)
├─ PROJECT_SUMMARY.md (overview)
└─ QUICK_REFERENCE.md (this file)
```

---

## Common Commands

### Git
```bash
# Clone
git clone https://github.com/Jakubikk/joystick-nav.git

# Update
git pull origin main
```

### Unity Build
```bash
# From Unity command line
unity-editor -quit -batchmode -projectPath "./DecartAI-Quest-Unity" \
  -buildTarget Android -executeMethod BuildScript.PerformBuild
```

### ADB Deployment
```bash
# Install
adb install DecartXR-Quest.apk

# Uninstall
adb uninstall com.yourname.decartxr

# Launch
adb shell am start -n com.yourname.decartxr/com.unity3d.player.UnityPlayerActivity
```

---

## Troubleshooting Quick Fixes

| Problem | Quick Fix |
|---------|-----------|
| Menu not visible | Press Start button |
| Can't navigate | Use LEFT joystick (not right) |
| Trigger not working | Use LEFT for back, RIGHT for confirm |
| No transformation | Wait 3-5 seconds for processing |
| Camera black | Grant camera permissions in Quest settings |
| App crashes | Check internet connection (8+ Mbps) |
| Build errors | Check Console in Unity for specific errors |

---

## API Endpoints

### Decart Models
- **Mirage**: `wss://api3.decart.ai/v1/stream-trial?model=mirage`
- **Lucy**: `wss://api3.decart.ai/v1/stream-trial?model=lucy_v2v_720p_rt`

### Resolution & FPS
- **Resolution**: 1280×720
- **FPS**: 30
- **Codec**: VP8
- **Latency**: ~150-200ms

---

## Unity Project Settings

### Android Build
- **Min SDK**: API 29 (Android 10)
- **Target SDK**: API 34 (Android 14)
- **Scripting Backend**: IL2CPP
- **Architecture**: ARM64 only

### Graphics
- **API**: OpenGLES3 only
- **Color Space**: Linear

### XR Settings
- ✅ Initialize XR on Startup
- ❌ Low Overhead Mode (disabled)
- ❌ Occlusion (disabled)
- ❌ Subsampled Layout (disabled)

---

## Code Snippets

### Send Custom Prompt
```csharp
webRtcConnection.SendCustomPrompt("Your prompt here");
```

### Navigate Menu
```csharp
Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
if (input.y > 0.5f) NavigateUp();
else if (input.y < -0.5f) NavigateDown();
```

### Detect Trigger
```csharp
if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
{
    // Right trigger pressed
}
```

---

## Performance Tips

### For Best Results
- ✅ Use 5GHz WiFi
- ✅ Strong signal strength
- ✅ Close background apps
- ✅ Good room lighting
- ✅ Clean camera lenses

### Avoid
- ❌ 2.4GHz WiFi (if possible)
- ❌ Weak/unstable connection
- ❌ Very dark or very bright rooms
- ❌ Dirty camera lenses

---

## Support Links

- **Discord**: https://discord.gg/decart
- **Email**: tom@decart.ai
- **GitHub**: https://github.com/Jakubikk/joystick-nav
- **Platform**: https://platform.decart.ai
- **Website**: https://decart.ai

---

## Version Info

- **Unity**: 6 (6000.0.34f1)
- **Quest OS**: Horizon OS v74+
- **App Version**: 1.0.0
- **Last Updated**: 2025

---

## Keyboard Shortcuts (Unity Editor)

| Key | Action |
|-----|--------|
| **Ctrl/Cmd + S** | Save scene |
| **Ctrl/Cmd + P** | Play mode |
| **Ctrl/Cmd + Shift + B** | Build settings |
| **F** | Frame selected object |
| **W/E/R** | Move/Rotate/Scale tools |

---

## Useful Unity Paths

```
Edit → Project Settings → Player → Android
Edit → Project Settings → XR Plug-in Management
Window → Package Manager
Assets → Import Package
File → Build Settings
```

---

## Content Counts

| Feature | Count |
|---------|-------|
| Time Periods | 12 |
| Clothing Items | 32+ |
| Biomes/Locations | 28 |
| Video Game Styles | 34+ |
| Custom Prompts | ∞ |
| **Total Options** | **106+** |

---

## Documentation Page Counts

| Document | Pages | Lines |
|----------|-------|-------|
| Setup Guide | ~21 | 700+ |
| Features Guide | ~17 | 600+ |
| Automation Guide | ~17 | 600+ |
| Unity Setup | ~13 | 400+ |
| Project Summary | ~11 | 350+ |
| Doc README | ~7 | 250+ |
| **Total** | **~86** | **~2,900** |

---

## Code Statistics

- **Scripts**: 6 new files
- **Lines of Code**: ~1,837
- **Unique Prompts**: 106+
- **Functions**: 50+
- **Classes**: 6

---

*For detailed information, see the full documentation in the Documentation folder.*

**Quick Links:**
- 🚀 [Complete Setup Guide](COMPLETE_SETUP_GUIDE.md)
- 🎮 [Features Guide](FEATURES_GUIDE.md)
- 🤖 [Automation Guide](AUTOMATION_VS_MANUAL.md)
- 🎨 [Unity Setup](UNITY_SCENE_SETUP.md)
- 📊 [Project Summary](PROJECT_SUMMARY.md)

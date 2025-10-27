# Complete Beginner's Guide: Meta Quest 3 AI Transformation App
## From Clone to Production

This guide will walk you through every step needed to get this Meta Quest 3 AI transformation app running, from cloning the repository to deploying it on your Quest headset. **No prior Unity experience required!**

---

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Initial Setup](#initial-setup)
3. [Installing Unity](#installing-unity)
4. [Opening the Project](#opening-the-project)
5. [Project Configuration](#project-configuration)
6. [Scene Setup](#scene-setup)
7. [Building for Quest](#building-for-quest)
8. [Installing on Quest](#installing-on-quest)
9. [Using the App](#using-the-app)
10. [Troubleshooting](#troubleshooting)

---

## Prerequisites

### Hardware Requirements
- **Meta Quest 3** or **Quest 3S** with Horizon OS v74 or higher
- **PC** (Windows 10/11 or macOS) with:
  - At least 16GB RAM
  - 20GB free disk space
  - Dedicated graphics card (recommended)
- **USB-C cable** to connect Quest to PC

### Software Requirements
- **Unity Hub** (free)
- **Unity 6** (6000.0.34f1 or newer)
- **Android Build Support** module for Unity
- **Git** (for cloning the repository)
- **Meta Quest Developer Account** (free)

### Network Requirements
- **WiFi connection** with at least 8 Mbps upload/download speed
- Stable internet connection for AI processing

---

## Initial Setup

### Step 1: Install Git
1. Go to [https://git-scm.com/downloads](https://git-scm.com/downloads)
2. Download Git for your operating system
3. Run the installer with default settings
4. Click "Next" through all prompts and "Install"

### Step 2: Clone the Repository
1. Open **Command Prompt** (Windows) or **Terminal** (Mac)
2. Navigate to where you want the project:
   ```bash
   cd Documents
   ```
3. Clone the repository:
   ```bash
   git clone https://github.com/Jakubikk/joystick-nav.git
   ```
4. Wait for the download to complete

### Step 3: Enable Developer Mode on Quest
1. Download **Meta Quest Mobile App** on your phone
2. Sign in with your Meta account
3. Connect your Quest headset to the app
4. In the app, go to **Menu** → **Devices** → Select your Quest
5. Tap **Developer Mode** and toggle it **ON**
6. If prompted, create a developer organization (it's free)
7. Restart your Quest headset

---

## Installing Unity

### Step 1: Download Unity Hub
1. Go to [https://unity.com/download](https://unity.com/download)
2. Click **Download Unity Hub**
3. Run the downloaded installer
4. Follow installation prompts with default settings

### Step 2: Create Unity Account
1. Open **Unity Hub**
2. Click **Sign in** in the top-right
3. Click **create one** if you don't have an account
4. Complete the sign-up process
5. Activate a **Personal license** (it's free):
   - Click your profile icon → **Manage licenses**
   - Click **Add** → **Get a free personal license**
   - Click **Agree and get personal edition license**

### Step 3: Install Unity 6
1. In Unity Hub, click the **Installs** tab on the left
2. Click **Install Editor** button (top-right)
3. Select **Unity 6** (look for version 6000.0.34f1 or newer)
4. Click **Continue**
5. **IMPORTANT**: Check these modules:
   - ✅ **Android Build Support**
     - ✅ **OpenJDK**
     - ✅ **Android SDK & NDK Tools**
   - ✅ **Documentation** (recommended)
6. Click **Continue** → **Install**
7. Wait for installation (this may take 30-60 minutes)

---

## Opening the Project

### Step 1: Add Project to Unity Hub
1. In Unity Hub, click the **Projects** tab
2. Click **Add** button (top-right)
3. Navigate to where you cloned the repository
4. Select the folder: `joystick-nav/DecartAI-Quest-Unity`
5. Click **Add Project**

### Step 2: Open the Project
1. Click on the project in Unity Hub to open it
2. Unity will import all assets (first time takes 5-15 minutes)
3. If prompted about Unity version, click **Continue** or **Open with Unity 6**
4. Wait for the project to fully load

---

## Project Configuration

### Step 1: Switch to Android Platform
1. In Unity, go to **File** → **Build Settings**
2. In the Platform list, select **Android**
3. Click **Switch Platform** (bottom-right)
4. Wait for Unity to reimport assets (5-10 minutes)

### Step 2: Configure Player Settings
1. Still in Build Settings, click **Player Settings** (bottom-left)
2. In the Inspector panel on the right:

#### **Other Settings Section:**
1. Scroll down to **Identification**
   - **Package Name**: Change to something like `com.yourname.questai` (must be unique)
   - **Version**: Keep as is
   - **Minimum API Level**: Select **Android 10.0 'Q' (API level 29)**
   - **Target API Level**: Select **Android 14.0 'U' (API level 34)**

2. Scroll to **Configuration**
   - **Scripting Backend**: Select **IL2CPP**
   - **API Compatibility Level**: **.NET Standard 2.1**
   - **Target Architectures**: 
     - ✅ Check **ARM64**
     - ❌ Uncheck **ARMv7** (if checked)

3. Scroll to **Graphics**
   - **Graphics APIs**: Should show **Vulkan** or **OpenGLES3**
   - If not, click the **+** button and add **Vulkan**

#### **XR Plug-in Management Section:**
1. Click **XR Plug-in Management** (left sidebar)
2. If you see "Install XR Plugin Management", click it and wait
3. Click the **Android tab** (Android robot icon)
4. Check ✅ **Oculus**

5. Click the small arrow next to **Oculus** to expand settings
6. Configure these settings:
   - ✅ **Initialize XR on Startup** (MUST be checked)
   - ❌ **Low Overhead Mode (GLES)** (MUST be unchecked)
   - ❌ **Meta Quest: Occlusion** (MUST be unchecked)
   - ❌ **Meta XR Subsampled Layout** (MUST be unchecked)

### Step 3: Configure Meta XR Settings
1. In the menu bar, go to **Edit** → **Project Settings** → **Meta XR**
2. You'll see two sections:

#### **Outstanding Issues:**
- Click **Fix All** for issues EXCEPT:
  - ❌ DO NOT FIX: "Hand Tracking must be enabled in OVRManager..."
  - ❌ DO NOT FIX: "When Using Passthrough Building Block..."
- Leave these two unfixed (fixing them breaks the app)

#### **Recommended Items:**
- Click **Apply All** for recommendations EXCEPT:
  - ❌ DO NOT APPLY: "Use Low Overhead Mode"
  - ❌ DO NOT APPLY: "Hand tracking support is set to 'Controllers Only'..."
- Leave these two as-is

3. Close Project Settings

---

## Scene Setup

### Step 1: Open Main Scene
1. In the **Project** panel (bottom), navigate to:
   ```
   Assets → Samples → DecartAI-Quest → DecartAI-Main.unity
   ```
2. Double-click **DecartAI-Main.unity** to open it

### Step 2: Verify Scene Hierarchy
1. In the **Hierarchy** panel (left), you should see:
   - MainCanvas
   - WebRTC Webcam Canvas
   - Client-StreamingCamera
   - PassthroughCamera
   - Other objects...

2. If you see a **MenuSystem** game object, that's perfect! If not, we'll add it:
   - Right-click in Hierarchy → **Create Empty**
   - Name it **"MenuSystem"**
   - In Inspector, click **Add Component**
   - Type **"MenuSystem"** and select it
   - The script will be attached

### Step 3: Create Menu UI (if not present)
If the menu UI doesn't exist yet, let's create it:

1. **Create Menu Panel:**
   - In Hierarchy, right-click **MainCanvas** → **UI** → **Panel**
   - Rename it to **"MenuPanel"**

2. **Create Title Text:**
   - Right-click **MenuPanel** → **UI** → **Text - TextMeshPro**
   - If prompted to import TMP Essentials, click **Import TMP Essentials**
   - Rename to **"TitleText"**
   - In Inspector:
     - **Text**: "Main Menu"
     - **Font Size**: 48
     - **Alignment**: Center, Top

3. **Create Menu Items Container:**
   - Right-click **MenuPanel** → **Create Empty**
   - Rename to **"MenuItemsContainer"**
   - Add **Vertical Layout Group** component:
     - Inspector → **Add Component** → **Vertical Layout Group**
     - Check ✅ **Child Force Expand** → Width

4. **Create Feature Panels:**
   - Right-click **MenuPanel** → **UI** → **Panel** (do this 5 times)
   - Rename them:
     - TimeTravelPanel
     - VirtualMirrorPanel
     - BiomePanel
     - VideoGamePanel
     - CustomPromptPanel

5. **Add Time Travel Slider:**
   - Right-click **TimeTravelPanel** → **UI** → **Slider**
   - Rename to **"YearSlider"**
   - Right-click **TimeTravelPanel** → **UI** → **Text - TextMeshPro**
   - Rename to **"YearDisplayText"**

6. **Configure MenuSystem Component:**
   - Select **MenuSystem** in Hierarchy
   - In Inspector, drag and drop references:
     - **Menu Panel**: Drag **MenuPanel** here
     - **Title Text**: Drag **TitleText** here
     - **Menu Items Container**: Drag **MenuItemsContainer** here
     - **Time Travel Panel**: Drag **TimeTravelPanel** here
     - (Continue for all other panels)
     - **Year Slider**: Drag **YearSlider** here
     - **Year Display Text**: Drag **YearDisplayText** here
     - **Web RTC Controller**: Find and drag **WebRTCController** object

7. **Save the Scene:**
   - **File** → **Save** (or Ctrl+S / Cmd+S)

---

## Building for Quest

### Step 1: Connect Your Quest
1. Turn on your **Meta Quest 3**
2. Connect it to your PC using **USB-C cable**
3. Put on the headset - you'll see a prompt asking to **"Allow USB debugging"**
4. Click **Allow** (and optionally check "Always allow")

### Step 2: Configure Build Settings
1. In Unity, go to **File** → **Build Settings**
2. Make sure **Android** is selected (should show in blue)
3. In **Scenes In Build**:
   - If **DecartAI-Main** is not listed, click **Add Open Scenes**
   - Make sure it's checked ✅

4. Click **Refresh** next to "Run Device"
5. Your Quest should appear in the dropdown
6. Select your Quest device

### Step 3: Build and Run
**Option A: Build and Run Directly** (Recommended for testing)
1. Click **Build And Run**
2. Choose a location to save the APK (create a "Builds" folder)
3. Name it something like `QuestAI_v1.apk`
4. Click **Save**
5. Unity will build the app (10-20 minutes first time)
6. The app will automatically install and launch on your Quest

**Option B: Build APK Only** (For later installation)
1. Click **Build**
2. Save the APK file
3. Install later using SideQuest (see [Installing on Quest](#installing-on-quest))

---

## Installing on Quest

### If you used "Build and Run", skip this section!

### Install via SideQuest (Alternative Method)

1. **Download SideQuest:**
   - Go to [https://sidequestvr.com/setup-howto](https://sidequestvr.com/setup-howto)
   - Download and install **SideQuest**

2. **Connect Quest:**
   - Launch SideQuest
   - Connect Quest via USB
   - Quest should show as connected (green dot)

3. **Install APK:**
   - In SideQuest, click the **folder icon** (Install APK file)
   - Navigate to your APK file
   - Select it and wait for installation

4. **Find Your App:**
   - On Quest, go to **Library**
   - Click **All** dropdown → Select **Unknown Sources**
   - Your app will be listed there

---

## Using the App

### First Launch
1. Put on your Quest headset
2. Launch the app from **Library** → **Unknown Sources**
3. When prompted, **Allow camera permissions**
4. The app will start and begin connecting to the AI service

### Navigation Controls

**All controls use the RIGHT controller:**

| Button/Action | Function |
|--------------|----------|
| **Hamburger Button** (Menu/Start) | Show/Hide Menu |
| **Joystick Up/Down** | Navigate through menu options |
| **Right Trigger** | Confirm selection |
| **Left Trigger** | Go back to previous menu |
| **Joystick Left/Right** | Adjust slider (Time Travel feature) |

### Features Overview

#### 1. **Time Travel**
- Select "Time Travel" from main menu
- Use **joystick left/right** to adjust the year (1800-2200)
- Year display updates in real-time
- Press **right trigger** to apply transformation
- Environment changes to match selected historical period

#### 2. **Virtual Mirror** (Clothing Try-On)
- Select "Virtual Mirror" from main menu
- Navigate clothing options with **joystick up/down**
- Preview different outfits:
  - Medieval Knight Armor
  - Evening Dress
  - Space Suit
  - Kimono
  - And more!
- Press **right trigger** to try on selected clothing
- Uses Lucy AI model for person transformation

#### 3. **Biome Transform**
- Select "Biome Transform" from main menu
- Choose from various environments:
  - Tropical Rainforest
  - Arctic Tundra
  - Japanese Garden
  - Futuristic City
  - And many more!
- Press **right trigger** to transform your room
- Environment maintains layout but changes style

#### 4. **Video Game Style**
- Select "Video Game Style" from main menu
- Choose game aesthetics:
  - Minecraft
  - Cyberpunk 2077
  - Lego World
  - Zelda
  - And more!
- Press **right trigger** to apply game style
- Your room transforms into that game's visual style

#### 5. **Custom Prompt**
- Select "Custom Prompt" from main menu
- Meta keyboard will appear automatically
- Type any transformation you want:
  - "Make everything look like a cartoon"
  - "Transform to ancient Rome"
  - "Add floating crystals everywhere"
  - Be creative!
- Press **Enter** or confirm to apply

### Tips for Best Results
- **Good Lighting**: Ensure your room is well-lit
- **WiFi**: Use 5GHz WiFi for best performance
- **Wait Time**: First transformation takes 5-10 seconds
- **Experiment**: Try different prompts and combinations
- **Movement**: Move slowly for best AI tracking

---

## Troubleshooting

### Camera Not Working
**Problem**: Black screen or "Camera failed to start" error

**Solutions**:
1. Check Quest is running Horizon OS v74 or higher:
   - Quest Settings → System → Software Update
2. Grant camera permissions:
   - Quest Settings → Apps → Your App → Permissions → Camera → Allow
3. Restart the app
4. Restart your Quest headset

### No AI Processing
**Problem**: Video shows but no transformation

**Solutions**:
1. Check internet connection:
   - Need 8+ Mbps upload/download
   - Use 5GHz WiFi if available
2. Wait 10-15 seconds for first connection
3. Try a different prompt
4. Check Decart service status at [https://platform.decart.ai](https://platform.decart.ai)
5. Restart the app

### Build Errors in Unity

**"Android SDK not found"**:
1. Edit → Preferences → External Tools
2. Click **Android SDK Tools** → Download
3. Restart Unity

**"Unable to list target platforms"**:
1. File → Build Settings
2. Click **Switch Platform** again
3. Wait for reimport

**"Gradle build failed"**:
1. Edit → Preferences → External Tools
2. Check ✅ **Gradle Installed with Unity**
3. Rebuild

### App Won't Install on Quest

**"App not installed"**:
1. Check Developer Mode is enabled on Quest
2. Check USB debugging is allowed
3. Try different USB cable
4. Restart Quest and PC

**"Insufficient storage"**:
1. Delete unused apps from Quest
2. Need at least 2GB free space

### Performance Issues

**Laggy or slow transformations**:
1. Close other apps on Quest
2. Check WiFi speed (run speed test)
3. Move closer to WiFi router
4. Use 5GHz WiFi instead of 2.4GHz
5. Let Quest cool down if overheating

### Menu Not Responding

**Buttons don't work**:
1. Make sure you're using the RIGHT controller
2. Try re-pairing controllers:
   - Quest Settings → Devices → Controllers
3. Check controller batteries
4. Restart the app

---

## Advanced Configuration

### Changing Default Year
1. Open Scene in Unity
2. Select **MenuSystem** in Hierarchy
3. Find **YearSlider** component
4. Change **Value** to desired default year

### Customizing Prompts
1. Open `MenuSystem.cs` in a code editor
2. Find the arrays:
   - `clothingOptions`
   - `biomeOptions`
   - `videoGameStyles`
3. Add your own options to the arrays
4. Save and rebuild

### Adding More Features
1. Create new menu mode in `MenuMode` enum
2. Add menu option to `mainMenuOptions` array
3. Create corresponding Show method
4. Add case in `SelectMainMenuOption`

---

## Getting Help

### Resources
- **Discord**: [https://discord.gg/decart](https://discord.gg/decart)
- **Documentation**: [https://docs.platform.decart.ai](https://docs.platform.decart.ai)
- **Issues**: Submit on GitHub repository
- **Email**: tom@decart.ai for technical support

### Before Asking for Help
1. Check this troubleshooting section
2. Try restarting app and Quest
3. Verify all configuration steps were followed
4. Check console logs in Unity for errors
5. Note your Unity version and Quest OS version

---

## Updating the App

### To Update After Code Changes:
1. Open project in Unity
2. Make your changes
3. File → Save
4. File → Build Settings → Build And Run
5. New version installs automatically

### To Get Latest Updates from Repository:
1. Open Command Prompt/Terminal
2. Navigate to project:
   ```bash
   cd Documents/joystick-nav
   ```
3. Pull latest changes:
   ```bash
   git pull
   ```
4. Reopen in Unity
5. Rebuild and install

---

## Automation Tips

### Quick Build Script (Windows)
Create a file `QuickBuild.bat`:
```batch
@echo off
echo Building Quest AI App...
"C:\Program Files\Unity\Hub\Editor\6000.0.34f1\Editor\Unity.exe" ^
-quit -batchmode -projectPath "C:\Path\To\joystick-nav\DecartAI-Quest-Unity" ^
-buildTarget Android -executeMethod BuildCommand.BuildAndroid
echo Build complete!
pause
```

### Quick Build Script (Mac/Linux)
Create a file `quick_build.sh`:
```bash
#!/bin/bash
echo "Building Quest AI App..."
/Applications/Unity/Hub/Editor/6000.0.34f1/Unity.app/Contents/MacOS/Unity \
-quit -batchmode -projectPath "/path/to/joystick-nav/DecartAI-Quest-Unity" \
-buildTarget Android -executeMethod BuildCommand.BuildAndroid
echo "Build complete!"
```

Make it executable:
```bash
chmod +x quick_build.sh
```

---

## Next Steps

### After Successful Setup:
1. ✅ Explore all features
2. ✅ Customize prompts for your use case
3. ✅ Share feedback on Discord
4. ✅ Create your own transformations
5. ✅ Show friends the magic! 🎉

### For Developers:
1. Study the code in `MenuSystem.cs`
2. Review `WebRTCController.cs` for WebRTC integration
3. Check Decart API documentation for advanced features
4. Experiment with custom AI prompts
5. Contribute improvements back to the project

---

## Conclusion

Congratulations! You now have a fully functional AI-powered Quest 3 app. The combination of real-time AI video transformation and VR creates truly magical experiences.

**Remember**: This uses cloud AI processing, so internet quality matters. For best results, use fast WiFi and experiment with different prompts!

**Have fun transforming reality! 🚀✨**

---

*Last updated: October 2025*
*For the latest version of this guide, check the GitHub repository.*

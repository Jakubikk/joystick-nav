# Complete Beginner's Guide: Meta Quest 3 AI Transformation App
### From Clone to Production - Step by Step

---

## Table of Contents
1. [Introduction](#introduction)
2. [Prerequisites](#prerequisites)
3. [Installing Unity](#installing-unity)
4. [Cloning the Repository](#cloning-the-repository)
5. [Opening the Project](#opening-the-project)
6. [Understanding the Project Structure](#understanding-the-project-structure)
7. [Setting Up for Quest Development](#setting-up-for-quest-development)
8. [Configuring the Features](#configuring-the-features)
9. [Building for Meta Quest 3](#building-for-meta-quest-3)
10. [Installing on Your Quest](#installing-on-your-quest)
11. [Using the App](#using-the-app)
12. [Troubleshooting](#troubleshooting)
13. [Publishing to Production](#publishing-to-production)

---

## Introduction

This guide will walk you through every single step needed to build and deploy the Meta Quest 3 AI Transformation app, even if you've never used Unity before. By the end of this guide, you'll have a working app on your Quest 3 headset.

**What This App Does:**
- **Time Travel**: View your environment as it would appear in different historical eras
- **Virtual Mirror**: Try on different clothing virtually using AI
- **Biome Transformation**: See your room as different countries or environments
- **Video Game Worlds**: Experience reality transformed into various video game aesthetics
- **Custom Prompts**: Type your own AI transformation ideas

**Navigation Controls:**
- **Joystick Up/Down**: Navigate through menu options
- **Right Trigger**: Confirm/Select
- **Left Trigger**: Go back
- **Hamburger Button** (☰): Show/Hide menu

---

## Prerequisites

### What You'll Need

1. **Computer Requirements:**
   - Windows 10/11 or macOS 10.15+
   - At least 16GB RAM (32GB recommended)
   - 50GB free disk space
   - Graphics card with DirectX 11 support

2. **Hardware:**
   - Meta Quest 3 or Quest 3S headset
   - USB-C cable (for connecting Quest to computer)
   - Stable internet connection (8+ Mbps)

3. **Accounts:**
   - Meta Developer account (free - we'll set this up)
   - Decart AI account (optional - for API key if needed)
   - GitHub account (free)

---

## Installing Unity

### Step 1: Download Unity Hub

1. Open your web browser
2. Go to: https://unity.com/download
3. Click the big **"Download Unity Hub"** button
4. Wait for the download to complete (about 100MB)

### Step 2: Install Unity Hub

**For Windows:**
1. Find the downloaded file (usually in your Downloads folder)
2. Double-click `UnityHubSetup.exe`
3. Click "Yes" when Windows asks if you want to allow changes
4. Click "I Agree" on the license agreement
5. Click "Install"
6. Wait for installation to complete (1-2 minutes)
7. Click "Finish"

**For macOS:**
1. Find the downloaded file in Downloads
2. Double-click the `.dmg` file
3. Drag Unity Hub to your Applications folder
4. Open Unity Hub from Applications
5. Click "Open" when macOS asks to confirm

### Step 3: Create Unity Account

1. Unity Hub will open automatically
2. Click the **person icon** in the top right
3. Click **"Sign in"**
4. Click **"Create account"**
5. Fill in:
   - Your email address
   - Create a password (at least 8 characters)
   - Your name
6. Click **"Create a Unity ID"**
7. Check your email and click the verification link
8. Return to Unity Hub and sign in

### Step 4: Activate Unity License

1. In Unity Hub, click **"Manage licenses"** (or the gear icon → Licenses)
2. Click **"Add"** or **"Activate New License"**
3. Click **"Get a free personal license"**
4. Click **"Agree and get personal edition license"**
5. You should now see "Personal" license listed

### Step 5: Install Unity Editor

1. In Unity Hub, click the **"Installs"** tab on the left
2. Click the **"Install Editor"** button (or **"Add"** button)
3. Find **Unity 6 (6000.0.34f1)** in the list
   - If you don't see it, click "download archive" and search for this version
4. Click **"Install"** next to Unity 6000.0.34f1
5. A window shows modules to install. Make sure these are checked:
   - ✅ **Android Build Support**
     - ✅ Android SDK & NDK Tools
     - ✅ OpenJDK
   - ✅ **Documentation** (optional but helpful)
6. Click **"Continue"**
7. Accept the terms and click **"Install"**
8. Wait for installation (20-40 minutes depending on internet speed)
   - Unity will download about 8GB of files
   - You can monitor progress in Unity Hub

---

## Cloning the Repository

### Step 1: Install Git

**For Windows:**
1. Go to: https://git-scm.com/download/win
2. Download will start automatically
3. Run the installer
4. Click "Next" for all options (defaults are fine)
5. Click "Install"
6. Click "Finish"

**For macOS:**
1. Open Terminal (press Cmd+Space, type "Terminal")
2. Type: `git --version` and press Enter
3. If Git isn't installed, macOS will prompt you to install it
4. Click "Install" and follow prompts

### Step 2: Choose a Location

1. Decide where to store the project
   - We recommend: `C:\UnityProjects\` (Windows) or `~/UnityProjects/` (Mac)
2. Create this folder:
   - **Windows:** Open File Explorer, create `C:\UnityProjects`
   - **Mac:** Open Finder, create a folder named `UnityProjects` in your home folder

### Step 3: Clone the Repository

**For Windows:**
1. Press `Windows Key + R`
2. Type `cmd` and press Enter (opens Command Prompt)
3. Type: `cd C:\UnityProjects` and press Enter
4. Type: `git clone https://github.com/Jakubikk/joystick-nav.git` and press Enter
5. Wait for download to complete (2-5 minutes)

**For macOS:**
1. Open Terminal (Cmd+Space, type "Terminal")
2. Type: `cd ~/UnityProjects` and press Enter
3. Type: `git clone https://github.com/Jakubikk/joystick-nav.git` and press Enter
4. Wait for download to complete (2-5 minutes)

You should see messages showing the download progress. When complete, you'll see "done" or return to the command prompt.

---

## Opening the Project

### Step 1: Open Project in Unity Hub

1. Open Unity Hub
2. Click the **"Projects"** tab on the left
3. Click the **"Add"** button (or dropdown arrow next to "Open")
4. Navigate to where you cloned the repository:
   - **Windows:** `C:\UnityProjects\joystick-nav\DecartAI-Quest-Unity`
   - **Mac:** `~/UnityProjects/joystick-nav/DecartAI-Quest-Unity`
5. Select the `DecartAI-Quest-Unity` folder
6. Click **"Select Folder"** or **"Open"**

### Step 2: Wait for Unity to Open

1. The project will appear in your Projects list
2. Click on the project to open it
3. Unity will take 5-10 minutes to:
   - Import all assets
   - Compile scripts
   - Generate library files
4. You'll see a progress bar at the bottom of Unity
5. **Don't close Unity** while this is happening, even if it seems frozen

### Step 3: Understanding the Unity Interface

When Unity finishes loading, you'll see several windows:

- **Scene View** (center): Shows your 3D environment
- **Game View** (tab next to Scene): Shows what the player sees
- **Hierarchy** (left): List of all objects in your scene
- **Project** (bottom): All your files and assets
- **Inspector** (right): Properties of selected objects
- **Console** (bottom tab): Shows messages and errors

---

## Understanding the Project Structure

### Key Folders in the Project Window

Navigate in the Project window (bottom) to see these folders:

```
Assets/
├── Samples/
│   └── DecartAI-Quest/
│       ├── Scenes/
│       │   └── DecartAI-Main.unity  (Main scene - this is what you'll build)
│       ├── Scripts/
│       │   ├── MenuSystem.cs         (Main menu controller)
│       │   ├── WebRTCController.cs   (Handles video connection)
│       │   └── Features/
│       │       ├── TimeTravelController.cs
│       │       ├── VirtualMirrorController.cs
│       │       ├── BiomeController.cs
│       │       ├── VideoGameWorldController.cs
│       │       └── CustomPromptController.cs
├── PassthroughCameraApiSamples/  (Camera access code)
└── metaVoiceSdk/                 (Voice recognition - optional)
```

### Opening the Main Scene

1. In the Project window (bottom), navigate to:
   `Assets → Samples → DecartAI-Quest → Scenes`
2. Double-click **DecartAI-Main.unity**
3. The scene will load in the Scene View

---

## Setting Up for Quest Development

### Step 1: Enable Developer Mode on Quest

1. **Put on your Quest headset**
2. Go to **Settings** (gear icon in bottom menu)
3. Go to **System** → **Developer**
4. If you don't see Developer option:
   - On your phone, open the **Meta Quest app**
   - Go to **Menu** → **Devices**
   - Select your Quest 3
   - Tap **Developer Mode**
   - Toggle **Developer Mode** ON
   - You may need to create a developer organization (free)
     - Click "Create Organization"
     - Choose a name
     - Verify your account if prompted
   - Return to headset Settings

5. In headset: **Enable Developer Mode** (toggle ON)
6. Quest will restart

### Step 2: Connect Quest to Computer

1. Use a USB-C cable to connect Quest to your computer
2. Put on the headset
3. You'll see a prompt: **"Allow USB debugging?"**
4. Check **"Always allow from this computer"**
5. Click **"Allow"**

### Step 3: Verify Connection

**Windows:**
1. Press `Windows Key + R`
2. Type `cmd` and press Enter
3. Type: `adb devices` and press Enter
4. You should see your Quest listed with "device" next to it

**Mac:**
1. Open Terminal
2. Type: `adb devices` and press Enter
3. You should see your Quest listed

If `adb` is not recognized:
- The Android tools should have been installed with Unity
- Find ADB at: `[Unity Installation]/Editor/Data/PlaybackEngines/AndroidPlayer/SDK/platform-tools`

### Step 4: Switch Build Platform to Android

1. In Unity, go to **File** → **Build Settings**
2. In the "Platform" list, click **Android**
3. Click **"Switch Platform"** (bottom right)
4. Wait 5-10 minutes for Unity to re-import assets for Android
5. **Do not close the Build Settings window yet**

### Step 5: Configure Player Settings

1. In Build Settings window, click **"Player Settings..."** (bottom left)
2. The Inspector window (right) now shows Player Settings

#### Company and Product Name

1. In Inspector, find **Company Name**
   - Type your name or company name
   - Example: `MyCompany`
2. Find **Product Name**
   - Type: `Quest AI Transformer`

#### XR Plugin Management

1. In the left sidebar of Project Settings, find **XR Plug-in Management**
2. If you don't see it, you need to install it:
   - Go to **Window** → **Package Manager**
   - In Package Manager, change dropdown from "In Project" to **"Unity Registry"**
   - Find **XR Plugin Management**
   - Click **Install**
   - Wait for installation
   - Close Package Manager
   - Return to Project Settings → XR Plug-in Management

3. Click the **Android tab** (Android robot icon)
4. Check ✅ **Oculus**

#### Oculus Settings

1. Under **XR Plug-in Management**, click **Oculus** (appears after checking it)
2. Configure these settings:
   - ✅ **Initialize XR on Startup**
   - ❌ **Low Overhead Mode (GLES)** - MUST be UNCHECKED
   - ❌ **Meta Quest: Occlusion** - MUST be UNCHECKED
   - ❌ **Meta XR Subsampled Layout** - MUST be UNCHECKED

#### Graphics Settings

1. In left sidebar, go back to **Player**
2. Scroll down to find **Other Settings** section (click to expand)
3. Find **Graphics APIs**
   - Click the **+** button if empty
   - Make sure **Vulkan** or **OpenGLES3** is listed
   - Remove **Vulkan** if you want to use OpenGLES3 (select it and click -)

#### Scripting Settings

1. Still in **Other Settings**, scroll down
2. Find **Scripting Backend**
   - Select **IL2CPP** from dropdown
3. Find **Target Architectures**
   - Check ✅ **ARM64**
   - Uncheck ❌ **ARMv7** if it's checked

#### Android Settings

1. Scroll up to **Identification** section
2. Find **Minimum API Level**
   - Select **Android 10.0 (API level 29)** or higher
3. Find **Target API Level**
   - Select **Android 14.0 (API level 34)** or **Automatic (highest installed)**

#### Permissions

1. Scroll down to **Configuration** section
2. Find **Internet Access**
   - Select **Require**
3. Click **Android Permissions** to expand
   - Check ✅ **Camera**

#### Custom Permissions (Important!)

1. Scroll down to find **Publish Settings**
2. Find **Custom Main Manifest** - check it
3. This will create `Assets/Plugins/Android/AndroidManifest.xml`
4. In Project window, navigate to `Assets → Plugins → Android`
5. Double-click **AndroidManifest.xml** to open it in your code editor
6. Find the `<manifest>` section
7. Add this line inside `<manifest>` but before `<application>`:
   ```xml
   <uses-permission android:name="horizonos.permission.HEADSET_CAMERA" />
   ```
8. Save the file

### Step 6: Install Meta XR SDK (If Not Already)

1. Go to **Window** → **Package Manager**
2. Change dropdown to **"Unity Registry"**
3. Search for **"Meta XR"** or **"Oculus"**
4. Install:
   - **Meta XR Core SDK**
   - **Meta XR Platform SDK** (optional)
5. Close Package Manager

### Step 7: Configure Meta XR Project Settings

1. Go to **Edit** → **Project Settings → Meta XR**
2. You'll see "Outstanding Issues" and "Recommended Items"
3. **Fix these issues:**
   - Click "Fix All" for most items
   - **EXCEPT:**
     - ❌ **DO NOT FIX**: "Hand Tracking must be enabled in OVRManager when using its Building Block"
     - ❌ **DO NOT FIX**: "When Using Passthrough Building Block as an underlay it's required to set the camera background to transparent"
     - ❌ **DO NOT FIX**: "Use Low Overhead Mode"
     - ❌ **DO NOT FIX**: Hand tracking support recommendations

Why? These "issues" actually break our app if "fixed"!

---

## Configuring the Features

### Understanding the Scene Setup

1. In the Hierarchy window (left), you'll see GameObjects
2. Find these important objects:
   - **MenuSystem** - Controls the main menu
   - **TimeTravelController** - Time travel feature
   - **VirtualMirrorController** - Clothing try-on
   - **BiomeController** - Environment transformations
   - **VideoGameWorldController** - Game world transformations
   - **CustomPromptController** - Custom AI prompts
   - **WebRTCConnection** - Handles AI communication

### Setting Up the Menu System

1. In Hierarchy, click **MenuSystem**
2. Look at Inspector (right side)
3. You should see references to:
   - Menu Canvas
   - Feature Controllers
   - WebRTC Connection

If any show "None (Missing)":
1. Drag the correct GameObject from Hierarchy to that field
2. For example, drag **TimeTravelController** to the "Time Travel Controller" field

### Verifying WebRTC Connection

1. Click **WebRTCConnection** in Hierarchy
2. In Inspector, verify:
   - **Mirage WebSocket**: Should be filled with Decart AI URL
   - **Lucy WebSocket**: Should be filled with Decart AI URL
   - **Streaming Camera**: Should reference a Camera object

These should already be configured, but if not:
- URLs are in the Decart documentation folder
- Default: `wss://api3.decart.ai/v1/stream-trial?model=mirage`

---

## Building for Meta Quest 3

### Step 1: Add Scene to Build

1. Go to **File** → **Build Settings**
2. Make sure **DecartAI-Main** scene is in "Scenes In Build"
3. If not:
   - Click **"Add Open Scenes"** button
   - Or drag the scene from Project window into the box

### Step 2: Configure Build Settings

In Build Settings window:
1. **Platform**: Should show **Android** (if not, select it and click "Switch Platform")
2. **Texture Compression**: Select **ASTC**
3. **Run Device**: Should show your Quest 3 (if connected)
4. Check ✅ **Development Build** (helpful for debugging)

### Step 3: Build the APK

**Option A: Build and Run (Recommended for first time)**
1. Make sure Quest is connected via USB
2. Click **"Build and Run"** button
3. Choose where to save the APK:
   - Create a folder called `Builds` in your project folder
   - Name the file: `QuestAITransformer.apk`
4. Click **"Save"**
5. Unity will build the project (10-20 minutes first time)
6. The app will automatically install and launch on Quest

**Option B: Build Only**
1. Click **"Build"** button
2. Save as `QuestAITransformer.apk`
3. Wait for build to complete

### Step 4: Monitor Build Progress

- Watch the progress bar in Unity (bottom right)
- Check the Console window (bottom) for any errors
- First build takes longest (10-30 minutes)
- Subsequent builds are faster (5-10 minutes)

### Step 5: Handle Build Errors

If you get errors:

**"Unable to find Unity Engine.dll":**
- Close Unity
- Delete the `Library` folder in your project
- Reopen Unity
- Try building again

**"Android SDK not found":**
- Go to **Edit** → **Preferences** → **External Tools**
- Click checkboxes for Android SDK and NDK
- Let Unity download them
- Try building again

**"Gradle build failed":**
- Go to **Edit** → **Preferences** → **External Tools**
- Uncheck "Gradle Installed with Unity"
- Let Unity download a fresh copy
- Try building again

---

## Installing on Your Quest

### If You Used "Build and Run"

The app should already be on your Quest! Skip to [Using the App](#using-the-app).

### If You Just Built the APK

#### Method 1: SideQuest (Easiest for Beginners)

1. **Install SideQuest:**
   - Go to: https://sidequestvr.com/setup-howto
   - Download SideQuest for your OS
   - Install it

2. **Connect Quest to Computer:**
   - Connect Quest with USB-C cable
   - Open SideQuest
   - Should show "Connected" in top left

3. **Install Your APK:**
   - In SideQuest, click the **folder icon** (top right)
   - Navigate to your APK: `Builds/QuestAITransformer.apk`
   - Click **"Open"**
   - Wait for installation (1-2 minutes)

#### Method 2: ADB Command Line

**Windows:**
1. Press `Windows Key + R`
2. Type `cmd` and press Enter
3. Navigate to your Builds folder:
   ```
   cd C:\UnityProjects\joystick-nav\DecartAI-Quest-Unity\Builds
   ```
4. Install APK:
   ```
   adb install QuestAITransformer.apk
   ```

**Mac:**
1. Open Terminal
2. Navigate to Builds folder:
   ```
   cd ~/UnityProjects/joystick-nav/DecartAI-Quest-Unity/Builds
   ```
3. Install APK:
   ```
   adb install QuestAITransformer.apk
   ```

---

## Using the App

### First Launch

1. **Put on your Quest headset**
2. Press the **Apps button** on your Quest (grid icon on dock)
3. Click the dropdown at the top (says "All")
4. Select **"Unknown Sources"** or **"App Lab & Unknown Sources"**
5. Find **"Quest AI Transformer"**
6. Click to launch

### Grant Permissions

When you first launch:
1. App will request **Camera Permission**
   - Click **"Allow"**
2. May request **Internet Permission**
   - Click **"Allow"**

### Main Controls

- **Joystick Up/Down**: Navigate menu options
- **Right Trigger**: Select/Confirm
- **Left Trigger**: Go back
- **Hamburger Button** (☰ on left controller): Show/Hide menu

### Using Each Feature

#### Time Travel
1. Select "Time Travel" from main menu (Right Trigger)
2. Use **Joystick Left/Right** to move through years
3. Watch the era name update (1800-2100)
4. Press **Right Trigger** to apply the transformation
5. Your environment will transform to that era
6. Press **Left Trigger** to go back to menu

#### Virtual Mirror
1. Stand in front of a mirror (real mirror in your space)
2. Select "Virtual Mirror" from menu
3. Use **Joystick Up/Down** to browse clothing options
4. Press **Right Trigger** to try on the selected outfit
5. AI will transform your clothing in the mirror
6. Browse and try different outfits
7. **Left Trigger** to return to menu

#### Biome Transformation
1. Select "Biome Transformation"
2. Use **Joystick Up/Down** to browse environments
   - Japan, France, Tropical Rainforest, Desert, etc.
3. Press **Right Trigger** to transform your room
4. Your environment changes to the selected biome
5. **Left Trigger** to go back

#### Video Game Worlds
1. Select "Video Game Worlds"
2. Use **Joystick Up/Down** to browse games
   - Minecraft, Fortnite, Cyberpunk 2077, etc.
3. Press **Right Trigger** to transform to that game world
4. Your reality becomes the game aesthetic
5. **Left Trigger** to return

#### Custom Prompt
1. Select "Custom Prompt"
2. Press **Right Trigger** on the text field
3. Meta's keyboard appears in VR
4. Type your custom transformation:
   - "Make everything look like a watercolor painting"
   - "Add floating jellyfish everywhere"
   - "Transform my room into a spaceship interior"
5. Press **A Button** (or close keyboard) to apply
6. **Left Trigger** to go back

---

## Troubleshooting

### Camera Not Working

**Problem:** Black screen or no video feed

**Solutions:**
1. Grant camera permissions:
   - Go to Quest Settings → Apps → Quest AI Transformer → Permissions
   - Enable Camera
2. Restart the app
3. Restart your Quest
4. Check if you have adequate lighting
5. Clean your Quest camera lenses

### No AI Transformation

**Problem:** Video shows but no AI effects

**Solutions:**
1. **Check internet connection:**
   - Quest Settings → Wi-Fi
   - Connect to 5GHz network if available
   - Ensure 8+ Mbps speed
2. **Wait 5-10 seconds:**
   - AI processing takes a few seconds to start
3. **Try a different prompt:**
   - Some prompts work better than others
4. **Restart the app**

### Menu Not Appearing

**Problem:** Can't see the menu

**Solutions:**
1. Press **Hamburger Button** (☰) to show menu
2. Menu might be behind you - turn around
3. Restart the app

### App Crashes on Launch

**Problem:** App closes immediately

**Solutions:**
1. **Rebuild the app:**
   - Delete existing APK
   - Build again from Unity
   - Reinstall
2. **Check Quest OS version:**
   - Update Quest to latest Horizon OS
3. **Check build settings:**
   - Verify IL2CPP is selected
   - Verify ARM64 is checked
4. **View logs:**
   - Connect Quest to computer
   - Run: `adb logcat > log.txt`
   - Check for errors in log.txt

### Poor Performance

**Problem:** Laggy or low framerate

**Solutions:**
1. **Connect to better Wi-Fi:**
   - Use 5GHz network
   - Ensure strong signal
   - Reduce distance to router
2. **Close other apps:**
   - Quest Settings → Apps
   - Force close unused apps
3. **Lower transformation complexity:**
   - Some prompts are more demanding
   - Try simpler transformations
4. **Let Quest cool down:**
   - Quest may throttle when hot
   - Take a break, let it cool

### Controllers Not Responding

**Problem:** Buttons don't work

**Solutions:**
1. **Check controller batteries**
2. **Re-pair controllers:**
   - Quest Settings → Devices → Controllers
   - Unpair and pair again
3. **Verify button mappings:**
   - Our controls are different from default
   - Joystick Up/Down = Navigate
   - Right Trigger = Select
   - Left Trigger = Back
   - Hamburger = Menu toggle

---

## Publishing to Production

### Preparing for Release

#### 1. Disable Development Build
1. Go to **File** → **Build Settings**
2. Uncheck **Development Build**
3. This makes the app smaller and faster

#### 2. Optimize Performance
1. Go to **Edit** → **Project Settings** → **Quality**
2. Select Android platform
3. Choose appropriate quality level
4. Consider reducing:
   - Texture resolution
   - Shadow quality
   - Anti-aliasing

#### 3. Test Thoroughly
- Test all features multiple times
- Test on different Wi-Fi networks
- Test in different environments
- Test with different Quest 3 devices if possible

#### 4. Create Final Build
1. **File** → **Build Settings**
2. Build with:
   - No Development Build
   - IL2CPP
   - ARM64
   - ASTC compression
3. Name it: `QuestAITransformer_v1.0.apk`

### Distributing Your App

#### Option 1: App Lab (Free, Public)

1. **Create Meta Developer Account:**
   - Go to: https://developer.oculus.com
   - Click "Sign Up"
   - Complete registration

2. **Create App:**
   - Dashboard → "Create New App"
   - Select "Quest" platform
   - Choose "App Lab" distribution
   - Fill in app details:
     - Name: "Quest AI Transformer"
     - Description: [Your app description]
     - Category: Utilities or Entertainment

3. **Upload Build:**
   - Go to your app dashboard
   - Upload your APK
   - Add screenshots from Quest
   - Write release notes
   - Submit for review

4. **Review Process:**
   - Meta reviews in 1-2 weeks
   - They check for policy compliance
   - Fix any issues and resubmit

#### Option 2: Side-Loading (Private)

**For Personal Use:**
1. Keep the APK file
2. Share with friends via:
   - SideQuest
   - Direct ADB install
3. Recipients need Developer Mode enabled

**For Small Team:**
1. Upload APK to cloud storage (Google Drive, Dropbox)
2. Share download link
3. Provide installation instructions

#### Option 3: Meta Quest Store (Paid, Official)

1. **Requires:**
   - Polished app
   - Unique value proposition
   - High quality standards
   - Business plan

2. **Process:**
   - Apply through Developer Dashboard
   - More strict review process
   - Potential for monetization
   - Official store presence

### Version Control

**Keep track of versions:**
1. In Unity, **Edit** → **Project Settings** → **Player**
2. Find **Version** field
3. Update version number (e.g., 1.0, 1.1, 2.0)
4. Build number increases automatically

**Git Commits:**
```bash
git add .
git commit -m "Release v1.0 - Initial public release"
git tag v1.0
git push origin main --tags
```

---

## Advanced Topics

### Adding Your Own Features

#### Create a New Feature Controller

1. In Project window: `Assets/Samples/DecartAI-Quest/Scripts/Features`
2. Right-click → **Create** → **C# Script**
3. Name it: `MyCustomController.cs`
4. Follow pattern of existing controllers:
   ```csharp
   using UnityEngine;
   using SimpleWebRTC;
   
   namespace QuestCameraKit.Menu
   {
       public class MyCustomController : MonoBehaviour
       {
           [SerializeField] private WebRTCConnection webRTCConnection;
           private bool isActive = false;
           
           public void Activate()
           {
               isActive = true;
               // Your activation code
           }
           
           public void Deactivate()
           {
               isActive = false;
               // Your deactivation code
           }
       }
   }
   ```

#### Add to Menu System

1. In Hierarchy, select **MenuSystem**
2. In Inspector, add new field for your controller
3. Edit `MenuSystem.cs` to include your feature

### Customizing AI Prompts

Edit the feature controllers to add your own prompts:

**Example:** Add a new clothing option
1. Open `VirtualMirrorController.cs`
2. Find `InitializeClothingOptions()` method
3. Add your own:
   ```csharp
   AddClothingOption("My Custom Outfit", 
       "Description of outfit",
       "Change the outfit to [detailed AI prompt]");
   ```

---

## Support and Resources

### Official Documentation

- Unity Manual: https://docs.unity3d.com/Manual/
- Meta Quest Development: https://developer.oculus.com/documentation/unity/
- Decart AI Documentation: Included in `decart documentation` folder

### Community

- Unity Forums: https://forum.unity.com/
- Meta Developer Forums: https://forums.oculusvr.com/
- Decart Discord: https://discord.gg/decart

### Getting Help

If you're stuck:
1. Check this guide's Troubleshooting section
2. Check Unity Console for error messages (bottom of Unity)
3. Search error messages online
4. Ask on Unity Forums or Meta Developer Forums
5. Review existing GitHub Issues in the repository

---

## Conclusion

Congratulations! You've successfully:
✅ Installed Unity and required tools
✅ Cloned and opened the project
✅ Configured it for Quest development
✅ Built and deployed to your Quest 3
✅ Learned how to use all features
✅ Understood how to publish your app

**What's Next?**
- Customize prompts for your preferences
- Add new features
- Share with friends
- Publish to App Lab
- Contribute improvements back to the project

**Remember:**
- Save your Unity project often (Ctrl+S / Cmd+S)
- Test frequently on actual Quest hardware
- Keep your Quest and Unity updated
- Back up your builds folder

Happy developing! 🎮✨

---

*This guide was created for complete beginners. If you found any steps confusing or have suggestions for improvement, please submit feedback or contribute to the project on GitHub.*

**Last Updated:** 2025
**Project Version:** 1.0
**Unity Version:** 6000.0.34f1
**Target Platform:** Meta Quest 3 / Quest 3S

# Complete Beginner's Guide: Clone to Production
## Meta Quest 3 XR Application Development

**Target Audience:** Complete beginners who have never used Unity before  
**Time to Complete:** 2-4 hours (depending on download speeds)  
**Last Updated:** October 2025

---

## Table of Contents
1. [System Requirements](#1-system-requirements)
2. [Installing Required Software](#2-installing-required-software)
3. [Cloning the Repository](#3-cloning-the-repository)
4. [Setting Up Unity](#4-setting-up-unity)
5. [Opening the Project](#5-opening-the-project)
6. [Configuring Project Settings](#6-configuring-project-settings)
7. [Understanding the Scene](#7-understanding-the-scene)
8. [Testing in Unity Editor](#8-testing-in-unity-editor)
9. [Building for Meta Quest 3](#9-building-for-meta-quest-3)
10. [Installing on Quest Device](#10-installing-on-quest-device)
11. [Using the Application](#11-using-the-application)
12. [Troubleshooting](#12-troubleshooting)

---

## 1. System Requirements

### Your Computer Must Have:
- **Operating System:** Windows 10/11 (64-bit) OR macOS 12+ OR Ubuntu 20.04+
- **CPU:** Intel Core i5 or AMD Ryzen 5 (or better)
- **RAM:** Minimum 8GB (16GB recommended)
- **Storage:** At least 20GB free space
- **Graphics:** DirectX 11 compatible GPU
- **Internet:** Stable broadband connection (for downloads)

### Your Quest Device Must Be:
- **Device:** Meta Quest 3 or Quest 3S
- **OS Version:** Horizon OS v74 or newer
- **Developer Mode:** Enabled (we'll show you how)
- **USB Cable:** USB-C cable that supports data transfer

---

## 2. Installing Required Software

### Step 2.1: Install Git

**Windows:**
1. Go to https://git-scm.com/download/windows
2. Download the 64-bit installer
3. Run the installer
4. Click "Next" through all options (default settings are fine)
5. Click "Install"
6. Wait for installation to complete
7. Click "Finish"

**macOS:**
1. Open Terminal (Applications → Utilities → Terminal)
2. Type: `xcode-select --install`
3. Press Enter
4. Click "Install" when prompted
5. Wait for installation to complete

**Linux:**
```bash
sudo apt-get update
sudo apt-get install git
```

**Verify Git Installation:**
1. Open Command Prompt (Windows) or Terminal (Mac/Linux)
2. Type: `git --version`
3. Press Enter
4. You should see something like "git version 2.x.x"

### Step 2.2: Install Unity Hub

1. Go to https://unity.com/download
2. Click the big "Download Unity Hub" button
3. Save the installer file to your Downloads folder
4. Find and run the installer:
   - **Windows:** Double-click `UnityHubSetup.exe`
   - **macOS:** Double-click `Unity Hub.dmg`, then drag Unity Hub to Applications
   - **Linux:** Make executable and run the AppImage

5. Follow installation wizard:
   - Click "I agree" to the license terms
   - Choose default installation location
   - Click "Install"
   - Wait for installation (1-2 minutes)
   - Click "Finish"

6. Launch Unity Hub
7. Create a Unity account (or sign in if you have one):
   - Click "Create account"
   - Fill in your email and password
   - Verify your email
   - Sign back into Unity Hub

### Step 2.3: Install Unity Editor Version 6000.0.34f1

1. In Unity Hub, click "Installs" on the left sidebar
2. Click the blue "Install Editor" button (top right)
3. Find and select "Unity 6000.0.34f1"
   - If you don't see it, click "Archive" tab, then find this version
4. Click "Next"
5. **IMPORTANT:** Add these modules (check the boxes):
   - ✅ Android Build Support
   - ✅ Android SDK & NDK Tools
   - ✅ OpenJDK
6. Click "Next"
7. Accept the license agreement
8. Click "Install"
9. **Wait patiently** - this downloads 5-10GB and takes 15-60 minutes depending on your internet speed
10. When finished, you'll see Unity 6000.0.34f1 in your Installs list with a green checkmark

### Step 2.4: Install Android Platform Tools (ADB)

**Windows:**
1. Go to https://developer.android.com/tools/releases/platform-tools
2. Click "Download SDK Platform-Tools for Windows"
3. Extract the ZIP file to `C:\platform-tools`
4. Add to System PATH:
   - Press Windows key
   - Type "environment variables"
   - Click "Edit the system environment variables"
   - Click "Environment Variables" button
   - Under "System variables", find "Path"
   - Click "Edit"
   - Click "New"
   - Type: `C:\platform-tools`
   - Click "OK" on all windows
5. Open NEW Command Prompt window
6. Type: `adb version`
7. You should see ADB version info

**macOS:**
```bash
# Install Homebrew if you don't have it
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Install platform-tools
brew install android-platform-tools

# Verify
adb version
```

**Linux:**
```bash
sudo apt-get update
sudo apt-get install android-tools-adb android-tools-fastboot
adb version
```

### Step 2.5: Install SideQuest (Optional but Recommended)

1. Go to https://sidequestvr.com/setup-howto
2. Download SideQuest for your operating system
3. Install it
4. We'll use this later for easier APK installation

---

## 3. Cloning the Repository

### Step 3.1: Choose Your Project Location

1. **Create a dedicated folder:**
   - Windows: `C:\Unity Projects\`
   - macOS/Linux: `~/UnityProjects/`

2. **DO NOT use folders with:**
   - Spaces in the name (bad: "My Unity Projects")
   - Special characters (bad: "Unity@Projects!")
   - Cloud sync (bad: OneDrive, Dropbox, iCloud folders)

### Step 3.2: Clone the Repository

1. Open Command Prompt (Windows) or Terminal (Mac/Linux)

2. Navigate to your projects folder:
   ```bash
   # Windows
   cd C:\Unity Projects

   # macOS/Linux
   cd ~/UnityProjects
   ```

3. Clone the repository:
   ```bash
   git clone https://github.com/Jakubikk/joystick-nav.git
   ```

4. Wait for download to complete (1-2 minutes)

5. Navigate into the project:
   ```bash
   cd joystick-nav
   ```

6. Verify the Unity project exists:
   ```bash
   # Windows
   dir DecartAI-Quest-Unity

   # macOS/Linux
   ls DecartAI-Quest-Unity
   ```

You should see folders like "Assets", "Packages", "ProjectSettings"

---

## 4. Setting Up Unity

### Step 4.1: Enable Developer Mode on Quest

**On Your Computer:**
1. Go to https://developer.oculus.com/
2. Sign in with your Meta/Facebook account (same one as your Quest)
3. Agree to terms and conditions
4. Your account is now a developer account!

**On Your Quest 3:**
1. Put on your Quest headset
2. Open Settings
3. Scroll down to "System"
4. Click "Developer"
5. Toggle "USB Connection Dialog" to ON
6. Toggle "Passthrough Camera" to ON

**Verify Developer Mode:**
- If you see developer options, you're good!
- If not, restart your Quest and check again

### Step 4.2: Connect Quest to Computer

1. Plug USB-C cable into Quest and computer
2. Put on Quest headset
3. You'll see dialog: "Allow USB Debugging?"
4. Check "Always allow from this computer"
5. Click "OK"

**Test ADB Connection:**
```bash
adb devices
```

You should see something like:
```
List of devices attached
1WMHH123456789    device
```

If you see "unauthorized", check the dialog in your Quest headset.

---

## 5. Opening the Project

### Step 5.1: Launch Unity Hub

1. Open Unity Hub application
2. Click "Projects" on the left sidebar

### Step 5.2: Add the Project

1. Click "Add" button (top right)
2. Click "Add project from disk"
3. Navigate to your project:
   - Windows: `C:\Unity Projects\joystick-nav\DecartAI-Quest-Unity`
   - macOS/Linux: `~/UnityProjects/joystick-nav/DecartAI-Quest-Unity`
4. Click "Select Folder" or "Add Project"

### Step 5.3: Open the Project

1. Click on the project in Unity Hub
2. **WAIT PATIENTLY** - First import takes 5-15 minutes
3. You'll see progress bars as Unity imports assets
4. Unity Editor will open when complete

### Step 5.4: Install Required Packages (Automatic)

When Unity opens, it will automatically detect and import:
- Meta XR SDK
- Unity WebRTC
- TextMeshPro
- Input System

Just click "Install" or "Yes" if any dialogs appear asking to install packages.

---

## 6. Configuring Project Settings

### Step 6.1: Switch to Android Platform

1. In Unity, click **File** → **Build Settings**
2. In the "Platform" list, click **Android**
3. Click the **Switch Platform** button (bottom right)
4. **WAIT** - this takes 5-10 minutes the first time
5. When done, the Unity icon will be next to "Android"

### Step 6.2: Configure Player Settings

1. With Build Settings still open, click **Player Settings** button (bottom left)
2. This opens Project Settings window

**Company Name:**
- Click **Player** tab (left sidebar)
- In "Company Name" field, type your name or company
- In "Product Name" field, keep "DecartAI-Quest"

**Android Settings:**
1. Still in Player settings, click the **Android tab** (icon looks like Android robot)

2. **Other Settings section:**
   - Find "Rendering"
   - Graphics APIs: Should show "Vulkan" or "OpenGLES3"
   - If it shows anything else, click the list, remove Vulkan if present, ensure OpenGLES3 is there
   
3. Scroll down to **Identification:**
   - Package Name: Keep default "com.YourCompany.DecartAI-Quest"
   - Minimum API Level: Select "Android 10.0 (API Level 29)"
   - Target API Level: Select "Automatic (highest installed)"

4. Scroll down to **Configuration:**
   - Scripting Backend: Select "IL2CPP"
   - API Compatibility Level: Select ".NET Standard 2.1"
   - Target Architectures: ✅ Check "ARM64" ONLY (uncheck ARMv7)

### Step 6.3: Configure XR Settings

1. In Project Settings window, click **XR Plug-in Management** (left sidebar)
2. Click the **Android tab** (Android icon at top)
3. Check ✅ **Oculus** (this will auto-install if not already)

4. Wait for Oculus XR plugin to install (if needed)

5. After Oculus is checked, click **Oculus** under XR Plug-in Management (a sub-menu appears)

6. **Configure these EXACT settings:**
   - ✅ Check "Initialize XR on Startup"
   - ❌ UNCHECK "Low Overhead Mode (GLES)"
   - ❌ UNCHECK "Meta Quest: Occlusion"
   - Stereo Rendering Mode: "Multiview"
   - Target Devices: ✅ Check "Quest 3"

### Step 6.4: Fix Meta XR Issues

1. In Unity menu bar, click **Edit** → **Project Settings**
2. Click **Meta XR** (left sidebar)
3. If there's a "Fix All" button at the top, click it
4. **Fix Outstanding Issues:**
   - Click each "Fix" button EXCEPT for these two (leave these unfixed):
     - "Hand Tracking must be enabled in OVRManager..."
     - "When Using Passthrough Building Block..."
5. **Fix Recommended Items:**
   - Click each "Fix" button EXCEPT for these two (leave these unfixed):
     - "Use Low Overhead Mode"
     - "Hand tracking support is set to 'Controllers Only'..."

### Step 6.5: Save Everything

1. Click **File** → **Save Project**
2. Close the Build Settings window
3. Close the Project Settings window

---

## 7. Understanding the Scene

### Step 7.1: Open the Main Scene

1. In Unity's Project window (bottom), navigate to:
   - **Assets** → **Samples** → **DecartAI-Quest** → **DecartAI-Main.unity**
2. Double-click **DecartAI-Main.unity** to open it

### Step 7.2: Scene Overview

You'll see several important GameObjects in the Hierarchy (left panel):

**Main Components:**
- **OVRCameraRig** - This is your VR camera and controllers
- **MainCanvas** - The main menu UI
- **TimeTravelPanel** - Time travel feature UI
- **ClothingPanel** - Virtual mirror clothing UI
- **BiomePanel** - Biome/country transformation UI
- **VideoGamePanel** - Video game style UI
- **CustomPromptPanel** - Custom prompt input UI
- **WebRTCConnection** - Handles AI video processing
- **PassthroughCameraManager** - Manages Quest camera

**What Each Panel Does:**
- All panels are UI canvases that appear in VR
- Only one panel shows at a time (menu system handles this)
- Each panel has its own controller script

---

## 8. Testing in Unity Editor (Optional)

⚠️ **Note:** VR features won't work in editor, but you can test UI logic

1. Click the **Play** button (▶️ at top center of Unity)
2. You'll see the scene in Game view
3. Press **Play** again (⏸) to stop
4. Skip to Building for Quest to test the real app

---

## 9. Building for Meta Quest 3

### Step 9.1: Prepare for Build

1. Make sure your Quest 3 is still connected via USB
2. Make sure it's not in sleep mode (tap the headset)
3. Verify connection:
   ```bash
   adb devices
   ```
   Should show your Quest as "device"

### Step 9.2: Configure Build Settings

1. In Unity, click **File** → **Build Settings**
2. Make sure "Android" is selected
3. Click **Add Open Scenes** button (this adds DecartAI-Main.unity if not already there)
4. Verify the scene list shows: "Scenes/DecartAI-Main"

5. **Texture Compression:**
   - Find "Texture Compression" dropdown
   - Select "ASTC"

6. **Development Build (optional but recommended for first build):**
   - ✅ Check "Development Build"
   - This helps with debugging if something goes wrong

### Step 9.3: Build the APK

**Option A: Build and Run (Direct Install)**
1. Click **Build And Run** button (bottom right)
2. When "Save As" dialog appears:
   - Navigate to a folder you can find easily (like Documents)
   - Name it: `DecartQuest3.apk`
   - Click "Save"
3. **WAIT PATIENTLY** - first build takes 10-20 minutes!
4. You'll see progress bar at bottom of Unity
5. When complete:
   - The APK installs automatically to Quest
   - The app launches automatically on your Quest
   - Put on headset to use it!

**Option B: Build Only (Manual Install Later)**
1. Click **Build** button (bottom right)
2. Save as `DecartQuest3.apk` in Documents folder
3. Wait for build to complete
4. Continue to Step 10 for manual installation

### Step 9.4: What Happens During Build

The progress bar shows different stages:
1. "Building Player..." (2-5 min)
2. "Building IL2CPP..." (5-10 min)
3. "Creating APK..." (2-3 min)
4. "Installing to Quest..." (1-2 min)

**DO NOT CLOSE UNITY OR DISCONNECT QUEST DURING BUILD!**

---

## 10. Installing on Quest Device

### If you used "Build And Run" - SKIP THIS SECTION

### Method 1: Using ADB (Command Line)

1. Open Command Prompt or Terminal

2. Navigate to where you saved the APK:
   ```bash
   cd Documents
   ```

3. Install the APK:
   ```bash
   adb install -r DecartQuest3.apk
   ```

4. Wait for "Success" message (30 seconds to 2 minutes)

5. The app is now installed!

### Method 2: Using SideQuest (Easier)

1. Open SideQuest application
2. Connect Quest with USB (if not already)
3. In SideQuest, click the folder icon (📁) at the top
4. Navigate to your APK file
5. Click "Open"
6. Wait for installation to complete
7. You'll see success message

### Method 3: Using Meta Quest Developer Hub (Official)

1. Download Meta Quest Developer Hub from:
   https://developer.oculus.com/downloads/package/oculus-developer-hub-win/
2. Install it
3. Open Meta Quest Developer Hub
4. Connect your Quest
5. Click "Device Manager"
6. Click "Add Build"
7. Select your APK file
8. Wait for installation

---

## 11. Using the Application

### Step 11.1: Launch the App

**In Your Quest Headset:**
1. Put on the headset
2. Press the **Meta button** (Oculus logo on right controller)
3. Click "Apps" in the menu
4. Click dropdown at top: select "Unknown Sources"
5. Find "DecartAI-Quest"
6. Click it to launch

### Step 11.2: First Time Setup

**Camera Permissions:**
1. First launch will ask: "Allow DecartAI-Quest to access your camera?"
2. Click "Allow"
3. You should see your passthrough camera feed

**Note:** If you don't see the camera feed:
- Close app
- Go to Settings → Apps → DecartAI-Quest → Permissions
- Enable Camera
- Re-launch app

### Step 11.3: Controller Layout

**Left Controller:**
- **Trigger (Index):** Go Back to previous menu
- **Joystick (Thumbstick):** Navigate up/down in menus

**Right Controller:**
- **Trigger (Index):** Confirm selection / Apply transformation
- **Joystick (Thumbstick):** Control sliders (left/right)

**Either Controller:**
- **Menu Button (Hamburger/Start):** Show/Hide menu

### Step 11.4: Using Each Feature

**Main Menu:**
1. Use **left joystick UP/DOWN** to highlight menu options
2. Press **right trigger** to select
3. Current options:
   - Time Travel
   - Virtual Mirror (Clothing)
   - Biome/Country
   - Video Game Style
   - Custom Prompt

**Time Travel Feature:**
1. Select "Time Travel" from main menu
2. Use **right joystick LEFT/RIGHT** to move time slider
3. Watch the year change
4. The environment transforms to that era
5. Try different years: 1500, 1920, 2050, 2200, etc.
6. Press **left trigger** to go back to main menu

**Virtual Mirror (Clothing):**
1. Stand in front of a real mirror (optional, for best effect)
2. Select "Virtual Mirror" from main menu
3. Use **left joystick UP/DOWN** to browse clothing
4. Press **right trigger** to try on selected outfit
5. Wait 3-5 seconds for AI to apply
6. Browse and try different outfits
7. Press **left trigger** to go back

**Biome/Country:**
1. Select "Biome/Country" from main menu
2. Use **left joystick UP/DOWN** to browse locations
3. Press **right trigger** to apply transformation
4. Your room transforms to that environment
5. Try: Japan, Arctic Tundra, Tropical Rainforest, etc.
6. Press **left trigger** to go back

**Video Game Style:**
1. Select "Video Game Style" from main menu
2. Use **left joystick UP/DOWN** to browse game styles
3. Press **right trigger** to apply
4. Environment transforms to that game's aesthetic
5. Try: Minecraft, Cyberpunk 2077, Zelda, Portal, etc.
6. Press **left trigger** to go back

**Custom Prompt:**
1. Select "Custom Prompt" from main menu
2. Point at the text input field
3. Press **right trigger** to activate virtual keyboard
4. Type your custom transformation prompt
5. Press **right trigger** again OR click "Submit" to apply
6. Wait for AI to process your request
7. Press **left trigger** to go back

### Step 11.5: Tips for Best Results

**For All Features:**
- **Good lighting** - Make sure your room is well-lit
- **Clean camera lenses** - Wipe Quest cameras if image is blurry
- **Stable internet** - Connect to 5GHz WiFi for best performance
- **Wait for processing** - First transformation takes 5-10 seconds
- **Subsequent changes** - Take 2-3 seconds each

**For Clothing/Mirror:**
- Stand still for best results
- Full body visible works best
- Good lighting on yourself

**For Custom Prompts:**
- Be descriptive: "Transform into cyberpunk city with neon lights and rain"
- Not just: "cyberpunk"
- Use 20-50 words for best results

---

## 12. Troubleshooting

### Problem: Unity won't open the project

**Solution:**
1. Check Unity Hub shows correct Unity version (6000.0.34f1)
2. Try: Unity Hub → Installs → Click ⋮ next to Unity version → "Open Editor"
3. Check you're opening the `DecartAI-Quest-Unity` folder, not parent folder

### Problem: "Unable to add package" errors

**Solution:**
1. Unity menu: Window → Package Manager
2. Click the ⋮ at top right
3. Click "Reset Packages to defaults"
4. Wait for re-import

### Problem: Build fails with "Android SDK not found"

**Solution:**
1. Unity → Edit → Preferences → External Tools
2. Find "Android SDK Tools"
3. If empty, click "Download" or point to:
   - Windows: `C:\Program Files\Unity\Hub\Editor\6000.0.34f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK`
4. Same for "Android NDK" and "JDK"

### Problem: Quest not detected by ADB

**Solution:**
1. Reconnect USB cable
2. Put on Quest, check for "Allow USB Debugging?" dialog
3. Try different USB port
4. Try: `adb kill-server` then `adb devices`
5. Make sure Developer Mode is ON in Quest settings

### Problem: App crashes on launch

**Solution:**
1. Check Quest settings: Settings → Apps → DecartAI-Quest → Permissions
2. Enable Camera permission
3. Reinstall app:
   ```bash
   adb uninstall com.YourCompany.DecartAI-Quest
   adb install DecartQuest3.apk
   ```

### Problem: No camera feed in app

**Solution:**
1. Grant camera permission (see above)
2. Check passthrough is enabled in Quest settings
3. Rebuild with camera permissions enabled in manifest
4. Check Quest OS version is v74+

### Problem: No AI transformations happening

**Solution:**
1. Check internet connection on Quest
2. Settings → Wi-Fi → Connect to strong network
3. Try 5GHz WiFi instead of 2.4GHz
4. Restart app
5. Check Decart AI service status

### Problem: Transformations are very slow

**Solution:**
1. Connect to faster WiFi (5GHz recommended)
2. Move closer to WiFi router
3. Close other apps using bandwidth
4. Restart Quest device
5. First transformation is always slower (5-10 sec), subsequent ones faster (2-3 sec)

### Problem: Build takes forever / Unity freezes

**Solution:**
1. **This is NORMAL for first build** - can take 20+ minutes
2. Check Unity isn't actually frozen:
   - Look for progress bar movement
   - Check Task Manager (Windows) or Activity Monitor (Mac)
   - Unity process should show 10-30% CPU usage
3. Don't click anything, just wait
4. If truly frozen (no progress after 1 hour):
   - Close Unity
   - Delete `Library` folder in project
   - Reopen project (will re-import, 10 min)
   - Try build again

---

## Additional Resources

**Meta Quest Development:**
- https://developer.oculus.com/documentation/

**Unity Learn:**
- https://learn.unity.com/

**Decart AI Platform:**
- https://platform.decart.ai/

**Project Discord:**
- https://discord.gg/decart

**Need Help?**
- GitHub Issues: https://github.com/Jakubikk/joystick-nav/issues
- Email: tom@decart.ai

---

## Glossary of Terms

**APK:** Android Package file - the installable app file  
**ADB:** Android Debug Bridge - tool to communicate with Android devices  
**SDK:** Software Development Kit - tools for development  
**IL2CPP:** Unity's ahead-of-time compiler for better performance  
**WebRTC:** Real-time communication protocol for video streaming  
**Passthrough:** Quest's camera feed showing real world  
**Decart AI:** The AI service that transforms video  
**Prompt:** Text description telling AI how to transform video  

---

**Congratulations!** You've successfully built and deployed a Meta Quest 3 XR application! 🎉


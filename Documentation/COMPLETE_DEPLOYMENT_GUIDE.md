# Complete Beginner's Guide: Decart Quest 3 App - From Clone to Production

This guide will walk you through every step needed to build and deploy the Decart Quest 3 app, from cloning the repository to shipping to production. **No prior Unity experience required!**

---

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Installing Required Software](#installing-required-software)
3. [Cloning the Repository](#cloning-the-repository)
4. [Setting Up Unity](#setting-up-unity)
5. [Opening the Project](#opening-the-project)
6. [Understanding the Project Structure](#understanding-the-project-structure)
7. [Configuring the Project](#configuring-the-project)
8. [Setting Up Meta Quest Development](#setting-up-meta-quest-development)
9. [Building the Application](#building-the-application)
10. [Installing on Quest 3](#installing-on-quest-3)
11. [Testing the App](#testing-the-app)
12. [Troubleshooting](#troubleshooting)
13. [Deploying to Production](#deploying-to-production)

---

## Prerequisites

### Hardware Requirements
- **Meta Quest 3** headset (or Quest 3S)
- Windows PC or Mac with:
  - 16GB RAM minimum (32GB recommended)
  - 50GB free disk space
  - USB-C cable to connect Quest 3 to computer
- Good internet connection (8+ Mbps recommended)

### Accounts You'll Need
- Meta/Oculus account (free)
- Meta Developer account (free) - we'll set this up
- Decart AI account (free trial available) - for API access

---

## Installing Required Software

### Step 1: Install Unity Hub

1. Go to https://unity.com/download
2. Click **Download Unity Hub**
3. Run the downloaded installer
4. Click **Next** through the installation wizard
5. Launch Unity Hub when installation completes

### Step 2: Install Unity Editor 6

1. In Unity Hub, click **Installs** on the left sidebar
2. Click **Install Editor** button (blue button in top right)
3. Find **Unity 6 (6000.0.34f1)** or latest Unity 6 version
4. Click **Install**
5. In the "Add modules" screen, check these boxes:
   - ✅ **Android Build Support**
     - ✅ Android SDK & NDK Tools
     - ✅ OpenJDK
   - ✅ **Documentation** (optional but helpful)
6. Click **Continue** and wait for installation (this takes 20-40 minutes)

### Step 3: Install Git

**Windows:**
1. Go to https://git-scm.com/download/win
2. Download and run the installer
3. Click **Next** through all options (defaults are fine)

**Mac:**
1. Open Terminal (Applications → Utilities → Terminal)
2. Type: `git --version` and press Enter
3. If prompted, click **Install** to install Xcode Command Line Tools

### Step 4: Install Android Debug Tools (ADB)

**Windows:**
1. Go to https://developer.android.com/tools/releases/platform-tools
2. Download "SDK Platform-Tools for Windows"
3. Extract the ZIP file to `C:\platform-tools`
4. Add to PATH:
   - Press Windows key, type "Environment Variables"
   - Click "Edit the system environment variables"
   - Click "Environment Variables" button
   - Under "System variables", find "Path" and click "Edit"
   - Click "New" and add `C:\platform-tools`
   - Click OK on all windows

**Mac:**
1. Open Terminal
2. Install Homebrew if not installed: `/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"`
3. Install ADB: `brew install android-platform-tools`

---

## Cloning the Repository

### Step 1: Open Terminal/Command Prompt

**Windows:**
- Press Windows key + R
- Type `cmd` and press Enter

**Mac:**
- Press Command + Space
- Type `Terminal` and press Enter

### Step 2: Navigate to Your Desired Folder

```bash
# Windows example:
cd C:\Users\YourName\Documents

# Mac example:
cd ~/Documents
```

### Step 3: Clone the Repository

```bash
git clone https://github.com/Jakubikk/joystick-nav.git
cd joystick-nav
```

---

## Setting Up Unity

### Step 1: Add Project to Unity Hub

1. Open Unity Hub
2. Click **Projects** on the left sidebar
3. Click **Add** button (or **Open** dropdown → **Add project from disk**)
4. Navigate to where you cloned the repo
5. Select the folder: `joystick-nav/DecartAI-Quest-Unity`
6. Click **Select Folder** (or **Open** on Mac)

### Step 2: Open the Project

1. In Unity Hub, find your newly added project
2. Make sure it shows **Unity 6000.0.34f1** (or your installed version)
3. Click on the project to open it
4. **First time opening will take 10-20 minutes** as Unity imports all assets

**What you'll see:**
- Unity Editor will open
- Console window may show some warnings (this is normal)
- Wait until the progress bar at bottom-right completes

---

## Opening the Project

### Finding the Main Scene

1. In the **Project** window (bottom of screen), navigate to:
   ```
   Assets → Samples → DecartAI-Quest → DecartAI-Main.unity
   ```
2. Double-click **DecartAI-Main.unity** to open the scene
3. You should now see the VR scene in the **Scene** view (middle of screen)

---

## Understanding the Project Structure

### Key Folders:

```
DecartAI-Quest-Unity/
├── Assets/
│   ├── Samples/
│   │   └── DecartAI-Quest/
│   │       ├── DecartAI-Main.unity      (Main scene)
│   │       └── Scripts/
│   │           ├── Menu/                 (New menu system)
│   │           │   ├── MenuManager.cs
│   │           │   ├── TimeTravelFeature.cs
│   │           │   ├── VirtualTryOnFeature.cs
│   │           │   ├── BiomeTransformFeature.cs
│   │           │   ├── VideoGameStyleFeature.cs
│   │           │   └── CustomPromptFeature.cs
│   │           └── WebRTCController.cs   (Main controller)
│   ├── PassthroughCameraApiSamples/      (Camera access)
│   └── metaVoiceSdk/                     (Voice SDK - not used)
└── LocalPackages/
    └── com.firedragongamestudio.simplewebrtc/  (WebRTC)
```

---

## Configuring the Project

### Step 1: Install Required Unity Packages

1. Go to **Window** → **Package Manager**
2. Wait for the package list to load
3. Verify these packages are installed (look in the list on the left):
   - ✅ Meta XR Core SDK
   - ✅ XR Plugin Management
   - ✅ Oculus XR Plugin
   - ✅ Universal Render Pipeline
   - ✅ TextMeshPro
   - ✅ Input System

If any are missing:
1. Click the **+** button in top-left
2. Select **Add package by name**
3. Enter the package name (e.g., `com.meta.xr.sdk.core`)
4. Click **Add**

### Step 2: Configure Project Settings for Quest

1. Go to **Edit** → **Project Settings**

#### Player Settings

1. Click **Player** on the left
2. Click the **Android tab** (Android robot icon)
3. Under **Other Settings**:
   - **Company Name**: Enter your company/developer name
   - **Product Name**: `Decart Quest 3 VR`
   - **Package Name**: `com.yourname.decartquest` (use lowercase, no spaces)
   - **Version**: `1.0.0`
   - **Minimum API Level**: Select **Android 10.0 (API level 29)**
   - **Target API Level**: Select **Android 14.0 (API level 34)**
   - **Scripting Backend**: Select **IL2CPP**
   - **Api Compatibility Level**: **.NET Standard 2.1**
   - **Target Architectures**: Check ✅ **ARM64** only (uncheck ARMv7)

4. Under **Other Settings** → **Configuration**:
   - **Color Space**: Select **Linear**

5. Under **Other Settings** → **Graphics**:
   - **Graphics APIs**: Should show **Vulkan** or **OpenGLES3**
   - If it shows others, click the minus (-) to remove them

6. Scroll down to **XR Settings** (may be at bottom):
   - Make sure nothing is checked here (XR Plugin Management handles this)

#### XR Plugin Management

1. Click **XR Plug-in Management** on the left
2. Click the **Android tab** (Android robot icon)
3. Check ✅ **Oculus**
4. Click **Oculus** under XR Plug-in Management (on the left)
5. Configure these settings:
   - ✅ **Initialize XR on Startup**: Checked
   - ❌ **Low Overhead Mode (GLES)**: **UNCHECKED** (important!)
   - ❌ **Meta Quest: Occlusion**: **UNCHECKED**
   - ❌ **Meta XR Subsampled Layout**: **UNCHECKED**
   - **Target Devices**: Select **Quest 3** and **Quest 3S**

#### Quality Settings

1. Click **Quality** on the left
2. For Android, select **Medium** or **High** quality level
3. **Anti-Aliasing**: Set to **4x Multi Sampling**

7. Close Project Settings window

### Step 3: Configure Build Settings

1. Go to **File** → **Build Settings**
2. Click **Android** in the Platform list
3. If it shows **Switch Platform** button, click it and wait (takes a few minutes)
4. Once Android is selected (Unity icon appears next to it):
   - **Texture Compression**: Select **ASTC**
   - **Build System**: **Gradle**
   - Check ✅ **Development Build** (for debugging)
5. Keep this window open (we'll use it later)

### Step 4: Set Up Scene in Build

1. In Build Settings window, under **Scenes in Build**:
2. If the list is empty:
   - Click **Add Open Scenes** button
3. Make sure **DecartAI-Main** shows with a checkmark

---

## Setting Up Meta Quest Development

### Step 1: Enable Developer Mode on Quest 3

1. **On your phone/tablet:**
   - Install **Meta Quest** app from App Store or Google Play
   - Open the Meta Quest app
   - Make sure your Quest 3 is paired with the app
   - Tap **Menu** (three horizontal lines)
   - Tap **Devices**
   - Select your Quest 3
   - Tap **Headset Settings**
   - Tap **Developer Mode**
   - Toggle **Developer Mode** to ON
   
2. **If you don't see Developer Mode option:**
   - You need to become a Meta Developer:
   - Go to https://developer.oculus.com
   - Click **Sign In** (use your Meta account)
   - Accept terms and conditions
   - Verify your account (may require 2FA)
   - Create an organization name
   - Now Developer Mode should appear in the app

### Step 2: Connect Quest 3 to Computer

1. Turn on your Quest 3 headset
2. Connect USB-C cable from Quest 3 to your computer
3. **Put on the headset**
4. You should see a prompt: "Allow USB debugging?"
   - Check **Always allow from this computer**
   - Click **OK**
5. Another prompt may appear: "Allow access to data?"
   - Click **Allow**

### Step 3: Verify Connection

**Windows:**
```bash
cd C:\platform-tools
adb devices
```

**Mac:**
```bash
adb devices
```

**You should see:**
```
List of devices attached
1WMHH123ABC456    device
```

If you see `unauthorized`, put on the headset and approve the prompt.

If you see `no devices found`, try:
- Different USB port
- Different cable
- Restart Quest 3
- Restart computer

---

## Building the Application

### Step 1: Final Build Checks

1. In Unity, verify the scene is open: **DecartAI-Main.unity**
2. Go to **File** → **Build Settings**
3. Verify:
   - Platform: **Android** (selected)
   - Scene: **DecartAI-Main** is in the list and checked
   - Development Build: Checked (for testing)

### Step 2: Build the APK

**Option A: Build and Run (Recommended for first time)**

1. Make sure Quest 3 is connected and shows in `adb devices`
2. In Build Settings, click **Build And Run**
3. Choose a save location for the APK
   - Suggested: `C:\Users\YourName\Documents\DecartQuest\Builds`
   - Create a folder named `Builds`
   - Name the file: `DecartQuest-v1.0.apk`
4. Click **Save**
5. Unity will now build (this takes 10-30 minutes first time)

**What happens during build:**
- Unity compiles all scripts
- Converts assets for Android
- Packages everything into APK
- Installs directly to Quest 3
- Launches the app

**Option B: Build Only (for later installations)**

1. In Build Settings, click **Build**
2. Choose save location and filename
3. Click **Save**
4. After build completes, install manually:

```bash
adb install "C:\path\to\DecartQuest-v1.0.apk"
```

### Step 3: Monitor Build Progress

- Watch the progress bar at bottom-right of Unity
- Build log appears in **Console** window
- Warnings are normal, errors will stop the build
- **If errors occur**, see [Troubleshooting](#troubleshooting) section

---

## Installing on Quest 3

### If you used "Build and Run"

The app is automatically installed and launched on your Quest 3!

### Manual Installation

If you only built the APK without installing:

```bash
# Navigate to your build folder
cd "C:\Users\YourName\Documents\DecartQuest\Builds"

# Install to Quest 3
adb install DecartQuest-v1.0.apk
```

**If you see "INSTALL_FAILED_UPDATE_INCOMPATIBLE":**
```bash
# Uninstall old version first
adb uninstall com.yourname.decartquest

# Then install again
adb install DecartQuest-v1.0.apk
```

---

## Testing the App

### Step 1: Launch the App on Quest 3

1. Put on your Quest 3 headset
2. Click the **Apps** button (grid icon in the bottom menu)
3. Click **All** tab at the top
4. Look for **Decart Quest 3 VR** (or your product name)
5. Click to launch

**Can't find the app?**
- Click the filter dropdown (top right)
- Select **Unknown Sources**
- Your app should appear there

### Step 2: Grant Camera Permissions

1. When app launches, you'll see a permission request
2. Click **Allow** for Camera access
3. Click **Allow** for any other permissions requested

### Step 3: Using the App

**Main Menu Navigation:**
- **Joystick Up/Down**: Navigate through menu options
- **Right Trigger**: Confirm selection
- **Left Trigger**: Go back to previous menu
- **Hamburger Button (Start button)**: Show/Hide menu

**Features Available:**

1. **Time Travel**
   - Select to enter time travel mode
   - Use joystick up/down to adjust year (1800-2100)
   - Right trigger to apply the time period transformation
   - See your environment transform to that era!

2. **Virtual Try-On**
   - Select to see clothing options
   - Joystick up/down to browse clothing
   - Right trigger to try on selected outfit
   - Stand in front of a mirror for best results

3. **Biome Transform**
   - Select to see location/biome options
   - Joystick up/down to browse
   - Right trigger to transform room to that biome
   - Try "Japan - Cherry Blossoms" or "Tropical Beach"!

4. **Video Game Style**
   - Select to see game style options
   - Joystick up/down to browse
   - Right trigger to apply game aesthetic
   - Try "Minecraft" or "Cyberpunk 2077"!

5. **Custom Prompt**
   - Select to enter custom mode
   - Click on the text input field
   - Quest's virtual keyboard will appear
   - Type your custom transformation
   - Right trigger to apply

### Step 4: Verify Features Work

1. **Test Menu Navigation:**
   - Open/close menu with hamburger button
   - Navigate through all 5 options
   - Confirm right trigger selects items

2. **Test Time Travel:**
   - Enter Time Travel
   - Adjust slider to year 1920 (Roaring Twenties)
   - Apply and wait 5-10 seconds
   - Environment should transform to 1920s aesthetic

3. **Test Virtual Try-On:**
   - Enter Virtual Try-On
   - Select "Superhero Suit"
   - Stand in front of mirror or camera
   - You should see yourself in superhero costume

4. **Test a Custom Prompt:**
   - Enter Custom Prompt
   - Type: "Transform to magical fairy forest"
   - Apply and verify transformation works

### Step 5: Check Performance

- **Frame rate**: Should feel smooth (72+ FPS)
- **Latency**: AI transformation appears within 3-5 seconds
- **Connection**: Status should show "Connected"

**If performance is poor:**
- Check your internet connection (need 8+ Mbps)
- Move closer to WiFi router
- Close other apps on Quest
- See [Troubleshooting](#troubleshooting)

---

## Troubleshooting

### Build Errors

**Error: "Unable to list target platforms"**
- Solution: Install Android Build Support in Unity Hub
- Go to Unity Hub → Installs → Click gear icon → Add Modules → Check Android Build Support

**Error: "Android SDK not found"**
- Solution: In Unity, go to Edit → Preferences → External Tools
- Click "Download" next to Android SDK or JDK if they show as missing

**Error: "IL2CPP error"**
- Solution: Make sure IL2CPP is selected in Player Settings
- Check ARM64 is selected (not ARMv7)

**Error: Gradle build failed**
- Solution: Clean the project
  - Delete `DecartAI-Quest-Unity/Library` folder
  - Delete `DecartAI-Quest-Unity/Temp` folder
  - Reopen project in Unity
  - Try building again

### Runtime Errors

**App crashes on launch**
- Check Quest OS is updated (need v74+)
- Verify all permissions granted
- Try Developer Build mode
- Check Unity logs in Android Logcat

**Camera not working**
- Grant camera permissions in Quest settings
- Settings → Apps → Decart Quest 3 VR → Permissions
- Enable Camera and Headset Camera

**No AI processing happening**
- Check internet connection on Quest
- Verify Decart API is accessible
- Try different WiFi network
- Wait 10-15 seconds for initial connection

**Menu not responding**
- Check OVR Input is working
- Try restarting the app
- Verify controllers are paired and tracked

### Connection Issues

**Quest not detected by ADB**
- Try different USB port (USB 3.0 preferred)
- Try different USB cable
- Disable USB debugging, re-enable it
- Restart Quest 3 and computer
- Check developer mode is still enabled

**"Device unauthorized"**
- Put on headset
- Look for USB debugging prompt
- Allow and check "Always allow"

### Performance Issues

**Low frame rate**
- Close other Quest apps
- Check WiFi signal strength
- Reduce quality in Project Settings
- Disable Development Build for final version

**High latency**
- Check internet speed (need 8+ Mbps)
- Move closer to router
- Connect to 5GHz WiFi if available
- Close bandwidth-heavy apps on network

---

## Deploying to Production

### Step 1: Prepare for Release Build

1. **Disable Development Mode:**
   - File → Build Settings
   - Uncheck "Development Build"
   - Uncheck "Script Debugging"

2. **Set Final Version Number:**
   - Edit → Project Settings → Player
   - Update Version to `1.0` (or your version)
   - Update Bundle Version Code to `1` (increment for each release)

3. **Optimize Project:**
   - Edit → Project Settings → Quality
   - Select High quality preset for Android
   - Player → Other Settings → Managed Stripping Level: High

### Step 2: Build Release APK

1. File → Build Settings
2. Click **Build**
3. Save as: `DecartQuest-Release-v1.0.apk`
4. Wait for build (may take 15-30 minutes for optimized build)

### Step 3: Test Release Build

```bash
# Install release build
adb install DecartQuest-Release-v1.0.apk

# Launch and test thoroughly
# Verify all features work
# Check performance is good
```

### Step 4: Distribute Your App

**Option A: SideQuest (Easiest)**

1. Go to https://sidequestvr.com
2. Create developer account
3. Upload your APK
4. Set description, screenshots, category
5. Publish!
6. Share your SideQuest link with users

**Option B: Meta Quest Store (Official)**

1. Go to https://developer.oculus.com
2. Create new app submission
3. Fill in all required information:
   - App name, description
   - Screenshots and videos
   - Age rating
   - Privacy policy
4. Upload Release APK
5. Submit for review
6. Wait for Meta approval (can take weeks)

**Option C: Direct Distribution**

1. Share APK file directly with users
2. Users install via:
   ```bash
   adb install DecartQuest-Release-v1.0.apk
   ```
3. Or use SideQuest to load APK file

### Step 5: Create App Store Assets

**What you need:**

1. **Icon** (1024x1024 PNG)
   - Your app icon
   - No text or borders
   - Transparent background OK

2. **Screenshots** (1920x1080 or higher)
   - At least 3-5 screenshots
   - Show different features
   - Capture in-headset if possible

3. **Trailer Video** (30-60 seconds)
   - Show gameplay
   - Highlight unique features
   - MP4 format, 1080p minimum

4. **Description**
   - Explain what the app does
   - List features (Time Travel, Virtual Try-On, etc.)
   - Mention Decart AI technology
   - Include system requirements

**Example Description:**
```
Transform your reality with AI! Decart Quest 3 VR brings real-time 
AI video transformation to your Meta Quest 3.

FEATURES:
• Time Travel: View your environment in any historical era
• Virtual Try-On: Try different outfits in real-time
• Biome Transform: Transport your room to exotic locations
• Video Game Style: See reality through video game aesthetics
• Custom Prompts: Create your own transformations

Powered by Decart AI technology for sub-200ms latency.

REQUIREMENTS:
• Meta Quest 3 or Quest 3S
• 8+ Mbps internet connection
• Camera permissions
```

### Step 6: Update and Maintain

**For updates:**

1. Make changes in Unity
2. Increment version number
   - Edit → Project Settings → Player
   - Version: `1.1` (minor update) or `2.0` (major update)
   - Bundle Version Code: `2` (must be higher than previous)
3. Build new APK
4. Test thoroughly
5. Upload to SideQuest/Meta Store

**Version numbering:**
- `1.0` = First release
- `1.1` = Small updates, bug fixes
- `2.0` = Major new features

---

## Additional Resources

### Unity Documentation
- Unity Manual: https://docs.unity3d.com/Manual/
- Unity Scripting: https://docs.unity3d.com/ScriptReference/

### Meta Quest Development
- Quest Development: https://developer.oculus.com/documentation/
- Meta XR SDK: https://developer.oculus.com/downloads/package/unity-integration/

### Decart AI
- Decart Documentation: https://docs.platform.decart.ai/
- API Reference: https://docs.platform.decart.ai/api-reference
- Discord Community: https://discord.gg/decart

### Tools
- Unity Hub: https://unity.com/download
- Android Debug Bridge: https://developer.android.com/tools/adb
- SideQuest: https://sidequestvr.com

---

## Support

### Getting Help

**For Unity Issues:**
- Unity Forum: https://forum.unity.com
- Unity Answers: https://answers.unity.com

**For Quest Issues:**
- Meta Developer Forum: https://forums.oculusvr.com
- Meta Support: https://support.oculus.com

**For Decart Issues:**
- Decart Discord: https://discord.gg/decart
- Decart Support: https://platform.decart.ai

**For This Project:**
- GitHub Issues: https://github.com/Jakubikk/joystick-nav/issues
- Project Wiki: https://github.com/Jakubikk/joystick-nav/wiki

---

## Success Checklist

Before shipping to production, verify:

- ✅ All 5 features work correctly
- ✅ Menu navigation is smooth
- ✅ No crashes or errors
- ✅ Camera permissions granted
- ✅ Good performance (smooth 72+ FPS)
- ✅ AI transformations work (3-5 second response)
- ✅ Custom prompts work with keyboard
- ✅ Release build tested
- ✅ App store assets prepared
- ✅ Version number set correctly
- ✅ Privacy policy created (if required)

---

## Congratulations!

You've successfully built and deployed the Decart Quest 3 VR app! 🎉

Remember:
- Start with development builds for testing
- Test all features thoroughly
- Get feedback from users
- Iterate and improve
- Share your creation!

**Happy developing!** 🚀

---

*Last Updated: October 2025*
*Unity Version: 6000.0.34f1*
*Meta Quest OS: v74+*

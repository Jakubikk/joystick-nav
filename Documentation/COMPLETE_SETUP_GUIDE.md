# Complete Unity to Quest 3 Deployment Guide - For Absolute Beginners

This guide will take you from cloning the repository to having the app running on your Meta Quest 3, with step-by-step instructions for someone who has never used Unity before.

---

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Step 1: Install Required Software](#step-1-install-required-software)
3. [Step 2: Clone the Repository](#step-2-clone-the-repository)
4. [Step 3: Open Project in Unity](#step-3-open-project-in-unity)
5. [Step 4: Configure Unity Project Settings](#step-4-configure-unity-project-settings)
6. [Step 5: Setup the Menu System](#step-5-setup-the-menu-system)
7. [Step 6: Build for Meta Quest 3](#step-6-build-for-meta-quest-3)
8. [Step 7: Deploy to Quest 3](#step-7-deploy-to-quest-3)
9. [Step 8: Using the App](#step-8-using-the-app)
10. [Troubleshooting](#troubleshooting)

---

## Prerequisites

### Hardware Requirements
- **PC**: Windows 10/11 or macOS (Intel/Apple Silicon)
  - Minimum: 8GB RAM, 20GB free disk space
  - Recommended: 16GB RAM, SSD with 50GB free space
- **Meta Quest 3** with Horizon OS v74 or higher
- **USB-C Cable** for connecting Quest to PC

### Software You'll Need
- Unity Hub
- Unity 6 (6000.0.34f1)
- Android Build Support for Unity
- Meta Quest Developer Account (free)
- SideQuest or Meta Quest Developer Hub

---

## Step 1: Install Required Software

### 1.1 Install Unity Hub

**Windows:**
1. Go to [https://unity.com/download](https://unity.com/download)
2. Click the blue **"Download Unity Hub"** button
3. Run the downloaded installer (`UnityHubSetup.exe`)
4. Follow the installation wizard:
   - Click **"I agree"** to accept terms
   - Click **"Install"** 
   - Click **"Finish"** when done

**macOS:**
1. Go to [https://unity.com/download](https://unity.com/download)
2. Click the blue **"Download Unity Hub"** button
3. Open the downloaded `.dmg` file
4. Drag Unity Hub to your Applications folder
5. Open Unity Hub from Applications

### 1.2 Create Unity Account

1. Open Unity Hub
2. Click **"Sign in"** in the top right
3. Click **"Create account"**
4. Fill in your details and verify your email
5. Sign in with your new account

### 1.3 Get Unity Personal License

1. In Unity Hub, click your profile icon
2. Click **"Manage licenses"**
3. Click **"Add"** 
4. Select **"Get a free personal license"**
5. Click **"Agree and get personal edition license"**

### 1.4 Install Unity Editor

1. In Unity Hub, click the **"Installs"** tab on the left
2. Click the blue **"Install Editor"** button
3. Find **Unity 6 (6000.0.34f1)** or the latest Unity 6 version
4. Click **"Install"**
5. In the "Add modules" screen, make sure these are checked:
   - ✅ **Android Build Support**
     - ✅ Android SDK & NDK Tools
     - ✅ OpenJDK
   - ✅ Documentation (recommended)
6. Click **"Continue"**
7. Accept the terms and click **"Install"**
8. Wait for installation (15-30 minutes depending on internet speed)

### 1.5 Install SideQuest

**For deploying the app to Quest:**

1. Go to [https://sidequestvr.com/setup-howto](https://sidequestvr.com/setup-howto)
2. Download **SideQuest Advanced Installer** for your operating system
3. Run the installer and follow instructions
4. Open SideQuest after installation

---

## Step 2: Clone the Repository

### 2.1 Install Git (if not already installed)

**Windows:**
1. Go to [https://git-scm.com/download/win](https://git-scm.com/download/win)
2. Download and run the installer
3. Use default settings throughout installation

**macOS:**
1. Open Terminal (press Cmd+Space, type "Terminal")
2. Type: `git --version`
3. If not installed, follow prompts to install Xcode Command Line Tools

### 2.2 Clone the Repository

**Using Command Line:**
1. Open Command Prompt (Windows) or Terminal (macOS)
2. Navigate to where you want to store the project:
   ```bash
   cd Documents
   ```
3. Clone the repository:
   ```bash
   git clone https://github.com/Jakubikk/joystick-nav.git
   ```
4. Wait for download to complete

**Using GitHub Desktop (Alternative):**
1. Download GitHub Desktop from [https://desktop.github.com/](https://desktop.github.com/)
2. Install and open GitHub Desktop
3. Click **"Clone a repository"**
4. Enter URL: `https://github.com/Jakubikk/joystick-nav`
5. Choose a local path and click **"Clone"**

---

## Step 3: Open Project in Unity

### 3.1 Add Project to Unity Hub

1. Open **Unity Hub**
2. Click the **"Projects"** tab on the left
3. Click the **"Add"** button (top right, dropdown arrow next to "New project")
4. Select **"Add project from disk"**
5. Navigate to your cloned repository folder
6. Go into: `joystick-nav/DecartAI-Quest-Unity/`
7. Click **"Add Project"** or **"Select Folder"**

### 3.2 Open the Project

1. In Unity Hub's Projects tab, find **"DecartAI-Quest-Unity"**
2. Make sure the Unity version shows **"6000.0.34f1"** or similar
   - If it shows a different version, click the version dropdown and select the correct one
3. Click on the project name to open it
4. **First-time opening takes 10-30 minutes** as Unity imports all assets
5. You'll see a progress bar - be patient and don't interrupt this process

### 3.3 Verify Project Opened Successfully

Once Unity opens, you should see:
- **Scene View** (large 3D viewport in center)
- **Hierarchy** panel (left side, list of GameObjects)
- **Inspector** panel (right side, properties)
- **Project** panel (bottom, files and folders)
- **Console** panel (bottom, logs and errors)

---

## Step 4: Configure Unity Project Settings

### 4.1 Switch to Android Build Platform

1. Click **"File"** menu → **"Build Settings"**
2. In the Platform list, select **"Android"**
3. Click **"Switch Platform"** button (bottom right)
4. Wait 5-10 minutes for Unity to reimport assets for Android
5. Keep the Build Settings window open

### 4.2 Configure Player Settings

1. In the Build Settings window, click **"Player Settings"** (bottom left)
2. This opens the Project Settings window

**Configure Company and Product:**
1. Expand **"Player"** section if not already expanded
2. Set **Company Name**: `YourName` (use your name or company)
3. Set **Product Name**: `DecartXR Quest`

**Configure Android Settings:**
1. Scroll down to **"Other Settings"** section
2. Under **"Identification"**:
   - Set **Package Name**: `com.yourname.decartxr` (must be unique, use lowercase, no spaces)
   - Set **Version**: `1.0`
   - Set **Bundle Version Code**: `1`
3. Under **"Minimum API Level"**: Select **"Android 10.0 (API Level 29)"**
4. Under **"Target API Level"**: Select **"Android 14.0 (API Level 34)"** or **"Automatic (highest installed)"**
5. Under **"Scripting Backend"**: Select **"IL2CPP"**
6. Under **"Target Architectures"**: 
   - ✅ Check **ARM64**
   - ❌ Uncheck **ARMv7** (if checked)

**Configure Graphics:**
1. Scroll to **"Rendering"** section
2. **Graphics APIs**: 
   - Click the **+** and add **"OpenGLES3"** if not present
   - Remove **"Vulkan"** if present (click and press Delete or click - button)
   - Only **OpenGLES3** should remain
3. **Color Space**: Select **"Linear"**

**Configure XR Settings:**
1. In Project Settings, find **"XR Plug-in Management"** on the left sidebar
2. Click on the **Android tab** (Android robot icon)
3. Check ✅ **"Oculus"** (or **"Meta XR"** in newer versions)
4. Click on **"Oculus"** settings (appears below after checking)
5. Settings should be:
   - ✅ **Initialize XR on Startup**
   - ❌ **Low Overhead Mode** (MUST BE UNCHECKED)
   - ❌ **Meta Quest: Occlusion** (MUST BE UNCHECKED)
   - ❌ **Meta XR Subsampled Layout** (MUST BE UNCHECKED)

**Add Camera Permissions:**
1. In Project Settings, stay in **"XR Plug-in Management"** → **"Oculus"** 
2. Or go to **"Player"** → **"Android"** → **"Other Settings"**
3. Find **"Android Permissions"** section (may need to scroll)
4. Make sure **"Camera"** permission is enabled
5. Note: You may need to manually add `horizonos.permission.HEADSET_CAMERA` to AndroidManifest.xml later

### 4.3 Resolve Meta XR Configuration Issues

1. Go to **"Edit"** menu → **"Project Settings"**
2. Click **"Meta XR"** on the left sidebar (if available)
3. Look for **"Outstanding Issues"** section
4. Click **"Fix All"** for most issues, EXCEPT:
   - ❌ Do NOT fix: "Hand Tracking must be enabled in OVRManager"
   - ❌ Do NOT fix: "Passthrough Building Block background transparency"
5. Look for **"Recommended Items"** section
6. Click **"Apply"** for most recommendations, EXCEPT:
   - ❌ Do NOT apply: "Use Low Overhead Mode"
   - ❌ Do NOT apply: "Hand tracking support"

---

## Step 5: Setup the Menu System

### 5.1 Open the Main Scene

1. In the **Project** panel (bottom), navigate to:
   ```
   Assets → Samples → DecartAI-Quest → DecartAI-Main.unity
   ```
2. Double-click **"DecartAI-Main.unity"** to open the scene

### 5.2 Create Menu System GameObjects

We'll create the UI structure in Unity Editor:

**Create Main Menu Canvas:**

1. In **Hierarchy** panel, right-click in empty space
2. Select **"UI"** → **"Canvas"**
3. This creates a Canvas GameObject - rename it to **"MenuCanvas"**
4. Select **MenuCanvas** in Hierarchy
5. In **Inspector** panel, find **Canvas** component
6. Set **Render Mode**: **"World Space"**
7. Set **Position**: `X=0, Y=1.5, Z=2` (2 meters in front, eye level)
8. Set **Rotation**: `X=0, Y=0, Z=0`
9. Set **Scale**: `X=0.001, Y=0.001, Z=0.001`
10. In **Rect Transform**:
    - **Width**: 2000
    - **Height**: 1500

**Create Menu Title Text:**

1. Right-click on **MenuCanvas** in Hierarchy
2. Select **"UI"** → **"Text - TextMeshPro"**
3. If prompted to "Import TMP Essentials", click **"Import TMP Essentials"**
4. Rename this to **"MenuTitleText"**
5. In Inspector:
   - Set **Text**: `"Decart XR - Main Menu"`
   - Set **Font Size**: `80`
   - Set **Alignment**: Center and Top
   - Set **Color**: White
   - In **Rect Transform**:
     - **Pos Y**: `650`
     - **Width**: `1800`
     - **Height**: `150`

**Create Menu Items Container:**

1. Right-click on **MenuCanvas**
2. Select **"UI"** → **"Vertical Layout Group"** (this creates an empty GameObject)
3. Rename to **"MenuItemsContainer"**
4. In Inspector **Rect Transform**:
   - **Pos Y**: `0`
   - **Width**: `1600`
   - **Height**: `1000`
5. In **Vertical Layout Group** component:
   - **Spacing**: `20`
   - **Child Alignment**: Upper Center
   - Check ✅ **Child Force Expand Width**
   - Check ✅ **Child Force Expand Height**

### 5.3 Create Feature Panels

For each feature, we'll create a panel. Let's do Time Travel as an example:

**Create Time Travel Panel:**

1. Right-click on **MenuCanvas** in Hierarchy
2. Select **"UI"** → **"Panel"**
3. Rename to **"TimeTravelPanel"**
4. In Inspector **Rect Transform**, click the anchor preset button (small square with crosshairs)
5. Hold **Alt+Shift** and click the bottom-right preset (stretch to fill parent)
6. Set all **Left/Right/Top/Bottom** to `0` to fill the canvas
7. In **Image** component, set **Color**: Semi-transparent black `(0, 0, 0, 200)`
8. **Disable** this panel by unchecking the checkbox next to its name in Inspector

**Add Time Travel UI Elements:**

1. Right-click **TimeTravelPanel**
2. Add **"UI"** → **"Slider"**
3. Rename to **"YearSlider"**
4. Configure slider position and size in Rect Transform
5. Add **"UI"** → **"Text - TextMeshPro"** for year display
6. Rename to **"YearText"**
7. Add another **"Text - TextMeshPro"** for description
8. Rename to **"DescriptionText"**
9. Add **"UI"** → **"Button - TextMeshPro"**
10. Rename to **"ApplyButton"**

Repeat this process for:
- **TryOnPanel** (with scrollable list)
- **BiomePanel** (with scrollable list)
- **VideoGamePanel** (with scrollable list)
- **CustomPromptPanel** (with input field and buttons)

### 5.4 Attach Scripts to GameObjects

**Attach MenuManager:**

1. Select **MenuCanvas** in Hierarchy
2. In Inspector, click **"Add Component"**
3. Type **"MenuManager"** and select it
4. This creates a MenuManager component
5. Drag-and-drop the appropriate GameObjects to the inspector fields:
   - **Menu Canvas**: Drag **MenuCanvas** here
   - **Menu Title Text**: Drag **MenuTitleText** here
   - **Menu Items Container**: Drag **MenuItemsContainer** here
   - **Time Travel Panel**: Drag **TimeTravelPanel** here
   - **Try On Panel**: Drag **TryOnPanel** here
   - **Biome Panel**: Drag **BiomePanel** here
   - **Video Game Panel**: Drag **VideoGamePanel** here
   - **Custom Prompt Panel**: Drag **CustomPromptPanel** here
   - **Web RTC Connection**: Find **WebRTCConnection** GameObject in scene and drag here

**Attach Feature Scripts:**

1. Select **TimeTravelPanel**
2. Click **"Add Component"**
3. Add **"TimeTravelFeature"** script
4. Assign all required fields in Inspector

Repeat for other panels with their respective scripts:
- **TryOnPanel** → **TryOnFeature**
- **BiomePanel** → **BiomeFeature**  
- **VideoGamePanel** → **VideoGameFeature**
- **CustomPromptPanel** → **CustomPromptFeature**

### 5.5 Save the Scene

1. Press **Ctrl+S** (Windows) or **Cmd+S** (macOS)
2. Or go to **"File"** → **"Save"**
3. Your scene is now saved with the menu system!

---

## Step 6: Build for Meta Quest 3

### 6.1 Prepare Build Settings

1. Open **"File"** → **"Build Settings"**
2. Click **"Add Open Scenes"** to add DecartAI-Main.unity (if not already added)
3. Verify:
   - Platform is **"Android"**
   - Scene **"DecartAI-Main"** is checked in the scene list

### 6.2 Build the APK

1. Click **"Build"** button (NOT "Build And Run" yet)
2. Choose a save location (e.g., create a "Builds" folder in your project)
3. Name the file: `DecartXR-Quest.apk`
4. Click **"Save"**
5. Unity will build the APK (takes 15-30 minutes for first build)
6. Watch the progress bar at the bottom of Unity
7. If errors appear in Console, see [Troubleshooting](#troubleshooting) section
8. When complete, you'll get a notification

---

## Step 7: Deploy to Quest 3

### 7.1 Enable Developer Mode on Quest 3

**Create Meta Developer Account:**
1. On your phone, open the **Meta Quest app**
2. Go to **Menu** → **Settings** → **Developer**
3. Tap **"Create Organization"**
4. Follow prompts to create a developer account (free)

**Enable Developer Mode:**
1. In Meta Quest app on phone
2. Go to **Menu** → **Devices** → Select your **Quest 3**
3. Tap **"Developer Mode"**
4. Toggle **ON**
5. Your Quest will need to restart - confirm the restart

### 7.2 Connect Quest to PC

1. Plug your Quest 3 into your PC using USB-C cable
2. Put on the Quest headset
3. You'll see a prompt: **"Allow USB debugging?"**
4. Check ✅ **"Always allow from this computer"**
5. Click **"OK"**

### 7.3 Deploy Using SideQuest

**Method A: Using SideQuest**

1. Open **SideQuest** on your PC
2. Your Quest should show as **connected** (green dot)
3. Click the **"Install APK file"** icon (top toolbar, folder icon)
4. Navigate to your build location
5. Select **DecartXR-Quest.apk**
6. Click **"Open"**
7. Wait for installation to complete
8. You'll see a success message when done

**Method B: Using ADB Command Line**

1. Open Command Prompt (Windows) or Terminal (macOS)
2. Navigate to your build folder:
   ```bash
   cd path/to/your/Builds
   ```
3. Run ADB install command:
   ```bash
   adb install DecartXR-Quest.apk
   ```
4. Wait for "Success" message

**Method C: Build And Run from Unity**

1. With Quest still connected
2. In Unity **Build Settings**
3. Click **"Build And Run"**
4. This builds and automatically installs to Quest
5. App will launch automatically when done

---

## Step 8: Using the App

### 8.1 Launch the App

1. Put on your Quest 3
2. Press the **Oculus/Meta button** on right controller
3. Go to **"Library"**
4. Click **"All"** filter
5. Select **"Unknown Sources"** from dropdown
6. Find **"DecartXR Quest"** (or your Product Name)
7. Click to launch

### 8.2 Grant Permissions

On first launch:
1. App will request **Camera Permission**
2. Click **"Allow"**
3. App will start showing passthrough camera

### 8.3 Navigation Controls

**Button Layout:**
- **Left Trigger**: Go back to previous menu
- **Right Trigger**: Confirm selection / Apply
- **Left Joystick Up/Down**: Navigate through menu options
- **Start Button (Hamburger)**: Show/Hide menu
- **Right Joystick Left/Right**: Adjust sliders (Time Travel feature)

### 8.4 Using Each Feature

**Time Travel:**
1. Select "Time Travel" from main menu
2. Press Right Trigger to confirm
3. Use Right Joystick Left/Right to change year
4. Press Right Trigger to apply transformation
5. Press Left Trigger to go back

**Virtual Try-On:**
1. Stand in front of a mirror (for best results)
2. Select "Virtual Try-On"
3. Use Joystick Up/Down to browse clothing
4. Press Right Trigger to try on selected clothing
5. See yourself transformed in the mirror

**Biome Transform:**
1. Select "Biome Transform"
2. Browse locations with Joystick Up/Down
3. Press Right Trigger to apply transformation
4. Your room transforms to selected environment

**Video Game Style:**
1. Select "Video Game Style"  
2. Browse game styles with Joystick Up/Down
3. Press Right Trigger to apply
4. Your environment transforms to game aesthetic

**Custom Prompt:**
1. Select "Custom Prompt"
2. Press Right Trigger to open Meta keyboard
3. Type your custom transformation (e.g., "Make everything look like candy")
4. Press Enter or select done on keyboard
5. Press A button to submit prompt

---

## Troubleshooting

### Unity Won't Open Project

**Problem:** "This project uses a different Unity version"
- **Solution**: Install the correct Unity version (6000.0.34f1) via Unity Hub

**Problem:** Import errors or missing packages
- **Solution**: 
  1. Close Unity
  2. Delete the `Library` folder in project root
  3. Reopen project (will take longer to import)

### Build Errors

**Problem:** "Android SDK not found"
- **Solution**: 
  1. In Unity Hub → Installs
  2. Click ⋮ next to Unity version
  3. Select "Add modules"
  4. Install Android Build Support + SDK & NDK

**Problem:** "Unable to find il2cpp.exe"
- **Solution**: Install IL2CPP via Unity Hub modules

**Problem:** Compilation errors in scripts
- **Solution**: 
  1. Check Console panel for specific errors
  2. Make sure all .meta files exist
  3. Ensure all required packages are imported

### Quest Connection Issues

**Problem:** Quest not showing in SideQuest
- **Solution**:
  1. Enable Developer Mode in Meta Quest app
  2. Check USB cable is data-capable (not charge-only)
  3. Accept USB debugging prompt on Quest
  4. Try different USB port
  5. Restart Quest and PC

**Problem:** "adb: command not found"
- **Solution**: Add Android SDK platform-tools to system PATH
  - Windows: Add `C:\Program Files\Unity\Hub\Editor\[version]\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools`
  - macOS: Add to `~/.bash_profile` or `~/.zshrc`

### App Crashes on Quest

**Problem:** App crashes immediately on launch
- **Solution**:
  1. Check Quest has enough storage space
  2. Verify camera permissions were granted
  3. Ensure Horizon OS is v74 or higher
  4. Rebuild with Development Build checked for logs

**Problem:** Black screen in app
- **Solution**:
  1. Grant camera permissions in Quest settings
  2. Verify passthrough cameras are working in system
  3. Check adequate lighting in room

### Performance Issues

**Problem:** App is laggy or low frame rate
- **Solution**:
  1. Ensure strong WiFi connection (8+ Mbps)
  2. Close other apps on Quest
  3. Wait 5-10 seconds for AI processing to start
  4. Move to area with better lighting
  5. Reduce AI transformation complexity

**Problem:** No internet connection / AI not responding
- **Solution**:
  1. Verify Quest is connected to WiFi
  2. Test internet speed (needs 8+ Mbps)
  3. Try different WiFi network
  4. Restart the app
  5. Check Decart service status

### Menu Not Responding

**Problem:** Can't navigate menu with joystick
- **Solution**:
  1. Press Start (hamburger) button to show menu
  2. Make sure MenuManager script is attached to MenuCanvas
  3. Verify OVRInput is working (test in other Quest apps)
  4. Check that panels are properly assigned in Inspector

**Problem:** Buttons don't work
- **Solution**:
  1. Ensure buttons have Button component
  2. Verify onClick events are configured
  3. Check scripts are attached to panels
  4. Look for errors in Unity Console during Build

---

## Getting Help

If you encounter issues not covered here:

1. **Check the GitHub Issues**: [https://github.com/Jakubikk/joystick-nav/issues](https://github.com/Jakubikk/joystick-nav/issues)
2. **Join Discord**: [https://discord.gg/decart](https://discord.gg/decart)
3. **Email Support**: tom@decart.ai
4. **Unity Learn**: [https://learn.unity.com/](https://learn.unity.com/)
5. **Meta Quest Developer**: [https://developer.oculus.com/](https://developer.oculus.com/)

---

## Next Steps

After successfully deploying:

1. **Explore all features** - Try each menu option
2. **Experiment with prompts** - Create custom transformations
3. **Share feedback** - Report bugs or suggest improvements
4. **Modify and extend** - Use this as a learning project
5. **Build your own features** - Add new transformation modes

**Congratulations!** You've successfully built and deployed a VR app to Meta Quest 3! 🎉

---

*Last Updated: 2025*
*Made with ❤️ for the Quest developer community*

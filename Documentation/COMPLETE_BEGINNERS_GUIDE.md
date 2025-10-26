# Complete Beginner's Guide: Meta Quest 3 AI Transformation App
## From Clone to Production - Step by Step

This guide will walk you through everything from cloning the repository to shipping your Quest 3 app to production. No prior Unity experience required!

---

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Software Installation](#software-installation)
3. [Cloning the Repository](#cloning-the-repository)
4. [Unity Setup](#unity-setup)
5. [Project Configuration](#project-configuration)
6. [Building for Quest 3](#building-for-quest-3)
7. [Testing on Quest 3](#testing-on-quest-3)
8. [Using the App](#using-the-app)
9. [Troubleshooting](#troubleshooting)
10. [Shipping to Production](#shipping-to-production)

---

## Prerequisites

### Hardware Requirements
- **Computer**: Windows 10/11 or macOS (Windows recommended)
- **Meta Quest 3 or Quest 3S** headset
- **USB-C Cable**: To connect Quest to computer
- **Internet Connection**: 8+ Mbps bidirectional for AI processing

### Accounts You'll Need
1. **Meta Developer Account** (free)
   - Go to https://developer.oculus.com
   - Click "Sign Up" in top right
   - Use your Meta/Facebook account or create new
   
2. **Unity Account** (free)
   - Go to https://id.unity.com/account/new
   - Fill in your information
   - Verify your email

3. **Decart API Account** (for AI processing)
   - Go to https://platform.decart.ai
   - Sign up for an account
   - Navigate to API Keys section
   - Click "Create New API Key"
   - Copy and save your API key securely

---

## Software Installation

### Step 1: Install Unity Hub
Unity Hub manages Unity installations and projects.

**Windows:**
1. Go to https://unity.com/download
2. Click "Download Unity Hub" button
3. Run the downloaded `UnityHubSetup.exe`
4. Follow the installation wizard
5. Click "I Agree" to the terms
6. Choose installation location (default is fine)
7. Click "Install"
8. Launch Unity Hub when complete

**macOS:**
1. Go to https://unity.com/download
2. Click "Download Unity Hub"
3. Open the downloaded `.dmg` file
4. Drag Unity Hub to Applications folder
5. Open Applications folder and launch Unity Hub

### Step 2: Install Unity 6
1. Open Unity Hub
2. Click "Installs" on the left sidebar
3. Click "Install Editor" button (top right)
4. Select "Unity 6" → Find version **6000.0.34f1** exactly
5. Click "Continue"
6. Select these modules (very important!):
   - ✅ **Android Build Support**
   - ✅ **OpenJDK** (under Android Build Support)
   - ✅ **Android SDK & NDK Tools** (under Android Build Support)
   - ✅ **Documentation** (optional but helpful)
7. Accept the terms
8. Click "Install"
9. Wait for installation (15-30 minutes depending on internet)

### Step 3: Install Git
Git is needed to clone the repository.

**Windows:**
1. Go to https://git-scm.com/download/win
2. Download the installer
3. Run the installer
4. Use default settings for all options
5. Click "Next" through all steps
6. Click "Install"

**macOS:**
Git is usually pre-installed. To check:
1. Open Terminal (Command + Space, type "Terminal")
2. Type: `git --version`
3. If not installed, macOS will prompt you to install it

### Step 4: Enable Developer Mode on Quest 3
1. Put on your Quest 3 headset
2. Go to **Settings** (gear icon)
3. Select **System**
4. Select **Developer**
5. If Developer option doesn't appear:
   - Go to https://developer.oculus.com/manage/organizations/create
   - Create an organization (any name is fine)
   - Wait 10 minutes
   - Restart your Quest 3
   - Developer option should now appear
6. Enable **USB Connection Dialog**
7. Enable **Developer Mode**

---

## Cloning the Repository

### Step 1: Choose a Location
1. **Windows**: Open File Explorer, navigate to `C:\Users\YourName\Documents`
2. **macOS**: Open Finder, navigate to your Documents folder

### Step 2: Clone with Git

**Windows:**
1. In File Explorer, navigate to Documents folder
2. Right-click in empty space
3. Select "Git Bash Here" (if you don't see this, use Command Prompt)
4. Type this command:
```bash
git clone https://github.com/Jakubikk/joystick-nav.git
```
5. Press Enter
6. Wait for download to complete

**macOS:**
1. Open Terminal
2. Type:
```bash
cd ~/Documents
git clone https://github.com/Jakubikk/joystick-nav.git
```
3. Press Enter
4. Wait for download to complete

You should now have a folder: `Documents/joystick-nav`

---

## Unity Setup

### Step 1: Add Project to Unity Hub
1. Open **Unity Hub**
2. Click **"Projects"** tab on left sidebar
3. Click **"Add"** button (top right)
4. Navigate to where you cloned the repository
5. Go into: `joystick-nav/DecartAI-Quest-Unity`
6. Click **"Add Project"**

### Step 2: Open the Project
1. In Unity Hub, you'll see "DecartAI-Quest-Unity" in your projects list
2. Click on the project name to open it
3. **Wait patiently** - First time opening takes 5-15 minutes
4. Unity will import all assets and compile scripts
5. You'll see a progress bar at bottom of Unity window

### Step 3: Understanding the Unity Interface
When Unity opens, you'll see:
- **Scene View** (center): Where you view and edit 3D scenes
- **Game View** (center, different tab): What the app looks like running
- **Hierarchy** (left): List of objects in current scene
- **Project** (bottom): All your project files
- **Inspector** (right): Properties of selected objects
- **Console** (bottom, tab next to Project): Error and log messages

---

## Project Configuration

### Step 1: Load the Main Scene
1. In Unity, look at the **Project panel** (bottom)
2. Navigate to: `Assets/Samples/DecartAI-Quest/`
3. Double-click on **DecartAI-Main.unity**
4. The scene will load in the Scene View

### Step 2: Configure for Android/Quest
1. Go to **File** → **Build Settings**
2. In the "Platform" list, select **Android**
3. Click **"Switch Platform"** button (bottom right)
4. Wait for Unity to process (2-5 minutes)

### Step 3: Player Settings
1. Still in Build Settings window, click **"Player Settings..."** (bottom left)
2. Inspector panel on right will show Player Settings

**Company Name and Product Name:**
1. At top of Inspector, change:
   - **Company Name**: Your name or company
   - **Product Name**: Quest AI Transform (or your choice)

**Icon Settings:**
1. Scroll to **"Icon"** section
2. Click the circle next to "Default Icon"
3. You can use the existing icon or add your own

**Android Settings:**
1. Click the **Android tab** (icon looks like Android robot)
2. Expand **"Other Settings"** section

**Identification:**
- **Package Name**: Must be unique
  - Format: `com.YourCompanyName.QuestAITransform`
  - Example: `com.mystudio.questaitransform`
  - Use lowercase, no spaces
- **Version**: Leave as `0.1` for now
- **Bundle Version Code**: Leave as `1`
- **Minimum API Level**: Android 10.0 (API 29)
- **Target API Level**: Android 14.0 (API 34)

**Configuration:**
- **Scripting Backend**: IL2CPP (should already be set)
- **Target Architectures**: Check only **ARM64** (uncheck ARMv7)
- **API Compatibility Level**: .NET Standard 2.1

**Graphics APIs:**
1. Scroll to **"Rendering"** section
2. Find **"Graphics APIs"**
3. Click the **"-"** button to remove any API that's NOT OpenGLES3 or Vulkan
4. Should have: **Vulkan** and **OpenGLES3**

### Step 4: XR Plugin Management
1. Close Player Settings
2. Go to **Edit** → **Project Settings**
3. Select **XR Plug-in Management** on the left
4. Click the **Android tab** (Android icon at top)
5. Check ✅ **Oculus**

**Oculus Settings:**
1. Still in XR Plug-in Management, click **Oculus** underneath
2. Set these options:
   - ✅ **Initialize XR on Startup**
   - ❌ **Low Overhead Mode (GLES)** - MUST BE UNCHECKED
   - ❌ **Meta Quest: Occlusion** - MUST BE UNCHECKED
   - ❌ **Meta XR Subsampled Layout** - MUST BE UNCHECKED

### Step 5: Meta XR Settings (Important!)
1. Go to **Edit** → **Project Settings**
2. Select **Meta XR** on the left
3. You'll see several "Outstanding Issues" and "Recommended Items"

**DO NOT FIX THESE:**
- ❌ "Hand Tracking must be enabled..." - LEAVE THIS UNFIXED
- ❌ "When Using Passthrough Building Block..." - LEAVE THIS UNFIXED
- ❌ "Use Low Overhead Mode" - LEAVE THIS UNFIXED
- ❌ "Hand tracking support is set to..." - LEAVE THIS UNFIXED

**FIX ALL OTHER ISSUES** by clicking "Fix" button next to them.

### Step 6: Build Settings Scene List
1. Go back to **File** → **Build Settings**
2. Make sure **DecartAI-Main** scene is in "Scenes In Build" list
3. If not:
   - Scene should be open in Unity
   - Click **"Add Open Scenes"** button

---

## Building for Quest 3

### Step 1: Connect Your Quest 3
1. **Turn on your Quest 3** headset
2. Connect it to your computer with **USB-C cable**
3. **Put on the headset**
4. You'll see a dialog: "Allow USB Debugging?"
5. Check ✅ "Always allow from this computer"
6. Click **"Allow"**

### Step 2: Verify Connection
1. In Unity, go to **File** → **Build Settings**
2. Make sure **Android** platform is selected
3. Next to "Run Device", you should see your Quest 3 listed
4. If it says "No Devices" → Check USB cable, try different USB port

### Step 3: Build and Run
**Method 1: Build and Run (Fastest for Testing)**
1. In **File** → **Build Settings**
2. Make sure Quest 3 is selected in "Run Device"
3. Check ✅ **"Development Build"** (for debugging)
4. Click **"Build And Run"** button
5. Choose where to save the APK:
   - Create a folder called `Builds` in your project folder
   - Name the file: `QuestAITransform.apk`
6. Click **"Save"**
7. Wait for build process (first build: 10-20 minutes)

**Method 2: Build Only (For Production)**
1. In **File** → **Build Settings**
2. Uncheck "Development Build"
3. Click **"Build"** button
4. Save as `QuestAITransform_v1.0.apk`
5. Wait for build to complete

### Step 4: Build Progress
During build, you'll see:
- Progress bar at bottom of Unity
- Console messages scrolling
- Don't close Unity during this process!

**Common Build Messages (Normal):**
- "Building Player..."
- "Compiling C# scripts..."
- "Building Gradle project..."
- "Installing APK..."

---

## Testing on Quest 3

### Step 1: Launch the App
If you used "Build And Run":
- App will automatically launch on Quest 3
- Put on your headset

If you built APK only:
1. Put on Quest 3 headset
2. Press **Oculus button** (right controller)
3. Go to **Library**
4. Select **"Unknown Sources"** from dropdown at top
5. Find **"Quest AI Transform"**
6. Click to launch

### Step 2: Grant Permissions
When app first launches:
1. You'll see a permission request: **"Allow camera access?"**
2. Select **"Allow"**
3. If you don't see this, the app may not work correctly

### Step 3: First Time Setup
The app will:
1. Initialize camera (3-5 seconds)
2. Connect to WebRTC (2-3 seconds)
3. Start streaming (2-5 seconds)

---

## Using the App

### Navigation Controls
- **Joystick Up/Down**: Navigate through menu options
- **Right Trigger**: Confirm selection / Apply transformation
- **Left Trigger**: Go back to previous menu
- **Hamburger Button** (Start/Menu button on right controller): Show/Hide menu

### Main Menu Features

#### 1. Time Travel
Transform your environment to different time periods.

**How to use:**
1. Select "Time Travel" from main menu
2. Press Right Trigger to confirm
3. Use **Joystick Left/Right** to adjust year (moves by 10 years)
4. Watch the year display change
5. Press **Right Trigger** to apply the time period transformation
6. Your environment will transform to match that era!

**Available Periods:**
- 1800s: Victorian era with period furniture
- 1920s-1940s: Early 20th century, Art Deco
- 1950s-1970s: Mid-century modern
- 1980s-1990s: Retro technology aesthetic
- 2000s-2020s: Modern contemporary
- 2050s+: Futuristic sci-fi

#### 2. Virtual Clothing Try-On
See yourself in different clothing styles (stand in front of mirror for best results).

**How to use:**
1. Select "Virtual Clothing Try-On" from main menu
2. Press Right Trigger
3. Use **Joystick Up/Down** to browse clothing options
4. Options include:
   - Business suits and formal wear
   - Casual clothing
   - Traditional cultural garments
   - Costumes and fantasy outfits
   - Sports uniforms
   - Historical fashion
5. Press **Right Trigger** to try on selected outfit

**Best Results:**
- Stand in front of a mirror
- Ensure good lighting
- Face the camera directly
- Stand still for a moment after applying

#### 3. Biome Transformation
Transform your room into different environments or locations.

**How to use:**
1. Select "Biome Transformation" from main menu
2. Press Right Trigger
3. Use **Joystick Up/Down** to browse biomes
4. Categories include:
   - **Natural Biomes**: Rainforest, Arctic, Desert, Forest
   - **Cities**: Tokyo, Paris, New York, Venice, Dubai
   - **Historical**: Ancient Egypt, Medieval Castle, Greece
   - **Fantasy**: Enchanted Forest, Crystal Cave, Floating Islands
   - **Seasonal**: Winter, Spring, Summer, Autumn
   - **Sci-Fi**: Space Station, Alien Planet, Cyberpunk
5. Press **Right Trigger** to apply transformation

#### 4. Video Game World
See your environment styled like popular video games.

**How to use:**
1. Select "Video Game World" from main menu
2. Press Right Trigger
3. Use **Joystick Up/Down** to browse game styles
4. Options include:
   - **Blocky**: Minecraft, LEGO, Terraria
   - **Anime**: Anime style, Studio Ghibli
   - **Cyberpunk**: Cyberpunk 2077, Deus Ex, Tron
   - **Fantasy**: World of Warcraft, Zelda, Dark Souls, Skyrim
   - **Retro**: 8-bit, 16-bit, pixel art
   - **Realistic**: GTA, The Sims, Red Dead Redemption
   - **Horror**: Silent Hill, Resident Evil
   - **Artistic**: Journey, Okami, Mirror's Edge, Borderlands
5. Press **Right Trigger** to apply game style

#### 5. Custom Prompt
Type your own transformation ideas!

**How to use:**
1. Select "Custom Prompt" from main menu
2. Press Right Trigger
3. Look at the input field
4. **Touch the input field** with your controller pointer
5. Quest keyboard will appear
6. Type your custom transformation prompt
7. Examples:
   - "Transform into a magical forest with glowing mushrooms"
   - "Make everything look like it's made of candy"
   - "Turn this into an underwater scene with fish"
8. Press **Right Trigger** or press **A button** to submit
9. Press **B button** to clear the text field

**Recent Prompts:**
- Your last 5 prompts are saved
- Click on any recent prompt to reuse it

### Menu Visibility
- Press **Hamburger/Start button** anytime to hide/show menu
- Useful for seeing transformations without UI

### Tips for Best Results
1. **Good Lighting**: Ensure your room is well-lit
2. **Stable Position**: Try to stay relatively still
3. **Internet Connection**: Strong WiFi (5GHz recommended)
4. **Be Patient**: Transformations take 3-5 seconds to start
5. **Detailed Prompts**: More detailed custom prompts = better results

---

## Troubleshooting

### Camera Not Working
**Problem**: Black screen or no camera feed

**Solutions**:
1. Check Quest 3 permissions:
   - Go to Quest Settings → Apps → Quest AI Transform
   - Enable Camera permission
2. Restart the app
3. Restart Quest 3 headset
4. Clean camera lenses on headset
5. Ensure adequate lighting in room

### No AI Processing
**Problem**: Camera works but no transformations appear

**Solutions**:
1. **Check Internet**:
   - Quest Settings → Wi-Fi
   - Ensure connected to strong WiFi
   - Recommended: 8+ Mbps speed
2. **Wait Longer**: First connection takes 5-10 seconds
3. **Restart App**: Close completely and reopen
4. **Try Different Network**: Switch to different WiFi if available

### Build Errors in Unity
**Problem**: Build fails with errors

**Common Solutions**:

**Error: "Unable to install APK"**
- Quest not in Developer Mode → Enable it
- USB debugging not allowed → Put on headset, allow USB debugging
- Wrong USB cable → Try different cable

**Error: "Android SDK not found"**
- Go to Edit → Preferences → External Tools
- Click "Android" tab
- Make sure SDK/NDK paths are filled in
- If not, reinstall Android Build Support in Unity Hub

**Error: "Duplicate class" or "Compilation failed"**
- Delete `Library` folder in project
- Reopen project in Unity
- Let it reimport everything

### Performance Issues
**Problem**: App is laggy or stuttering

**Solutions**:
1. Close other apps on Quest 3
2. Use 5GHz WiFi instead of 2.4GHz
3. Move closer to WiFi router
4. Let Quest 3 cool down if overheating
5. Lower complexity of transformations (simpler prompts)

### Menu Not Responding
**Problem**: Can't navigate menu with joystick

**Solutions**:
1. Make sure controllers have batteries
2. Re-center controllers (hold Oculus button)
3. Try other controller if one not working
4. Restart app

### Quest 3 Not Detected in Unity
**Problem**: "No Devices" in Build Settings

**Solutions**:
1. **Check Cable**: Try different USB cable (must be data cable)
2. **Check USB Port**: Try different USB port on computer
3. **USB Debugging**:
   - Put on Quest 3
   - Should see "Allow USB Debugging?" dialog
   - Click "Always allow"
4. **Restart Both**: Restart computer AND Quest 3
5. **ADB Drivers** (Windows only):
   - Download from: https://developer.android.com/studio/run/win-usb
   - Install the drivers
   - Reconnect Quest 3

---

## Shipping to Production

### Step 1: Prepare for Release

**Update Version Numbers:**
1. Open Unity
2. Edit → Project Settings → Player
3. Android tab → Other Settings
4. Update **Version**: e.g., `1.0.0`
5. Update **Bundle Version Code**: Increment by 1

**Create Release Build:**
1. File → Build Settings
2. ✅ Ensure "Development Build" is **UNCHECKED**
3. Click "Build"
4. Save as: `QuestAITransform_v1.0.0.apk`
5. Note the file size (should be 80-150 MB)

### Step 2: Test the Release Build

**Install on Quest 3:**
1. Connect Quest 3 to computer
2. Use SideQuest (install from https://sidequestvr.com)
3. Or use ADB command:
```bash
adb install QuestAITransform_v1.0.0.apk
```

**Full Testing Checklist:**
- [ ] App launches without errors
- [ ] Camera permissions requested and work
- [ ] All 5 menu options accessible
- [ ] Time Travel feature works
- [ ] Clothing Try-On feature works
- [ ] Biome Transformation works
- [ ] Game World transformations work
- [ ] Custom Prompt with keyboard works
- [ ] Menu show/hide works (Hamburger button)
- [ ] All navigation controls work properly
- [ ] AI transformations process correctly
- [ ] No crashes during 10+ minute session

### Step 3: Distribution Options

#### Option A: Meta Quest Store (Official)
Most legitimate and discoverable method.

**Requirements:**
1. Meta Developer Account (verified)
2. Organization created on Meta Developer portal
3. App must meet Meta's quality standards

**Process:**
1. Go to https://developer.oculus.com
2. Create new app in Developer Dashboard
3. Fill in all metadata:
   - App name
   - Description
   - Screenshots (in-VR)
   - Icons
   - Privacy policy
   - Age rating
4. Upload your APK
5. Submit for review
6. Wait for Meta approval (1-4 weeks typically)

**Pros:**
- Official distribution
- Easy for users to find
- Automatic updates
- In-app purchases possible

**Cons:**
- Approval process required
- Takes time to publish
- Must meet Meta's standards

#### Option B: SideQuest / App Lab
Easier approval process than main store.

**App Lab (Recommended for Beginners):**
1. Go to https://developer.oculus.com
2. Go to App Lab section
3. Create new App Lab listing
4. Upload APK and metadata
5. Get approval (usually within days)
6. Users can install via Quest store search or direct link

**Pros:**
- Faster approval
- Less strict requirements
- Still official Meta distribution
- Good for beta testing

**Cons:**
- Less discoverable than main store
- Smaller user base

#### Option C: Direct Distribution
For personal use, beta testing, or small groups.

**Via SideQuest:**
1. Upload APK to cloud storage (Google Drive, Dropbox)
2. Share download link
3. Users install SideQuest on their PC
4. Users enable Developer Mode on Quest 3
5. Users install your APK via SideQuest

**Via ADB Directly:**
Users with Developer Mode can install via:
```bash
adb install QuestAITransform_v1.0.0.apk
```

**Pros:**
- No approval needed
- Instant distribution
- Complete control

**Cons:**
- Users need Developer Mode
- Manual installation required
- No automatic updates
- Limited to advanced users

### Step 4: Create Documentation Package

For users who download your app, provide:

**README.txt:**
```
Quest AI Transform App v1.0.0
============================

INSTALLATION:
1. Enable Developer Mode on Quest 3
2. Install SideQuest on PC (sidequestvr.com)
3. Connect Quest 3 to PC
4. Use SideQuest to install QuestAITransform_v1.0.0.apk

FIRST TIME SETUP:
1. Launch app from Unknown Sources in Library
2. Grant camera permissions when asked

CONTROLS:
- Joystick Up/Down: Navigate menu
- Right Trigger: Confirm/Apply
- Left Trigger: Go Back
- Hamburger Button: Show/Hide Menu

FEATURES:
1. Time Travel - See your room in different eras
2. Virtual Clothing Try-On - Try different outfits
3. Biome Transformation - Transform to different locations
4. Video Game World - See room as video game
5. Custom Prompt - Type your own transformations

REQUIREMENTS:
- Meta Quest 3 or Quest 3S
- WiFi connection (8+ Mbps recommended)
- Decart API is free for trial use

SUPPORT:
For issues or questions, visit: [Your Support Link]
```

**Include:**
- APK file
- README.txt
- Screenshots/video demo
- Known issues list
- Contact information

### Step 5: Marketing Assets

**Screenshots (taken in VR):**
1. Use Quest's screenshot feature (Oculus button + Trigger)
2. Capture:
   - Main menu
   - Each feature in action (Time Travel, Clothing, etc.)
   - Before/after transformations
   - Custom prompt feature

**Video Trailer:**
1. Use Quest's recording feature
2. Show:
   - App launching
   - Menu navigation
   - Each feature briefly
   - Impressive transformations
3. Edit to 30-60 seconds
4. Add text overlay explaining features

**Description Template:**
```
Transform Your Reality with AI - Quest 3

Experience the power of real-time AI transformation on your Meta Quest 3! 
See your environment through different lenses with 5 incredible features:

🕐 TIME TRAVEL - Visit your room in different eras from Victorian to futuristic
👔 VIRTUAL WARDROBE - Try on any outfit instantly
🌍 WORLD EXPLORER - Transform your space to exotic locations
🎮 GAME WORLDS - See reality as your favorite video games
✍️ CUSTOM MAGIC - Type any transformation you can imagine

Features:
✅ Real-time AI processing (powered by Decart)
✅ 100+ pre-made transformations
✅ Unlimited custom prompts
✅ Intuitive joystick navigation
✅ No PC required after installation

Perfect for:
- Creative exploration
- Virtual fashion
- Entertainment
- Content creation
- Showing friends something amazing

Download now and see reality differently!
```

### Step 6: Post-Launch

**Monitor:**
- User reviews and feedback
- Crash reports (if using Meta store)
- Feature requests
- Performance issues

**Update Plan:**
1. Collect feedback for 1-2 weeks
2. Fix critical bugs
3. Add requested features
4. Increment version number
5. Re-release updated APK

**Version History:**
- v1.0.0 - Initial release
- v1.1.0 - Bug fixes, added X feature
- v1.2.0 - Performance improvements
- etc.

---

## Additional Resources

### Official Documentation
- **Unity Documentation**: https://docs.unity3d.com
- **Meta Quest Development**: https://developer.oculus.com/documentation
- **Decart API**: https://docs.platform.decart.ai

### Community Support
- **Unity Forums**: https://forum.unity.com
- **Meta Developer Forums**: https://communityforums.atmeta.com/t5/Developer/bd-p/developer
- **Decart Discord**: https://discord.gg/decart

### Learning Resources
- **Unity Learn**: https://learn.unity.com (Free Unity courses)
- **Meta Quest Developer Tutorials**: https://developer.oculus.com/resources
- **YouTube**: Search for "Unity Quest 3 development"

### Tools
- **SideQuest**: https://sidequestvr.com (For sideloading apps)
- **Android Debug Bridge (ADB)**: https://developer.android.com/studio/command-line/adb
- **Meta XR Simulator**: Test without headset (advanced)

---

## Conclusion

Congratulations! You now know how to:
- ✅ Set up Unity for Quest 3 development
- ✅ Clone and configure the AI Transformation app
- ✅ Build and deploy to your Quest 3
- ✅ Use all features of the app
- ✅ Troubleshoot common issues
- ✅ Prepare and ship to production

This app demonstrates the cutting edge of real-time AI and VR integration. Experiment with different transformations, create amazing experiences, and most importantly - have fun!

**Questions or Issues?**
- Check the Troubleshooting section
- Visit the project GitHub: https://github.com/Jakubikk/joystick-nav
- Join the Decart Discord for AI-related questions

**Happy Creating! 🚀**

---

*Last Updated: 2025-10-26*
*App Version: 1.0.0*
*Unity Version: 6000.0.34f1*

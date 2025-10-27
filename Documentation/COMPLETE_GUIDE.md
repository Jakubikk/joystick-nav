# Complete Guide: From Clone to Production
## Meta Quest 3 AI Transformation App - Step-by-Step Guide for Absolute Beginners

This guide will walk you through every single step needed to build and deploy this app to your Meta Quest 3, even if you've never used Unity before.

---

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Installing Required Software](#installing-required-software)
3. [Setting Up Your Development Environment](#setting-up-your-development-environment)
4. [Cloning the Repository](#cloning-the-repository)
5. [Opening the Project in Unity](#opening-the-project-in-unity)
6. [Configuring Unity Project Settings](#configuring-unity-project-settings)
7. [Setting Up Decart AI API](#setting-up-decart-ai-api)
8. [Building the Application](#building-the-application)
9. [Deploying to Meta Quest 3](#deploying-to-meta-quest-3)
10. [Testing the Application](#testing-the-application)
11. [Troubleshooting](#troubleshooting)

---

## Prerequisites

### Hardware Requirements
- **Computer**: Windows 10/11 or macOS (with at least 16GB RAM recommended)
- **Meta Quest 3**: Fully charged and updated to the latest firmware
- **USB-C Cable**: To connect Quest 3 to your computer
- **Internet Connection**: Minimum 8 Mbps for AI processing

### Account Requirements
- **Decart AI Account**: Free account at [platform.decart.ai](https://platform.decart.ai)
- **Meta Developer Account**: Free account at [developer.oculus.com](https://developer.oculus.com)

---

## Installing Required Software

### Step 1: Install Unity Hub

1. **Download Unity Hub**
   - Go to [unity.com/download](https://unity.com/download)
   - Click the big green "Download Unity Hub" button
   - Save the installer to your Downloads folder

2. **Install Unity Hub**
   - **Windows**: Double-click the downloaded .exe file
   - **Mac**: Double-click the downloaded .dmg file and drag Unity Hub to Applications
   - Follow the installation wizard (just keep clicking "Next" and accept defaults)
   - Click "Finish" when done

3. **Create Unity Account**
   - Open Unity Hub (it should open automatically, or find it in your Start Menu/Applications)
   - Click "Create account" in the upper right
   - Fill in your email and password
   - Verify your email address
   - Choose the "Personal" license (it's free)
   - Complete the survey (you can skip questions if you want)

### Step 2: Install Unity Editor Version 6

1. **In Unity Hub, click "Installs" on the left side**

2. **Click "Install Editor" button (top right)**

3. **Select Unity 6 (6000.0.34f1)**
   - Scroll down to find version 6000.0.34f1
   - If you can't find this exact version, any Unity 6 version will work
   - Click "Install" next to it

4. **Select Android Build Support**
   - A window will pop up showing modules to add
   - ✅ **Check "Android Build Support"**
     - This will expand showing more options
     - ✅ **Check "Android SDK & NDK Tools"**
     - ✅ **Check "OpenJDK"**
   - ❌ Leave everything else unchecked
   - Click "Continue"

5. **Wait for Installation**
   - This will take 15-45 minutes depending on your internet speed
   - The total download is about 5-8 GB
   - You can minimize Unity Hub and do other things while it downloads
   - You'll see a green checkmark when it's done

### Step 3: Install Git

1. **Download Git**
   - **Windows**: Go to [git-scm.com/download/win](https://git-scm.com/download/win)
   - **Mac**: Go to [git-scm.com/download/mac](https://git-scm.com/download/mac)
   - The download should start automatically

2. **Install Git**
   - **Windows**: 
     - Run the downloaded .exe file
     - Just keep clicking "Next" through all the options (defaults are fine)
     - Click "Install" at the end
     - Click "Finish"
   - **Mac**:
     - Run the downloaded .pkg file
     - Click "Continue" through the prompts
     - Click "Install"
     - Enter your Mac password if asked

3. **Verify Git Installation**
   - **Windows**: Open "Command Prompt" (search for it in Start Menu)
   - **Mac**: Open "Terminal" (search for it in Spotlight)
   - Type: `git --version`
   - Press Enter
   - You should see something like "git version 2.x.x"

### Step 4: Install SideQuest (for deploying to Quest)

1. **Download SideQuest**
   - Go to [sidequestvr.com/setup-howto](https://sidequestvr.com/setup-howto)
   - Click "Download for Windows" or "Download for Mac"

2. **Install SideQuest**
   - **Windows**: Run the downloaded .exe file and follow the installer
   - **Mac**: Open the .dmg file and drag SideQuest to Applications

---

## Setting Up Your Development Environment

### Step 1: Enable Developer Mode on Quest 3

1. **Install Meta Quest Mobile App**
   - On your phone, download "Meta Quest" app from App Store or Google Play
   - Open the app and log in with your Meta/Facebook account
   - Make sure your Quest 3 is connected to the same Wi-Fi

2. **Enable Developer Mode**
   - In the Meta Quest app, tap **Menu** (bottom right)
   - Tap **Devices**
   - Select your **Quest 3** from the list
   - Tap **Headset Settings**
   - Scroll down to **Developer Mode**
   - Toggle it **ON**
   - You may need to create a developer organization:
     - If prompted, click "Create Organization"
     - Enter a name (can be anything, like "MyDevOrg")
     - Click "Submit"

3. **Verify Developer Mode**
   - Put on your Quest 3 headset
   - Look for "Developer" in your settings
   - You should see USB debugging options

### Step 2: Set Up USB Debugging

1. **Connect Quest 3 to Computer**
   - Use a USB-C cable to connect your Quest 3 to your computer
   - Put on your headset

2. **Allow USB Debugging**
   - You'll see a popup in VR asking "Allow USB debugging?"
   - ✅ Check "Always allow from this computer"
   - Click "OK"

3. **Verify Connection in SideQuest**
   - Open SideQuest on your computer
   - In the top left corner, you should see a green dot next to your Quest 3
   - If you see a red dot, unplug and replug the USB cable

---

## Cloning the Repository

### Step 1: Choose a Location

1. **Create a Projects Folder**
   - **Windows**: Open File Explorer, go to Documents
   - **Mac**: Open Finder, go to Documents
   - Create a new folder called "Unity Projects"
   - Open this folder

### Step 2: Clone Using Git

1. **Open Command Line**
   - **Windows**: Right-click in the "Unity Projects" folder, select "Git Bash Here"
     - If you don't see this option, open Command Prompt and type:
       ```
       cd Documents\Unity Projects
       ```
   - **Mac**: Right-click the folder, select "New Terminal at Folder"
     - Or open Terminal and type:
       ```
       cd ~/Documents/Unity\ Projects
       ```

2. **Clone the Repository**
   - Type or paste this command:
     ```bash
     git clone https://github.com/Jakubikk/joystick-nav.git
     ```
   - Press Enter
   - Wait for the download to complete (should take 1-3 minutes)
   - You should see "Cloning into 'joystick-nav'..."

3. **Verify Clone**
   - You should now see a folder called "joystick-nav" in your Unity Projects folder
   - Open it
   - Inside you'll see "DecartAI-Quest-Unity" folder and other files

---

## Opening the Project in Unity

### Step 1: Add Project to Unity Hub

1. **Open Unity Hub**

2. **Click "Open" button** (top right, next to "New project")

3. **Navigate to Project Folder**
   - Browse to: `Documents/Unity Projects/joystick-nav/DecartAI-Quest-Unity`
   - Click "Open" or "Select Folder"

4. **Wait for Import**
   - Unity Hub will show "Opening..."
   - Unity Editor will launch
   - **First time opening takes 5-15 minutes** as Unity imports all assets
   - You'll see a progress bar at the bottom
   - Don't panic if it seems stuck - this is normal!

### Step 2: Verify Project Loaded

1. **Check Project Window**
   - At the bottom of Unity, you should see a "Project" tab
   - You should see folders like "Assets", "Packages", etc.

2. **Open Main Scene**
   - In Project window, navigate to:
     ```
     Assets → Samples → DecartAI-Quest → DecartAI-Main.unity
     ```
   - Double-click "DecartAI-Main.unity"
   - You should see the scene layout in the center of Unity

---

## Configuring Unity Project Settings

### Step 1: Switch to Android Platform

1. **Open Build Settings**
   - Click **File** in the top menu
   - Click **Build Settings**
   - A window will open

2. **Select Android**
   - In the "Platform" list on the left, click **Android**
   - Click **Switch Platform** button at the bottom
   - This will take 2-5 minutes
   - You'll see a progress bar

### Step 2: Configure Android Settings

1. **Click "Player Settings"** (bottom left of Build Settings window)

2. **Company and Product Name**
   - In the Inspector on the right, you'll see "Company Name"
   - Change it to your name or company name
   - "Product Name" should be "DecartAI Quest"
   - You can change this if you want

3. **Navigate to Android Tab**
   - At the top of Inspector, click the **Android (robot icon)** tab

4. **Set Package Name**
   - Expand "Other Settings" section
   - Find "Package Name" 
   - Change it to something like: `com.yourname.questai`
   - Use only lowercase letters, numbers, and dots
   - Must start with "com."

5. **Set Minimum API Level**
   - In "Other Settings", scroll to "Minimum API Level"
   - Click the dropdown
   - Select **Android 10.0 (API Level 29)** or higher

6. **Set Target API Level**
   - Find "Target API Level"
   - Select **API Level 34** or the highest available

7. **Configure Graphics**
   - Scroll to "Graphics APIs"
   - You should see "Vulkan" and/or "OpenGLES3"
   - This is correct - don't change it

8. **Set Scripting Backend**
   - Scroll to "Scripting Backend"
   - Select **IL2CPP**

9. **Set Target Architectures**
   - Expand "Target Architectures"
   - ✅ Check **ARM64**
   - ❌ Uncheck **ARMv7** if it's checked

### Step 3: Configure XR Settings

1. **Open XR Plugin Management**
   - In Project Settings (same window), scroll down on the left
   - Click **XR Plug-in Management**
   - If you don't see it, click "Install XR Plugin Management" first

2. **Select Android Tab** (small Android icon at the top)

3. **Enable Oculus**
   - ✅ Check **Oculus**
   - Leave everything else unchecked

4. **Configure Oculus Settings**
   - Click the **Oculus** item (appears under XR Plug-in Management)
   - ✅ **Check "Initialize XR on Startup"**
   - ❌ **Uncheck "Low Overhead Mode (GLES)"** - IMPORTANT!
   - ❌ **Uncheck "Meta Quest: Occlusion"** - IMPORTANT!

### Step 4: Fix Meta XR Issues

1. **Open Meta XR Tools**
   - In Unity menu, click **Edit** → **Project Settings** → **Meta XR**
   - You'll see "Project Setup Tool"

2. **Fix Required Issues**
   - Click **"Fix All"** button
   - Wait for fixes to apply
   - **IMPORTANT**: If you see these two issues, DON'T fix them:
     - "Hand Tracking must be enabled..." - Leave this UNFIXED
     - "When Using Passthrough..." - Leave this UNFIXED
   - Fix everything else

3. **Apply Recommended Fixes**
   - Click "Recommended Items" tab
   - Click **"Apply All"**
   - **IMPORTANT**: Skip these two:
     - "Use Low Overhead Mode" - DON'T apply this
     - "Hand tracking support..." - DON'T apply this
   - Apply everything else

4. **Close Project Settings**
   - Click the X to close Project Settings window

---

## Setting Up Decart AI API

### Step 1: Get API Key

1. **Create Decart Account**
   - Go to [platform.decart.ai](https://platform.decart.ai)
   - Click "Sign Up"
   - Enter your email and create a password
   - Verify your email

2. **Generate API Key**
   - Log in to Decart platform
   - Click on **"API Keys"** in the left menu
   - Click **"Create New API Key"**
   - Give it a name like "Quest App"
   - Click **"Create"**
   - **IMPORTANT**: Copy the API key immediately - you can't see it again!
   - Save it in a text file somewhere safe

### Step 2: Configure API in Unity (Optional)

The app uses trial endpoints by default, which work without an API key. If you want to use your own API key:

1. **Find WebRTCConnection Script**
   - In Unity Project window, search for "WebRTCConnection"
   - Double-click to open in your code editor

2. **Add Your API Key**
   - Look for lines with "api3.decart.ai"
   - You can add your API key to the URL if needed
   - For now, the default trial endpoints work fine

---

## Building the Application

### Step 1: Prepare for Build

1. **Save the Scene**
   - Press **Ctrl+S** (Windows) or **Cmd+S** (Mac)
   - Or click **File** → **Save**

2. **Open Build Settings**
   - Click **File** → **Build Settings**

3. **Add Scene to Build**
   - Make sure "DecartAI-Main" is in the "Scenes In Build" list
   - If not, click **"Add Open Scenes"**
   - It should show: `DecartAI-Main` with a checkmark

### Step 2: Build the APK

1. **Click "Build"** (NOT "Build And Run")
   - A file dialog will open

2. **Choose Save Location**
   - Navigate to your Desktop or Documents
   - Create a new folder called "Quest Builds"
   - Name the file: `QuestAI.apk`
   - Click **"Save"**

3. **Wait for Build**
   - This takes 10-30 minutes for the first build
   - You'll see a progress bar
   - Unity might appear frozen - this is normal!
   - The progress bar shows stages like "Compiling scripts", "Building player", etc.
   - **Don't close Unity or your computer during this process**

4. **Build Complete**
   - You'll see "Build Successful" in the console
   - The APK file will be in your chosen location

---

## Deploying to Meta Quest 3

### Method 1: Using SideQuest (Easier)

1. **Connect Quest 3**
   - Plug in your Quest 3 with USB cable
   - Make sure SideQuest shows green dot (connected)

2. **Install APK**
   - Open SideQuest
   - Click the **"Install APK file from folder"** icon (looks like a box with down arrow)
   - Navigate to your QuestAI.apk file
   - Click **"Open"**
   - Wait for "Successfully Installed App"

3. **Launch App**
   - Put on your Quest 3 headset
   - Go to **Apps** → **All**
   - Look for "Unknown Sources" at the top right
   - Click it
   - Find "DecartAI Quest" (or your product name)
   - Click to launch!

### Method 2: Using ADB (Alternative)

1. **Open Command Prompt/Terminal**

2. **Navigate to Android SDK**
   ```bash
   # Windows (Unity's default Android SDK location)
   cd C:\Program Files\Unity\Hub\Editor\2021.3.x\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools
   
   # Mac
   cd ~/Library/Android/sdk/platform-tools
   ```

3. **Install APK**
   ```bash
   adb install "C:\path\to\your\QuestAI.apk"
   ```
   Replace path with your actual APK location

4. **Verify Installation**
   ```bash
   adb shell pm list packages | grep yourpackagename
   ```

---

## Testing the Application

### Step 1: First Launch

1. **Put on Quest 3 Headset**

2. **Grant Permissions**
   - When you first launch, you'll see:
     - "Allow camera access?" → Click **Allow**
     - "Allow microphone access?" → Click **Allow** (if prompted)

3. **Wait for Camera Initialization**
   - The app will take 5-10 seconds to start the camera
   - You should see your passthrough camera feed

### Step 2: Connect to AI Service

1. **Wait for Connection**
   - The app automatically connects to Decart AI servers
   - You'll see "Connecting..." 
   - When connected, you'll see "Connected"
   - This takes 5-15 seconds on first launch

2. **Test Transformation**
   - The AI processing starts automatically
   - After 3-5 seconds, you should see the transformed video
   - Default style depends on the model selected

### Step 3: Using the Features

**Menu Navigation:**
- Press **Start Button** (hamburger icon) to show/hide menu
- Use **Joystick Up/Down** to navigate menu options
- Press **Right Trigger** to select an option
- Press **Left Trigger** to go back to main menu

**Time Travel Feature:**
1. Select "Time Travel" from main menu
2. Move joystick left/right to change year
3. Or use the slider with your controller
4. Press right trigger to apply the time period

**Virtual Try-On Feature:**
1. Stand in front of a mirror
2. Select "Virtual Try-On" from menu
3. Browse clothing options with joystick
4. Press right trigger to try on selected outfit

**Biome Transformation:**
1. Select "Biome Transformation"
2. Browse different environments with joystick
3. Press right trigger to transform your room

**Video Game Style:**
1. Select "Video Game Style"
2. Browse game styles with joystick
3. Press right trigger to apply the game aesthetic

**Custom Prompt:**
1. Select "Custom Prompt"
2. Press **Y button** to open Meta keyboard
3. Type your custom transformation idea
4. Press right trigger to apply

### Step 4: Verify Everything Works

**Checklist:**
- ✅ Camera feed visible
- ✅ AI transformation working
- ✅ Menu appears when pressing Start button
- ✅ Joystick navigation works
- ✅ Right trigger selects options
- ✅ Left trigger goes back
- ✅ All 5 features accessible
- ✅ Custom prompts can be typed

---

## Troubleshooting

### Camera Not Working

**Problem**: Black screen or "Camera failed to start"

**Solutions**:
1. **Check Permissions**
   - Go to Quest Settings → Apps → DecartAI Quest → Permissions
   - Make sure Camera permission is enabled

2. **Restart App**
   - Close the app completely
   - Relaunch from Unknown Sources

3. **Check Quest OS Version**
   - Settings → System → Software Update
   - Update to latest version (need v74 or higher)

### AI Connection Fails

**Problem**: "Connection failed" or no AI processing

**Solutions**:
1. **Check Internet Connection**
   - Make sure Quest is connected to WiFi
   - Need at least 8 Mbps upload/download
   - Try connecting to 5GHz WiFi if available

2. **Restart Quest**
   - Hold power button
   - Select "Restart"
   - Relaunch app

3. **Check Decart Service Status**
   - Visit [platform.decart.ai](https://platform.decart.ai)
   - Check if services are online

### App Won't Install

**Problem**: "Installation failed" in SideQuest

**Solutions**:
1. **Uninstall Old Version**
   - In Quest: Settings → Apps → find old version → Uninstall
   - Try installing again

2. **Check USB Connection**
   - Try different USB cable
   - Try different USB port on computer
   - Make sure cable supports data transfer (not just charging)

3. **Verify Developer Mode**
   - Check Meta Quest app on phone
   - Make sure Developer Mode is still ON

### Build Errors in Unity

**Problem**: Build fails with errors

**Solutions**:
1. **Check Unity Version**
   - Make sure you're using Unity 6 (6000.0.34f1 or similar)

2. **Reimport Assets**
   - Assets → Reimport All
   - Wait for reimport to complete

3. **Clear Library**
   - Close Unity
   - Delete "Library" folder in project directory
   - Reopen project (will regenerate Library)

4. **Check Android SDK**
   - Edit → Preferences → External Tools
   - Make sure Android SDK path is set correctly

### Performance Issues

**Problem**: Laggy or stuttering video

**Solutions**:
1. **Check WiFi Speed**
   - Run speed test on Quest browser
   - Need minimum 8 Mbps
   - Use 5GHz WiFi for better performance

2. **Close Other Apps**
   - Make sure no other apps running in background
   - Restart Quest to clear memory

3. **Lower Graphics Quality** (if you modified settings)
   - The default settings are already optimized

### Menu Not Appearing

**Problem**: Can't see the menu or it won't open

**Solutions**:
1. **Press Start Button**
   - The hamburger button (≡) on right controller
   - Should toggle menu visibility

2. **Check Canvas Settings**
   - Menu might be positioned incorrectly
   - Restart app

### Keyboard Not Opening

**Problem**: Custom prompt keyboard won't appear

**Solutions**:
1. **Press Y Button**
   - Yellow Y button on left controller
   - Or tap the input field with controller ray

2. **Update Quest OS**
   - Keyboard feature requires latest OS
   - Settings → System → Software Update

---

## Additional Resources

### Official Documentation
- **Decart AI Docs**: [docs.platform.decart.ai](https://docs.platform.decart.ai)
- **Unity Learn**: [learn.unity.com](https://learn.unity.com)
- **Meta Quest Development**: [developer.oculus.com](https://developer.oculus.com)

### Community Support
- **Decart Discord**: [discord.gg/decart](https://discord.gg/decart)
- **Unity Forum**: [forum.unity.com](https://forum.unity.com)

### Video Tutorials
- Search YouTube for "Meta Quest Unity development tutorial"
- Search for "Unity Android build tutorial"

---

## Next Steps

Now that you have the app working:

1. **Experiment with Features**: Try all the different transformation options
2. **Create Custom Prompts**: Get creative with the custom prompt feature
3. **Modify and Learn**: Open the Unity scripts and learn how they work
4. **Share Your Experience**: Share screenshots and videos with the community
5. **Report Issues**: If you find bugs, report them on GitHub

---

## Conclusion

Congratulations! You've successfully:
- ✅ Set up Unity for Quest development
- ✅ Cloned and configured the project
- ✅ Built an APK file
- ✅ Deployed to your Quest 3
- ✅ Tested all features

You now have a fully working AI-powered VR transformation app on your Meta Quest 3!

Enjoy exploring different time periods, trying on virtual clothing, transforming your environment into different biomes, experiencing video game aesthetics, and creating your own custom transformations!

---

*Last updated: 2025*
*For questions or issues, please visit the GitHub repository*

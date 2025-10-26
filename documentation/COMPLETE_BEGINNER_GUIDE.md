# Complete Beginner's Guide: DecartXR Quest 3 App - From Clone to Production

This guide will walk you through every single step needed to build and deploy the DecartXR app to your Meta Quest 3, even if you've never used Unity before.

---

## Table of Contents

1. [Prerequisites and Setup](#1-prerequisites-and-setup)
2. [Installing Unity](#2-installing-unity)
3. [Cloning the Repository](#3-cloning-the-repository)
4. [Opening the Project in Unity](#4-opening-the-project-in-unity)
5. [Setting Up Decart API Key](#5-setting-up-decart-api-key)
6. [Setting Up Voice Control (Optional)](#6-setting-up-voice-control-optional)
7. [Configuring Unity Project Settings](#7-configuring-unity-project-settings)
8. [Understanding the New Features](#8-understanding-the-new-features)
9. [Building for Quest 3](#9-building-for-quest-3)
10. [Deploying to Your Quest 3](#10-deploying-to-your-quest-3)
11. [Using the App](#11-using-the-app)
12. [Troubleshooting](#12-troubleshooting)

---

## 1. Prerequisites and Setup

### What You'll Need:

1. **A Computer** (Windows, Mac, or Linux)
   - Minimum 8GB RAM (16GB recommended)
   - At least 20GB free disk space
   - A decent graphics card (for Unity Editor)

2. **Meta Quest 3 or Quest 3S Headset**
   - Running Horizon OS v74 or later
   - Charged battery (at least 50%)

3. **A USB-C Cable**
   - To connect your Quest to your computer
   - The cable that came with your Quest works fine

4. **Internet Connection**
   - At least 8 Mbps upload/download speed
   - For downloading Unity, packages, and running Decart AI

### Creating Necessary Accounts:

#### A. Meta Developer Account
1. Go to https://developer.oculus.com/
2. Click "Sign Up" in the top right
3. Use your Facebook/Meta account or create a new one
4. Fill in your developer information
5. Accept the terms and conditions

#### B. Decart API Account
1. Go to https://platform.decart.ai/
2. Click "Sign Up"
3. Create an account with your email
4. Verify your email address
5. Once logged in, go to "API Keys" section
6. Click "Create New API Key"
7. Copy and save your API key somewhere safe (you'll need it later)

#### C. Wit.ai Account (Optional - for voice control)
1. Go to https://wit.ai/
2. Click "Sign Up" or "Continue with Facebook"
3. Create or link your account
4. We'll configure this later in step 6

---

## 2. Installing Unity

### Step-by-Step Unity Installation:

1. **Download Unity Hub**
   - Go to https://unity.com/download
   - Click "Download Unity Hub"
   - Run the installer and follow the prompts
   - Windows: Double-click the .exe file
   - Mac: Double-click the .dmg file and drag Unity Hub to Applications

2. **Install Unity Hub**
   - Accept the license agreement
   - Choose installation location (default is fine)
   - Click "Install"
   - Wait for installation to complete

3. **Open Unity Hub**
   - Launch Unity Hub from your desktop or applications folder
   - Sign in or create a Unity account (free Personal license is fine)

4. **Install Unity Editor Version 6 (6000.0.34f1 or later)**
   - In Unity Hub, click "Installs" on the left sidebar
   - Click the "Install Editor" button (blue button in top right)
   - Click "Archive" link at the top
   - Find Unity 6 (specifically version 6000.0.34f1 or newer)
   - Click "Install this version"

5. **Add Android Build Support**
   - During the Unity installation, you'll see "Add modules"
   - **CRITICAL**: Check these boxes:
     - ✅ Android Build Support
     - ✅ Android SDK & NDK Tools
     - ✅ OpenJDK
   - If you already installed Unity, you can add these:
     - Go to Installs tab
     - Click the gear icon next to your Unity version
     - Click "Add Modules"
     - Check the Android options above
     - Click "Install"

6. **Wait for Installation**
   - This can take 30-60 minutes depending on your internet speed
   - Unity will download about 5-10GB of data
   - You can minimize Unity Hub and do other things while waiting

---

## 3. Cloning the Repository

### Option A: Using Git (Recommended)

1. **Install Git**
   - Windows: Download from https://git-scm.com/download/win
   - Mac: Open Terminal and type `git --version` (it will prompt installation if needed)
   - Linux: Use your package manager (`sudo apt-get install git`)

2. **Open Command Line/Terminal**
   - Windows: Press `Win + R`, type `cmd`, press Enter
   - Mac: Press `Cmd + Space`, type `Terminal`, press Enter
   - Linux: Open your terminal application

3. **Navigate to Where You Want the Project**
   ```bash
   cd Documents
   ```

4. **Clone the Repository**
   ```bash
   git clone https://github.com/Jakubikk/joystick-nav.git
   cd joystick-nav
   ```

### Option B: Download ZIP (Easier but not recommended for updates)

1. Go to https://github.com/Jakubikk/joystick-nav
2. Click the green "Code" button
3. Click "Download ZIP"
4. Extract the ZIP file to a location you'll remember (like Documents)

---

## 4. Opening the Project in Unity

1. **Open Unity Hub**
   - Launch Unity Hub if it's not already open

2. **Add the Project**
   - Click "Projects" in the left sidebar (not "Installs")
   - Click "Add" button (or "Open" on some versions)
   - Navigate to where you cloned/extracted the repository
   - Go into the folder: `joystick-nav/DecartAI-Quest-Unity`
   - Click "Select Folder" or "Open"

3. **Wait for Unity to Open**
   - Unity Hub will show the project with the correct Unity version
   - Click on the project name to open it
   - **FIRST TIME OPENING WILL TAKE 10-20 MINUTES**
   - Unity needs to import all assets and compile code
   - You'll see a progress bar - this is normal!
   - **DO NOT CLOSE UNITY DURING THIS IMPORT**

4. **When Unity Opens**
   - You'll see the Unity Editor interface
   - If you see a "Updating..." or "Importing..." dialog, wait for it to finish
   - Once complete, you should see the Unity Editor with panels:
     - Scene view (center)
     - Hierarchy (left)
     - Inspector (right)
     - Project (bottom)

---

## 5. Setting Up Decart API Key

The app needs your Decart API key to connect to the AI service.

### Step-by-Step:

1. **Locate Your API Key**
   - Go back to https://platform.decart.ai/
   - Navigate to "API Keys" section
   - Copy your API key (should look like a long string of random characters)

2. **Find the WebRTC Configuration in Unity**
   - In Unity, look at the bottom "Project" panel
   - Navigate to: `Assets` → `Samples` → `DecartAI-Quest` 
   - Double-click on `DecartAI-Main.unity` to open the scene

3. **Update the API Key**
   - In the "Hierarchy" panel (left side), look for object named "WebRTC Connection" or similar
   - Click on it to select it
   - In the "Inspector" panel (right side), you'll see various settings
   - Look for a field that mentions API key or WebSocket URL
   - The URL should look like: `wss://api3.decart.ai/v1/stream-trial?model=mirage`
   
4. **Save the Scene**
   - Press `Ctrl+S` (Windows) or `Cmd+S` (Mac)
   - Or go to File → Save

**Note**: If using the trial endpoints, no API key is needed initially. For production use with your own API key, you would modify the WebSocket URL to include `&api_key=YOUR_API_KEY`.

---

## 6. Setting Up Voice Control (Optional)

Voice control allows you to speak custom prompts. This step is optional but recommended.

### Creating Your Wit.ai App:

1. **Go to Wit.ai Dashboard**
   - Visit https://wit.ai/apps
   - Click "New App"

2. **Create New App**
   - Name: "DecartXR Voice" (or any name you like)
   - Language: English
   - Click "Create"

3. **Get Your Tokens**
   - Once created, click on "Settings" in the left sidebar
   - You'll see:
     - Server Access Token (long string)
     - Client Access Token (another long string)
   - Copy BOTH tokens - you'll need them in a moment

4. **Configure in Unity**
   - In Unity's Project panel, navigate to: `Assets`
   - Find a file called `CustomNLP.asset`
   - Click on it to select it
   - In the Inspector panel (right), you'll see:
     - Server Access Token field
     - Client Access Token field
   - Paste your tokens into these fields
   - Press `Ctrl+S` (Windows) or `Cmd+S` (Mac) to save

**That's it!** Wit.ai requires no additional configuration - it will automatically convert your speech to text.

---

## 7. Configuring Unity Project Settings

Now we need to configure Unity to build for Quest 3.

### A. Switch to Android Platform

1. **Open Build Settings**
   - Go to: `File` → `Build Settings`
   - A window will pop up

2. **Select Android**
   - In the "Platform" list, click on "Android"
   - Click the "Switch Platform" button at the bottom right
   - Wait 2-5 minutes while Unity re-imports assets for Android

### B. Configure Android Settings

1. **Open Player Settings**
   - In the Build Settings window, click "Player Settings" button at bottom left
   - This opens the Player Settings in the Inspector

2. **Company and Product Name**
   - At the very top, you'll see:
     - Company Name: Enter your name or company
     - Product Name: "DecartXR" (or your preferred name)

3. **Configure Android Settings (CRITICAL)**
   - In the Inspector, you'll see tabs with icons
   - Click the Android tab (looks like Android robot icon)
   
4. **Other Settings Section**
   - Scroll down to "Other Settings"
   - Find "Rendering" section:
     - **Graphics APIs**: Should show "Vulkan" or "OpenGLES3"
       - If you see Vulkan, that's fine
       - If not, click the + button and add OpenGLES3
     - **Color Space**: Set to "Linear"
   
   - Find "Identification" section:
     - **Package Name**: Change to something like `com.yourname.decartxr`
       - Must be format: `com.companyname.appname`
       - All lowercase, no spaces
     - **Version**: 1.0
     - **Bundle Version Code**: 1
   
   - Find "Configuration" section:
     - **Scripting Backend**: Set to "IL2CPP"
     - **API Compatibility Level**: Set to ".NET Standard 2.1"
     - **Target Architectures**: 
       - ✅ ARM64 (MUST be checked)
       - ❌ ARMv7 (MUST be unchecked)
   
   - Find "Minimum API Level" and "Target API Level":
     - **Minimum API Level**: Set to at least "Android 10.0 (API Level 29)"
     - **Target API Level**: Set to "Android 14.0 (API Level 34)" or higher

5. **XR Settings**
   - Scroll to top of Player Settings
   - Find "XR Plug-in Management" in left sidebar
   - Click on it
   - Under "Android" tab (robot icon):
     - ✅ Check "Oculus"
   - Click on "Oculus" that appears below
   - Settings should show:
     - ✅ Initialize XR on Startup
     - ❌ Low Overhead Mode (MUST be DISABLED)

### C. Configure Meta XR Settings

1. **Open Meta XR Project Setup Tool**
   - Go to: `Edit` → `Project Settings`
   - In left sidebar, scroll down to find "Meta XR"
   - Click on it

2. **Fix Issues**
   - You'll see two sections: "Outstanding Issues" and "Recommended Items"
   - **Outstanding Issues Section**:
     - Click "Fix All" EXCEPT for:
       - "Hand Tracking must be enabled..." - SKIP this one
       - "When Using Passthrough Building Block..." - SKIP this one
   - **Recommended Items Section**:
     - Click "Apply All" EXCEPT for:
       - "Use Low Overhead Mode" - SKIP this one
       - "Hand tracking support..." - SKIP this one

3. **Close Project Settings**
   - Close the Project Settings window
   - Unity will save automatically

---

## 8. Understanding the New Features

The app now has 5 main features:

### 1. **Time Travel**
- View your environment in different time periods
- Use a slider to select a year from 1000 to 3000
- See your room as it might have looked in medieval times, or the future!
- Navigate the slider with joystick left/right

### 2. **Virtual Dressing Room**
- Try on different clothing styles
- 20+ outfit options from formal wear to superhero costumes
- See yourself in different clothes in real-time
- Navigate with joystick up/down, confirm with right trigger

### 3. **Biome/Country Transformation**
- Transform your environment to different locations
- Visit tropical rainforests, arctic tundra, Sahara desert
- Teleport to Paris, Tokyo, Venice, and more
- 20+ different locations and biomes
- Navigate with joystick up/down, confirm with right trigger

### 4. **Video Game Styles**
- View your world as if it were in a video game
- 25+ game styles: Minecraft, Zelda, GTA, Cyberpunk, and more
- Navigate with joystick up/down, confirm with right trigger

### 5. **Custom Input**
- Type your own creative prompts
- Uses Meta Quest's built-in virtual keyboard
- Press right trigger to open keyboard
- Type anything you can imagine!

### Navigation Controls:
- **Left Joystick Up/Down**: Navigate menu options
- **Right Trigger**: Confirm selection
- **Left Trigger**: Go back to previous menu
- **Start Button (Hamburger Menu)**: Show/hide menu

---

## 9. Building for Quest 3

Now we'll create the APK file to install on your Quest.

### Step-by-Step Build Process:

1. **Save Everything**
   - Go to `File` → `Save` (or press Ctrl+S)
   - Go to `File` → `Save Project`

2. **Open Build Settings**
   - Go to `File` → `Build Settings`

3. **Add the Main Scene**
   - In the "Scenes in Build" section at the top:
   - If you see "DecartAI-Main" scene, great!
   - If not, click "Add Open Scenes" button
   - The scene should show with index 0

4. **Verify Platform**
   - Make sure "Android" is selected and shows a Unity icon next to it
   - If not, select Android and click "Switch Platform"

5. **Build the APK**
   - Click the "Build" button (NOT "Build and Run" yet)
   - A file browser will open
   - Navigate to a location you'll remember (like Desktop)
   - Create a new folder called "DecartXR_Build"
   - Name your file: "DecartXR.apk"
   - Click "Save"

6. **Wait for Build to Complete**
   - Unity will now compile everything
   - **This takes 10-30 minutes the first time**
   - You'll see a progress bar at the bottom of Unity
   - Don't close Unity during this process!
   - When complete, you'll see "Build Successful" message

7. **Locate Your APK**
   - Navigate to the folder you chose
   - You should see a file called "DecartXR.apk"
   - This is your app!

---

## 10. Deploying to Your Quest 3

Now let's get the app onto your headset.

### A. Enable Developer Mode on Quest

1. **Install Meta Quest App on Phone**
   - Download "Meta Quest" app from App Store (iOS) or Play Store (Android)
   - Open the app and sign in with your Meta account

2. **Pair Your Quest**
   - Put on your Quest headset
   - Follow the pairing instructions in the app
   - Your Quest should appear in the app

3. **Enable Developer Mode**
   - In the Meta Quest phone app:
   - Tap "Menu" (three lines) in bottom right
   - Tap "Devices"
   - Select your Quest 3
   - Tap "Developer Mode"
   - Toggle it ON
   - If it asks you to create a developer organization:
     - Tap "Create Organization"
     - Enter any name
     - Verify with your phone number if asked

4. **Restart Your Quest**
   - Take off the headset
   - Hold the power button for 3 seconds
   - Select "Restart"
   - Wait for it to boot back up

### B. Install SideQuest (Easiest Method)

1. **Download SideQuest**
   - Go to https://sidequestvr.com/setup-howto
   - Download SideQuest for your operating system:
     - Windows: Download the .exe installer
     - Mac: Download the .dmg file
     - Linux: Download the AppImage

2. **Install SideQuest**
   - Run the installer
   - Follow the installation prompts
   - Launch SideQuest when installation completes

3. **Connect Your Quest**
   - Connect your Quest 3 to your computer with USB-C cable
   - Put on your Quest headset
   - You should see a prompt: "Allow USB Debugging?"
   - Check "Always allow from this computer"
   - Click "OK"

4. **Verify Connection in SideQuest**
   - In SideQuest, you should see a green dot in the top left
   - This means your Quest is connected
   - If it's red, try:
     - Unplugging and replugging the USB cable
     - Restarting SideQuest
     - Making sure you clicked "Allow" on the Quest

5. **Install Your APK**
   - In SideQuest, click the "Install APK file from folder" icon
     - It looks like a box with a down arrow
     - It's in the top toolbar
   - Navigate to where you saved "DecartXR.apk"
   - Select the file
   - Click "Open"
   - Wait for installation (5-30 seconds)
   - You'll see "Installation successful" message

### C. Alternative: Using ADB (Advanced)

If you prefer command line:

1. **Install ADB**
   - Windows: Download Platform Tools from https://developer.android.com/studio/releases/platform-tools
   - Mac: Install with Homebrew: `brew install android-platform-tools`
   - Linux: `sudo apt-get install android-tools-adb`

2. **Connect Quest and Verify**
   ```bash
   adb devices
   ```
   - You should see your Quest listed
   - If it shows "unauthorized", check your headset for the USB debugging prompt

3. **Install APK**
   ```bash
   adb install -r path/to/DecartXR.apk
   ```
   - Replace `path/to/DecartXR.apk` with actual path
   - Wait for "Success" message

---

## 11. Using the App

### First Launch:

1. **Put on Your Quest**
   - Make sure it's charged
   - Put it on and adjust for comfort

2. **Find the App**
   - Press the Oculus button (button with "O" on right controller)
   - Go to "App Library"
   - Click "All" at the top right
   - Scroll down to find "DecartXR"
   - Click on it to launch

3. **Grant Permissions**
   - First time you run it, you'll see permission requests
   - "Allow camera access?" → Click "Allow"
   - "Allow microphone access?" → Click "Allow" (if using voice)

4. **Main Menu Appears**
   - You'll see a menu with 5 options:
     - Time Travel
     - Virtual Dressing Room
     - Biome/Country
     - Video Game Styles
     - Custom Input

### Using Each Feature:

#### Time Travel:
1. Select "Time Travel" with joystick and right trigger
2. Use joystick left/right to move the year slider
3. Watch your environment transform to that time period
4. Try different eras from medieval times to distant future
5. Press left trigger to go back to main menu

#### Virtual Dressing Room:
1. Select "Virtual Dressing Room"
2. Stand where the camera can see you (passthrough view)
3. Use joystick up/down to browse clothing options
4. Press right trigger to try on the selected outfit
5. Wait 3-5 seconds for AI to process
6. See yourself in the new outfit!
7. Press left trigger to go back

#### Biome/Country:
1. Select "Biome/Country"
2. Use joystick up/down to browse locations
3. Press right trigger to transform your environment
4. Try tropical rainforest, Paris, Tokyo, Arctic, etc.
5. Each transformation takes 3-5 seconds
6. Press left trigger to go back

#### Video Game Styles:
1. Select "Video Game Styles"
2. Use joystick up/down to browse game styles
3. Press right trigger to apply the style
4. See your world as Minecraft, Zelda, Cyberpunk, etc.
5. Press left trigger to go back

#### Custom Input:
1. Select "Custom Input"
2. Press right trigger to open the virtual keyboard
3. Type your creative prompt:
   - "Transform into a magical fantasy forest"
   - "Make everything look like watercolor painting"
   - "Turn this into ancient Rome"
4. Press Enter or A button to submit
5. Watch your custom transformation!
6. Press left trigger to go back

### General Controls:
- **Start Button (≡)**: Hide/show menu at any time
- **Left Trigger**: Go back to previous menu
- **Right Trigger**: Confirm/select
- **Joystick Up/Down**: Navigate options
- **Joystick Left/Right**: Adjust slider (Time Travel feature)

### Tips for Best Results:
- Ensure good lighting in your room
- Stand 3-6 feet from walls for best camera view
- Wait 3-5 seconds after selecting a transformation
- Have stable WiFi connection (8+ Mbps)
- For clothing try-on, face the camera directly

---

## 12. Troubleshooting

### Problem: "Build Failed" in Unity

**Solution:**
1. Make sure Android Build Support is installed
2. Check that Scripting Backend is set to IL2CPP
3. Verify ARM64 is checked, ARMv7 is unchecked
4. Try: `Edit` → `Preferences` → `External Tools` → Click "Regenerate project files"

### Problem: Camera Shows Black Screen

**Solution:**
1. Make sure you granted camera permissions
2. Check Quest is running Horizon OS v74+
3. Ensure lighting in room is adequate
4. Restart the app
5. Check if passthrough works in Quest settings

### Problem: No AI Processing/Video Stays Normal

**Solution:**
1. Check your internet connection (need 8+ Mbps)
2. Wait 5-10 seconds - first processing takes time
3. Try closing and reopening the app
4. Verify you're using correct API endpoint
5. Try different WiFi network

### Problem: "Installation Failed" in SideQuest

**Solution:**
1. Make sure Developer Mode is enabled on Quest
2. Check USB debugging was allowed on headset
3. Try different USB cable
4. Uninstall old version first
5. Try restarting both Quest and computer

### Problem: App Won't Launch on Quest

**Solution:**
1. Try restarting your Quest
2. Reinstall the APK
3. Check App Library → All → Unknown Sources
4. Verify you granted all permissions
5. Check Quest has enough storage space

### Problem: Menu Not Responding to Controller

**Solution:**
1. Make sure controllers are paired and charged
2. Try restarting controllers (hold Oculus + menu buttons)
3. Check controller batteries
4. Restart the app
5. Re-pair controllers in Quest settings

### Problem: Voice Input Not Working

**Solution:**
1. Verify Wit.ai tokens are correctly entered
2. Grant microphone permissions
3. Check internet connection
4. Speak clearly while holding trigger
5. Make sure you're using the correct trigger button

### Problem: Transformations Look Wrong/Weird

**Solution:**
1. Try being more specific in prompts
2. Ensure good lighting
3. Wait longer (some transformations take 10 seconds)
4. Try moving to different position
5. Ensure stable internet connection

### Problem: App Crashes or Freezes

**Solution:**
1. Restart your Quest
2. Clear Quest storage (Settings → Storage → Clear Cache)
3. Ensure Quest isn't overheating
4. Close other running apps
5. Reinstall the app

### Problem: Low Frame Rate/Laggy

**Solution:**
1. Use 5GHz WiFi instead of 2.4GHz
2. Close other apps running on Quest
3. Ensure strong WiFi signal
4. Let Quest cool down if hot
5. Restart router if needed

---

## Need More Help?

### Resources:
- **Discord Community**: https://discord.gg/decart
- **Technical Support**: tom@decart.ai
- **Decart Documentation**: https://docs.platform.decart.ai
- **GitHub Issues**: https://github.com/Jakubikk/joystick-nav/issues

### Common Questions:

**Q: Can I use this without internet?**
A: No, the AI processing requires internet connection to Decart's servers.

**Q: How much does Decart API cost?**
A: Check https://platform.decart.ai for current pricing. Trial version is available.

**Q: Can I add my own custom features?**
A: Yes! The code is open source. You can modify the scripts to add new prompts or features.

**Q: Why does it only use one camera?**
A: Meta Quest security limits access to one passthrough camera at a time.

**Q: Can I use this on Quest 2 or Quest Pro?**
A: The app is designed for Quest 3/3S. Quest 2 may work but performance will vary. Quest Pro should work fine.

**Q: How do I update the app?**
A: Pull latest changes from GitHub, rebuild the APK, and reinstall on your Quest.

---

## Congratulations!

You've successfully built and deployed the DecartXR app! Enjoy exploring different time periods, trying on virtual clothing, visiting exotic locations, and experiencing your world through video game aesthetics.

**Have fun creating and exploring!** 🎮🌍✨

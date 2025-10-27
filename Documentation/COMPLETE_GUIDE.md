# Complete Beginner's Guide: Quest 3 Decart AI App
## From Repository Clone to Production Deployment

This guide assumes you have ZERO Unity experience and will walk you through every single step.

---

## Table of Contents
1. [Prerequisites & Initial Setup](#prerequisites--initial-setup)
2. [Installing Unity & Required Software](#installing-unity--required-software)
3. [Cloning the Repository](#cloning-the-repository)
4. [Opening the Project in Unity](#opening-the-project-in-unity)
5. [Understanding the Project Structure](#understanding-the-project-structure)
6. [Setting Up Wit.ai for Voice Input](#setting-up-witai-for-voice-input)
7. [Configuring Unity Settings](#configuring-unity-settings)
8. [Understanding the New Features](#understanding-the-new-features)
9. [Building for Quest 3](#building-for-quest-3)
10. [Installing on Your Quest 3](#installing-on-your-quest-3)
11. [Testing the Application](#testing-the-application)
12. [Troubleshooting Common Issues](#troubleshooting-common-issues)
13. [Advanced: Customizing Features](#advanced-customizing-features)

---

## Prerequisites & Initial Setup

### What You'll Need:
- **Computer**: Windows 10/11, macOS, or Linux
- **Meta Quest 3 headset** (or Quest 3S)
- **USB-C cable** to connect Quest to computer
- **Internet connection** (8+ Mbps for testing)
- **Meta/Oculus account** (free to create)
- **Decart API account** (optional - app uses trial mode by default)
- **Disk Space**: At least 20GB free space
- **Time**: Plan for 2-4 hours for first-time setup

### System Requirements:
- **RAM**: 16GB minimum, 32GB recommended
- **CPU**: Modern quad-core processor
- **GPU**: NVIDIA GTX 1060 or equivalent (for Unity Editor)

---

## Installing Unity & Required Software

### Step 1: Install Unity Hub
1. **Download Unity Hub**:
   - Go to: https://unity.com/download
   - Click "Download Unity Hub" button
   - Save the installer to your Downloads folder

2. **Install Unity Hub**:
   - **Windows**: Double-click `UnityHubSetup.exe`, click "Yes" to allow changes
   - **macOS**: Open `UnityHub.dmg`, drag Unity Hub to Applications
   - **Linux**: Follow the terminal commands provided on the download page

3. **Launch Unity Hub**:
   - **Windows**: Press Windows key, type "Unity Hub", press Enter
   - **macOS**: Open Applications folder, double-click Unity Hub
   - **Linux**: Run from your applications menu

4. **Sign in or Create Account**:
   - Click "Sign In" button in top-right
   - If you don't have an account, click "Create account"
   - Follow the prompts to create a free Unity account
   - Complete email verification

### Step 2: Get a Unity License (Free)
1. In Unity Hub, click your profile icon (top right)
2. Click "Manage licenses"
3. Click "Add" button
4. Select "Get a free personal license"
5. Check "I don't use Unity in a professional capacity"
6. Click "Done"

### Step 3: Install Unity Editor Version 6
1. In Unity Hub, click "Installs" tab on left side
2. Click "Install Editor" button (top right)
3. Find "Unity 6 (6000.0.34f1)" in the list
   - If you don't see it, click "Archive" and select "Unity 6"
4. Click "Install" next to Unity 6 (6000.0.34f1)

5. **Select Modules** (IMPORTANT - don't skip):
   - Check ✅ **Android Build Support**
     - This will expand - make sure both sub-items are checked:
       - ✅ Android SDK & NDK Tools
       - ✅ OpenJDK
   - Check ✅ **Documentation** (optional but helpful)
   - Uncheck everything else to save space
   
6. Click "Install" button
7. Accept the license agreements
8. **Wait** - This will take 30-60 minutes depending on your internet speed
9. When done, you'll see Unity 6 (6000.0.34f1) in your Installs list

### Step 4: Install Git (for Cloning the Repository)
1. **Download Git**:
   - Windows: https://git-scm.com/download/win
   - macOS: Open Terminal and type: `git --version` (macOS will prompt you to install)
   - Linux: Run: `sudo apt-get install git` (Ubuntu/Debian)

2. **Install Git**:
   - Windows: Run the installer, keep all default settings
   - macOS: Follow the prompts
   - Linux: Already done if command above worked

3. **Verify Git Installation**:
   - Open Terminal/Command Prompt
   - Type: `git --version`
   - You should see something like "git version 2.43.0"

---

## Cloning the Repository

### Option A: Using Command Line (Recommended)
1. **Open Terminal/Command Prompt**:
   - Windows: Press `Win + R`, type `cmd`, press Enter
   - macOS: Press `Cmd + Space`, type "Terminal", press Enter
   - Linux: Press `Ctrl + Alt + T`

2. **Navigate to Where You Want the Project**:
   ```bash
   # Windows example - put it in Documents
   cd C:\Users\YourUsername\Documents
   
   # macOS/Linux example
   cd ~/Documents
   ```

3. **Clone the Repository**:
   ```bash
   git clone https://github.com/Jakubikk/joystick-nav.git
   ```

4. **Wait for Download** (about 1-3 minutes):
   - You'll see progress as files download
   - When done, you'll see "done" message

5. **Navigate into the Project**:
   ```bash
   cd joystick-nav
   ```

### Option B: Using GitHub Desktop (Easier for Beginners)
1. **Download GitHub Desktop**:
   - Go to: https://desktop.github.com/
   - Download and install

2. **Clone via GitHub Desktop**:
   - Click "File" → "Clone Repository"
   - Click "URL" tab
   - Paste: `https://github.com/Jakubikk/joystick-nav.git`
   - Choose where to save (e.g., Documents folder)
   - Click "Clone"

---

## Opening the Project in Unity

### Step 1: Add Project to Unity Hub
1. **Open Unity Hub** (if not already open)
2. Click "Projects" tab on the left side
3. Click "Add" button (top right, next to "New project")
4. **Navigate** to where you cloned the repository:
   - Find the `joystick-nav` folder
   - Open it
   - Select the `DecartAI-Quest-Unity` folder (this is the Unity project)
5. Click "Select Folder" or "Add Project"

### Step 2: Open the Project
1. You should now see "DecartAI-Quest-Unity" in your Projects list
2. Next to it, you'll see the Unity version (should say 6000.0.34f1)
3. **Click on the project name** to open it
4. **First time opening will take 10-15 minutes**:
   - Unity is importing all assets
   - Don't close Unity during this time
   - You'll see a progress bar

### Step 3: Initial Unity Window Layout
When Unity finally opens, you'll see several panels:
- **Scene View** (center): 3D view of your game world
- **Game View** (center, tabs): What players see
- **Hierarchy** (left): List of objects in current scene
- **Project** (bottom): All your files and assets
- **Inspector** (right): Properties of selected objects
- **Console** (bottom, tabs): Error messages and logs

---

## Understanding the Project Structure

### Key Folders in the Project Panel:
```
Assets/
├── Samples/
│   └── DecartAI-Quest/
│       ├── Scripts/                  ← All the code
│       │   ├── MenuManager.cs        ← Main menu controller
│       │   ├── TimeTravelController.cs
│       │   ├── VirtualMirrorController.cs
│       │   ├── BiomeController.cs
│       │   ├── VideoGameController.cs
│       │   └── CustomPromptController.cs
│       └── DecartAI-Main.unity      ← Main scene (double-click to open)
├── MetaXR/                           ← Meta Quest SDK
├── PassthroughCameraApiSamples/      ← Camera access code
└── metaVoiceSdk/                     ← Voice recognition SDK
```

### Opening the Main Scene:
1. In the Project panel (bottom), navigate to:
   `Assets → Samples → DecartAI-Quest`
2. **Double-click** on `DecartAI-Main.unity`
3. The scene will load in the Scene view

---

## Setting Up Wit.ai for Voice Input

Voice input is OPTIONAL but recommended for custom prompts.

### Step 1: Create Wit.ai Account
1. Go to: https://wit.ai/
2. Click "Sign Up" (top right)
3. Sign in with your Facebook/Meta account (or create one)
4. Click "Get Started"

### Step 2: Create a New App
1. Once logged in, click "New App" button
2. **App Name**: Type "QuestDecartAI" (or any name you like)
3. **Language**: Select "English" (or your preferred language)
4. **Privacy**: Keep as "Private"
5. Click "Create"

### Step 3: Get Your Access Tokens
1. In your new app, click "Settings" (gear icon, top right)
2. Scroll down to "Server Access Token" section
3. Click the eye icon to reveal the token
4. **Copy the Server Access Token** (click the copy button)
5. Keep this window open - we'll need it soon

### Step 4: Configure Unity with Wit.ai Token
1. **Go back to Unity Editor**
2. In the Project panel, navigate to:
   `Assets → CustomNLP.asset`
3. **Click once** on `CustomNLP.asset` to select it
4. Look at the Inspector panel (right side)
5. Find the field labeled "Server Access Token"
6. **Paste your token** into this field
7. Press `Ctrl + S` (Windows/Linux) or `Cmd + S` (macOS) to save

**Note**: You can also paste the Client Access Token in the "Client Access Token" field if desired, but Server Access Token is what's required.

---

## Configuring Unity Settings

Unity should be mostly configured, but let's verify critical settings.

### Step 1: Verify Android Build Support
1. Click "File" (top menu) → "Build Settings"
2. In the "Platform" list, find "Android"
3. **If Android has a Unity icon next to it**: You're good! Close this window.
4. **If Android does NOT have Unity icon**:
   - Click "Android" once to select it
   - Click "Switch Platform" button (bottom right)
   - Wait 5-10 minutes for Unity to switch
   - Click "Close" when done

### Step 2: Verify Player Settings
1. Click "Edit" (top menu) → "Project Settings"
2. Click "Player" in the left sidebar
3. Make sure you see the Android tab (Android robot icon)
4. Click the Android tab

#### Company Name and Product Name:
- **Company Name**: Change if you want (e.g., your name)
- **Product Name**: Leave as is or change (this is the app name)

#### Other Settings Section:
1. Scroll down to "Other Settings" section
2. Verify these settings:

**Rendering:**
- Color Space: **Linear** ✅
- Auto Graphics API: **UNCHECKED** ❌
- Graphics APIs: Should show **OpenGLES3** (or Vulkan is also okay)
  - If not, click the "+" and add OpenGLES3

**Identification:**
- Package Name: Should be something like `com.CompanyName.DecartAI`
  - You can change the company name part if you want

**Configuration:**
- Scripting Backend: **IL2CPP** ✅ (IMPORTANT!)
- API Compatibility Level: **.NET Standard 2.1** ✅
- Target Architectures: **ARM64 only** ✅ (UNCHECK ARMv7 if checked)
- Target API Level: **34 or higher** (Android 14)
- Minimum API Level: **29 or higher** (Android 10)

#### XR Plugin Management:
1. Still in Project Settings, click "XR Plug-in Management" on left
2. Click the Android tab (Android robot icon)
3. Check ✅ **Oculus** (this enables Quest support)
4. Click "Oculus" text that appears below to expand settings
5. Verify these settings:
   - ✅ **Initialize XR on Startup**: CHECKED
   - ❌ **Low Overhead Mode (GLES)**: UNCHECKED (IMPORTANT!)
   - ❌ **Meta Quest: Occlusion**: UNCHECKED
   - ❌ **Meta XR Subsampled Layout**: UNCHECKED

### Step 3: Close Project Settings
- Click the X on the Project Settings window
- Unity auto-saves, so you're good

---

## Understanding the New Features

This app now has 5 main features accessible through a menu system:

### 1. Time Travel ⏰
- **Purpose**: View your environment as it would look in different historical periods
- **How it works**: Use joystick to select a year (1700-2300), press trigger to apply
- **Periods include**:
  - Colonial Era (1700s)
  - Victorian Era (1800s)
  - Disco Era (1970s)
  - Near Future (2025-2050)
  - Distant Future (2100+)
  - And more...

### 2. Virtual Mirror 👔
- **Purpose**: Try on different clothing and costumes virtually
- **How it works**: Stand in front of a mirror, select clothing, see yourself wearing it
- **Options include**:
  - Casual wear (T-shirt, hoodie)
  - Formal wear (suit, evening dress)
  - Historical (Knight, Victorian, Roman)
  - Fantasy (Wizard, Superhero, Space suit)
  - Cultural (Kimono, Kilt, Sari)
  - 22 total options

### 3. Biomes & Countries 🌍
- **Purpose**: Transform your room to look like different locations worldwide
- **How it works**: Select a location/biome, your environment transforms
- **Options include**:
  - Natural: Rainforest, Arctic, Desert, Ocean
  - Cities: Paris, Tokyo, New York, Dubai
  - Fantasy: Enchanted forest, Crystal cave, Alien planet
  - Seasons: Autumn forest, Winter wonderland
  - 28 total options

### 4. Video Game Styles 🎮
- **Purpose**: See your world through the lens of popular video games
- **How it works**: Select a game, your environment gets that game's visual style
- **Options include**:
  - Minecraft (blocky voxel world)
  - GTA V (realistic city)
  - Cyberpunk 2077 (neon dystopia)
  - Zelda BOTW (cel-shaded fantasy)
  - Portal (clean test chambers)
  - 32 total game styles

### 5. Custom Prompt ⌨️
- **Purpose**: Type your own transformation prompt
- **How it works**: Opens Quest keyboard, type whatever you want
- **Examples**:
  - "Make everything look like candy"
  - "Transform to ancient Egypt"
  - "Steampunk Victorian London"

### Menu Navigation:
- **Hamburger Button** (Start button): Show/hide menu
- **Joystick Up/Down**: Navigate menu options
- **Right Trigger**: Select/confirm
- **Left Trigger**: Go back to main menu

---

## Building for Quest 3

### Step 1: Prepare Your Quest 3
1. **Enable Developer Mode** on your Quest:
   - On your phone, open the Meta Quest app
   - Tap "Menu" (bottom right)
   - Tap "Devices"
   - Select your Quest 3
   - Tap "Developer Mode"
   - Toggle it ON
   - If you don't see this option:
     - Go to meta.com/quest/manage-organizations
     - Create a developer organization (free, requires phone verification)
     - Then try again

2. **Connect Quest to Computer**:
   - Use USB-C cable to connect Quest to your computer
   - Put on your Quest headset
   - You'll see a prompt: "Allow USB debugging?"
   - Check "Always allow from this computer"
   - Click "OK"

### Step 2: Verify Connection
1. **In Unity**, click "File" → "Build Settings"
2. Look at "Run Device" dropdown (near bottom)
3. Click "Refresh" button next to it
4. After a moment, you should see your Quest device name
   - Example: "Quest 3 (serial number)"
5. **If you don't see your device**:
   - Make sure Quest is unlocked and on
   - Make sure you approved USB debugging
   - Try unplugging and replugging the cable
   - Click "Refresh" again

### Step 3: Build Settings Configuration
1. Still in Build Settings window:
2. **Platform**: Make sure "Android" is selected and has Unity icon
3. **Scenes in Build**: Should show "DecartAI-Main" with checkbox checked
   - If it doesn't:
     - Click "Add Open Scenes" button
4. **Run Device**: Select your Quest from dropdown
5. **Build System**: Keep as "Gradle (New)"
6. Leave everything else as default

### Step 4: Build and Run
1. **Click "Build And Run" button** (bottom right)
2. Unity will ask where to save the APK
   - Create a new folder called "Builds" in your project root
   - Name the file: "DecartAI-Quest.apk"
   - Click "Save"

3. **Building process starts** (15-30 minutes first time):
   - You'll see progress in Unity's status bar (bottom)
   - You'll see various messages in the Console
   - **DO NOT close Unity or disconnect Quest during this time**
   - Go get coffee ☕

4. **When build completes**:
   - Unity will automatically install the app on your Quest
   - Your Quest screen will go black briefly
   - The app will launch automatically

### Step 5: Grant Permissions
1. When app first launches, you'll see permission requests
2. **Allow Camera Access**: Tap "Allow" (REQUIRED)
3. **Allow Storage Access** (if asked): Tap "Allow"

---

## Installing on Your Quest 3

If you built successfully in the previous section, the app is already installed! But here's how to find and launch it:

### Finding Your App:
1. **In Quest**, press the Meta button (Oculus button on controller)
2. Navigate to "Apps" library
3. Click the dropdown at top (says "All" by default)
4. Select "Unknown Sources"
5. You should see "DecartAI-Quest" (or whatever you named it)
6. Click on it to launch

### Creating a Shortcut (Optional):
1. In Unknown Sources list, find your app
2. Hover over it with controller
3. Click the three dots (⋮) button
4. Select "Add to Home"
5. Now it appears on your main app grid

---

## Testing the Application

### First Launch:
1. **Put on your Quest 3**
2. **Make sure passthrough is enabled** (you should see your room)
3. **Launch the app** from Unknown Sources

### What You Should See:
1. **Initial loading** (3-5 seconds)
2. **Camera feed appears** - you should see your room
3. **Main menu appears** - showing 5 options:
   - Time Travel
   - Virtual Mirror
   - Biomes & Countries
   - Video Game Styles
   - Custom Prompt

### Testing Navigation:
1. **Move joystick up/down** - menu selection should move
2. **Press hamburger button (Start)** - menu should hide
3. **Press hamburger again** - menu should reappear
4. **Select "Time Travel"** - press right trigger
5. **You should see** year slider and era information
6. **Move joystick** - year should change
7. **Press right trigger** - transformation should start
8. **Wait 3-5 seconds** - you should see AI transformation of your environment
9. **Press left trigger** - return to main menu

### Testing Each Feature:

#### Time Travel:
1. Select from main menu
2. Use joystick to select year
3. Press right trigger to apply transformation
4. Watch your environment change to that historical period

#### Virtual Mirror:
1. Stand in front of a mirror (or any reflective surface)
2. Select from main menu
3. Use joystick to browse clothing options
4. Press right trigger to try on selected outfit
5. See yourself wearing the selected clothing

#### Biomes & Countries:
1. Select from main menu
2. Use joystick to browse locations
3. Press right trigger to transform environment
4. Your room transforms to selected location

#### Video Game Styles:
1. Select from main menu
2. Use joystick to browse game styles
3. Press right trigger to apply game aesthetic
4. Your world looks like the selected video game

#### Custom Prompt:
1. Select from main menu
2. Press right trigger to open keyboard
3. Type your custom prompt (e.g., "underwater coral reef")
4. Press Enter or Done on keyboard
5. Prompt is sent to AI for transformation

### What to Expect:
- **Processing time**: 3-5 seconds after pressing trigger
- **Visual feedback**: Processed video should appear smoothly
- **Latency**: ~150-200ms (barely noticeable)
- **Quality**: 720p resolution, 30fps

---

## Troubleshooting Common Issues

### Camera Not Working
**Problem**: Black screen or no camera feed

**Solutions**:
1. Make sure you granted camera permissions
2. Check Quest is running Horizon OS v74 or later:
   - Settings → About → Software Version
   - Update if needed
3. Try restarting the app
4. Check adequate lighting (not too dark)
5. Clean camera lenses on Quest

### No AI Processing
**Problem**: Camera works but no transformation happens

**Solutions**:
1. Check internet connection (need 8+ Mbps)
2. Wait longer (first transformation can take 10-15 seconds)
3. Try different WiFi network (5GHz preferred)
4. Check Console in Unity for error messages
5. The app uses trial mode - no API key needed for testing

### Build Failed
**Problem**: Unity shows errors during build

**Common fixes**:
1. **"Android SDK not found"**:
   - In Unity Hub, go to Installs
   - Click gear icon next to Unity 6
   - Click "Add Modules"
   - Check Android Build Support
   - Install

2. **"IL2CPP build failed"**:
   - Edit → Project Settings → Player → Android
   - Other Settings → Configuration
   - Make sure Scripting Backend = IL2CPP
   - Make sure ARM64 is checked, ARMv7 unchecked

3. **"Unable to find Unity player"**:
   - File → Build Settings
   - Click "Switch Platform" to Android
   - Wait for it to complete

### App Crashes on Quest
**Problem**: App launches then crashes

**Solutions**:
1. Clear app data:
   - In Quest: Settings → Apps → Unknown Sources
   - Find your app → Storage → Clear data
2. Reinstall:
   - In Unity: File → Build Settings → Build And Run
3. Check Quest has enough storage (need 500MB+)

### Menu Not Responding
**Problem**: Can't navigate menu with controls

**Solutions**:
1. Make sure controllers are tracking (visible to Quest)
2. Try the other controller
3. Check battery level on controllers
4. Restart the app

### Voice/Keyboard Not Working
**Problem**: Can't type custom prompts

**Solutions**:
1. For voice: Make sure Wit.ai token is configured
2. For keyboard: Should use Quest system keyboard automatically
3. Check microphone permissions if using voice
4. Try typing slowly - keyboard might lag on first use

---

## Advanced: Customizing Features

### Adding Your Own Prompts

#### For Time Travel:
1. Open: `Assets/Samples/DecartAI-Quest/Scripts/TimeTravelController.cs`
2. Find `InitializeHistoricalEras()` method
3. Add new era:
```csharp
historicalEras.Add(new YearRange
{
    name = "Your Era Name",
    description = "Brief description",
    prompt = "Detailed transformation prompt",
    startYear = 2025,
    endYear = 2050
});
```

#### For Virtual Mirror:
1. Open: `Assets/Samples/DecartAI-Quest/Scripts/VirtualMirrorController.cs`
2. Find `InitializeClothingOptions()` method
3. Add new clothing:
```csharp
clothingOptions.Add(new ClothingOption
{
    name = "Your Outfit Name",
    description = "Brief description",
    prompt = "Change the person's outfit to [detailed description]"
});
```

#### For Biomes:
1. Open: `Assets/Samples/DecartAI-Quest/Scripts/BiomeController.cs`
2. Find `InitializeBiomeOptions()` method
3. Add new location:
```csharp
biomeOptions.Add(new BiomeOption
{
    name = "Your Location",
    description = "Brief description",
    prompt = "Transform environment to [detailed description]"
});
```

#### For Video Games:
1. Open: `Assets/Samples/DecartAI-Quest/Scripts/VideoGameController.cs`
2. Find `InitializeGameOptions()` method
3. Add new game:
```csharp
gameOptions.Add(new GameStyleOption
{
    name = "Your Game Name",
    description = "Brief description",
    prompt = "Transform to [game name] style, [visual details]"
});
```

### Changing Menu Appearance
(Note: Will require Unity UI knowledge)
1. Open scene: `Assets/Samples/DecartAI-Quest/DecartAI-Main.unity`
2. In Hierarchy, find UI elements
3. Modify in Inspector panel

### Testing Changes:
1. After making changes, save files (`Ctrl/Cmd + S`)
2. Click Play button in Unity (top center) to test in editor
3. Or build and run on Quest to test in VR

---

## Quick Reference: Controls

### Global Controls (Always Available):
- **Hamburger Button** (Start): Show/Hide Menu
- **Left Trigger**: Back/Return to Main Menu
- **Right Trigger**: Confirm/Select
- **Joystick Up/Down**: Navigate Options
- **No other buttons used**

### In Main Menu:
- Joystick: Navigate between features
- Right Trigger: Enter selected feature
- Hamburger: Hide menu

### In Time Travel:
- Joystick: Change year
- Right Trigger: Apply transformation
- Left Trigger: Back to main menu

### In Virtual Mirror:
- Joystick: Browse clothing
- Right Trigger: Try on outfit
- Left Trigger: Back to main menu

### In Biomes/Countries:
- Joystick: Browse locations
- Right Trigger: Transform environment
- Left Trigger: Back to main menu

### In Video Game:
- Joystick: Browse game styles
- Right Trigger: Apply game style
- Left Trigger: Back to main menu

### In Custom Prompt:
- Right Trigger: Open keyboard
- Type on Quest keyboard
- Press Enter: Send prompt
- Left Trigger: Back to main menu

---

## Performance Tips

### For Best Results:
1. **Use 5GHz WiFi** (not 2.4GHz)
2. **Stay close to router** during use
3. **Good lighting** - not too bright, not too dark
4. **Clean Quest cameras** before use
5. **Close other apps** on Quest
6. **Allow Quest to cool** if it gets warm

### Battery Life:
- Expect ~2 hours continuous use
- AI processing is network-based (doesn't drain battery much)
- Camera usage is main battery drain

---

## Getting Help

### Resources:
- **Decart Discord**: https://discord.gg/decart
- **Decart Documentation**: https://docs.platform.decart.ai
- **Unity Learn**: https://learn.unity.com
- **Meta Quest Developer**: https://developer.oculus.com

### Reporting Issues:
- GitHub Issues: Create issue in repository
- Include:
  - Quest model and OS version
  - Unity version
  - Steps to reproduce
  - Screenshots/video if possible

---

## Congratulations! 🎉

You've successfully set up, built, and deployed your Quest 3 Decart AI application!

You now have:
- ✅ Working Quest app with 5 AI features
- ✅ Time travel to different eras
- ✅ Virtual clothing try-on
- ✅ Environment transformation to any location
- ✅ Video game style filters
- ✅ Custom prompt capability

Enjoy exploring AI-transformed reality in VR! 🚀

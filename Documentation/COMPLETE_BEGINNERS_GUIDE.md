# Complete Beginner's Guide: Meta Quest 3 DecartAI App - From Clone to Production

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Installing Unity](#installing-unity)
3. [Cloning the Repository](#cloning-the-repository)
4. [Setting Up Unity Project](#setting-up-unity-project)
5. [Configuring Meta Quest Development](#configuring-meta-quest-development)
6. [Setting Up Decart API](#setting-up-decart-api)
7. [Setting Up Voice Control (Optional)](#setting-up-voice-control-optional)
8. [Understanding the Project Structure](#understanding-the-project-structure)
9. [Building for Meta Quest 3](#building-for-meta-quest-3)
10. [Deploying to Device](#deploying-to-device)
11. [Testing the Application](#testing-the-application)
12. [Troubleshooting](#troubleshooting)

---

## Prerequisites

### Hardware Requirements
- **Computer**: Windows 10/11 or macOS (Windows recommended for Quest development)
- **RAM**: Minimum 16GB (32GB recommended)
- **Storage**: At least 50GB free space
- **Meta Quest 3** headset with:
  - Horizon OS v74 or later
  - USB-C cable for development
  - Charged battery

### Software to Download
1. **Unity Hub** - https://unity.com/download
2. **Unity Editor 6 (6000.0.34f1)** - Will install through Unity Hub
3. **Git** - https://git-scm.com/downloads
4. **Android Studio** (for Android SDK) - https://developer.android.com/studio
5. **Meta Quest Developer Hub** (optional but helpful) - https://developer.oculus.com/downloads/package/oculus-developer-hub-win/

### Accounts to Create
1. **Unity Account** - https://id.unity.com/
2. **Meta Developer Account** - https://developer.oculus.com/
3. **Decart Platform Account** - https://platform.decart.ai/
4. **Wit.ai Account** (for voice control) - https://wit.ai/

---

## Installing Unity

### Step 1: Download and Install Unity Hub
1. Go to https://unity.com/download
2. Click **"Download Unity Hub"**
3. Run the installer
4. Follow the installation wizard (accept all defaults)
5. Launch Unity Hub when installation completes

### Step 2: Sign In to Unity
1. Click **"Sign in"** in the top-right corner
2. Enter your Unity account credentials
3. If you don't have an account, click **"Create account"**

### Step 3: Install Unity Editor
1. In Unity Hub, click **"Installs"** on the left sidebar
2. Click **"Install Editor"** or the **"+"** button
3. Find **"Unity 6 (6000.0.34f1)"** in the list
4. Click **"Install"**
5. In the modules selection screen, check these boxes:
   - ✅ **Android Build Support**
     - ✅ Android SDK & NDK Tools
     - ✅ OpenJDK
   - ✅ **Documentation** (optional)
6. Click **"Install"**
7. Wait for installation to complete (this may take 30-60 minutes)

---

## Cloning the Repository

### Step 1: Install Git
1. Download Git from https://git-scm.com/downloads
2. Run the installer
3. Use all default settings during installation
4. Click **"Finish"**

### Step 2: Clone the Repository
1. Open **Command Prompt** (Windows) or **Terminal** (Mac)
2. Navigate to where you want to store the project:
   ```bash
   cd C:\Users\YourName\Documents
   ```
3. Clone the repository:
   ```bash
   git clone https://github.com/Jakubikk/joystick-nav.git
   ```
4. Wait for the download to complete
5. Navigate into the Unity project folder:
   ```bash
   cd joystick-nav/DecartAI-Quest-Unity
   ```

---

## Setting Up Unity Project

### Step 1: Open Project in Unity
1. Open **Unity Hub**
2. Click **"Projects"** on the left sidebar
3. Click **"Add"** or **"Open"** button
4. Navigate to: `joystick-nav/DecartAI-Quest-Unity`
5. Click **"Select Folder"** or **"Open"**
6. The project will appear in your projects list
7. Click on the project to open it
8. **IMPORTANT**: When Unity asks about version compatibility, click **"Continue"**
9. Wait for Unity to import all assets (this can take 10-20 minutes the first time)

### Step 2: Install Required Packages
1. In Unity, go to **Window** → **Package Manager**
2. Install these packages if they're not already installed:

   **From Unity Registry:**
   - Click the **"+"** dropdown in the top-left
   - Select **"Add package from git URL"**
   - Add these one at a time:
     - `com.meta.xr.sdk.core` (Meta XR Core SDK)
     - `com.unity.xr.management` (XR Plugin Management)
     - `com.unity.xr.oculus` (Oculus XR Plugin)
     - `com.unity.render-pipelines.universal` (Universal Render Pipeline)
     - `com.unity.inputsystem` (Input System)

3. Close Package Manager when done

### Step 3: Import Local Packages
The project includes two local packages that should auto-import:
- **NativeWebSocket** (for WebSocket communication)
- **SimpleWebRTC** (for video streaming)

If they don't appear:
1. Go to **Window** → **Package Manager**
2. Click dropdown at top-left and select **"Packages: In Project"**
3. Verify both packages are listed

---

## Configuring Meta Quest Development

### Step 1: Enable Developer Mode on Quest 3
1. **On your phone/tablet:**
   - Install **Meta Quest** app from App Store or Google Play
   - Open the app and sign in
   - Connect to your Quest 3 headset
   - Tap **Menu** (three lines) → **Devices**
   - Select your **Quest 3**
   - Tap **Headset Settings**
   - Tap **Developer Mode**
   - Toggle **Developer Mode** to **ON**
   - If prompted, create a developer organization at developer.oculus.com

2. **On your Quest 3 headset:**
   - Put on the headset
   - Accept the developer mode prompt if it appears
   - Restart the headset if prompted

### Step 2: Configure Unity for Android Build
1. In Unity, go to **File** → **Build Settings**
2. Select **"Android"** from the platform list
3. Click **"Switch Platform"** (wait for Unity to reimport assets)
4. Click **"Player Settings"** button

### Step 3: Configure Player Settings
In the Inspector panel that opens:

**Company Name and Product Name:**
- **Company Name**: Enter your name or company
- **Product Name**: `DecartAI Quest App` (or your preferred name)

**Other Settings section:**
1. Scroll down to **"Rendering"**
   - **Color Space**: Select **"Linear"**
   - **Graphics APIs**: 
     - Remove **Vulkan** if present (click on it, click "-" button)
     - Ensure **OpenGLES3** is listed
     - Click the "-" to remove Vulkan, then "+" to re-add OpenGLES3 if needed

2. Scroll to **"Identification"**
   - **Package Name**: `com.yourcompany.decartaiquest` (must be unique, lowercase)
   - **Version**: `1.0`
   - **Bundle Version Code**: `1`
   - **Minimum API Level**: Select **"Android 10.0 (API level 29)"**
   - **Target API Level**: Select **"Android 14.0 (API level 34)"** or **"Automatic"**

3. Scroll to **"Configuration"**
   - **Scripting Backend**: Select **"IL2CPP"**
   - **Api Compatibility Level**: Select **".NET Standard 2.1"**
   - **Target Architectures**: 
     - ✅ Check **ARM64** (must be checked)
     - ❌ Uncheck **ARMv7** (must be unchecked)

4. Scroll to **"Other Settings"** → **"Permissions"**
   - Check these permissions:
     - ✅ **Camera**
     - ✅ **Microphone** (if using voice)
     - ✅ **Internet** (should be auto-checked)
   
5. In the **"Custom Permissions"** field, add:
   ```
   horizonos.permission.HEADSET_CAMERA
   ```

### Step 4: Configure XR Plugin Management
1. In Unity, go to **Edit** → **Project Settings**
2. Select **"XR Plug-in Management"** from the left sidebar
3. Click the **Android** tab (Android icon)
4. Check these boxes:
   - ✅ **Oculus**
   - ✅ **Initialize XR on Startup**

5. Click on **"Oculus"** sub-item under XR Plug-in Management
6. Configure these settings:
   - ❌ **Low Overhead Mode (GLES)** - MUST be DISABLED
   - ❌ **Meta Quest: Occlusion** - MUST be DISABLED  
   - ❌ **Meta XR Subsampled Layout** - MUST be DISABLED
   - ✅ **Stereo Rendering Mode**: **Multiview**

### Step 5: Meta XR Project Setup (Fix Known Issues)
1. In Unity, go to **Edit** → **Project Settings**
2. Select **"Meta XR"** from the left sidebar
3. Look at the **"Outstanding Issues"** section:
   - Fix all issues **EXCEPT** these two (leave these unfixed):
     - "Hand Tracking must be enabled in OVRManager..."
     - "When Using Passthrough Building Block..."
   
4. Look at **"Recommended Items"** section:
   - Fix all recommendations **EXCEPT** these two (leave these unfixed):
     - "Use Low Overhead Mode"
     - "Hand tracking support is set to 'Controllers Only'..."

---

## Setting Up Decart API

### Step 1: Create Decart Account
1. Go to https://platform.decart.ai/
2. Click **"Sign Up"** or **"Get Started"**
3. Create your account (you can use Google/GitHub sign-in)
4. Verify your email if required

### Step 2: Get API Key (Optional for Trial)
The app uses trial endpoints by default, but for production:

1. Log into https://platform.decart.ai/
2. Navigate to **"API Keys"** section
3. Click **"Create New API Key"**
4. Give it a name (e.g., "Quest App")
5. Click **"Create"**
6. **Copy the API key** (you won't see it again!)
7. Store it safely

### Step 3: Configure API in Unity (For Production)
If using a paid API key instead of trial:

1. In Unity, go to **Hierarchy** window
2. Find **"WebRTC-Connection"** GameObject
3. Select it
4. In the **Inspector** window, find **"WebRTCConnection"** component
5. Update the WebSocket URLs:
   - **Mirage WebSocket**: Replace with `wss://api3.decart.ai/v1/stream?api_key=YOUR_API_KEY&model=mirage`
   - **Lucy WebSocket**: Replace with `wss://api3.decart.ai/v1/stream?api_key=YOUR_API_KEY&model=lucy_v2v_720p_rt`
   - Replace `YOUR_API_KEY` with your actual API key

---

## Setting Up Voice Control (Optional)

Voice control allows you to speak custom prompts instead of typing them.

### Step 1: Create Wit.ai Account
1. Go to https://wit.ai/
2. Click **"Continue with Facebook"** or **"Sign Up"**
3. Log in with your Facebook/Meta account
4. Accept terms of service

### Step 2: Create Wit.ai App
1. On the Wit.ai dashboard, click **"+ New App"**
2. **App Name**: Enter `Quest Voice Control`
3. **Language**: Select **"English"**
4. **Visibility**: Select **"Private"**
5. Click **"Create"**

### Step 3: Get Access Tokens
1. In your new Wit.ai app, click **"Management"** at the top
2. Click **"Settings"** in the left sidebar
3. Find the **"Server Access Token"** section
4. Click to reveal and **copy the token**
5. Also copy the **"Client Access Token"**
6. Save both tokens in a text file for now

### Step 4: Configure Voice in Unity
1. In Unity, go to **Project** window
2. Navigate to **Assets** folder (root)
3. Find the file **"CustomNLP.asset"**
4. Click on it to select it
5. In the **Inspector** window:
   - **Server Access Token**: Paste your Wit.ai server token
   - **Client Access Token**: Paste your Wit.ai client token
6. Press **Ctrl+S** (or Cmd+S on Mac) to save

**Note:** Wit.ai is only used for speech-to-text. No training or configuration needed!

---

## Understanding the Project Structure

### Main Scene
The main scene is located at:
```
Assets/Samples/DecartAI-Quest/DecartAI-Main.unity
```

### Key GameObjects in the Scene

1. **Main Menu System**
   - **MainMenuCanvas**: The main menu UI
   - **MenuManager**: Controls navigation (attached script)

2. **Feature Panels** (UI elements)
   - **TimeTravelPanel**: Time travel feature UI
   - **VirtualTryOnPanel**: Virtual try-on feature UI
   - **BiomePanel**: Biome transformation UI
   - **VideoGamePanel**: Video game style UI
   - **CustomPromptPanel**: Custom text input UI

3. **Core Systems**
   - **WebRTC-Connection**: Manages Decart AI streaming
   - **PassthroughCameraManager**: Handles Quest camera access
   - **VoiceManager**: Manages voice recognition (if enabled)

### Navigation Controls

The app uses these Quest controller buttons:

| Button | Function |
|--------|----------|
| **Left Trigger** | Go back to previous menu |
| **Right Trigger** | Confirm selection / Apply transformation |
| **Joystick Up/Down** | Navigate menu options / Adjust values |
| **Joystick Left/Right** | Adjust sliders (in Time Travel) |
| **Start (☰) Button** | Show/Hide menu |
| **A Button** | Open keyboard (in Custom Prompt) |

### Feature Descriptions

1. **Time Travel**
   - Use the year slider to select any year from 1500 to 2500
   - The environment transforms to match that historical period
   - Includes Ancient Egypt, Medieval, Victorian, Cyberpunk future, etc.

2. **Virtual Try-On**
   - Stand in front of the camera like a mirror
   - Browse 30+ clothing options (tuxedos, kimonos, superhero costumes, etc.)
   - See yourself wearing different outfits in real-time

3. **Biome/Country Transformation**
   - Transform your room to look like different countries or environments
   - 35+ options including Japan, Paris, tropical rainforest, desert, underwater
   - Complete environmental transformation

4. **Video Game Styles**
   - View your environment in the style of popular video games
   - 60+ game styles (Minecraft, LEGO, Zelda, Cyberpunk 2077, etc.)
   - Each with authentic game aesthetic

5. **Custom Prompt**
   - Type any custom transformation you can imagine
   - Uses Meta Quest's built-in keyboard
   - Direct access to Decart AI with your creativity

---

## Building for Meta Quest 3

### Step 1: Prepare for Build
1. In Unity, go to **File** → **Build Settings**
2. Ensure **"Android"** is selected as platform
3. Check that **"DecartAI-Main"** scene is in "Scenes In Build" list
   - If not, open the scene, then click **"Add Open Scenes"**

### Step 2: Connect Your Quest 3
1. Put on your Quest 3 headset
2. Connect it to your computer using USB-C cable
3. In the headset, you'll see a prompt **"Allow USB debugging?"**
4. Check **"Always allow from this computer"**
5. Click **"OK"**

### Step 3: Verify Connection
**In Unity:**
1. Go to **File** → **Build Settings**
2. Click **"Refresh"** button next to "Run Device"
3. Your Quest 3 should appear in the dropdown (e.g., "Meta Quest 3")
4. Select it from the dropdown

**Or use Command Prompt:**
1. Open Command Prompt (Windows) or Terminal (Mac)
2. Navigate to Android SDK platform-tools:
   ```
   cd C:\Users\YourName\AppData\Local\Android\Sdk\platform-tools
   ```
3. Run:
   ```
   adb devices
   ```
4. You should see your Quest 3 listed

### Step 4: Build the APK
1. In Unity **Build Settings** window, click **"Build And Run"**
   - Or click **"Build"** to just create the APK file
2. Choose a location to save the APK (e.g., `Builds/DecartAIQuest.apk`)
3. Click **"Save"**
4. Unity will start building (this takes 10-30 minutes the first time)
5. Watch the progress bar in the bottom-right of Unity

### Step 5: Wait for Build to Complete
- First build: 15-30 minutes
- Subsequent builds: 5-10 minutes
- You'll see:
  - "Compiling shaders..."
  - "Building Player..."
  - "Packaging APK..."

---

## Deploying to Device

### Option 1: Automatic Deploy (Build And Run)
If you used **"Build And Run"**:
1. The app automatically installs to your Quest 3
2. It will launch automatically when installation completes
3. Put on your headset to use the app!

### Option 2: Manual Install via SideQuest
If you only built the APK:

1. **Download SideQuest:**
   - Go to https://sidequestvr.com/
   - Click **"Download SideQuest"**
   - Install SideQuest on your computer

2. **Connect Quest 3:**
   - Connect Quest 3 via USB-C cable
   - Open SideQuest
   - Your Quest should show as connected (green dot)

3. **Install APK:**
   - In SideQuest, click the **"Install APK file"** button (top-right)
   - Navigate to your built APK file
   - Select it and click **"Open"**
   - Wait for installation to complete

### Option 3: Manual Install via ADB
1. Open Command Prompt/Terminal
2. Navigate to platform-tools folder:
   ```
   cd C:\Users\YourName\AppData\Local\Android\Sdk\platform-tools
   ```
3. Install the APK:
   ```
   adb install "C:\path\to\your\DecartAIQuest.apk"
   ```
4. Wait for "Success" message

---

## Testing the Application

### First Launch

1. **Put on your Quest 3 headset**

2. **Find the app:**
   - Press the **Meta/Oculus button** to open the menu
   - Go to **"App Library"**
   - Click **"All"** tab at the top
   - Sort by **"Recent"** or search for your app name
   - You'll see it under **"Unknown Sources"** category

3. **Launch the app:**
   - Click on the app icon
   - Click **"Open"** or **"Launch"**

4. **Grant permissions:**
   - When prompted, allow **Camera** permission
   - If using voice, allow **Microphone** permission
   - These prompts appear once on first launch

### Using the App

**Initial Screen:**
- You'll see the main menu with 5 options
- Your camera feed appears in the background
- The menu shows:
  1. Time Travel
  2. Virtual Try-On
  3. Biome/Country
  4. Video Game Style
  5. Custom Prompt

**Navigation:**
1. **Move joystick UP/DOWN** to highlight different menu options
2. **Pull RIGHT TRIGGER** to select highlighted option
3. **Pull LEFT TRIGGER** to go back to main menu
4. **Press START button (☰)** to hide/show menu

**Testing Each Feature:**

**1. Time Travel:**
- Select "Time Travel" from main menu
- Use **joystick left/right** to move the year slider
- Or manually drag the slider in VR
- Watch your environment transform to different eras
- Try years like:
  - 1500 (Renaissance)
  - 1920 (Roaring Twenties)
  - 2077 (Cyberpunk future)

**2. Virtual Try-On:**
- Select "Virtual Try-On"
- Stand facing the camera (treat it like a mirror)
- Use **joystick up/down** to browse clothing
- **Right trigger** to apply selected outfit
- Try outfits like:
  - Tuxedo
  - Kimono
  - Superhero Costume
  - Medieval Armor

**3. Biome/Country:**
- Select "Biome/Country"
- Use **joystick up/down** to browse locations
- **Right trigger** to apply transformation
- Try locations like:
  - Tropical Rainforest
  - Japan
  - Paris
  - Underwater Coral Reef

**4. Video Game Style:**
- Select "Video Game Style"
- Use **joystick up/down** to browse game styles
- **Right trigger** to apply game aesthetic
- Try games like:
  - Minecraft
  - LEGO
  - Zelda BOTW
  - Cyberpunk 2077

**5. Custom Prompt:**
- Select "Custom Prompt"
- Press **A button** to open the keyboard
- Type your custom transformation
  - Example: "Transform this into a pirate ship"
  - Example: "Make everything look like candy"
- **Right trigger** to submit and apply

### Performance Testing

**Check these aspects:**
- **Camera feed**: Should be clear and smooth
- **Transformation delay**: 3-5 seconds for AI processing to start
- **Frame rate**: Should maintain 25-30 FPS after processing starts
- **Latency**: Total latency should be 150-200ms (feels near real-time)

**Internet connection matters:**
- Requires stable 8+ Mbps connection
- Use 5GHz WiFi for best results
- Poor connection = slower transformations or failures

---

## Troubleshooting

### Camera Issues

**Problem: Black screen or no camera feed**

Solutions:
1. Grant camera permissions:
   - In Quest, go to **Settings** → **Apps**
   - Find your app → **Permissions**
   - Enable **Camera** permission

2. Check Horizon OS version:
   - Go to **Settings** → **System** → **About**
   - OS version should be **v74 or higher**
   - Update if needed

3. Check camera in passthrough:
   - Double-tap side of headset to enter passthrough
   - If passthrough works, app cameras should work

**Problem: Camera feed is laggy**
- Close other apps running on Quest
- Restart the app
- Restart Quest headset
- Check WiFi connection

### Build Errors

**Problem: "IL2CPP error" during build**

Solution:
- Go to **Edit** → **Project Settings** → **Player**
- Under **Other Settings** → **Configuration**
- Ensure **Scripting Backend** is set to **IL2CPP**
- Ensure **Target Architectures** has only **ARM64** checked

**Problem: "Unable to install APK"**

Solutions:
1. Enable developer mode on Quest 3
2. Check USB debugging is allowed
3. Try different USB cable
4. Run in Command Prompt:
   ```
   adb kill-server
   adb start-server
   adb devices
   ```

**Problem: "Shader errors" during build**

Solution:
- Go to **Edit** → **Project Settings** → **Graphics**
- Set **Scriptable Render Pipeline Settings** to the URP asset
- Go to **Edit** → **Render Pipeline** → **Universal Render Pipeline** → **Build for XXX**

### Runtime Errors

**Problem: App crashes on launch**

Solutions:
1. Check all permissions are granted
2. Rebuild with **Development Build** checked (helps debugging)
3. Check Android **Minimum API Level** is 29+
4. Verify **ARM64** architecture only

**Problem: No AI processing / transformations don't work**

Solutions:
1. Check internet connection:
   - In Quest, go to **Settings** → **WiFi**
   - Ensure connected to 5GHz network
   - Run speed test (need 8+ Mbps)

2. Check WebRTC connection:
   - Look for debug logs in Unity (if Development Build)
   - Should see "WebSocket connection opened!"

3. Check Decart API status:
   - Visit https://platform.decart.ai/
   - Check for service status announcements

4. Wait longer:
   - First transformation can take 10-15 seconds
   - Subsequent ones are faster (3-5 seconds)

**Problem: Voice control doesn't work**

Solutions:
1. Grant microphone permission
2. Verify Wit.ai tokens in CustomNLP.asset
3. Hold trigger button while speaking
4. Speak clearly and wait for transcription
5. Check internet connection

### Performance Issues

**Problem: Low frame rate / stuttering**

Solutions:
1. Close background apps on Quest
2. Use 5GHz WiFi network
3. Move closer to WiFi router
4. Let Quest cool down if overheating
5. Reduce video quality (if option added)

**Problem: High latency / delayed response**

Solutions:
1. Check network speed (minimum 8 Mbps)
2. Use wired internet connection for router
3. Reduce network congestion (close streaming services)
4. Try during off-peak hours

### Menu Issues

**Problem: Can't navigate menu**

Solutions:
1. Ensure controllers are paired and tracking
2. Check controller batteries
3. Press **Start button (☰)** to toggle menu visibility
4. Restart app if menu is stuck

**Problem: Menu doesn't respond to controls**

Solutions:
1. Check OVR Input is working:
   - Try using A/B buttons
   - Try grip buttons
   - If none work, restart app

2. Re-pair controllers:
   - Go to Quest **Settings** → **Devices** → **Controllers**
   - Unpair and re-pair controllers

### Build Settings Issues

**Problem: Can't see Quest 3 in Run Device**

Solutions:
1. Install correct USB drivers:
   - Download Meta Quest drivers
   - Or use Android Studio to install drivers

2. Enable USB debugging again:
   - In headset, go to **Settings** → **System** → **Developer**
   - Toggle **USB Debugging** off then on

3. Check cable:
   - Use official Meta Quest cable
   - Or quality USB-C 3.0+ cable

4. Try different USB port on computer

**Problem: Build takes forever**

Solutions:
1. First build is always slow (15-30 min)
2. Subsequent builds much faster
3. Close other applications
4. Disable antivirus temporarily during build
5. Use SSD storage for project

---

## Production Deployment

### Preparing for Release

**1. Disable Development Features:**
- In Build Settings, uncheck **"Development Build"**
- Uncheck **"Script Debugging"**
- Uncheck **"Wait For Managed Debugger"**

**2. Update Version:**
- Edit → Project Settings → Player
- Increment **Bundle Version Code**
- Update **Version** number (e.g., 1.0 → 1.1)

**3. Use Production API:**
- Replace trial endpoints with your production Decart API key
- Update WebSocket URLs in WebRTCConnection component
- Test thoroughly before release

**4. Optimize Build:**
- Go to **Edit** → **Project Settings** → **Player**
- Under **Publishing Settings**:
  - Check **"Minify"** options
  - Set **"IL2CPP Code Generation"** to **"Faster runtime"**

### Distribution Options

**Option 1: Meta Quest Store (Official)**
1. Create Meta Developer Organization
2. Submit app to Meta for review
3. Follow Meta's submission guidelines
4. Wait for approval (1-2 weeks)

**Option 2: SideQuest Store**
1. Create SideQuest developer account
2. Upload APK to SideQuest
3. Provide screenshots and description
4. Publish (faster approval)

**Option 3: Direct Distribution**
1. Share APK file directly
2. Users install via SideQuest or ADB
3. Mark as "Unknown Sources" app
4. Good for beta testing

---

## Additional Resources

### Official Documentation
- **Meta Quest Development**: https://developer.oculus.com/documentation/
- **Unity XR**: https://docs.unity3d.com/Manual/XR.html
- **Decart API**: https://docs.platform.decart.ai/
- **Wit.ai**: https://wit.ai/docs

### Community Support
- **Decart Discord**: https://discord.gg/decart
- **Unity Forums**: https://forum.unity.com/
- **Meta Quest Discord**: Various community servers
- **Reddit**: r/OculusQuest, r/Unity3D

### Video Tutorials
- Search YouTube for:
  - "Unity Quest 3 development tutorial"
  - "Meta XR development beginner"
  - "Unity Android build tutorial"

### Getting Help
If you encounter issues not covered in this guide:

1. **Check Console logs** in Unity (Window → General → Console)
2. **Enable Development Build** for better debugging
3. **Search error messages** on Google
4. **Ask on Discord**:
   - Decart Discord for AI-related issues
   - Unity Discord for Unity issues
   - Meta developer forums for Quest issues

---

## Congratulations!

You've successfully set up, built, and deployed your Meta Quest 3 DecartAI application! 

You now have a powerful AR application with:
- ✅ Time travel through different historical periods
- ✅ Virtual try-on for clothing and costumes
- ✅ Biome and country transformations
- ✅ Video game style filters
- ✅ Custom AI prompts via keyboard
- ✅ Intuitive joystick navigation
- ✅ Real-time AI processing

Enjoy exploring different realities with your Quest 3!

---

**Document Version**: 1.0  
**Last Updated**: October 26, 2024  
**Compatible with**: Unity 6 (6000.0.34f1), Meta Quest 3, Horizon OS v74+

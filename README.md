# Quest's First RealTime World Transformation App

**Built with ❤️ for the Quest developer community, we are introducing the First RealTime World Transformation for VR**

<p align="center">
  <a href="https://discord.gg/decart">🗣️ Join our Discord</a> •
  <a href="https://decart.ai">🌐 Visit Decart.ai</a> •
  <a href="https://platform.decart.ai">⚡ API Platform</a> •
  <a href="mailto:tom@decart.ai">📧 Technical Support</a>
</p>

<p align="center">
  <img src="assets/images/animated-cyberpunk.gif" alt="Cyberpunk AI Transformation" width="400" style="margin-right: 20px;">
  <img src="assets/images/animated-lego-mc.gif" alt="LEGO and Minecraft AI Transformations" width="400">
</p>

<p align="center">
  <img src="assets/images/luxury.png" alt="Cyberpunk Style" width="200" style="margin: 5px;">
  <img src="assets/images/ultrareal.png" alt="Ultra Realistic Style" width="200" style="margin: 5px;">
  <img src="assets/images/lego-world.png" alt="LEGO Style" width="200" style="margin: 5px;">
  <img src="assets/images/minecraft.png" alt="Minecraft Style" width="200" style="margin: 5px;">
</p>

Developed by Decart AI, this Unity application demonstrates real-time AI-powered video transformation on Meta Quest 3 devices, enabling users to live inside AI-generated art with sub-200ms latency. Released as an open sourced project for researchers, developers, and the Quest community to explore the intersection of VR and AI.

![Quest AI Video Processing Demo](https://img.shields.io/badge/Platform-Meta%20Quest%203-blue) ![Unity](https://img.shields.io/badge/Unity-6-brightgreen) ![WebRTC](https://img.shields.io/badge/WebRTC-Real--time%20Streaming-orange) ![AI](https://img.shields.io/badge/AI-Decart%20Processing-purple)

## ✨ Features

### Core Capabilities
- 🎥 **Real-time Camera Capture** - Direct access to Quest 3 passthrough cameras
- 🤖 **Dual AI Models** - Mirage (world transformations) and Lucy (person transformations)
- ⚡ **Ultra-low Latency** - ~150-200ms end-to-end processing time
- 🌐 **WebRTC Streaming** - Efficient VP8 video encoding at 30fps
- 📱 **VR-Optimized UI** - Intuitive joystick-based navigation system

### 5 Transformation Features

#### 🕰️ Time Travel (23 historical eras)
Transform your environment to any year from 1500 to 2200. Experience medieval castles, Victorian steam-punk, retro 1980s neon, or far-future space-age aesthetics. Control with an intuitive year slider using the joystick.

#### 👗 Virtual Mirror - Clothing Try-On (32 outfits)
Stand in front of a mirror and try on different clothing styles. Categories include formal wear, casual, athletic, professional uniforms, cultural traditional, fantasy costumes, seasonal wear, and vintage styles. See yourself in real-time wearing medieval knight armor, chef whites, traditional kimono, or any of 32+ options.

#### 🌍 Biome/Country Transformation (47 locations)
View your room as if it was in a different country or environment. Explore Arctic tundra, tropical rainforest, Japanese zen gardens, Parisian cafes, Egyptian temples, underwater reefs, or fantasy environments like enchanted forests and floating islands.

#### 🎮 Video Game Style (57 game aesthetics)
Transform your environment to match your favorite video games. Experience Minecraft's blocky voxels, Cyberpunk 2077's neon dystopia, Zelda's cel-shaded fantasy, Portal's test chambers, or classic 8-bit pixel art. Covers iconic games, open world, sci-fi, horror, RPG, retro, and more.

#### ✍️ Custom Prompt (unlimited possibilities)
Type your own transformation prompts using the Meta Quest virtual keyboard. Describe anything you can imagine with detailed text descriptions, and the AI will interpret and apply your vision in real-time.

**See [Documentation/FEATURES.md](Documentation/FEATURES.md) for complete feature details and usage guide.**

## 🚀 Quick Start

## 🔬 Decart AI Model Integration

This project showcases Decart's real-time video-to-video AI transformation system optimized for VR applications. The model processes live camera feeds at 30fps and 720p resolution, delivering consistent style transformations while maintaining temporal stability crucial for VR experiences.

### Prerequisites

- **Hardware**: Meta Quest 3 with Horizon OS v74+
- **Unity**: Unity 6 (6000.0.34f1) with Android build support
- **Network**: 8+ Mbps bidirectional internet connection

### Installation

1. **Clone the Repository**
   ```bash
   git clone https://github.com/DecartAI/Decart-XR.git
   cd Decart-XR/DecartAI-Quest-Unity
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Add project from `DecartAI-Quest-Unity/` folder
   - Open with Unity 6 (6000.0.34f1)

3. **Install Required Packages**
   - Install packages via Package Manager (see [Package Dependencies](#package-dependencies))
   - Import local packages (NativeWebSocket and SimpleWebRTC are included)

4. **Configure Project Settings**
   - Follow [Unity Project Configuration](#unity-project-configuration) section below

5. **Load Main Scene**
   ```
   DecartAI-Quest-Unity/Assets/Samples/DecartAI-Quest/DecartAI-Main.unity
   ```

6. **Build for Quest**
   - Switch platform to Android (File → Build Settings → Android → Switch Platform)
   - Configure build settings for Quest 3
   - Open Edit → Project Settings → Meta XR and resolve configuration issues:
     - In Outstanding Issues: Fix all issues EXCEPT "Hand Tracking must be enabled in OVRManager when using its Building Block" and "When Using Passthrough Building Block as an underlay it's required to set the camera background to transparent" (keep these unfixed)
     - In Recommended Items: Fix all recommendations EXCEPT "Use Low Overhead Mode" and "Hand tracking support is set to 'Controllers Only', hand tracking will not work in this mode" (keep these unfixed)
   - Build and install APK to headset (File → Build Settings → Build) or use Unity menu: **DecartAI → Build and Install to Quest**

7. **Launch & Use**
   - Grant camera permissions when prompted
   - See live camera feed immediately
   - Use the menu to select features (see Controls below)

## 🎮 Controls

### Navigation
- **Left Joystick UP/DOWN**: Navigate through menu options
- **Right Trigger**: Confirm selection / Apply transformation
- **Left Trigger**: Go back to previous menu
- **Menu Button (Start/Hamburger)**: Show/Hide main menu

### Features
- **Time Travel**: Use right joystick LEFT/RIGHT to control year slider
- **Clothing/Biome/Game Styles**: Browse with left joystick UP/DOWN, confirm with right trigger
- **Custom Prompt**: Point at text field and press right trigger to open Quest keyboard

**Note**: Voice control features have been replaced with joystick navigation and keyboard input for better reliability and user experience.

## 📦 Package Dependencies

Install these packages via Unity Package Manager:

### Required Unity Packages
- **Meta XR Core SDK** - `com.meta.xr.sdk.core` - Quest platform support
- **XR Plug-in Management** - `com.unity.xr.management` - XR system management
- **Oculus XR Plugin** - `com.unity.xr.oculus` - Quest device support
- **Universal Render Pipeline** - `com.unity.render-pipelines.universal` - URP rendering
- **Input System** - `com.unity.inputsystem` - Modern input handling

### Included Local Packages
- **NativeWebSocket** - `com.endel.nativewebsocket` - WebSocket communication
- **SimpleWebRTC** - `com.firedragongamestudio.simplewebrtc` - WebRTC integration

## ⚙️ Unity Project Configuration

### XR Plugin Management Settings

Navigate to **Edit → Project Settings → XR Plug-in Management → Oculus**:

- ✅ **Initialize XR on Startup**
- ❌ **Low Overhead Mode (GLES)** - Must be **DISABLED**
- ❌ **Meta Quest: Occlusion** - Must be **DISABLED**
- ❌ **Meta XR Subsampled Layout** - Must be **DISABLED** (OpenXR)

### Player Settings

Navigate to **Edit → Project Settings → Player → Android Settings**:

#### Graphics Settings
- **Graphics APIs**: **Vulkan** or **OpenGLES3**
- **Color Space**: Linear
- **Rendering Path**: Forward

#### Configuration
- **Scripting Backend**: IL2CPP
- **Api Compatibility Level**: .NET Standard 2.1
- **Target Architectures**: ARM64 ✅ (ARMv7 ❌)

#### Android Permissions
Add to **Other Settings → Configuration**:
- ✅ **Camera**
- Add custom permission: `horizonos.permission.HEADSET_CAMERA`

#### Scripting Define Symbols
In **Other Settings → Script Compilation**:
- No additional symbols required for basic WebRTC functionality

### Build Settings
- **Platform**: Android
- **Texture Compression**: ASTC
- **Development Build**: Recommended for debugging

## 🤖 Automation & Documentation

### Quick Setup Scripts
For faster project setup, use our automated scripts:

**Windows**:
```batch
Scripts\setup-project.bat
Scripts\install-apk.bat
```

**macOS/Linux**:
```bash
./Scripts/setup-project.sh
./Scripts/install-apk.sh
```

### Unity Editor Tools
Use the **DecartAI** menu in Unity for automated build tasks:
- **Build APK** - Quick APK build with timestamp
- **Build and Install to Quest** - One-click build and deploy
- **Configure Project Settings** - Auto-configure Android settings
- **Check Quest Connection** - Verify device connectivity

### Documentation
- **[Complete Beginner's Guide](Documentation/COMPLETE_BEGINNERS_GUIDE.md)** - Step-by-step from clone to production (22KB, 500+ lines)
- **[Features Guide](Documentation/FEATURES.md)** - Detailed feature documentation with usage examples (17KB)
- **[Automation vs Manual](Documentation/AUTOMATION_VS_MANUAL.md)** - ROI analysis and automation strategy (18KB)

## 🏗️ Architecture

```
Quest Camera → Unity WebRTC → Decart AI → Processed Video → Quest Display
     ↑              ↑              ↑            ↑              ↑
  Camera API    VP8 Encoding   Style AI    VP8 Decoding    UI Rendering
   Permissions   @30fps/4Mbps  ~50-100ms     Real-time      Real-time
```

### Core Technologies

- **[Decart AI](https://mirage.decart.ai/)** - Advanced video-to-video neural networks
- **SimpleWebRTC** - Unity WebRTC integration and video streaming
- **NativeWebSocket** - Cross-platform WebSocket communication
- **Quest Passthrough Camera API** - Native camera access on Quest 3
- **Android Camera2 API** - Low-level camera control and configuration
- **WebSocket Signaling** - Custom protocol for AI service communication

## 📖 Documentation

For detailed technical documentation, see the Wiki

## 🛠️ Development

### Key Components

#### Core Controllers
- `MenuController.cs` - Main menu system with joystick navigation
- `WebRTCController.cs` - WebRTC connection management and video display
- `WebRTCConnection.cs` - Unity WebRTC lifecycle and video streaming
- `WebRTCManager.cs` - Core WebRTC logic and signaling protocol

#### Feature Controllers
- `TimeTravelController.cs` - Time period transformation with year slider (23 eras)
- `ClothingController.cs` - Virtual mirror clothing try-on (32 outfits)
- `BiomeController.cs` - Environment and country transformations (47 locations)
- `VideoGameController.cs` - Game aesthetic transformations (57 styles)
- `CustomPromptController.cs` - Custom text prompt input with Quest keyboard

#### Camera & Permissions
- `WebCamTextureManager.cs` - Quest camera integration via Unity API
- `PassthroughCameraUtils.cs` - Android Camera2 API integration
- `PassthroughCameraPermissions.cs` - Runtime permission management

#### Editor Tools
- `AutoBuildTools.cs` - Automated build and deployment utilities

### Build Configuration

```
Platform: Android
Target: Quest 3
API Level: Minimum 29, Target 34+
Architecture: ARM64
Scripting Backend: IL2CPP
```

### Performance Specifications

- **Resolution**: 1280×720 @ 30fps
- **Codec**: VP8 with adaptive bitrate (1-4 Mbps)
- **Latency**: ~150-200ms end-to-end
- **Battery**: ~2 hours continuous use
- **Memory**: ~50MB additional usage

## 🔧 Troubleshooting

### Common Issues

**Camera not working?**
- Ensure Quest 3/3S with Horizon OS v74+
- Grant camera permissions in Quest settings
- Check adequate lighting and clean camera lenses

**No AI processing?**
- Verify 8+ Mbps internet connection
- Wait 5-10 seconds for initial processing
- Try different network if connection fails
- reopen the app

**Performance issues?**
- Use 5GHz WiFi with strong signal
- Allow device to cool if overheating
- Close other bandwidth-intensive apps

## 📦 Dependencies & Third-Party Licenses

This project incorporates several open source components and proprietary SDKs. We gratefully acknowledge the following:

### Unity & Rendering Dependencies
- **[Meta XR SDK](https://developer.oculus.com/downloads/package/unity-integration/)** - Quest development SDK (Meta License)
- **[Universal Render Pipeline](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal/)** - URP rendering (Unity License)

### Open Source Foundation Components
- **[QuestCameraKit](https://github.com/xrdevrob/QuestCameraKit)** by [@xrdevrob](https://github.com/xrdevrob) (MIT License)
- **[SimpleWebRTC](https://github.com/FireDragonGameStudio/SimpleWebRTC)** by [Fire Dragon Game Studio](https://github.com/FireDragonGameStudio) (MIT License)
- **[NativeWebSocket](https://github.com/endel/NativeWebSocket)** by [@endel](https://github.com/endel) (MIT License)
- **[Meta Voice SDK](https://developer.oculus.com/documentation/unity/voice-sdk-overview/)** (Meta License)
- **[Decart AI](https://platform.decart.ai/)** (Proprietary Service)

### License Compatibility
All open source components are compatible with MIT licensing. The Meta SDKs are used under their respective developer agreements. This project respects all original licenses and attributions.

**Full license texts and detailed attributions can be found in the individual component directories within this repository.**

## 🙏 Special Thanks

Special thanks to [@xrdevrob](https://github.com/xrdevrob) for creating QuestCameraKit, which provided the foundational camera access implementation that made this project possible.


## 🤝 Contributing

We welcome contributions to advance VR AI research! Key areas include performance optimization, platform support, and enhanced user experiences. See our wiki for detailed contribution guidelines.

## 📜 License

This project is available under MIT License.

The Decart AI service has its own terms of service. Quest development requires Meta's developer agreements.

## ⚠️ Disclaimer

This is an experimental project demonstrating real-time AI video processing capabilities. Performance may vary based on network conditions, device temperature, and AI service availability.

The AI processing service is provided by Decart and subject to their terms of service and availability.

## 📞 Contact

For research collaboration, questions, or technical support:

**Technical Support:** tom@decart.ai
**Discord Community:** https://discord.gg/decart
**Main Website:** https://decart.ai
**API Platform:** https://platform.decart.ai

For general inquiries about this open source project, please use GitHub Issues.

---

*Built with ❤️ for the Quest developer community. Powered by Decart AI technology.*

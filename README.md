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

### Four Main Transformation Modes:
- 🕰️ **Time Travel** - View your environment as it would appear in any year from 1800 to 2200 with an interactive slider
- 👔 **Virtual Mirror / Clothing Try-On** - Stand in front of a mirror and try on 15 different clothing styles instantly
- 🌴 **Biome Transformation** - Transform your room into 15 different environments (tropical paradise, arctic tundra, etc.)
- 🎮 **Video Game Styles** - See your world as 15 different video games (Minecraft, LEGO, Cyberpunk, etc.)
- ⌨️ **Custom Prompts** - Type any transformation using the built-in Meta Quest keyboard

### Technical Features:
- 🎥 **Real-time Camera Capture** - Direct access to Quest 3 passthrough cameras
- 🤖 **AI-Powered Transformations** - Powered by Decart AI (Mirage and Lucy models)
- ⚡ **Ultra-low Latency** - ~150-200ms end-to-end processing time
- 🌐 **WebRTC Streaming** - Efficient VP8 video encoding at 30fps
- 🎮 **Joystick Navigation** - Intuitive menu system with joystick up/down navigation
- 📱 **VR-Optimized UI** - Beautiful, easy-to-use menu interface

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
   - Build and install APK to headset (File → Build Settings → Build)

7. **Launch & Enjoy**
   - Grant camera permissions when prompted
   - Use the intuitive joystick navigation system to explore features
   - See live transformation in real-time!

## 🎮 Navigation Controls

The app uses a simple, intuitive joystick navigation system:

| Control | Action |
|---------|--------|
| **Joystick Up/Down** | Navigate through menu options |
| **Joystick Left/Right** | Navigate within submenus (for clothing, biomes, games) |
| **Right Trigger** | Confirm selection / Apply transformation |
| **Left Trigger** | Go back to previous menu |
| **Start Button (≡)** | Show / Hide menu |

**No other buttons are used** - designed for maximum simplicity!

## 📖 Complete Beginner's Guide

New to Unity? Never deployed to Quest before? We've got you covered!

**See our comprehensive guide**: [Documentation/COMPLETE_BEGINNERS_GUIDE.md](Documentation/COMPLETE_BEGINNERS_GUIDE.md)

This guide walks you through:
- Installing all required software
- Cloning the repository
- Setting up Unity from scratch
- Configuring every single setting
- Building and deploying to your Quest 3
- Using all features of the app
- Troubleshooting common issues

Written for absolute beginners with step-by-step instructions!

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

- `MenuManager.cs` - **NEW** - Menu navigation system and feature management
- `MenuOptionDisplay.cs` - **NEW** - UI helper for displaying menu options
- `WebRTCController.cs` - Main application controller and UI management
- `WebRTCConnection.cs` - Unity WebRTC lifecycle, video streaming, and model selection
- `WebRTCManager.cs` - Core WebRTC logic with AI prompt integration
- `WebCamTextureManager.cs` - Quest camera integration via Unity API
- `PassthroughCameraUtils.cs` - Android Camera2 API integration

### The Four Main Features

#### 1. Time Travel (1800-2200)
Transform your environment to show how it would appear in different time periods.
- **Interactive slider** to select any year from 1800 to 2200
- **Smart prompts** automatically generated based on era
- Examples:
  - 1850: "Horse-drawn carriages, gas lamps, historical architecture"
  - 2024: "Modern vehicles, contemporary architecture"
  - 2150: "Flying vehicles, holographic displays, futuristic cities"

#### 2. Virtual Mirror / Clothing Try-On
Stand in front of a mirror and try on 15 different clothing styles instantly.
- **15 Clothing Options**: Business Suit, Casual Jeans, Summer Dress, Winter Coat, Athletic Wear, Formal Gown, Leather Jacket, Traditional Kimono, Medieval Armor, Space Suit, Pirate Outfit, Victorian Dress, Superhero Costume, Chef Uniform, Beach Wear
- Uses **Lucy AI model** optimized for person transformations
- **Preserves your identity** while changing clothing

#### 3. Biome / Environment Transformation
Transform your room into 15 different environments and climates.
- **15 Biome Options**: Tropical Paradise, Arctic Tundra, Desert Oasis, Rainforest Jungle, Mountain Summit, Underwater Reef, Autumn Forest, Cherry Blossom Garden, Savanna Plains, Bamboo Forest, Volcanic Landscape, Meadow Flowers, Mangrove Swamp, Ice Cave, Canyon Desert
- Uses **Mirage AI model** for complete environment transformation
- **Maintains temporal consistency** for smooth VR experience

#### 4. Video Game Styles
See your world as if it were rendered in 15 different video game engines.
- **15 Game Styles**: Minecraft, LEGO, Grand Theft Auto, The Legend of Zelda, Cyberpunk 2077, Animal Crossing, Mario Kart, Fortnite, Red Dead Redemption, Super Mario, Portal, Pac-Man, Sonic, Pokemon, Fallout
- Each style faithfully recreates the visual aesthetic
- **Real-time transformation** with game-accurate graphics

#### 5. Custom Prompts
Type any transformation you can imagine using Meta Quest's built-in keyboard.
- Opens **native Meta Quest keyboard** interface
- Supports complex, detailed prompts
- Example: "Transform into a steampunk workshop with brass gears, steam vents, Victorian machinery"

## 🛠️ Development
- `PassthroughCameraPermissions.cs` - Runtime permission management

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

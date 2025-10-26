# Documentation

This folder contains comprehensive documentation for the Decart Quest 3 VR application.

## Quick Start

If you're new to the project, start here:

1. **[COMPLETE_DEPLOYMENT_GUIDE.md](COMPLETE_DEPLOYMENT_GUIDE.md)** - Full guide from cloning to production
   - For complete beginners who never used Unity
   - Step-by-step instructions with screenshots
   - Everything from installation to App Store submission

2. **[FEATURES_GUIDE.md](FEATURES_GUIDE.md)** - Complete feature overview
   - Detailed description of all 5 features
   - How to use each feature
   - Tips and best practices

3. **[UNITY_SCENE_SETUP.md](UNITY_SCENE_SETUP.md)** - Unity Editor setup instructions
   - For developers setting up the scene
   - Component connections and references
   - UI creation and configuration

## Documentation Files

### For Users

- **FEATURES_GUIDE.md** - Learn about all features and how to use them
  - Time Travel (17 time periods)
  - Virtual Try-On (30+ outfits)
  - Biome Transform (40+ locations)
  - Video Game Style (60+ games)
  - Custom Prompt (unlimited)

### For Developers

- **COMPLETE_DEPLOYMENT_GUIDE.md** - Full development and deployment workflow
  - Installing Unity and required tools
  - Cloning and opening the project
  - Building and testing on Quest 3
  - Publishing to stores

- **UNITY_SCENE_SETUP.md** - Unity Editor specific instructions
  - Scene hierarchy setup
  - Component configuration
  - UI creation with MenuUISetup tool
  - Reference connections

## Project Structure

```
joystick-nav/
├── Documentation/              (You are here)
│   ├── README.md              (This file)
│   ├── COMPLETE_DEPLOYMENT_GUIDE.md
│   ├── FEATURES_GUIDE.md
│   └── UNITY_SCENE_SETUP.md
├── DecartAI-Quest-Unity/      (Unity project)
│   ├── Assets/
│   │   └── Samples/
│   │       └── DecartAI-Quest/
│   │           ├── DecartAI-Main.unity  (Main scene)
│   │           └── Scripts/
│   │               └── Menu/
│   │                   ├── README.md    (Menu system docs)
│   │                   ├── MenuManager.cs
│   │                   ├── TimeTravelFeature.cs
│   │                   ├── VirtualTryOnFeature.cs
│   │                   ├── BiomeTransformFeature.cs
│   │                   ├── VideoGameStyleFeature.cs
│   │                   ├── CustomPromptFeature.cs
│   │                   └── Editor/
│   │                       └── MenuUISetup.cs
│   └── LocalPackages/
│       └── com.firedragongamestudio.simplewebrtc/
├── decart documentation/       (Official Decart AI docs)
└── README.md                   (Project main README)
```

## Quick Links

### Getting Started

- [Install Unity](https://unity.com/download)
- [Get Quest Developer Mode](https://developer.oculus.com)
- [Decart AI Platform](https://platform.decart.ai)

### Documentation

- [Unity Manual](https://docs.unity3d.com/Manual/)
- [Meta Quest Development](https://developer.oculus.com/documentation/)
- [Decart API Docs](https://docs.platform.decart.ai/)

### Support

- [GitHub Issues](https://github.com/Jakubikk/joystick-nav/issues)
- [Decart Discord](https://discord.gg/decart)
- [Meta Developer Forum](https://forums.oculusvr.com)

## Feature Overview

### 1. Time Travel
View your environment in any year from 1800 to 2100. Experience historical eras and future visions with 17 distinct time periods.

**Use Cases:** Education, historical experiences, architectural visualization

### 2. Virtual Try-On
Try on 30+ different outfits using AI transformation. From business suits to superhero costumes, see yourself in any style.

**Use Cases:** Fashion, cosplay, entertainment, virtual fitting rooms

### 3. Biome Transform
Transform your room to 40+ different locations and environments. Visit Japan, tropical beaches, fantasy worlds, and more.

**Use Cases:** Virtual tourism, relaxation, productivity, themed events

### 4. Video Game Style
Apply 60+ video game aesthetics to your reality. See the world through Minecraft, Cyberpunk, Zelda, and many more games.

**Use Cases:** Gaming content, nostalgia, entertainment, creative projects

### 5. Custom Prompt
Create unlimited custom transformations using Meta's VR keyboard. Type any prompt you can imagine.

**Use Cases:** Unlimited creativity, professional needs, unique visions

## Navigation Controls

All features use the same simple control scheme:

- **Joystick Up/Down**: Navigate menu options
- **Right Trigger**: Confirm selection / Apply transformation
- **Left Trigger**: Go back to previous menu
- **Hamburger Button**: Show/Hide menu

**No other buttons are used** - keeping it simple and intuitive!

## Technical Specifications

- **Platform**: Meta Quest 3 / Quest 3S
- **Unity Version**: Unity 6 (6000.0.34f1)
- **AI Models**: Decart Mirage & Lucy
- **Resolution**: 1280×720 @ 30 FPS
- **Latency**: Sub-200ms
- **Internet**: 8+ Mbps required

## What's Included

### Core Features ✅

- [x] Complete menu system with joystick navigation
- [x] Time Travel with year slider (1800-2100)
- [x] Virtual Try-On with 30+ clothing options
- [x] Biome Transform with 40+ locations
- [x] Video Game Style with 60+ game aesthetics
- [x] Custom Prompt with keyboard input (no voice)
- [x] Automated Unity Editor UI setup tool
- [x] Complete deployment guide
- [x] Unity scene setup instructions
- [x] Comprehensive feature documentation

### User Experience ✅

- [x] Simple joystick + trigger navigation
- [x] Clear visual feedback
- [x] VR-optimized UI
- [x] Real-time AI transformations
- [x] No voice input (keyboard only)
- [x] Hamburger menu toggle
- [x] Preset options for quick access

### Developer Tools ✅

- [x] MenuUISetup editor tool
- [x] Modular feature architecture
- [x] Well-documented code
- [x] Unity scene structure
- [x] Build configuration guide
- [x] Testing instructions

## Workflow Overview

### For Users (Playing the App)

1. Put on Quest 3
2. Launch app
3. Grant camera permissions
4. Main menu appears
5. Navigate with joystick, select with trigger
6. Choose feature → Apply transformation → Enjoy!

### For Developers (Building the App)

1. Clone repository
2. Install Unity 6
3. Open project in Unity
4. Run MenuUISetup tool (Tools → Decart → Setup Menu UI)
5. Connect component references
6. Build to Quest 3
7. Test and iterate

### For Publishers (Releasing the App)

1. Complete all development
2. Build release APK
3. Test thoroughly on Quest 3
4. Create store assets (screenshots, videos)
5. Submit to Meta Quest Store or SideQuest
6. Market and distribute

## Best Practices

### Development

- Test one feature at a time
- Use Unity Editor playmode for quick iteration
- Check Console for errors frequently
- Keep references organized in Inspector
- Commit changes regularly

### User Experience

- Start with good lighting
- Ensure strong WiFi connection
- Allow 5-10 seconds for AI connection
- Try preset options before custom prompts
- Experiment with different features

### Content Creation

- Write detailed prompts (15-30 words)
- Include atmosphere and mood
- Specify visual elements clearly
- Use presets as examples
- Test prompts before finalizing

## Troubleshooting

### Common Issues

**Menu not visible:**
- Check Canvas position (0, 1.5, 2)
- Verify Event Camera is set
- Ensure Canvas is enabled

**AI not working:**
- Check internet connection
- Verify Decart API access
- Wait 10-15 seconds for initial connection

**Build errors:**
- Check all references are connected
- Verify Unity version is correct
- Review Console for specific errors

**See COMPLETE_DEPLOYMENT_GUIDE.md for detailed troubleshooting**

## Version History

### v1.0 (Current)

- Initial release with 5 complete features
- Menu system with joystick navigation
- 150+ built-in transformation options
- Custom prompt support
- Full VR optimization
- No voice-to-text (keyboard only)

## Roadmap (Potential Future Features)

These are ideas, not commitments:

- Save favorite transformations
- History of applied prompts
- Preset libraries
- Social sharing
- Multi-user experiences
- Enhanced customization

## Contributing

For developers wanting to contribute:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

See CONTRIBUTING.md in project root for details.

## License

See LICENSE file in project root for licensing information.

## Credits

- **AI Technology**: Decart AI
- **VR Platform**: Meta Quest 3
- **Development**: Decart XR Team
- **WebRTC**: SimpleWebRTC
- **Camera**: QuestCameraKit

## Support

Need help? Resources available:

- **Documentation**: You're reading it!
- **GitHub Issues**: Report bugs or request features
- **Decart Discord**: Community support
- **Email**: See project README for contact

## Final Notes

This documentation is designed to be comprehensive yet accessible. Whether you're a complete beginner or an experienced developer, you should find the information you need.

**Start with COMPLETE_DEPLOYMENT_GUIDE.md if you're new to Unity!**

Happy developing! 🚀

---

*Documentation Last Updated: October 2025*
*Unity Version: 6000.0.34f1*
*Meta Quest OS: v74+*

# Documentation

This folder contains comprehensive documentation for the Meta Quest 3 AI Environment Transformation app.

## Available Documentation

### 📘 [COMPLETE_BEGINNERS_GUIDE.md](COMPLETE_BEGINNERS_GUIDE.md)
**For**: Absolute beginners who have never used Unity or deployed to Quest before

**Contents**:
- Step-by-step installation of all required software
- Cloning the repository
- Complete Unity setup from scratch
- Every single configuration setting explained
- Building and deploying to Quest 3
- Using all app features
- Comprehensive troubleshooting section
- Success checklist

**Length**: ~24,000 words of detailed instructions

**Start here if**: You've never used Unity, never deployed to Quest, or want complete step-by-step guidance.

---

### 🎮 [FEATURES.md](FEATURES.md)
**For**: Users who want to understand all available features in detail

**Contents**:
- Navigation controls reference
- Complete Feature 1: Time Travel (1800-2200)
- Complete Feature 2: Virtual Mirror / Clothing Try-On (15 styles)
- Complete Feature 3: Biome/Environment Transformation (15 biomes)
- Complete Feature 4: Video Game Styles (15 games)
- Complete Feature 5: Custom Prompts with keyboard
- Technical implementation details
- Tips for writing great prompts
- Performance specifications
- Troubleshooting guide

**Length**: ~19,000 words with full feature descriptions

**Start here if**: You want to learn about all available features and how to use them effectively.

---

## Quick Navigation

### I'm a complete beginner
→ Start with [COMPLETE_BEGINNERS_GUIDE.md](COMPLETE_BEGINNERS_GUIDE.md)

### I have Unity experience
→ Read the main [README.md](../README.md) in the root folder

### I want to learn about features
→ Read [FEATURES.md](FEATURES.md)

### I'm having issues
→ Check the Troubleshooting sections in:
- [COMPLETE_BEGINNERS_GUIDE.md](COMPLETE_BEGINNERS_GUIDE.md#troubleshooting)
- [FEATURES.md](FEATURES.md#troubleshooting)

---

## Project Structure

```
joystick-nav/
├── Documentation/                    ← You are here
│   ├── README.md                    ← This file
│   ├── COMPLETE_BEGINNERS_GUIDE.md  ← Full setup guide
│   └── FEATURES.md                  ← Feature documentation
├── README.md                         ← Main project README
├── decart documentation/             ← Decart AI documentation
└── DecartAI-Quest-Unity/            ← Unity project
    └── Assets/
        └── Samples/
            └── DecartAI-Quest/
                ├── Scripts/
                │   ├── Menu/        ← Menu system scripts
                │   └── WebRTCController.cs
                └── DecartAI-Main.unity  ← Main scene
```

---

## Key Features Summary

### 🕰️ Time Travel (1800-2200)
View your environment across 400 years of history and future, with an interactive slider and era-appropriate transformations.

### 👔 Virtual Mirror / Clothing Try-On
Stand in front of a mirror and instantly try on 15 different clothing styles, from business suits to superhero costumes.

### 🌴 Biome Transformation
Transform your room into 15 different environments - tropical paradise, arctic tundra, desert oasis, and more.

### 🎮 Video Game Styles
See your world as 15 different video games - Minecraft, LEGO, Cyberpunk, Pokemon, and others.

### ⌨️ Custom Prompts
Type any transformation you can imagine using Meta Quest's built-in keyboard.

---

## Navigation Controls

| Control | Function |
|---------|----------|
| **Joystick Up/Down** | Navigate menu options |
| **Joystick Left/Right** | Navigate within submenus |
| **Right Trigger** | Confirm / Apply |
| **Left Trigger** | Go back |
| **Start Button (≡)** | Show/Hide menu |

---

## Technical Requirements

### Hardware
- Meta Quest 3 or Quest 3S
- Horizon OS v74 or higher
- USB-C cable for deployment

### Software
- Unity 6 (6000.0.34f1 or later)
- Android Build Support for Unity
- SideQuest (for APK deployment)

### Network
- 8+ Mbps bidirectional internet
- 5GHz WiFi recommended
- Stable connection required

---

## Getting Help

### Official Resources
- **Decart Discord**: https://discord.gg/decart
- **Decart Documentation**: https://docs.platform.decart.ai
- **GitHub Issues**: https://github.com/Jakubikk/joystick-nav/issues

### Before Asking for Help
1. Check the [COMPLETE_BEGINNERS_GUIDE.md](COMPLETE_BEGINNERS_GUIDE.md) troubleshooting section
2. Check the [FEATURES.md](FEATURES.md) troubleshooting section
3. Verify all setup steps were followed exactly
4. Check Unity Console for error messages
5. Ensure your Quest 3 has the latest OS update

### When Reporting Issues
Please include:
- Quest 3 OS version
- Unity version used
- Complete error messages (screenshots help)
- What step failed
- What you've already tried

---

## Contributing

Contributions are welcome! See [CONTRIBUTING.md](../CONTRIBUTING.md) in the root folder.

---

## License

This project is available under MIT License. See [LICENSE](../LICENSE) in the root folder.

---

**Last Updated**: 2024  
**Version**: 1.0  
**Platform**: Meta Quest 3  
**Engine**: Unity 6

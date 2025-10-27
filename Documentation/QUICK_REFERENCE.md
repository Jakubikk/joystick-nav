# Quick Reference Guide

## 📋 Quick Start

### For Complete Beginners
→ **Start here:** [COMPLETE_GUIDE.md](COMPLETE_GUIDE.md)
- Step-by-step from zero to deployed app
- Assumes no Unity knowledge
- Covers everything you need

### For Experienced Developers
```bash
# Clone repository
git clone https://github.com/Jakubikk/joystick-nav.git
cd joystick-nav

# Build and deploy (automated)
chmod +x scripts/*.sh
./scripts/build-and-deploy.sh

# Or manual: Open DecartAI-Quest-Unity in Unity 6
```

---

## 🎮 Features at a Glance

| Feature | What It Does | How Many Options |
|---------|-------------|-----------------|
| 🕰️ **Time Travel** | View environment in different years | 1800-2200 (401 years) |
| 👔 **Virtual Try-On** | Try on different clothing | 15 outfits |
| 🌍 **Biome Transformation** | Transform room to different places | 18 environments |
| 🎮 **Video Game Style** | Apply game aesthetics | 20 game styles |
| ✍️ **Custom Prompt** | Type your own ideas | Unlimited |

---

## 🎯 Controller Mapping

```
Left Trigger    → Go Back
Right Trigger   → Confirm / Apply
Joystick ↕      → Navigate Menu
Hamburger (≡)   → Show/Hide Menu
Y Button        → Open Keyboard (Custom Prompt only)
X Button        → Clear Text (Custom Prompt only)
```

**No other buttons are mapped!**

---

## 📚 Documentation Index

### User Guides
- **[COMPLETE_GUIDE.md](COMPLETE_GUIDE.md)** - Full setup guide for beginners
- **[FEATURES.md](FEATURES.md)** - Detailed feature descriptions

### Developer Guides
- **[AUTOMATION_VS_MANUAL.md](AUTOMATION_VS_MANUAL.md)** - Build automation comparison
- **[IMPLEMENTATION_SUMMARY.md](IMPLEMENTATION_SUMMARY.md)** - Technical implementation details
- **[scripts/README.md](../scripts/README.md)** - Automation scripts documentation

### Project Standards
- **[CODING_STANDARDS.md](../CODING_STANDARDS.md)** - Code conventions
- **[CONTRIBUTING.md](../CONTRIBUTING.md)** - How to contribute

---

## 🚀 Building the App

### Automated (Recommended)
```bash
# Build only
./scripts/build.sh

# Deploy only (requires built APK)
./scripts/deploy.sh

# Build and deploy together
./scripts/build-and-deploy.sh
```

### Manual (Traditional)
1. Open Unity Hub
2. Open project: `DecartAI-Quest-Unity`
3. File → Build Settings → Android → Switch Platform
4. Configure settings (see COMPLETE_GUIDE.md)
5. File → Build Settings → Build

---

## 🔧 Key Files Reference

### C# Scripts
```
MenuController.cs           - Main menu system
TimeTravelController.cs     - Time travel feature
VirtualTryOnController.cs   - Virtual try-on feature
BiomeController.cs          - Biome transformation
VideoGameController.cs      - Video game styles
CustomPromptController.cs   - Custom prompts
BuildAutomation.cs          - Build automation
```

### Automation
```
scripts/build.sh            - Automated builds
scripts/deploy.sh           - Automated deployment
scripts/build-and-deploy.sh - Combined pipeline
```

### Documentation
```
Documentation/COMPLETE_GUIDE.md         - Beginner's guide
Documentation/FEATURES.md               - Features reference
Documentation/AUTOMATION_VS_MANUAL.md   - Automation guide
Documentation/IMPLEMENTATION_SUMMARY.md - Technical summary
```

---

## 🐛 Quick Troubleshooting

### Build Issues
```bash
# Clean build directory
rm -rf Builds/

# Verify Unity path in build.sh
# Update UNITY_PATH variable if needed
```

### Deploy Issues
```bash
# Check device connection
adb devices

# Restart ADB server
adb kill-server
adb start-server

# Verify Developer Mode is ON in Quest settings
```

### App Issues
- **Camera not working?** → Check permissions in Quest settings
- **AI not connecting?** → Check WiFi (need 8+ Mbps)
- **Menu not showing?** → Press Hamburger button (≡)

---

## 📖 How to Use Each Feature

### Time Travel
1. Select "Time Travel" from menu
2. Move joystick left/right to change year
3. Press right trigger to apply

### Virtual Try-On
1. Stand in front of a mirror
2. Select "Virtual Try-On"
3. Browse with joystick up/down
4. Press right trigger to try on

### Biome Transformation
1. Select "Biome Transformation"
2. Browse with joystick up/down
3. Press right trigger to transform

### Video Game Style
1. Select "Video Game Style"
2. Browse with joystick up/down
3. Press right trigger to apply

### Custom Prompt
1. Select "Custom Prompt"
2. Press Y button to open keyboard
3. Type your idea
4. Press right trigger to apply

---

## 🎓 Learning Path

### Day 1: Setup
1. Read COMPLETE_GUIDE.md sections 1-3
2. Install Unity and required software
3. Clone repository

### Day 2: Build
1. Read COMPLETE_GUIDE.md sections 4-6
2. Open project in Unity
3. Try automated build script

### Day 3: Deploy
1. Enable Developer Mode on Quest
2. Build APK
3. Deploy to Quest 3

### Day 4: Explore
1. Read FEATURES.md
2. Try all 5 features
3. Experiment with custom prompts

---

## 💡 Tips & Best Practices

### For Users
- Use 5GHz WiFi for best performance
- Stand in good lighting for camera
- Wait 5-10 seconds for AI to start
- Try custom prompts with 20-30 words

### For Developers
- Start with automated scripts
- Use Development builds for testing
- Check build logs for errors
- Follow coding standards

---

## 🔗 Quick Links

### External Resources
- **Decart Platform:** https://platform.decart.ai
- **Decart Discord:** https://discord.gg/decart
- **Unity Docs:** https://docs.unity3d.com
- **Meta Quest Dev:** https://developer.oculus.com

### Repository
- **GitHub:** https://github.com/Jakubikk/joystick-nav
- **Issues:** https://github.com/Jakubikk/joystick-nav/issues

---

## 📊 Quick Stats

```
Total Features:        5
Total Options:        50+
C# Scripts:           9 new (2,400+ lines)
Documentation:        4 guides (63KB total)
Automation Scripts:   3 shell scripts
Supported Years:      1800-2200 (401 years)
Clothing Options:     15 styles
Biome Options:        18 environments
Game Styles:          20 aesthetics
```

---

## 🚦 Status Overview

| Component | Status |
|-----------|--------|
| C# Code | ✅ Complete |
| Documentation | ✅ Complete |
| Automation Scripts | ✅ Complete |
| Unity Scene Setup | ⚠️ Required |
| Testing | ⚠️ Required |

---

## ❓ Get Help

### Questions?
1. Check [COMPLETE_GUIDE.md](COMPLETE_GUIDE.md) troubleshooting section
2. Review [FEATURES.md](FEATURES.md) for feature-specific help
3. See [AUTOMATION_VS_MANUAL.md](AUTOMATION_VS_MANUAL.md) for build issues
4. Ask on [Decart Discord](https://discord.gg/decart)
5. Open [GitHub Issue](https://github.com/Jakubikk/joystick-nav/issues)

### Found a Bug?
1. Check if it's already in GitHub Issues
2. Include error logs and steps to reproduce
3. Mention your Unity version and Quest OS version

### Want to Contribute?
1. Read [CONTRIBUTING.md](../CONTRIBUTING.md)
2. Follow [CODING_STANDARDS.md](../CODING_STANDARDS.md)
3. Submit a Pull Request

---

*Last Updated: October 27, 2025*
*Quick Reference v1.0*

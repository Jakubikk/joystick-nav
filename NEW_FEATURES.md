# Quest 3 Decart AI - Enhanced Menu System

## 🆕 New Features (v2.0)

This enhanced version includes a comprehensive menu-driven system with 5 major AI transformation features:

### 🎮 Features at a Glance

- ⏰ **Time Travel** - View your environment in 12 different historical periods (1700-2300)
- 👔 **Virtual Mirror** - Try on 22 different clothing styles and costumes
- 🌍 **Biomes & Countries** - Transform your room to 28 different locations worldwide
- 🎮 **Video Game Styles** - Apply 32 different video game aesthetic filters
- ⌨️ **Custom Prompts** - Type your own unlimited transformation ideas

**Total**: 94+ pre-configured transformations + unlimited custom options!

---

## 🎯 Navigation Controls

Simple and intuitive controls - no complexity, just immersion:

- **Hamburger Button** (Start): Show/hide menu
- **Joystick Up/Down**: Navigate through options
- **Right Trigger**: Confirm selection / Apply transformation
- **Left Trigger**: Go back to main menu
- **No other buttons used** - Clean, focused experience

---

## 📖 Complete Documentation

### For Absolute Beginners:
👉 **[Complete Beginner's Guide](Documentation/COMPLETE_GUIDE.md)** - From zero Unity knowledge to production deployment. Every single step explained in detail.

### For Understanding Features:
👉 **[Feature Documentation](Documentation/FEATURES.md)** - Comprehensive guide to all 94+ transformations, prompts, and how to customize them.

### For Optimizing Workflow:
👉 **[Automation Guide](Documentation/AUTOMATION_COMPARISON.md)** - Manual vs automated workflows, time savings analysis (29% faster!), and best practices.

### Implementation Summary:
👉 **[Implementation Summary](Documentation/IMPLEMENTATION_SUMMARY.md)** - Technical details, statistics, and project completion report.

---

## 🚀 Quick Start

### Option 1: Using Automation (Recommended)
```bash
# Clone repository
git clone https://github.com/Jakubikk/joystick-nav.git
cd joystick-nav

# Build for Quest
cd Automation
chmod +x *.sh
./build.sh --platform quest --config release

# Deploy to Quest
./deploy.sh --all
```

### Option 2: Manual in Unity
See [Complete Beginner's Guide](Documentation/COMPLETE_GUIDE.md) for step-by-step instructions.

---

## 🎨 Feature Highlights

### Time Travel ⏰
Transform your environment to different time periods:
- Colonial Era (1700s)
- Victorian Era (1800s)  
- Disco Era (1970s)
- Modern Era (2010s)
- Near Future (2025-2050)
- Distant Future (2100+)
- And 6 more eras!

### Virtual Mirror 👔
Try on different outfits virtually:
- Casual wear (t-shirts, hoodies)
- Formal wear (suits, dresses)
- Historical (knight armor, toga, Victorian)
- Fantasy (wizard robes, superhero suits)
- Cultural (kimono, kilt, sari)
- Sports uniforms
- Professional attire
- Character costumes (pirate, cowboy)

### Biomes & Countries 🌍
Transform your room into:
- Natural biomes (rainforest, arctic, desert, ocean)
- Major cities (Paris, Tokyo, NYC, Venice, Dubai)
- Fantasy locations (enchanted forest, alien planet, crystal cave)
- Seasonal variations (autumn, winter, spring, summer)
- Historical settings (Ancient Greece, Medieval castle, Wild West)

### Video Game Styles 🎮
See your world as your favorite games:
- Blocky: Minecraft, LEGO World
- RPG: Zelda, Witcher 3, Skyrim, Final Fantasy
- Action: GTA V, Assassin's Creed, Uncharted
- Sci-Fi: Cyberpunk 2077, Halo, Mass Effect, Portal
- Horror: Resident Evil, Silent Hill
- Retro: 8-bit, 16-bit SNES
- And 20+ more game styles!

### Custom Prompts ⌨️
Unlimited creativity - type anything you imagine:
- Opens Meta Quest's system keyboard
- Type your own transformation ideas
- Saves prompt history
- Includes quick presets

---

## 🛠️ Automation Tools

Save time with our automation scripts:

### Build Script
```bash
./build.sh --platform quest --config release
```
**Saves**: ~21 minutes per build cycle

### Deploy Script  
```bash
./deploy.sh --all
```
**Saves**: 4-24 minutes depending on device count

See [Automation README](Automation/README.md) for details.

---

## 📊 Project Statistics

- **94+ Pre-configured Options** across 4 major features
- **Unlimited Custom Options** via keyboard input
- **6 New Controller Scripts** (~2,000 lines of code)
- **4 Documentation Files** (64KB total)
- **4 Automation Scripts** (cross-platform)
- **29% Time Savings** with automation
- **100% Coverage** from beginner to production

---

## 🎓 Learning Resources

### New to Unity?
Start here: [Complete Beginner's Guide](Documentation/COMPLETE_GUIDE.md)
- Installing Unity (with screenshots)
- Opening the project
- Building for Quest
- Deploying to device
- Troubleshooting

### Want to Customize?
Read: [Feature Documentation](Documentation/FEATURES.md)
- How to add your own time periods
- How to add new clothing options
- How to add locations/biomes
- How to add game styles
- Prompt engineering tips

### Want to Optimize Workflow?
Check out: [Automation Comparison](Documentation/AUTOMATION_COMPARISON.md)
- Manual vs automated workflows
- Time savings analysis
- ROI calculations
- Best practices

---

## 🏗️ Technical Architecture

```
Menu System
    ├── Time Travel (Mirage model)
    │   └── 12 historical eras
    ├── Virtual Mirror (Lucy model)
    │   └── 22 clothing options
    ├── Biomes & Countries (Mirage model)
    │   └── 28 locations
    ├── Video Game Styles (Mirage model)
    │   └── 32 game aesthetics
    └── Custom Prompts (Both models)
        └── Unlimited user-defined
```

### AI Models:
- **Decart Mirage v2**: Environment transformations
- **Decart Lucy v2v**: Person transformations
- **Processing**: ~3-5 seconds per transformation
- **Latency**: ~150-200ms end-to-end

---

## 🤝 Contributing

We welcome contributions! Areas where you can help:

- Adding more transformation options
- Creating UI improvements
- Testing on different devices
- Improving documentation
- Adding language support
- Creating tutorial videos

See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

---

## 📜 License

This project is available under MIT License.

The Decart AI service has its own terms of service. Quest development requires Meta's developer agreements.

---

## 📞 Support

### Documentation:
- [Complete Guide](Documentation/COMPLETE_GUIDE.md) - Full beginner tutorial
- [Features](Documentation/FEATURES.md) - All transformations documented
- [Automation](Documentation/AUTOMATION_COMPARISON.md) - Workflow optimization
- [Summary](Documentation/IMPLEMENTATION_SUMMARY.md) - Technical overview

### Community:
- **Decart Discord**: https://discord.gg/decart
- **GitHub Issues**: For bug reports and feature requests
- **Email**: tom@decart.ai (technical support)

### Resources:
- **Decart Docs**: https://docs.platform.decart.ai
- **Meta Quest Dev**: https://developer.oculus.com
- **Unity Learn**: https://learn.unity.com

---

## 🙏 Credits

- **Original Project**: Decart AI Team
- **Enhanced Features**: Community contributions
- **AI Technology**: Decart AI (https://decart.ai)
- **Camera Integration**: Based on QuestCameraKit by @xrdevrob
- **WebRTC**: SimpleWebRTC by Fire Dragon Game Studio
- **Voice SDK**: Meta Voice SDK

---

## ⚠️ Important Notes

### Requirements:
- Meta Quest 3 or Quest 3S
- Horizon OS v74 or later
- Unity 6 (6000.0.34f1)
- 8+ Mbps internet connection
- WiFi (5GHz recommended)

### Known Limitations:
- Single camera access (Meta security limitation)
- Requires internet for AI processing
- ~2 hour battery life during continuous use
- Works best in well-lit environments

### Beta Status:
This is an experimental project demonstrating real-time AI capabilities. Performance may vary based on network conditions and device temperature.

---

## 🎉 Get Started Now!

1. **Clone** the repository
2. **Read** the [Complete Guide](Documentation/COMPLETE_GUIDE.md)
3. **Build** the project
4. **Experience** AI-transformed reality in VR!

**Ready to transform your reality?** 🚀

---

*Built with ❤️ for the Quest developer community. Powered by Decart AI technology.*

# Meta Quest 3 AI Transformation - Quick Reference

## 🚀 Quick Start

1. **Clone**: `git clone https://github.com/Jakubikk/joystick-nav.git`
2. **Open in Unity 6**: `DecartAI-Quest-Unity` folder
3. **Configure**: Follow settings in [COMPLETE_BEGINNERS_GUIDE.md](COMPLETE_BEGINNERS_GUIDE.md)
4. **Build**: File → Build Settings → Android → Build
5. **Deploy**: Install APK to Quest 3 via SideQuest

## 🎮 Controls

| Button | Action |
|--------|--------|
| Joystick ⬆️⬇️ | Navigate |
| Joystick ⬅️➡️ | Browse (in submenus) |
| Right Trigger 🎯 | Confirm |
| Left Trigger ⬅ | Back |
| Start ☰ | Show/Hide Menu |

## ✨ 4 Main Features

### 1. ⏰ Time Travel
**Range**: 1800 - 2200  
**Control**: Joystick left/right to adjust year  
**Use**: See environment in different eras

### 2. 👔 Virtual Mirror
**Options**: 15 clothing styles  
**Use**: Stand before mirror, try on outfits  
**Examples**: Business suit, Kimono, Space suit, Pirate

### 3. 🌴 Biome Transform
**Options**: 15 environments  
**Use**: Transform your room  
**Examples**: Tropical, Arctic, Desert, Rainforest

### 4. 🎮 Video Game Styles
**Options**: 15 game aesthetics  
**Use**: See world as video game  
**Examples**: Minecraft, LEGO, Pokemon, Cyberpunk

### 5. ⌨️ Custom Prompts
**Input**: Meta Quest keyboard  
**Use**: Type any transformation  
**Tip**: Be specific and descriptive!

## 📁 Project Structure

```
joystick-nav/
├── Documentation/
│   ├── COMPLETE_BEGINNERS_GUIDE.md  ← Start here if new!
│   ├── FEATURES.md                   ← All features explained
│   └── README.md                     ← Doc navigation
├── DecartAI-Quest-Unity/
│   └── Assets/
│       └── Samples/
│           └── DecartAI-Quest/
│               ├── Scripts/
│               │   ├── Menu/         ← New menu system
│               │   │   ├── MenuManager.cs
│               │   │   ├── TimeTravelSliderController.cs
│               │   │   ├── OptionListDisplay.cs
│               │   │   └── ControlsDisplay.cs
│               │   └── WebRTCController.cs
│               └── DecartAI-Main.unity
└── README.md                         ← Main project info
```

## 🔧 Unity Configuration Checklist

### Player Settings
- [x] Platform: Android
- [x] Package Name: `com.yourname.questaiapp`
- [x] Min API: 29, Target API: 34
- [x] Scripting Backend: IL2CPP
- [x] Architecture: ARM64 only
- [x] Graphics API: Vulkan or OpenGLES3

### XR Plug-in Management
- [x] Oculus: Enabled
- [x] Initialize XR on Startup
- [ ] Low Overhead Mode (MUST BE DISABLED)
- [ ] Occlusion (MUST BE DISABLED)

### Permissions
- [x] Camera
- [x] Internet
- [x] Custom: `horizonos.permission.HEADSET_CAMERA`

## 📊 Technical Specs

- **Resolution**: 1280×720 @ 30fps
- **Latency**: 150-200ms
- **Processing**: 3-10 seconds per transformation
- **Internet**: 8+ Mbps required
- **Battery**: ~2 hours continuous use

## 🎯 Pro Tips

### For Best Results
1. **Good Lighting**: Well-lit room, natural light preferred
2. **Strong WiFi**: 5GHz network, close to router
3. **Detailed Prompts**: 20-50 words with specifics
4. **Clean Lens**: Wipe Quest camera before use
5. **Wait Patiently**: Full 3-10 seconds for processing

### Writing Great Prompts
✅ **Good**: "Transform into cozy English cottage with stone fireplace, wooden beams, warm candlelight, vintage furniture, floral wallpaper"

❌ **Bad**: "Make it look different"

Include:
- Colors ("deep burgundy", "neon blue")
- Materials ("polished metal", "rough stone")
- Lighting ("soft afternoon sun")
- Atmosphere ("peaceful", "energetic")

## 🐛 Quick Troubleshooting

| Problem | Solution |
|---------|----------|
| App won't install | Check Developer Mode enabled, try different cable |
| Camera black screen | Grant camera permissions, restart app |
| No AI response | Check WiFi (8+ Mbps), wait 10 seconds |
| Low quality | Improve lighting, clean camera, simplify prompt |
| Menu not responding | Press Start to show/hide, check controller battery |

## 📚 Documentation

- **New to Unity?** → [COMPLETE_BEGINNERS_GUIDE.md](Documentation/COMPLETE_BEGINNERS_GUIDE.md)
- **Learn features?** → [FEATURES.md](Documentation/FEATURES.md)
- **Need help?** → Check troubleshooting in both guides

## 🔗 Important Links

- **Decart Discord**: https://discord.gg/decart
- **Decart Docs**: https://docs.platform.decart.ai
- **GitHub**: https://github.com/Jakubikk/joystick-nav
- **Meta Quest Support**: https://www.meta.com/help/quest

## ⚡ From Zero to Running (Absolute Beginner)

1. **Install**: Git, Unity Hub, Unity 6, SideQuest
2. **Setup Quest**: Enable Developer Mode in Meta Quest app
3. **Clone**: `git clone https://github.com/Jakubikk/joystick-nav.git`
4. **Open**: Unity Hub → Add → `DecartAI-Quest-Unity`
5. **Configure**: Follow EVERY step in [COMPLETE_BEGINNERS_GUIDE.md](Documentation/COMPLETE_BEGINNERS_GUIDE.md)
6. **Build**: File → Build Settings → Build
7. **Deploy**: SideQuest → Install APK
8. **Launch**: Quest → Apps → Unknown Sources → DecartXR

**Estimated Time**: 2-3 hours first time

## 📌 Key Scripts Reference

| Script | Purpose |
|--------|---------|
| `MenuManager.cs` | Main menu navigation and state |
| `TimeTravelSliderController.cs` | Year slider for time travel |
| `OptionListDisplay.cs` | List navigation (clothing, biomes, games) |
| `ControlsDisplay.cs` | Shows control hints |
| `MenuOptionDisplay.cs` | UI formatting helper |
| `WebRTCController.cs` | AI communication |

## 🎨 Features at a Glance

**Time Travel**: 400 years (1800-2200) × Era descriptions  
**Clothing**: 15 styles × Detailed prompts  
**Biomes**: 15 environments × Rich descriptions  
**Games**: 15 styles × Authentic aesthetics  
**Custom**: ∞ possibilities × Your imagination

**Total Transformations**: 45 pre-made + unlimited custom = Endless!

## 💡 Remember

- **No voice features**: Voice-to-text removed as requested
- **Joystick only**: Simple navigation scheme
- **5 menu options**: Time Travel, Clothing, Biomes, Games, Custom
- **All documented**: Every feature fully explained
- **Beginner-friendly**: Step-by-step for Unity newbies

## 🏁 Success Criteria

You're done when:
- [ ] App builds without errors
- [ ] Installs on Quest 3
- [ ] Menu shows and navigates
- [ ] All 4 features work
- [ ] Custom keyboard opens
- [ ] Transformations apply (3-10 sec)
- [ ] No crashes

---

**Version**: 1.0  
**Platform**: Meta Quest 3  
**Unity**: 6 (6000.0.34f1+)  
**AI**: Decart (Mirage + Lucy)  

**Made with ❤️ for the Quest community**

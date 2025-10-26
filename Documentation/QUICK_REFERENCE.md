# Quick Reference Guide - Quest 3 AI Transformation App

## Controls Summary

| Action | Button | Description |
|--------|--------|-------------|
| Navigate Up | Joystick Up | Move selection up in menus |
| Navigate Down | Joystick Down | Move selection down in menus |
| Navigate Left/Right | Joystick Left/Right | Adjust values (Time Travel feature) |
| Confirm / Apply | Right Trigger | Select menu item or apply transformation |
| Go Back | Left Trigger | Return to previous menu |
| Show/Hide Menu | Hamburger Button (Start) | Toggle menu visibility |

## Features Quick Access

### 1. Time Travel
**Path**: Main Menu → Time Travel  
**Controls**: Joystick Left/Right to change year, Right Trigger to apply  
**Range**: 1800 - 2100  

**Quick Periods**:
- 1800s: Victorian era
- 1950s: Mid-century modern
- 1980s: Retro tech
- 2024: Modern day
- 2050+: Futuristic

### 2. Virtual Clothing Try-On
**Path**: Main Menu → Virtual Clothing Try-On  
**Controls**: Joystick Up/Down to browse, Right Trigger to apply  

**Categories**:
- Formal wear (suits, gowns, tuxedos)
- Casual (jeans, t-shirts, dresses)
- Professional (doctor, chef, etc.)
- Cultural (kimono, sari, kilt)
- Costumes (superhero, knight, wizard)
- Sports uniforms
- Historical fashion

**Tip**: Stand in front of mirror for best results!

### 3. Biome Transformation
**Path**: Main Menu → Biome Transformation  
**Controls**: Joystick Up/Down to browse, Right Trigger to apply  

**Categories**:
- Natural Biomes (rainforest, arctic, desert)
- Cities (Tokyo, Paris, NYC, Venice, Dubai)
- Historical (Ancient Egypt, Medieval, Greece)
- Fantasy (Enchanted Forest, Crystal Cave)
- Seasonal (Winter, Spring, Summer, Autumn)
- Sci-Fi (Space Station, Alien Planet, Cyberpunk)

### 4. Video Game World
**Path**: Main Menu → Video Game World  
**Controls**: Joystick Up/Down to browse, Right Trigger to apply  

**Popular Styles**:
- Minecraft / LEGO (blocky voxel)
- Anime / Studio Ghibli
- Cyberpunk 2077 (neon dystopia)
- Zelda / World of Warcraft (fantasy)
- 8-bit / 16-bit (retro pixel art)
- GTA / Red Dead Redemption (realistic)
- Borderlands / Okami (artistic)

### 5. Custom Prompt
**Path**: Main Menu → Custom Prompt  
**Controls**: Touch input field with controller, type with Quest keyboard, Right Trigger to submit  

**Example Prompts**:
- "Transform into a magical enchanted forest with glowing mushrooms and fireflies"
- "Make everything look like it's made of colorful candy and sweets"
- "Turn this into an underwater coral reef with tropical fish"
- "Transform to ancient Roman architecture with marble columns"
- "Make it look like a cozy winter cabin with snow and fireplace"

**Tips for Good Prompts**:
- Be specific and detailed
- Describe colors, materials, textures
- Mention lighting and atmosphere
- 20-40 words works best

## Troubleshooting Quick Fixes

| Problem | Quick Fix |
|---------|-----------|
| Black screen | Check camera permissions in Quest Settings |
| No transformations | Check WiFi connection (need 8+ Mbps) |
| Laggy performance | Close other apps, move closer to router |
| Menu not responding | Check controller batteries, restart app |
| Build error in Unity | Delete Library folder, reopen project |

## Performance Tips

✅ **Do**:
- Use 5GHz WiFi
- Ensure good lighting
- Stand relatively still
- Wait 3-5 seconds for processing
- Use detailed prompts

❌ **Don't**:
- Use on slow internet (< 5 Mbps)
- Use in dim lighting
- Move around rapidly during processing
- Try to process multiple transformations simultaneously

## Technical Specifications

- **Platform**: Meta Quest 3 / Quest 3S
- **Unity Version**: 6000.0.34f1
- **Min Android API**: 29 (Android 10)
- **Target Android API**: 34 (Android 14)
- **Internet Required**: Yes, 8+ Mbps recommended
- **Storage**: ~150 MB
- **AI Processing**: Decart API (cloud-based)
- **Latency**: ~150-200ms typical

## File Locations (For Developers)

**Scripts**:
- `/Assets/Samples/DecartAI-Quest/Scripts/MenuManager.cs`
- `/Assets/Samples/DecartAI-Quest/Scripts/TimeTravelController.cs`
- `/Assets/Samples/DecartAI-Quest/Scripts/ClothingTryOnController.cs`
- `/Assets/Samples/DecartAI-Quest/Scripts/BiomeTransformController.cs`
- `/Assets/Samples/DecartAI-Quest/Scripts/GameWorldController.cs`
- `/Assets/Samples/DecartAI-Quest/Scripts/CustomPromptController.cs`

**Main Scene**:
- `/Assets/Samples/DecartAI-Quest/DecartAI-Main.unity`

**Documentation**:
- `/Documentation/COMPLETE_BEGINNERS_GUIDE.md`
- `/Documentation/QUICK_REFERENCE.md` (this file)

## Support & Resources

- **GitHub**: https://github.com/Jakubikk/joystick-nav
- **Decart API Docs**: https://docs.platform.decart.ai
- **Decart Discord**: https://discord.gg/decart
- **Meta Developer Portal**: https://developer.oculus.com

---

**Version**: 1.0.0  
**Last Updated**: 2025-10-26

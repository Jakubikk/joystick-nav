# New Features Documentation

This document describes the new features added to the Meta Quest 3 DecartAI application.

## Overview

The application now includes five main features accessible through an intuitive joystick-based menu system:

1. **Time Travel** - Experience your environment in different historical periods
2. **Virtual Try-On** - Try on various clothing and costumes virtually
3. **Biome/Country Transformation** - Transform your room to different locations worldwide
4. **Video Game Styles** - View your environment in popular video game aesthetics
5. **Custom Prompt** - Enter any custom transformation via Meta keyboard

## Navigation Controls

| Button | Function |
|--------|----------|
| **Left Trigger** | Go back to previous menu |
| **Right Trigger** | Confirm selection / Apply transformation |
| **Joystick Up/Down** | Navigate through menu options |
| **Joystick Left/Right** | Adjust sliders (Time Travel feature) |
| **Start Button (☰)** | Show/Hide the entire menu |
| **A Button** | Open keyboard (Custom Prompt feature) |

**No other buttons are bound** - the system is designed to be simple and intuitive.

## Feature Details

### 1. Time Travel

**Purpose**: Transform your environment to match different historical time periods.

**How to Use**:
1. Select "Time Travel" from the main menu
2. Use the joystick (left/right) or manually drag the slider to select a year (1500-2500)
3. The transformation automatically applies as you move the slider
4. Press left trigger to return to main menu

**Available Time Periods**:
- **Ancient Times**: Stone Age, Ancient Egypt, Ancient Rome
- **Medieval Era**: Medieval period with castles and torches
- **Renaissance**: 1400-1600 with ornate architecture
- **Baroque**: 1600-1750 with grand palaces
- **Victorian**: 1837-1901 with gas lamps and brick buildings
- **Roaring Twenties**: 1920s Art Deco style
- **1950s**: Mid-century modern aesthetics
- **1980s**: Neon colors and arcade vibes
- **Modern Day**: Contemporary 2000-2024
- **Near Future**: 2025-2050 with advanced technology
- **Cyberpunk Future**: 2050-2100 with neon signs
- **Space Age**: 2100-2200 with metallic surfaces
- **Far Future**: 2200-2500 with alien-like architecture

### 2. Virtual Try-On

**Purpose**: See yourself wearing different clothing and costumes in real-time.

**How to Use**:
1. Select "Virtual Try-On" from the main menu
2. Stand facing the camera (use it like a mirror)
3. Use joystick up/down to browse through clothing options
4. Press right trigger to apply the selected outfit
5. Press left trigger to go back

**Categories** (30+ options total):
- **Formal Wear**: Tuxedo, Evening Gown, Business Suit, Cocktail Dress
- **Traditional/Cultural**: Kimono, Sari, Hanbok, Kilt
- **Casual Wear**: Denim Jacket, Leather Jacket, Hoodie, Sundress
- **Sports/Athletic**: Soccer Jersey, Basketball Jersey, Yoga Outfit
- **Professional Uniforms**: Chef, Doctor, Police Officer, Pilot
- **Fantasy/Costume**: Medieval Knight Armor, Pirate, Superhero, Victorian Dress
- **Modern Fashion**: Streetwear, Punk Rock, Bohemian, Minimalist
- **Seasonal**: Winter Coat, Raincoat, Beach Wear, Ski Gear

### 3. Biome/Country Transformation

**Purpose**: Transform your room to look like it's in different countries or environmental biomes.

**How to Use**:
1. Select "Biome/Country" from the main menu
2. Use joystick up/down to browse locations
3. Press right trigger to apply the transformation
4. Press left trigger to return to main menu

**Natural Biomes**:
- Tropical Rainforest
- Desert Oasis
- Arctic Tundra
- Bamboo Forest
- Coral Reef (underwater)
- Mountain Peak
- Savanna
- Temperate Forest
- Cave System
- Volcanic Landscape

**Country/Cultural Locations** (25+ total):
- **Asia**: Japan (traditional), China, Bali Indonesia, Hong Kong, India, Tokyo Neon District
- **Europe**: Paris France, Santorini Greece, Iceland, Switzerland, Venice Italy, London Victorian
- **Middle East**: Morocco, Dubai UAE, Egypt (Ancient)
- **Americas**: Brazil, Mexico, New York, California
- **Oceania**: Australia Outback

**Fantasy/Special Locations**:
- Enchanted Forest
- Crystal Cave
- Floating Islands
- Alien Planet
- Steampunk City

**Seasonal Transformations**:
- Winter Wonderland
- Spring Garden
- Autumn Harvest
- Summer Beach

### 4. Video Game Styles

**Purpose**: View your environment transformed into the visual style of popular video games.

**How to Use**:
1. Select "Video Game Style" from the main menu
2. Use joystick up/down to browse game styles
3. Press right trigger to apply the selected style
4. Press left trigger to return to main menu

**Categories** (60+ game styles total):

**Popular Franchises**:
- Minecraft, LEGO, Grand Theft Auto, Portal, Fallout, Skyrim
- The Witcher, Zelda BOTW, Final Fantasy, Dark Souls

**Retro/Classic**:
- Super Mario, Sonic, 8-Bit Retro, 16-Bit RPG, Pac-Man

**FPS Games**:
- Call of Duty, Halo, Doom, Half-Life, Borderlands

**Horror Games**:
- Silent Hill, Resident Evil, Bioshock

**Open World**:
- Red Dead Redemption, Assassin's Creed, Cyberpunk 2077, Spider-Man PS4

**RPG/Fantasy**:
- World of Warcraft, Diablo, Dragon Age, Mass Effect

**Adventure/Puzzle**:
- The Last of Us, Uncharted, Journey, Monument Valley

**Cartoon/Stylized**:
- Fortnite, Overwatch, Team Fortress 2, Animal Crossing

**Racing Games**:
- Mario Kart, Gran Turismo, Rocket League

**Survival/Crafting**:
- Terraria, Stardew Valley, Subnautica

**Battle Royale**:
- PUBG, Apex Legends

**Strategy**:
- Civilization, StarCraft

**Indie Gems**:
- Celeste, Hollow Knight, Cuphead

### 5. Custom Prompt

**Purpose**: Enter any custom transformation prompt using the Meta Quest keyboard.

**How to Use**:
1. Select "Custom Prompt" from the main menu
2. Press **A button** to open the Meta Quest keyboard
3. Type your custom transformation prompt
4. Press right trigger (or click Submit) to apply
5. Press left trigger to return to main menu

**Examples of Custom Prompts**:
- "Transform this into a pirate ship on the high seas"
- "Make everything look like it's made of candy"
- "Turn my room into a spaceship cockpit"
- "Make it look like a medieval dragon's lair"
- "Transform into an underwater palace with mermaids"
- "Make everything look like a 1960s psychedelic poster"

**Tips for Best Results**:
- Be descriptive and specific
- Include visual details (colors, materials, atmosphere)
- Use 15-30 words for optimal results
- Reference the Decart documentation for prompt guidelines

## Technical Details

### Scripts

All feature controllers are located in:
```
Assets/Samples/DecartAI-Quest/Scripts/
```

- **MenuManager.cs** - Main menu navigation and state management
- **TimeTravelController.cs** - Time travel feature logic and prompts
- **VirtualTryOnController.cs** - Virtual try-on feature logic and prompts
- **BiomeController.cs** - Biome/country transformation logic and prompts
- **VideoGameController.cs** - Video game style logic and prompts
- **CustomPromptController.cs** - Custom text input handling

### Integration with Decart

All features use the existing WebRTCConnection system to send prompts to Decart AI:
- Each controller calls `webRtcConnection.SendCustomPrompt(prompt)`
- Prompts are specifically crafted for optimal Decart transformation results
- Uses the Mirage model for environment transformations
- Real-time processing with 150-200ms latency

### Menu System

The MenuManager handles:
- Showing/hiding UI panels based on current state
- Joystick navigation with cooldown to prevent rapid scrolling
- Visual feedback (highlighted menu items)
- Controller input mapping
- State transitions between menus

## Performance Considerations

- **Internet Required**: All transformations require active internet (8+ Mbps recommended)
- **WiFi Quality**: Use 5GHz WiFi for best results
- **Processing Time**: 
  - First transformation: 5-10 seconds
  - Subsequent transformations: 3-5 seconds
- **Frame Rate**: Maintains 25-30 FPS during transformation
- **Latency**: ~150-200ms end-to-end for real-time feel

## Troubleshooting

### Menu Not Responding
- Ensure controllers are properly paired
- Check controller batteries
- Press Start button (☰) to ensure menu is visible
- Restart the application

### Transformations Not Working
- Verify internet connection (Settings → WiFi)
- Check WiFi speed (need minimum 8 Mbps)
- Wait 10 seconds for first transformation
- Try again with different prompt
- Restart application if persistent

### Keyboard Not Opening (Custom Prompt)
- Press A button to activate
- Ensure you're in the Custom Prompt panel
- Check if Meta Quest keyboard is enabled in system settings
- Restart app if needed

### Controls Not Working
- Verify button mappings (see Navigation Controls above)
- Re-pair Quest controllers
- Check controller tracking
- Restart application

## Future Enhancements

Potential future additions:
- Save favorite transformations
- Quick access presets
- Voice control for all features (currently only available separately)
- Multiple simultaneous transformations
- Transformation history
- Social sharing of transformations
- Custom prompt suggestions

## Credits

- **Decart AI** - Real-time video transformation technology
- **Meta Quest 3** - XR platform
- **Unity 6** - Game engine
- **WebRTC** - Real-time communication

## Support

For issues or questions:
- Check the Complete Beginner's Guide in `/Documentation/`
- Visit Decart Discord: https://discord.gg/decart
- Report issues on GitHub repository
- Contact technical support: tom@decart.ai

---

**Version**: 1.0  
**Last Updated**: October 26, 2024  
**Compatible With**: Meta Quest 3, Horizon OS v74+, Unity 6

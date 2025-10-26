# DecartXR - Enhanced Features Documentation

This document describes the new features added to the DecartXR Quest 3 application.

## Overview

The DecartXR app has been enhanced with 5 major new features, each providing unique ways to transform your reality using AI:

1. **Time Travel** - View your environment across different time periods
2. **Virtual Dressing Room** - Try on different clothing styles
3. **Biome/Country Transformation** - Experience different locations and environments
4. **Video Game Styles** - See your world through various game aesthetics
5. **Custom Input** - Create your own transformation prompts

## New Features

### 1. Time Travel 🕰️

**File**: `TimeTravelController.cs`

Transform your environment to see how it might have looked in different historical eras or how it could look in the future.

**Key Features**:
- Year slider from 1000 to 3000
- 12 distinct time periods with appropriate transformations
- Real-time preview as you adjust the slider
- Smooth transitions between eras

**Time Periods**:
- Medieval Times (1000-1500)
- Renaissance Era (1500-1700)
- Colonial Period (1700-1850)
- Victorian Era (1850-1920)
- Early 20th Century (1920-1950)
- Mid-Century Modern (1950-1980)
- Late 20th Century (1980-2000)
- Present Day (2000-2030)
- Near Future (2030-2100)
- Advanced Future (2100-2200)
- Far Future (2200-2500)
- Distant Future (2500-3000)

**Usage**:
- Navigate to Time Travel from main menu
- Use joystick left/right to adjust the year slider
- Watch your environment transform in real-time
- Press left trigger to return to main menu

### 2. Virtual Dressing Room 👔

**File**: `ClothingController.cs`

Try on different clothing styles and outfits using AI-powered person transformation (requires Lucy model).

**Key Features**:
- 20 different clothing options
- Maintains your identity while changing outfit
- Real-time preview of clothing
- Browse through options easily

**Clothing Options**:
- Business Formal
- Casual Streetwear
- Athletic Sportswear
- Formal Evening Wear
- Traditional Kimono
- Medieval Knight
- Superhero Costume
- Pirate Outfit
- Doctor's Coat
- Astronaut Suit
- Victorian Era
- Summer Beach
- Winter Coat
- Chef Uniform
- Rock Star
- Cowboy Outfit
- Cyberpunk Style
- Fantasy Wizard
- 1920s Flapper
- Safari Explorer

**Usage**:
- Stand in front of the camera
- Select Virtual Dressing Room from main menu
- Use joystick up/down to browse options
- Press right trigger to try on selected outfit
- Wait 3-5 seconds for AI processing
- Press left trigger to return to main menu

### 3. Biome/Country Transformation 🌍

**File**: `BiomeController.cs`

Transform your environment to experience different geographic locations, natural biomes, or famous cities around the world.

**Key Features**:
- 20+ different locations and biomes
- Accurate environmental transformations
- Mix of natural biomes and cultural locations
- Immersive atmosphere for each location

**Locations Include**:

**Natural Biomes**:
- Tropical Rainforest
- Arctic Tundra
- Sahara Desert
- Amazon Jungle
- Coral Reef
- Mountain Peak
- Cherry Blossom Grove

**Cities & Countries**:
- Tokyo, Japan
- Paris, France
- Ancient Egypt
- New York City
- Venice, Italy
- Morocco Bazaar
- Iceland Landscape
- Greek Islands
- African Savanna
- London, England
- Australian Outback
- Swiss Alps
- Bali, Indonesia
- Dubai Luxury

**Usage**:
- Select Biome/Country from main menu
- Use joystick up/down to browse locations
- Press right trigger to apply transformation
- Experience your room as a different place
- Press left trigger to return to main menu

### 4. Video Game Styles 🎮

**File**: `GameStyleController.cs`

View your environment as if it were rendered in different video game art styles and aesthetics.

**Key Features**:
- 25 popular video game styles
- Accurate recreation of game aesthetics
- Wide variety from retro to modern games
- Immersive game-world feeling

**Game Styles Include**:
- Minecraft
- Legend of Zelda
- Grand Theft Auto
- Fortnite
- Cyberpunk 2077
- The Sims
- Super Mario
- Dark Souls
- Animal Crossing
- Halo
- Fallout
- The Witcher
- Final Fantasy
- Pokemon
- Borderlands
- Red Dead Redemption
- Skyrim
- Assassin's Creed
- Portal
- Overwatch
- Roblox
- Among Us
- Resident Evil
- Street Fighter
- LEGO Games

**Usage**:
- Select Video Game Styles from main menu
- Use joystick up/down to browse game styles
- Press right trigger to apply selected style
- See your world transformed into game graphics
- Press left trigger to return to main menu

### 5. Custom Input ⌨️

**File**: `CustomInputController.cs`

Create your own custom transformation prompts using Meta Quest's virtual keyboard.

**Key Features**:
- Full keyboard support via Meta Quest system keyboard
- Unlimited creative possibilities
- Direct prompt entry
- Real-time preview of typed text

**Usage**:
- Select Custom Input from main menu
- Press right trigger to open virtual keyboard
- Type your creative prompt (e.g., "Transform into a magical fairy tale forest")
- Press Enter or A button to submit
- Watch your custom transformation come to life
- Press left trigger to return to main menu

## Navigation System

All features use a consistent navigation scheme:

### Controller Mapping

- **Left Joystick Up**: Navigate up in menus
- **Left Joystick Down**: Navigate down in menus
- **Left Joystick Left/Right**: Adjust slider (Time Travel)
- **Right Trigger**: Confirm/Select option
- **Left Trigger**: Go back to previous menu
- **Start Button (≡)**: Show/Hide menu system
- **A Button (One)**: Alternative confirm (some contexts)
- **B Button (Two)**: Alternative back (some contexts)

### Menu Flow

```
Main Menu
├── Time Travel
│   └── Year Slider (1000-3000)
├── Virtual Dressing Room
│   └── 20 Clothing Options
├── Biome/Country
│   └── 20+ Locations
├── Video Game Styles
│   └── 25 Game Styles
└── Custom Input
    └── Keyboard Input
```

## Technical Implementation

### Architecture

The new feature system is built on top of the existing DecartXR foundation:

1. **MenuManager.cs** - Handles main menu navigation and state management
2. **FeatureIntegrationController.cs** - Coordinates all feature controllers with WebRTC
3. **Individual Feature Controllers** - Each feature has its own controller
4. **WebRTCConnection** - Existing component for Decart AI communication

### Integration with Decart AI

All features use the existing Decart AI models:
- **Mirage Model**: For environment/world transformations (Time Travel, Biome, Game Styles)
- **Lucy Model**: For person transformations (Virtual Dressing Room)
- **Custom Prompts**: Routed through the same WebRTC connection

### Prompt Engineering

Each feature includes carefully crafted prompts designed to:
- Provide detailed visual descriptions
- Include relevant textures, materials, and atmosphere
- Maintain temporal consistency
- Work effectively with Decart's AI models

## File Structure

```
Assets/Samples/DecartAI-Quest/Scripts/
├── MenuManager.cs                      # Main menu navigation
├── TimeTravelController.cs            # Time travel feature
├── ClothingController.cs              # Virtual dressing room
├── BiomeController.cs                 # Biome/country transformation
├── GameStyleController.cs             # Video game styles
├── CustomInputController.cs           # Custom text input
├── FeatureIntegrationController.cs    # Feature integration
└── WebRTCController.cs                # Original WebRTC controller
```

## Setup Instructions

### For Developers

1. **Add Controllers to Scene**:
   - Create empty GameObjects for each controller
   - Add the corresponding script component
   - Assign UI references in the Inspector

2. **Create UI Panels**:
   - Create Canvas UI panels for each feature
   - Add TextMeshPro components for labels
   - Add Slider component for Time Travel
   - Add InputField component for Custom Input

3. **Wire Up References**:
   - Assign UI elements to controller public fields
   - Link WebRTCConnection reference
   - Configure colors and settings

4. **Build and Deploy**:
   - Follow the complete beginner guide in `documentation/COMPLETE_BEGINNER_GUIDE.md`
   - Build for Android/Quest 3
   - Deploy via SideQuest or ADB

### UI Design Guidelines

For best VR experience:
- Use large, readable fonts (32pt+)
- High contrast colors for visibility
- Minimum button size: 100x100 pixels
- Space elements for VR pointer precision
- Use world-space canvas for 3D positioning

## Performance Considerations

### Optimization Tips

1. **Prompt Sending**:
   - Cooldown timers prevent flooding the AI service
   - Prompts are queued to avoid conflicts
   - Only send when values actually change

2. **Memory Management**:
   - Controllers are lightweight
   - No heavy asset loading
   - Minimal overhead on existing system

3. **Network Usage**:
   - Prompts are text-based (minimal bandwidth)
   - AI processing happens server-side
   - Video stream is handled by existing WebRTC

## Customization

### Adding New Options

To add new clothing, biomes, or game styles:

1. Open the relevant controller file
2. Find the `InitializeOptions()` method
3. Add new options to the list:

```csharp
new ClothingOption 
{ 
    name = "Your Option Name", 
    prompt = "Detailed description for AI transformation..."
}
```

### Modifying Time Periods

Edit `GenerateTimeTravelPrompt()` in `TimeTravelController.cs`:

```csharp
else if (year < YOUR_YEAR)
{
    return "Your detailed time period prompt...";
}
```

### Changing Navigation Colors

Update in Inspector or modify defaults:
```csharp
[SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.8f);
[SerializeField] private Color selectedColor = new Color(0f, 1f, 1f, 1f);
```

## Troubleshooting

### Common Issues

**Menu not responding to joystick**:
- Check OVRInput is properly configured
- Verify controllers are paired
- Check for conflicting input handlers

**Prompts not transforming view**:
- Verify WebRTC connection is established
- Check internet connection (8+ Mbps)
- Wait full 3-5 seconds for processing
- Check Decart AI service status

**Custom keyboard not appearing**:
- Ensure TMP_InputField is assigned
- Check Quest keyboard settings
- Verify input field is set to activate

**Transformations look incorrect**:
- Ensure good lighting
- Try more detailed prompts
- Check camera positioning
- Verify stable internet connection

## Future Enhancements

Potential additions:
- Saved favorites for quick access
- History of recent transformations
- Preset combinations
- Social sharing features
- More granular controls
- Animation transitions between states

## Credits

Built on the DecartXR foundation:
- Decart AI Platform - AI transformation service
- Meta Quest SDK - VR framework
- Unity WebRTC - Real-time communication
- TextMeshPro - UI text rendering

## Support

For help with the new features:
- Discord: https://discord.gg/decart
- Email: tom@decart.ai
- Documentation: `/documentation/COMPLETE_BEGINNER_GUIDE.md`
- GitHub Issues: https://github.com/Jakubikk/joystick-nav/issues

---

**Version**: 2.0
**Last Updated**: 2025
**Compatible with**: Unity 6 (6000.0.34f1+), Quest 3/3S, Horizon OS v74+

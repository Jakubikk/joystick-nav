# Menu System for Decart Quest 3

This folder contains the complete menu system implementation for the Decart Quest 3 VR application.

## Features

The menu system provides 5 main features:

### 1. **Time Travel** (`TimeTravelFeature.cs`)
- View your environment in different historical time periods
- Year range: 1800-2100
- Slider-based selection
- 17 distinct time periods with accurate prompts
- Covers eras from Colonial 1800s to Distant Future 2100s

### 2. **Virtual Try-On** (`VirtualTryOnFeature.cs`)
- Try on different clothing and outfits
- 30+ clothing options
- Categories: Professional, Casual, Historical, Fantasy, Cultural
- Stand in front of mirror for best results
- Uses Decart Lucy model for person transformation

### 3. **Biome Transform** (`BiomeTransformFeature.cs`)
- Transform your room to different countries or environments
- 40+ biomes and locations
- Includes: Countries, Natural biomes, Fantasy environments
- Examples: Japan Cherry Blossoms, Tropical Rainforest, Cyberpunk City

### 4. **Video Game Style** (`VideoGameStyleFeature.cs`)
- View environment as if it was any video game
- 60+ game styles
- Categories: Classic/Retro, RPG, Shooters, Open World, Indie
- Examples: Minecraft, Zelda, Cyberpunk 2077, Among Us

### 5. **Custom Prompt** (`CustomPromptFeature.cs`)
- Type custom transformation prompts
- Uses Meta's built-in VR keyboard
- No voice-to-text (keyboard only as per requirements)
- 10 preset examples for quick access
- Fully customizable transformations

## Navigation Scheme

As per the requirements, the navigation is strictly controlled:

- **Joystick Up/Down**: Navigate through menu options
- **Right Trigger**: Confirm selection / Apply transformation
- **Left Trigger**: Go back to previous menu
- **Hamburger Button (Start)**: Show/Hide menu
- **No other buttons are bound**

## Architecture

### Core Components

1. **MenuManager.cs**
   - Main controller for the menu system
   - Handles navigation and feature switching
   - Manages menu visibility
   - Coordinates between all features

2. **Feature Scripts**
   - Each feature is a self-contained component
   - Activates/Deactivates when selected
   - Manages its own UI and state
   - Communicates with WebRTC for AI transformations

3. **WebRTC Integration**
   - All features use the existing `WebRTCConnection` component
   - Custom prompts are sent via `SendCustomPrompt()` method
   - Real-time AI transformation with sub-200ms latency

### UI Structure

```
MenuCanvas (World Space)
├── MenuPanel (Main Menu)
│   ├── TitleText
│   ├── MenuItemsContainer
│   └── MenuItem (Prefab instances)
├── TimeTravelPanel
│   ├── TitleText
│   ├── InstructionsText
│   ├── YearSlider
│   └── Content
├── VirtualTryOnPanel
│   ├── TitleText
│   ├── CurrentItemText
│   ├── InstructionsText
│   └── ContentArea (Scrollable list)
├── BiomeTransformPanel
│   └── (Same structure as VirtualTryOn)
├── VideoGameStylePanel
│   └── (Same structure as VirtualTryOn)
└── CustomPromptPanel
    ├── PromptInputField
    ├── ApplyButton
    ├── ClearButton
    └── PresetList
```

## Files

### Core Scripts
- `MenuManager.cs` - Main menu controller
- `TimeTravelFeature.cs` - Time travel functionality
- `VirtualTryOnFeature.cs` - Clothing try-on
- `BiomeTransformFeature.cs` - Environment transformation
- `VideoGameStyleFeature.cs` - Game style application
- `CustomPromptFeature.cs` - Custom prompt input

### Editor Tools
- `Editor/MenuUISetup.cs` - Automated UI creation tool

### Prefabs (Created by MenuUISetup)
- `MenuItem.prefab` - Main menu item template
- `ListItem.prefab` - List item for scrollable lists

## Setup

See `Documentation/UNITY_SCENE_SETUP.md` for complete setup instructions.

Quick setup:
1. Open DecartAI-Main scene in Unity
2. Go to **Tools** → **Decart** → **Setup Menu UI**
3. Click **Create Menu UI**
4. Add MenuManager component and connect references
5. Add each feature component and connect their references
6. Test and build

## Usage

### For Users

1. Launch the app on Meta Quest 3
2. Main menu appears automatically
3. Use joystick up/down to navigate
4. Right trigger to select a feature
5. Each feature has its own sub-menu with options
6. Right trigger to apply transformations
7. Left trigger to go back
8. Hamburger button to show/hide menu

### For Developers

**Adding New Options:**

To add more time periods, biomes, games, or clothing:

1. Open the relevant feature script
2. Find the Dictionary (e.g., `gameStyleOptions`)
3. Add a new entry:
```csharp
{ "Display Name", "Detailed Decart AI prompt describing the transformation" }
```
4. Save and Unity will auto-compile

**Customizing Colors:**

In MenuManager Inspector:
- `normalColor` - Unselected item color
- `selectedColor` - Selected item highlight color

**Adjusting Navigation:**

Each feature has:
- `navigationCooldown` - Delay between navigation inputs
- Individual feature settings (e.g., slider speed)

## Technical Details

### Decart AI Integration

All features send prompts to Decart's real-time AI service:

- **Time Travel & Biomes**: Use Mirage model (environment transformation)
- **Virtual Try-On**: Uses Lucy model (person transformation)
- **Video Games**: Uses Mirage model (stylized transformation)
- **Custom Prompts**: User chooses the type

### Performance

- Menu navigation: Instant response
- AI transformation: 3-5 seconds initial processing
- Real-time updates: 25-30 FPS after initial connection
- Latency: Sub-200ms once connected

### VR Considerations

- World Space Canvas for comfortable viewing
- Large text for readability (48-72pt)
- High contrast colors
- Simple navigation (joystick + triggers only)
- Position: 2 meters in front, 1.5 meters height

## Prompts

All prompts are carefully crafted for optimal Decart AI results:

### Prompt Structure
```
"Transform to [style/era/location] with [specific details], [atmosphere], [visual elements]"
```

### Examples

**Good Prompt:**
```
"Transform to 1920s Art Deco style with jazz age aesthetic, flapper fashion, 
vintage cars, elegant geometric designs, and golden era glamour"
```

**Bad Prompt:**
```
"Make it look like the 1920s"
```

The detailed prompts ensure:
- Consistent transformations
- Better visual quality
- Clear artistic direction
- Recognizable results

## No Voice-to-Text

As per requirements, this implementation **does not** use voice-to-text:
- Voice components are disabled
- Custom prompts use keyboard only
- Meta's built-in VR keyboard for text input
- No microphone permissions required

## Troubleshooting

**Menu not visible:**
- Check MenuCanvas position and scale
- Verify Event Camera is set

**Features not working:**
- Ensure WebRTCConnection is assigned
- Check internet connection
- Verify Decart API accessibility

**Navigation issues:**
- Check OVR Input is initialized
- Verify button mappings
- Test in Unity Editor first

## Future Enhancements

Potential additions (not in current scope):
- Save favorite transformations
- History of applied prompts
- User-created preset libraries
- Thumbnail previews
- Voice control (optional toggle)
- Multi-language support

## Credits

- Menu System: Developed for Decart Quest 3
- AI Technology: Decart AI Platform
- VR Platform: Meta Quest 3
- WebRTC Integration: SimpleWebRTC package
- Camera Access: QuestCameraKit

## License

See project root LICENSE file for details.

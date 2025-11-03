# Feature Implementation Guide

This document explains the technical implementation of each feature in the Quest 3 AI Transformation App.

## Architecture Overview

The app uses a modular architecture with the following components:

```
MenuManager (Main navigation controller)
    ├── TimeTravelController (Year-based transformations)
    ├── ClothingTryOnController (Clothing transformations)
    ├── BiomeTransformController (Environment transformations)
    ├── GameWorldController (Game aesthetic transformations)
    └── CustomPromptController (User-defined transformations)

WebRTCController (Camera and streaming management)
    └── WebRTCConnection (Decart API integration)
```

## Menu System

### MenuManager.cs

**Purpose**: Central navigation hub for all features

**Key Responsibilities**:
- Display main menu with 5 feature options
- Handle joystick-based navigation (up/down)
- Manage menu visibility (hamburger button toggle)
- Route to feature-specific controllers
- Handle back navigation (left trigger)

**Input Mapping**:
- `OVRInput.Axis2D.PrimaryThumbstick` - Up/Down navigation
- `OVRInput.Button.SecondaryIndexTrigger` - Confirm selection (Right Trigger)
- `OVRInput.Button.PrimaryIndexTrigger` - Go back (Left Trigger)
- `OVRInput.Button.Start` - Toggle menu visibility (Hamburger)

**State Machine**:
```
MainMenu → [Select Feature] → Feature Panel
    ↑                              ↓
    └──────── [Back Button] ───────┘
```

## Feature Controllers

### 1. TimeTravelController.cs

**Purpose**: Transform environment based on historical time periods

**Implementation**:
- Year range: 1800-2100
- Slider-based selection using joystick left/right
- Predefined prompt templates for different eras
- Uses Mirage model for environment transformation

**Prompt Generation**:
```csharp
private string GenerateTimePeriodPrompt(int year)
{
    // Returns era-specific transformation prompt
    // Examples:
    // 2050+: Futuristic with holograms, flying vehicles
    // 1980s: Retro tech with CRT TVs, cassettes
    // 1850s: Victorian era with oil lamps, antiques
}
```

**Era Definitions**:
- 2050+: Futuristic sci-fi
- 2000-2049: Modern contemporary
- 1980-1999: Retro 80s/90s
- 1950-1979: Mid-century modern
- 1920-1949: Early 20th century
- 1850-1919: Victorian era
- 1800-1849: Historical 1800s

### 2. ClothingTryOnController.cs

**Purpose**: Virtual try-on for different clothing styles

**Implementation**:
- 20+ clothing options across multiple categories
- Scrollable list with joystick navigation
- Uses Lucy model for person transformation (better for clothing)
- Detailed prompts for high-quality results

**Categories**:
1. Formal Wear (suits, gowns, tuxedos)
2. Casual Wear (jeans, t-shirts, dresses)
3. Professional (medical scrubs, chef uniforms)
4. Cultural & Traditional (kimono, sari, kilt)
5. Costumes & Fantasy (superhero, knight, wizard)
6. Sports Uniforms (basketball, soccer)
7. Historical Fashion (1920s flapper, Victorian)
8. Modern Fashion (leather jacket, bohemian)

**Prompt Structure**:
```
"Change the person's outfit to [description], 
[material details], [fit details], [color details], 
[additional features], [style descriptor]"
```

**Example**:
```
"Change the person's outfit to a tailored business suit, 
charcoal gray wool, slim fit blazer, matching dress pants, 
crisp white dress shirt, silk tie, polished leather shoes, 
professional appearance"
```

### 3. BiomeTransformController.cs

**Purpose**: Transform environment to different locations or natural biomes

**Implementation**:
- 24+ biome/location options
- Categorized for easy browsing
- Uses Mirage model for full environment transformation
- Rich descriptive prompts for immersive results

**Categories**:
1. Natural Biomes (rainforest, arctic, desert, forest)
2. World Cities (Tokyo, Paris, NYC, Venice, Dubai)
3. Historical & Cultural (Egypt, medieval, Greece, Wild West)
4. Fantasy & Mystical (enchanted forest, crystal cave)
5. Seasonal (winter, spring, summer, autumn)
6. Space & Sci-Fi (space station, alien planet, cyberpunk)

**Prompt Structure**:
```
"Transform to [location/biome] environment, 
[key visual elements], [atmospheric details], 
[color palette], [lighting], [mood/feeling]"
```

**Example**:
```
"Transform to tropical rainforest environment, 
lush green vegetation, tall palm trees, exotic plants, 
humid atmosphere, vibrant flowers, jungle vines, 
colorful birds, tropical paradise"
```

### 4. GameWorldController.cs

**Purpose**: Apply video game visual aesthetics to environment

**Implementation**:
- 30+ game style options
- Categorized by game genre/type
- Uses Mirage model for stylistic transformation
- Captures distinctive visual styles of popular games

**Categories**:
1. Blocky/Voxel (Minecraft, LEGO, Terraria)
2. Anime & Cel-Shaded (Anime, Ghibli, comic book)
3. Cyberpunk & Futuristic (Cyberpunk 2077, Deus Ex, Tron)
4. Fantasy & RPG (WoW, Zelda, Dark Souls, Skyrim)
5. Retro & Pixel Art (8-bit, 16-bit, modern pixel art)
6. Realistic & Simulation (GTA, The Sims, Red Dead)
7. Horror & Atmospheric (Silent Hill, Resident Evil, Limbo)
8. Sci-Fi Shooters (Halo, Destiny, Half-Life)
9. Artistic & Unique (Journey, Okami, Mirror's Edge, Borderlands)

**Prompt Structure**:
```
"Transform to [game name] style, 
[visual characteristics], [color treatment], 
[distinctive features], [aesthetic description]"
```

**Example**:
```
"Transform to Minecraft game style, 
blocky voxel world, pixelated textures, cube-shaped objects, 
block-based terrain, distinctive Minecraft aesthetic, 
low-poly geometric shapes"
```

### 5. CustomPromptController.cs

**Purpose**: Allow users to create custom transformations via text input

**Implementation**:
- TMP_InputField for text entry
- Integrates with Meta Quest keyboard
- Recent prompts history (last 5)
- Direct submission to Decart API

**Features**:
- Touch input field to trigger Quest keyboard
- Submit with Right Trigger or A button
- Clear with B button
- Reuse recent prompts with click
- Status feedback for user

**Keyboard Integration**:
```csharp
promptInputField.shouldHideMobileInput = false;
promptInputField.onSelect.AddListener(OnInputFieldSelected);
```

This triggers the native Meta Quest keyboard interface.

## Integration with Decart API

### WebRTC Connection

All feature controllers use the same WebRTC connection for AI processing:

```csharp
[SerializeField] private WebRTCConnection webRtcConnection;

// In feature controller:
webRtcConnection.SendCustomPrompt(prompt);
```

### Model Selection

The app uses two Decart models:

**Mirage Model** (Environment transformations):
- Used by: TimeTravelController, BiomeTransformController, GameWorldController
- Best for: Full scene transformations, environments, worlds
- WebSocket: `wss://api3.decart.ai/v1/stream-trial?model=mirage`

**Lucy Model** (Person transformations):
- Used by: ClothingTryOnController
- Best for: People, clothing, character modifications
- WebSocket: `wss://api3.decart.ai/v1/stream-trial?model=lucy_v2v_720p_rt`

### Prompt Transmission

Prompts are sent via WebRTC data channel:

```csharp
public void SendCustomPrompt(string customPrompt) {
    if (webRTCManager != null) {
        webRTCManager.SendCustomPrompt(customPrompt);
    }
}
```

The WebRTC manager handles:
1. WebSocket signaling
2. VP8 video encoding
3. Prompt transmission
4. Receiving transformed video stream

## UI Design Principles

### Navigation Consistency
- All menus use same navigation pattern
- Joystick up/down for all lists
- Right trigger always confirms
- Left trigger always goes back
- Hamburger always shows/hide menu

### Visual Feedback
- Selected items highlighted in yellow/cyan/green
- Font size increase for selected items
- Status text shows current action
- Descriptive text explains selections

### VR Comfort
- Text size: 22-28pt for readability
- High contrast colors (white on dark)
- Cooldown timers prevent accidental double-inputs
- Menu can be hidden for unobstructed view

## Performance Considerations

### Cooldown Timers
All controllers implement joystick cooldown:
```csharp
private float joystickCooldown = 0f;
private const float JOYSTICK_COOLDOWN_TIME = 0.3f;

if (joystickCooldown > 0) {
    joystickCooldown -= Time.deltaTime;
}
```

This prevents rapid scrolling and accidental inputs.

### Memory Management
- UI elements destroyed and recreated on menu changes
- Recent prompts limited to 5 items
- Prefab instantiation for dynamic content

### Network Optimization
- Single WebRTC connection shared by all features
- Prompts sent only on user confirmation
- Video stream maintained throughout session

## Testing Recommendations

### Unit Testing
Each controller should be tested for:
- ✅ Proper initialization
- ✅ Input handling (joystick, triggers)
- ✅ Menu navigation (up, down, back)
- ✅ Prompt generation accuracy
- ✅ WebRTC connection handling

### Integration Testing
- ✅ Menu transitions between all features
- ✅ Back navigation from each feature
- ✅ Menu show/hide functionality
- ✅ Concurrent use of different features
- ✅ WebRTC connection stability

### User Testing
- ✅ Navigation feels intuitive
- ✅ Text is readable in VR
- ✅ Prompts produce expected results
- ✅ Response times are acceptable
- ✅ No motion sickness from UI

## Future Enhancement Ideas

### Additional Features
- **Favorites System**: Save favorite transformations
- **Presets**: Quick-access to popular combinations
- **Intensity Slider**: Control transformation strength
- **Before/After Toggle**: Compare original vs transformed
- **Screenshot Capture**: Save transformation results

### UI Improvements
- **Search Function**: Find specific transformations
- **Categories View**: Filter by category
- **Grid View**: Visual preview of options
- **Tutorial System**: First-time user guidance
- **Settings Menu**: Customize experience

### Technical Enhancements
- **Local Caching**: Cache recent transformations
- **Offline Mode**: Pre-downloaded transformations
- **Multi-user**: Share transformations with friends
- **Voice Commands**: Navigate via voice (already has voice for prompts)
- **Gesture Controls**: Alternative to joystick

## Troubleshooting Guide for Developers

### Common Issues

**Controller not responding**:
- Check OVRInput button mapping
- Verify controller batteries
- Test in Unity Play mode first

**Prompts not sending**:
- Verify WebRTCConnection reference
- Check WebSocket connection status
- Look for errors in Console

**UI not visible**:
- Check Canvas render mode (World Space)
- Verify camera distance
- Check Canvas layer and rendering

**Menu navigation broken**:
- Verify joystick deadzone (> 0.5)
- Check cooldown timer not too long
- Ensure Update() is being called

## Code Style Guidelines

### Naming Conventions
- Controllers: `[Feature]Controller.cs`
- Private fields: `camelCase` with underscore prefix for truly private
- Public fields: `PascalCase`
- Methods: `PascalCase`
- Constants: `UPPER_SNAKE_CASE`

### Documentation
- Use XML comments for public methods
- Add `[Header]` attributes for Unity Inspector organization
- Include `[Tooltip]` for designer-facing fields
- Document non-obvious logic with inline comments

### Unity Best Practices
- Use `[SerializeField]` instead of public for Unity references
- Cache component references in OnEnable/Start
- Clean up listeners in OnDisable/OnDestroy
- Use object pooling for frequently instantiated items

---

**For More Information**:
- See `/Documentation/COMPLETE_BEGINNERS_GUIDE.md` for user documentation
- See `/Documentation/QUICK_REFERENCE.md` for quick command reference
- Visit Decart API docs: https://docs.platform.decart.ai

**Last Updated**: 2025-10-26  
**Version**: 1.0.0

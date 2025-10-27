# Quest 3 AI Transformation App - Features Overview

## Navigation Scheme

This app uses a consistent navigation scheme across all features:

### Controller Mapping
- **Left Trigger (Index Trigger)**: Go back / Return to previous menu
- **Right Trigger (Hand Trigger)**: Confirm selection / Apply transformation
- **Joystick Up/Down**: Navigate through menu options
- **Hamburger Button (Start Button)**: Hide or show the main menu
- **Y Button**: Open Meta keyboard (in Custom Prompt feature)
- **X Button**: Clear text (in Custom Prompt feature)

**No other buttons are bound to any functions.**

---

## Main Features

### 1. Time Travel 🕰️

**Description**: Transform your environment to see how it would look in different historical periods or future years.

**How to Use**:
1. Select "Time Travel" from the main menu
2. Use joystick left/right or the slider to select a year (1800-2200)
3. Press right trigger to apply the transformation

**Year Ranges**:
- **1800-1850**: Early Industrial Era
- **1850-1900**: Victorian Era
- **1900-1920**: Edwardian Era
- **1920-1950**: Early 20th Century
- **1950-1980**: Mid 20th Century
- **1980-2000**: Late 20th Century
- **2000-2030**: Modern Era
- **2030-2100**: Near Future
- **2100-2200**: Distant Future

**Technical Details**:
- Uses Decart AI's Mirage model for environment transformation
- Generates detailed prompts including architecture style, technology level, and atmosphere
- Maintains room layout while changing materials and styling
- Real-time transformation at ~150-200ms latency

---

### 2. Virtual Try-On 👔

**Description**: Stand in front of a mirror and try on different types of clothing using AI.

**How to Use**:
1. Stand in front of a real mirror
2. Select "Virtual Try-On" from the main menu
3. Browse clothing options using joystick up/down
4. Press right trigger to try on the selected outfit

**Available Clothing**:
1. **Business Suit** - Professional charcoal gray suit
2. **Casual Wear** - Comfortable jeans and t-shirt
3. **Formal Dress** - Elegant evening gown
4. **Athletic Wear** - Sports and fitness clothing
5. **Traditional Kimono** - Japanese silk kimono
6. **Medieval Armor** - Full knight's armor
7. **Wizard Robes** - Mystical purple robes
8. **Superhero Suit** - Comic book hero costume
9. **Cowboy Outfit** - Wild West attire
10. **Astronaut Suit** - NASA space gear
11. **Victorian Gown** - 19th century formal wear
12. **Cyberpunk Outfit** - Futuristic street wear
13. **Chef Uniform** - Professional culinary attire
14. **Pirate Costume** - Swashbuckling buccaneer
15. **Beach Wear** - Summer vacation clothing

**Technical Details**:
- Uses Decart AI's Lucy model for person transformation
- Preserves your identity and facial features
- Changes clothing while maintaining body pose
- Works best with mirrors for seeing yourself

---

### 3. Biome Transformation 🌍

**Description**: Transform your room to look like it's in different countries, environments, or biomes.

**How to Use**:
1. Select "Biome Transformation" from the main menu
2. Browse environment options using joystick up/down
3. Press right trigger to transform your room

**Available Biomes** (18 total):
1. **Japanese Garden** - Zen garden with cherry blossoms
2. **Tropical Paradise** - Exotic island with palm trees
3. **Arctic Tundra** - Frozen northern wilderness
4. **Desert Oasis** - Middle Eastern desert setting
5. **English Countryside** - Classic British pastoral scene
6. **Amazon Rainforest** - Dense tropical jungle
7. **Alpine Mountain Lodge** - Swiss mountain retreat
8. **Mediterranean Villa** - Coastal Italian setting
9. **African Savanna** - Wide open grasslands
10. **Scandinavian Interior** - Modern Nordic design
11. **Moroccan Riad** - North African courtyard
12. **New York Loft** - Urban industrial space
13. **Balinese Temple** - Indonesian sacred space
14. **Icelandic Geothermal** - Volcanic landscape
15. **French Château** - Elegant palace interior
16. **Australian Outback** - Red desert landscape
17. **Underwater Reef** - Ocean floor environment
18. **Space Station** - Futuristic orbital habitat

**Technical Details**:
- Uses Decart AI's Mirage model for environment transformation
- Comprehensive prompt engineering for each biome
- Maintains room structure while changing aesthetic
- Rich descriptions of materials, colors, and atmosphere

---

### 4. Video Game Style 🎮

**Description**: View your environment as if it was from any popular video game.

**How to Use**:
1. Select "Video Game Style" from the main menu
2. Browse game style options using joystick up/down
3. Press right trigger to apply the game aesthetic

**Available Game Styles** (20 total):
1. **Minecraft** - Blocky voxel world
2. **The Legend of Zelda** - Cel-shaded adventure
3. **Cyberpunk 2077** - Futuristic neon city
4. **Animal Crossing** - Cute cartoon island
5. **Dark Souls** - Gothic dark fantasy
6. **Borderlands** - Cell-shaded comic book
7. **Portal** - Sleek test chamber
8. **Fallout** - Post-apocalyptic wasteland
9. **Super Mario** - Colorful platform world
10. **Grand Theft Auto** - Urban open world
11. **Stardew Valley** - Pixel art farm
12. **Fortnite** - Vibrant battle royale
13. **Halo** - Sci-fi military installation
14. **Journey** - Artistic desert expanse
15. **Bioshock** - Art Deco underwater city
16. **Doom** - Industrial hellscape
17. **Overwatch** - Bright hero shooter
18. **Resident Evil** - Survival horror mansion
19. **The Sims** - Everyday life simulation
20. **Red Dead Redemption** - Wild West frontier

**Technical Details**:
- Uses Decart AI's Mirage model for artistic style transformation
- Each game has carefully crafted prompts matching visual style
- Includes rendering techniques, color palettes, and aesthetic details
- Supports wide range from pixel art to photorealistic styles

---

### 5. Custom Prompt ✍️

**Description**: Type your own custom transformation prompt using Meta's built-in keyboard.

**How to Use**:
1. Select "Custom Prompt" from the main menu
2. Press **Y button** or tap the input field to open Meta keyboard
3. Type your custom transformation idea
4. Press right trigger to apply
5. Press **X button** to clear the prompt

**Example Prompts**:
- "Transform into a magical fairy tale forest with glowing mushrooms"
- "Make everything look like it's made of candy and sweets"
- "Transform the room into a futuristic holographic command center"
- "Make it look like an ancient Egyptian temple with hieroglyphics"
- "Transform into a cozy hobbit hole with round doors and warm lighting"
- "Make everything appear as if underwater in a coral reef"
- "Transform the space into a steampunk workshop with brass gears"
- "Make it look like a medieval castle throne room"
- "Transform into a neon-lit cyberpunk alleyway"
- "Make everything look like a watercolor painting"

**Tips for Best Results**:
- Be specific and descriptive
- Include materials, colors, and atmosphere
- Mention lighting and textures
- Describe the overall mood or theme
- Use 20-30 words for optimal results

**Technical Details**:
- Uses Meta Quest's system keyboard for input
- Supports both Mirage and Lucy models
- Direct integration with Decart AI's prompt system
- No voice-to-text (keyboard input only as requested)

---

## Menu System

### Main Menu
The main menu provides access to all five features:

**Design**:
- Clean, modern interface
- Clear feature descriptions
- Visual selection indicators
- Easy navigation with joystick

**Navigation**:
- Joystick up/down to browse
- Right trigger to select
- Hamburger button to show/hide menu
- Left trigger to go back (when in feature)

### Feature Panels
Each feature has its own dedicated panel with:
- Clear instructions
- Options or controls specific to that feature
- Visual feedback for selections
- Consistent navigation scheme

---

## Technical Architecture

### AI Models Used

**Mirage Model** (for environments):
- Time Travel
- Biome Transformation
- Video Game Style
- Custom Prompt (environment transformations)

**Lucy Model** (for people):
- Virtual Try-On
- Custom Prompt (person transformations)

### Performance

**Specifications**:
- Resolution: 1280×720 @ 30fps
- Latency: ~150-200ms end-to-end
- Codec: VP8 with adaptive bitrate (1-4 Mbps)
- Processing: Real-time AI transformation via WebRTC

**Requirements**:
- Meta Quest 3 with Horizon OS v74+
- 8+ Mbps internet connection (upload and download)
- 5GHz WiFi recommended for best performance

---

## Integration with Existing System

### WebRTC Connection
All features integrate seamlessly with the existing WebRTC infrastructure:
- Uses `WebRTCConnection.SendCustomPrompt()` method
- Maintains existing camera and video streaming
- Compatible with current prompt system
- No changes to core WebRTC logic

### Camera System
Works with existing passthrough camera integration:
- Uses `WebCamTextureManager` for camera access
- Maintains camera permissions handling
- Compatible with Quest 3 passthrough API

### Voice System (Optional)
The existing voice control system is still available but not required:
- Voice can still be used for custom prompts if Wit.ai is configured
- Keyboard input is the primary method as requested
- Voice setup is optional, not mandatory

---

## User Experience Flow

1. **Launch App**
   - Grant camera permissions
   - Wait for camera initialization (5-10 seconds)
   - See passthrough camera feed

2. **Main Menu Appears**
   - Menu visible by default
   - Five main options displayed
   - Current selection highlighted

3. **Select Feature**
   - Navigate with joystick
   - Press right trigger to confirm
   - Feature panel opens

4. **Use Feature**
   - Browse options (if applicable)
   - Adjust settings
   - Press right trigger to apply transformation

5. **See Results**
   - AI processing takes 3-5 seconds
   - Transformed video appears
   - Can change options and reapply

6. **Return to Menu**
   - Press left trigger to go back
   - Press hamburger to hide/show menu
   - Select different feature or same feature again

---

## Development Details

### Scripts Created

**Core Controllers**:
- `MenuController.cs` - Main menu system with joystick navigation
- `MenuOption.cs` - Menu option data structure

**Feature Controllers**:
- `TimeTravelController.cs` - Time Travel feature
- `VirtualTryOnController.cs` - Virtual Try-On feature
- `BiomeController.cs` - Biome Transformation feature
- `VideoGameController.cs` - Video Game Style feature
- `CustomPromptController.cs` - Custom Prompt feature

**Build Automation**:
- `BuildAutomation.cs` - Unity Editor build automation

### Code Standards
All code follows the project's established standards:
- PascalCase for public methods and properties
- camelCase with underscore prefix for private fields
- Comprehensive XML documentation
- Proper memory management and cleanup
- Unity best practices

---

## Future Enhancements

Potential additions (not implemented in current version):

### Additional Features
- **Historical Figures**: Transform into famous historical people
- **Seasonal Themes**: Spring, Summer, Fall, Winter transformations
- **Fantasy Creatures**: Transform into dragons, unicorns, etc.
- **Art Styles**: Impressionism, Cubism, Surrealism, etc.

### UI Improvements
- Icons for each menu option
- Preview thumbnails
- Animation transitions
- Sound effects for all interactions

### Advanced Controls
- Favorites/bookmarks system
- History of recent transformations
- Preset combinations
- Custom prompt library

### Social Features
- Share screenshots
- Save favorite transformations
- Community prompts
- Multiplayer shared environments

---

## Credits

**AI Processing**: [Decart AI](https://decart.ai) - Real-time video transformation
**Platform**: Meta Quest 3
**Framework**: Unity 6
**WebRTC**: Unity WebRTC package
**Voice SDK**: Meta Voice SDK (optional)

---

## Support

For issues, questions, or feature requests:
- GitHub Issues: [Repository Issues](https://github.com/Jakubikk/joystick-nav/issues)
- Decart Discord: [discord.gg/decart](https://discord.gg/decart)
- Documentation: See `/Documentation` folder

---

*This features document describes the complete implementation of all requested features for the Meta Quest 3 AI Transformation App.*

# DecartXR Quest 3 - Features Overview

A comprehensive guide to all features and capabilities of the DecartXR application for Meta Quest 3.

---

## Table of Contents

1. [Introduction](#introduction)
2. [Navigation System](#navigation-system)
3. [Time Travel Feature](#time-travel-feature)
4. [Virtual Try-On Feature](#virtual-try-on-feature)
5. [Biome Transform Feature](#biome-transform-feature)
6. [Video Game Style Feature](#video-game-style-feature)
7. [Custom Prompt Feature](#custom-prompt-feature)
8. [Technical Details](#technical-details)

---

## Introduction

DecartXR is a revolutionary VR application that uses AI-powered real-time video transformation to change how you see your environment. Using Decart's advanced AI models (Mirage and Lucy), the app transforms your Quest 3's passthrough view into various styles, locations, time periods, and more.

### Core Technology
- **Decart Mirage Model**: World environment transformations
- **Decart Lucy Model**: Person and clothing transformations  
- **Real-time Processing**: ~150-200ms latency
- **WebRTC Streaming**: 30fps @ 1280×720 resolution
- **Quest 3 Integration**: Native passthrough camera access

---

## Navigation System

The app uses an intuitive joystick and trigger-based navigation system designed specifically for VR.

### Controller Layout

**Left Controller:**
- **Left Trigger**: Go back to previous menu
- **Left Joystick**: No function (reserved)
- **Other buttons**: No function

**Right Controller:**
- **Right Trigger**: Confirm selection / Apply transformation
- **Right Joystick**:
  - **Up/Down**: Navigate through menu items
  - **Left/Right**: Adjust sliders (in Time Travel mode)
- **Start Button (Hamburger)**: Show/Hide menu
- **Other buttons**: No function

### Menu Structure

```
Main Menu
├── Time Travel
├── Virtual Try-On
├── Biome Transform
├── Video Game Style
└── Custom Prompt
```

### Navigation Flow

1. Press **Start** to show menu
2. Use **Joystick Up/Down** to highlight options
3. Press **Right Trigger** to select
4. Press **Left Trigger** to go back
5. Press **Start** to hide menu and use VR normally

---

## Time Travel Feature

Transform your environment to see how it might have looked in different historical periods or could look in the future.

### How It Works

The Time Travel feature uses a slider-based year selection system that applies historical or futuristic aesthetics to your environment using the Decart Mirage model.

### Available Time Periods

| Year Range | Era | Description |
|------------|-----|-------------|
| **1800** | Early Industrial Era | Gas lamps, cobblestone streets, Victorian architecture |
| **1850** | Victorian Age | Ornate architecture, formal gardens, period furnishings |
| **1900** | Turn of Century | Art Nouveau style, elegant street lamps, belle époque |
| **1920** | Roaring Twenties | Art Deco style, jazz age, geometric patterns |
| **1950** | Mid-Century Modern | Clean lines, pastel colors, retro furniture |
| **1980** | Retro 80s | Neon colors, geometric patterns, synth wave vibes |
| **2000** | Y2K Era | Sleek technology, minimalist design, chrome accents |
| **2020** | Modern Day | Contemporary style, smart technology, current trends |
| **2050** | Near Future | Advanced technology, holographic displays, smart cities |
| **2100** | Advanced Future | Highly advanced tech, flying vehicles, AI integration |
| **2150** | Space Age | Zero-gravity elements, planetary views, robotics |
| **2200** | Post-Human Era | Cybernetic integration, quantum tech, transcendent design |

### Usage Instructions

1. Select **Time Travel** from main menu
2. Use **Right Joystick Left/Right** to adjust the year slider
3. Watch the year display and description update
4. Press **Right Trigger** to apply the transformation
5. Your environment transforms to the selected time period
6. Press **Left Trigger** to return to menu

### Examples

- **1920s**: Your room becomes an Art Deco lounge
- **2100**: Your space transforms into a futuristic smart home
- **1800**: Environment gains Victorian industrial aesthetic

---

## Virtual Try-On Feature

Try on different clothing items virtually by standing in front of a mirror. The app uses the Decart Lucy model to transform your appearance.

### How It Works

Stand in front of any mirror or reflective surface. The Lucy AI model will detect you and apply the selected clothing transformation, allowing you to see yourself wearing different outfits in the mirror's reflection.

### Clothing Categories (32+ Items)

#### 1. Formal Wear (4 items)
- Classic Tuxedo
- Elegant Evening Gown
- Business Suit
- Cocktail Dress

#### 2. Casual Wear (4 items)
- Leather Jacket & Jeans
- Summer Dress
- Hoodie & Joggers
- Denim Jacket Outfit

#### 3. Historical Costumes (4 items)
- Medieval Knight Armor
- Victorian Gentleman
- Renaissance Noble
- Ancient Roman Toga

#### 4. Fantasy & Cosplay (4 items)
- Wizard Robes
- Superhero Suit
- Samurai Armor
- Elven Attire

#### 5. Professional Uniforms (4 items)
- Chef Uniform
- Doctor's Coat
- Police Uniform
- Pilot Uniform

#### 6. Cultural & Traditional (4 items)
- Japanese Kimono
- Indian Sari
- Scottish Kilt
- Arabian Thobe

#### 7. Sports & Athletic (4 items)
- Basketball Jersey
- Soccer Kit
- Tennis Outfit
- Cycling Gear

### Usage Instructions

1. Stand in front of a real mirror
2. Select **Virtual Try-On** from main menu
3. Use **Joystick Up/Down** to browse clothing options
4. Selected item highlights in cyan
5. Press **Right Trigger** to try on the selected clothing
6. View yourself in the mirror wearing the new outfit
7. Try different items by navigating and pressing Right Trigger
8. Press **Left Trigger** to return to menu

### Tips for Best Results

- ✅ Use a large mirror for full-body outfits
- ✅ Ensure good lighting
- ✅ Stand at arm's length from mirror
- ✅ Face the mirror directly
- ✅ Hold still for a moment after applying

---

## Biome Transform Feature

Transform your room into different natural environments, famous locations, or fantastical settings from around the world.

### How It Works

Uses the Decart Mirage model to completely transform your environment into various biomes, countries, or fantasy locations while maintaining the spatial structure of your room.

### Available Transformations (28 biomes)

#### Natural Biomes (6 environments)
- **Tropical Rainforest**: Lush jungle with exotic plants
- **Arctic Tundra**: Frozen landscape with ice and aurora
- **Desert Oasis**: Sandy dunes with palm trees
- **Underwater Ocean**: Beneath the sea with coral reefs
- **Mountain Cave**: Rocky cave with crystals
- **Bamboo Forest**: Serene Asian bamboo grove

#### Country/City Locations (8 locations)
- **Japanese Garden**: Traditional Zen garden with cherry blossoms
- **Parisian Cafe**: Charming outdoor cafe with Eiffel Tower view
- **New York Loft**: Industrial Manhattan loft with city skyline
- **Egyptian Temple**: Ancient temple near pyramids
- **Moroccan Palace**: Ornate palace with tile mosaics
- **Scandinavian Cabin**: Cozy Nordic cabin in woods
- **Indian Palace**: Colorful Maharaja palace
- **Greek Island**: White buildings by Mediterranean sea

#### Fantasy Locations (6 environments)
- **Magical Forest**: Enchanted woods with glowing mushrooms
- **Space Station**: Futuristic orbital habitat
- **Cloud Palace**: Floating castle in the sky
- **Steampunk Workshop**: Victorian mechanical workshop
- **Cyberpunk City**: Neon-lit futuristic metropolis
- **Medieval Castle**: Stone fortress interior

#### Seasonal/Weather (4 scenes)
- **Cherry Blossom Spring**: Japanese spring with pink blossoms
- **Autumn Forest**: Fall colors with golden leaves
- **Winter Wonderland**: Snowy holiday scene
- **Rainy Day**: Cozy rain with water droplets

### Usage Instructions

1. Select **Biome Transform** from main menu
2. Use **Joystick Up/Down** to browse locations
3. Selected biome highlights in green
4. Read the description of each location
5. Press **Right Trigger** to apply transformation
6. Your room transforms into the selected environment
7. Try different biomes as desired
8. Press **Left Trigger** to return to menu

### Examples

- **Tropical Rainforest**: Your walls become jungle vegetation
- **Space Station**: Your room becomes a futuristic orbital hab
- **Medieval Castle**: Stone walls and torch lighting appear

---

## Video Game Style Feature

See your environment as if it was rendered in various iconic video game styles and engines.

### How It Works

Applies video game aesthetic filters and visual styles to your environment using the Decart Mirage model, mimicking the look of famous games.

### Available Game Styles (34+ games)

#### Block/Voxel Games (3 styles)
- **Minecraft**: Blocky voxel world with pixelated textures
- **LEGO World**: Everything made of colorful LEGO bricks
- **Roblox**: Simple blocky characters and bright colors

#### RPG/Fantasy Games (4 styles)
- **Skyrim**: Nordic medieval architecture and fantasy
- **The Witcher**: Dark fantasy medieval world
- **World of Warcraft**: Stylized fantasy with vibrant colors
- **Final Fantasy**: Fantasy and futuristic blend

#### Shooter Games (3 styles)
- **Call of Duty**: Modern military realistic environment
- **Borderlands**: Cell-shaded comic book style
- **Halo**: Futuristic alien architecture and tech

#### Adventure Games (3 styles)
- **Legend of Zelda**: Colorful cel-shaded fantasy
- **Uncharted**: Realistic ancient ruins
- **Tomb Raider**: Archaeological exploration sites

#### Horror Games (3 styles)
- **Resident Evil**: Dark survival horror atmosphere
- **Silent Hill**: Foggy psychological horror
- **Dead Space**: Futuristic space horror

#### Retro/Classic Games (4 styles)
- **Super Mario**: Colorful cartoon mushroom kingdom
- **Sonic**: Fast-paced colorful zones
- **Pac-Man**: Retro arcade with neon mazes
- **8-Bit Retro**: Classic NES pixel art

#### Open World Games (3 styles)
- **GTA**: Modern urban city environment
- **Red Dead Redemption**: Wild West frontier
- **Cyberpunk 2077**: Neon dystopian future

#### Puzzle/Artistic Games (3 styles)
- **Portal**: Sterile Aperture Science test chambers
- **Journey**: Artistic desert with flowing sand
- **Gris**: Watercolor painted aesthetics

#### Fighting Games (2 styles)
- **Street Fighter**: Vibrant 2D fighting stages
- **Mortal Kombat**: Dark tournament arenas

#### Strategy Games (2 styles)
- **Civilization**: Strategic map hexagonal tiles
- **StarCraft**: Sci-fi RTS battlefield

### Usage Instructions

1. Select **Video Game Style** from main menu
2. Use **Joystick Up/Down** to browse games
3. Selected game highlights in magenta
4. Read the genre and description
5. Press **Right Trigger** to apply style
6. Your environment transforms to match the game
7. Explore different game aesthetics
8. Press **Left Trigger** to return to menu

### Examples

- **Minecraft**: Everything becomes blocky and pixelated
- **Cyberpunk 2077**: Neon lights and holograms appear
- **Portal**: Clean white test chamber aesthetic

---

## Custom Prompt Feature

Create your own unique transformations by typing custom prompts using the Meta Quest keyboard.

### How It Works

You type a text description of how you want your environment transformed, and the Decart AI interprets and applies it in real-time.

### Keyboard Integration

The app integrates with Meta Quest's built-in system keyboard for easy text input in VR.

**Opening the Keyboard:**
- Press **Right Trigger** on the Custom Prompt screen
- Or tap the "Open Keyboard" button
- Meta Quest's virtual keyboard appears

### Usage Instructions

1. Select **Custom Prompt** from main menu
2. Press **Right Trigger** to open Meta keyboard
3. Type your transformation description
4. Examples shown below for inspiration
5. Press Enter or close keyboard when done
6. Press **A button** (or tap Submit) to apply
7. Watch your custom transformation come to life
8. Press **B button** (or tap Clear) to reset and try again
9. Press **Left Trigger** to return to menu

### Example Custom Prompts

**Creative Transformations:**
- "Transform everything into a magical candy land with chocolate walls and lollipop trees"
- "Make the environment look like it's underwater with coral and swimming fish"
- "Turn the room into a cozy library with wooden bookshelves and warm lighting"
- "Transform into a spaceship interior with holographic controls and star views"

**Material Transformations:**
- "Make everything look like it's made of glass with crystal clear transparency"
- "Turn all surfaces into pure gold with metallic reflections"
- "Transform materials to look like they're carved from ice"
- "Make everything appear to be made of colorful stained glass"

**Thematic Transformations:**
- "Turn the environment into a wild west saloon with wooden floors"
- "Transform into an ancient Egyptian tomb with hieroglyphics"
- "Make it look like a tropical tiki bar with bamboo decorations"
- "Turn everything into a winter ice palace with frozen architecture"

**Unusual Transformations:**
- "Transform into a steampunk laboratory with brass gears and machinery"
- "Make it look like I'm inside a giant aquarium with fish swimming around"
- "Turn my room into a haunted mansion with cobwebs and vintage furniture"
- "Transform everything into a vibrant art gallery with paintings on walls"

### Tips for Writing Effective Prompts

**Do:**
- ✅ Be specific and descriptive
- ✅ Mention materials, colors, and textures
- ✅ Include atmosphere and lighting
- ✅ Reference specific styles or themes
- ✅ Describe what you want to see

**Don't:**
- ❌ Be too vague ("make it cool")
- ❌ Use negatives ("don't show...")
- ❌ Write overly long prompts (keep under 500 characters)
- ❌ Request impossible physical changes

### Character Limit
- **Maximum**: 500 characters
- **Recommended**: 100-200 characters for best results

---

## Technical Details

### AI Models Used

**Decart Mirage Model:**
- Purpose: World and environment transformations
- Used in: Time Travel, Biome Transform, Video Game Style
- Strengths: Complete scene transformation, artistic styles
- Resolution: 1280×720 @ 30fps
- Latency: ~150-200ms

**Decart Lucy Model:**
- Purpose: Person and clothing transformations
- Used in: Virtual Try-On
- Strengths: Preserves identity, precise clothing edits
- Resolution: 1280×720 @ 30fps
- Latency: ~150-200ms

### Performance Requirements

**Network:**
- Minimum: 8 Mbps bidirectional
- Recommended: 15+ Mbps for best quality
- Connection: WiFi (5GHz recommended)

**Hardware:**
- Device: Meta Quest 3 with Horizon OS v74+
- Camera: Passthrough cameras enabled
- Storage: ~200MB app size

**Environment:**
- Lighting: Adequate room lighting
- Space: 2×2 meters minimum play area
- Cameras: Clean Quest camera lenses

### Transformation Process

1. **Capture**: Quest passthrough cameras capture environment
2. **Stream**: Video streamed to Decart servers via WebRTC
3. **Process**: AI model applies transformation
4. **Return**: Transformed video streamed back
5. **Display**: Shown in VR at 30fps
6. **Total latency**: 150-200ms end-to-end

### Data Flow

```
Quest Camera → WebRTC Encode (VP8) → Internet → Decart AI → 
Process (Mirage/Lucy) → Internet → WebRTC Decode → Quest Display
```

---

## Frequently Asked Questions

**Q: Can I save my transformations?**
A: Currently transformations are real-time only. Screenshots can be taken using Quest's capture button.

**Q: Does it work in all lighting conditions?**
A: Works best in well-lit environments. Very dark or very bright areas may affect quality.

**Q: Can I use multiple features at once?**
A: Select one feature at a time. Each applies its own transformation.

**Q: Is there a limit to custom prompts?**
A: Prompts are limited to 500 characters. More concise prompts often work better.

**Q: Why does it take a few seconds to start?**
A: Initial AI processing takes 3-5 seconds to establish connection and begin transformation.

**Q: Can I adjust the intensity?**
A: Currently intensity is fixed for quality. Future updates may add controls.

**Q: Does it work without internet?**
A: No, requires internet connection for AI processing on Decart servers.

---

## Future Features (Planned)

Potential additions in future versions:

- **Intensity Slider**: Adjust transformation strength
- **Favorites System**: Save and recall favorite transformations
- **Screenshot Gallery**: Built-in gallery of captured transformations
- **Multi-user Mode**: Share transformations with nearby users
- **Audio Effects**: Match sound to visual transformation
- **Recording**: Save video clips of transformations
- **Community Prompts**: Share and download user-created prompts
- **Hand Tracking**: Use hand gestures instead of controllers

---

## Credits

- **AI Technology**: Decart AI ([https://decart.ai](https://decart.ai))
- **Platform**: Meta Quest 3
- **Development**: Built with Unity 6
- **Voice SDK**: Meta Voice SDK
- **WebRTC**: Unity WebRTC
- **Open Source Components**: See LICENSE file

---

*For setup instructions, see [COMPLETE_SETUP_GUIDE.md](COMPLETE_SETUP_GUIDE.md)*  
*For automation details, see [AUTOMATION_VS_MANUAL.md](AUTOMATION_VS_MANUAL.md)*

**Last Updated**: 2025  
**Version**: 1.0.0

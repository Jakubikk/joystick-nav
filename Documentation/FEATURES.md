# Feature Documentation
## Quest 3 Decart AI Application

This document describes all features of the Quest 3 Decart AI application, their prompts, and how they work.

---

## Table of Contents
1. [Feature Overview](#feature-overview)
2. [Time Travel](#time-travel)
3. [Virtual Mirror](#virtual-mirror)
4. [Biomes & Countries](#biomes--countries)
5. [Video Game Styles](#video-game-styles)
6. [Custom Prompts](#custom-prompts)
6. [Technical Details](#technical-details)

---

## Feature Overview

The application provides 5 main AI-powered transformation features:

### Navigation Controls (Universal):
- **Hamburger Button (Start)**: Toggle menu visibility
- **Joystick Up/Down**: Navigate options
- **Right Trigger**: Confirm/Apply
- **Left Trigger**: Back to main menu

### Features:
1. **Time Travel** - Historical period transformations (12 eras)
2. **Virtual Mirror** - Clothing try-on system (22 outfits)
3. **Biomes & Countries** - Location transformations (28 locations)
4. **Video Game Styles** - Game aesthetic filters (32 games)
5. **Custom Prompts** - User-defined transformations

---

## Time Travel

### Purpose
View your environment as it would appear in different historical periods or future scenarios.

### How It Works
1. Select "Time Travel" from main menu
2. Use joystick to adjust year slider (1700-2300)
3. Era name and description update automatically
4. Press right trigger to apply transformation
5. Environment transforms to match selected time period

### Available Eras (12 total):

#### 1. Colonial Era (1700-1799)
**Description**: Colonial architecture, horse-drawn carriages, cobblestone streets

**Prompt**: "Transform to colonial era 1700s style, cobblestone streets, gas lamps, horse carriages, colonial architecture, wooden buildings, dirt roads"

**Best For**: Historical tours, education, period settings

---

#### 2. Victorian Era (1800-1899)
**Description**: Victorian architecture, steam trains, gas street lamps

**Prompt**: "Transform to Victorian era 1800s, ornate Victorian architecture, gas street lamps, horse-drawn carriages, Victorian fashion, cobblestone streets, industrial smoke"

**Best For**: Historical immersion, steampunk aesthetics

---

#### 3. Early Modern (1900-1949)
**Description**: Art deco buildings, vintage cars, early electric lights

**Prompt**: "Transform to early 1900s modern era, art deco architecture, vintage automobiles, early electric street lights, fedora hats, classic storefronts"

**Best For**: 1920s-40s nostalgia, film noir atmosphere

---

#### 4. Mid-Century Modern (1950-1969)
**Description**: Classic cars, neon signs, retro aesthetic

**Prompt**: "Transform to 1950s-1960s retro style, classic vintage cars, neon signs, mid-century modern architecture, vibrant colors, retro diners, vintage fashion"

**Best For**: Retro vibes, Americana, vintage aesthetics

---

#### 5. Disco Era (1970-1979)
**Description**: Bright colors, disco balls, funky patterns

**Prompt**: "Transform to 1970s disco era style, bright vibrant colors, funky patterns, disco ball reflections, bell-bottom pants, platform shoes, groovy aesthetic"

**Best For**: Party atmospheres, groovy vibes

---

#### 6. Neon Eighties (1980-1989)
**Description**: Neon lights, synthwave aesthetic, bold colors

**Prompt**: "Transform to 1980s aesthetic, neon lights, synthwave colors, geometric patterns, bold bright colors, arcade machines, cassette tapes, retro technology"

**Best For**: Synthwave, retro gaming, 80s nostalgia

---

#### 7. 90s Nostalgia (1990-1999)
**Description**: Grunge aesthetic, early tech, CRT monitors

**Prompt**: "Transform to 1990s style, grunge aesthetic, early computers, CRT monitors, VHS tapes, mall culture, baggy clothes, chunky technology"

**Best For**: 90s kids nostalgia, early internet era

---

#### 8. Early Digital Age (2000-2009)
**Description**: Early smartphones, flat screens, modern cars

**Prompt**: "Transform to early 2000s digital age, flip phones, early flat screen displays, Y2K aesthetic, modern cars, digital billboards, early social media era"

**Best For**: Recent nostalgia, early 2000s vibes

---

#### 9. Modern Era (2010-2024)
**Description**: Smartphones, LED screens, contemporary design

**Prompt**: "Transform to modern 2010s style, smartphones everywhere, LED screens, contemporary architecture, electric vehicles, modern minimalist design, digital displays"

**Best For**: Current day reference, contemporary settings

---

#### 10. Near Future (2025-2050)
**Description**: Advanced tech, sleek design, holographic displays

**Prompt**: "Transform to near future 2025-2050 style, holographic displays, autonomous vehicles, sleek futuristic architecture, advanced technology, clean energy, smart city infrastructure"

**Best For**: Sci-fi scenarios, future planning

---

#### 11. Mid Future (2051-2100)
**Description**: Flying vehicles, mega structures, advanced AI

**Prompt**: "Transform to mid-future 2051-2100 cyberpunk style, flying vehicles, massive skyscrapers, neon lit megacity, advanced robotics, holographic advertisements, cybernetic enhancements"

**Best For**: Cyberpunk aesthetics, dystopian futures

---

#### 12. Distant Future (2101-2300)
**Description**: Space age, crystalline structures, ultra-advanced civilization

**Prompt**: "Transform to distant future 2100+ style, space age architecture, crystalline structures, floating platforms, energy fields, ultra-advanced technology, utopian sci-fi aesthetic"

**Best For**: Far future scenarios, utopian settings

---

### Technical Notes:
- **Model Used**: Decart Mirage (environment transformation)
- **Processing Time**: 3-5 seconds
- **Resolution**: 1280x720 @ 30fps
- **Latency**: ~150-200ms

---

## Virtual Mirror

### Purpose
Try on different clothing and costumes virtually while looking at yourself in a mirror.

### How It Works
1. Stand in front of a mirror (or reflective surface)
2. Select "Virtual Mirror" from main menu
3. Browse clothing options with joystick
4. Press right trigger to "wear" selected outfit
5. See yourself in the new clothing in real-time

### Available Outfits (22 total):

#### Casual Wear (2 options)
1. **Casual T-Shirt & Jeans**: Everyday comfort wear
2. **Hoodie & Joggers**: Sporty casual style

#### Formal Wear (2 options)
3. **Business Suit**: Professional formal attire
4. **Elegant Evening Dress**: Sophisticated formal wear

#### Historical Costumes (4 options)
5. **Medieval Knight**: Armored warrior with chainmail
6. **Victorian Era**: 19th century elegance
7. **Ancient Roman**: Classical toga and sandals
8. **1920s Flapper**: Roaring twenties style

#### Fantasy & Sci-Fi (4 options)
9. **Wizard Robes**: Mystical magical attire
10. **Space Suit**: Futuristic astronaut gear
11. **Cyberpunk Outfit**: Neon-lit future style
12. **Superhero Suit**: Comic book hero costume

#### Cultural & Traditional (3 options)
13. **Traditional Kimono**: Japanese formal wear
14. **Scottish Kilt**: Traditional Highland dress
15. **Indian Sari**: Traditional Indian attire

#### Sports & Athletic (2 options)
16. **Football/Soccer Jersey**: Professional sports uniform
17. **Basketball Uniform**: Professional basketball gear

#### Professions (3 options)
18. **Doctor Scrubs**: Medical professional attire
19. **Chef Uniform**: Professional culinary attire
20. **Police Uniform**: Law enforcement attire

#### Character Costumes (2 options)
21. **Pirate Costume**: Swashbuckling seafarer
22. **Cowboy Outfit**: Wild West style

### Technical Notes:
- **Model Used**: Decart Lucy (person-focused transformation)
- **Best Results**: Stand 3-6 feet from mirror, good lighting
- **Maintains**: Your identity, pose, and movement
- **Changes**: Only clothing and accessories

---

## Biomes & Countries

### Purpose
Transform your room/environment to appear as different locations worldwide or fantasy settings.

### How It Works
1. Select "Biomes & Countries" from main menu
2. Browse location options with joystick
3. Press right trigger to transform environment
4. Your surroundings change to match selected location

### Available Locations (28 total):

#### Natural Biomes (6 locations)
1. **Tropical Rainforest**: Lush jungle with exotic plants
2. **Arctic Tundra**: Frozen landscape with snow
3. **Desert Oasis**: Sandy dunes with palm trees
4. **Mountain Forest**: Pine forest with mountain peaks
5. **Ocean Underwater**: Beneath the sea with coral
6. **Cherry Blossom Park**: Japanese spring garden

#### Major Cities (8 locations)
7. **Paris, France**: Romantic Parisian streets, Eiffel Tower
8. **Tokyo, Japan**: Neon-lit Japanese cityscape
9. **New York City, USA**: Bustling Manhattan streets
10. **Venice, Italy**: Romantic canals and gondolas
11. **Cairo, Egypt**: Ancient pyramids and desert
12. **London, England**: Classic British cityscape
13. **Amsterdam, Netherlands**: Dutch canals and bicycles
14. **Dubai, UAE**: Futuristic luxury architecture

#### Fantasy Environments (4 locations)
15. **Enchanted Forest**: Magical woodland with mystical creatures
16. **Volcanic Landscape**: Lava flows and volcanic rock
17. **Alien Planet**: Extraterrestrial landscape
18. **Crystal Cave**: Glowing crystalline cavern

#### Seasonal Variations (4 locations)
19. **Autumn Forest**: Fall colors and falling leaves
20. **Winter Wonderland**: Snowy Christmas atmosphere
21. **Spring Meadow**: Colorful flowers and greenery
22. **Summer Beach**: Sunny seaside paradise

#### Historical Settings (3 locations)
23. **Ancient Greece**: Classical marble temples
24. **Medieval Castle**: Stone fortress and courtyard
25. **Wild West Town**: Frontier saloon and dusty streets

### Technical Notes:
- **Model Used**: Decart Mirage (environment transformation)
- **Works Best**: In well-lit rooms with clear features
- **Transforms**: Walls, floors, furniture, entire environment
- **Maintains**: Room layout and spatial awareness

---

## Video Game Styles

### Purpose
See your world through the visual style of popular video games.

### How It Works
1. Select "Video Game Styles" from main menu
2. Browse game options with joystick
3. Press right trigger to apply game aesthetic
4. Environment adopts selected game's visual style

### Available Game Styles (32 total):

#### Blocky/Voxel Games (2 games)
1. **Minecraft**: Blocky voxel world with pixelated textures
2. **LEGO World**: Everything made of plastic LEGO bricks

#### RPG Games (4 games)
3. **The Legend of Zelda: BOTW**: Cel-shaded fantasy
4. **The Witcher 3**: Dark fantasy medieval
5. **Skyrim**: Epic Nordic fantasy
6. **Final Fantasy**: Japanese RPG aesthetics

#### Action/Adventure (3 games)
7. **Grand Theft Auto V**: Realistic modern cityscape
8. **Assassin's Creed**: Historical adventure
9. **Uncharted**: Cinematic adventure visuals

#### Sci-Fi Games (4 games)
10. **Cyberpunk 2077**: Neon-drenched dystopia
11. **Halo**: Sci-fi military aesthetic
12. **Mass Effect**: Space opera visuals
13. **Portal**: Clean testing facility

#### Horror Games (2 games)
14. **Resident Evil**: Survival horror atmosphere
15. **Silent Hill**: Foggy psychological horror

#### Shooter Games (3 games)
16. **Call of Duty**: Modern military realism
17. **Battlefield**: Large-scale warfare
18. **Overwatch**: Colorful hero shooter

#### Racing Games (2 games)
19. **Forza Horizon**: Photorealistic racing
20. **Mario Kart**: Whimsical cartoon racing

#### Platformer Games (2 games)
21. **Super Mario Odyssey**: Vibrant Nintendo world
22. **Sonic the Hedgehog**: High-speed colorful zones

#### Indie Games (3 games)
23. **Stardew Valley**: Cozy pixel art farming
24. **Hollow Knight**: Hand-drawn metroidvania
25. **Celeste**: Pixel art mountain climbing

#### Strategy Games (2 games)
26. **Civilization VI**: Stylized world map
27. **Age of Empires**: Historical RTS aesthetic

#### Fighting Games (2 games)
28. **Street Fighter**: Stylized fighting arenas
29. **Mortal Kombat**: Dark martial arts realm

#### Classic Retro (2 games)
30. **8-Bit Retro**: Classic NES/Atari style
31. **16-Bit SNES Era**: Super Nintendo graphics

### Technical Notes:
- **Model Used**: Decart Mirage (style transformation)
- **Effect**: Applies game's visual filters, colors, rendering style
- **Best Results**: Rooms with varied objects and textures
- **Realism**: Ranges from photorealistic to highly stylized

---

## Custom Prompts

### Purpose
Type your own transformation prompts for unlimited creative possibilities.

### How It Works
1. Select "Custom Prompt" from main menu
2. Press right trigger to open Meta's system keyboard
3. Type your transformation description
4. Press Enter/Done on keyboard
5. Your custom prompt is sent to AI for processing

### Tips for Good Prompts:

#### Structure:
```
[Action] to [style/theme], [visual details], [atmosphere], [specific elements]
```

#### Examples:

**Good Prompts** (detailed):
- ✅ "Transform to underwater coral reef, blue water, colorful fish swimming, sea plants, shimmering light rays, peaceful ocean atmosphere"
- ✅ "Change to ancient Egyptian temple, sandstone pillars, hieroglyphics on walls, golden decorations, torches, mystical atmosphere"
- ✅ "Transform to candy land, everything made of sweets, lollipop trees, chocolate buildings, rainbow colors, whimsical sugary aesthetic"

**Poor Prompts** (too vague):
- ❌ "Make it cool"
- ❌ "Change the colors"
- ❌ "Something different"

#### Tips:
1. **Be Specific**: Describe exact details you want
2. **Include Materials**: "wooden", "metal", "glass", "fabric"
3. **Describe Lighting**: "sunny", "neon", "candlelit", "shadowy"
4. **Add Atmosphere**: "peaceful", "ominous", "cheerful", "mysterious"
5. **Use Examples**: Reference known styles/places
6. **Length**: 10-30 words works best

### Quick Preset Prompts:
The controller includes 4 quick presets:
1. **Anime**: Vibrant cartoon style with bold outlines
2. **Oil Painting**: Visible brush strokes, rich colors
3. **Cyberpunk**: Neon lights, futuristic atmosphere
4. **Watercolor**: Soft flowing colors, artistic

### Technical Notes:
- **Works With**: Both Mirage (environment) and Lucy (people) models
- **Keyboard**: Uses Meta Quest's built-in system keyboard
- **History**: Saves your last 5 prompts
- **Processing**: Same 3-5 second processing time

---

## Technical Details

### AI Models Used

#### Decart Mirage (Environment Transformations):
- **Used By**: Time Travel, Biomes, Video Games, Custom (environments)
- **Specializes In**: World/scene transformation
- **Best For**: Changing entire environments, backgrounds, settings
- **Resolution**: 1280x704 @ 25fps
- **Latency**: ~150-200ms

#### Decart Lucy (Person Transformations):
- **Used By**: Virtual Mirror, Custom (people)
- **Specializes In**: Person-focused edits
- **Best For**: Clothing changes, character transformations
- **Resolution**: 1280x704 @ 25fps
- **Latency**: ~150-200ms

### Processing Pipeline:
1. **Capture**: Quest passthrough camera feed
2. **Encode**: VP8 video encoding @ 30fps
3. **Stream**: WebRTC connection to Decart API
4. **Transform**: AI processing on Decart servers
5. **Return**: Transformed video stream
6. **Display**: Rendered in VR environment

### Performance:
- **End-to-End Latency**: ~150-200ms total
- **Network Requirement**: 8+ Mbps bidirectional
- **Video Quality**: 720p @ 30fps
- **Battery Life**: ~2 hours continuous use
- **Memory Usage**: ~50MB additional

### Quest Requirements:
- **Hardware**: Meta Quest 3 or Quest 3S
- **OS**: Horizon OS v74 or later
- **Permissions**: Camera access required
- **Network**: WiFi connection (5GHz recommended)

---

## Adding Your Own Content

### To Add Time Travel Eras:
Edit `TimeTravelController.cs`, add to `InitializeHistoricalEras()`:
```csharp
historicalEras.Add(new YearRange {
    name = "Your Era",
    description = "Brief description",
    prompt = "Detailed transformation prompt",
    startYear = 2000,
    endYear = 2050
});
```

### To Add Clothing Options:
Edit `VirtualMirrorController.cs`, add to `InitializeClothingOptions()`:
```csharp
clothingOptions.Add(new ClothingOption {
    name = "Your Outfit",
    description = "Brief description",
    prompt = "Change the person's outfit to [details]"
});
```

### To Add Locations:
Edit `BiomeController.cs`, add to `InitializeBiomeOptions()`:
```csharp
biomeOptions.Add(new BiomeOption {
    name = "Your Location",
    description = "Brief description",
    prompt = "Transform environment to [details]"
});
```

### To Add Game Styles:
Edit `VideoGameController.cs`, add to `InitializeGameOptions()`:
```csharp
gameOptions.Add(new GameStyleOption {
    name = "Your Game",
    description = "Brief description",
    prompt = "Transform to [game] style, [details]"
});
```

---

## Best Practices

### For Best Results:
1. **Good Lighting**: Not too bright, not too dark
2. **Clean Cameras**: Wipe Quest camera lenses
3. **Strong WiFi**: Use 5GHz, stay close to router
4. **Clear Space**: Transformations work better with varied objects
5. **Wait**: Give AI 3-5 seconds to process each time

### Troubleshooting:
- **Slow Processing**: Check WiFi speed, move closer to router
- **Poor Quality**: Improve room lighting, clean cameras
- **Disconnections**: Restart app, check network stability
- **Camera Issues**: Check permissions, update Quest OS

---

## Credits

**AI Technology**: Decart AI (https://decart.ai)
**Models**: Mirage v2 (environments), Lucy v2v (people)
**Platform**: Meta Quest 3
**Framework**: Unity 6 with WebRTC

---

Enjoy transforming your reality! 🚀

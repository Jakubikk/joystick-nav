# Unity Scene Setup Instructions

## ⚠️ IMPORTANT: Unity Scene Configuration Required

The C# scripts are complete and ready, but you need to set up the Unity scene UI elements manually in the Unity Editor.

This file provides step-by-step instructions for creating the menu UI in Unity.

---

## Why Manual Setup?

Unity `.unity` scene files are binary and complex. Rather than modifying them programmatically (which could corrupt the scene), you'll create the UI elements in the Unity Editor interface.

**Time Required:** 30-60 minutes
**Difficulty:** Beginner-friendly with these instructions

---

## Prerequisites

Before starting:
- [ ] Unity project opened (DecartAI-Quest-Unity)
- [ ] DecartAI-Main.unity scene loaded
- [ ] All scripts compiled without errors

---

## Part 1: Main Menu Canvas Setup

### Step 1: Create Main Menu Canvas

1. **Create Canvas:**
   - Right-click in Hierarchy → UI → Canvas
   - Name it "MainMenuCanvas"

2. **Configure Canvas:**
   - Select MainMenuCanvas in Hierarchy
   - In Inspector, find Canvas component:
     - **Render Mode:** World Space
     - **Event Camera:** Drag Main Camera here
     - **Sorting Layer:** Default

3. **Position Canvas:**
   - In Inspector, find RectTransform:
     - **Position:** X=0, Y=1.5, Z=2 (in front of user at eye level)
     - **Rotation:** X=0, Y=0, Z=0
     - **Scale:** X=0.001, Y=0.001, Z=0.001
     - **Width:** 1920
     - **Height:** 1080

### Step 2: Create Menu Background

1. **Add Background Panel:**
   - Right-click MainMenuCanvas → UI → Panel
   - Name it "MenuBackground"

2. **Configure Background:**
   - **Color:** R=20, G=20, B=30, A=200 (dark semi-transparent)
   - **RectTransform:** Anchor to stretch (full canvas)

### Step 3: Create Menu Title

1. **Add Text:**
   - Right-click MainMenuCanvas → UI → Text - TextMeshPro
   - Name it "MenuTitle"
   - (If prompted to import TMP Essentials, click "Import TMP Essentials")

2. **Configure Title:**
   - **Text:** "DECART AI MENU"
   - **Font Size:** 72
   - **Alignment:** Center, Top
   - **Color:** White
   - **Position:** X=0, Y=-100, Z=0 (from top)
   - **Width:** 1800
   - **Height:** 120

### Step 4: Create Menu Items Container

1. **Add Vertical Layout:**
   - Right-click MainMenuCanvas → UI → Vertical Layout Group
   - Name it "MenuItemsContainer"

2. **Configure Layout:**
   - **Position:** X=0, Y=-300, Z=0
   - **Width:** 1600
   - **Height:** 800
   - In Vertical Layout Group component:
     - **Spacing:** 20
     - **Child Alignment:** Upper Center
     - **Child Force Expand:** Width ✓, Height ✗

### Step 5: Create 5 Menu Item Buttons

For each of the 5 features, create a menu item:

1. **Create Button:**
   - Right-click MenuItemsContainer → UI → Button - TextMeshPro
   - Name it "MenuItem_TimeTravel" (or respective feature name)

2. **Configure Button:**
   - **Preferred Height:** 140 (in Layout Element component)
   - **Background Color (Normal):** R=50, G=50, B=60, A=200
   - **Background Color (Highlighted):** R=100, G=150, B=255, A=220

3. **Configure Button Text:**
   - Select child "Text (TMP)" under button
   - **Text:**
     - Button 1: "Time Travel"
     - Button 2: "Virtual Try-On"
     - Button 3: "Biome Transform"  
     - Button 4: "Video Game Style"
     - Button 5: "Custom Prompt"
   - **Font Size:** 48
   - **Alignment:** Center
   - **Color:** White

4. **Repeat** for all 5 buttons

---

## Part 2: Feature UI Panels

For each feature, create a dedicated UI panel (initially disabled).

### Template for Each Feature Panel:

1. **Create Panel:**
   - Right-click MainMenuCanvas → UI → Panel
   - Name it "[FeatureName]Panel" (e.g., "TimeTravelPanel")
   - **Important:** Uncheck the checkbox at top of Inspector to disable it

2. **Configure Panel:**
   - **RectTransform:** Anchor to stretch (full canvas)
   - **Color:** R=10, G=10, B=15, A=230

3. **Add Title:**
   - Right-click panel → UI → Text - TextMeshPro
   - Name it "FeatureTitle"
   - **Text:** Feature name
   - **Font Size:** 60
   - **Position:** Top center

4. **Add Instructions:**
   - Right-click panel → UI → Text - TextMeshPro
   - Name it "InstructionsText"
   - **Font Size:** 32
   - **Alignment:** Center
   - **Position:** Bottom of panel

### Specific Feature Elements:

#### Time Travel Panel:
1. Add Slider:
   - Right-click panel → UI → Slider
   - Name it "YearSlider"
   - Configure min/max (will be set by script)

2. Add Text fields:
   - "YearText" - displays current year
   - "DescriptionText" - displays period description

#### Virtual Try-On Panel:
1. Add Text fields:
   - "CategoryText" - shows current category
   - "SelectedClothingText" - shows item name
   - "DescriptionText" - shows item description

#### Biome Transform Panel:
1. Add Text fields:
   - "SelectedBiomeText" - shows location name
   - "DescriptionText" - shows location description

#### Video Game Style Panel:
1. Add Text fields:
   - "SelectedGameText" - shows game name
   - "DescriptionText" - shows game description

#### Custom Prompt Panel:
1. Add InputField:
   - Right-click panel → UI → InputField - TextMeshPro
   - Name it "PromptInputField"
   - Configure for multiline text

2. Add Buttons:
   - "MirageButton" - selects Mirage model
   - "LucyButton" - selects Lucy model
   - "ApplyButton" - sends prompt

3. Add Text fields:
   - "ModelText" - shows selected model
   - "StatusText" - shows feedback

---

## Part 3: Connect Scripts to UI

### Step 1: Add MenuManager Script

1. **Create GameObject:**
   - Right-click Hierarchy → Create Empty
   - Name it "MenuSystem"

2. **Add Script:**
   - Select MenuSystem
   - In Inspector, click "Add Component"
   - Search for "MenuManager"
   - Click to add

3. **Configure MenuManager:**
   - **Menu Canvas:** Drag MainMenuCanvas here
   - **Menu Items:** Set size to 5
     - Element 0: Drag MenuItem_TimeTravel
     - Element 1: Drag MenuItem_VirtualTryOn
     - Element 2: Drag MenuItem_BiomeTransform
     - Element 3: Drag MenuItem_VideoGameStyle
     - Element 4: Drag MenuItem_CustomPrompt
   - **Features:** Drag each feature panel's script (see next step)

### Step 2: Add Feature Scripts to Panels

For each feature panel:

1. **Select Panel** in Hierarchy
2. **Add Component** → Search for feature script:
   - TimeTravelPanel → TimeTravelFeature
   - VirtualTryOnPanel → VirtualTryOnFeature
   - BiomeTransformPanel → BiomeTransformFeature
   - VideoGameStylePanel → VideoGameStyleFeature
   - CustomPromptPanel → CustomPromptFeature

3. **Configure Script References:**
   - Drag UI elements to corresponding fields
   - For example, TimeTravelFeature:
     - **Year Slider:** Drag YearSlider
     - **Year Text:** Drag YearText
     - **Description Text:** Drag DescriptionText
     - **Instructions Text:** Drag InstructionsText
     - **WebRTC Connection:** Drag WebRTCConnection GameObject

4. **Repeat** for all feature panels

### Step 3: Update WebRTCController

1. **Find WebRTCController** in Hierarchy (likely on existing GameObject)
2. **In Inspector**, find WebRTCController script
3. **Add Reference:**
   - **Menu Manager:** Drag MenuSystem GameObject here

---

## Part 4: Final Configuration

### Step 1: Find WebRTCConnection GameObject

Look for the GameObject with WebRTCConnection script attached (likely in existing scene).

### Step 2: Verify All Feature Scripts Reference It

Each feature script should have:
- **WebRTC Connection:** Reference to WebRTCConnection GameObject

### Step 3: Test in Play Mode

1. **Click Play** button in Unity
2. **Verify:**
   - Menu appears in front of camera
   - Can navigate with joystick (simulated with keyboard: WASD)
   - Can select features (simulated with mouse click)
   - Feature panels appear when selected

### Step 4: Build for Quest

Once everything works in Play mode:
1. File → Build Settings
2. Build And Run
3. Test on actual Quest 3 device

---

## Troubleshooting

### UI Not Visible:
- Check Canvas position (Z=2, in front of camera)
- Check Canvas Render Mode (World Space)
- Check Canvas Scale (0.001)

### Scripts Not Working:
- Verify all references are set in Inspector
- Check Console for errors
- Ensure scripts are compiled

### Input Not Responding:
- Make sure EventSystem exists in scene
- Check OVRInput is working
- Test in Play mode first

---

## Optional: Use Prefabs

To save time on future changes:

1. **Create Prefabs:**
   - Drag configured panels to Project window
   - Creates reusable prefab

2. **Benefits:**
   - Easy to duplicate
   - Changes apply to all instances
   - Version control friendly

---

## Need More Help?

- See COMPLETE_GUIDE.md for Unity basics
- See ARCHITECTURE.md for component details
- Check Unity forums for UI-specific questions

---

## Quick Reference: Common Unity UI Components

| Component | Purpose | Location |
|-----------|---------|----------|
| Canvas | Container for all UI | Hierarchy → UI → Canvas |
| Panel | Background container | Right-click Canvas → UI → Panel |
| Button | Clickable element | Right-click parent → UI → Button |
| Text (TMP) | Display text | Right-click parent → UI → Text |
| InputField | Text input | Right-click parent → UI → InputField |
| Slider | Value selection | Right-click parent → UI → Slider |
| Layout Group | Auto-arrange children | Add Component → Layout |

---

**Good Luck!** 🚀

The scripts are ready and waiting for your UI setup.

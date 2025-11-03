# Unity Scene Setup Guide

This guide explains exactly how to configure the Unity scene to work with the new menu system and feature controllers.

## Overview

All C# scripts are complete and ready. This guide walks you through creating the UI hierarchy and wiring up the component references in Unity Editor.

## Prerequisites

- Unity project opened (`DecartAI-Quest-Unity`)
- Main scene loaded (`Assets/Samples/DecartAI-Quest/DecartAI-Main.unity`)
- All scripts compiled without errors

---

## Part 1: Create Menu UI Hierarchy

### Step 1: Create Main Menu Canvas

1. **Create Menu Canvas**:
   - In Hierarchy, right-click → UI → Canvas
   - Rename to `MenuCanvas`
   - In Inspector, set:
     - Render Mode: World Space
     - Position: (0, 1.5, 2) - In front of user at eye level
     - Rotation: (0, 0, 0)
     - Scale: (0.001, 0.001, 0.001)
     - Width: 1920
     - Height: 1080

2. **Configure Canvas**:
   - Add Component → Canvas Scaler
   - UI Scale Mode: Constant Pixel Size
   - Scale Factor: 1
   - Reference Pixels: 100

### Step 2: Create Main Menu Panel

1. **Create Main Menu**:
   - Right-click MenuCanvas → UI → Panel
   - Rename to `MainMenuPanel`
   - Set:
     - Anchor: Stretch both (full canvas)
     - Left, Right, Top, Bottom: 0
     - Color: Semi-transparent dark (R:0, G:0, B:0, A:200)

2. **Add Menu Title**:
   - Right-click MainMenuPanel → UI → Text - TextMeshPro
   - Rename to `MenuTitle`
   - Set text: "AI Transformation Menu"
   - Font Size: 48
   - Alignment: Center, Top
   - Position Y: -50
   - Width: 800
   - Height: 100
   - Color: White

3. **Create Menu Items Container**:
   - Right-click MainMenuPanel → Create Empty
   - Rename to `MenuItemsContainer`
   - Add Component → Vertical Layout Group
     - Spacing: 20
     - Child Alignment: Upper Center
     - Child Force Expand: Width ✅, Height ❌
   - Add Component → Content Size Fitter
     - Vertical Fit: Preferred Size
   - Set RectTransform:
     - Anchor: Center
     - Position: (0, -100, 0)
     - Width: 800
     - Height: 500

### Step 3: Create Menu Item Prefab

1. **Create Menu Item Template**:
   - Right-click MenuItemsContainer → UI → Button
   - Rename to `MenuItemPrefab`
   - Set:
     - Width: 700
     - Height: 80

2. **Configure Button**:
   - In Inspector, Button component:
     - Transition: Color Tint
     - Normal: White
     - Highlighted: Yellow
     - Pressed: Green

3. **Add Text**:
   - Right-click MenuItemPrefab → UI → Text - TextMeshPro
   - Rename to `MenuItemText`
   - Set:
     - Anchor: Stretch both
     - Left, Right, Top, Bottom: 0
     - Font Size: 24
     - Alignment: Center, Middle
     - Color: White

4. **Make it a Prefab**:
   - Drag MenuItemPrefab from Hierarchy to Project panel
   - Save to: `Assets/Samples/DecartAI-Quest/Prefabs/` (create folder if needed)
   - Delete MenuItemPrefab from Hierarchy (we'll spawn them dynamically)

---

## Part 2: Create Feature Panels

### Feature Panel Template

Each feature panel follows this structure. Create all 5:

1. **Time Travel Panel**
2. **Clothing Panel**
3. **Biome Panel**
4. **Game World Panel**
5. **Custom Prompt Panel**

### A. Time Travel Panel

1. **Create Panel**:
   - Right-click MenuCanvas → UI → Panel
   - Rename to `TimeTravelPanel`
   - Initially set Active: ❌ (unchecked)
   - Same settings as MainMenuPanel

2. **Add Title**:
   - Right-click TimeTravelPanel → UI → Text - TextMeshPro
   - Name: `TimeTravelTitle`
   - Text: "Time Travel"
   - Font Size: 42
   - Position Y: -50

3. **Add Year Slider**:
   - Right-click TimeTravelPanel → UI → Slider
   - Rename to `YearSlider`
   - Position: (0, -150, 0)
   - Width: 600
   - Height: 60
   - Configure slider:
     - Min Value: 1800
     - Max Value: 2100
     - Whole Numbers: ✅
     - Value: 2024

4. **Add Year Display**:
   - Right-click TimeTravelPanel → UI → Text - TextMeshPro
   - Name: `YearDisplayText`
   - Position Y: -100
   - Font Size: 36
   - Alignment: Center
   - Text: "Year: 2024"

5. **Add Description Text**:
   - Right-click TimeTravelPanel → UI → Text - TextMeshPro
   - Name: `DescriptionText`
   - Position Y: -250
   - Width: 700
   - Height: 200
   - Font Size: 22
   - Alignment: Center, Top
   - Text: "Use joystick to adjust year..."

6. **Add Instructions**:
   - Right-click TimeTravelPanel → UI → Text - TextMeshPro
   - Name: `InstructionsText`
   - Position Y: -450
   - Font Size: 20
   - Color: Light Gray
   - Text: "Joystick Left/Right: Adjust | Right Trigger: Apply | Left Trigger: Back"

### B. Clothing Panel

1. **Create Panel**:
   - Right-click MenuCanvas → UI → Panel
   - Rename to `ClothingPanel`
   - Initially set Active: ❌

2. **Add Scroll View**:
   - Right-click ClothingPanel → UI → Scroll View
   - Rename to `ClothingScrollView`
   - Position: (0, -100, 0)
   - Width: 800
   - Height: 600

3. **Configure Scroll View**:
   - Delete Horizontal Scrollbar
   - Keep Vertical Scrollbar
   - In Content (child of Viewport):
     - Add Component → Vertical Layout Group
     - Spacing: 10
     - Add Component → Content Size Fitter
     - Vertical Fit: Preferred Size

4. **Set Container Reference**:
   - The `Content` child is what we'll reference as `clothingListContainer`

5. **Add Selected Text**:
   - Right-click ClothingPanel → UI → Text - TextMeshPro
   - Name: `SelectedClothingText`
   - Position Y: 400
   - Font Size: 28
   - Text: "Selected: [None]"

### C. Biome Panel

Same structure as Clothing Panel:

1. Create `BiomePanel`
2. Add Scroll View → `BiomeScrollView`
3. Configure Content as `biomeListContainer`
4. Add `SelectedBiomeText`
5. Add `CategoryText` (shows category)

### D. Game World Panel

Same structure as Clothing Panel:

1. Create `GameWorldPanel`
2. Add Scroll View → `GameWorldScrollView`
3. Configure Content as `gameListContainer`
4. Add `SelectedGameText`
5. Add `DescriptionText`

### E. Custom Prompt Panel

1. **Create Panel**:
   - Right-click MenuCanvas → UI → Panel
   - Rename to `CustomPromptPanel`
   - Initially set Active: ❌

2. **Add Title**:
   - Right-click CustomPromptPanel → UI → Text - TextMeshPro
   - Name: `CustomPromptTitle`
   - Text: "Custom Prompt"
   - Font Size: 42
   - Position Y: -50

3. **Add Instruction Text**:
   - Right-click CustomPromptPanel → UI → Text - TextMeshPro
   - Name: `InstructionText`
   - Position Y: -120
   - Width: 800
   - Height: 100
   - Font Size: 20
   - Text: "Enter your custom transformation prompt..."

4. **Add Input Field**:
   - Right-click CustomPromptPanel → UI → Input Field - TextMeshPro
   - Rename to `PromptInputField`
   - Position: (0, -250, 0)
   - Width: 700
   - Height: 200
   - Configure:
     - Line Type: Multi Line Submit
     - Character Limit: 500
     - Placeholder: "E.g., Transform to magical forest..."

5. **Add Submit Button**:
   - Right-click CustomPromptPanel → UI → Button
   - Rename to `SubmitButton`
   - Position: (-150, -400, 0)
   - Width: 200
   - Height: 60
   - Button Text: "Submit (Right Trigger)"

6. **Add Clear Button**:
   - Right-click CustomPromptPanel → UI → Button
   - Rename to `ClearButton`
   - Position: (150, -400, 0)
   - Width: 200
   - Height: 60
   - Button Text: "Clear"

7. **Add Status Text**:
   - Right-click CustomPromptPanel → UI → Text - TextMeshPro
   - Name: `StatusText`
   - Position Y: -480
   - Font Size: 18
   - Color: Yellow

8. **Add Recent Prompts Area**:
   - Right-click CustomPromptPanel → UI → Panel
   - Name: `RecentPromptsArea`
   - Position Y: 350
   - Width: 800
   - Height: 150
   - Add child → Text - TextMeshPro
     - Name: `RecentPromptsLabel`
     - Text: "Recent Prompts:"
     - Position Y: 50
   - Add child → Empty GameObject
     - Name: `RecentPromptsContainer`
     - Add Vertical Layout Group
     - This will hold recent prompt items

---

## Part 3: Wire Up Components

### Step 1: Add MenuManager to Scene

1. **Create MenuManager GameObject**:
   - In Hierarchy, right-click → Create Empty
   - Rename to `MenuManager`
   - Position: (0, 0, 0)

2. **Add MenuManager Script**:
   - In Inspector, click Add Component
   - Type "MenuManager"
   - Select it

3. **Configure References**:
   - **Menu Root Object**: Drag `MenuCanvas` here
   - **Menu Items Container**: Drag `MenuItemsContainer` from MainMenuPanel
   - **Menu Title Text**: Drag `MenuTitle`
   - **Menu Item Prefab**: Drag the MenuItemPrefab from Project folder
   - **Time Travel Panel**: Drag `TimeTravelPanel`
   - **Clothing Panel**: Drag `ClothingPanel`
   - **Biome Panel**: Drag `BiomePanel`
   - **Game World Panel**: Drag `GameWorldPanel`
   - **Custom Prompt Panel**: Drag `CustomPromptPanel`

### Step 2: Add Feature Controllers

#### Time Travel Controller

1. **Add to Scene**:
   - Select `TimeTravelPanel`
   - Add Component → TimeTravelController

2. **Configure**:
   - **Year Slider**: Drag the Slider from TimeTravelPanel
   - **Year Display Text**: Drag YearDisplayText
   - **Description Text**: Drag DescriptionText
   - **Web RTC Connection**: Find and drag the WebRTCConnection GameObject from scene

#### Clothing Controller

1. **Add to Scene**:
   - Select `ClothingPanel`
   - Add Component → ClothingTryOnController

2. **Configure**:
   - **Clothing List Container**: Drag the `Content` from ClothingScrollView
   - **Clothing Item Prefab**: Use the same MenuItemPrefab (or create specific one)
   - **Selected Clothing Text**: Drag SelectedClothingText
   - **Web RTC Connection**: Drag WebRTCConnection

#### Biome Controller

1. **Add to Scene**:
   - Select `BiomePanel`
   - Add Component → BiomeTransformController

2. **Configure**:
   - **Biome List Container**: Drag the `Content` from BiomeScrollView
   - **Biome Item Prefab**: Use MenuItemPrefab
   - **Selected Biome Text**: Drag SelectedBiomeText
   - **Category Text**: Drag CategoryText
   - **Web RTC Connection**: Drag WebRTCConnection

#### Game World Controller

1. **Add to Scene**:
   - Select `GameWorldPanel`
   - Add Component → GameWorldController

2. **Configure**:
   - **Game List Container**: Drag the `Content` from GameWorldScrollView
   - **Game Item Prefab**: Use MenuItemPrefab
   - **Selected Game Text**: Drag SelectedGameText
   - **Description Text**: Drag DescriptionText (if created)
   - **Web RTC Connection**: Drag WebRTCConnection

#### Custom Prompt Controller

1. **Add to Scene**:
   - Select `CustomPromptPanel`
   - Add Component → CustomPromptController

2. **Configure**:
   - **Prompt Input Field**: Drag PromptInputField (TMP_InputField)
   - **Instruction Text**: Drag InstructionText
   - **Status Text**: Drag StatusText
   - **Submit Button**: Drag SubmitButton
   - **Clear Button**: Drag ClearButton
   - **Recent Prompts Container**: Drag RecentPromptsContainer
   - **Recent Prompt Item Prefab**: Use MenuItemPrefab (or create specific one)
   - **Max Recent Prompts**: 5
   - **Web RTC Connection**: Drag WebRTCConnection

---

## Part 4: Testing in Unity Editor

### Test Checklist

1. **Play Mode Test**:
   - Press Play in Unity
   - Menu should be visible
   - All 5 options should appear in MainMenu

2. **Navigation Test**:
   - Cannot test OVR input in editor
   - But scripts should compile without errors
   - Check Console for any runtime errors

3. **Reference Verification**:
   - Check that no Inspector fields show "None" or "Missing"
   - All required references should be assigned

### Common Issues

**"NullReferenceException"**:
- Check all Inspector references are assigned
- Verify WebRTCConnection exists in scene

**"Menu items not appearing"**:
- Check MenuItemPrefab is assigned
- Check MenuItemsContainer has Vertical Layout Group

**"Panels not switching"**:
- Verify all panels start with Active = false except MainMenuPanel
- Check MenuManager has all panel references

---

## Part 5: Building for Quest

### Pre-Build Checklist

- ✅ All scripts compile without errors
- ✅ All Inspector references assigned
- ✅ All panels configured correctly
- ✅ MenuManager added to scene
- ✅ Feature controllers added to respective panels
- ✅ WebRTCConnection reference set in all controllers

### Build Steps

1. **Save Scene**: File → Save (Ctrl+S)
2. **File → Build Settings**
3. **Add Open Scenes** (if not already in list)
4. **Platform**: Android (should already be switched)
5. **Build and Run** (Quest 3 connected)

---

## Part 6: On-Device Testing

### First Launch

1. Grant camera permissions
2. Menu should appear in front of you
3. Test hamburger button (Start) to hide/show menu

### Navigation Testing

Use these Quest 3 controls:

- **Joystick Up/Down**: Should navigate menu items
- **Right Trigger**: Should enter selected feature
- **Left Trigger**: Should go back to main menu
- **Hamburger/Start**: Should toggle menu visibility

### Feature Testing

Test each feature:

1. **Time Travel**:
   - Joystick left/right changes year
   - Right trigger applies transformation
   
2. **Clothing**:
   - Joystick up/down scrolls clothing list
   - Right trigger applies selected outfit
   
3. **Biome**:
   - Joystick up/down scrolls locations
   - Right trigger applies transformation
   
4. **Game World**:
   - Joystick up/down scrolls game styles
   - Right trigger applies transformation
   
5. **Custom Prompt**:
   - Touch input field with controller laser
   - Quest keyboard should appear
   - Type prompt
   - Right trigger or A button submits

---

## Troubleshooting

### Menu Not Visible

**Check**:
- MenuCanvas Render Mode is World Space
- Position is (0, 1.5, 2) - in front of camera
- Canvas is Active in hierarchy
- Camera can see it (not behind user)

### Controllers Not Working

**Check**:
- OVR Input is working (test in existing app features)
- Scripts are attached to correct GameObjects
- No compile errors in Console

### Transformations Not Working

**Check**:
- WebRTCConnection reference is set
- Internet connection is active
- WebRTC is connected (check existing app functionality)

### UI Too Big/Small

**Adjust**:
- Canvas Scale (currently 0.001)
- Increase to 0.002 for bigger UI
- Decrease to 0.0005 for smaller UI

### Text Not Readable

**Fix**:
- Increase font sizes
- Use higher contrast colors
- Add background panels behind text

---

## Visual Design Tips

### Colors

Recommended color scheme:
- **Background**: Dark semi-transparent (R:0, G:0, B:0, A:180)
- **Text**: White or Light Gray
- **Selected Item**: Yellow or Cyan
- **Buttons**: Blue normal, Green pressed
- **Warnings/Status**: Yellow

### Fonts

- **Titles**: 40-48pt
- **Menu Items**: 24-28pt
- **Body Text**: 20-22pt
- **Instructions**: 18-20pt

### Spacing

- **Between menu items**: 15-20 units
- **Padding**: 20-30 units from edges
- **Button size**: 60-80 units height

---

## Optional Enhancements

### Loading Indicator

Add a loading spinner when AI is processing:

1. Create UI → Image (spinner)
2. Add Rotate script
3. Show when prompt sent
4. Hide when transformation received

### Sound Effects

Add audio feedback:

1. Menu navigation sound (tick)
2. Selection confirm sound (ding)
3. Transformation applied sound (whoosh)

### Visual Effects

1. Fade transitions between panels
2. Highlight animation on selected item
3. Particle effects when transformation applied

---

## Summary

You now have:

1. ✅ Complete C# implementation
2. ✅ Detailed Unity setup instructions
3. ✅ Testing procedures
4. ✅ Troubleshooting guide

**Next Steps**:

1. Open Unity Editor
2. Follow Part 1-3 to create UI
3. Follow Part 4 to test in editor
4. Follow Part 5 to build
5. Follow Part 6 to test on device

The app is ready to build once the Unity scene is configured!

---

**Questions?** Refer to:
- `COMPLETE_BEGINNERS_GUIDE.md` for general Unity help
- `FEATURE_IMPLEMENTATION.md` for code details
- `QUICK_REFERENCE.md` for controls

**Last Updated**: 2025-10-26

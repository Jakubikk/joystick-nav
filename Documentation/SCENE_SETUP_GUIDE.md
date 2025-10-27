# Unity Scene Setup Instructions
## Configuring the Menu System in Unity Editor

This guide explains how to set up the UI and menu system in the Unity Editor after all scripts have been created.

---

## Prerequisites

- Unity 6 installed and project opened
- All scripts committed and present in `Assets/Samples/DecartAI-Quest/Scripts/`
- Main scene: `Assets/Samples/DecartAI-Quest/DecartAI-Main.unity` opened

---

## Part 1: Create Menu System GameObject

### Step 1: Create MenuSystem Object

1. In **Hierarchy** panel, right-click → **Create Empty**
2. Rename to **"MenuSystem"**
3. In **Inspector**, click **Add Component**
4. Type **"MenuSystem"** and select it from the list

---

## Part 2: Create UI Canvas and Panels

### Step 2: Create Main Menu Panel

1. Find **MainCanvas** in Hierarchy (should already exist)
2. Right-click **MainCanvas** → **UI** → **Panel**
3. Rename to **"MenuPanel"**
4. In Inspector, set **Rect Transform**:
   - Anchor: Center
   - Position: X=0, Y=0, Z=0
   - Width: 800
   - Height: 600

### Step 3: Create Title Text

1. Right-click **MenuPanel** → **UI** → **Text - TextMeshPro**
   - If prompted, click **Import TMP Essentials**
2. Rename to **"TitleText"**
3. In Inspector:
   - **Text**: "Main Menu"
   - **Font Size**: 48
   - **Alignment**: Center, Top
   - **Color**: White
   - **Rect Transform**:
     - Anchor: Top-Center
     - Position: Y=-50
     - Width: 700
     - Height: 80

### Step 4: Create Menu Items Container

1. Right-click **MenuPanel** → **Create Empty**
2. Rename to **"MenuItemsContainer"**
3. In Inspector, click **Add Component** → **Vertical Layout Group**
4. Configure **Vertical Layout Group**:
   - ✅ Control Child Size → Width
   - ✅ Child Force Expand → Width
   - Spacing: 10
   - Padding: Left=50, Right=50, Top=100, Bottom=50
5. Set **Rect Transform**:
   - Anchor: Stretch-Stretch
   - Left: 50, Right: -50
   - Top: -100, Bottom: 50

---

## Part 3: Create Feature Panels

### Step 5: Create Feature Panels

Create **5 panels** for each feature. For each panel:

1. Right-click **MenuPanel** → **UI** → **Panel**
2. Rename according to feature:
   - **TimeTravelPanel**
   - **VirtualMirrorPanel**
   - **BiomePanel**
   - **VideoGamePanel**
   - **CustomPromptPanel**
3. Set each panel **active = false** (uncheck box in Inspector)
4. For each panel, set **Rect Transform**:
   - Anchor: Stretch-Stretch
   - Left: 0, Right: 0, Top: 0, Bottom: 0

---

## Part 4: Time Travel Panel Setup

### Step 6: Add Year Slider

1. Right-click **TimeTravelPanel** → **UI** → **Slider**
2. Rename to **"YearSlider"**
3. In Inspector:
   - **Min Value**: 1800
   - **Max Value**: 2200
   - **Value**: 2024
   - **Whole Numbers**: ✅ Checked
4. Set **Rect Transform**:
   - Anchor: Center
   - Position: Y=0
   - Width: 600
   - Height: 50

### Step 7: Add Year Display Text

1. Right-click **TimeTravelPanel** → **UI** → **Text - TextMeshPro**
2. Rename to **"YearDisplayText"**
3. In Inspector:
   - **Text**: "Year: 2024"
   - **Font Size**: 36
   - **Alignment**: Center, Middle
   - **Color**: White
4. Set **Rect Transform**:
   - Anchor: Center
   - Position: Y=100
   - Width: 400
   - Height: 60

---

## Part 5: Other Feature Panels

### Step 8: Setup Remaining Panels

For **VirtualMirrorPanel**, **BiomePanel**, and **VideoGamePanel**:

1. Right-click panel → **Create Empty**
2. Rename to **"OptionsContainer"** (or similar descriptive name)
3. Add **Vertical Layout Group** component
4. Configure similar to MenuItemsContainer (Step 4)

For **CustomPromptPanel**:

1. Right-click **CustomPromptPanel** → **UI** → **Input Field - TextMeshPro**
2. Rename to **"CustomPromptInput"**
3. In Inspector:
   - **Placeholder**: "Enter custom prompt..."
   - **Character Limit**: 200
   - **Line Type**: Multi Line Submit
4. Set **Rect Transform**:
   - Anchor: Center
   - Width: 600
   - Height: 100

---

## Part 6: Create Menu Item Prefab

### Step 9: Create Menu Item Template

1. Right-click in **Hierarchy** → **UI** → **Panel**
2. Rename to **"MenuItemPrefab"**
3. Add **Image** component (should already have one)
   - **Color**: RGB(50, 50, 50, 200)
4. Right-click **MenuItemPrefab** → **UI** → **Text - TextMeshPro**
5. Rename child to **"ItemText"**
6. Configure **ItemText**:
   - **Font Size**: 24
   - **Alignment**: Left, Middle
   - **Color**: White
   - **Rect Transform**: Stretch-Stretch with padding
7. Set **MenuItemPrefab Rect Transform**:
   - Height: 60

### Step 10: Convert to Prefab

1. Create folder: `Assets/Samples/DecartAI-Quest/Prefabs/` (if doesn't exist)
2. Drag **MenuItemPrefab** from Hierarchy to Prefabs folder
3. Delete **MenuItemPrefab** from Hierarchy (prefab is saved)

---

## Part 7: Wire Up MenuSystem Component

### Step 11: Assign References in MenuSystem

1. Select **MenuSystem** GameObject in Hierarchy
2. In Inspector, find **MenuSystem (Script)** component
3. Drag and drop references:

**Menu References**:
- **Menu Panel**: Drag **MenuPanel**
- **Title Text**: Drag **TitleText**
- **Menu Items Container**: Drag **MenuItemsContainer**
- **Menu Item Prefab**: Drag prefab from Project panel

**Feature Panels**:
- **Time Travel Panel**: Drag **TimeTravelPanel**
- **Virtual Mirror Panel**: Drag **VirtualMirrorPanel**
- **Biome Panel**: Drag **BiomePanel**
- **Video Game Panel**: Drag **VideoGamePanel**
- **Custom Prompt Panel**: Drag **CustomPromptPanel**

**Time Travel UI**:
- **Year Slider**: Drag **YearSlider**
- **Year Display Text**: Drag **YearDisplayText**

**Container References** (if created):
- **Clothing Options Container**: Drag from VirtualMirrorPanel
- **Biome Options Container**: Drag from BiomePanel
- **Game Style Options Container**: Drag from VideoGamePanel

**Custom Prompt UI**:
- **Custom Prompt Input**: Drag **CustomPromptInput**

**References**:
- **Web RTC Controller**: Find and drag **WebRTCController** object from scene

---

## Part 8: Setup KeyboardInputManager (Optional)

### Step 12: Add KeyboardInputManager

1. In Hierarchy, right-click → **Create Empty**
2. Rename to **"KeyboardInputManager"**
3. Add **KeyboardInputManager** script component
4. In Inspector:
   - **Input Field**: Drag **CustomPromptInput**
   - **Keyboard Panel**: Drag **CustomPromptPanel**

---

## Part 9: Configure Canvas Settings

### Step 13: Adjust MainCanvas for VR

1. Select **MainCanvas** in Hierarchy
2. In Inspector, find **Canvas** component:
   - **Render Mode**: World Space
   - **Event Camera**: Drag **Main Camera** (or OVR CameraRig camera)
3. Set **Canvas Rect Transform**:
   - Position: X=0, Y=1.5, Z=2 (in front of user)
   - Rotation: X=0, Y=0, Z=0
   - Scale: X=0.001, Y=0.001, Z=0.001

### Step 14: Add Canvas Scaler (Optional but Recommended)

1. With **MainCanvas** selected
2. Add **Canvas Scaler** component
3. Set:
   - **UI Scale Mode**: Scale With Screen Size
   - **Reference Resolution**: 1920 x 1080
   - **Match**: 0.5

---

## Part 10: Initial State Configuration

### Step 15: Set Initial States

1. **MenuPanel**: ✅ Active
2. **TimeTravelPanel**: ❌ Inactive
3. **VirtualMirrorPanel**: ❌ Inactive
4. **BiomePanel**: ❌ Inactive
5. **VideoGamePanel**: ❌ Inactive
6. **CustomPromptPanel**: ❌ Inactive

---

## Part 11: Save and Test

### Step 16: Save Everything

1. **File** → **Save** (Ctrl+S / Cmd+S)
2. Save scene when prompted

### Step 17: Test in Unity

1. Click **Play** button
2. Menu should appear
3. Test with keyboard inputs (simulate controller in editor)

---

## Part 12: Build and Deploy

### Step 18: Build for Quest

1. **File** → **Build Settings**
2. Ensure **Android** is selected
3. Click **Build and Run** or use build automation:
   ```bash
   python Documentation/build_automation.py
   ```

Or from Unity menu:
- **Build** → **Quick Build Quest APK**

---

## Visual Hierarchy

When complete, your hierarchy should look like:

```
MainCanvas
├── MenuPanel (Active)
│   ├── TitleText
│   ├── MenuItemsContainer
│   ├── TimeTravelPanel (Inactive)
│   │   ├── YearSlider
│   │   └── YearDisplayText
│   ├── VirtualMirrorPanel (Inactive)
│   │   └── OptionsContainer
│   ├── BiomePanel (Inactive)
│   │   └── OptionsContainer
│   ├── VideoGamePanel (Inactive)
│   │   └── OptionsContainer
│   └── CustomPromptPanel (Inactive)
│       └── CustomPromptInput
├── [Other existing elements]
MenuSystem (separate GameObject)
KeyboardInputManager (separate GameObject)
```

---

## Troubleshooting

### Menu doesn't appear
- Check MainCanvas is active
- Verify MenuPanel is active
- Check Canvas is set to World Space
- Ensure Canvas is positioned in front of camera

### References missing
- Re-drag all references in MenuSystem component
- Check that all objects exist in scene
- Verify script compilation (no errors in Console)

### Slider doesn't work
- Check YearSlider Min/Max values
- Verify Whole Numbers is checked
- Test in Play mode

### TextMeshPro errors
- Import TMP Essentials when prompted
- Window → TextMeshPro → Import TMP Essential Resources

---

## Quick Reference Card

**Essential References to Assign**:
1. MenuPanel → MenuSystem
2. TitleText → MenuSystem
3. MenuItemsContainer → MenuSystem
4. All 5 feature panels → MenuSystem
5. YearSlider → MenuSystem
6. YearDisplayText → MenuSystem
7. CustomPromptInput → MenuSystem
8. WebRTCController → MenuSystem

---

## Next Steps

After scene setup:
1. Test in Unity Editor Play mode
2. Build APK for Quest
3. Install and test on actual hardware
4. Adjust UI positioning for comfort in VR
5. Fine-tune colors and sizing as needed

---

**Scene setup complete! Ready for building and testing on Quest 3!** 🎉

For questions: Check [COMPLETE_BEGINNER_GUIDE.md](COMPLETE_BEGINNER_GUIDE.md)

---

*Last updated: October 2025*

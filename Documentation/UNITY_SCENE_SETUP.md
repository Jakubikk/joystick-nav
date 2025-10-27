# Unity Scene Configuration Guide

This guide explains how to configure the Unity scene to set up the menu system and all feature panels in the Unity Editor.

**Note**: These steps must be performed in Unity Editor after the scripts are in place.

---

## Prerequisites

- Unity project opened in Unity Editor
- All scripts (.cs files) imported without errors
- Main scene (DecartAI-Main.unity) loaded

---

## Step 1: Create Main Menu Canvas

### 1.1 Create Canvas GameObject

1. In **Hierarchy** panel, right-click → **UI** → **Canvas**
2. Rename to **"MenuCanvas"**
3. Select MenuCanvas
4. In **Inspector**, configure **Canvas** component:
   - **Render Mode**: `World Space`
   - **Position**: X=`0`, Y=`1.5`, Z=`2`
   - **Rotation**: X=`0`, Y=`0`, Z=`0`
   - **Scale**: X=`0.001`, Y=`0.001`, Z=`0.001`

### 1.2 Configure Rect Transform

With MenuCanvas selected:
- **Width**: `2000`
- **Height**: `1500`

### 1.3 Add Canvas Scaler (Optional but Recommended)

1. With MenuCanvas selected, click **Add Component**
2. Add **Canvas Scaler**
3. Settings:
   - **UI Scale Mode**: `Constant Pixel Size`
   - **Scale Factor**: `1`

---

## Step 2: Create Menu Title Text

1. Right-click **MenuCanvas** → **UI** → **Text - TextMeshPro**
2. If prompted "Import TMP Essentials", click **Import TMP Essentials**, wait for completion
3. Rename to **"MenuTitleText"**
4. Configure in Inspector:
   - **Text**: `Decart XR - Main Menu`
   - **Font Size**: `80`
   - **Alignment**: Center Horizontal, Top Vertical
   - **Color**: White (255, 255, 255, 255)
   - **Rect Transform**:
     - **Anchor**: Top center
     - **Pos X**: `0`
     - **Pos Y**: `-100`
     - **Width**: `1800`
     - **Height**: `150`

---

## Step 3: Create Menu Items Container

1. Right-click **MenuCanvas** → **Create Empty**
2. Rename to **"MenuItemsContainer"**
3. With MenuItemsContainer selected:
   - Click **Add Component** → **Vertical Layout Group**
   - Configure **Vertical Layout Group**:
     - **Spacing**: `20`
     - **Child Alignment**: Upper Center
     - ✅ Check **Child Force Expand Width**
     - ✅ Check **Child Force Expand Height**
   - Configure **Rect Transform**:
     - **Anchor**: Center
     - **Pos X**: `0`
     - **Pos Y**: `0`
     - **Width**: `1600`
     - **Height**: `1000`

---

## Step 4: Create Time Travel Panel

### 4.1 Create Panel

1. Right-click **MenuCanvas** → **UI** → **Panel**
2. Rename to **"TimeTravelPanel"**
3. Configure **Rect Transform**:
   - Click anchor preset (small square icon)
   - Hold **Alt+Shift**, click bottom-right preset (stretch both)
   - Set all margins to `0` (Left, Right, Top, Bottom)
4. Configure **Image** component:
   - **Color**: Black with alpha ~200 (0, 0, 0, 200)
5. **Disable** the panel (uncheck box next to name in Inspector)

### 4.2 Add Year Slider

1. Right-click **TimeTravelPanel** → **UI** → **Slider**
2. Rename to **"YearSlider"**
3. Configure **Rect Transform**:
   - **Anchor**: Middle center
   - **Pos X**: `0`
   - **Pos Y**: `100`
   - **Width**: `1200`
   - **Height**: `60`
4. Configure **Slider** component:
   - **Min Value**: `1800`
   - **Max Value**: `2200`
   - **Whole Numbers**: ✅ Checked
   - **Value**: `2020`

### 4.3 Add Year Display Text

1. Right-click **TimeTravelPanel** → **UI** → **Text - TextMeshPro**
2. Rename to **"YearText"**
3. Configure:
   - **Text**: `Year: 2020`
   - **Font Size**: `60`
   - **Alignment**: Center
   - **Color**: White
   - **Rect Transform**:
     - **Pos X**: `0`
     - **Pos Y**: `200`
     - **Width**: `600`
     - **Height**: `100`

### 4.4 Add Description Text

1. Right-click **TimeTravelPanel** → **UI** → **Text - TextMeshPro**
2. Rename to **"DescriptionText"**
3. Configure:
   - **Text**: `Modern Day`
   - **Font Size**: `40`
   - **Alignment**: Center
   - **Color**: Light Gray (200, 200, 200, 255)
   - **Rect Transform**:
     - **Pos X**: `0`
     - **Pos Y**: `-50`
     - **Width**: `1400`
     - **Height**: `200`
   - Enable **Word Wrapping**

### 4.5 Add Apply Button

1. Right-click **TimeTravelPanel** → **UI** → **Button - TextMeshPro**
2. Rename to **"ApplyButton"**
3. Find child **Text (TMP)** and set text to: `Apply Time Period`
4. Configure button:
   - **Rect Transform**:
     - **Pos X**: `0`
     - **Pos Y**: `-300`
     - **Width**: `400`
     - **Height**: `80`

---

## Step 5: Create Try-On Panel

### 5.1 Create Panel

1. Right-click **MenuCanvas** → **UI** → **Panel**
2. Rename to **"TryOnPanel"**
3. Configure like TimeTravelPanel (stretched, semi-transparent black)
4. **Disable** the panel

### 5.2 Add Scroll View

1. Right-click **TryOnPanel** → **UI** → **Scroll View**
2. Rename to **"ClothingScrollView"**
3. Configure **Rect Transform**:
   - **Anchor**: Stretch both
   - Margins: Left=`100`, Right=`100`, Top=`200`, Bottom=`200`
4. Find child **Viewport** → **Content**
5. Rename Content to **"ClothingListContainer"**
6. Configure **Content** (ClothingListContainer):
   - Add **Vertical Layout Group** component:
     - **Spacing**: `10`
     - **Child Alignment**: Upper Left
     - ✅ **Child Force Expand Width**
     - ❌ **Child Force Expand Height**
   - Add **Content Size Fitter** component:
     - **Vertical Fit**: `Preferred Size`

### 5.3 Add Instruction Text

1. Right-click **TryOnPanel** → **UI** → **Text - TextMeshPro**
2. Rename to **"InstructionText"**
3. Configure:
   - **Text**: `Stand in front of a mirror. Navigate with joystick, select with Right Trigger.`
   - **Font Size**: `35`
   - **Alignment**: Center
   - **Rect Transform**:
     - **Pos Y**: `650`
     - **Width**: `1600`
     - **Height**: `150`

---

## Step 6: Create Biome Panel

### 6.1 Create Panel (Same as Try-On)

1. Right-click **MenuCanvas** → **UI** → **Panel**
2. Rename to **"BiomePanel"**
3. Configure like previous panels (stretched, semi-transparent)
4. **Disable** the panel

### 6.2 Add Scroll View and Container

1. Right-click **BiomePanel** → **UI** → **Scroll View**
2. Setup like TryOnPanel
3. Rename Content to **"BiomeListContainer"**
4. Add **Vertical Layout Group** and **Content Size Fitter**

### 6.3 Add Description Text

1. Right-click **BiomePanel** → **UI** → **Text - TextMeshPro**
2. Rename to **"BiomeDescriptionText"**
3. Configure:
   - **Font Size**: `35`
   - **Alignment**: Center
   - **Word Wrapping**: Enabled
   - **Rect Transform**:
     - **Pos Y**: `600`
     - **Width**: `1600`
     - **Height**: `200`

---

## Step 7: Create Video Game Panel

Follow same pattern as Biome Panel:
1. Create **VideoGamePanel** (panel + scroll view)
2. Create **GameListContainer** (content with layout)
3. Create **GameDescriptionText** (description display)
4. **Disable** the panel

---

## Step 8: Create Custom Prompt Panel

### 8.1 Create Panel

1. Right-click **MenuCanvas** → **UI** → **Panel**
2. Rename to **"CustomPromptPanel"**
3. Configure and **disable**

### 8.2 Add Input Field

1. Right-click **CustomPromptPanel** → **UI** → **Input Field - TextMeshPro**
2. Rename to **"PromptInputField"**
3. Configure:
   - **Character Limit**: `500`
   - **Rect Transform**:
     - **Pos Y**: `200`
     - **Width**: `1400`
     - **Height**: `200`
   - Find child **Placeholder** text:
     - Set text: `Enter your custom transformation prompt...`

### 8.3 Add Buttons

Create three buttons:

**Open Keyboard Button:**
- Position: Y=`0`
- Text: `Open Keyboard (Right Trigger)`

**Submit Button:**
- Position: Y=`-150`
- Text: `Submit (A Button)`

**Clear Button:**
- Position: Y=`-300`
- Text: `Clear (B Button)`

### 8.4 Add Status Text

1. Add Text - TextMeshPro for **"StatusText"**
2. Position at bottom
3. Font size: `30`

### 8.5 Add Instruction Text

1. Add Text - TextMeshPro for **"InstructionText"**
2. Position at top
3. Multi-line instructions

---

## Step 9: Attach Scripts to GameObjects

### 9.1 Attach MenuManager to MenuCanvas

1. Select **MenuCanvas**
2. **Add Component** → type `MenuManager` → select it
3. In Inspector, assign all references:
   - **Menu Canvas**: Drag **MenuCanvas** here
   - **Menu Title Text**: Drag **MenuTitleText**
   - **Menu Items Container**: Drag **MenuItemsContainer**
   - **Time Travel Panel**: Drag **TimeTravelPanel**
   - **Try On Panel**: Drag **TryOnPanel**
   - **Biome Panel**: Drag **BiomePanel**
   - **Video Game Panel**: Drag **VideoGamePanel**
   - **Custom Prompt Panel**: Drag **CustomPromptPanel**
   - **Web RTC Connection**: Find existing **WebRTCConnection** in scene, drag it

### 9.2 Attach TimeTravelFeature

1. Select **TimeTravelPanel**
2. **Add Component** → `TimeTravelFeature`
3. Assign references:
   - **Year Slider**: Drag YearSlider
   - **Year Text**: Drag YearText
   - **Description Text**: Drag DescriptionText
   - **Apply Button**: Drag ApplyButton
   - **Web RTC Connection**: Drag WebRTCConnection from scene

### 9.3 Attach TryOnFeature

1. Select **TryOnPanel**
2. **Add Component** → `TryOnFeature`
3. Assign:
   - **Clothing List Container**: ClothingListContainer
   - **Instruction Text**: InstructionText
   - **Web RTC Connection**: WebRTCConnection

### 9.4 Attach BiomeFeature

1. Select **BiomePanel**
2. **Add Component** → `BiomeFeature`
3. Assign:
   - **Biome List Container**: BiomeListContainer
   - **Description Text**: BiomeDescriptionText
   - **Web RTC Connection**: WebRTCConnection

### 9.5 Attach VideoGameFeature

1. Select **VideoGamePanel**
2. **Add Component** → `VideoGameFeature`
3. Assign:
   - **Game List Container**: GameListContainer
   - **Description Text**: GameDescriptionText
   - **Web RTC Connection**: WebRTCConnection

### 9.6 Attach CustomPromptFeature

1. Select **CustomPromptPanel**
2. **Add Component** → `CustomPromptFeature`
3. Assign all UI elements:
   - **Prompt Input Field**: PromptInputField
   - **Open Keyboard Button**: OpenKeyboardButton
   - **Submit Button**: SubmitButton
   - **Clear Button**: ClearButton
   - **Status Text**: StatusText
   - **Instruction Text**: InstructionText
   - **Web RTC Connection**: WebRTCConnection

---

## Step 10: Final Scene Configuration

### 10.1 Verify WebRTCConnection GameObject

Ensure the existing WebRTCConnection GameObject is properly configured and active in the scene.

### 10.2 Set Canvas Camera (if needed)

If using Screen Space - Camera:
1. Select MenuCanvas
2. In Canvas component, set **Render Camera** to main VR camera

### 10.3 Configure Event System

Unity should auto-create an **EventSystem** GameObject when you create UI.
If missing:
1. Right-click Hierarchy → **UI** → **Event System**

### 10.4 Save the Scene

Press **Ctrl+S** (Windows) or **Cmd+S** (macOS) to save all changes.

---

## Step 11: Test in Editor (Optional)

1. Press **Play** button in Unity
2. Check Console for errors
3. Verify no null reference exceptions
4. Stop Play mode
5. Fix any issues before building

---

## Common Issues

**Problem**: Scripts not showing in Add Component
- **Solution**: Check Console for compilation errors, fix them first

**Problem**: TextMeshPro not available
- **Solution**: Import TMP Essentials when prompted

**Problem**: UI not visible in scene
- **Solution**: 
  - Check Canvas is in World Space mode
  - Verify position is in front of camera (Z=2)
  - Check canvas is active (checkbox enabled)

**Problem**: References won't assign
- **Solution**:
  - Make sure GameObject names match exactly
  - GameObjects must be in same scene
  - Check object is not disabled/destroyed

**Problem**: Menu doesn't appear in build
- **Solution**:
  - Verify all panels are children of MenuCanvas
  - Check MenuCanvas is active in scene
  - Ensure scene is added to Build Settings

---

## Verification Checklist

Before building, verify:

- [ ] MenuCanvas exists and is configured
- [ ] All 5 feature panels created and disabled
- [ ] MenuManager script attached to MenuCanvas
- [ ] All feature scripts attached to respective panels
- [ ] All Inspector references assigned (no "None" values)
- [ ] WebRTCConnection referenced in all feature scripts
- [ ] Scene saved
- [ ] No errors in Console
- [ ] Test Play mode works without errors

---

## Next Steps

After completing Unity Editor setup:

1. Build APK (File → Build Settings → Build)
2. Deploy to Quest 3
3. Test all features
4. Adjust UI positioning/sizing as needed
5. Fine-tune colors and fonts

---

## Notes

- **World Space Canvas**: Allows VR users to see UI naturally in 3D space
- **Position Z=2**: Places menu 2 meters in front of user at app start
- **Scale 0.001**: Converts Unity units to appropriate VR scale
- **Disabled Panels**: MenuManager enables panels dynamically when selected

---

*For complete beginner guide, see [COMPLETE_SETUP_GUIDE.md](COMPLETE_SETUP_GUIDE.md)*  
*For feature details, see [FEATURES_GUIDE.md](FEATURES_GUIDE.md)*

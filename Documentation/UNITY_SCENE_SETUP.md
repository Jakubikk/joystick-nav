# Unity Scene Setup Instructions

This document provides detailed instructions for setting up the new features in the Unity scene. Follow these steps after the scripts have been created.

## Prerequisites

- All controller scripts are in: `Assets/Samples/DecartAI-Quest/Scripts/`
- Unity 6 (6000.0.34f1) is installed
- Project is already open in Unity

## Step 1: Create UI Panels

### 1.1 Main Menu Panel

1. In **Hierarchy**, find or create a **Canvas** named `MainMenuCanvas`
2. Right-click on the Canvas → **UI** → **Panel**
3. Rename it to `MainMenuPanel`
4. In the Inspector:
   - Set **Rect Transform** → **Anchor Presets** → **Stretch** (both width and height)
   - Set **Image** → **Color** to semi-transparent (R:0, G:0, B:0, A:200)

5. Add 5 buttons to MainMenuPanel:
   - Right-click MainMenuPanel → **UI** → **Button - TextMeshPro**
   - Rename buttons:
     - `TimeTravelButton`
     - `VirtualTryOnButton`
     - `BiomeButton`
     - `VideoGameButton`
     - `CustomPromptButton`

6. Position buttons vertically:
   - Select all 5 buttons
   - Right-click → **Add Component** → **Vertical Layout Group**
   - Set **Spacing**: 20
   - Check **Child Force Expand** → Width

7. Update button text (for each button):
   - Expand button in Hierarchy
   - Select **Text (TMP)** child
   - In TextMeshPro component, set:
     - `TimeTravelButton`: "⏰ Time Travel"
     - `VirtualTryOnButton`: "👔 Virtual Try-On"
     - `BiomeButton`: "🌍 Biome/Country"
     - `VideoGameButton`: "🎮 Video Game Style"
     - `CustomPromptButton`: "⌨️ Custom Prompt"
   - Set **Font Size**: 36
   - Set **Alignment**: Center

### 1.2 Time Travel Panel

1. Right-click Canvas → **UI** → **Panel**
2. Rename to `TimeTravelPanel`
3. Set to inactive (uncheck box at top of Inspector)
4. Add components:

   **Year Slider:**
   - Right-click TimeTravelPanel → **UI** → **Slider**
   - Rename to `YearSlider`
   - In Slider component:
     - Min Value: 1500
     - Max Value: 2500
     - Whole Numbers: checked
     - Value: 2024

   **Year Display Text:**
   - Right-click TimeTravelPanel → **UI** → **Text - TextMeshPro**
   - Rename to `YearDisplayText`
   - Set text: "Year: 2024"
   - Font Size: 48

   **Description Text:**
   - Right-click TimeTravelPanel → **UI** → **Text - TextMeshPro**
   - Rename to `DescriptionText`
   - Set text: "Period: Modern Day"
   - Font Size: 32

### 1.3 Virtual Try-On Panel

1. Right-click Canvas → **UI** → **Panel**
2. Rename to `VirtualTryOnPanel`
3. Set to inactive
4. Add components:

   **Selected Clothing Text:**
   - Right-click VirtualTryOnPanel → **UI** → **Text - TextMeshPro**
   - Rename to `SelectedClothingText`
   - Set text: "Selected: Tuxedo"
   - Font Size: 42

   **Instruction Text:**
   - Right-click VirtualTryOnPanel → **UI** → **Text - TextMeshPro**
   - Rename to `InstructionText`
   - Set text: "Stand in front of camera\nUse joystick to browse"
   - Font Size: 28

### 1.4 Biome Panel

1. Right-click Canvas → **UI** → **Panel**
2. Rename to `BiomePanel`
3. Set to inactive
4. Add components:

   **Selected Biome Text:**
   - Right-click BiomePanel → **UI** → **Text - TextMeshPro**
   - Rename to `SelectedBiomeText`
   - Set text: "Selected: Tropical Rainforest"
   - Font Size: 42

   **Description Text:**
   - Right-click BiomePanel → **UI** → **Text - TextMeshPro**
   - Rename to `DescriptionText`
   - Set text: "Select a biome or country"
   - Font Size: 28

### 1.5 Video Game Panel

1. Right-click Canvas → **UI** → **Panel**
2. Rename to `VideoGamePanel`
3. Set to inactive
4. Add components:

   **Selected Game Text:**
   - Right-click VideoGamePanel → **UI** → **Text - TextMeshPro**
   - Rename to `SelectedGameText`
   - Set text: "Selected: Minecraft"
   - Font Size: 42

   **Description Text:**
   - Right-click VideoGamePanel → **UI** → **Text - TextMeshPro**
   - Rename to `DescriptionText`
   - Set text: "Select a video game style"
   - Font Size: 28

### 1.6 Custom Prompt Panel

1. Right-click Canvas → **UI** → **Panel**
2. Rename to `CustomPromptPanel`
3. Set to inactive
4. Add components:

   **Instruction Text:**
   - Right-click CustomPromptPanel → **UI** → **Text - TextMeshPro**
   - Rename to `InstructionText`
   - Set text: "Enter custom prompt\nPress A to open keyboard"
   - Font Size: 32

   **Input Field:**
   - Right-click CustomPromptPanel → **UI** → **Input Field - TextMeshPro**
   - Rename to `PromptInputField`
   - In TMP Input Field component:
     - Character Limit: 200
     - Content Type: Standard
     - Placeholder text: "Type your transformation..."

   **Submit Button:**
   - Right-click CustomPromptPanel → **UI** → **Button - TextMeshPro**
   - Rename to `SubmitButton`
   - Set button text to "Submit"

   **Last Prompt Text:**
   - Right-click CustomPromptPanel → **UI** → **Text - TextMeshPro**
   - Rename to `LastPromptText`
   - Set text: "Last prompt: None"
   - Font Size: 24

## Step 2: Create Controller GameObjects

### 2.1 Menu Manager

1. In Hierarchy, right-click → **Create Empty**
2. Rename to `MenuManager`
3. **Add Component** → Search for `MenuManager`
4. In Inspector, configure **MenuManager** component:

   **Menu References:**
   - Main Menu Panel: Drag `MainMenuPanel` from Hierarchy
   - Time Travel Panel: Drag `TimeTravelPanel`
   - Virtual Try On Panel: Drag `VirtualTryOnPanel`
   - Biome Panel: Drag `BiomePanel`
   - Video Game Panel: Drag `VideoGamePanel`
   - Custom Prompt Panel: Drag `CustomPromptPanel`

   **Main Menu Items:**
   - Size: 5
   - Element 0: Drag `TimeTravelButton`
   - Element 1: Drag `VirtualTryOnButton`
   - Element 2: Drag `BiomeButton`
   - Element 3: Drag `VideoGameButton`
   - Element 4: Drag `CustomPromptButton`

   **Navigation:**
   - Normal Color: White (255, 255, 255, 255)
   - Highlighted Color: Yellow (255, 255, 0, 255)
   - Navigation Cooldown: 0.25

### 2.2 Time Travel Controller

1. In Hierarchy, right-click → **Create Empty**
2. Rename to `TimeTravelController`
3. **Add Component** → Search for `TimeTravelController`
4. In Inspector, configure:

   **References:**
   - Web RTC Connection: Drag the existing `WebRTC-Connection` GameObject
   - Year Slider: Drag `YearSlider` from TimeTravelPanel
   - Year Display Text: Drag `YearDisplayText`
   - Description Text: Drag `DescriptionText`

   **Year Range:**
   - Min Year: 1500
   - Max Year: 2500

### 2.3 Virtual Try-On Controller

1. In Hierarchy, right-click → **Create Empty**
2. Rename to `VirtualTryOnController`
3. **Add Component** → Search for `VirtualTryOnController`
4. In Inspector, configure:

   **References:**
   - Web RTC Connection: Drag `WebRTC-Connection`
   - Selected Clothing Text: Drag from VirtualTryOnPanel
   - Instruction Text: Drag from VirtualTryOnPanel

   **Navigation:**
   - Normal Color: White
   - Highlighted Color: Cyan (0, 255, 255, 255)

### 2.4 Biome Controller

1. In Hierarchy, right-click → **Create Empty**
2. Rename to `BiomeController`
3. **Add Component** → Search for `BiomeController`
4. In Inspector, configure:

   **References:**
   - Web RTC Connection: Drag `WebRTC-Connection`
   - Selected Biome Text: Drag from BiomePanel
   - Description Text: Drag from BiomePanel

   **Navigation:**
   - Normal Color: White
   - Highlighted Color: Green (0, 255, 0, 255)

### 2.5 Video Game Controller

1. In Hierarchy, right-click → **Create Empty**
2. Rename to `VideoGameController`
3. **Add Component** → Search for `VideoGameController`
4. In Inspector, configure:

   **References:**
   - Web RTC Connection: Drag `WebRTC-Connection`
   - Selected Game Text: Drag from VideoGamePanel
   - Description Text: Drag from VideoGamePanel

   **Navigation:**
   - Normal Color: White
   - Highlighted Color: Magenta (255, 0, 255, 255)

### 2.6 Custom Prompt Controller

1. In Hierarchy, right-click → **Create Empty**
2. Rename to `CustomPromptController`
3. **Add Component** → Search for `CustomPromptController`
4. In Inspector, configure:

   **References:**
   - Web RTC Connection: Drag `WebRTC-Connection`
   - Prompt Input Field: Drag from CustomPromptPanel
   - Instruction Text: Drag from CustomPromptPanel
   - Last Prompt Text: Drag from CustomPromptPanel
   - Submit Button: Drag from CustomPromptPanel

   **Settings:**
   - Max Prompt Length: 200

## Step 3: Wire Up Button Events

### 3.1 Main Menu Button Events

For each button in MainMenuPanel:

**Time Travel Button:**
1. Select `TimeTravelButton` in Hierarchy
2. In Inspector, find **Button** component
3. Under **On Click ()** events:
   - Click **+** to add new event
   - Drag `MenuManager` GameObject to the object field
   - Select function: **MenuManager → ShowTimeTravel()**

**Virtual Try-On Button:**
- Add event → MenuManager → **ShowVirtualTryOn()**

**Biome Button:**
- Add event → MenuManager → **ShowBiome()**

**Video Game Button:**
- Add event → MenuManager → **ShowVideoGame()**

**Custom Prompt Button:**
- Add event → MenuManager → **ShowCustomPrompt()**

### 3.2 Submit Button Event (Custom Prompt)

1. Select `SubmitButton` in CustomPromptPanel
2. In Button component → On Click ():
   - Add event
   - Drag `CustomPromptController` GameObject
   - Select: **CustomPromptController → SubmitPrompt()**

## Step 4: Configure Canvas Settings

1. Select `MainMenuCanvas` (or your Canvas)
2. In **Canvas** component:
   - Render Mode: **World Space** (for VR)
   - Position: Set to be in front of camera (e.g., Z: 2)
   - Scale: (0.001, 0.001, 0.001) for appropriate VR size

3. In **Canvas Scaler** component:
   - UI Scale Mode: **Scale With Screen Size**
   - Reference Resolution: 1920 x 1080
   - Screen Match Mode: Match Width Or Height
   - Match: 0.5

## Step 5: Update WebRTC Controller (Optional)

If you want to integrate the old WebRTCController with the new MenuManager:

1. Find existing `WebRTCController` script
2. Either:
   - Keep it for backward compatibility
   - Or modify it to work alongside MenuManager
   - Or disable it and rely on new system

## Step 6: Test in Unity Editor

1. Press **Play** button in Unity
2. Verify:
   - Main menu appears
   - Joystick simulation works (keyboard arrows for testing)
   - Panel switching works
   - All text fields display correctly
   - Buttons respond to clicks

3. Common issues:
   - If panels don't switch: Check MenuManager references
   - If text doesn't appear: Check TextMeshPro components
   - If buttons don't work: Check On Click events

## Step 7: Build and Test on Quest 3

1. **Save the scene**: File → Save (Ctrl+S)
2. **Build Settings**: File → Build Settings
3. Ensure scene is in "Scenes In Build" list
4. Click **Build And Run**
5. Wait for build to complete
6. Test on Quest 3:
   - Put on headset
   - Launch app
   - Test all navigation:
     - Joystick up/down through menu
     - Right trigger to confirm
     - Left trigger to go back
     - Start button to toggle menu
   - Test each feature:
     - Time Travel slider
     - Virtual Try-On browsing
     - Biome selection
     - Video Game styles
     - Custom Prompt keyboard

## Step 8: Fine-Tuning

### Adjust UI Positioning
- Move panels to comfortable VR viewing distance
- Adjust text sizes for readability
- Test with different lighting conditions

### Adjust Colors
- Ensure text is readable
- High contrast for visibility
- Consider color-blind friendly options

### Adjust Timings
- Navigation cooldown (currently 0.25s)
- Slider sensitivity
- Menu transition speed

## Step 9: Save as Prefab (Optional)

To save your menu system for reuse:

1. In Hierarchy, select `MainMenuCanvas`
2. Drag it to Project window → Create Prefab
3. Name it `MenuSystem`
4. Now you can reuse this in other scenes

## Troubleshooting

### Scripts Not Found
- Ensure all `.cs` files are in correct folder
- Check for compile errors in Console window
- Reimport scripts if needed

### References Missing
- Drag and drop GameObjects from Hierarchy
- Use the circle/target icon to select from scene
- Ensure objects are active in scene

### Controls Not Working
- Check OVRInput is properly set up
- Verify Meta XR SDK is installed
- Test with Quest 3 controllers in build

### Text Not Displaying
- Import TextMeshPro if not already done
- Window → TextMeshPro → Import TMP Essential Resources
- Ensure TMP components are used (not legacy Text)

## Next Steps

After scene setup is complete:
1. Test all features thoroughly
2. Create screenshots for documentation
3. Record demo videos
4. Prepare for distribution
5. Submit to SideQuest or Meta Quest Store

## Additional Resources

- Unity TextMeshPro Docs: https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest
- Meta Quest UI Guidelines: https://developer.oculus.com/resources/
- Unity UI System: https://docs.unity3d.com/Packages/com.unity.ugui@latest

---

**Note**: These instructions assume familiarity with Unity's interface. If you're completely new to Unity, please refer to the COMPLETE_BEGINNERS_GUIDE.md first.

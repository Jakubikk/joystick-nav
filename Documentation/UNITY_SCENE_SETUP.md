# Unity Scene Setup Instructions

This document provides step-by-step instructions for setting up the new menu system in Unity.

## Prerequisites

- Unity 6 (6000.0.34f1) or later installed
- Project opened in Unity
- DecartAI-Main scene loaded

## Step 1: Create Menu UI Using Editor Tool

1. In Unity, go to **Tools** → **Decart** → **Setup Menu UI**
2. A window will appear titled "Menu UI Setup"
3. Click the **"Create Menu UI"** button
4. Wait for the process to complete
5. You should see a success dialog

This creates:
- MenuCanvas (World Space Canvas for VR)
- MenuPanel (Main menu panel)
- Feature panels for all 5 features
- MenuItem and ListItem prefabs

## Step 2: Add Menu Manager Component

1. In the Hierarchy, right-click and select **Create Empty**
2. Name it `MenuSystemManager`
3. With `MenuSystemManager` selected, click **Add Component** in the Inspector
4. Search for and add `MenuManager` component
5. The component will appear in the Inspector with empty references

## Step 3: Connect Menu UI References

With `MenuSystemManager` still selected in the Hierarchy:

1. In the Inspector, find the **MenuManager** component
2. Drag and drop the following from the Hierarchy to the Inspector:

   **Menu UI References:**
   - **Menu Panel**: Drag `MenuCanvas → MenuPanel`
   - **Title Text**: Drag `MenuCanvas → MenuPanel → TitleText`
   - **Menu Items Container**: Drag `MenuCanvas → MenuPanel → MenuItemsContainer`
   - **Menu Item Prefab**: From Project window, drag `Assets/Samples/DecartAI-Quest/Prefabs/MenuItem.prefab`

## Step 4: Create and Add Feature Components

### Time Travel Feature

1. In Hierarchy, right-click and select **Create Empty**
2. Name it `TimeTravelFeature`
3. Add the `TimeTravelFeature` component
4. Connect references:
   - **Feature Panel**: Drag `MenuCanvas → TimeTravelPanel`
   - **Title Text**: Drag `TimeTravelPanel → TitleText`
   - **Instructions Text**: Drag `TimeTravelPanel → InstructionsText`
   - **WebRTC Connection**: Drag the existing `WebRTCConnection` object from the scene
   
5. For the slider, you need to manually add UI elements to TimeTravelPanel:
   - Right-click `TimeTravelPanel` → UI → Slider
   - Name it `YearSlider`
   - Configure slider: Min Value = 1800, Max Value = 2100, Whole Numbers = true
   - Add a Text child for year display, name it `YearText`
   - Add another Text for description, name it `DescriptionText`
   - Drag these to the TimeTravelFeature component references

### Virtual Try-On Feature

1. Create Empty GameObject named `VirtualTryOnFeature`
2. Add `VirtualTryOnFeature` component
3. Connect references:
   - **Feature Panel**: Drag `MenuCanvas → VirtualTryOnPanel`
   - **Title Text**: Drag `VirtualTryOnPanel → TitleText`
   - **Current Item Text**: Add a new TextMeshProUGUI child to the panel, name it `CurrentItemText`
   - **Instructions Text**: Drag `VirtualTryOnPanel → InstructionsText`
   - **Clothing List Container**: Drag `VirtualTryOnPanel → ContentArea → Viewport → Container`
   - **List Item Prefab**: Drag `Assets/Samples/DecartAI-Quest/Prefabs/ListItem.prefab`
   - **WebRTC Connection**: Drag existing `WebRTCConnection`

### Biome Transform Feature

1. Create Empty GameObject named `BiomeTransformFeature`
2. Add `BiomeTransformFeature` component
3. Connect references (same pattern as Virtual Try-On):
   - **Feature Panel**: Drag `MenuCanvas → BiomeTransformPanel`
   - **Title Text**: Drag panel's TitleText
   - **Current Biome Text**: Add new TextMeshProUGUI, name it `CurrentBiomeText`
   - **Instructions Text**: Drag panel's InstructionsText
   - **Biome List Container**: Drag `BiomeTransformPanel → ContentArea → Viewport → Container`
   - **List Item Prefab**: Drag ListItem.prefab
   - **WebRTC Connection**: Drag WebRTCConnection

### Video Game Style Feature

1. Create Empty GameObject named `VideoGameStyleFeature`
2. Add `VideoGameStyleFeature` component
3. Connect references (same pattern):
   - **Feature Panel**: Drag `MenuCanvas → VideoGameStylePanel`
   - **Title Text**: Drag panel's TitleText
   - **Current Style Text**: Add new TextMeshProUGUI, name it `CurrentStyleText`
   - **Instructions Text**: Drag panel's InstructionsText
   - **Style List Container**: Drag `VideoGameStylePanel → ContentArea → Viewport → Container`
   - **List Item Prefab**: Drag ListItem.prefab
   - **WebRTC Connection**: Drag WebRTCConnection

### Custom Prompt Feature

1. Create Empty GameObject named `CustomPromptFeature`
2. Add `CustomPromptFeature` component
3. Add UI elements to CustomPromptPanel:
   - Right-click panel → UI → Input Field - TextMeshPro
   - Name it `PromptInputField`
   - Add two buttons: `ApplyButton` and `ClearButton`
   - Add Text: `StatusText`
4. Connect references:
   - **Feature Panel**: Drag `MenuCanvas → CustomPromptPanel`
   - **Title Text**: Drag panel's TitleText
   - **Prompt Input Field**: Drag PromptInputField
   - **Instructions Text**: Drag panel's InstructionsText
   - **Status Text**: Drag StatusText
   - **Apply Button**: Drag ApplyButton
   - **Clear Button**: Drag ClearButton
   - **Preset List Container**: Drag `CustomPromptPanel → ContentArea → Viewport → Container`
   - **Preset Item Prefab**: Drag ListItem.prefab
   - **WebRTC Connection**: Drag WebRTCConnection

## Step 5: Connect Features to Menu Manager

Now go back to the `MenuSystemManager` GameObject:

1. Select `MenuSystemManager` in Hierarchy
2. In the **MenuManager** component, find **Feature Managers** section
3. Drag each feature GameObject to its corresponding slot:
   - **Time Travel Feature**: Drag `TimeTravelFeature` GameObject
   - **Virtual Try On Feature**: Drag `VirtualTryOnFeature` GameObject
   - **Biome Transform Feature**: Drag `BiomeTransformFeature` GameObject
   - **Video Game Style Feature**: Drag `VideoGameStyleFeature` GameObject
   - **Custom Prompt Feature**: Drag `CustomPromptFeature` GameObject

## Step 6: Adjust Canvas for VR

1. Select `MenuCanvas` in Hierarchy
2. In the Inspector, set:
   - **Render Mode**: World Space
   - **Position**: X=0, Y=1.5, Z=2 (in front of player)
   - **Scale**: X=0.001, Y=0.001, Z=0.001
   - **Size Delta**: 1920 x 1080

3. Add an **Event Camera**:
   - Find your VR camera in the scene (usually under OVRCameraRig or XR Origin)
   - Drag it to the Canvas component's **Event Camera** field

## Step 7: Disable Voice Features (As Per Requirements)

Since the requirements state "do not include voice to text features", we need to disable the voice components:

1. Find any `VoiceManager`, `VoiceIntentController`, or `AppVoiceExperience` components
2. Either:
   - Disable the GameObject they're on, OR
   - Remove the components entirely
3. The CustomPromptFeature uses keyboard input only, not voice

## Step 8: Configure Input System

1. Go to **Edit** → **Project Settings** → **XR Interaction Toolkit**
2. Ensure OVR Input is properly configured
3. Test button mappings:
   - **Left Trigger** = PrimaryIndexTrigger (back/cancel)
   - **Right Trigger** = SecondaryIndexTrigger (confirm/apply)
   - **Left Joystick** = PrimaryThumbstick (navigate up/down)
   - **Start Button** = Start (toggle menu)

## Step 9: Test in Unity Editor

1. Make sure all references are connected (no "None" entries in Inspector)
2. Save the scene: **File** → **Save** (Ctrl+S)
3. Click Play button in Unity Editor
4. Check Console for any errors
5. Menu should be visible in the Game view
6. Use keyboard to simulate VR controls:
   - WASD for joystick
   - Space for right trigger
   - Left Shift for left trigger

## Step 10: Build and Test on Quest

1. Connect Meta Quest 3 via USB
2. Go to **File** → **Build Settings**
3. Ensure **DecartAI-Main** scene is checked in build
4. Click **Build And Run**
5. Wait for build to complete
6. App will install and launch on Quest
7. Test all features:
   - Menu navigation with joystick
   - Feature selection with right trigger
   - Going back with left trigger
   - Toggling menu with hamburger button

## Troubleshooting

### Menu Not Visible
- Check Canvas position is at (0, 1.5, 2)
- Verify Event Camera is set
- Check Canvas is enabled in Hierarchy

### References Missing
- Make sure all GameObjects exist in scene
- Check Inspector for "None" references
- Re-drag components if needed

### Features Not Working
- Verify WebRTCConnection is connected to all features
- Check that each feature's panel is properly set up
- Look for errors in Console window

### Input Not Responding
- Verify OVR Input is initialized
- Check that MenuManager is enabled
- Test button mappings on Quest

### Compilation Errors
- Make sure all scripts are in correct folders
- Check .meta files exist for all scripts
- Reimport scripts if needed: Right-click → Reimport

## Additional Customization

### Changing Menu Colors

In MenuManager component:
- **Normal Color**: Color for unselected items
- **Selected Color**: Color for currently selected item

### Adjusting Navigation Speed

In each feature component:
- **Navigation Cooldown**: Time between navigation inputs (default 0.2s)
- **Slider Speed** (Time Travel): Speed of year adjustment

### Adding More Options

To add more biomes, games, or clothing:
1. Open the respective feature script (.cs file)
2. Find the Dictionary definition (e.g., `biomeOptions`, `gameStyleOptions`)
3. Add new entries in the format:
   ```csharp
   { "Display Name", "Decart AI prompt text" }
   ```
4. Save and let Unity recompile

## Next Steps

Once setup is complete:
1. Test all features thoroughly
2. Adjust UI positions for comfortable VR viewing
3. Customize colors and styling to your preference
4. Build final version for distribution
5. Follow COMPLETE_DEPLOYMENT_GUIDE.md for publishing

## Support

For issues or questions:
- Check the COMPLETE_DEPLOYMENT_GUIDE.md in Documentation folder
- Review Unity Console for error messages
- Verify all references are connected in Inspector
- Test one feature at a time to isolate issues

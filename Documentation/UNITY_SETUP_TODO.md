# Unity Scene Setup TODO

This document provides a detailed checklist for configuring the Unity scene to work with the new feature controllers. All C# scripts are complete - this is purely Unity Editor configuration work.

---

## Overview

**Current Status:**
- ✅ All C# scripts written and committed
- ✅ All documentation complete
- ✅ Build automation ready
- ⚠️ Unity scene needs UI configuration

**What Needs to Be Done:**
- Create UI prefabs
- Update Unity scene hierarchy
- Configure Inspector references
- Test in Unity Editor
- Build and deploy to Quest 3

**Estimated Time:** 4-6 hours

---

## Prerequisites

Before starting:
- [ ] Unity 6 (6000.0.34f1) installed
- [ ] Project opened in Unity
- [ ] DecartAI-Main.unity scene loaded
- [ ] All packages imported successfully

---

## Part 1: Create UI Prefabs (1-2 hours)

### 1.1 Menu Item Prefab

**Location:** `Assets/Samples/DecartAI-Quest/Prefabs/MenuItemPrefab.prefab`

**Steps:**
1. Right-click in Project → Create → UI → Panel
2. Name it "MenuItemPrefab"
3. Add components:
   - Image (background)
   - TextMeshPro - Text (item name)
4. Configure:
   - Panel size: 400x60
   - Background color: (0.1, 0.1, 0.1, 0.7)
   - Text: Font size 24, centered
5. Drag to Prefabs folder
6. Delete from Hierarchy

### 1.2 Feature Panel Prefabs

Create 5 separate prefabs (or use a base template):

#### TimeTravelPanel Prefab

**Location:** `Assets/Samples/DecartAI-Quest/Prefabs/TimeTravelPanel.prefab`

**Components:**
```
TimeTravelPanel (Panel)
├── TitleText (TextMeshPro)
├── YearSlider (Slider)
│   ├── Background
│   ├── Fill Area
│   │   └── Fill
│   └── Handle Slide Area
│       └── Handle
├── YearDisplayText (TextMeshPro)
├── PeriodDescriptionText (TextMeshPro)
└── ApplyButton (Button)
    └── ButtonText (TextMeshPro)
```

**Configuration:**
- Panel: Full screen, initially inactive
- Slider: Min 1800, Max 2200, Whole Numbers
- Texts: Appropriate font sizes
- Button: 200x60, centered at bottom

#### VirtualTryOnPanel Prefab

**Location:** `Assets/Samples/DecartAI-Quest/Prefabs/VirtualTryOnPanel.prefab`

**Components:**
```
VirtualTryOnPanel (Panel)
├── TitleText (TextMeshPro)
├── InstructionText (TextMeshPro)
├── ClothingOptionsContainer (Vertical Layout Group)
│   └── (Menu items will be spawned here)
├── SelectedClothingText (TextMeshPro)
└── ScrollView (Scroll Rect)
```

**Configuration:**
- Panel: Full screen, initially inactive
- Layout Group: Spacing 10, padding 20
- Scroll View: Vertical scrolling enabled

#### BiomePanel Prefab

**Location:** `Assets/Samples/DecartAI-Quest/Prefabs/BiomePanel.prefab`

**Components:**
```
BiomePanel (Panel)
├── TitleText (TextMeshPro)
├── DescriptionText (TextMeshPro)
├── BiomeOptionsContainer (Vertical Layout Group)
│   └── (Menu items will be spawned here)
├── SelectedBiomeText (TextMeshPro)
└── ScrollView (Scroll Rect)
```

**Configuration:**
- Similar to VirtualTryOnPanel
- Panel: Full screen, initially inactive

#### VideoGamePanel Prefab

**Location:** `Assets/Samples/DecartAI-Quest/Prefabs/VideoGamePanel.prefab`

**Components:**
```
VideoGamePanel (Panel)
├── TitleText (TextMeshPro)
├── DescriptionText (TextMeshPro)
├── GameStylesContainer (Vertical Layout Group)
│   └── (Menu items will be spawned here)
├── SelectedGameText (TextMeshPro)
└── ScrollView (Scroll Rect)
```

**Configuration:**
- Similar to BiomePanel
- Panel: Full screen, initially inactive

#### CustomPromptPanel Prefab

**Location:** `Assets/Samples/DecartAI-Quest/Prefabs/CustomPromptPanel.prefab`

**Components:**
```
CustomPromptPanel (Panel)
├── TitleText (TextMeshPro)
├── InstructionText (TextMeshPro)
├── PromptInputField (TMP_InputField)
│   ├── Text Area
│   │   ├── Placeholder (TextMeshPro)
│   │   └── Text (TextMeshPro)
│   └── Background (Image)
├── CurrentPromptText (TextMeshPro)
├── SuggestedPromptsText (TextMeshPro)
├── OpenKeyboardButton (Button)
│   └── ButtonText (TextMeshPro)
├── ApplyButton (Button)
│   └── ButtonText (TextMeshPro)
└── ClearButton (Button)
    └── ButtonText (TextMeshPro)
```

**Configuration:**
- Panel: Full screen, initially inactive
- Input Field: Multi-line, character limit 500
- Buttons: 150x50 each

---

## Part 2: Update Main Scene (1-2 hours)

### 2.1 Create Menu System Hierarchy

**In DecartAI-Main.unity scene:**

1. Find or create MainCanvas
2. Add this hierarchy:

```
MainCanvas
├── MenuPanel
│   ├── TitleText ("Menu")
│   ├── DescriptionText
│   └── MenuOptionsContainer (Vertical Layout Group)
│       └── (MenuItems will spawn here)
├── TimeTravelPanel (from prefab)
├── VirtualTryOnPanel (from prefab)
├── BiomePanel (from prefab)
├── VideoGamePanel (from prefab)
└── CustomPromptPanel (from prefab)
```

**Configuration:**
- MainCanvas: World Space, positioned in front of user
- MenuPanel: 600x800, centered
- Feature panels: Initially set active = false

### 2.2 Add MenuController

1. Create new GameObject: "MenuSystem"
2. Add MenuController component
3. Configure in Inspector:
   - Menu Panel: Drag MenuPanel
   - Menu Options Container: Drag MenuOptionsContainer
   - Menu Item Prefab: Drag MenuItemPrefab
   - Title Text: Drag TitleText
   - Description Text: Drag DescriptionText
   - Time Travel Panel: Drag TimeTravelPanel
   - Virtual Try On Panel: Drag VirtualTryOnPanel
   - Biome Panel: Drag BiomePanel
   - Video Game Panel: Drag VideoGamePanel
   - Custom Prompt Panel: Drag CustomPromptPanel

### 2.3 Add Feature Controllers

For each feature panel:

#### TimeTravelPanel
1. Select TimeTravelPanel in Hierarchy
2. Add TimeTravelController component
3. Configure:
   - Year Slider: Drag the slider
   - Year Display Text: Drag YearDisplayText
   - Period Description Text: Drag PeriodDescriptionText
   - Apply Button: Drag ApplyButton
   - WebRTC Connection: Drag existing WebRTCConnection from scene

#### VirtualTryOnPanel
1. Select VirtualTryOnPanel
2. Add VirtualTryOnController component
3. Configure:
   - Clothing Options Container: Drag container
   - Clothing Item Prefab: Drag MenuItemPrefab
   - Instruction Text: Drag InstructionText
   - Selected Clothing Text: Drag SelectedClothingText
   - WebRTC Connection: Drag WebRTCConnection

#### BiomePanel
1. Select BiomePanel
2. Add BiomeController component
3. Configure:
   - Biome Options Container: Drag container
   - Biome Item Prefab: Drag MenuItemPrefab
   - Description Text: Drag DescriptionText
   - Selected Biome Text: Drag SelectedBiomeText
   - WebRTC Connection: Drag WebRTCConnection

#### VideoGamePanel
1. Select VideoGamePanel
2. Add VideoGameController component
3. Configure:
   - Game Styles Container: Drag container
   - Game Style Item Prefab: Drag MenuItemPrefab
   - Description Text: Drag DescriptionText
   - Selected Game Text: Drag SelectedGameText
   - WebRTC Connection: Drag WebRTCConnection

#### CustomPromptPanel
1. Select CustomPromptPanel
2. Add CustomPromptController component
3. Configure:
   - Prompt Input Field: Drag PromptInputField
   - Instruction Text: Drag InstructionText
   - Current Prompt Text: Drag CurrentPromptText
   - Suggested Prompts Text: Drag SuggestedPromptsText
   - Open Keyboard Button: Drag OpenKeyboardButton
   - Apply Button: Drag ApplyButton
   - Clear Button: Drag ClearButton
   - WebRTC Connection: Drag WebRTCConnection

---

## Part 3: Configure Existing Components (30 min)

### 3.1 Update WebRTCController

The existing WebRTCController should still be in the scene.

**Verify/Update:**
- Canvas Raw Image reference
- Prompt Name Text reference
- WebRTC Connection reference
- Passthrough Camera Manager reference

**Note:** The Update() method was modified to remove old button handling, so this should work automatically with the new menu system.

### 3.2 Verify WebRTC Connection

Make sure WebRTCConnection component has:
- Correct WebSocket URLs for Mirage and Lucy
- Streaming Camera reference
- Receiving Raw Images Parent reference

---

## Part 4: Testing in Unity Editor (1 hour)

### 4.1 Play Mode Tests

1. Press Play in Unity Editor
2. Check Console for errors
3. Verify:
   - [ ] Menu appears
   - [ ] Can navigate with arrow keys (simulates joystick)
   - [ ] Can select features
   - [ ] Feature panels open
   - [ ] Can navigate back to main menu

### 4.2 Component Tests

For each feature:
- [ ] TimeTravelController initializes
- [ ] VirtualTryOnController creates clothing items
- [ ] BiomeController creates biome items
- [ ] VideoGameController creates game style items
- [ ] CustomPromptController input field works

### 4.3 Fix Common Issues

**If menu items don't appear:**
- Check MenuItemPrefab is assigned
- Check MenuOptionsContainer has Vertical Layout Group
- Check MenuController.Start() is called

**If feature panels don't open:**
- Check panels are referenced in MenuController
- Check panels have correct controllers attached
- Check feature controllers have all fields assigned

**If navigation doesn't work:**
- Check OVRInput is available (may not work in Editor)
- Can test with keyboard: Arrow keys = joystick, Space = trigger

---

## Part 5: Build and Deploy (1 hour)

### 5.1 Save Everything

1. Save Scene: Ctrl/Cmd+S
2. Save Project: File → Save Project

### 5.2 Build

**Option A: Automated**
```bash
./scripts/build.sh
```

**Option B: Manual**
1. File → Build Settings
2. Android platform
3. Build

### 5.3 Deploy to Quest

**Option A: Automated**
```bash
./scripts/deploy.sh
```

**Option B: Manual**
1. Use SideQuest
2. Install APK
3. Launch from Unknown Sources

### 5.4 Test on Quest 3

1. Put on headset
2. Launch app
3. Test each feature:
   - [ ] Menu appears
   - [ ] Hamburger button toggles menu
   - [ ] Joystick navigates
   - [ ] Right trigger selects
   - [ ] Left trigger goes back
   - [ ] Time Travel works
   - [ ] Virtual Try-On works
   - [ ] Biome Transformation works
   - [ ] Video Game Style works
   - [ ] Custom Prompt keyboard opens
   - [ ] AI transformations apply

---

## Part 6: Polish and Finalize (1 hour)

### 6.1 Visual Polish

- Adjust colors for VR visibility
- Adjust text sizes for readability
- Add spacing/padding as needed
- Ensure UI is comfortable in VR

### 6.2 Performance Check

- Check frame rate in VR
- Ensure no stuttering
- Verify AI latency is acceptable

### 6.3 Final Build

Once everything works:
1. Save all changes
2. Build final APK
3. Test one more time
4. Done!

---

## Troubleshooting Checklist

### Scene Issues

**Menu doesn't appear:**
- [ ] MenuPanel is active in scene
- [ ] MenuPanel is child of MainCanvas
- [ ] MenuController component is added
- [ ] All references are assigned

**Feature panels don't work:**
- [ ] Each panel has its controller script
- [ ] All UI references are assigned
- [ ] WebRTCConnection is assigned
- [ ] Panels are initially set to inactive

**Navigation doesn't work:**
- [ ] OVRInput package is installed
- [ ] Controllers are enabled in scene
- [ ] No conflicting input handlers

### Build Issues

**Build fails:**
- [ ] Check Console for errors
- [ ] Verify Android SDK is installed
- [ ] Check XR Plugin Management settings
- [ ] Review Meta XR Project Setup Tool

**APK won't install:**
- [ ] Developer Mode is enabled on Quest
- [ ] USB debugging is allowed
- [ ] Correct package name
- [ ] Try uninstalling old version first

### Runtime Issues

**App crashes on launch:**
- [ ] Check Unity logs
- [ ] Verify permissions are granted
- [ ] Check WebRTC connection URLs
- [ ] Review camera initialization

**AI doesn't work:**
- [ ] Check internet connection
- [ ] Verify Decart API is accessible
- [ ] Check WebSocket URLs
- [ ] Review prompt formatting

---

## Completion Checklist

When you finish, you should have:

- [ ] All UI prefabs created
- [ ] Scene hierarchy configured
- [ ] All Inspector references assigned
- [ ] Tested in Unity Editor
- [ ] Built APK successfully
- [ ] Deployed to Quest 3
- [ ] All features tested in VR
- [ ] No errors in Console
- [ ] Acceptable performance
- [ ] Professional appearance

---

## Next Steps After Completion

1. **Document any issues found**
   - Create GitHub issues for bugs
   - Note any improvements needed

2. **Share with community**
   - Post screenshots/videos
   - Share on Discord
   - Get feedback

3. **Future enhancements**
   - Add visual effects
   - Improve UI design
   - Add more features
   - Optimize performance

---

## Getting Help

If you get stuck:

1. Check Unity Console for error messages
2. Review [COMPLETE_GUIDE.md](COMPLETE_GUIDE.md) troubleshooting
3. Ask on [Decart Discord](https://discord.gg/decart)
4. Open [GitHub Issue](https://github.com/Jakubikk/joystick-nav/issues)

---

*This TODO represents the final steps to complete the project. All code is ready - just needs Unity Editor configuration!*

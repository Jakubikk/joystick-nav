# Unity Scene Setup Guide

This guide explains how to set up the Unity scene with the new menu system and features.

## Quick Setup Instructions

### Step 1: Scene Hierarchy Setup

Create the following GameObject hierarchy in your Unity scene:

```
Scene: DecartAI-Main
├── XR Origin (from Meta XR)
├── MainCanvas (World Space Canvas)
│   ├── MenuPanel
│   │   ├── Title (TextMeshPro)
│   │   ├── Description (TextMeshPro)
│   │   └── MenuItemsContainer (Vertical Layout Group)
│   │       └── [Menu items will be spawned here]
│   ├── TimeTravelPanel
│   │   ├── YearSlider (Slider)
│   │   ├── YearText (TextMeshPro)
│   │   ├── EraDescription (TextMeshPro)
│   │   └── Instructions (TextMeshPro)
│   ├── VirtualMirrorPanel
│   │   ├── ClothingOptionsContainer (Vertical Layout Group)
│   │   │   └── [Clothing items will be spawned here]
│   │   ├── SelectedClothingText (TextMeshPro)
│   │   └── Instructions (TextMeshPro)
│   ├── BiomePanel
│   │   ├── BiomeOptionsContainer (Vertical Layout Group)
│   │   │   └── [Biome items will be spawned here]
│   │   ├── SelectedBiomeText (TextMeshPro)
│   │   └── Instructions (TextMeshPro)
│   ├── VideoGamePanel
│   │   ├── GameWorldOptionsContainer (Vertical Layout Group)
│   │   │   └── [Game items will be spawned here]
│   │   ├── SelectedGameWorldText (TextMeshPro)
│   │   └── Instructions (TextMeshPro)
│   └── CustomPromptPanel
│       ├── PromptInputField (TMP_InputField)
│       ├── CurrentPromptText (TextMeshPro)
│       ├── ApplyButton (Button)
│       └── Instructions (TextMeshPro)
├── MenuSystem (Empty GameObject)
├── TimeTravelController (Empty GameObject)
├── VirtualMirrorController (Empty GameObject)
├── BiomeController (Empty GameObject)
├── VideoGameWorldController (Empty GameObject)
├── CustomPromptController (Empty GameObject)
└── WebRTCConnection (Existing)
```

### Step 2: Create Prefabs

Create these prefabs in `Assets/Samples/DecartAI-Quest/Prefabs/`:

#### MenuItemPrefab
- GameObject with Image background
- TextMeshProUGUI child for text
- Size: 400x60

#### ClothingOptionPrefab
- GameObject with Image background
- TextMeshProUGUI child for name
- Size: 450x50

#### BiomeOptionPrefab
- GameObject with Image background
- TextMeshProUGUI child for name
- Size: 450x50

#### GameWorldOptionPrefab
- GameObject with Image background
- TextMeshProUGUI child for name
- Size: 450x50

### Step 3: Configure MenuSystem

1. Select **MenuSystem** GameObject
2. Add **MenuSystem.cs** component
3. Configure references in Inspector:

**Menu UI References:**
- Menu Canvas → MainCanvas
- Menu Items Container → MenuPanel/MenuItemsContainer
- Menu Item Prefab → Your MenuItemPrefab
- Title Text → MenuPanel/Title
- Description Text → MenuPanel/Description

**Feature Controllers:**
- Time Travel Controller → TimeTravelController GameObject
- Virtual Mirror Controller → VirtualMirrorController GameObject
- Biome Controller → BiomeController GameObject
- Video Game World Controller → VideoGameWorldController GameObject
- Custom Prompt Controller → CustomPromptController GameObject

**WebRTC Connection:**
- WebRTC Connection → WebRTCConnection GameObject

### Step 4: Configure TimeTravelController

1. Select **TimeTravelController** GameObject
2. Add **TimeTravelController.cs** component
3. Configure in Inspector:

**UI References:**
- Time Travel UI → TimeTravelPanel
- Year Slider → TimeTravelPanel/YearSlider
- Year Text → TimeTravelPanel/YearText
- Era Description Text → TimeTravelPanel/EraDescription

**WebRTC Connection:**
- WebRTC Connection → WebRTCConnection GameObject

### Step 5: Configure VirtualMirrorController

1. Select **VirtualMirrorController** GameObject
2. Add **VirtualMirrorController.cs** component
3. Configure in Inspector:

**UI References:**
- Virtual Mirror UI → VirtualMirrorPanel
- Clothing Options Container → VirtualMirrorPanel/ClothingOptionsContainer
- Clothing Option Prefab → Your ClothingOptionPrefab
- Selected Clothing Text → VirtualMirrorPanel/SelectedClothingText
- Instructions Text → VirtualMirrorPanel/Instructions

**WebRTC Connection:**
- WebRTC Connection → WebRTCConnection GameObject

### Step 6: Configure BiomeController

1. Select **BiomeController** GameObject
2. Add **BiomeController.cs** component
3. Configure in Inspector:

**UI References:**
- Biome UI → BiomePanel
- Biome Options Container → BiomePanel/BiomeOptionsContainer
- Biome Option Prefab → Your BiomeOptionPrefab
- Selected Biome Text → BiomePanel/SelectedBiomeText
- Instructions Text → BiomePanel/Instructions

**WebRTC Connection:**
- WebRTC Connection → WebRTCConnection GameObject

### Step 7: Configure VideoGameWorldController

1. Select **VideoGameWorldController** GameObject
2. Add **VideoGameWorldController.cs** component
3. Configure in Inspector:

**UI References:**
- Video Game UI → VideoGamePanel
- Game World Options Container → VideoGamePanel/GameWorldOptionsContainer
- Game World Option Prefab → Your GameWorldOptionPrefab
- Selected Game World Text → VideoGamePanel/SelectedGameWorldText
- Instructions Text → VideoGamePanel/Instructions

**WebRTC Connection:**
- WebRTC Connection → WebRTCConnection GameObject

### Step 8: Configure CustomPromptController

1. Select **CustomPromptController** GameObject
2. Add **CustomPromptController.cs** component
3. Configure in Inspector:

**UI References:**
- Custom Prompt UI → CustomPromptPanel
- Prompt Input Field → CustomPromptPanel/PromptInputField
- Current Prompt Text → CustomPromptPanel/CurrentPromptText
- Instructions Text → CustomPromptPanel/Instructions
- Apply Button → CustomPromptPanel/ApplyButton

**WebRTC Connection:**
- WebRTC Connection → WebRTCConnection GameObject

## Canvas Setup Details

### MainCanvas Configuration

1. **Canvas** component:
   - Render Mode: **World Space**
   - Position: In front of user (0, 1.5, 2) - adjust as needed
   - Scale: (0.001, 0.001, 0.001)
   - Width: 1920
   - Height: 1080

2. **Canvas Scaler** component:
   - UI Scale Mode: **Scale With Screen Size**
   - Reference Resolution: 1920x1080
   - Match: 0.5

3. Add **Billboard.cs** component to face the user

### Panel Setup

Each panel (MenuPanel, TimeTravelPanel, etc.):
1. Set as child of MainCanvas
2. Anchor: Stretch (full canvas)
3. Offset: All zeros
4. Initially set inactive (except MenuPanel)

### Styling Recommendations

#### Colors
- Background: Dark semi-transparent (0, 0, 0, 0.8)
- Selected item: Yellow (1, 0.92, 0.016, 1)
- Normal text: White (1, 1, 1, 1)
- Disabled text: Gray (0.5, 0.5, 0.5, 1)

#### Fonts
- Title: 48pt Bold
- Description: 24pt Regular
- Menu items: 28pt Regular
- Instructions: 20pt Italic

#### Layout
- Menu items: 10px spacing
- Padding: 20px all around
- Vertical Layout Groups: Control Size Height enabled

## Testing

### Scene Testing Checklist

- [ ] MainCanvas is visible in scene view
- [ ] All panels are children of MainCanvas
- [ ] Menu items spawn when entering play mode
- [ ] Controllers have proper references (no missing components)
- [ ] WebRTCConnection is referenced by all feature controllers
- [ ] Prefabs are assigned to all controllers that need them

### Play Mode Testing

1. Enter Play Mode
2. Check Console for any errors
3. Verify MenuSystem initializes
4. Test menu navigation with keyboard (simulate joystick):
   - Up/Down arrow keys
   - Space for trigger
5. Verify each panel activates when selected

## Common Issues

### "Missing Reference" Errors
- Ensure all Inspector fields are filled
- Drag GameObjects from Hierarchy to Inspector
- Check prefabs exist and are assigned

### Menu Items Not Appearing
- Verify prefabs are assigned to controllers
- Check MenuItemsContainer has Vertical Layout Group
- Ensure Content Size Fitter is on container

### Panels Not Switching
- Verify all panels are children of MainCanvas
- Check that panels are initially inactive
- Ensure MenuSystem has references to all controllers

## Advanced Customization

### Custom Menu Styling

Edit prefabs to match your design:
1. Add images/backgrounds
2. Adjust text colors and fonts
3. Add icons
4. Implement animations

### Custom Transitions

Add to MenuSystem.cs:
```csharp
using DG.Tweening; // If using DOTween

private void TransitionToPanel(GameObject panel)
{
    panel.transform.localScale = Vector3.zero;
    panel.SetActive(true);
    panel.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
}
```

### Custom Sound Effects

Add AudioSource to MenuSystem and play on:
- Navigation (scroll sound)
- Selection (click sound)
- Panel switch (whoosh sound)

## Next Steps

After scene setup:
1. Build for Quest 3 (see main README.md)
2. Test on device
3. Adjust UI positioning for comfort
4. Fine-tune joystick navigation sensitivity
5. Add visual polish

## Support

If you encounter issues during setup:
1. Check Unity Console for errors
2. Verify all references are set
3. Review this guide step-by-step
4. See COMPLETE_BEGINNERS_GUIDE.md for detailed help

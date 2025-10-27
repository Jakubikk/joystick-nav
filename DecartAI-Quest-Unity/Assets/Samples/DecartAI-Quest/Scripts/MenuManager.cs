using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Manages the main menu system with joystick navigation
    /// Navigation: Left Trigger = Back, Right Trigger = Confirm, Joystick Up/Down = Navigate, Hamburger = Toggle Menu
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu Settings")]
        [SerializeField] private GameObject menuCanvas;
        [SerializeField] private bool menuVisible = true;
        
        [Header("Menu Items")]
        [SerializeField] private List<MenuItemUI> menuItems = new List<MenuItemUI>();
        
        [Header("Features")]
        [SerializeField] private TimeTravelFeature timeTravelFeature;
        [SerializeField] private VirtualTryOnFeature virtualTryOnFeature;
        [SerializeField] private BiomeTransformFeature biomeTransformFeature;
        [SerializeField] private VideoGameStyleFeature videoGameStyleFeature;
        [SerializeField] private CustomPromptFeature customPromptFeature;
        
        private int currentMenuIndex = 0;
        private bool inSubMenu = false;
        private float joystickCooldown = 0f;
        private const float JOYSTICK_COOLDOWN_TIME = 0.3f;
        
        public enum MenuState
        {
            MainMenu,
            TimeTravel,
            VirtualTryOn,
            BiomeTransform,
            VideoGameStyle,
            CustomPrompt
        }
        
        private MenuState currentState = MenuState.MainMenu;
        
        private void Start()
        {
            InitializeMenu();
            UpdateMenuSelection();
        }
        
        private void InitializeMenu()
        {
            // Setup menu items
            if (menuItems.Count == 0)
            {
                Debug.LogWarning("MenuManager: No menu items configured!");
            }
            
            // Initially hide all features
            if (timeTravelFeature != null) timeTravelFeature.gameObject.SetActive(false);
            if (virtualTryOnFeature != null) virtualTryOnFeature.gameObject.SetActive(false);
            if (biomeTransformFeature != null) biomeTransformFeature.gameObject.SetActive(false);
            if (videoGameStyleFeature != null) videoGameStyleFeature.gameObject.SetActive(false);
            if (customPromptFeature != null) customPromptFeature.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            HandleMenuToggle();
            
            if (!menuVisible)
                return;
            
            // Update cooldown
            if (joystickCooldown > 0)
            {
                joystickCooldown -= Time.deltaTime;
            }
            
            if (currentState == MenuState.MainMenu)
            {
                HandleMainMenuNavigation();
            }
            else
            {
                HandleSubMenuNavigation();
            }
        }
        
        private void HandleMenuToggle()
        {
            // Hamburger button (Start button on Quest) toggles menu visibility
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                menuVisible = !menuVisible;
                if (menuCanvas != null)
                {
                    menuCanvas.SetActive(menuVisible);
                }
                
                Debug.Log($"Menu visibility: {menuVisible}");
            }
        }
        
        private void HandleMainMenuNavigation()
        {
            // Joystick Up/Down navigation
            if (joystickCooldown <= 0)
            {
                Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                if (joystick.y > 0.5f) // Up
                {
                    currentMenuIndex--;
                    if (currentMenuIndex < 0)
                        currentMenuIndex = menuItems.Count - 1;
                    
                    UpdateMenuSelection();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
                else if (joystick.y < -0.5f) // Down
                {
                    currentMenuIndex++;
                    if (currentMenuIndex >= menuItems.Count)
                        currentMenuIndex = 0;
                    
                    UpdateMenuSelection();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
            }
            
            // Right Trigger = Confirm/Select
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                SelectMenuItem(currentMenuIndex);
            }
        }
        
        private void HandleSubMenuNavigation()
        {
            // Left Trigger = Back to main menu
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
            {
                ReturnToMainMenu();
            }
            
            // Right Trigger = Confirm (handled by individual features)
            // Joystick navigation (handled by individual features)
        }
        
        private void UpdateMenuSelection()
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i] != null)
                {
                    menuItems[i].SetSelected(i == currentMenuIndex);
                }
            }
        }
        
        private void SelectMenuItem(int index)
        {
            if (index < 0 || index >= menuItems.Count)
                return;
            
            Debug.Log($"Selected menu item: {index}");
            
            // Activate the corresponding feature based on index
            switch (index)
            {
                case 0: // Time Travel
                    ActivateFeature(MenuState.TimeTravel, timeTravelFeature);
                    break;
                case 1: // Virtual Try-On
                    ActivateFeature(MenuState.VirtualTryOn, virtualTryOnFeature);
                    break;
                case 2: // Biome Transform
                    ActivateFeature(MenuState.BiomeTransform, biomeTransformFeature);
                    break;
                case 3: // Video Game Style
                    ActivateFeature(MenuState.VideoGameStyle, videoGameStyleFeature);
                    break;
                case 4: // Custom Prompt
                    ActivateFeature(MenuState.CustomPrompt, customPromptFeature);
                    break;
            }
        }
        
        private void ActivateFeature(MenuState state, MonoBehaviour feature)
        {
            currentState = state;
            inSubMenu = true;
            
            // Hide main menu
            if (menuCanvas != null)
            {
                // Keep canvas active but hide main menu items
                foreach (var item in menuItems)
                {
                    if (item != null)
                        item.gameObject.SetActive(false);
                }
            }
            
            // Show the selected feature
            if (feature != null)
            {
                feature.gameObject.SetActive(true);
            }
        }
        
        public void ReturnToMainMenu()
        {
            Debug.Log("Returning to main menu");
            
            currentState = MenuState.MainMenu;
            inSubMenu = false;
            
            // Deactivate all features
            if (timeTravelFeature != null) timeTravelFeature.gameObject.SetActive(false);
            if (virtualTryOnFeature != null) virtualTryOnFeature.gameObject.SetActive(false);
            if (biomeTransformFeature != null) biomeTransformFeature.gameObject.SetActive(false);
            if (videoGameStyleFeature != null) videoGameStyleFeature.gameObject.SetActive(false);
            if (customPromptFeature != null) customPromptFeature.gameObject.SetActive(false);
            
            // Show main menu items
            foreach (var item in menuItems)
            {
                if (item != null)
                    item.gameObject.SetActive(true);
            }
            
            UpdateMenuSelection();
        }
    }
    
    /// <summary>
    /// Represents a single menu item in the UI
    /// </summary>
    [System.Serializable]
    public class MenuItemUI
    {
        public GameObject gameObject;
        public TMP_Text text;
        public Image background;
        public Color normalColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        public Color selectedColor = new Color(0.4f, 0.6f, 1.0f, 0.9f);
        
        public void SetSelected(bool selected)
        {
            if (background != null)
            {
                background.color = selected ? selectedColor : normalColor;
            }
            
            if (text != null)
            {
                text.fontStyle = selected ? FontStyles.Bold : FontStyles.Normal;
            }
        }
    }
}

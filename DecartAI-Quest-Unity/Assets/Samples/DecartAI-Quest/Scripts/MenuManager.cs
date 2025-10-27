using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Main menu manager for Quest 3 Decart AI features
    /// Controls navigation between Time Travel, Virtual Mirror, Biome, Video Game, and Custom modes
    /// Navigation: Left trigger = back, Right trigger = confirm, Joystick up/down = navigate, Hamburger = toggle menu
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu UI References")]
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private Transform menuItemContainer;
        [SerializeField] private GameObject menuItemPrefab;
        [SerializeField] private TMP_Text titleText;
        
        [Header("Feature Controllers")]
        [SerializeField] private TimeTravelController timeTravelController;
        [SerializeField] private VirtualMirrorController virtualMirrorController;
        [SerializeField] private BiomeController biomeController;
        [SerializeField] private VideoGameController videoGameController;
        [SerializeField] private CustomPromptController customPromptController;
        
        [Header("WebRTC")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private enum MenuState
        {
            MainMenu,
            TimeTravel,
            VirtualMirror,
            Biome,
            VideoGame,
            Custom
        }
        
        private MenuState currentState = MenuState.MainMenu;
        private int selectedIndex = 0;
        private List<MenuItem> menuItems = new List<MenuItem>();
        private bool menuVisible = true;
        
        private float joystickCooldown = 0.2f;
        private float lastJoystickInput = 0f;
        
        private class MenuItem
        {
            public string name;
            public string description;
            public MenuState targetState;
            public GameObject gameObject;
        }
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            InitializeMainMenu();
            UpdateMenuDisplay();
        }
        
        private void InitializeMainMenu()
        {
            menuItems.Clear();
            
            menuItems.Add(new MenuItem
            {
                name = "Time Travel",
                description = "View your environment in different historical periods",
                targetState = MenuState.TimeTravel
            });
            
            menuItems.Add(new MenuItem
            {
                name = "Virtual Mirror",
                description = "Try on different clothing and styles",
                targetState = MenuState.VirtualMirror
            });
            
            menuItems.Add(new MenuItem
            {
                name = "Biomes & Countries",
                description = "Transform your room to different locations",
                targetState = MenuState.Biome
            });
            
            menuItems.Add(new MenuItem
            {
                name = "Video Game Styles",
                description = "See your world as your favorite games",
                targetState = MenuState.VideoGame
            });
            
            menuItems.Add(new MenuItem
            {
                name = "Custom Prompt",
                description = "Type your own transformation prompt",
                targetState = MenuState.Custom
            });
            
            CreateMenuItems();
        }
        
        private void CreateMenuItems()
        {
            // Clear existing menu items
            if (menuItemContainer != null)
            {
                foreach (Transform child in menuItemContainer)
                {
                    Destroy(child.gameObject);
                }
            }
            
            // Create menu item UI elements
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItemPrefab != null && menuItemContainer != null)
                {
                    GameObject itemObj = Instantiate(menuItemPrefab, menuItemContainer);
                    menuItems[i].gameObject = itemObj;
                    
                    TMP_Text nameText = itemObj.transform.Find("Name")?.GetComponent<TMP_Text>();
                    TMP_Text descText = itemObj.transform.Find("Description")?.GetComponent<TMP_Text>();
                    
                    if (nameText != null) nameText.text = menuItems[i].name;
                    if (descText != null) descText.text = menuItems[i].description;
                }
            }
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            // Hamburger button (Start button on Quest) - Toggle menu visibility
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                ToggleMenu();
            }
            
            if (!menuVisible) return;
            
            // Left trigger - Back/Cancel
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            {
                HandleBackButton();
            }
            
            // Right trigger - Confirm/Select
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                HandleConfirmButton();
            }
            
            // Joystick navigation (up/down)
            if (Time.time - lastJoystickInput > joystickCooldown)
            {
                Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                if (joystick.y > 0.5f) // Up
                {
                    NavigateUp();
                    lastJoystickInput = Time.time;
                }
                else if (joystick.y < -0.5f) // Down
                {
                    NavigateDown();
                    lastJoystickInput = Time.time;
                }
            }
        }
        
        private void ToggleMenu()
        {
            menuVisible = !menuVisible;
            if (menuPanel != null)
            {
                menuPanel.SetActive(menuVisible);
            }
            Debug.Log($"Menu visibility: {menuVisible}");
        }
        
        private void HandleBackButton()
        {
            if (currentState == MenuState.MainMenu)
            {
                // Already at main menu, do nothing or hide menu
                ToggleMenu();
            }
            else
            {
                // Return to main menu from any feature
                ReturnToMainMenu();
            }
        }
        
        private void HandleConfirmButton()
        {
            if (currentState == MenuState.MainMenu)
            {
                // Select menu item
                if (selectedIndex >= 0 && selectedIndex < menuItems.Count)
                {
                    ActivateFeature(menuItems[selectedIndex].targetState);
                }
            }
            else
            {
                // Feature-specific confirm action
                ConfirmFeatureAction();
            }
        }
        
        private void NavigateUp()
        {
            if (currentState == MenuState.MainMenu)
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = menuItems.Count - 1;
                UpdateMenuDisplay();
            }
            else
            {
                // Feature-specific navigation
                NavigateFeatureUp();
            }
        }
        
        private void NavigateDown()
        {
            if (currentState == MenuState.MainMenu)
            {
                selectedIndex++;
                if (selectedIndex >= menuItems.Count) selectedIndex = 0;
                UpdateMenuDisplay();
            }
            else
            {
                // Feature-specific navigation
                NavigateFeatureDown();
            }
        }
        
        private void UpdateMenuDisplay()
        {
            if (titleText != null)
            {
                titleText.text = "Decart AI - Main Menu";
            }
            
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i].gameObject != null)
                {
                    Image background = menuItems[i].gameObject.GetComponent<Image>();
                    if (background != null)
                    {
                        background.color = (i == selectedIndex) ? new Color(0.3f, 0.5f, 0.8f, 0.8f) : new Color(0.2f, 0.2f, 0.2f, 0.6f);
                    }
                }
            }
        }
        
        private void ActivateFeature(MenuState newState)
        {
            currentState = newState;
            
            // Deactivate all features
            DeactivateAllFeatures();
            
            // Activate selected feature
            switch (newState)
            {
                case MenuState.TimeTravel:
                    if (timeTravelController != null)
                    {
                        timeTravelController.gameObject.SetActive(true);
                        timeTravelController.Activate();
                    }
                    break;
                    
                case MenuState.VirtualMirror:
                    if (virtualMirrorController != null)
                    {
                        virtualMirrorController.gameObject.SetActive(true);
                        virtualMirrorController.Activate();
                    }
                    break;
                    
                case MenuState.Biome:
                    if (biomeController != null)
                    {
                        biomeController.gameObject.SetActive(true);
                        biomeController.Activate();
                    }
                    break;
                    
                case MenuState.VideoGame:
                    if (videoGameController != null)
                    {
                        videoGameController.gameObject.SetActive(true);
                        videoGameController.Activate();
                    }
                    break;
                    
                case MenuState.Custom:
                    if (customPromptController != null)
                    {
                        customPromptController.gameObject.SetActive(true);
                        customPromptController.Activate();
                    }
                    break;
            }
            
            Debug.Log($"Activated feature: {newState}");
        }
        
        private void DeactivateAllFeatures()
        {
            if (timeTravelController != null)
            {
                timeTravelController.gameObject.SetActive(false);
            }
            if (virtualMirrorController != null)
            {
                virtualMirrorController.gameObject.SetActive(false);
            }
            if (biomeController != null)
            {
                biomeController.gameObject.SetActive(false);
            }
            if (videoGameController != null)
            {
                videoGameController.gameObject.SetActive(false);
            }
            if (customPromptController != null)
            {
                customPromptController.gameObject.SetActive(false);
            }
        }
        
        private void ReturnToMainMenu()
        {
            currentState = MenuState.MainMenu;
            DeactivateAllFeatures();
            selectedIndex = 0;
            UpdateMenuDisplay();
            
            if (titleText != null)
            {
                titleText.text = "Decart AI - Main Menu";
            }
        }
        
        private void ConfirmFeatureAction()
        {
            switch (currentState)
            {
                case MenuState.TimeTravel:
                    if (timeTravelController != null) timeTravelController.Confirm();
                    break;
                case MenuState.VirtualMirror:
                    if (virtualMirrorController != null) virtualMirrorController.Confirm();
                    break;
                case MenuState.Biome:
                    if (biomeController != null) biomeController.Confirm();
                    break;
                case MenuState.VideoGame:
                    if (videoGameController != null) videoGameController.Confirm();
                    break;
                case MenuState.Custom:
                    if (customPromptController != null) customPromptController.Confirm();
                    break;
            }
        }
        
        private void NavigateFeatureUp()
        {
            switch (currentState)
            {
                case MenuState.TimeTravel:
                    if (timeTravelController != null) timeTravelController.NavigateUp();
                    break;
                case MenuState.VirtualMirror:
                    if (virtualMirrorController != null) virtualMirrorController.NavigateUp();
                    break;
                case MenuState.Biome:
                    if (biomeController != null) biomeController.NavigateUp();
                    break;
                case MenuState.VideoGame:
                    if (videoGameController != null) videoGameController.NavigateUp();
                    break;
                case MenuState.Custom:
                    if (customPromptController != null) customPromptController.NavigateUp();
                    break;
            }
        }
        
        private void NavigateFeatureDown()
        {
            switch (currentState)
            {
                case MenuState.TimeTravel:
                    if (timeTravelController != null) timeTravelController.NavigateDown();
                    break;
                case MenuState.VirtualMirror:
                    if (virtualMirrorController != null) virtualMirrorController.NavigateDown();
                    break;
                case MenuState.Biome:
                    if (biomeController != null) biomeController.NavigateDown();
                    break;
                case MenuState.VideoGame:
                    if (videoGameController != null) videoGameController.NavigateDown();
                    break;
                case MenuState.Custom:
                    if (customPromptController != null) customPromptController.NavigateDown();
                    break;
            }
        }
        
        public WebRTCConnection GetWebRTCConnection()
        {
            return webRtcConnection;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Main menu manager for Quest 3 Decart app with joystick navigation.
    /// Handles menu display, navigation, and feature selection.
    /// Navigation: Joystick Up/Down to navigate, Right Trigger to confirm, Left Trigger to go back, Hamburger button to toggle menu.
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu UI References")]
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Transform menuItemsContainer;
        [SerializeField] private GameObject menuItemPrefab;
        
        [Header("Feature Managers")]
        [SerializeField] private TimeTravelFeature timeTravelFeature;
        [SerializeField] private VirtualTryOnFeature virtualTryOnFeature;
        [SerializeField] private BiomeTransformFeature biomeTransformFeature;
        [SerializeField] private VideoGameStyleFeature videoGameStyleFeature;
        [SerializeField] private CustomPromptFeature customPromptFeature;

        [Header("Settings")]
        [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.7f);
        [SerializeField] private Color selectedColor = new Color(0.2f, 0.6f, 1f, 1f);
        [SerializeField] private float navigationCooldown = 0.2f;

        private List<MenuItem> menuItems = new List<MenuItem>();
        private int currentMenuIndex = 0;
        private bool isMenuVisible = true;
        private float lastNavigationTime = 0f;
        private MenuState currentState = MenuState.MainMenu;
        
        private enum MenuState
        {
            MainMenu,
            TimeTravel,
            VirtualTryOn,
            BiomeTransform,
            VideoGameStyle,
            CustomPrompt
        }

        private class MenuItem
        {
            public GameObject gameObject;
            public TMP_Text text;
            public Image background;
            public System.Action onSelect;
        }

        private void Start()
        {
            InitializeMainMenu();
            UpdateMenuSelection();
            
            // Start with menu visible
            SetMenuVisibility(true);
        }

        private void Update()
        {
            HandleMenuToggle();
            
            if (!isMenuVisible || currentState != MenuState.MainMenu)
                return;

            HandleNavigation();
            HandleSelection();
            HandleBackButton();
        }

        private void InitializeMainMenu()
        {
            // Clear existing menu items
            foreach (Transform child in menuItemsContainer)
            {
                Destroy(child.gameObject);
            }
            menuItems.Clear();

            // Create menu items
            CreateMenuItem("Time Travel", () => EnterTimeTravel());
            CreateMenuItem("Virtual Try-On", () => EnterVirtualTryOn());
            CreateMenuItem("Biome Transform", () => EnterBiomeTransform());
            CreateMenuItem("Video Game Style", () => EnterVideoGameStyle());
            CreateMenuItem("Custom Prompt", () => EnterCustomPrompt());

            titleText.text = "Decart Quest 3 - Main Menu";
        }

        private void CreateMenuItem(string text, System.Action onSelect)
        {
            GameObject itemObj = Instantiate(menuItemPrefab, menuItemsContainer);
            TMP_Text itemText = itemObj.GetComponentInChildren<TMP_Text>();
            Image itemBg = itemObj.GetComponent<Image>();

            if (itemText != null)
                itemText.text = text;

            MenuItem item = new MenuItem
            {
                gameObject = itemObj,
                text = itemText,
                background = itemBg,
                onSelect = onSelect
            };

            menuItems.Add(item);
        }

        private void HandleMenuToggle()
        {
            // Hamburger button (Start button on Quest) toggles menu
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                SetMenuVisibility(!isMenuVisible);
            }
        }

        private void SetMenuVisibility(bool visible)
        {
            isMenuVisible = visible;
            menuPanel.SetActive(visible);
            
            // When hiding menu, return to main menu state
            if (!visible && currentState != MenuState.MainMenu)
            {
                ReturnToMainMenu();
            }
        }

        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown)
                return;

            Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            // Navigate up
            if (joystick.y > 0.5f)
            {
                currentMenuIndex--;
                if (currentMenuIndex < 0)
                    currentMenuIndex = menuItems.Count - 1;
                
                lastNavigationTime = Time.time;
                UpdateMenuSelection();
            }
            // Navigate down
            else if (joystick.y < -0.5f)
            {
                currentMenuIndex++;
                if (currentMenuIndex >= menuItems.Count)
                    currentMenuIndex = 0;
                
                lastNavigationTime = Time.time;
                UpdateMenuSelection();
            }
        }

        private void HandleSelection()
        {
            // Right trigger to confirm
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                if (currentMenuIndex >= 0 && currentMenuIndex < menuItems.Count)
                {
                    menuItems[currentMenuIndex].onSelect?.Invoke();
                }
            }
        }

        private void HandleBackButton()
        {
            // Left trigger to go back
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                // In main menu, left trigger could hide the menu
                SetMenuVisibility(false);
            }
        }

        private void UpdateMenuSelection()
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i].background != null)
                {
                    menuItems[i].background.color = (i == currentMenuIndex) ? selectedColor : normalColor;
                }
                
                if (menuItems[i].text != null)
                {
                    menuItems[i].text.fontStyle = (i == currentMenuIndex) ? FontStyles.Bold : FontStyles.Normal;
                }
            }
        }

        // Feature entry points
        private void EnterTimeTravel()
        {
            currentState = MenuState.TimeTravel;
            SetMenuVisibility(false);
            if (timeTravelFeature != null)
                timeTravelFeature.Activate();
        }

        private void EnterVirtualTryOn()
        {
            currentState = MenuState.VirtualTryOn;
            SetMenuVisibility(false);
            if (virtualTryOnFeature != null)
                virtualTryOnFeature.Activate();
        }

        private void EnterBiomeTransform()
        {
            currentState = MenuState.BiomeTransform;
            SetMenuVisibility(false);
            if (biomeTransformFeature != null)
                biomeTransformFeature.Activate();
        }

        private void EnterVideoGameStyle()
        {
            currentState = MenuState.VideoGameStyle;
            SetMenuVisibility(false);
            if (videoGameStyleFeature != null)
                videoGameStyleFeature.Activate();
        }

        private void EnterCustomPrompt()
        {
            currentState = MenuState.CustomPrompt;
            SetMenuVisibility(false);
            if (customPromptFeature != null)
                customPromptFeature.Activate();
        }

        public void ReturnToMainMenu()
        {
            currentState = MenuState.MainMenu;
            
            // Deactivate all features
            if (timeTravelFeature != null)
                timeTravelFeature.Deactivate();
            if (virtualTryOnFeature != null)
                virtualTryOnFeature.Deactivate();
            if (biomeTransformFeature != null)
                biomeTransformFeature.Deactivate();
            if (videoGameStyleFeature != null)
                videoGameStyleFeature.Deactivate();
            if (customPromptFeature != null)
                customPromptFeature.Deactivate();
            
            SetMenuVisibility(true);
        }
    }
}

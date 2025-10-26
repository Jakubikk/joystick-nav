using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Main menu system for Quest AI transformation app.
    /// Handles joystick navigation and menu state management.
    /// Navigation: Joystick Up/Down = Navigate, Right Trigger = Confirm, Left Trigger = Back, Hamburger = Toggle Menu
    /// </summary>
    public class MenuSystem : MonoBehaviour
    {
        [Header("Menu UI References")]
        [SerializeField] private GameObject menuCanvas;
        [SerializeField] private Transform menuItemsContainer;
        [SerializeField] private GameObject menuItemPrefab;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;

        [Header("Feature Controllers")]
        [SerializeField] private TimeTravelController timeTravelController;
        [SerializeField] private VirtualMirrorController virtualMirrorController;
        [SerializeField] private BiomeController biomeController;
        [SerializeField] private VideoGameWorldController videoGameController;
        [SerializeField] private CustomPromptController customPromptController;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        private List<MenuItem> menuItems = new List<MenuItem>();
        private int currentMenuIndex = 0;
        private bool menuVisible = true;
        private MenuState currentState = MenuState.MainMenu;
        private float inputCooldown = 0f;
        private const float INPUT_DELAY = 0.2f;

        private enum MenuState
        {
            MainMenu,
            TimeTravel,
            VirtualMirror,
            Biome,
            VideoGame,
            CustomPrompt
        }

        private class MenuItem
        {
            public string title;
            public string description;
            public MenuState targetState;
            public GameObject uiElement;
        }

        private void Awake()
        {
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            menuItems.Clear();

            // Create main menu items
            AddMenuItem("Time Travel", "Travel through time and see your world in different eras", MenuState.TimeTravel);
            AddMenuItem("Virtual Mirror", "Stand in front of a mirror and try on different clothing", MenuState.VirtualMirror);
            AddMenuItem("Biome Transformation", "Transform your environment to different countries and biomes", MenuState.Biome);
            AddMenuItem("Video Game Worlds", "Experience your reality as various video game worlds", MenuState.VideoGame);
            AddMenuItem("Custom Prompt", "Enter your own AI transformation prompt", MenuState.CustomPrompt);

            UpdateMenuDisplay();
        }

        private void AddMenuItem(string title, string description, MenuState targetState)
        {
            var item = new MenuItem
            {
                title = title,
                description = description,
                targetState = targetState
            };

            if (menuItemPrefab != null && menuItemsContainer != null)
            {
                item.uiElement = Instantiate(menuItemPrefab, menuItemsContainer);
                var text = item.uiElement.GetComponentInChildren<TMP_Text>();
                if (text != null)
                {
                    text.text = title;
                }
            }

            menuItems.Add(item);
        }

        private void Update()
        {
            if (inputCooldown > 0)
            {
                inputCooldown -= Time.deltaTime;
                return;
            }

            HandleMenuToggle();
            
            if (!menuVisible) return;

            HandleNavigation();
            HandleSelection();
            HandleBack();
        }

        private void HandleMenuToggle()
        {
            // Hamburger button (Menu button on Quest controllers)
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                menuVisible = !menuVisible;
                menuCanvas?.SetActive(menuVisible);
                inputCooldown = INPUT_DELAY;
            }
        }

        private void HandleNavigation()
        {
            if (currentState != MenuState.MainMenu) return;

            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            // Navigate up
            if (joystickInput.y > 0.7f)
            {
                currentMenuIndex--;
                if (currentMenuIndex < 0) currentMenuIndex = menuItems.Count - 1;
                UpdateMenuDisplay();
                inputCooldown = INPUT_DELAY;
            }
            // Navigate down
            else if (joystickInput.y < -0.7f)
            {
                currentMenuIndex++;
                if (currentMenuIndex >= menuItems.Count) currentMenuIndex = 0;
                UpdateMenuDisplay();
                inputCooldown = INPUT_DELAY;
            }
        }

        private void HandleSelection()
        {
            // Right trigger to confirm
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (currentState == MenuState.MainMenu && currentMenuIndex < menuItems.Count)
                {
                    SelectMenuItem(menuItems[currentMenuIndex]);
                    inputCooldown = INPUT_DELAY;
                }
            }
        }

        private void HandleBack()
        {
            // Left trigger to go back
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                if (currentState != MenuState.MainMenu)
                {
                    ReturnToMainMenu();
                    inputCooldown = INPUT_DELAY;
                }
            }
        }

        private void SelectMenuItem(MenuItem item)
        {
            currentState = item.targetState;

            // Deactivate all feature controllers first
            DeactivateAllFeatures();

            // Activate selected feature
            switch (item.targetState)
            {
                case MenuState.TimeTravel:
                    timeTravelController?.Activate();
                    break;
                case MenuState.VirtualMirror:
                    virtualMirrorController?.Activate();
                    break;
                case MenuState.Biome:
                    biomeController?.Activate();
                    break;
                case MenuState.VideoGame:
                    videoGameController?.Activate();
                    break;
                case MenuState.CustomPrompt:
                    customPromptController?.Activate();
                    break;
            }

            Debug.Log($"MenuSystem: Selected {item.title}");
        }

        private void ReturnToMainMenu()
        {
            currentState = MenuState.MainMenu;
            DeactivateAllFeatures();
            UpdateMenuDisplay();
            Debug.Log("MenuSystem: Returned to main menu");
        }

        private void DeactivateAllFeatures()
        {
            timeTravelController?.Deactivate();
            virtualMirrorController?.Deactivate();
            biomeController?.Deactivate();
            videoGameController?.Deactivate();
            customPromptController?.Deactivate();
        }

        private void UpdateMenuDisplay()
        {
            if (menuItems.Count == 0) return;

            // Update selection highlighting
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i].uiElement != null)
                {
                    var text = menuItems[i].uiElement.GetComponentInChildren<TMP_Text>();
                    if (text != null)
                    {
                        text.color = (i == currentMenuIndex) ? Color.yellow : Color.white;
                        text.fontStyle = (i == currentMenuIndex) ? FontStyles.Bold : FontStyles.Normal;
                    }
                }
            }

            // Update title and description
            if (currentMenuIndex < menuItems.Count)
            {
                if (titleText != null)
                    titleText.text = menuItems[currentMenuIndex].title;
                
                if (descriptionText != null)
                    descriptionText.text = menuItems[currentMenuIndex].description;
            }
        }

        public bool IsMenuVisible()
        {
            return menuVisible;
        }

        public void HideMenu()
        {
            menuVisible = false;
            menuCanvas?.SetActive(false);
        }

        public void ShowMenu()
        {
            menuVisible = true;
            menuCanvas?.SetActive(true);
        }
    }
}

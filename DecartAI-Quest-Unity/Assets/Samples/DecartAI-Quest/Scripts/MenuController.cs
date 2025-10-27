using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Manages the main menu UI with joystick navigation for Quest 3
    /// Navigation: Joystick up/down to navigate, Right trigger to confirm, Left trigger to go back, Hamburger to hide/show
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private RectTransform menuOptionsContainer;
        [SerializeField] private GameObject menuItemPrefab;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;

        [Header("Feature Panels")]
        [SerializeField] private GameObject timeTravelPanel;
        [SerializeField] private GameObject virtualTryOnPanel;
        [SerializeField] private GameObject biomePanel;
        [SerializeField] private GameObject videoGamePanel;
        [SerializeField] private GameObject customPromptPanel;

        [Header("Audio")]
        [SerializeField] private AudioClip navigationSound;
        [SerializeField] private AudioClip confirmSound;
        [SerializeField] private AudioClip backSound;

        private List<MenuOption> menuOptions;
        private List<GameObject> menuItemObjects;
        private int currentSelectionIndex = 0;
        private bool isMenuVisible = true;
        private AudioSource audioSource;

        // Controller for feature panels
        private TimeTravelController timeTravelController;
        private VirtualTryOnController virtualTryOnController;
        private BiomeController biomeController;
        private VideoGameController videoGameController;
        private CustomPromptController customPromptController;

        private void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            InitializeMenuOptions();
            CreateMenuItems();
            
            // Get or add feature controllers
            timeTravelController = timeTravelPanel?.GetComponent<TimeTravelController>();
            virtualTryOnController = virtualTryOnPanel?.GetComponent<VirtualTryOnController>();
            biomeController = biomePanel?.GetComponent<BiomeController>();
            videoGameController = videoGamePanel?.GetComponent<VideoGameController>();
            customPromptController = customPromptPanel?.GetComponent<CustomPromptController>();
        }

        private void InitializeMenuOptions()
        {
            menuOptions = new List<MenuOption>
            {
                new MenuOption(MenuOptionType.TimeTravel, "Time Travel", 
                    "View your environment as it would look in different historical periods or future years"),
                new MenuOption(MenuOptionType.VirtualTryOn, "Virtual Try-On", 
                    "Stand in front of a mirror and try on different types of clothing"),
                new MenuOption(MenuOptionType.BiomeTransformation, "Biome Transformation", 
                    "Transform your room to look like different countries or environments"),
                new MenuOption(MenuOptionType.VideoGameStyle, "Video Game Style", 
                    "View your environment as if it was from any video game"),
                new MenuOption(MenuOptionType.CustomPrompt, "Custom Prompt", 
                    "Enter your own custom transformation prompt")
            };

            menuItemObjects = new List<GameObject>();
        }

        private void CreateMenuItems()
        {
            if (menuItemPrefab == null || menuOptionsContainer == null)
            {
                Debug.LogError("MenuController: Menu item prefab or container not assigned");
                return;
            }

            // Clear existing items
            foreach (Transform child in menuOptionsContainer)
            {
                Destroy(child.gameObject);
            }
            menuItemObjects.Clear();

            // Create menu items
            for (int i = 0; i < menuOptions.Count; i++)
            {
                GameObject item = Instantiate(menuItemPrefab, menuOptionsContainer);
                TMP_Text itemText = item.GetComponentInChildren<TMP_Text>();
                if (itemText != null)
                {
                    itemText.text = menuOptions[i].DisplayName;
                }
                menuItemObjects.Add(item);
            }

            UpdateSelection();
        }

        private void Start()
        {
            // Hide all feature panels initially
            HideAllFeaturePanels();
            UpdateMenuVisibility();
            UpdateSelection();
        }

        private void Update()
        {
            HandleMenuInput();
        }

        private void HandleMenuInput()
        {
            // Hamburger button (Start button) to toggle menu visibility
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                ToggleMenu();
                PlaySound(navigationSound);
            }

            // Only process other inputs if menu is visible
            if (!isMenuVisible) return;

            // Check if we're in a feature panel
            bool inFeaturePanel = IsAnyFeaturePanelActive();

            if (inFeaturePanel)
            {
                // Left trigger to go back to main menu
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    BackToMainMenu();
                    PlaySound(backSound);
                }
            }
            else
            {
                // Main menu navigation
                Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

                // Joystick up/down navigation with deadzone
                if (joystickInput.y > 0.5f && !IsJoystickCooldown())
                {
                    NavigateUp();
                    StartJoystickCooldown();
                }
                else if (joystickInput.y < -0.5f && !IsJoystickCooldown())
                {
                    NavigateDown();
                    StartJoystickCooldown();
                }

                // Right trigger to confirm selection
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    ConfirmSelection();
                    PlaySound(confirmSound);
                }
            }
        }

        private float joystickCooldownTime = 0f;
        private const float JOYSTICK_COOLDOWN = 0.2f;

        private bool IsJoystickCooldown()
        {
            return Time.time < joystickCooldownTime;
        }

        private void StartJoystickCooldown()
        {
            joystickCooldownTime = Time.time + JOYSTICK_COOLDOWN;
        }

        private void NavigateUp()
        {
            if (menuOptions.Count == 0) return;

            currentSelectionIndex--;
            if (currentSelectionIndex < 0)
            {
                currentSelectionIndex = menuOptions.Count - 1;
            }
            UpdateSelection();
            PlaySound(navigationSound);
        }

        private void NavigateDown()
        {
            if (menuOptions.Count == 0) return;

            currentSelectionIndex++;
            if (currentSelectionIndex >= menuOptions.Count)
            {
                currentSelectionIndex = 0;
            }
            UpdateSelection();
            PlaySound(navigationSound);
        }

        private void UpdateSelection()
        {
            if (menuOptions.Count == 0 || menuItemObjects.Count == 0) return;

            // Update visual indication of selected item
            for (int i = 0; i < menuItemObjects.Count; i++)
            {
                Image bgImage = menuItemObjects[i].GetComponent<Image>();
                if (bgImage != null)
                {
                    bgImage.color = (i == currentSelectionIndex) ? 
                        new Color(0.2f, 0.6f, 1f, 0.8f) : // Highlighted color
                        new Color(0.1f, 0.1f, 0.1f, 0.7f); // Normal color
                }
            }

            // Update description text
            if (descriptionText != null && currentSelectionIndex < menuOptions.Count)
            {
                descriptionText.text = menuOptions[currentSelectionIndex].Description;
            }
        }

        private void ConfirmSelection()
        {
            if (currentSelectionIndex >= menuOptions.Count) return;

            MenuOption selected = menuOptions[currentSelectionIndex];
            Debug.Log($"MenuController: Selected {selected.DisplayName}");

            // Show the appropriate feature panel
            HideAllFeaturePanels();
            
            switch (selected.Type)
            {
                case MenuOptionType.TimeTravel:
                    ShowPanel(timeTravelPanel);
                    timeTravelController?.OnPanelOpened();
                    break;
                case MenuOptionType.VirtualTryOn:
                    ShowPanel(virtualTryOnPanel);
                    virtualTryOnController?.OnPanelOpened();
                    break;
                case MenuOptionType.BiomeTransformation:
                    ShowPanel(biomePanel);
                    biomeController?.OnPanelOpened();
                    break;
                case MenuOptionType.VideoGameStyle:
                    ShowPanel(videoGamePanel);
                    videoGameController?.OnPanelOpened();
                    break;
                case MenuOptionType.CustomPrompt:
                    ShowPanel(customPromptPanel);
                    customPromptController?.OnPanelOpened();
                    break;
            }
        }

        private void BackToMainMenu()
        {
            HideAllFeaturePanels();
            
            // Notify controllers they're being closed
            timeTravelController?.OnPanelClosed();
            virtualTryOnController?.OnPanelClosed();
            biomeController?.OnPanelClosed();
            videoGameController?.OnPanelClosed();
            customPromptController?.OnPanelClosed();
        }

        private void ToggleMenu()
        {
            isMenuVisible = !isMenuVisible;
            UpdateMenuVisibility();
        }

        private void UpdateMenuVisibility()
        {
            if (menuPanel != null)
            {
                menuPanel.SetActive(isMenuVisible);
            }
        }

        private void ShowPanel(GameObject panel)
        {
            if (panel != null)
            {
                panel.SetActive(true);
            }
        }

        private void HideAllFeaturePanels()
        {
            if (timeTravelPanel != null) timeTravelPanel.SetActive(false);
            if (virtualTryOnPanel != null) virtualTryOnPanel.SetActive(false);
            if (biomePanel != null) biomePanel.SetActive(false);
            if (videoGamePanel != null) videoGamePanel.SetActive(false);
            if (customPromptPanel != null) customPromptPanel.SetActive(false);
        }

        private bool IsAnyFeaturePanelActive()
        {
            return (timeTravelPanel != null && timeTravelPanel.activeSelf) ||
                   (virtualTryOnPanel != null && virtualTryOnPanel.activeSelf) ||
                   (biomePanel != null && biomePanel.activeSelf) ||
                   (videoGamePanel != null && videoGamePanel.activeSelf) ||
                   (customPromptPanel != null && customPromptPanel.activeSelf);
        }

        private void PlaySound(AudioClip clip)
        {
            if (audioSource != null && clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}

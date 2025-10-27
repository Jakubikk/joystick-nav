using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    public enum MenuMode
    {
        MainMenu,
        TimeTravel,
        VirtualMirror,
        BiomeTransform,
        VideoGameStyle,
        CustomPrompt
    }

    public class MenuSystem : MonoBehaviour
    {
        [Header("Menu References")]
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Transform menuItemsContainer;
        [SerializeField] private GameObject menuItemPrefab;

        [Header("Feature Panels")]
        [SerializeField] private GameObject timeTravelPanel;
        [SerializeField] private GameObject virtualMirrorPanel;
        [SerializeField] private GameObject biomePanel;
        [SerializeField] private GameObject videoGamePanel;
        [SerializeField] private GameObject customPromptPanel;

        [Header("Time Travel UI")]
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearDisplayText;

        [Header("Virtual Mirror UI")]
        [SerializeField] private Transform clothingOptionsContainer;

        [Header("Biome UI")]
        [SerializeField] private Transform biomeOptionsContainer;

        [Header("Video Game UI")]
        [SerializeField] private Transform gameStyleOptionsContainer;

        [Header("Custom Prompt UI")]
        [SerializeField] private TMP_InputField customPromptInput;
        [SerializeField] private Button submitPromptButton;

        [Header("References")]
        [SerializeField] private WebRTCController webRTCController;

        private MenuMode currentMode = MenuMode.MainMenu;
        private List<MenuItemUI> menuItems = new List<MenuItemUI>();
        private int selectedIndex = 0;
        private bool menuVisible = true;

        // Main menu options
        private readonly string[] mainMenuOptions = new string[]
        {
            "Time Travel",
            "Virtual Mirror",
            "Biome Transform",
            "Video Game Style",
            "Custom Prompt"
        };

        // Time Travel years (1800-2200)
        private const int MIN_YEAR = 1800;
        private const int MAX_YEAR = 2200;

        // Virtual Mirror clothing options
        private readonly string[] clothingOptions = new string[]
        {
            "Medieval Knight Armor",
            "Elegant Evening Dress",
            "Futuristic Space Suit",
            "Traditional Kimono",
            "Superhero Costume",
            "Business Suit",
            "Victorian Era Outfit",
            "Casual Streetwear",
            "Pirate Costume",
            "Wizard Robes"
        };

        // Biome/Country options
        private readonly string[] biomeOptions = new string[]
        {
            "Tropical Rainforest",
            "Arctic Tundra",
            "Desert Oasis",
            "Japanese Garden",
            "Medieval Castle",
            "Futuristic City",
            "Underwater Reef",
            "Mountain Summit",
            "Sahara Desert",
            "Amazon Jungle",
            "Paris Streets",
            "Tokyo Neon District",
            "Ancient Egypt",
            "Swiss Alps"
        };

        // Video Game style options
        private readonly string[] videoGameStyles = new string[]
        {
            "Minecraft",
            "Lego World",
            "Cyberpunk 2077",
            "The Legend of Zelda",
            "Grand Theft Auto",
            "Fortnite",
            "Borderlands Cell-Shaded",
            "Portal Test Chamber",
            "Dark Souls Gothic",
            "Animal Crossing",
            "Fallout Post-Apocalyptic",
            "Super Mario",
            "Halo Sci-Fi",
            "Skyrim Fantasy"
        };

        private void Start()
        {
            if (webRTCController == null)
            {
                webRTCController = FindFirstObjectByType<WebRTCController>();
            }

            InitializeMenu();
            SetupTimeTravelSlider();
            ShowMainMenu();
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            // Hamburger button (Menu/Start button) - toggle menu visibility
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                ToggleMenuVisibility();
            }

            if (!menuVisible) return;

            // Joystick up/down navigation
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            if (joystickInput.y > 0.5f && Time.time > lastNavigationTime + navigationDelay)
            {
                NavigateUp();
                lastNavigationTime = Time.time;
            }
            else if (joystickInput.y < -0.5f && Time.time > lastNavigationTime + navigationDelay)
            {
                NavigateDown();
                lastNavigationTime = Time.time;
            }

            // Right trigger - confirm selection
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                ConfirmSelection();
            }

            // Left trigger - go back
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                GoBack();
            }

            // Handle feature-specific inputs
            HandleFeatureSpecificInput(joystickInput);
        }

        private float lastNavigationTime = 0f;
        private float navigationDelay = 0.2f;

        private void HandleFeatureSpecificInput(Vector2 joystickInput)
        {
            switch (currentMode)
            {
                case MenuMode.TimeTravel:
                    // Horizontal joystick for year slider
                    if (Mathf.Abs(joystickInput.x) > 0.1f)
                    {
                        float newValue = yearSlider.value + (joystickInput.x * Time.deltaTime * 100f);
                        yearSlider.value = Mathf.Clamp(newValue, yearSlider.minValue, yearSlider.maxValue);
                        UpdateYearDisplay();
                    }
                    break;
            }
        }

        private void ToggleMenuVisibility()
        {
            menuVisible = !menuVisible;
            menuPanel.SetActive(menuVisible);
        }

        private void NavigateUp()
        {
            if (menuItems.Count == 0) return;
            
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = menuItems.Count - 1;
            
            UpdateMenuSelection();
        }

        private void NavigateDown()
        {
            if (menuItems.Count == 0) return;
            
            selectedIndex++;
            if (selectedIndex >= menuItems.Count) selectedIndex = 0;
            
            UpdateMenuSelection();
        }

        private void UpdateMenuSelection()
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                menuItems[i].SetSelected(i == selectedIndex);
            }
        }

        private void ConfirmSelection()
        {
            if (menuItems.Count == 0) return;

            switch (currentMode)
            {
                case MenuMode.MainMenu:
                    SelectMainMenuOption(selectedIndex);
                    break;
                case MenuMode.VirtualMirror:
                    ApplyClothingOption(selectedIndex);
                    break;
                case MenuMode.BiomeTransform:
                    ApplyBiomeOption(selectedIndex);
                    break;
                case MenuMode.VideoGameStyle:
                    ApplyVideoGameStyle(selectedIndex);
                    break;
                case MenuMode.TimeTravel:
                    ApplyTimeTravelYear();
                    break;
                case MenuMode.CustomPrompt:
                    SubmitCustomPrompt();
                    break;
            }
        }

        private void GoBack()
        {
            if (currentMode == MenuMode.MainMenu)
            {
                ToggleMenuVisibility();
            }
            else
            {
                ShowMainMenu();
            }
        }

        private void InitializeMenu()
        {
            // Ensure all panels are initially hidden
            HideAllFeaturePanels();
        }

        private void ShowMainMenu()
        {
            currentMode = MenuMode.MainMenu;
            HideAllFeaturePanels();
            
            ClearMenuItems();
            
            titleText.text = "Main Menu";
            
            for (int i = 0; i < mainMenuOptions.Length; i++)
            {
                CreateMenuItem(mainMenuOptions[i]);
            }
            
            selectedIndex = 0;
            UpdateMenuSelection();
        }

        private void SelectMainMenuOption(int index)
        {
            switch (index)
            {
                case 0:
                    ShowTimeTravelMenu();
                    break;
                case 1:
                    ShowVirtualMirrorMenu();
                    break;
                case 2:
                    ShowBiomeMenu();
                    break;
                case 3:
                    ShowVideoGameMenu();
                    break;
                case 4:
                    ShowCustomPromptMenu();
                    break;
            }
        }

        private void ShowTimeTravelMenu()
        {
            currentMode = MenuMode.TimeTravel;
            HideAllFeaturePanels();
            timeTravelPanel.SetActive(true);
            titleText.text = "Time Travel";
            ClearMenuItems();
        }

        private void ShowVirtualMirrorMenu()
        {
            currentMode = MenuMode.VirtualMirror;
            HideAllFeaturePanels();
            virtualMirrorPanel.SetActive(true);
            titleText.text = "Virtual Mirror - Choose Clothing";
            
            ClearMenuItems();
            foreach (var option in clothingOptions)
            {
                CreateMenuItem(option);
            }
            
            selectedIndex = 0;
            UpdateMenuSelection();
        }

        private void ShowBiomeMenu()
        {
            currentMode = MenuMode.BiomeTransform;
            HideAllFeaturePanels();
            biomePanel.SetActive(true);
            titleText.text = "Biome Transform";
            
            ClearMenuItems();
            foreach (var option in biomeOptions)
            {
                CreateMenuItem(option);
            }
            
            selectedIndex = 0;
            UpdateMenuSelection();
        }

        private void ShowVideoGameMenu()
        {
            currentMode = MenuMode.VideoGameStyle;
            HideAllFeaturePanels();
            videoGamePanel.SetActive(true);
            titleText.text = "Video Game Style";
            
            ClearMenuItems();
            foreach (var option in videoGameStyles)
            {
                CreateMenuItem(option);
            }
            
            selectedIndex = 0;
            UpdateMenuSelection();
        }

        private void ShowCustomPromptMenu()
        {
            currentMode = MenuMode.CustomPrompt;
            HideAllFeaturePanels();
            customPromptPanel.SetActive(true);
            titleText.text = "Custom Prompt";
            ClearMenuItems();
            
            // Open Meta keyboard for input
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
        }

        private void HideAllFeaturePanels()
        {
            if (timeTravelPanel != null) timeTravelPanel.SetActive(false);
            if (virtualMirrorPanel != null) virtualMirrorPanel.SetActive(false);
            if (biomePanel != null) biomePanel.SetActive(false);
            if (videoGamePanel != null) videoGamePanel.SetActive(false);
            if (customPromptPanel != null) customPromptPanel.SetActive(false);
        }

        private void CreateMenuItem(string text)
        {
            if (menuItemPrefab == null || menuItemsContainer == null) return;
            
            GameObject itemObj = Instantiate(menuItemPrefab, menuItemsContainer);
            MenuItemUI menuItem = itemObj.GetComponent<MenuItemUI>();
            
            if (menuItem == null)
            {
                menuItem = itemObj.AddComponent<MenuItemUI>();
            }
            
            menuItem.SetText(text);
            menuItems.Add(menuItem);
        }

        private void ClearMenuItems()
        {
            foreach (var item in menuItems)
            {
                if (item != null && item.gameObject != null)
                {
                    Destroy(item.gameObject);
                }
            }
            menuItems.Clear();
        }

        private void SetupTimeTravelSlider()
        {
            if (yearSlider == null) return;
            
            yearSlider.minValue = MIN_YEAR;
            yearSlider.maxValue = MAX_YEAR;
            yearSlider.value = 2024; // Current year as default
            yearSlider.onValueChanged.AddListener((value) => UpdateYearDisplay());
            
            UpdateYearDisplay();
        }

        private void UpdateYearDisplay()
        {
            if (yearDisplayText != null)
            {
                int year = Mathf.RoundToInt(yearSlider.value);
                yearDisplayText.text = $"Year: {year}";
            }
        }

        private void ApplyTimeTravelYear()
        {
            int year = Mathf.RoundToInt(yearSlider.value);
            string prompt = GenerateTimeTravelPrompt(year);
            SendPromptToWebRTC(prompt);
        }

        private string GenerateTimeTravelPrompt(int year)
        {
            // Generate appropriate prompt based on the year
            if (year < 1850)
            {
                return $"Transform the environment to look like the year {year}, with historical architecture, horse-drawn carriages, candlelight, period-appropriate clothing and buildings";
            }
            else if (year < 1900)
            {
                return $"Transform the environment to look like the Victorian era around {year}, with gas lamps, cobblestone streets, industrial revolution aesthetic";
            }
            else if (year < 1950)
            {
                return $"Transform the environment to look like the early 20th century around {year}, with vintage cars, art deco architecture, black and white film aesthetic";
            }
            else if (year < 2000)
            {
                return $"Transform the environment to look like the late 20th century around {year}, with retro technology, period-appropriate fashion and architecture";
            }
            else if (year <= 2024)
            {
                return $"Transform the environment to modern {year} aesthetic with current technology and contemporary design";
            }
            else if (year < 2100)
            {
                return $"Transform the environment to a near-future {year} with advanced technology, holographic displays, sleek modern architecture, flying vehicles";
            }
            else
            {
                return $"Transform the environment to a far-future {year} with highly advanced technology, sci-fi architecture, space-age aesthetic, futuristic cityscapes";
            }
        }

        private void ApplyClothingOption(int index)
        {
            if (index < 0 || index >= clothingOptions.Length) return;
            
            string clothing = clothingOptions[index];
            string prompt = $"Change the person's outfit to {clothing.ToLower()}, maintaining their identity and pose";
            SendPromptToWebRTC(prompt);
        }

        private void ApplyBiomeOption(int index)
        {
            if (index < 0 || index >= biomeOptions.Length) return;
            
            string biome = biomeOptions[index];
            string prompt = $"Transform the environment to look like {biome.ToLower()}, maintaining the layout but changing the style, textures, lighting and atmosphere";
            SendPromptToWebRTC(prompt);
        }

        private void ApplyVideoGameStyle(int index)
        {
            if (index < 0 || index >= videoGameStyles.Length) return;
            
            string gameStyle = videoGameStyles[index];
            string prompt = $"Transform the environment to look like {gameStyle} video game style, with appropriate graphics, textures, and visual effects";
            SendPromptToWebRTC(prompt);
        }

        private void SubmitCustomPrompt()
        {
            if (customPromptInput == null) return;
            
            string prompt = customPromptInput.text;
            if (!string.IsNullOrEmpty(prompt))
            {
                SendPromptToWebRTC(prompt);
                customPromptInput.text = "";
            }
        }

        private void SendPromptToWebRTC(string prompt)
        {
            if (webRTCController != null)
            {
                webRTCController.QueueCustomPrompt(prompt);
                Debug.Log($"MenuSystem: Sent prompt - {prompt}");
            }
            else
            {
                Debug.LogError("MenuSystem: WebRTCController reference is missing!");
            }
        }

        /// <summary>
        /// Public method for KeyboardInputManager to submit custom prompts
        /// </summary>
        public void SubmitCustomPromptFromKeyboard(string prompt)
        {
            if (!string.IsNullOrEmpty(prompt))
            {
                SendPromptToWebRTC(prompt);
            }
        }
    }

    // Simple menu item UI component
    public class MenuItemUI : MonoBehaviour
    {
        private TMP_Text textComponent;
        private Image backgroundImage;
        private bool isSelected = false;

        [SerializeField] private Color normalColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        [SerializeField] private Color selectedColor = new Color(0.3f, 0.5f, 0.8f, 0.9f);

        private void Awake()
        {
            textComponent = GetComponentInChildren<TMP_Text>();
            backgroundImage = GetComponent<Image>();
            
            if (backgroundImage == null)
            {
                backgroundImage = gameObject.AddComponent<Image>();
            }
        }

        public void SetText(string text)
        {
            if (textComponent != null)
            {
                textComponent.text = text;
            }
        }

        public void SetSelected(bool selected)
        {
            isSelected = selected;
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            if (backgroundImage != null)
            {
                backgroundImage.color = isSelected ? selectedColor : normalColor;
            }
            
            if (textComponent != null)
            {
                textComponent.fontSize = isSelected ? 28 : 24;
            }
        }
    }
}

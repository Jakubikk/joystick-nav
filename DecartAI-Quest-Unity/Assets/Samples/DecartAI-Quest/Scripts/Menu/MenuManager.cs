using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace QuestCameraKit.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu References")]
        [SerializeField] private GameObject menuRootPanel;
        [SerializeField] private TMP_Text menuTitleText;
        
        [Header("Main Menu")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private List<MenuButton> mainMenuButtons;
        
        [Header("Submenus")]
        [SerializeField] private GameObject timeTravelPanel;
        [SerializeField] private GameObject clothingPanel;
        [SerializeField] private GameObject biomePanel;
        [SerializeField] private GameObject videoGamePanel;
        [SerializeField] private GameObject customPromptPanel;
        
        [Header("Helper Components")]
        [SerializeField] private TimeTravelSliderController timeTravelController;
        [SerializeField] private OptionListDisplay clothingListDisplay;
        [SerializeField] private OptionListDisplay biomeListDisplay;
        [SerializeField] private OptionListDisplay videoGameListDisplay;
        [SerializeField] private ControlsDisplay controlsDisplay;
        
        [Header("Navigation")]
        [SerializeField] private int currentMenuIndex = 0;
        [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.7f);
        [SerializeField] private Color selectedColor = new Color(0.2f, 0.8f, 1f, 1f);
        
        private MenuState currentState = MenuState.MainMenu;
        private bool menuVisible = true;
        private float inputCooldown = 0f;
        private const float INPUT_DELAY = 0.2f;
        
        // Feature-specific data
        private int currentClothingIndex = 0;
        private int currentBiomeIndex = 0;
        private int currentVideoGameIndex = 0;
        
        // Reference to WebRTC controller
        private WebRTC.WebRTCController webRTCController;
        
        private enum MenuState
        {
            MainMenu,
            TimeTravel,
            Clothing,
            Biome,
            VideoGame,
            CustomPrompt
        }
        
        private void Start()
        {
            webRTCController = FindFirstObjectByType<WebRTC.WebRTCController>();
            
            if (webRTCController == null)
            {
                Debug.LogError("MenuManager: WebRTCController not found!");
            }
            
            InitializeMenu();
            UpdateMenuDisplay();
        }
        
        private void InitializeMenu()
        {
            // Hide all panels initially
            if (mainMenuPanel) mainMenuPanel.SetActive(true);
            if (timeTravelPanel) timeTravelPanel.SetActive(false);
            if (clothingPanel) clothingPanel.SetActive(false);
            if (biomePanel) biomePanel.SetActive(false);
            if (videoGamePanel) videoGamePanel.SetActive(false);
            if (customPromptPanel) customPromptPanel.SetActive(false);
            
            // Initialize helper components
            InitializeClothingOptions();
            InitializeBiomeOptions();
            InitializeVideoGameOptions();
            
            currentState = MenuState.MainMenu;
            currentMenuIndex = 0;
            menuVisible = true;
            
            // Update controls display
            if (controlsDisplay != null)
            {
                controlsDisplay.UpdateInstructions(MenuContext.MainMenu);
            }
        }
        
        private void InitializeClothingOptions()
        {
            if (clothingListDisplay == null) return;
            
            var options = new List<OptionData>();
            foreach (var clothing in GetClothingOptions())
            {
                options.Add(new OptionData(clothing.name, clothing.prompt));
            }
            clothingListDisplay.SetOptions(options, "Virtual Mirror - Try On Clothing");
            clothingListDisplay.SetActive(false);
        }
        
        private void InitializeBiomeOptions()
        {
            if (biomeListDisplay == null) return;
            
            var options = new List<OptionData>();
            foreach (var biome in GetBiomeOptions())
            {
                options.Add(new OptionData(biome.name, biome.prompt));
            }
            biomeListDisplay.SetOptions(options, "Biome Transformation");
            biomeListDisplay.SetActive(false);
        }
        
        private void InitializeVideoGameOptions()
        {
            if (videoGameListDisplay == null) return;
            
            var options = new List<OptionData>();
            foreach (var game in GetVideoGameOptions())
            {
                options.Add(new OptionData(game.name, game.prompt));
            }
            videoGameListDisplay.SetOptions(options, "Video Game Styles");
            videoGameListDisplay.SetActive(false);
        }
        
        private void Update()
        {
            if (inputCooldown > 0f)
            {
                inputCooldown -= Time.deltaTime;
            }
            
            HandleMenuToggle();
            
            if (menuVisible && inputCooldown <= 0f)
            {
                HandleNavigation();
                HandleConfirm();
                HandleBack();
            }
        }
        
        private void HandleMenuToggle()
        {
            // Hamburger button (Start button on Quest) to toggle menu
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                menuVisible = !menuVisible;
                if (menuRootPanel != null)
                {
                    menuRootPanel.SetActive(menuVisible);
                }
                inputCooldown = INPUT_DELAY;
            }
        }
        
        private void HandleNavigation()
        {
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            // Navigate up/down with joystick
            if (joystickInput.y > 0.5f)
            {
                NavigateUp();
                inputCooldown = INPUT_DELAY;
            }
            else if (joystickInput.y < -0.5f)
            {
                NavigateDown();
                inputCooldown = INPUT_DELAY;
            }
            
            // Left/right navigation for submenus
            if (joystickInput.x > 0.5f && currentState != MenuState.MainMenu)
            {
                NavigateRight();
                inputCooldown = INPUT_DELAY;
            }
            else if (joystickInput.x < -0.5f && currentState != MenuState.MainMenu)
            {
                NavigateLeft();
                inputCooldown = INPUT_DELAY;
            }
        }
        
        private void HandleConfirm()
        {
            // Right trigger to confirm
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ConfirmSelection();
                inputCooldown = INPUT_DELAY;
            }
        }
        
        private void HandleBack()
        {
            // Left trigger to go back
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                GoBack();
                inputCooldown = INPUT_DELAY;
            }
        }
        
        private void NavigateUp()
        {
            switch (currentState)
            {
                case MenuState.MainMenu:
                    currentMenuIndex = Mathf.Max(0, currentMenuIndex - 1);
                    UpdateMenuDisplay();
                    break;
                case MenuState.Clothing:
                    if (clothingListDisplay != null) clothingListDisplay.NavigatePrevious();
                    break;
                case MenuState.Biome:
                    if (biomeListDisplay != null) biomeListDisplay.NavigatePrevious();
                    break;
                case MenuState.VideoGame:
                    if (videoGameListDisplay != null) videoGameListDisplay.NavigatePrevious();
                    break;
            }
        }
        
        private void NavigateDown()
        {
            switch (currentState)
            {
                case MenuState.MainMenu:
                    int maxMainMenu = 4; // 5 options total
                    currentMenuIndex = Mathf.Min(maxMainMenu, currentMenuIndex + 1);
                    UpdateMenuDisplay();
                    break;
                case MenuState.Clothing:
                    if (clothingListDisplay != null) clothingListDisplay.NavigateNext();
                    break;
                case MenuState.Biome:
                    if (biomeListDisplay != null) biomeListDisplay.NavigateNext();
                    break;
                case MenuState.VideoGame:
                    if (videoGameListDisplay != null) videoGameListDisplay.NavigateNext();
                    break;
            }
        }
        
        private void NavigateLeft()
        {
            if (currentState == MenuState.Clothing && clothingListDisplay != null)
            {
                clothingListDisplay.NavigatePrevious();
            }
            else if (currentState == MenuState.Biome && biomeListDisplay != null)
            {
                biomeListDisplay.NavigatePrevious();
            }
            else if (currentState == MenuState.VideoGame && videoGameListDisplay != null)
            {
                videoGameListDisplay.NavigatePrevious();
            }
        }
        
        private void NavigateRight()
        {
            if (currentState == MenuState.Clothing && clothingListDisplay != null)
            {
                clothingListDisplay.NavigateNext();
            }
            else if (currentState == MenuState.Biome && biomeListDisplay != null)
            {
                biomeListDisplay.NavigateNext();
            }
            else if (currentState == MenuState.VideoGame && videoGameListDisplay != null)
            {
                videoGameListDisplay.NavigateNext();
            }
        }
        
        private void ConfirmSelection()
        {
            switch (currentState)
            {
                case MenuState.MainMenu:
                    OpenSelectedMenu();
                    break;
                case MenuState.TimeTravel:
                    ApplyTimeTravelYear();
                    break;
                case MenuState.Clothing:
                    ApplyClothingSelection();
                    break;
                case MenuState.Biome:
                    ApplyBiomeSelection();
                    break;
                case MenuState.VideoGame:
                    ApplyVideoGameSelection();
                    break;
                case MenuState.CustomPrompt:
                    OpenMetaKeyboard();
                    break;
            }
        }
        
        private void GoBack()
        {
            if (currentState != MenuState.MainMenu)
            {
                // Return to main menu
                currentState = MenuState.MainMenu;
                currentMenuIndex = 0;
                
                // Hide all submenus
                if (timeTravelPanel) timeTravelPanel.SetActive(false);
                if (clothingPanel) clothingPanel.SetActive(false);
                if (biomePanel) biomePanel.SetActive(false);
                if (videoGamePanel) videoGamePanel.SetActive(false);
                if (customPromptPanel) customPromptPanel.SetActive(false);
                
                // Show main menu
                if (mainMenuPanel) mainMenuPanel.SetActive(true);
                
                UpdateMenuDisplay();
            }
        }
        
        private void OpenSelectedMenu()
        {
            // Hide main menu
            if (mainMenuPanel) mainMenuPanel.SetActive(false);
            
            switch (currentMenuIndex)
            {
                case 0: // Time Travel
                    currentState = MenuState.TimeTravel;
                    if (timeTravelPanel) timeTravelPanel.SetActive(true);
                    if (timeTravelController != null) timeTravelController.SetActive(true);
                    if (controlsDisplay != null) controlsDisplay.UpdateInstructions(MenuContext.TimeTravel);
                    break;
                    
                case 1: // Virtual Mirror/Clothing
                    currentState = MenuState.Clothing;
                    if (clothingPanel) clothingPanel.SetActive(true);
                    if (clothingListDisplay != null)
                    {
                        clothingListDisplay.SetActive(true);
                        clothingListDisplay.SetIndex(0);
                    }
                    if (controlsDisplay != null) controlsDisplay.UpdateInstructions(MenuContext.ClothingList);
                    break;
                    
                case 2: // Biome/Environment
                    currentState = MenuState.Biome;
                    if (biomePanel) biomePanel.SetActive(true);
                    if (biomeListDisplay != null)
                    {
                        biomeListDisplay.SetActive(true);
                        biomeListDisplay.SetIndex(0);
                    }
                    if (controlsDisplay != null) controlsDisplay.UpdateInstructions(MenuContext.BiomeList);
                    break;
                    
                case 3: // Video Game
                    currentState = MenuState.VideoGame;
                    if (videoGamePanel) videoGamePanel.SetActive(true);
                    if (videoGameListDisplay != null)
                    {
                        videoGameListDisplay.SetActive(true);
                        videoGameListDisplay.SetIndex(0);
                    }
                    if (controlsDisplay != null) controlsDisplay.UpdateInstructions(MenuContext.VideoGameList);
                    break;
                    
                case 4: // Custom Prompt
                    currentState = MenuState.CustomPrompt;
                    if (customPromptPanel) customPromptPanel.SetActive(true);
                    if (controlsDisplay != null) controlsDisplay.UpdateInstructions(MenuContext.CustomPrompt);
                    break;
            }
        }
        
        private void UpdateMenuDisplay()
        {
            if (mainMenuButtons == null || mainMenuButtons.Count == 0) return;
            
            for (int i = 0; i < mainMenuButtons.Count; i++)
            {
                if (mainMenuButtons[i] != null && mainMenuButtons[i].buttonImage != null)
                {
                    mainMenuButtons[i].buttonImage.color = (i == currentMenuIndex) ? selectedColor : normalColor;
                }
            }
        }
        
        // Time Travel Methods
        private void ApplyTimeTravelYear()
        {
            if (timeTravelController == null || webRTCController == null) return;
            
            int year = timeTravelController.GetCurrentYear();
            string prompt = GenerateTimeTravelPrompt(year);
            
            Debug.Log($"Applying Time Travel to year: {year}");
            webRTCController.QueueCustomPrompt(prompt);
        }
        
        private string GenerateTimeTravelPrompt(int year)
        {
            if (year < 1900)
            {
                return $"Transform the environment to show how it would look in the year {year}, with historical architecture, cobblestone streets, horse-drawn carriages, gas lamps, period-appropriate clothing and technology from that era";
            }
            else if (year < 1950)
            {
                return $"Transform the environment to show how it would look in the year {year}, with early 20th century architecture, vintage cars, old-fashioned street lamps, period clothing and technology from that era";
            }
            else if (year < 2000)
            {
                return $"Transform the environment to show how it would look in the year {year}, with mid-century modern architecture, classic cars from that decade, retro technology and fashion from that era";
            }
            else if (year <= 2024)
            {
                return $"Transform the environment to show how it would look in the year {year}, with contemporary architecture, modern vehicles, current technology and fashion from that year";
            }
            else if (year < 2100)
            {
                return $"Transform the environment to show a futuristic vision of year {year}, with advanced architecture, flying vehicles, holographic displays, sustainable technology, clean energy systems, modern futuristic clothing and advanced technology";
            }
            else
            {
                return $"Transform the environment to show a far-future vision of year {year}, with ultra-advanced architecture, space-age vehicles, AI-integrated systems, holographic interfaces, advanced robotics, futuristic fashion and revolutionary technology";
            }
        }
        
        // Clothing Methods
        private List<ClothingOption> GetClothingOptions()
        {
            return new List<ClothingOption>
            {
                new ClothingOption("Business Suit", "Change the person's clothing to a professional tailored business suit, charcoal gray wool, slim fit blazer, matching dress pants, white crisp shirt, silk tie, polished leather dress shoes"),
                new ClothingOption("Casual Jeans", "Change the person's clothing to casual denim jeans, blue wash, fitted cut, paired with a comfortable cotton t-shirt, casual sneakers"),
                new ClothingOption("Summer Dress", "Change the person's clothing to a flowing summer dress, light floral pattern, knee-length, sleeveless, soft pastel colors, comfortable sandals"),
                new ClothingOption("Winter Coat", "Change the person's clothing to a warm winter puffer coat, thick insulated material, hood with fur trim, dark color, winter boots, warm gloves and scarf"),
                new ClothingOption("Athletic Wear", "Change the person's clothing to athletic sportswear, moisture-wicking fabric, running shoes, fitted athletic pants or shorts, sports top with breathable mesh"),
                new ClothingOption("Formal Gown", "Change the person's clothing to an elegant formal evening gown, floor-length, luxurious silk or satin fabric, elegant draping, formal accessories, high heels"),
                new ClothingOption("Leather Jacket", "Change the person's clothing to a classic black leather jacket, fitted cut, metal zippers, paired with dark jeans and boots"),
                new ClothingOption("Traditional Kimono", "Change the person's clothing to a traditional Japanese kimono, intricate floral patterns, silk fabric, wide obi belt, wooden geta sandals"),
                new ClothingOption("Medieval Armor", "Change the person's clothing to medieval knight armor, shining steel plates, chainmail, ornate engravings, sword and shield"),
                new ClothingOption("Space Suit", "Change the person's clothing to a futuristic astronaut space suit, white with colored accents, reflective visor helmet, technical details and life support systems"),
                new ClothingOption("Pirate Outfit", "Change the person's clothing to a classic pirate outfit, tricorn hat, long coat, white shirt, leather vest, boots, belt with pistol"),
                new ClothingOption("Victorian Dress", "Change the person's clothing to Victorian era formal dress, elaborate lace details, corset bodice, full skirt with layers, period-appropriate accessories"),
                new ClothingOption("Superhero Costume", "Change the person's clothing to a superhero costume, form-fitting suit, vibrant colors, cape, mask, emblem on chest, boots"),
                new ClothingOption("Chef Uniform", "Change the person's clothing to professional chef whites, double-breasted jacket, chef's hat, checkered pants, apron, professional appearance"),
                new ClothingOption("Beach Wear", "Change the person's clothing to casual beach attire, colorful swim shorts or swimsuit, light cover-up, flip-flops, sun hat, sunglasses")
            };
        }
        
        private void ApplyClothingSelection()
        {
            if (webRTCController == null || clothingListDisplay == null) return;
            
            var option = clothingListDisplay.GetCurrentOption();
            if (option != null)
            {
                Debug.Log($"Applying clothing: {option.name}");
                webRTCController.QueueCustomPrompt(option.fullPrompt);
            }
        }
        
        // Biome Methods
        private List<BiomeOption> GetBiomeOptions()
        {
            return new List<BiomeOption>
            {
                new BiomeOption("Tropical Paradise", "Transform the environment into a tropical paradise, lush palm trees, white sandy beaches, crystal clear turquoise water, vibrant tropical flowers, warm sunny atmosphere"),
                new BiomeOption("Arctic Tundra", "Transform the environment into an arctic tundra, snow-covered ground, ice formations, northern lights in the sky, frozen landscape, cold misty atmosphere"),
                new BiomeOption("Desert Oasis", "Transform the environment into a desert oasis, golden sand dunes, palm trees around water, clear blue sky, warm desert sun, Middle Eastern architecture"),
                new BiomeOption("Rainforest Jungle", "Transform the environment into a dense rainforest jungle, towering trees with thick canopy, hanging vines, exotic plants, misty atmosphere, vibrant green foliage"),
                new BiomeOption("Mountain Summit", "Transform the environment into a mountain summit, rocky peaks, snow-capped mountains, thin clouds, expansive valley views, alpine atmosphere"),
                new BiomeOption("Underwater Reef", "Transform the environment into an underwater coral reef scene, colorful coral formations, tropical fish swimming, blue ocean water, shafts of sunlight from above"),
                new BiomeOption("Autumn Forest", "Transform the environment into an autumn forest, trees with red, orange and yellow leaves, fallen leaves on ground, crisp fall atmosphere, warm golden lighting"),
                new BiomeOption("Cherry Blossom Garden", "Transform the environment into a Japanese cherry blossom garden, pink sakura trees in full bloom, petals falling, peaceful zen atmosphere, traditional stone lanterns"),
                new BiomeOption("Savanna Plains", "Transform the environment into African savanna, golden grass plains, acacia trees, warm sunset lighting, wildlife in distance, open expansive landscape"),
                new BiomeOption("Bamboo Forest", "Transform the environment into a bamboo forest, tall green bamboo stalks, filtered sunlight, peaceful zen atmosphere, stone pathway"),
                new BiomeOption("Volcanic Landscape", "Transform the environment into a volcanic landscape, dark volcanic rock, glowing lava flows, ash in air, dramatic red-orange lighting, intense atmosphere"),
                new BiomeOption("Meadow Flowers", "Transform the environment into a flower meadow, colorful wildflowers, green grass, butterflies, blue sky with white clouds, peaceful spring atmosphere"),
                new BiomeOption("Mangrove Swamp", "Transform the environment into a mangrove swamp, twisted mangrove roots in water, dense vegetation, humid tropical atmosphere, exotic birds"),
                new BiomeOption("Ice Cave", "Transform the environment into an ice cave, blue-tinted ice walls, icicles hanging, frozen formations, ethereal lighting, crystal-clear ice structures"),
                new BiomeOption("Canyon Desert", "Transform the environment into a desert canyon, red rock formations, layers of sedimentary rock, dry desert plants, warm afternoon sun, vast landscape")
            };
        }
        
        private void ApplyBiomeSelection()
        {
            if (webRTCController == null || biomeListDisplay == null) return;
            
            var option = biomeListDisplay.GetCurrentOption();
            if (option != null)
            {
                Debug.Log($"Applying biome: {option.name}");
                webRTCController.QueueCustomPrompt(option.fullPrompt);
            }
        }
        
        // Video Game Methods
        private List<VideoGameOption> GetVideoGameOptions()
        {
            return new List<VideoGameOption>
            {
                new VideoGameOption("Minecraft", "Transform the environment into Minecraft style, everything made of cubic blocks, pixelated textures, blocky terrain, bright vibrant colors, game interface elements"),
                new VideoGameOption("LEGO", "Transform the environment into LEGO world, everything constructed from colorful LEGO bricks, plastic shiny surfaces, studs on top of blocks, LEGO minifigure scale"),
                new VideoGameOption("Grand Theft Auto", "Transform the environment into GTA style, urban city environment, realistic graphics with slight stylization, vehicles and pedestrians, urban architecture, street details"),
                new VideoGameOption("The Legend of Zelda", "Transform the environment into Zelda Breath of the Wild style, cel-shaded graphics, vibrant fantasy colors, magical atmosphere, adventure game aesthetic"),
                new VideoGameOption("Cyberpunk 2077", "Transform the environment into Cyberpunk 2077 style, neon lights, futuristic city, holographic advertisements, rain-slicked streets, high-tech cyberpunk aesthetic"),
                new VideoGameOption("Animal Crossing", "Transform the environment into Animal Crossing style, cute rounded shapes, pastel colors, friendly cartoon aesthetic, cheerful atmosphere, simplified details"),
                new VideoGameOption("Mario Kart", "Transform the environment into Mario Kart racing game style, colorful racing track, item boxes, bright cartoony graphics, playful Nintendo aesthetic"),
                new VideoGameOption("Fortnite", "Transform the environment into Fortnite style, colorful cartoon graphics, stylized character designs, bright vibrant colors, battle royale aesthetic"),
                new VideoGameOption("Red Dead Redemption", "Transform the environment into Red Dead Redemption style, Wild West setting, dusty towns, desert landscape, western frontier atmosphere, realistic graphics"),
                new VideoGameOption("Super Mario", "Transform the environment into Super Mario world style, bright colorful platforms, question blocks, coins, green pipes, classic Nintendo platformer aesthetic"),
                new VideoGameOption("Portal", "Transform the environment into Portal game style, clean white test chambers, orange and blue portal surfaces, futuristic Aperture Science facility aesthetic"),
                new VideoGameOption("Pac-Man", "Transform the environment into Pac-Man style, retro arcade graphics, simple geometric shapes, bright primary colors, classic 1980s game aesthetic, pixelated look"),
                new VideoGameOption("Sonic", "Transform the environment into Sonic the Hedgehog style, fast-paced colorful zones, loop-de-loops, golden rings, bright vibrant Sega genesis aesthetic"),
                new VideoGameOption("Pokemon", "Transform the environment into Pokemon game style, colorful cartoon graphics, tall grass, pokemon creatures visible, trainer aesthetic, friendly game world"),
                new VideoGameOption("Fallout", "Transform the environment into Fallout post-apocalyptic style, retro-futuristic ruins, wasteland landscape, 1950s aesthetic mixed with destruction, vault-tec colors")
            };
        }
        
        private void UpdateVideoGameDisplay()
        
        private void ApplyVideoGameSelection()
        {
            if (webRTCController == null || videoGameListDisplay == null) return;
            
            var option = videoGameListDisplay.GetCurrentOption();
            if (option != null)
            {
                Debug.Log($"Applying video game style: {option.name}");
                webRTCController.QueueCustomPrompt(option.fullPrompt);
            }
        }
        
        // Custom Prompt Methods
        private void OpenMetaKeyboard()
        {
            Debug.Log("Opening Meta keyboard for custom prompt input");
            // Meta Quest keyboard integration will be handled through OVR SDK
            // This is a placeholder for the keyboard functionality
            #if UNITY_ANDROID && !UNITY_EDITOR
            StartCoroutine(ShowMetaKeyboard());
            #else
            Debug.LogWarning("Meta keyboard only available on Quest device");
            #endif
        }
        
        private System.Collections.IEnumerator ShowMetaKeyboard()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            // Use Meta's TouchScreenKeyboard
            TouchScreenKeyboard keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "Enter custom transformation prompt");
            
            while (!keyboard.done)
            {
                yield return null;
            }
            
            if (keyboard.status == TouchScreenKeyboard.Status.Done && !string.IsNullOrEmpty(keyboard.text))
            {
                Debug.Log($"Custom prompt entered: {keyboard.text}");
                if (webRTCController != null)
                {
                    webRTCController.QueueCustomPrompt(keyboard.text);
                }
            }
            #else
            yield return null;
            #endif
        }
        
        // Helper classes
        private class ClothingOption
        {
            public string name;
            public string prompt;
            
            public ClothingOption(string name, string prompt)
            {
                this.name = name;
                this.prompt = prompt;
            }
        }
        
        private class BiomeOption
        {
            public string name;
            public string prompt;
            
            public BiomeOption(string name, string prompt)
            {
                this.name = name;
                this.prompt = prompt;
            }
        }
        
        private class VideoGameOption
        {
            public string name;
            public string prompt;
            
            public VideoGameOption(string name, string prompt)
            {
                this.name = name;
                this.prompt = prompt;
            }
        }
    }
    
    [System.Serializable]
    public class MenuButton
    {
        public string buttonText;
        public Image buttonImage;
        public TMP_Text buttonTextComponent;
    }
}

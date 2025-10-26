using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Video Game Style feature - transform environment to look like any video game.
    /// Uses Decart AI to apply video game aesthetics.
    /// </summary>
    public class VideoGameStyleFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject featurePanel;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text currentStyleText;
        [SerializeField] private TMP_Text instructionsText;
        [SerializeField] private Transform styleListContainer;
        [SerializeField] private GameObject listItemPrefab;

        [Header("WebRTC")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        [Header("Settings")]
        [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.6f);
        [SerializeField] private Color selectedColor = new Color(0.8f, 0.2f, 0.8f, 1f);
        [SerializeField] private float navigationCooldown = 0.2f;

        private MenuManager menuManager;
        private List<GameStyleItem> styleItems = new List<GameStyleItem>();
        private int currentIndex = 0;
        private bool isActive = false;
        private float lastNavigationTime = 0f;

        private class GameStyleItem
        {
            public string name;
            public string prompt;
            public GameObject uiObject;
            public TMP_Text text;
            public Image background;
        }

        // Video game style options
        private Dictionary<string, string> gameStyleOptions = new Dictionary<string, string>()
        {
            // Classic & Retro Games
            { "Minecraft", "Transform to Minecraft blocky voxel world with cube-shaped blocks, pixelated textures, familiar block patterns, and distinctive Minecraft aesthetic" },
            { "LEGO World", "Transform to LEGO brick world with everything made of colorful plastic LEGO bricks, smooth surfaces, studs on top, and toy-like appearance" },
            { "Super Mario Bros", "Transform to Super Mario Bros style with bright colors, cartoon aesthetic, floating blocks, pipes, coins, and Nintendo platformer look" },
            { "The Legend of Zelda", "Transform to Zelda fantasy world with cel-shaded graphics, mystical elements, treasure chests, rupees, and adventure game aesthetic" },
            { "Pokémon World", "Transform to Pokémon world with vibrant colors, anime style, pokéballs, tall grass, and cheerful Nintendo RPG atmosphere" },
            { "Sonic the Hedgehog", "Transform to Sonic world with loop-de-loops, checkered patterns, rings floating, bright colors, and fast-paced platformer aesthetic" },
            { "Pac-Man Arcade", "Transform to retro Pac-Man arcade style with simple geometric shapes, maze walls, dots, bright primary colors, and 80s arcade look" },
            { "Tetris Blocks", "Transform to Tetris world with everything made of falling tetromino blocks, primary colors, grid patterns, and puzzle game aesthetic" },
            
            // RPG & Fantasy Games
            { "World of Warcraft", "Transform to World of Warcraft fantasy MMO style with exaggerated proportions, vibrant colors, fantasy architecture, and Blizzard art style" },
            { "Skyrim", "Transform to Elder Scrolls Skyrim with Nordic architecture, medieval fantasy, dragons, stone buildings, and epic RPG atmosphere" },
            { "The Witcher 3", "Transform to Witcher 3 dark fantasy world with realistic medieval setting, monster hunting atmosphere, and gritty fantasy aesthetic" },
            { "Dark Souls", "Transform to Dark Souls gothic fantasy with dark atmosphere, medieval ruins, ominous lighting, souls-like aesthetic, and challenging environment" },
            { "Final Fantasy", "Transform to Final Fantasy JRPG style with anime-inspired characters, crystal formations, magical energy, and square enix aesthetic" },
            { "Dragon Age", "Transform to Dragon Age fantasy world with medieval castles, magic effects, heroic atmosphere, and BioWare RPG style" },
            
            // Shooter & Action Games
            { "Call of Duty", "Transform to Call of Duty modern warfare setting with military equipment, realistic textures, war-torn environment, and tactical shooter aesthetic" },
            { "Halo", "Transform to Halo sci-fi world with alien architecture, UNSC technology, energy shields, futuristic military, and bungie art style" },
            { "Doom", "Transform to DOOM hell-scape with demonic elements, industrial metal, red and orange tones, brutal atmosphere, and id software aesthetic" },
            { "Fortnite", "Transform to Fortnite cartoon battle royale style with vibrant colors, exaggerated proportions, cartoon shading, and Epic Games aesthetic" },
            { "Overwatch", "Transform to Overwatch bright hero shooter style with colorful environments, futuristic tech, playful atmosphere, and Blizzard character design" },
            { "Borderlands", "Transform to Borderlands cel-shaded comic book style with thick outlines, vibrant colors, comic aesthetic, and gearbox art direction" },
            { "Apex Legends", "Transform to Apex Legends sci-fi battle royale with futuristic buildings, neon accents, sleek design, and Respawn aesthetic" },
            
            // Open World & Adventure
            { "Grand Theft Auto V", "Transform to GTA V realistic urban environment with modern city, cars, graffiti, urban atmosphere, and Rockstar realism" },
            { "Red Dead Redemption", "Transform to Red Dead Redemption Wild West with dusty towns, wooden buildings, desert landscape, and western frontier atmosphere" },
            { "Assassin's Creed", "Transform to Assassin's Creed historical setting with parkour architecture, viewpoints, historical accuracy, and Ubisoft period detail" },
            { "The Last of Us", "Transform to Last of Us post-apocalyptic world with overgrown vegetation, abandoned buildings, nature reclaiming, and decay atmosphere" },
            { "Fallout", "Transform to Fallout retro-futuristic post-apocalypse with 1950s aesthetic, nuclear wasteland, rust, and Bethesda wasteland style" },
            { "Cyberpunk 2077", "Transform to Cyberpunk 2077 with neon-lit megacity, futuristic tech, holographic ads, rain-slicked streets, and CD Projekt cyberpunk aesthetic" },
            { "Spider-Man PS4", "Transform to Spider-Man PS4 stylized New York with superhero aesthetic, web-slinging architecture, comic elements, and Insomniac style" },
            
            // Horror & Survival
            { "Resident Evil", "Transform to Resident Evil survival horror with dark corridors, zombies lurking, eerie atmosphere, gore, and Capcom horror aesthetic" },
            { "Silent Hill", "Transform to Silent Hill psychological horror with fog, rust, decay, otherworld transformation, and Konami nightmare aesthetic" },
            { "Dead Space", "Transform to Dead Space sci-fi horror with dark spaceship corridors, necromorphs, industrial design, and visceral horror atmosphere" },
            { "Bloodborne", "Transform to Bloodborne Victorian gothic horror with dark architecture, eldritch atmosphere, gothic design, and FromSoftware aesthetic" },
            
            // Stylized & Artistic
            { "Cel-Shaded Anime", "Transform to cel-shaded anime game style with bold outlines, flat colors, anime aesthetic, and Japanese animation look" },
            { "Cartoon Network Style", "Transform to Cartoon Network animated style with thick outlines, simple shapes, vibrant colors, and western cartoon aesthetic" },
            { "Studio Ghibli Game", "Transform to Studio Ghibli inspired game with watercolor aesthetic, whimsical atmosphere, hand-drawn feel, and Miyazaki magic" },
            { "Paper Mario", "Transform to Paper Mario papercraft style with flat 2D characters, paper textures, pop-up book aesthetic, and Nintendo charm" },
            { "Cuphead", "Transform to Cuphead 1930s cartoon style with rubber hose animation, grainy film, watercolor backgrounds, and vintage cartoon look" },
            { "Hollow Knight", "Transform to Hollow Knight hand-drawn bug world with gothic atmosphere, dark caverns, intricate details, and Team Cherry art style" },
            { "Ori and the Blind Forest", "Transform to Ori style with beautiful hand-painted forest, glowing elements, ethereal atmosphere, and Moon Studios artistry" },
            { "Journey", "Transform to Journey minimalist desert with flowing sand, ancient ruins, minimalist beauty, and thatgamecompany artistic vision" },
            
            // Strategy & Simulation
            { "The Sims", "Transform to The Sims life simulation with plumbobs, bright colors, domestic setting, playful interface, and Maxis style" },
            { "SimCity", "Transform to SimCity urban planning view with grid layout, zoning colors, city infrastructure, and city-building aesthetic" },
            { "Civilization VI", "Transform to Civilization VI stylized historical with exaggerated architecture, strategic map view, and Firaxis art style" },
            { "Stardew Valley", "Transform to Stardew Valley pixel art farming sim with 16-bit style, charming pixels, rural setting, and ConcernedApe aesthetic" },
            { "Animal Crossing", "Transform to Animal Crossing cute island life with pastel colors, anthropomorphic animals, cozy atmosphere, and Nintendo charm" },
            
            // Racing & Sports
            { "Mario Kart", "Transform to Mario Kart racing world with colorful tracks, item boxes, cartoon vehicles, and Nintendo kart racing fun" },
            { "Gran Turismo", "Transform to Gran Turismo realistic racing simulation with photorealistic cars, racing circuits, and Polyphony Digital realism" },
            { "Rocket League", "Transform to Rocket League arena with rocket-powered cars, soccer elements, boost trails, and Psyonix competitive aesthetic" },
            
            // Indie & Unique
            { "Among Us", "Transform to Among Us space station with simple cartoon crewmates, colorful rooms, task locations, and InnerSloth social deduction style" },
            { "Undertale", "Transform to Undertale retro RPG with 8-bit inspired graphics, quirky atmosphere, bullet patterns, and Toby Fox charm" },
            { "Terraria", "Transform to Terraria 2D sandbox with pixel art, block-based building, underground caves, and Re-Logic sprite work" },
            { "Subnautica", "Transform to Subnautica alien ocean with bioluminescent underwater world, sea creatures, deep blue, and Unknown Worlds aquatic beauty" },
        };

        private void Awake()
        {
            menuManager = FindFirstObjectByType<MenuManager>();
            
            if (webRTCConnection == null)
            {
                webRTCConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            if (featurePanel != null)
                featurePanel.SetActive(false);
        }

        public void Activate()
        {
            isActive = true;
            
            if (featurePanel != null)
                featurePanel.SetActive(true);

            if (titleText != null)
                titleText.text = "Video Game Style";

            if (instructionsText != null)
                instructionsText.text = "Joystick Up/Down: Navigate | Right Trigger: Apply | Left Trigger: Back";

            InitializeStyleList();
            UpdateSelection();
        }

        public void Deactivate()
        {
            isActive = false;
            
            if (featurePanel != null)
                featurePanel.SetActive(false);

            ClearStyleList();
        }

        private void InitializeStyleList()
        {
            ClearStyleList();

            foreach (var option in gameStyleOptions)
            {
                GameObject itemObj = Instantiate(listItemPrefab, styleListContainer);
                TMP_Text itemText = itemObj.GetComponentInChildren<TMP_Text>();
                Image itemBg = itemObj.GetComponent<Image>();

                if (itemText != null)
                    itemText.text = option.Key;

                GameStyleItem item = new GameStyleItem
                {
                    name = option.Key,
                    prompt = option.Value,
                    uiObject = itemObj,
                    text = itemText,
                    background = itemBg
                };

                styleItems.Add(item);
            }

            currentIndex = 0;
        }

        private void ClearStyleList()
        {
            foreach (var item in styleItems)
            {
                if (item.uiObject != null)
                    Destroy(item.uiObject);
            }
            styleItems.Clear();
        }

        private void Update()
        {
            if (!isActive)
                return;

            HandleNavigation();
            HandleSelection();
            HandleBack();
        }

        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown)
                return;

            Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            if (joystick.y > 0.5f)
            {
                currentIndex--;
                if (currentIndex < 0)
                    currentIndex = styleItems.Count - 1;
                
                lastNavigationTime = Time.time;
                UpdateSelection();
            }
            else if (joystick.y < -0.5f)
            {
                currentIndex++;
                if (currentIndex >= styleItems.Count)
                    currentIndex = 0;
                
                lastNavigationTime = Time.time;
                UpdateSelection();
            }
        }

        private void HandleSelection()
        {
            // Right trigger to apply game style
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                if (currentIndex >= 0 && currentIndex < styleItems.Count)
                {
                    ApplyGameStyle(styleItems[currentIndex]);
                }
            }
        }

        private void HandleBack()
        {
            // Left trigger to go back
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (menuManager != null)
                    menuManager.ReturnToMainMenu();
            }
        }

        private void UpdateSelection()
        {
            for (int i = 0; i < styleItems.Count; i++)
            {
                if (styleItems[i].background != null)
                {
                    styleItems[i].background.color = (i == currentIndex) ? selectedColor : normalColor;
                }
                
                if (styleItems[i].text != null)
                {
                    styleItems[i].text.fontStyle = (i == currentIndex) ? FontStyles.Bold : FontStyles.Normal;
                }
            }

            if (currentStyleText != null && currentIndex >= 0 && currentIndex < styleItems.Count)
            {
                currentStyleText.text = $"Selected: {styleItems[currentIndex].name}";
            }
        }

        private void ApplyGameStyle(GameStyleItem item)
        {
            if (item == null || webRTCConnection == null)
                return;

            Debug.Log($"Video Game Style: Applying {item.name} with prompt: {item.prompt}");
            webRTCConnection.SendCustomPrompt(item.prompt);

            if (currentStyleText != null)
            {
                currentStyleText.text = $"Current Style: {item.name} ✓";
            }
        }
    }
}

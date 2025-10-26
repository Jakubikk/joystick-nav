using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Video Game World feature controller.
    /// Transforms the environment to look like various video game worlds.
    /// Uses Mirage model for complete world transformations with game aesthetics.
    /// </summary>
    public class VideoGameWorldController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject videoGameUI;
        [SerializeField] private Transform gameWorldOptionsContainer;
        [SerializeField] private GameObject gameWorldOptionPrefab;
        [SerializeField] private TMP_Text selectedGameWorldText;
        [SerializeField] private TMP_Text instructionsText;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        private bool isActive = false;
        private int currentGameWorldIndex = 0;
        private List<GameWorldOption> gameWorldOptions = new List<GameWorldOption>();
        private float inputCooldown = 0f;
        private const float INPUT_DELAY = 0.2f;

        private class GameWorldOption
        {
            public string name;
            public string description;
            public string aiPrompt;
            public GameObject uiElement;
        }

        private void Awake()
        {
            if (videoGameUI != null)
                videoGameUI.SetActive(false);

            InitializeGameWorldOptions();
        }

        private void InitializeGameWorldOptions()
        {
            gameWorldOptions.Clear();

            // Classic & Retro Games
            AddGameWorldOption("Minecraft", 
                "Blocky voxel world with cubic blocks",
                "Transform into Minecraft world with blocky cubic structures, pixelated textures, square trees, grass blocks, crafted materials, low-poly voxel aesthetic, game-like rendering");

            AddGameWorldOption("Super Mario Bros", 
                "Colorful platformer with power-ups and pipes",
                "Transform into Super Mario world with bright vibrant colors, green pipes, question mark blocks, brick platforms, coins floating, mushroom power-ups, classic Nintendo platformer aesthetic");

            AddGameWorldOption("The Legend of Zelda", 
                "Fantasy adventure with Hyrulean aesthetic",
                "Transform into Legend of Zelda world with fantasy medieval setting, Hyrulean architecture, triforce symbols, treasure chests, mystical atmosphere, cel-shaded art style, adventure game aesthetic");

            AddGameWorldOption("Pokémon World", 
                "Colorful world with Pokémon creatures",
                "Transform into Pokémon world with vibrant anime-style graphics, Pokémon creatures visible, Poké Balls, tall grass, colorful friendly aesthetic, Japanese RPG art style");

            AddGameWorldOption("Sonic the Hedgehog", 
                "Fast-paced world with loops and rings",
                "Transform into Sonic world with checkered patterns, golden rings floating, loop-de-loops, bright vibrant colors, SEGA Genesis aesthetic, speed lines, retro 90s game style");

            // Open World Games
            AddGameWorldOption("Grand Theft Auto V", 
                "Urban Los Santos city environment",
                "Transform into GTA V world with Los Angeles-inspired city, modern urban environment, realistic graphics, street life, traffic, detailed city buildings, crime drama aesthetic");

            AddGameWorldOption("Red Dead Redemption", 
                "Wild West frontier setting",
                "Transform into Red Dead world with American Old West setting, frontier towns, dusty roads, saloons, horses, cowboy era, cinematic western aesthetic, 1899 atmosphere");

            AddGameWorldOption("Skyrim (Elder Scrolls)", 
                "Nordic fantasy with dragons and magic",
                "Transform into Skyrim world with Nordic fantasy architecture, snowy mountains, stone buildings, medieval fantasy setting, dragon motifs, epic fantasy RPG aesthetic, mystical atmosphere");

            AddGameWorldOption("The Witcher", 
                "Dark fantasy medieval world",
                "Transform into Witcher world with dark medieval fantasy, Eastern European architecture, monster hunting atmosphere, magical elements, gritty realistic fantasy, Slavic aesthetic");

            AddGameWorldOption("Assassin's Creed", 
                "Historical cities with parkour elements",
                "Transform into Assassin's Creed world with historical architecture, parkour-able buildings, hidden blade aesthetic, cinematic action style, period-accurate details, stealth atmosphere");

            // Sci-Fi Games
            AddGameWorldOption("Halo", 
                "Futuristic military sci-fi universe",
                "Transform into Halo world with futuristic UNSC military technology, alien Covenant architecture, energy shields, sci-fi weapons, space marine aesthetic, epic sci-fi warfare atmosphere");

            AddGameWorldOption("Mass Effect", 
                "Space opera with alien civilizations",
                "Transform into Mass Effect world with advanced alien technology, sleek futuristic architecture, holographic displays, space station aesthetic, galactic civilization style, sci-fi opera atmosphere");

            AddGameWorldOption("Cyberpunk 2077", 
                "Dystopian futuristic Night City",
                "Transform into Cyberpunk 2077 world with neon-lit megacity, holographic advertisements, cybernetic enhancements visible, futuristic cars, dystopian atmosphere, high-tech low-life aesthetic");

            AddGameWorldOption("Portal", 
                "Minimalist test chamber environment",
                "Transform into Portal world with clean white test chambers, orange and blue portals, Aperture Science aesthetic, minimalist sci-fi design, testing facility atmosphere, GLaDOS style");

            AddGameWorldOption("Destiny", 
                "Sci-fi fantasy with mystical technology",
                "Transform into Destiny world with golden age technology ruins, futuristic guardians, space magic aesthetic, alien architecture, sci-fi fantasy blend, epic space opera atmosphere");

            // Fantasy & RPG
            AddGameWorldOption("World of Warcraft", 
                "High fantasy MMORPG world",
                "Transform into World of Warcraft with vibrant high fantasy aesthetic, Azeroth architecture, magical elements, fantasy races, stylized graphics, MMORPG atmosphere, epic fantasy world");

            AddGameWorldOption("Final Fantasy", 
                "Japanese RPG fantasy aesthetics",
                "Transform into Final Fantasy world with Japanese RPG art style, fantasy-tech blend, crystals and magic, ornate architecture, beautiful anime-inspired graphics, Square Enix aesthetic");

            AddGameWorldOption("Dark Souls", 
                "Gothic dark fantasy ruins",
                "Transform into Dark Souls world with gothic medieval ruins, dark fantasy atmosphere, crumbling stone architecture, ominous lighting, challenging atmosphere, FromSoftware aesthetic");

            // Horror Games
            AddGameWorldOption("Resident Evil", 
                "Survival horror mansion atmosphere",
                "Transform into Resident Evil world with survival horror atmosphere, abandoned mansion aesthetic, dim lighting, creepy environment, horror game tension, Umbrella Corporation style");

            AddGameWorldOption("Silent Hill", 
                "Psychological horror fog world",
                "Transform into Silent Hill world with thick fog everywhere, rusted industrial aesthetic, psychological horror atmosphere, otherworld dimension, eerie lighting, disturbing environment");

            // Stylized & Artistic
            AddGameWorldOption("Borderlands", 
                "Cel-shaded comic book style",
                "Transform into Borderlands world with thick black outlines, cel-shaded graphics, comic book aesthetic, vibrant colors, hand-drawn style, Pandora wasteland atmosphere, stylized violence");

            AddGameWorldOption("Fortnite", 
                "Colorful battle royale island",
                "Transform into Fortnite world with vibrant cartoon graphics, buildable structures, colorful environment, playful aesthetic, battle royale island setting, Epic Games art style");

            AddGameWorldOption("Overwatch", 
                "Futuristic hero-shooter world",
                "Transform into Overwatch world with stylized near-future aesthetic, heroic architecture, vibrant colors, Blizzard art style, optimistic future setting, character-focused design");

            AddGameWorldOption("Team Fortress 2", 
                "Stylized 1960s industrial aesthetic",
                "Transform into Team Fortress 2 world with stylized 1960s design, industrial facilities, cartoonish proportions, Valve Source engine aesthetic, humorous atmosphere, distinct art style");

            // Survival & Crafting
            AddGameWorldOption("Subnautica", 
                "Underwater alien ocean world",
                "Transform into Subnautica world with alien underwater environment, bioluminescent creatures, coral reefs, deep ocean atmosphere, survival aesthetic, beautiful aquatic alien planet");

            AddGameWorldOption("Terraria", 
                "2D pixelated sandbox adventure",
                "Transform into Terraria world with 2D pixel art style, side-scrolling perspective, blocky terrain, pixelated graphics, retro game aesthetic, sandbox adventure atmosphere");

            AddGameWorldOption("Don't Starve", 
                "Tim Burton-esque survival world",
                "Transform into Don't Starve world with hand-drawn Tim Burton art style, gothic aesthetics, dark whimsical atmosphere, sketchy lines, survival horror cartoon style");
        }

        private void AddGameWorldOption(string name, string description, string aiPrompt)
        {
            var option = new GameWorldOption
            {
                name = name,
                description = description,
                aiPrompt = aiPrompt
            };

            if (gameWorldOptionPrefab != null && gameWorldOptionsContainer != null)
            {
                option.uiElement = Instantiate(gameWorldOptionPrefab, gameWorldOptionsContainer);
                var text = option.uiElement.GetComponentInChildren<TMP_Text>();
                if (text != null)
                {
                    text.text = name;
                }
            }

            gameWorldOptions.Add(option);
        }

        private void Update()
        {
            if (!isActive) return;

            if (inputCooldown > 0)
            {
                inputCooldown -= Time.deltaTime;
                return;
            }

            HandleNavigation();
            HandleSelection();
        }

        private void HandleNavigation()
        {
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            // Navigate up
            if (joystickInput.y > 0.7f)
            {
                currentGameWorldIndex--;
                if (currentGameWorldIndex < 0) currentGameWorldIndex = gameWorldOptions.Count - 1;
                UpdateGameWorldDisplay();
                inputCooldown = INPUT_DELAY;
            }
            // Navigate down
            else if (joystickInput.y < -0.7f)
            {
                currentGameWorldIndex++;
                if (currentGameWorldIndex >= gameWorldOptions.Count) currentGameWorldIndex = 0;
                UpdateGameWorldDisplay();
                inputCooldown = INPUT_DELAY;
            }
        }

        private void HandleSelection()
        {
            // Right trigger to apply game world transformation
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                ApplyGameWorldTransformation();
                inputCooldown = INPUT_DELAY;
            }
        }

        private void UpdateGameWorldDisplay()
        {
            if (gameWorldOptions.Count == 0) return;

            // Update selection highlighting
            for (int i = 0; i < gameWorldOptions.Count; i++)
            {
                if (gameWorldOptions[i].uiElement != null)
                {
                    var text = gameWorldOptions[i].uiElement.GetComponentInChildren<TMP_Text>();
                    if (text != null)
                    {
                        text.color = (i == currentGameWorldIndex) ? Color.yellow : Color.white;
                        text.fontStyle = (i == currentGameWorldIndex) ? FontStyles.Bold : FontStyles.Normal;
                    }
                }
            }

            // Update selected game world text
            if (currentGameWorldIndex < gameWorldOptions.Count)
            {
                if (selectedGameWorldText != null)
                {
                    selectedGameWorldText.text = $"<b>{gameWorldOptions[currentGameWorldIndex].name}</b>\n{gameWorldOptions[currentGameWorldIndex].description}";
                }
            }
        }

        private void ApplyGameWorldTransformation()
        {
            if (currentGameWorldIndex >= gameWorldOptions.Count) return;

            var selectedGameWorld = gameWorldOptions[currentGameWorldIndex];
            
            if (webRTCConnection != null)
            {
                webRTCConnection.SendCustomPrompt(selectedGameWorld.aiPrompt);
                Debug.Log($"VideoGameWorldController: Applied {selectedGameWorld.name} transformation");
            }
            else
            {
                Debug.LogWarning("VideoGameWorldController: WebRTC connection not set");
            }
        }

        public void Activate()
        {
            isActive = true;
            if (videoGameUI != null)
                videoGameUI.SetActive(true);

            UpdateGameWorldDisplay();

            if (instructionsText != null)
            {
                instructionsText.text = "Joystick Up/Down: Browse game worlds\nRight Trigger: Transform to game world";
            }

            Debug.Log("VideoGameWorldController: Activated");
        }

        public void Deactivate()
        {
            isActive = false;
            if (videoGameUI != null)
                videoGameUI.SetActive(false);
            
            Debug.Log("VideoGameWorldController: Deactivated");
        }
    }
}

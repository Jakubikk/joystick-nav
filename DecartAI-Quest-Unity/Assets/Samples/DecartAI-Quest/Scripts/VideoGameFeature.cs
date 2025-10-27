using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Controls the Video Game Style feature allowing users to view their environment as if it was in various video games.
    /// Uses Decart Mirage model for game-style transformation.
    /// </summary>
    public class VideoGameFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Transform gameListContainer;
        [SerializeField] private GameObject gameItemPrefab;
        [SerializeField] private TMP_Text descriptionText;
        
        [Header("WebRTC Integration")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Navigation Settings")]
        [SerializeField] private float navigationCooldown = 0.25f;
        
        private List<GameStyleData> gameStyles;
        private List<GameObject> gameUIItems;
        private int selectedIndex = 0;
        private float lastNavigationTime = 0f;
        
        [Serializable]
        private class GameStyleData
        {
            public string gameName;
            public string genre;
            public string description;
            public string decartPrompt;
        }
        
        private void Start()
        {
            InitializeGameStyles();
            BuildGameUI();
        }
        
        private void InitializeGameStyles()
        {
            gameStyles = new List<GameStyleData>
            {
                // Block/Voxel Games
                new GameStyleData { gameName = "Minecraft", genre = "Sandbox", 
                    description = "Blocky voxel world with pixelated textures",
                    decartPrompt = "Transform the environment into Minecraft style with blocky voxel shapes, pixelated textures, cube-based structures, grass blocks, oak wood planks, torch lighting, and iconic Minecraft aesthetic" },
                new GameStyleData { gameName = "LEGO World", genre = "Sandbox", 
                    description = "Everything made of colorful LEGO bricks",
                    decartPrompt = "Transform the environment into a LEGO world where everything is made of colorful LEGO bricks with plastic sheen, studded surfaces, bright primary colors, and playful toy-like aesthetic" },
                new GameStyleData { gameName = "Roblox", genre = "Sandbox", 
                    description = "Simple blocky characters and colorful world",
                    decartPrompt = "Transform the environment into Roblox style with simple geometric shapes, bright saturated colors, smooth plastic materials, friendly blocky aesthetic, and cartoonish design" },
                
                // RPG/Fantasy Games
                new GameStyleData { gameName = "Skyrim", genre = "RPG", 
                    description = "Medieval fantasy with Nordic influence",
                    decartPrompt = "Transform the environment into Skyrim style with Nordic medieval architecture, stone walls, wooden beams, torch-lit dungeons, fantasy atmosphere, rugged textures, and Elder Scrolls aesthetic" },
                new GameStyleData { gameName = "The Witcher", genre = "RPG", 
                    description = "Dark fantasy medieval world",
                    decartPrompt = "Transform the environment into The Witcher style with dark fantasy medieval architecture, weathered wood, stone castles, mystical atmosphere, European folklore elements, and gritty realistic fantasy" },
                new GameStyleData { gameName = "World of Warcraft", genre = "MMORPG", 
                    description = "Stylized fantasy with vibrant colors",
                    decartPrompt = "Transform the environment into World of Warcraft style with stylized fantasy architecture, vibrant saturated colors, exaggerated proportions, magical elements, and iconic WoW aesthetic" },
                new GameStyleData { gameName = "Final Fantasy", genre = "JRPG", 
                    description = "Blend of fantasy and advanced technology",
                    decartPrompt = "Transform the environment into Final Fantasy style with mix of fantasy and futuristic elements, crystalline structures, magical lighting, ornate Japanese-inspired design, and epic JRPG atmosphere" },
                
                // Shooter Games
                new GameStyleData { gameName = "Call of Duty", genre = "Shooter", 
                    description = "Modern military realistic environment",
                    decartPrompt = "Transform the environment into Call of Duty style with modern military architecture, tactical lighting, realistic urban warfare setting, gritty textures, and contemporary combat atmosphere" },
                new GameStyleData { gameName = "Borderlands", genre = "Shooter", 
                    description = "Cell-shaded comic book style",
                    decartPrompt = "Transform the environment into Borderlands style with thick black outlines, cel-shaded rendering, exaggerated proportions, vibrant colors, comic book aesthetic, and cartoonish violence vibe" },
                new GameStyleData { gameName = "Halo", genre = "Sci-Fi Shooter", 
                    description = "Futuristic alien architecture and tech",
                    decartPrompt = "Transform the environment into Halo style with futuristic UNSC military bases, Forerunner alien architecture, holographic displays, sci-fi lighting, metallic surfaces, and iconic Halo aesthetic" },
                
                // Adventure Games  
                new GameStyleData { gameName = "Legend of Zelda", genre = "Adventure", 
                    description = "Colorful fantasy adventure world",
                    decartPrompt = "Transform the environment into Legend of Zelda style with colorful cel-shaded graphics, whimsical fantasy elements, ancient temples, magical atmosphere, vibrant nature, and Nintendo charm" },
                new GameStyleData { gameName = "Uncharted", genre = "Adventure", 
                    description = "Realistic adventure with ancient ruins",
                    decartPrompt = "Transform the environment into Uncharted style with photorealistic ancient ruins, jungle overgrowth, weathered stone, adventure movie aesthetic, dramatic lighting, and treasure hunter atmosphere" },
                new GameStyleData { gameName = "Tomb Raider", genre = "Adventure", 
                    description = "Archaeological exploration sites",
                    decartPrompt = "Transform the environment into Tomb Raider style with ancient archaeological sites, mysterious ruins, puzzle elements, atmospheric lighting, exploration atmosphere, and adventure game aesthetic" },
                
                // Horror Games
                new GameStyleData { gameName = "Resident Evil", genre = "Horror", 
                    description = "Survival horror with dark atmosphere",
                    decartPrompt = "Transform the environment into Resident Evil style with dark moody lighting, abandoned building atmosphere, horror elements, survival aesthetic, grimy textures, and unsettling zombie apocalypse vibe" },
                new GameStyleData { gameName = "Silent Hill", genre = "Horror", 
                    description = "Foggy psychological horror setting",
                    decartPrompt = "Transform the environment into Silent Hill style with dense fog, rusty metal textures, disturbing atmosphere, industrial decay, psychological horror elements, and nightmarish aesthetic" },
                new GameStyleData { gameName = "Dead Space", genre = "Sci-Fi Horror", 
                    description = "Futuristic space horror",
                    decartPrompt = "Transform the environment into Dead Space style with dark sci-fi corridors, industrial space station, flickering lights, claustrophobic atmosphere, futuristic horror, and alien threat presence" },
                
                // Retro/Classic Games
                new GameStyleData { gameName = "Super Mario", genre = "Platformer", 
                    description = "Colorful cartoon mushroom kingdom",
                    decartPrompt = "Transform the environment into Super Mario style with bright primary colors, cartoon aesthetics, mushroom kingdom elements, question mark blocks, pipes, coins, and playful Nintendo design" },
                new GameStyleData { gameName = "Sonic", genre = "Platformer", 
                    description = "Fast-paced colorful zones",
                    decartPrompt = "Transform the environment into Sonic the Hedgehog style with vibrant checkered patterns, loop-de-loops, springs, bright zone aesthetics, speed boost elements, and classic SEGA design" },
                new GameStyleData { gameName = "Pac-Man", genre = "Arcade", 
                    description = "Retro arcade with neon mazes",
                    decartPrompt = "Transform the environment into Pac-Man style with glowing neon mazes, dark backgrounds, bright dots, retro arcade aesthetic, simple geometric shapes, and classic 80s game design" },
                new GameStyleData { gameName = "8-Bit Retro", genre = "Retro", 
                    description = "Classic NES pixel art style",
                    decartPrompt = "Transform the environment into 8-bit retro style with pixelated graphics, limited color palette, NES-era aesthetics, simple geometric shapes, nostalgic 1980s video game look" },
                
                // Open World Games
                new GameStyleData { gameName = "GTA", genre = "Open World", 
                    description = "Modern urban city environment",
                    decartPrompt = "Transform the environment into Grand Theft Auto style with urban city atmosphere, realistic modern architecture, street elements, contemporary vehicles visible, and crime drama aesthetic" },
                new GameStyleData { gameName = "Red Dead Redemption", genre = "Western", 
                    description = "Wild West frontier setting",
                    decartPrompt = "Transform the environment into Red Dead Redemption style with Wild West frontier town, wooden saloons, dusty atmosphere, western aesthetic, rustic materials, and cowboy era design" },
                new GameStyleData { gameName = "Cyberpunk 2077", genre = "RPG", 
                    description = "Neon dystopian future city",
                    decartPrompt = "Transform the environment into Cyberpunk 2077 style with neon lights, holographic advertisements, dystopian future city, high-tech low-life aesthetic, rain-soaked streets, and cyberpunk atmosphere" },
                
                // Puzzle/Artistic Games
                new GameStyleData { gameName = "Portal", genre = "Puzzle", 
                    description = "Sterile test chambers with portals",
                    decartPrompt = "Transform the environment into Portal style with white sterile Aperture Science test chambers, clean panels, blue and orange portal indicators, minimalist design, and clinical sci-fi aesthetic" },
                new GameStyleData { gameName = "Journey", genre = "Art", 
                    description = "Artistic desert with flowing sand",
                    decartPrompt = "Transform the environment into Journey style with flowing sand dunes, ancient ruins, golden desert atmosphere, minimalist artistic design, warm sunset colors, and meditative peaceful aesthetic" },
                new GameStyleData { gameName = "Gris", genre = "Art", 
                    description = "Watercolor artistic platformer",
                    decartPrompt = "Transform the environment into Gris style with watercolor painted aesthetics, flowing artistic design, emotional color palette, abstract platforms, beautiful hand-drawn look, and contemplative atmosphere" },
                
                // Fighting Games
                new GameStyleData { gameName = "Street Fighter", genre = "Fighting", 
                    description = "Vibrant 2D fighting game stages",
                    decartPrompt = "Transform the environment into Street Fighter style with vibrant 2D stage backgrounds, dynamic action atmosphere, international locations, arcade fighting game aesthetic, and energetic combat arena" },
                new GameStyleData { gameName = "Mortal Kombat", genre = "Fighting", 
                    description = "Dark martial arts tournament arenas",
                    decartPrompt = "Transform the environment into Mortal Kombat style with dark tournament arenas, mystical elements, martial arts atmosphere, gothic architecture, dramatic lighting, and deadly combat setting" },
                
                // Strategy Games
                new GameStyleData { gameName = "Civilization", genre = "Strategy", 
                    description = "Historical civilization overview",
                    decartPrompt = "Transform the environment into Civilization style with strategic map view aesthetics, hexagonal tiles, historical architecture elements, empire building atmosphere, and turn-based strategy game look" },
                new GameStyleData { gameName = "StarCraft", genre = "RTS", 
                    description = "Sci-fi real-time strategy battles",
                    decartPrompt = "Transform the environment into StarCraft style with futuristic sci-fi military bases, alien Zerg structures, Protoss crystalline architecture, RTS battlefield aesthetic, and space warfare atmosphere" }
            };
        }
        
        private void BuildGameUI()
        {
            gameUIItems = new List<GameObject>();
            
            if (descriptionText != null)
            {
                descriptionText.text = "Select a video game style to transform your environment";
            }
            
            foreach (var game in gameStyles)
            {
                GameObject uiItem = CreateGameUIItem(game);
                gameUIItems.Add(uiItem);
            }
            
            UpdateSelection();
        }
        
        private GameObject CreateGameUIItem(GameStyleData game)
        {
            GameObject itemObj = new GameObject($"Game_{game.gameName}");
            itemObj.transform.SetParent(gameListContainer, false);
            
            RectTransform rect = itemObj.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(700, 70);
            
            TMP_Text text = itemObj.AddComponent<TextMeshProUGUI>();
            text.text = $"[{game.genre}] {game.gameName}";
            text.fontSize = 32;
            text.color = Color.white;
            text.alignment = TextAlignmentOptions.Left;
            
            return itemObj;
        }
        
        private void Update()
        {
            if (!gameObject.activeSelf) return;
            
            HandleNavigation();
            HandleSelection();
            UpdateDescription();
        }
        
        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown) return;
            
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            if (joystickInput.y > 0.5f)
            {
                NavigateUp();
                lastNavigationTime = Time.time;
            }
            else if (joystickInput.y < -0.5f)
            {
                NavigateDown();
                lastNavigationTime = Time.time;
            }
        }
        
        private void HandleSelection()
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyGameStyle();
            }
        }
        
        private void NavigateUp()
        {
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = gameStyles.Count - 1;
            }
            UpdateSelection();
        }
        
        private void NavigateDown()
        {
            selectedIndex++;
            if (selectedIndex >= gameStyles.Count)
            {
                selectedIndex = 0;
            }
            UpdateSelection();
        }
        
        private void UpdateSelection()
        {
            for (int i = 0; i < gameUIItems.Count; i++)
            {
                TMP_Text text = gameUIItems[i].GetComponent<TextMeshProUGUI>();
                if (text != null)
                {
                    text.color = (i == selectedIndex) ? Color.magenta : Color.white;
                    text.fontSize = (i == selectedIndex) ? 36 : 32;
                }
            }
        }
        
        private void UpdateDescription()
        {
            if (descriptionText != null && selectedIndex >= 0 && selectedIndex < gameStyles.Count)
            {
                GameStyleData game = gameStyles[selectedIndex];
                descriptionText.text = $"{game.gameName}\n{game.description}";
            }
        }
        
        private void ApplyGameStyle()
        {
            if (selectedIndex >= 0 && selectedIndex < gameStyles.Count)
            {
                GameStyleData game = gameStyles[selectedIndex];
                
                if (webRtcConnection != null)
                {
                    Debug.Log($"Applying game style: {game.gameName}");
                    webRtcConnection.SendCustomPrompt(game.decartPrompt);
                }
                else
                {
                    Debug.LogWarning("WebRTC connection not available");
                }
            }
        }
        
        private void OnEnable()
        {
            Debug.Log("Video Game Style feature activated");
            selectedIndex = 0;
            if (gameUIItems != null && gameUIItems.Count > 0)
            {
                UpdateSelection();
            }
        }
    }
}

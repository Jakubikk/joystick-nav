using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Video Game feature - transforms environment to look like popular video games
    /// Uses Decart Mirage model for game-style transformations
    /// </summary>
    public class VideoGameController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private Transform gameListContainer;
        [SerializeField] private GameObject gameItemPrefab;
        [SerializeField] private TMP_Text categoryText;
        [SerializeField] private TMP_Text instructionText;
        
        private WebRTCConnection webRtcConnection;
        private int selectedIndex = 0;
        private List<GameStyleOption> gameOptions = new List<GameStyleOption>();
        
        private class GameStyleOption
        {
            public string name;
            public string description;
            public string prompt;
            public GameObject gameObject;
        }
        
        private void Awake()
        {
            InitializeGameOptions();
        }
        
        private void InitializeGameOptions()
        {
            gameOptions.Clear();
            
            // Blocky/Voxel Games
            gameOptions.Add(new GameStyleOption
            {
                name = "Minecraft",
                description = "Blocky voxel world",
                prompt = "Transform to Minecraft style, blocky voxel graphics, pixelated textures, cubic blocks, Minecraft aesthetic, low poly cubic world, pixelated block textures"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "LEGO World",
                description = "Everything made of LEGO bricks",
                prompt = "Transform to LEGO brick world, everything made of colorful plastic LEGO bricks, toy-like appearance, glossy plastic texture, LEGO minifigure aesthetic"
            });
            
            // RPG Games
            gameOptions.Add(new GameStyleOption
            {
                name = "The Legend of Zelda: BOTW",
                description = "Cel-shaded fantasy adventure",
                prompt = "Transform to Zelda Breath of the Wild style, cel-shaded graphics, vibrant colors, fantasy landscape, painterly aesthetic, animated game art style"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "The Witcher 3",
                description = "Dark fantasy medieval world",
                prompt = "Transform to Witcher 3 style, dark fantasy medieval aesthetic, realistic fantasy graphics, moody atmosphere, detailed fantasy world, European medieval setting"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Skyrim",
                description = "Epic Nordic fantasy",
                prompt = "Transform to Skyrim style, Nordic fantasy aesthetic, mountainous landscape, medieval fantasy architecture, Elder Scrolls visual style, epic fantasy atmosphere"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Final Fantasy",
                description = "Japanese RPG aesthetics",
                prompt = "Transform to Final Fantasy style, JRPG aesthetics, fantasy crystals, detailed fantasy architecture, magical atmosphere, Square Enix art style"
            });
            
            // Action/Adventure Games
            gameOptions.Add(new GameStyleOption
            {
                name = "Grand Theft Auto V",
                description = "Realistic modern cityscape",
                prompt = "Transform to GTA V style, realistic modern city graphics, Los Angeles aesthetic, detailed urban environment, Rockstar Games visual style, contemporary city"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Assassin's Creed",
                description = "Historical adventure aesthetic",
                prompt = "Transform to Assassin's Creed style, historical architecture, parkour-friendly buildings, Ubisoft visual style, detailed historical setting, adventure game aesthetic"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Uncharted",
                description = "Cinematic adventure visuals",
                prompt = "Transform to Uncharted style, cinematic visuals, exotic locations, treasure hunter aesthetic, Naughty Dog graphics, adventure game atmosphere"
            });
            
            // Sci-Fi Games
            gameOptions.Add(new GameStyleOption
            {
                name = "Cyberpunk 2077",
                description = "Neon-drenched dystopia",
                prompt = "Transform to Cyberpunk 2077 style, neon-lit futuristic city, cyberpunk aesthetic, holographic advertisements, dystopian future, night city atmosphere"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Halo",
                description = "Sci-fi military aesthetic",
                prompt = "Transform to Halo style, sci-fi military aesthetic, futuristic architecture, UNSC technology, Bungie/343 visual style, alien technology"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Mass Effect",
                description = "Space opera visuals",
                prompt = "Transform to Mass Effect style, space opera aesthetic, futuristic citadel architecture, sci-fi technology, BioWare visual style, galaxy-spanning civilization"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Portal",
                description = "Clean testing facility",
                prompt = "Transform to Portal style, clean white test chambers, Aperture Science aesthetic, futuristic test facility, minimalist sci-fi design, portal gun technology"
            });
            
            // Horror Games
            gameOptions.Add(new GameStyleOption
            {
                name = "Resident Evil",
                description = "Survival horror atmosphere",
                prompt = "Transform to Resident Evil style, survival horror atmosphere, dark moody lighting, zombie apocalypse aesthetic, Capcom horror visual style, tense environment"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Silent Hill",
                description = "Foggy psychological horror",
                prompt = "Transform to Silent Hill style, dense fog everywhere, psychological horror atmosphere, rust and decay, nightmarish aesthetic, eerie fog-covered town"
            });
            
            // Shooter Games
            gameOptions.Add(new GameStyleOption
            {
                name = "Call of Duty",
                description = "Modern military realism",
                prompt = "Transform to Call of Duty style, modern military aesthetic, realistic warfare graphics, tactical environment, Activision visual style, military operations"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Battlefield",
                description = "Large-scale warfare",
                prompt = "Transform to Battlefield style, large-scale warfare aesthetic, destructible environment, military vehicles, DICE visual style, realistic combat zones"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Overwatch",
                description = "Colorful hero shooter",
                prompt = "Transform to Overwatch style, colorful stylized graphics, hero shooter aesthetic, Blizzard art style, vibrant futuristic setting, character-focused design"
            });
            
            // Racing Games
            gameOptions.Add(new GameStyleOption
            {
                name = "Forza Horizon",
                description = "Photorealistic racing",
                prompt = "Transform to Forza Horizon style, photorealistic racing graphics, beautiful scenery, luxury cars, racing festival atmosphere, playground Games visual style"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Mario Kart",
                description = "Whimsical cartoon racing",
                prompt = "Transform to Mario Kart style, colorful cartoon graphics, whimsical race tracks, Nintendo aesthetic, playful cartoon world, rainbow roads"
            });
            
            // Platformer Games
            gameOptions.Add(new GameStyleOption
            {
                name = "Super Mario Odyssey",
                description = "Vibrant Nintendo world",
                prompt = "Transform to Super Mario Odyssey style, vibrant colorful graphics, Nintendo aesthetic, whimsical platformer world, cheerful cartoon environment"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Sonic the Hedgehog",
                description = "High-speed colorful zones",
                prompt = "Transform to Sonic style, colorful zones, loop-de-loops, checkerboard patterns, SEGA aesthetic, high-speed platformer visuals, bright vibrant colors"
            });
            
            // Indie Games
            gameOptions.Add(new GameStyleOption
            {
                name = "Stardew Valley",
                description = "Cozy pixel art farming",
                prompt = "Transform to Stardew Valley style, pixel art graphics, cozy farming aesthetic, retro pixelated visuals, peaceful countryside, indie game charm"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Hollow Knight",
                description = "Hand-drawn metroidvania",
                prompt = "Transform to Hollow Knight style, hand-drawn art, dark whimsical aesthetic, bug kingdom visuals, metroidvania atmosphere, Team Cherry art style"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Celeste",
                description = "Pixel art mountain climbing",
                prompt = "Transform to Celeste style, pixel art platformer, mountain aesthetic, retro graphics, challenging platformer visuals, 8-bit inspired art"
            });
            
            // Strategy Games
            gameOptions.Add(new GameStyleOption
            {
                name = "Civilization VI",
                description = "Stylized world map",
                prompt = "Transform to Civilization VI style, stylized strategic map, board game aesthetic, hexagonal tiles, Firaxis visual style, colorful world domination"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Age of Empires",
                description = "Historical RTS aesthetic",
                prompt = "Transform to Age of Empires style, historical real-time strategy graphics, medieval buildings, classic RTS visual style, historical civilizations"
            });
            
            // Fighting Games
            gameOptions.Add(new GameStyleOption
            {
                name = "Street Fighter",
                description = "Stylized fighting arenas",
                prompt = "Transform to Street Fighter style, colorful fighting game aesthetic, dramatic backgrounds, Capcom fighting game visuals, martial arts arena"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "Mortal Kombat",
                description = "Dark martial arts realm",
                prompt = "Transform to Mortal Kombat style, dark martial arts aesthetic, NetherRealm Studios graphics, fighting tournament arena, mystical realm visuals"
            });
            
            // Classic Retro
            gameOptions.Add(new GameStyleOption
            {
                name = "8-Bit Retro",
                description = "Classic NES/Atari style",
                prompt = "Transform to 8-bit retro style, pixelated graphics, limited color palette, classic NES aesthetic, retro gaming visuals, old-school pixel art"
            });
            
            gameOptions.Add(new GameStyleOption
            {
                name = "16-Bit SNES Era",
                description = "Super Nintendo graphics",
                prompt = "Transform to 16-bit SNES style, Super Nintendo graphics, sprite-based visuals, retro pixel art, 90s gaming aesthetic, classic console era"
            });
        }
        
        public void Activate()
        {
            if (gamePanel != null)
            {
                gamePanel.SetActive(true);
            }
            
            MenuManager menuManager = FindFirstObjectByType<MenuManager>();
            if (menuManager != null)
            {
                webRtcConnection = menuManager.GetWebRTCConnection();
            }
            
            CreateGameItems();
            UpdateDisplay();
            
            if (instructionText != null)
            {
                instructionText.text = "Joystick: Navigate | Right Trigger: Apply Game Style";
            }
        }
        
        private void OnDisable()
        {
            if (gamePanel != null)
            {
                gamePanel.SetActive(false);
            }
        }
        
        private void CreateGameItems()
        {
            if (gameListContainer == null) return;
            
            // Clear existing items
            foreach (Transform child in gameListContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Create UI elements for each game option
            for (int i = 0; i < gameOptions.Count; i++)
            {
                if (gameItemPrefab != null)
                {
                    GameObject itemObj = Instantiate(gameItemPrefab, gameListContainer);
                    gameOptions[i].gameObject = itemObj;
                    
                    TMP_Text nameText = itemObj.transform.Find("Name")?.GetComponent<TMP_Text>();
                    TMP_Text descText = itemObj.transform.Find("Description")?.GetComponent<TMP_Text>();
                    
                    if (nameText != null) nameText.text = gameOptions[i].name;
                    if (descText != null) descText.text = gameOptions[i].description;
                }
            }
        }
        
        private void UpdateDisplay()
        {
            if (categoryText != null)
            {
                categoryText.text = "Video Game Styles - Game Selection";
            }
            
            for (int i = 0; i < gameOptions.Count; i++)
            {
                if (gameOptions[i].gameObject != null)
                {
                    Image background = gameOptions[i].gameObject.GetComponent<Image>();
                    if (background != null)
                    {
                        background.color = (i == selectedIndex) ? 
                            new Color(0.3f, 0.5f, 0.8f, 0.8f) : 
                            new Color(0.2f, 0.2f, 0.2f, 0.6f);
                    }
                }
            }
        }
        
        public void NavigateUp()
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = gameOptions.Count - 1;
            UpdateDisplay();
        }
        
        public void NavigateDown()
        {
            selectedIndex++;
            if (selectedIndex >= gameOptions.Count) selectedIndex = 0;
            UpdateDisplay();
        }
        
        public void Confirm()
        {
            if (selectedIndex >= 0 && selectedIndex < gameOptions.Count)
            {
                GameStyleOption selected = gameOptions[selectedIndex];
                if (webRtcConnection != null)
                {
                    Debug.Log($"Video Game Style - {selected.name} - Prompt: {selected.prompt}");
                    webRtcConnection.SendCustomPrompt(selected.prompt);
                }
            }
        }
    }
}

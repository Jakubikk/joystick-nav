using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Video Game Style feature - allows users to view their environment
    /// as if it was from various popular video games.
    /// </summary>
    public class VideoGameController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        [SerializeField] private TMP_Text selectedGameText;
        [SerializeField] private TMP_Text descriptionText;
        
        [Header("Game Style Menu")]
        [SerializeField] private List<Button> gameStyleButtons = new List<Button>();
        
        [Header("Navigation")]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color highlightedColor = Color.magenta;
        
        private int currentGameIndex = 0;
        private float lastNavigationTime = 0f;
        private const float navigationCooldown = 0.25f;
        
        // Video game styles with detailed visual transformation prompts
        private static readonly Dictionary<string, string> gameStyles = new Dictionary<string, string>
        {
            // Popular Game Franchises
            { "Minecraft", "Transform to Minecraft blocky voxel style with cubic blocks, pixelated textures, 16-bit aesthetic, block-based construction, distinctive Minecraft world" },
            { "LEGO", "Transform to LEGO brick world with everything made of colorful LEGO blocks, shiny plastic surfaces, studs on top, toy-like appearance" },
            { "Grand Theft Auto", "Transform to GTA-style with realistic urban environment, saturated colors, cinematic lighting, crime drama aesthetics, city life atmosphere" },
            { "Portal", "Transform to Portal game style with clean white test chambers, blue and orange portals, futuristic facility, sterile laboratory aesthetics" },
            { "Fallout", "Transform to post-apocalyptic Fallout style with retro-futuristic 1950s aesthetics, wasteland atmosphere, vault-tec elements, nuclear wasteland" },
            { "Skyrim", "Transform to Elder Scrolls Skyrim style with medieval fantasy, Nordic architecture, magical atmosphere, epic fantasy RPG aesthetics" },
            { "The Witcher", "Transform to Witcher 3 style with dark fantasy, medieval European aesthetics, mystical atmosphere, monster hunter world" },
            { "Zelda BOTW", "Transform to Zelda Breath of the Wild style with cel-shaded graphics, vibrant colors, anime-influenced design, fantasy adventure atmosphere" },
            { "Final Fantasy", "Transform to Final Fantasy style with JRPG aesthetics, fantasy architecture, magical crystals, epic adventure atmosphere" },
            { "Dark Souls", "Transform to Dark Souls gothic dark fantasy style with ominous atmosphere, medieval ruins, dramatic lighting, challenging world" },
            
            // Retro/Classic Games
            { "Super Mario", "Transform to Super Mario style with bright vibrant colors, cartoonish proportions, whimsical Nintendo aesthetics, playful atmosphere" },
            { "Sonic", "Transform to Sonic the Hedgehog style with vibrant blue and green colors, fast-paced aesthetics, loops and springs, Sega Genesis nostalgia" },
            { "8-Bit Retro", "Transform to 8-bit retro game style with chunky pixels, limited color palette, NES/Atari era aesthetics, nostalgic gaming" },
            { "16-Bit RPG", "Transform to 16-bit SNES RPG style with detailed sprites, vibrant pixel art, classic JRPG aesthetics, Super Nintendo era" },
            { "Pac-Man", "Transform to Pac-Man arcade style with simple geometric shapes, neon colors on black background, classic arcade aesthetics" },
            
            // FPS Games
            { "Call of Duty", "Transform to Call of Duty military shooter style with realistic modern warfare, tactical environment, gritty military aesthetics" },
            { "Halo", "Transform to Halo sci-fi style with futuristic UNSC technology, Forerunner architecture, space marine aesthetics, alien worlds" },
            { "Doom", "Transform to Doom style with heavy metal aesthetics, demonic atmosphere, industrial hellscapes, brutal FPS visuals" },
            { "Half-Life", "Transform to Half-Life style with Source engine aesthetics, industrial sci-fi, alien technology, dystopian atmosphere" },
            { "Borderlands", "Transform to Borderlands cel-shaded style with thick black outlines, comic book aesthetics, vibrant colors, stylized graphics" },
            
            // Horror Games
            { "Silent Hill", "Transform to Silent Hill psychological horror style with fog, rust, decay, nightmarish atmosphere, survival horror aesthetics" },
            { "Resident Evil", "Transform to Resident Evil survival horror style with dark mansion, zombie apocalypse atmosphere, tense horror environment" },
            { "Bioshock", "Transform to Bioshock art deco underwater city style with 1960s aesthetics, dystopian Rapture, steampunk elements" },
            
            // Open World Games
            { "Red Dead Redemption", "Transform to Red Dead Redemption Wild West style with old western town, dusty frontier, cowboy era aesthetics" },
            { "Assassin's Creed", "Transform to Assassin's Creed historical style with parkour-friendly architecture, historical accuracy, dramatic cityscapes" },
            { "Cyberpunk 2077", "Transform to Cyberpunk 2077 Night City style with neon signs, holographic ads, dystopian future, high-tech low-life" },
            { "Spider-Man PS4", "Transform to Spider-Man PS4 style with vibrant New York City, comic book aesthetics, superhero atmosphere" },
            
            // RPG/Fantasy
            { "World of Warcraft", "Transform to World of Warcraft style with stylized fantasy, vibrant colors, epic scale, MMO RPG aesthetics" },
            { "Diablo", "Transform to Diablo dark gothic style with demonic atmosphere, isometric view aesthetics, dark fantasy hellscapes" },
            { "Dragon Age", "Transform to Dragon Age medieval fantasy style with detailed environments, high fantasy, magical atmosphere" },
            { "Mass Effect", "Transform to Mass Effect sci-fi style with sleek futuristic design, alien technology, space opera aesthetics" },
            
            // Adventure/Puzzle
            { "The Last of Us", "Transform to The Last of Us post-apocalyptic style with overgrown vegetation, abandoned buildings, survival atmosphere" },
            { "Uncharted", "Transform to Uncharted adventure style with ancient ruins, treasure hunting atmosphere, cinematic adventure aesthetics" },
            { "Journey", "Transform to Journey artistic style with minimalist desert, flowing sand, spiritual atmosphere, artistic indie game look" },
            { "Monument Valley", "Transform to Monument Valley geometric puzzle style with impossible architecture, pastel colors, M.C. Escher-inspired design" },
            
            // Cartoon/Stylized
            { "Fortnite", "Transform to Fortnite battle royale style with vibrant cartoon graphics, colorful building materials, playful combat aesthetics" },
            { "Overwatch", "Transform to Overwatch Pixar-like style with colorful characters, heroic atmosphere, team shooter aesthetics" },
            { "Team Fortress 2", "Transform to Team Fortress 2 cartoony style with exaggerated proportions, comic aesthetics, valve's signature art style" },
            { "Animal Crossing", "Transform to Animal Crossing cute style with pastel colors, charming village, cozy life simulation aesthetics" },
            
            // Racing Games
            { "Mario Kart", "Transform to Mario Kart racing style with colorful tracks, power-up items, whimsical racing atmosphere" },
            { "Gran Turismo", "Transform to Gran Turismo realistic racing style with photo-realistic cars, professional racing atmosphere" },
            { "Rocket League", "Transform to Rocket League futuristic arena style with neon lights, soccer-car hybrid atmosphere" },
            
            // Survival/Crafting
            { "Terraria", "Transform to Terraria 2D pixel art style with vibrant pixel graphics, underground caves, adventure mining aesthetics" },
            { "Stardew Valley", "Transform to Stardew Valley cozy farming style with pixel art, peaceful countryside, farming sim aesthetics" },
            { "Subnautica", "Transform to Subnautica underwater alien ocean style with bioluminescent creatures, deep sea exploration atmosphere" },
            
            // Battle Royale
            { "PUBG", "Transform to PUBG realistic military style with tactical environment, battle royale wasteland, survival shooter aesthetics" },
            { "Apex Legends", "Transform to Apex Legends sci-fi style with futuristic weapons, hero shooter aesthetics, dynamic combat zones" },
            
            // Strategy
            { "Civilization", "Transform to Civilization strategic map style with hex grids, empire building aesthetics, historical strategy look" },
            { "StarCraft", "Transform to StarCraft RTS style with sci-fi military, alien races, space strategy warfare aesthetics" },
            
            // Indie Gems
            { "Celeste", "Transform to Celeste pixel art mountain style with retro graphics, challenging platformer aesthetics, mountain climbing atmosphere" },
            { "Hollow Knight", "Transform to Hollow Knight hand-drawn style with dark underground kingdom, bug characters, metroidvania aesthetics" },
            { "Cuphead", "Transform to Cuphead 1930s cartoon style with rubber hose animation, vintage cartoon aesthetics, hand-drawn bosses" },
        };
        
        private List<string> gameKeys = new List<string>();
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            // Convert dictionary keys to list for indexing
            gameKeys = new List<string>(gameStyles.Keys);
            
            if (descriptionText != null)
            {
                descriptionText.text = "Select a video game style to transform your environment";
            }
            
            UpdateGameStyleHighlight();
            UpdateSelectedGameDisplay();
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            // Navigation with cooldown
            if (Time.time - lastNavigationTime >= navigationCooldown)
            {
                Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                if (thumbstick.y > 0.5f) // Up
                {
                    NavigatePrevious();
                    lastNavigationTime = Time.time;
                }
                else if (thumbstick.y < -0.5f) // Down
                {
                    NavigateNext();
                    lastNavigationTime = Time.time;
                }
            }
            
            // Right trigger - Apply selected game style
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                ApplySelectedGameStyle();
            }
        }
        
        private void NavigateNext()
        {
            currentGameIndex++;
            if (currentGameIndex >= gameKeys.Count)
            {
                currentGameIndex = 0;
            }
            UpdateGameStyleHighlight();
            UpdateSelectedGameDisplay();
        }
        
        private void NavigatePrevious()
        {
            currentGameIndex--;
            if (currentGameIndex < 0)
            {
                currentGameIndex = gameKeys.Count - 1;
            }
            UpdateGameStyleHighlight();
            UpdateSelectedGameDisplay();
        }
        
        private void UpdateGameStyleHighlight()
        {
            // Update button colors if we have button references
            for (int i = 0; i < gameStyleButtons.Count && i < gameKeys.Count; i++)
            {
                if (gameStyleButtons[i] != null)
                {
                    var text = gameStyleButtons[i].GetComponentInChildren<TMP_Text>();
                    if (text != null)
                    {
                        text.color = (i == currentGameIndex) ? highlightedColor : normalColor;
                    }
                }
            }
        }
        
        private void UpdateSelectedGameDisplay()
        {
            if (selectedGameText != null && currentGameIndex >= 0 && currentGameIndex < gameKeys.Count)
            {
                selectedGameText.text = $"Selected: {gameKeys[currentGameIndex]}";
            }
        }
        
        private void ApplySelectedGameStyle()
        {
            if (currentGameIndex >= 0 && currentGameIndex < gameKeys.Count)
            {
                string gameName = gameKeys[currentGameIndex];
                string prompt = gameStyles[gameName];
                
                if (webRtcConnection != null)
                {
                    webRtcConnection.SendCustomPrompt(prompt);
                    Debug.Log($"VideoGameController: Applied game style {gameName}: {prompt}");
                }
            }
        }
        
        public void SelectGameStyle(string gameName)
        {
            int index = gameKeys.IndexOf(gameName);
            if (index >= 0)
            {
                currentGameIndex = index;
                UpdateGameStyleHighlight();
                UpdateSelectedGameDisplay();
            }
        }
        
        public void ApplyGameStyle(string gameName)
        {
            if (gameStyles.TryGetValue(gameName, out string prompt))
            {
                if (webRtcConnection != null)
                {
                    webRtcConnection.SendCustomPrompt(prompt);
                    Debug.Log($"VideoGameController: Applied game style {gameName}");
                }
            }
        }
    }
}

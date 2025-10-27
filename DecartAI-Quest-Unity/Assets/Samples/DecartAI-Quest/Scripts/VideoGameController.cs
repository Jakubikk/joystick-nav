using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls video game style transformation feature
    /// Allows user to view their environment as if it was from different video games
    /// </summary>
    public class VideoGameController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private List<TMP_Text> gameMenuItems;
        [SerializeField] private TMP_Text selectedGameText;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color selectedColor = Color.yellow;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Navigation Settings")]
        [SerializeField] private float joystickDeadzone = 0.5f;
        [SerializeField] private float navigationCooldown = 0.3f;
        
        private int currentGameIndex = 0;
        private float lastNavigationTime = 0f;
        private Dictionary<string, string> gamePrompts;
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            InitializeGamePrompts();
            UpdateGameHighlight();
        }
        
        private void InitializeGamePrompts()
        {
            // Define video game style transformation prompts optimized for Decart Mirage model
            gamePrompts = new Dictionary<string, string>
            {
                // Iconic Game Styles
                { "Minecraft", "Transform the environment into Minecraft blocky voxel world with cubic blocks, pixelated textures, squared shapes, and blocky aesthetic" },
                { "LEGO", "Transform the environment into LEGO brick world with colorful plastic bricks, studs on top, smooth surfaces, and toy construction aesthetic" },
                { "The Legend of Zelda", "Transform the environment into Zelda fantasy world with cel-shaded graphics, vibrant colors, magical atmosphere, and adventure game aesthetics" },
                { "Super Mario", "Transform the environment into Super Mario world with bright colorful blocks, question mark boxes, pipes, coins, and playful platformer style" },
                { "Portal", "Transform the environment into Portal test chamber with white panels, blue and orange portals, Aperture Science aesthetic, and sterile sci-fi design" },
                
                // Open World Games
                { "The Witcher 3", "Transform the environment into Witcher medieval fantasy with dark forests, wooden villages, mystical atmosphere, and fantasy RPG aesthetics" },
                { "Skyrim", "Transform the environment into Skyrim Nordic fantasy with stone architecture, dragon motifs, snowy peaks, torches, and Elder Scrolls atmosphere" },
                { "Red Dead Redemption", "Transform the environment into Wild West frontier with dusty towns, wooden buildings, desert landscape, and western frontier atmosphere" },
                { "Grand Theft Auto", "Transform the environment into GTA urban cityscape with neon signs, city streets, modern buildings, and crime game aesthetic" },
                { "Assassin's Creed", "Transform the environment into historical Assassin's Creed setting with parkour-friendly architecture, historical accuracy, and adventure atmosphere" },
                
                // Sci-Fi Games
                { "Cyberpunk 2077", "Transform the environment into Cyberpunk 2077 Night City with neon lights, holographic ads, futuristic tech, rain-slick streets, and dystopian atmosphere" },
                { "Halo", "Transform the environment into Halo sci-fi setting with UNSC technology, alien architecture, energy shields, and military sci-fi aesthetic" },
                { "Mass Effect", "Transform the environment into Mass Effect space station with sleek sci-fi design, holographic interfaces, alien technology, and space opera atmosphere" },
                { "Destiny", "Transform the environment into Destiny sci-fi fantasy with golden age architecture, alien geometry, mysterious technology, and space magic aesthetic" },
                { "Star Wars", "Transform the environment into Star Wars galaxy with sci-fi technology, droids, holographic displays, and iconic space opera design" },
                
                // Horror Games
                { "Resident Evil", "Transform the environment into Resident Evil survival horror with dark corridors, ominous lighting, abandoned facilities, and zombie apocalypse atmosphere" },
                { "Silent Hill", "Transform the environment into Silent Hill foggy nightmare with thick fog, rusty textures, otherworldly decay, and psychological horror aesthetic" },
                { "Dead Space", "Transform the environment into Dead Space sci-fi horror with dark spaceship corridors, blood stains, emergency lighting, and terrifying atmosphere" },
                { "BioShock", "Transform the environment into BioShock Rapture with art deco underwater city, leaking water, neon signs, and dystopian aesthetic" },
                
                // Fantasy RPGs
                { "Final Fantasy", "Transform the environment into Final Fantasy JRPG world with crystals, magical energy, fantasy architecture, and anime-inspired aesthetics" },
                { "Dark Souls", "Transform the environment into Dark Souls gothic fantasy with medieval castles, dark stone, torches, ominous atmosphere, and challenging RPG aesthetic" },
                { "Dragon Age", "Transform the environment into Dragon Age high fantasy with medieval kingdoms, magical effects, fantasy architecture, and epic RPG atmosphere" },
                { "World of Warcraft", "Transform the environment into WoW fantasy world with colorful stylized graphics, fantasy races, magical effects, and MMORPG aesthetic" },
                
                // Stylized Games
                { "Borderlands", "Transform the environment into Borderlands cel-shaded world with thick black outlines, comic book style, vibrant colors, and looter-shooter aesthetic" },
                { "Team Fortress 2", "Transform the environment into TF2 cartoonish style with exaggerated proportions, bright colors, retro-future design, and playful shooter aesthetic" },
                { "Overwatch", "Transform the environment into Overwatch futuristic world with bright colors, clean design, hero shooter aesthetic, and optimistic sci-fi style" },
                { "Fortnite", "Transform the environment into Fortnite cartoonish world with bright colors, simplified shapes, building materials, and battle royale aesthetic" },
                
                // Atmospheric Games
                { "Journey", "Transform the environment into Journey desert world with flowing sand, ancient ruins, cloth physics, golden lighting, and artistic minimalist style" },
                { "Firewatch", "Transform the environment into Firewatch national park with stylized nature, warm colors, lookout towers, and atmospheric indie aesthetic" },
                { "Limbo", "Transform the environment into Limbo monochrome world with black silhouettes, gray backgrounds, puzzle platformer aesthetic, and minimalist design" },
                { "Gris", "Transform the environment into Gris watercolor world with hand-drawn aesthetics, flowing colors, artistic design, and emotional atmosphere" },
                
                // Retro Games
                { "Pixel Art 8-bit", "Transform the environment into 8-bit pixel art style with chunky pixels, limited color palette, retro gaming aesthetic, and nostalgic design" },
                { "Pixel Art 16-bit", "Transform the environment into 16-bit pixel art with detailed sprites, expanded colors, SNES-era graphics, and classic gaming style" },
                { "Pac-Man", "Transform the environment into Pac-Man maze world with neon mazes, glowing dots, retro arcade aesthetic, and simple geometric design" },
                { "Tron", "Transform the environment into Tron digital world with glowing neon lines, grid floor, circuit patterns, and retro-futuristic aesthetic" },
                
                // Battle Royale & Survival
                { "PUBG", "Transform the environment into PUBG battleground with military realistic graphics, war-torn landscape, tactical atmosphere, and survival aesthetic" },
                { "Apex Legends", "Transform the environment into Apex Legends sci-fi arena with futuristic architecture, bright colors, high-tech design, and battle royale style" },
                { "Rust", "Transform the environment into Rust survival setting with makeshift structures, scrap metal, harsh environment, and post-apocalyptic aesthetic" },
                { "DayZ", "Transform the environment into DayZ zombie survival with abandoned buildings, Eastern European architecture, decay, and apocalyptic atmosphere" },
                
                // Anime Games
                { "Genshin Impact", "Transform the environment into Genshin Impact anime fantasy with vibrant colors, magical effects, Asian-inspired architecture, and gacha game aesthetic" },
                { "Persona 5", "Transform the environment into Persona 5 stylish world with bold reds, blacks, geometric UI elements, jazz aesthetic, and JRPG style" },
                { "Ni no Kuni", "Transform the environment into Ni no Kuni Studio Ghibli style with hand-drawn aesthetics, whimsical fantasy, watercolor backgrounds, and anime charm" },
                { "Tales Series", "Transform the environment into Tales JRPG world with anime aesthetics, fantasy elements, colorful magic, and adventure atmosphere" },
                
                // Racing Games
                { "Mario Kart", "Transform the environment into Mario Kart racing track with colorful courses, item boxes, boost pads, and playful racing aesthetic" },
                { "Need for Speed", "Transform the environment into NFS underground racing with neon lights, night city, custom cars, street racing atmosphere, and urban style" },
                { "Forza Horizon", "Transform the environment into Forza festival setting with scenic landscapes, exotic cars, music festivals, and racing game aesthetic" },
                
                // Strategy Games
                { "Civilization", "Transform the environment into Civilization hex-grid world with strategic map, historical wonders, empire-building aesthetic, and 4X strategy style" },
                { "StarCraft", "Transform the environment into StarCraft sci-fi battlefield with Terran bases, alien structures, RTS design, and space warfare aesthetic" },
                { "Age of Empires", "Transform the environment into Age of Empires medieval setting with castle walls, historical accuracy, RTS design, and strategic warfare style" }
            };
        }
        
        private void Update()
        {
            if (!gameObject.activeInHierarchy) return;
            
            HandleNavigation();
            HandleSelection();
        }
        
        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown) return;
            if (gameMenuItems == null || gameMenuItems.Count == 0) return;
            
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            // Up navigation
            if (joystickInput.y > joystickDeadzone)
            {
                currentGameIndex--;
                if (currentGameIndex < 0)
                {
                    currentGameIndex = gameMenuItems.Count - 1;
                }
                UpdateGameHighlight();
                lastNavigationTime = Time.time;
            }
            // Down navigation
            else if (joystickInput.y < -joystickDeadzone)
            {
                currentGameIndex++;
                if (currentGameIndex >= gameMenuItems.Count)
                {
                    currentGameIndex = 0;
                }
                UpdateGameHighlight();
                lastNavigationTime = Time.time;
            }
        }
        
        private void HandleSelection()
        {
            // Right trigger to select game style
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyGameStyle();
            }
        }
        
        private void UpdateGameHighlight()
        {
            if (gameMenuItems == null) return;
            
            for (int i = 0; i < gameMenuItems.Count; i++)
            {
                if (gameMenuItems[i] != null)
                {
                    gameMenuItems[i].color = (i == currentGameIndex) ? selectedColor : normalColor;
                }
            }
            
            if (selectedGameText != null && gameMenuItems.Count > 0 && currentGameIndex < gameMenuItems.Count)
            {
                string gameName = gameMenuItems[currentGameIndex].text;
                selectedGameText.text = $"Selected: {gameName}";
            }
        }
        
        private void ApplyGameStyle()
        {
            if (webRtcConnection == null || gameMenuItems == null || gameMenuItems.Count == 0) return;
            if (currentGameIndex < 0 || currentGameIndex >= gameMenuItems.Count) return;
            
            string gameName = gameMenuItems[currentGameIndex].text;
            
            if (gamePrompts.ContainsKey(gameName))
            {
                string prompt = gamePrompts[gameName];
                webRtcConnection.SendCustomPrompt(prompt);
                Debug.Log($"Video Game Style: Applied {gameName}");
                
                if (selectedGameText != null)
                {
                    selectedGameText.text = $"Applying: {gameName}";
                }
            }
            else
            {
                Debug.LogWarning($"Video Game Style: No prompt found for {gameName}");
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Handles video game world transformation feature using Mirage model
    /// Allows users to see their environment as various video game aesthetics
    /// </summary>
    public class GameWorldController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Transform gameListContainer;
        [SerializeField] private GameObject gameItemPrefab;
        [SerializeField] private TMP_Text selectedGameText;
        [SerializeField] private TMP_Text descriptionText;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private List<GameWorldOption> gameOptions = new List<GameWorldOption>();
        private int selectedIndex = 0;
        private float joystickCooldown = 0f;
        private const float JOYSTICK_COOLDOWN_TIME = 0.3f;
        
        private class GameWorldOption
        {
            public string Name;
            public string Description;
            public string Prompt;
            public GameObject UIElement;
        }
        
        private void OnEnable()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            InitializeGameOptions();
            UpdateDisplay();
        }
        
        private void InitializeGameOptions()
        {
            gameOptions.Clear();
            
            // Blocky/Voxel Games
            AddGameOption("Minecraft World", "Blocky voxel aesthetic",
                "Transform to Minecraft game style, blocky voxel world, pixelated textures, cube-shaped objects, block-based terrain, distinctive Minecraft aesthetic, low-poly geometric shapes");
            
            AddGameOption("LEGO World", "Everything made of LEGO bricks",
                "Transform to LEGO brick world, colorful plastic LEGO blocks, minifigure scale, studded brick surfaces, bright primary colors, toy-like appearance, LEGO building aesthetic");
            
            AddGameOption("Terraria Style", "2D side-scrolling pixel art",
                "Transform to Terraria pixel art style, 2D side-view perspective, retro pixel graphics, vibrant colors, sprite-based aesthetic, classic 2D game look");
            
            // Anime & Cel-Shaded
            AddGameOption("Anime World", "Japanese anime aesthetic",
                "Transform to anime style, large expressive eyes, vibrant colors, cel-shaded appearance, dramatic lighting, Japanese animation aesthetic, detailed character features, anime art style");
            
            AddGameOption("Cel-Shaded Comic", "Comic book cel-shading",
                "Transform to cel-shaded comic book style, bold outlines, flat colors, comic book aesthetic, graphic novel appearance, strong black outlines, pop art influence");
            
            AddGameOption("Studio Ghibli", "Ghibli animation style",
                "Transform to Studio Ghibli animation style, whimsical hand-drawn aesthetic, soft watercolor backgrounds, charming character design, magical atmosphere, Miyazaki-inspired");
            
            // Cyberpunk & Futuristic
            AddGameOption("Cyberpunk 2077", "Neon dystopian future",
                "Transform to Cyberpunk 2077 style, neon lights everywhere, dystopian future aesthetic, holographic displays, rain-slicked surfaces, chrome and neon, high-tech cyberpunk atmosphere");
            
            AddGameOption("Deus Ex", "Augmented reality overlay",
                "Transform to Deus Ex augmented vision, golden-amber color grading, tech overlay interface, futuristic cyberpunk aesthetic, augmented reality elements, conspiracy atmosphere");
            
            AddGameOption("Tron Legacy", "Digital grid world",
                "Transform to Tron digital world, glowing blue and orange lights, grid-like environment, digital circuits, neon pathways, sleek futuristic design, computer-generated realm");
            
            // Fantasy & RPG
            AddGameOption("World of Warcraft", "Fantasy MMO style",
                "Transform to World of Warcraft style, vibrant fantasy aesthetic, exaggerated proportions, rich colors, magical atmosphere, fantasy game world, stylized environment");
            
            AddGameOption("The Legend of Zelda", "Cel-shaded fantasy",
                "Transform to Zelda Breath of the Wild style, beautiful cel-shaded graphics, fantasy landscape, vibrant colors, adventure game aesthetic, magical kingdom atmosphere");
            
            AddGameOption("Dark Souls", "Gothic dark fantasy",
                "Transform to Dark Souls gothic style, dark medieval fantasy, foreboding atmosphere, dramatic shadows, weathered stone architecture, ominous and challenging aesthetic");
            
            AddGameOption("Skyrim", "High fantasy realm",
                "Transform to Skyrim fantasy style, Nordic medieval aesthetic, high fantasy atmosphere, epic landscapes, magical elements, elder scrolls visual style");
            
            // Retro & Pixel Art
            AddGameOption("8-Bit Retro", "Classic NES style",
                "Transform to 8-bit retro game style, chunky pixels, limited color palette, classic NES aesthetic, pixelated graphics, vintage video game look");
            
            AddGameOption("16-Bit Era", "Super Nintendo style",
                "Transform to 16-bit game style, SNES-era pixel art, vibrant colors, detailed sprites, classic Super Nintendo aesthetic, retro gaming nostalgia");
            
            AddGameOption("Pixel Art Modern", "Contemporary pixel art",
                "Transform to modern pixel art style, detailed pixelated graphics, vibrant color palette, indie game aesthetic, retro-inspired contemporary look");
            
            // Realistic & Simulation
            AddGameOption("Grand Theft Auto", "Urban open world",
                "Transform to GTA style, realistic urban environment, saturated colors, city atmosphere, open-world game aesthetic, modern city setting");
            
            AddGameOption("The Sims", "Life simulation style",
                "Transform to The Sims game style, bright cheerful colors, clean simulation aesthetic, everyday life setting, colorful UI elements, life sim appearance");
            
            AddGameOption("Red Dead Redemption", "Western frontier",
                "Transform to Red Dead Redemption style, Wild West aesthetic, cinematic lighting, realistic Western atmosphere, frontier setting, dusty landscapes");
            
            // Horror & Atmospheric
            AddGameOption("Silent Hill", "Psychological horror",
                "Transform to Silent Hill foggy atmosphere, thick fog everywhere, eerie lighting, psychological horror aesthetic, rusty textures, nightmarish environment");
            
            AddGameOption("Resident Evil", "Survival horror",
                "Transform to Resident Evil style, survival horror atmosphere, dark shadows, tense lighting, abandoned building aesthetic, zombie apocalypse setting");
            
            AddGameOption("Limbo/Inside", "Noir platformer",
                "Transform to Limbo monochrome style, black and white noir aesthetic, silhouette-based graphics, atmospheric puzzle platformer, minimalist dark design");
            
            // Sci-Fi Shooters
            AddGameOption("Halo", "Military sci-fi",
                "Transform to Halo sci-fi military style, futuristic UNSC technology, alien covenant aesthetic, space marine atmosphere, epic sci-fi warfare");
            
            AddGameOption("Destiny", "Space fantasy",
                "Transform to Destiny sci-fi fantasy style, futuristic guardian aesthetic, alien worlds, space magic atmosphere, epic sci-fi fantasy blend");
            
            AddGameOption("Half-Life", "Dystopian sci-fi",
                "Transform to Half-Life style, dystopian sci-fi environment, industrial decay, alien invasion aesthetic, scientific facility atmosphere");
            
            // Artistic & Unique
            AddGameOption("Journey", "Artistic adventure",
                "Transform to Journey game style, beautiful artistic aesthetic, flowing sand, warm golden colors, minimalist design, peaceful meditative atmosphere");
            
            AddGameOption("Okami", "Japanese watercolor",
                "Transform to Okami sumi-e style, Japanese watercolor painting aesthetic, cel-shaded with brushstroke effects, traditional Japanese art inspiration");
            
            AddGameOption("Mirror's Edge", "Pristine parkour",
                "Transform to Mirror's Edge clean aesthetic, stark white buildings with colored accents, first-person parkour view, minimalist urban design");
            
            AddGameOption("Borderlands", "Cel-shaded shooter",
                "Transform to Borderlands comic style, thick black outlines, hand-drawn appearance, vibrant colors, comic book cel-shading, stylized wasteland aesthetic");
        }
        
        private void AddGameOption(string name, string description, string prompt)
        {
            gameOptions.Add(new GameWorldOption
            {
                Name = name,
                Description = description,
                Prompt = prompt
            });
        }
        
        private void Update()
        {
            if (joystickCooldown > 0)
            {
                joystickCooldown -= Time.deltaTime;
            }
            else
            {
                Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                if (joystick.y > 0.5f) // Up
                {
                    NavigateUp();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
                else if (joystick.y < -0.5f) // Down
                {
                    NavigateDown();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
            }
            
            // Right trigger to apply selected game world
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyGameWorld();
            }
        }
        
        private void NavigateUp()
        {
            selectedIndex--;
            if (selectedIndex < 0)
                selectedIndex = gameOptions.Count - 1;
            UpdateDisplay();
        }
        
        private void NavigateDown()
        {
            selectedIndex++;
            if (selectedIndex >= gameOptions.Count)
                selectedIndex = 0;
            UpdateDisplay();
        }
        
        private void UpdateDisplay()
        {
            // Clear existing UI
            foreach (Transform child in gameListContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Create UI elements
            for (int i = 0; i < gameOptions.Count; i++)
            {
                GameObject itemObj = Instantiate(gameItemPrefab, gameListContainer);
                TMP_Text itemText = itemObj.GetComponentInChildren<TMP_Text>();
                
                if (itemText != null)
                {
                    itemText.text = gameOptions[i].Name;
                    
                    if (i == selectedIndex)
                    {
                        itemText.color = Color.green;
                        itemText.fontSize = 26;
                    }
                    else
                    {
                        itemText.color = Color.white;
                        itemText.fontSize = 22;
                    }
                }
                
                gameOptions[i].UIElement = itemObj;
            }
            
            if (selectedGameText != null)
            {
                selectedGameText.text = $"Selected: {gameOptions[selectedIndex].Name}";
            }
            
            if (descriptionText != null)
            {
                descriptionText.text = gameOptions[selectedIndex].Description;
            }
        }
        
        private void ApplyGameWorld()
        {
            if (webRtcConnection == null || selectedIndex >= gameOptions.Count) return;
            
            string prompt = gameOptions[selectedIndex].Prompt;
            webRtcConnection.SendCustomPrompt(prompt);
            
            Debug.Log($"Game World: Applied {gameOptions[selectedIndex].Name}");
        }
    }
}

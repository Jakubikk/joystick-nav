using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Video Game Style feature - transform environment to look like any video game
    /// Uses Decart Mirage model for game-style transformations
    /// </summary>
    public class VideoGameStyleFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text selectedGameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text instructionsText;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private int currentGameIndex = 0;
        private float joystickCooldown = 0f;
        private const float JOYSTICK_COOLDOWN_TIME = 0.3f;
        
        // Video game styles database
        private readonly List<(string name, string description, string prompt)> gameStyles = new List<(string, string, string)>()
        {
            // Popular Game Franchises
            ("Minecraft", 
             "Blocky voxel world, pixelated textures", 
             "Transform the environment into Minecraft world with blocky voxel structures, pixelated cube textures, grass blocks, stone blocks, and signature Minecraft aesthetic"),
            
            ("LEGO World", 
             "Colorful LEGO bricks, studs on top", 
             "Transform the environment into LEGO world with colorful plastic LEGO bricks, studs visible on blocks, smooth plastic material, and playful LEGO construction aesthetic"),
            
            ("Cyberpunk 2077", 
             "Neon lights, dystopian future, tech implants", 
             "Transform the environment into Cyberpunk 2077 style with neon signs, holographic displays, cybernetic elements, rain-slicked streets, dystopian megacity atmosphere"),
            
            ("Zelda: Breath of the Wild", 
             "Cell-shaded graphics, fantasy adventure", 
             "Transform the environment into Zelda Breath of the Wild style with cel-shaded cartoon graphics, vibrant colors, fantasy elements, adventure game aesthetic, mystical atmosphere"),
            
            ("Grand Theft Auto", 
             "Urban city, realistic graphics, street culture", 
             "Transform the environment into GTA style with urban city streets, realistic graphics, street culture elements, graffiti, city lights, and open-world game aesthetic"),
            
            ("The Sims", 
             "Suburban life, plumbob aesthetic, cozy homes", 
             "Transform the environment into The Sims world with colorful suburban homes, warm lighting, cozy furniture, life simulation game aesthetic"),
            
            ("Animal Crossing", 
             "Cute cartoon style, pastel colors, cozy village", 
             "Transform the environment into Animal Crossing style with cute rounded shapes, pastel colors, friendly cartoon aesthetic, cozy village atmosphere"),
            
            ("Final Fantasy", 
             "High fantasy, magical crystals, epic adventure", 
             "Transform the environment into Final Fantasy style with high fantasy elements, magical crystals, ornate designs, epic JRPG aesthetic, otherworldly atmosphere"),
            
            ("Portal", 
             "Clean white facility, portal gun, testing chambers", 
             "Transform the environment into Portal game style with white sterile facility, Aperture Science aesthetic, blue and orange portals, testing chamber atmosphere"),
            
            ("Halo", 
             "Sci-fi military, alien technology, space marine", 
             "Transform the environment into Halo style with futuristic military structures, alien covenant technology, UNSC elements, space marine sci-fi aesthetic"),
            
            // Art Styles
            ("Retro 8-bit", 
             "Pixelated graphics, limited color palette", 
             "Transform the environment into retro 8-bit pixel art style with chunky pixels, limited color palette, classic NES/SNES aesthetic, nostalgic gaming look"),
            
            ("16-bit RPG", 
             "Super Nintendo style, sprite-based graphics", 
             "Transform the environment into 16-bit RPG style like classic Super Nintendo games, detailed pixel sprites, vibrant colors, JRPG aesthetic"),
            
            ("Cel-Shaded Cartoon", 
             "Comic book outlines, flat colors, toon shading", 
             "Transform the environment into cel-shaded cartoon style with bold black outlines, flat color zones, comic book aesthetic, animated game look"),
            
            ("Realistic FPS", 
             "Modern military shooter, photorealistic", 
             "Transform the environment into realistic first-person shooter style with photorealistic graphics, military tactical elements, modern warfare aesthetic"),
            
            ("Anime JRPG", 
             "Japanese anime style, vibrant colors, expressive", 
             "Transform the environment into anime JRPG style with Japanese animation aesthetics, vibrant saturated colors, expressive character designs, manga-inspired look"),
            
            // Specific Game Aesthetics
            ("Dark Souls", 
             "Gothic architecture, dark fantasy, atmospheric", 
             "Transform the environment into Dark Souls style with gothic architecture, dark fantasy atmosphere, stone castles, dim lighting, melancholic medieval aesthetic"),
            
            ("Fortnite", 
             "Colorful cartoon, stylized characters, battle royale", 
             "Transform the environment into Fortnite style with colorful cartoon graphics, stylized proportions, vibrant colors, playful battle royale aesthetic"),
            
            ("Super Mario", 
             "Bright colors, mushroom kingdom, platformer fun", 
             "Transform the environment into Super Mario world with bright cheerful colors, question blocks, mushrooms, pipes, whimsical platformer aesthetic"),
            
            ("Sonic the Hedgehog", 
             "Speed lines, loop-de-loops, vibrant zones", 
             "Transform the environment into Sonic world with colorful zones, speed elements, loop-de-loops, golden rings, fast-paced platformer aesthetic"),
            
            ("Pokemon", 
             "Cute creatures, tall grass, adventure zones", 
             "Transform the environment into Pokemon world with cute creature aesthetics, tall grass patches, pokeballs, adventure RPG style, colorful Nintendo look"),
            
            ("Skyrim", 
             "Nordic fantasy, snowy mountains, medieval RPG", 
             "Transform the environment into Skyrim style with Nordic fantasy architecture, snowy mountain atmosphere, medieval RPG aesthetic, dragon-infused world"),
            
            ("Fallout", 
             "Post-apocalyptic, retro-futurism, wasteland", 
             "Transform the environment into Fallout style with post-apocalyptic wasteland, retro-futuristic 1950s aesthetic, rusted metal, Vault-Tec elements"),
            
            ("Overwatch", 
             "Colorful heroes, futuristic optimism, team shooter", 
             "Transform the environment into Overwatch style with colorful hero-shooter aesthetic, futuristic optimistic design, stylized characters, team-based arena look"),
            
            ("World of Warcraft", 
             "High fantasy MMO, colorful zones, epic scale", 
             "Transform the environment into World of Warcraft style with high fantasy MMO aesthetic, vibrant zone designs, epic scale architecture, Azeroth atmosphere"),
            
            ("Valorant", 
             "Clean tactical shooter, stylized realistic", 
             "Transform the environment into Valorant style with clean tactical shooter aesthetic, stylized realistic graphics, agent abilities visual effects, competitive map design"),
            
            ("Roblox", 
             "Simple blocky characters, user-created worlds", 
             "Transform the environment into Roblox style with simple blocky character aesthetic, colorful user-created world look, platform game simplicity"),
            
            ("Borderlands", 
             "Comic book cel-shading, wasteland humor, looter shooter", 
             "Transform the environment into Borderlands style with thick black outlines, cell-shaded comic look, wasteland aesthetic, pop-art colors, looter shooter vibe"),
            
            ("League of Legends", 
             "Fantasy MOBA, colorful champions, magical effects", 
             "Transform the environment into League of Legends style with fantasy MOBA aesthetic, colorful magical effects, Runeterra-inspired design, competitive arena look"),
            
            ("Among Us", 
             "Simple cartoon astronauts, space station", 
             "Transform the environment into Among Us style with simple cute astronaut aesthetics, colorful space station, minimalist cartoon design, social deduction game look"),
            
            ("Stardew Valley", 
             "Pixel art farming, cozy rural life, 16-bit charm", 
             "Transform the environment into Stardew Valley style with pixel art farming aesthetic, cozy rural atmosphere, warm colors, peaceful indie game charm")
        };
        
        private void OnEnable()
        {
            InitializeFeature();
        }
        
        private void InitializeFeature()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            // Ensure Mirage model is selected for game-style transformations
            if (webRtcConnection != null)
            {
                webRtcConnection.SetModelChoice(false); // false = Mirage model
            }
            
            currentGameIndex = 0;
            UpdateDisplay();
            
            if (instructionsText != null)
            {
                instructionsText.text = "Joystick UP/DOWN: Browse video game styles\n" +
                                       "Right Trigger: Apply game style\n" +
                                       "Left Trigger: Return to menu";
            }
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            // Update cooldown
            if (joystickCooldown > 0)
            {
                joystickCooldown -= Time.deltaTime;
            }
            
            // Joystick navigation
            if (joystickCooldown <= 0)
            {
                Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                if (joystick.y > 0.5f) // Up
                {
                    currentGameIndex--;
                    if (currentGameIndex < 0)
                        currentGameIndex = gameStyles.Count - 1;
                    
                    UpdateDisplay();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
                else if (joystick.y < -0.5f) // Down
                {
                    currentGameIndex++;
                    if (currentGameIndex >= gameStyles.Count)
                        currentGameIndex = 0;
                    
                    UpdateDisplay();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
            }
            
            // Right Trigger = Apply game style
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                ApplyGameStyle();
            }
        }
        
        private void UpdateDisplay()
        {
            if (currentGameIndex < gameStyles.Count)
            {
                var game = gameStyles[currentGameIndex];
                
                if (selectedGameText != null)
                {
                    selectedGameText.text = $"{game.name}\n({currentGameIndex + 1}/{gameStyles.Count})";
                }
                
                if (descriptionText != null)
                {
                    descriptionText.text = game.description;
                }
            }
        }
        
        private void ApplyGameStyle()
        {
            if (webRtcConnection == null)
            {
                Debug.LogError("VideoGameStyleFeature: WebRTCConnection not found!");
                return;
            }
            
            if (currentGameIndex >= gameStyles.Count)
            {
                Debug.LogError("VideoGameStyleFeature: Invalid game index!");
                return;
            }
            
            var selectedGame = gameStyles[currentGameIndex];
            string prompt = selectedGame.prompt;
            
            Debug.Log($"Applying game style: {selectedGame.name} - Prompt: {prompt}");
            
            // Send the prompt to Decart AI (Mirage model)
            webRtcConnection.SendCustomPrompt(prompt);
            
            // Provide visual feedback
            if (descriptionText != null)
            {
                descriptionText.text = $"{selectedGame.description}\n<color=green>✓ Game style applied!</color>";
            }
        }
    }
}

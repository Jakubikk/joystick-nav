using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Biome/Location Transform feature - view your room as if it was in different countries or biomes
    /// Uses Decart Mirage model for environment transformation
    /// </summary>
    public class BiomeTransformFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text selectedBiomeText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text instructionsText;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private int currentBiomeIndex = 0;
        private float joystickCooldown = 0f;
        private const float JOYSTICK_COOLDOWN_TIME = 0.3f;
        
        // Biomes and locations database
        private readonly List<(string name, string description, string prompt)> biomes = new List<(string, string, string)>()
        {
            // Natural Biomes
            ("Tropical Beach", 
             "Sandy beaches, palm trees, crystal blue ocean", 
             "Transform the environment into a tropical beach paradise with white sand, swaying palm trees, turquoise ocean water, beach umbrellas, seashells, and warm sunny atmosphere"),
            
            ("Desert Oasis", 
             "Sand dunes, desert plants, oasis water", 
             "Transform the environment into a desert oasis with golden sand dunes, cacti, palm trees around water, warm amber lighting, desert rocks, and arid atmosphere"),
            
            ("Tropical Rainforest", 
             "Dense jungle, exotic plants, waterfalls", 
             "Transform the environment into a lush tropical rainforest with dense vegetation, exotic plants, hanging vines, colorful birds, waterfalls, and humid jungle atmosphere"),
            
            ("Alpine Mountains", 
             "Snow peaks, pine trees, mountain air", 
             "Transform the environment into alpine mountains with snow-capped peaks, evergreen pine trees, rocky cliffs, crisp mountain air, and majestic scenery"),
            
            ("Arctic Tundra", 
             "Snow, ice, northern lights", 
             "Transform the environment into arctic tundra with snow-covered ground, ice formations, aurora borealis in the sky, frozen atmosphere, and polar landscape"),
            
            ("Bamboo Forest", 
             "Tall bamboo, zen atmosphere, Japanese style", 
             "Transform the environment into a serene bamboo forest with tall green bamboo stalks, dappled sunlight, Japanese zen garden elements, peaceful atmosphere"),
            
            ("Coral Reef Underwater", 
             "Underwater scene, colorful coral, fish", 
             "Transform the environment into an underwater coral reef with colorful corals, tropical fish swimming, blue water, aquatic plants, and submarine atmosphere"),
            
            ("Savanna Grassland", 
             "Golden grass, acacia trees, African plains", 
             "Transform the environment into African savanna with golden tall grass, acacia trees, warm sunset lighting, wildlife hints, and open plains atmosphere"),
            
            ("Cherry Blossom Garden", 
             "Pink cherry blossoms, Japanese garden", 
             "Transform the environment into a Japanese cherry blossom garden with pink sakura trees in bloom, petals falling, traditional garden elements, spring atmosphere"),
            
            // Countries and Cultures
            ("Japan - Traditional", 
             "Japanese architecture, paper lanterns, zen", 
             "Transform the environment into traditional Japan with wooden architecture, paper lanterns, tatami mats, sliding doors, zen garden elements, and serene Japanese atmosphere"),
            
            ("China - Imperial", 
             "Chinese architecture, red lanterns, dragons", 
             "Transform the environment into imperial China with ornate red and gold architecture, hanging lanterns, dragon motifs, pagoda elements, and majestic Chinese style"),
            
            ("Egypt - Ancient", 
             "Pyramids, hieroglyphs, desert temple", 
             "Transform the environment into ancient Egypt with pyramid structures, hieroglyphic carvings, sandstone pillars, desert atmosphere, and pharaonic architecture"),
            
            ("Greece - Ancient", 
             "White marble, Greek columns, Mediterranean", 
             "Transform the environment into ancient Greece with white marble columns, Greek architecture, Mediterranean blue sky, olive trees, and classical Hellenic style"),
            
            ("India - Palace", 
             "Colorful textiles, ornate design, Taj Mahal style", 
             "Transform the environment into an Indian palace with vibrant colors, intricate patterns, ornate archways, decorative tiles, and Mughal architectural style"),
            
            ("Morocco - Marketplace", 
             "Colorful tiles, lanterns, Arabic architecture", 
             "Transform the environment into a Moroccan marketplace with colorful mosaic tiles, brass lanterns, arched doorways, vibrant fabrics, and North African atmosphere"),
            
            ("Iceland - Volcanic", 
             "Black rock, geysers, volcanic landscape", 
             "Transform the environment into Icelandic volcanic landscape with black volcanic rock, steam geysers, moss-covered lava, dramatic Nordic atmosphere"),
            
            ("Italy - Venice", 
             "Canals, gondolas, Italian architecture", 
             "Transform the environment into Venice Italy with water canals, gondolas, ornate Italian architecture, bridges, and romantic Venetian atmosphere"),
            
            ("France - Paris", 
             "Parisian cafes, Eiffel tower style, cobblestone", 
             "Transform the environment into Paris France with Parisian cafe culture, wrought iron details, cobblestone streets, French elegance, and romantic atmosphere"),
            
            ("Dubai - Modern", 
             "Luxury skyscrapers, gold accents, modern Arabic", 
             "Transform the environment into modern Dubai with towering luxury skyscrapers, gold and glass architecture, futuristic design, opulent details, and contemporary Arabic style"),
            
            ("California - Beach Life", 
             "Palm trees, surfboards, sunny coastal vibe", 
             "Transform the environment into California beach lifestyle with palm trees, surfboards, beach houses, sunny atmosphere, and laid-back coastal California vibe"),
            
            // Fantasy and Special Biomes
            ("Crystal Cave", 
             "Glowing crystals, gemstones, magical underground", 
             "Transform the environment into a magical crystal cave with glowing luminescent crystals, gemstone formations, sparkles, underground cavern, and mystical atmosphere"),
            
            ("Fairy Forest", 
             "Magical lights, mushrooms, enchanted woods", 
             "Transform the environment into an enchanted fairy forest with glowing mushrooms, magical floating lights, twisted trees, sparkles, and whimsical fantasy atmosphere"),
            
            ("Space Station", 
             "Futuristic sci-fi, zero gravity, stars visible", 
             "Transform the environment into a space station interior with futuristic technology, viewing windows showing stars, sleek metallic surfaces, and zero-gravity sci-fi atmosphere"),
            
            ("Underwater City", 
             "Atlantis-style, glass domes, marine life", 
             "Transform the environment into an underwater city with glass dome structures, aquatic architecture, fish swimming by windows, blue water glow, and Atlantis-style atmosphere"),
            
            ("Cloud Kingdom", 
             "Floating islands, clouds, heavenly atmosphere", 
             "Transform the environment into a cloud kingdom with fluffy clouds as floor, floating islands, ethereal light, heavenly gates, and dreamlike sky atmosphere"),
            
            ("Mushroom Forest", 
             "Giant mushrooms, bioluminescence, fantasy biome", 
             "Transform the environment into a fantasy mushroom forest with giant colorful mushrooms, bioluminescent plants, spores floating, and magical fungal atmosphere")
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
            
            // Ensure Mirage model is selected for environment transformation
            if (webRtcConnection != null)
            {
                webRtcConnection.SetModelChoice(false); // false = Mirage model
            }
            
            currentBiomeIndex = 0;
            UpdateDisplay();
            
            if (instructionsText != null)
            {
                instructionsText.text = "Joystick UP/DOWN: Browse biomes and locations\n" +
                                       "Right Trigger: Apply transformation\n" +
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
                    currentBiomeIndex--;
                    if (currentBiomeIndex < 0)
                        currentBiomeIndex = biomes.Count - 1;
                    
                    UpdateDisplay();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
                else if (joystick.y < -0.5f) // Down
                {
                    currentBiomeIndex++;
                    if (currentBiomeIndex >= biomes.Count)
                        currentBiomeIndex = 0;
                    
                    UpdateDisplay();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
            }
            
            // Right Trigger = Apply biome transformation
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                ApplyBiomeTransformation();
            }
        }
        
        private void UpdateDisplay()
        {
            if (currentBiomeIndex < biomes.Count)
            {
                var biome = biomes[currentBiomeIndex];
                
                if (selectedBiomeText != null)
                {
                    selectedBiomeText.text = $"{biome.name}\n({currentBiomeIndex + 1}/{biomes.Count})";
                }
                
                if (descriptionText != null)
                {
                    descriptionText.text = biome.description;
                }
            }
        }
        
        private void ApplyBiomeTransformation()
        {
            if (webRtcConnection == null)
            {
                Debug.LogError("BiomeTransformFeature: WebRTCConnection not found!");
                return;
            }
            
            if (currentBiomeIndex >= biomes.Count)
            {
                Debug.LogError("BiomeTransformFeature: Invalid biome index!");
                return;
            }
            
            var selectedBiome = biomes[currentBiomeIndex];
            string prompt = selectedBiome.prompt;
            
            Debug.Log($"Applying biome transformation: {selectedBiome.name} - Prompt: {prompt}");
            
            // Send the prompt to Decart AI (Mirage model)
            webRtcConnection.SendCustomPrompt(prompt);
            
            // Provide visual feedback
            if (descriptionText != null)
            {
                descriptionText.text = $"{selectedBiome.description}\n<color=green>✓ Transformation applied!</color>";
            }
        }
    }
}

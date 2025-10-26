using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Biome/Country Transformation feature - allows users to view their room
    /// as if it was in different countries or environmental biomes.
    /// </summary>
    public class BiomeController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        [SerializeField] private TMP_Text selectedBiomeText;
        [SerializeField] private TMP_Text descriptionText;
        
        [Header("Biome Menu")]
        [SerializeField] private List<Button> biomeButtons = new List<Button>();
        
        [Header("Navigation")]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color highlightedColor = Color.green;
        
        private int currentBiomeIndex = 0;
        private float lastNavigationTime = 0f;
        private const float navigationCooldown = 0.25f;
        
        // Biomes and country transformations with detailed prompts
        private static readonly Dictionary<string, string> biomeOptions = new Dictionary<string, string>
        {
            // Natural Biomes
            { "Tropical Rainforest", "Transform the environment to tropical rainforest with lush green vegetation, hanging vines, exotic flowers, humid atmosphere, dense foliage, vibrant wildlife sounds" },
            { "Desert Oasis", "Transform to desert oasis with sand dunes, palm trees, clear blue water pool, warm golden sunlight, Arabian architecture, Middle Eastern atmosphere" },
            { "Arctic Tundra", "Transform to arctic tundra with snow-covered ground, ice formations, northern lights in sky, frozen landscape, polar atmosphere, crisp cold air" },
            { "Bamboo Forest", "Transform to serene bamboo forest with tall green bamboo stalks, filtered sunlight, peaceful Zen atmosphere, Japanese garden elements" },
            { "Coral Reef", "Transform to underwater coral reef with colorful corals, tropical fish swimming, blue water, aquatic plants, ocean floor aesthetics" },
            { "Mountain Peak", "Transform to mountain peak with rocky terrain, snow caps, breathtaking vistas, alpine atmosphere, crisp mountain air, dramatic landscapes" },
            { "Savanna", "Transform to African savanna with golden grasslands, acacia trees, warm sunset lighting, wildlife atmosphere, open plains" },
            { "Temperate Forest", "Transform to temperate forest with oak and maple trees, autumn leaves, forest floor, dappled sunlight, woodland atmosphere" },
            { "Cave System", "Transform to mystical cave system with stalactites, glowing crystals, underground pools, mysterious lighting, spelunking atmosphere" },
            { "Volcanic Landscape", "Transform to volcanic landscape with lava flows, obsidian rocks, steaming vents, dramatic red glow, primal atmosphere" },
            
            // Country/Cultural Transformations
            { "Japan", "Transform to traditional Japanese interior with tatami mats, shoji screens, paper lanterns, zen aesthetics, cherry blossom motifs, minimalist design" },
            { "Paris, France", "Transform to Parisian apartment with ornate moldings, crystal chandeliers, French windows, elegant furniture, romantic atmosphere, European sophistication" },
            { "Morocco", "Transform to Moroccan riad with colorful mosaic tiles, arched doorways, hanging lanterns, vibrant textiles, intricate patterns, Middle Eastern design" },
            { "Bali, Indonesia", "Transform to Balinese villa with teak wood, tropical plants, stone carvings, open-air design, peaceful island atmosphere" },
            { "Santorini, Greece", "Transform to Santorini house with white-washed walls, blue domed roofs, Mediterranean sea views, Greek island aesthetics" },
            { "Dubai, UAE", "Transform to luxury Dubai penthouse with modern Arabic design, gold accents, floor-to-ceiling windows, opulent furnishings, futuristic elegance" },
            { "Iceland", "Transform to Icelandic interior with modern Nordic design, natural materials, geothermal elements, minimalist aesthetics, aurora-inspired lighting" },
            { "India", "Transform to traditional Indian palace with ornate decorations, vibrant colors, intricate carvings, silk fabrics, Mughal architecture elements" },
            { "Brazil", "Transform to Brazilian beach house with tropical colors, hammocks, ocean views, carnival vibes, festive atmosphere" },
            { "Switzerland", "Transform to Swiss chalet with wooden interiors, Alpine decorations, cozy fireplace, mountain lodge atmosphere" },
            { "Egypt", "Transform to Ancient Egyptian palace with hieroglyphics, golden decorations, pharaonic statues, sandstone walls, desert kingdom atmosphere" },
            { "China", "Transform to traditional Chinese courtyard with red lanterns, dragon motifs, bamboo elements, imperial palace aesthetics" },
            { "Mexico", "Transform to colorful Mexican hacienda with vibrant tiles, terracotta, papel picado decorations, festive atmosphere" },
            { "Australia Outback", "Transform to Australian outback with rustic interior, Aboriginal art, natural wood, earthy tones, wilderness atmosphere" },
            { "Venice, Italy", "Transform to Venetian palazzo with marble floors, ornate mirrors, Renaissance art, canal-side atmosphere, Italian luxury" },
            
            // Fantasy Biomes
            { "Enchanted Forest", "Transform to enchanted magical forest with glowing mushrooms, fairy lights, mystical fog, magical creatures atmosphere, fantasy woodland" },
            { "Crystal Cave", "Transform to crystal cave with glowing gemstones, prismatic reflections, magical lighting, fantasy underground palace" },
            { "Floating Islands", "Transform to floating sky islands with waterfalls cascading down, clouds below, ethereal atmosphere, fantasy sky realm" },
            { "Alien Planet", "Transform to alien planet surface with exotic flora, strange rock formations, dual suns, otherworldly atmosphere, sci-fi environment" },
            { "Steampunk City", "Transform to steampunk interior with brass gears, Victorian machinery, copper pipes, industrial elegance, retro-futuristic design" },
            
            // Seasonal Transformations
            { "Winter Wonderland", "Transform to winter wonderland with falling snow, ice crystals, warm fireplaces, cozy holiday atmosphere, festive decorations" },
            { "Spring Garden", "Transform to spring garden with blooming flowers, fresh green leaves, butterflies, cherry blossoms, renewal atmosphere" },
            { "Autumn Harvest", "Transform to autumn harvest scene with golden leaves, pumpkins, warm colors, cozy fall atmosphere, harvest decorations" },
            { "Summer Beach", "Transform to tropical beach with white sand, ocean waves, palm trees, sunny sky, vacation paradise atmosphere" },
            
            // Urban Transformations
            { "Tokyo Neon District", "Transform to Tokyo neon district with colorful signs, holographic displays, cyberpunk aesthetics, bustling city atmosphere" },
            { "New York Loft", "Transform to New York industrial loft with exposed brick, large windows, city views, urban contemporary style" },
            { "London Victorian", "Transform to Victorian London interior with gas lamps, ornate furniture, British elegance, period architecture" },
            { "Hong Kong High-Rise", "Transform to Hong Kong high-rise with modern Asian design, city skyline views, sleek contemporary aesthetics" },
        };
        
        private List<string> biomeKeys = new List<string>();
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            // Convert dictionary keys to list for indexing
            biomeKeys = new List<string>(biomeOptions.Keys);
            
            if (descriptionText != null)
            {
                descriptionText.text = "Select a biome or country to transform your environment";
            }
            
            UpdateBiomeHighlight();
            UpdateSelectedBiomeDisplay();
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
            
            // Right trigger - Apply selected biome
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                ApplySelectedBiome();
            }
        }
        
        private void NavigateNext()
        {
            currentBiomeIndex++;
            if (currentBiomeIndex >= biomeKeys.Count)
            {
                currentBiomeIndex = 0;
            }
            UpdateBiomeHighlight();
            UpdateSelectedBiomeDisplay();
        }
        
        private void NavigatePrevious()
        {
            currentBiomeIndex--;
            if (currentBiomeIndex < 0)
            {
                currentBiomeIndex = biomeKeys.Count - 1;
            }
            UpdateBiomeHighlight();
            UpdateSelectedBiomeDisplay();
        }
        
        private void UpdateBiomeHighlight()
        {
            // Update button colors if we have button references
            for (int i = 0; i < biomeButtons.Count && i < biomeKeys.Count; i++)
            {
                if (biomeButtons[i] != null)
                {
                    var text = biomeButtons[i].GetComponentInChildren<TMP_Text>();
                    if (text != null)
                    {
                        text.color = (i == currentBiomeIndex) ? highlightedColor : normalColor;
                    }
                }
            }
        }
        
        private void UpdateSelectedBiomeDisplay()
        {
            if (selectedBiomeText != null && currentBiomeIndex >= 0 && currentBiomeIndex < biomeKeys.Count)
            {
                selectedBiomeText.text = $"Selected: {biomeKeys[currentBiomeIndex]}";
            }
        }
        
        private void ApplySelectedBiome()
        {
            if (currentBiomeIndex >= 0 && currentBiomeIndex < biomeKeys.Count)
            {
                string biomeName = biomeKeys[currentBiomeIndex];
                string prompt = biomeOptions[biomeName];
                
                if (webRtcConnection != null)
                {
                    webRtcConnection.SendCustomPrompt(prompt);
                    Debug.Log($"BiomeController: Applied biome {biomeName}: {prompt}");
                }
            }
        }
        
        public void SelectBiome(string biomeName)
        {
            int index = biomeKeys.IndexOf(biomeName);
            if (index >= 0)
            {
                currentBiomeIndex = index;
                UpdateBiomeHighlight();
                UpdateSelectedBiomeDisplay();
            }
        }
        
        public void ApplyBiome(string biomeName)
        {
            if (biomeOptions.TryGetValue(biomeName, out string prompt))
            {
                if (webRtcConnection != null)
                {
                    webRtcConnection.SendCustomPrompt(prompt);
                    Debug.Log($"BiomeController: Applied biome {biomeName}");
                }
            }
        }
    }
}

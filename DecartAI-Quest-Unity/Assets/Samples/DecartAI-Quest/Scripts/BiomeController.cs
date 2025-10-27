using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls biome/country environment transformation feature
    /// Allows user to view their room as if it was in different countries or biomes
    /// </summary>
    public class BiomeController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private List<TMP_Text> biomeMenuItems;
        [SerializeField] private TMP_Text selectedBiomeText;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color selectedColor = Color.yellow;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Navigation Settings")]
        [SerializeField] private float joystickDeadzone = 0.5f;
        [SerializeField] private float navigationCooldown = 0.3f;
        
        private int currentBiomeIndex = 0;
        private float lastNavigationTime = 0f;
        private Dictionary<string, string> biomePrompts;
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            InitializeBiomePrompts();
            UpdateBiomeHighlight();
        }
        
        private void InitializeBiomePrompts()
        {
            // Define biome and country transformation prompts optimized for Decart Mirage model
            biomePrompts = new Dictionary<string, string>
            {
                // Natural Biomes
                { "Arctic Tundra", "Transform the environment into Arctic tundra with snow-covered landscape, ice formations, frozen terrain, aurora borealis in sky, and extreme cold atmosphere" },
                { "Tropical Rainforest", "Transform the environment into lush tropical rainforest with dense jungle vegetation, exotic plants, vibrant flowers, hanging vines, and humid misty atmosphere" },
                { "Desert Oasis", "Transform the environment into desert oasis with golden sand dunes, palm trees, clear blue water, hot sun, and arid landscape" },
                { "Mountain Peak", "Transform the environment into high mountain peak with rocky cliffs, snow caps, thin clouds, distant valleys, and alpine atmosphere" },
                { "Underwater Reef", "Transform the environment into underwater coral reef with colorful corals, tropical fish, blue water, rays of sunlight filtering through, and aquatic atmosphere" },
                { "Savanna Plains", "Transform the environment into African savanna with golden grasslands, acacia trees, warm sunlight, and open plains stretching to horizon" },
                { "Bamboo Forest", "Transform the environment into serene bamboo forest with tall bamboo stalks, filtered green light, peaceful atmosphere, and Asian aesthetics" },
                { "Cherry Blossom Grove", "Transform the environment into Japanese cherry blossom grove with pink petals falling, blooming sakura trees, soft lighting, and spring atmosphere" },
                
                // Countries - Asia
                { "Japan", "Transform the environment into traditional Japanese setting with tatami floors, shoji screens, paper lanterns, zen garden elements, and minimalist aesthetics" },
                { "China", "Transform the environment into classical Chinese palace with red pillars, golden dragons, ornate decorations, lanterns, and imperial architecture" },
                { "India", "Transform the environment into vibrant Indian palace with colorful textiles, intricate patterns, ornate arches, carved pillars, and rich decorations" },
                { "Thailand", "Transform the environment into Thai temple setting with golden spires, Buddhist architecture, tropical plants, and Southeast Asian aesthetics" },
                { "Dubai (UAE)", "Transform the environment into luxury Dubai setting with modern opulence, gold accents, marble floors, futuristic design, and Middle Eastern grandeur" },
                { "Morocco", "Transform the environment into Moroccan riad with colorful mosaics, ornate tiles, arched doorways, hanging lanterns, and North African design" },
                
                // Countries - Europe
                { "France (Paris)", "Transform the environment into Parisian cafe with art nouveau details, wrought iron, elegant furniture, French windows, and romantic lighting" },
                { "Italy (Tuscany)", "Transform the environment into Italian villa with terracotta tiles, stone walls, rustic wood beams, vineyard views, and Mediterranean warmth" },
                { "Greece", "Transform the environment into Greek island setting with white-washed walls, blue domes, marble columns, ocean views, and Mediterranean sunlight" },
                { "England", "Transform the environment into English manor with dark wood paneling, leather furniture, fireplace, portraits, and Victorian elegance" },
                { "Russia", "Transform the environment into Russian palace with ornate gold decorations, baroque architecture, rich fabrics, and imperial grandeur" },
                { "Spain", "Transform the environment into Spanish hacienda with terracotta floors, wrought iron details, colorful tiles, arched windows, and warm ambiance" },
                { "Netherlands", "Transform the environment into Dutch interior with windmill views, canal-side windows, wooden clogs, tulips, and cozy atmosphere" },
                { "Norway", "Transform the environment into Norwegian fjord cabin with wooden interior, mountain views, Nordic design, and Scandinavian minimalism" },
                
                // Countries - Americas
                { "USA (New York)", "Transform the environment into New York loft with exposed brick, industrial pipes, large windows, city views, and urban modern design" },
                { "USA (Wild West)", "Transform the environment into Wild West saloon with wooden floors, swinging doors, wanted posters, and frontier atmosphere" },
                { "Mexico", "Transform the environment into Mexican hacienda with colorful tiles, terracotta pots, vibrant textiles, and warm Latin American style" },
                { "Brazil", "Transform the environment into Brazilian beach house with tropical colors, carnival decorations, bamboo furniture, and festive atmosphere" },
                { "Canada", "Transform the environment into Canadian log cabin with rustic wood, stone fireplace, flannel patterns, and cozy wilderness retreat" },
                { "Caribbean", "Transform the environment into Caribbean beach resort with bamboo furniture, tropical drinks, ocean breeze, and island paradise vibes" },
                
                // Countries - Africa & Oceania
                { "Egypt", "Transform the environment into ancient Egyptian temple with hieroglyphics, golden decorations, pharaoh statues, and archaeological mystique" },
                { "South Africa", "Transform the environment into African safari lodge with natural materials, animal prints, tribal art, and wildlife adventure aesthetics" },
                { "Australia", "Transform the environment into Australian outback station with corrugated metal, eucalyptus, aboriginal art, and rugged frontier style" },
                { "New Zealand", "Transform the environment into New Zealand Hobbiton with round doors, lush gardens, cozy interiors, and Middle Earth fantasy" },
                
                // Fantasy Biomes
                { "Enchanted Forest", "Transform the environment into magical enchanted forest with glowing mushrooms, fairy lights, mystical fog, magical creatures, and fantasy atmosphere" },
                { "Crystal Cave", "Transform the environment into crystalline cave with giant glowing crystals, luminescent minerals, underground lake, and magical geology" },
                { "Floating Islands", "Transform the environment into floating sky islands with waterfalls cascading into clouds, levitating rocks, and aerial fantasy landscape" },
                { "Volcanic Landscape", "Transform the environment into volcanic terrain with lava flows, glowing magma, obsidian rocks, ash in air, and dramatic lighting" },
                { "Ice Castle", "Transform the environment into frozen ice castle with crystalline ice walls, icicle chandeliers, snow sculptures, and winter wonderland" },
                { "Autumn Forest", "Transform the environment into autumn forest with golden leaves, warm orange colors, falling foliage, and cozy fall atmosphere" },
                { "Spring Meadow", "Transform the environment into spring meadow with wildflowers, fresh grass, butterflies, gentle breeze, and vibrant spring colors" },
                { "Night Garden", "Transform the environment into moonlit garden with glowing flowers, fireflies, night blooms, silver moonlight, and nocturnal magic" },
                
                // Extreme Environments
                { "Space Station", "Transform the environment into futuristic space station with zero gravity effects, Earth view through windows, high-tech panels, and sci-fi design" },
                { "Deep Ocean", "Transform the environment into deep ocean research base with porthole windows, bioluminescent creatures, submarine atmosphere, and underwater lighting" },
                { "Cloud City", "Transform the environment into city in the clouds with floating platforms, sky bridges, endless clouds below, and aerial architecture" },
                { "Lava Cave", "Transform the environment into underground lava cave with glowing molten rock, heat shimmer, volcanic tunnels, and dramatic fire lighting" }
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
            if (biomeMenuItems == null || biomeMenuItems.Count == 0) return;
            
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            // Up navigation
            if (joystickInput.y > joystickDeadzone)
            {
                currentBiomeIndex--;
                if (currentBiomeIndex < 0)
                {
                    currentBiomeIndex = biomeMenuItems.Count - 1;
                }
                UpdateBiomeHighlight();
                lastNavigationTime = Time.time;
            }
            // Down navigation
            else if (joystickInput.y < -joystickDeadzone)
            {
                currentBiomeIndex++;
                if (currentBiomeIndex >= biomeMenuItems.Count)
                {
                    currentBiomeIndex = 0;
                }
                UpdateBiomeHighlight();
                lastNavigationTime = Time.time;
            }
        }
        
        private void HandleSelection()
        {
            // Right trigger to select biome
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyBiome();
            }
        }
        
        private void UpdateBiomeHighlight()
        {
            if (biomeMenuItems == null) return;
            
            for (int i = 0; i < biomeMenuItems.Count; i++)
            {
                if (biomeMenuItems[i] != null)
                {
                    biomeMenuItems[i].color = (i == currentBiomeIndex) ? selectedColor : normalColor;
                }
            }
            
            if (selectedBiomeText != null && biomeMenuItems.Count > 0 && currentBiomeIndex < biomeMenuItems.Count)
            {
                string biomeName = biomeMenuItems[currentBiomeIndex].text;
                selectedBiomeText.text = $"Selected: {biomeName}";
            }
        }
        
        private void ApplyBiome()
        {
            if (webRtcConnection == null || biomeMenuItems == null || biomeMenuItems.Count == 0) return;
            if (currentBiomeIndex < 0 || currentBiomeIndex >= biomeMenuItems.Count) return;
            
            string biomeName = biomeMenuItems[currentBiomeIndex].text;
            
            if (biomePrompts.ContainsKey(biomeName))
            {
                string prompt = biomePrompts[biomeName];
                webRtcConnection.SendCustomPrompt(prompt);
                Debug.Log($"Biome Transform: Applied {biomeName}");
                
                if (selectedBiomeText != null)
                {
                    selectedBiomeText.text = $"Applying: {biomeName}";
                }
            }
            else
            {
                Debug.LogWarning($"Biome Transform: No prompt found for {biomeName}");
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Handles biome and location transformation feature using Mirage model
    /// Allows users to see their environment as different locations or biomes
    /// </summary>
    public class BiomeTransformController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Transform biomeListContainer;
        [SerializeField] private GameObject biomeItemPrefab;
        [SerializeField] private TMP_Text selectedBiomeText;
        [SerializeField] private TMP_Text categoryText;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private List<BiomeOption> biomeOptions = new List<BiomeOption>();
        private int selectedIndex = 0;
        private float joystickCooldown = 0f;
        private const float JOYSTICK_COOLDOWN_TIME = 0.3f;
        
        private class BiomeOption
        {
            public string Name;
            public string Category;
            public string Prompt;
            public GameObject UIElement;
        }
        
        private void OnEnable()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            InitializeBiomeOptions();
            UpdateDisplay();
        }
        
        private void InitializeBiomeOptions()
        {
            biomeOptions.Clear();
            
            // Natural Biomes
            AddBiomeOption("Tropical Rainforest", "Natural Biome",
                "Transform to tropical rainforest environment, lush green vegetation, tall palm trees, exotic plants, humid atmosphere, vibrant flowers, jungle vines, colorful birds, tropical paradise");
            
            AddBiomeOption("Arctic Tundra", "Natural Biome",
                "Transform to arctic tundra environment, snow-covered landscape, ice formations, frozen ground, polar atmosphere, white and blue color palette, northern lights in sky, cold climate");
            
            AddBiomeOption("Desert Oasis", "Natural Biome",
                "Transform to desert environment, golden sand dunes, palm trees around water, arid landscape, bright sunshine, warm earth tones, cacti, desert plants, dry heat atmosphere");
            
            AddBiomeOption("Autumn Forest", "Natural Biome",
                "Transform to autumn forest setting, orange and red maple leaves, golden foliage, fallen leaves on ground, crisp air, warm autumn colors, peaceful woodland atmosphere");
            
            AddBiomeOption("Cherry Blossom Garden", "Natural Biome",
                "Transform to Japanese cherry blossom garden, pink sakura trees in full bloom, petals falling gently, traditional stone paths, peaceful zen atmosphere, spring beauty");
            
            AddBiomeOption("Underwater Reef", "Natural Biome",
                "Transform to underwater coral reef, colorful coral formations, tropical fish swimming, blue water, aquatic plants, sunlight filtering through water, marine life");
            
            // World Locations - Cities
            AddBiomeOption("Tokyo Streets", "City",
                "Transform to Tokyo Japan cityscape, neon signs in Japanese, crowded urban streets, modern skyscrapers, cherry blossom trees, traditional architecture mixed with modern, vibrant city life");
            
            AddBiomeOption("Paris France", "City",
                "Transform to Parisian setting, Eiffel Tower visible, classic French architecture, cobblestone streets, outdoor cafes, romantic atmosphere, elegant European style");
            
            AddBiomeOption("New York City", "City",
                "Transform to New York cityscape, tall skyscrapers, yellow taxis, busy streets, urban energy, modern American city, bright lights, metropolitan atmosphere");
            
            AddBiomeOption("Venice Italy", "City",
                "Transform to Venice canals, gondolas in waterways, Italian architecture, bridges over water, romantic European setting, historic buildings, Mediterranean atmosphere");
            
            AddBiomeOption("Dubai Modern", "City",
                "Transform to futuristic Dubai setting, ultra-modern skyscrapers, luxurious architecture, gold accents, desert backdrop, opulent design, contemporary Middle Eastern style");
            
            // Historical & Cultural
            AddBiomeOption("Ancient Egypt", "Historical",
                "Transform to ancient Egyptian setting, pyramids visible, hieroglyphics on walls, golden decorations, sphinxes, desert sand, pharaoh aesthetic, ancient civilization");
            
            AddBiomeOption("Medieval Castle", "Historical",
                "Transform to medieval castle interior, stone walls, tapestries, torches on walls, suits of armor, medieval furniture, Gothic architecture, historical European setting");
            
            AddBiomeOption("Ancient Greece", "Historical",
                "Transform to ancient Greek setting, marble columns, classical architecture, Mediterranean atmosphere, togas and robes, olive trees, historical antiquity");
            
            AddBiomeOption("Wild West Town", "Historical",
                "Transform to Old West frontier town, wooden buildings, saloon doors, dusty streets, cowboy aesthetic, 1800s American frontier, rustic Western style");
            
            // Fantasy & Mystical
            AddBiomeOption("Enchanted Forest", "Fantasy",
                "Transform to magical enchanted forest, glowing mushrooms, mystical fog, ethereal lighting, fairy lights, magical atmosphere, fantasy woodland, bioluminescent plants");
            
            AddBiomeOption("Crystal Cave", "Fantasy",
                "Transform to crystal cave environment, large glowing crystals, gemstone formations, magical underground cavern, colorful mineral deposits, mystical lighting");
            
            AddBiomeOption("Floating Islands", "Fantasy",
                "Transform to floating sky islands, clouds below, waterfalls cascading into void, magical atmosphere, floating rocks, ethereal sky realm, fantasy landscape");
            
            AddBiomeOption("Dragon's Lair", "Fantasy",
                "Transform to dragon's treasure lair, piles of gold coins, jewels and treasures, rocky cave walls, dramatic lighting, fantasy dungeon, mysterious atmosphere");
            
            // Seasonal
            AddBiomeOption("Winter Wonderland", "Seasonal",
                "Transform to winter wonderland, heavy snowfall, snow-covered trees, icicles, frost on windows, cozy warm lighting, holiday atmosphere, winter magic");
            
            AddBiomeOption("Spring Meadow", "Seasonal",
                "Transform to spring meadow, colorful wildflowers, green grass, butterflies, fresh growth, pastel colors, renewal atmosphere, gentle spring breeze");
            
            AddBiomeOption("Summer Beach", "Seasonal",
                "Transform to tropical beach, white sand, turquoise water, palm trees, bright sunshine, beach umbrellas, summer paradise, coastal atmosphere");
            
            // Space & Sci-Fi
            AddBiomeOption("Space Station", "Sci-Fi",
                "Transform to futuristic space station, metallic walls, holographic displays, windows showing stars and planets, high-tech controls, zero gravity aesthetic, sci-fi interior");
            
            AddBiomeOption("Alien Planet", "Sci-Fi",
                "Transform to alien planet surface, exotic alien plants, strange rock formations, multiple moons in sky, otherworldly colors, science fiction landscape");
            
            AddBiomeOption("Cyberpunk City", "Sci-Fi",
                "Transform to cyberpunk cityscape, neon lights everywhere, holographic advertisements, rain-slicked streets, futuristic technology, dark dystopian atmosphere, high-tech low-life");
        }
        
        private void AddBiomeOption(string name, string category, string prompt)
        {
            biomeOptions.Add(new BiomeOption
            {
                Name = name,
                Category = category,
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
            
            // Right trigger to apply selected biome
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyBiome();
            }
        }
        
        private void NavigateUp()
        {
            selectedIndex--;
            if (selectedIndex < 0)
                selectedIndex = biomeOptions.Count - 1;
            UpdateDisplay();
        }
        
        private void NavigateDown()
        {
            selectedIndex++;
            if (selectedIndex >= biomeOptions.Count)
                selectedIndex = 0;
            UpdateDisplay();
        }
        
        private void UpdateDisplay()
        {
            // Clear existing UI
            foreach (Transform child in biomeListContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Create UI elements
            for (int i = 0; i < biomeOptions.Count; i++)
            {
                GameObject itemObj = Instantiate(biomeItemPrefab, biomeListContainer);
                TMP_Text itemText = itemObj.GetComponentInChildren<TMP_Text>();
                
                if (itemText != null)
                {
                    itemText.text = $"{biomeOptions[i].Name} ({biomeOptions[i].Category})";
                    
                    if (i == selectedIndex)
                    {
                        itemText.color = Color.cyan;
                        itemText.fontSize = 26;
                    }
                    else
                    {
                        itemText.color = Color.white;
                        itemText.fontSize = 22;
                    }
                }
                
                biomeOptions[i].UIElement = itemObj;
            }
            
            if (selectedBiomeText != null)
            {
                selectedBiomeText.text = $"Selected: {biomeOptions[selectedIndex].Name}";
            }
            
            if (categoryText != null)
            {
                categoryText.text = $"Category: {biomeOptions[selectedIndex].Category}";
            }
        }
        
        private void ApplyBiome()
        {
            if (webRtcConnection == null || selectedIndex >= biomeOptions.Count) return;
            
            string prompt = biomeOptions[selectedIndex].Prompt;
            webRtcConnection.SendCustomPrompt(prompt);
            
            Debug.Log($"Biome Transform: Applied {biomeOptions[selectedIndex].Name}");
        }
    }
}

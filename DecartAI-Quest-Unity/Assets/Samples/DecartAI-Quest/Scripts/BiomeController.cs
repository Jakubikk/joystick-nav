using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Biome/Country feature - transforms environment to different locations and climates
    /// Uses Decart Mirage model for environment transformation
    /// </summary>
    public class BiomeController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject biomePanel;
        [SerializeField] private Transform biomeListContainer;
        [SerializeField] private GameObject biomeItemPrefab;
        [SerializeField] private TMP_Text categoryText;
        [SerializeField] private TMP_Text instructionText;
        
        private WebRTCConnection webRtcConnection;
        private int selectedIndex = 0;
        private List<BiomeOption> biomeOptions = new List<BiomeOption>();
        
        private class BiomeOption
        {
            public string name;
            public string description;
            public string prompt;
            public GameObject gameObject;
        }
        
        private void Awake()
        {
            InitializeBiomeOptions();
        }
        
        private void InitializeBiomeOptions()
        {
            biomeOptions.Clear();
            
            // Natural Biomes
            biomeOptions.Add(new BiomeOption
            {
                name = "Tropical Rainforest",
                description = "Lush jungle with exotic plants",
                prompt = "Transform environment to tropical rainforest, dense jungle vegetation, palm trees, exotic plants, humid atmosphere, vibrant green colors, tropical flowers, jungle vines"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Arctic Tundra",
                description = "Frozen landscape with snow",
                prompt = "Transform environment to arctic tundra, snow-covered ground, ice formations, frozen landscape, cold blue tones, snowdrifts, polar atmosphere, icy surfaces"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Desert Oasis",
                description = "Sandy dunes with palm trees",
                prompt = "Transform environment to desert oasis, sand dunes, palm trees, cacti, warm golden light, desert heat shimmer, sandy terrain, Middle Eastern aesthetic"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Mountain Forest",
                description = "Pine forest with mountain peaks",
                prompt = "Transform environment to mountain forest, tall pine trees, rocky mountain peaks, alpine atmosphere, evergreen forest, mountain landscape, crisp air aesthetic"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Ocean Underwater",
                description = "Beneath the sea with coral",
                prompt = "Transform environment to underwater ocean scene, coral reefs, swimming fish, blue water atmosphere, underwater lighting, aquatic plants, oceanic environment"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Cherry Blossom Park",
                description = "Japanese spring garden",
                prompt = "Transform environment to cherry blossom park, pink sakura trees, falling petals, Japanese garden aesthetic, peaceful spring atmosphere, traditional elements"
            });
            
            // Country/Cultural Settings
            biomeOptions.Add(new BiomeOption
            {
                name = "Paris, France",
                description = "Romantic Parisian streets",
                prompt = "Transform environment to Parisian street, Eiffel Tower visible, French architecture, café tables, romantic atmosphere, French aesthetic, European charm"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Tokyo, Japan",
                description = "Neon-lit Japanese cityscape",
                prompt = "Transform environment to Tokyo street, neon signs with Japanese characters, modern architecture, vending machines, Japanese aesthetics, urban Tokyo atmosphere"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "New York City, USA",
                description = "Bustling Manhattan streets",
                prompt = "Transform environment to New York City street, yellow cabs, skyscrapers, Times Square atmosphere, American city aesthetic, urban Manhattan style"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Venice, Italy",
                description = "Romantic canals and gondolas",
                prompt = "Transform environment to Venice Italy, water canals, gondolas, Italian Renaissance architecture, bridges, Venetian aesthetic, Mediterranean atmosphere"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Cairo, Egypt",
                description = "Ancient pyramids and desert",
                prompt = "Transform environment to Cairo Egypt, pyramids visible, ancient Egyptian architecture, desert sand, hieroglyphics, Middle Eastern aesthetic, ancient monuments"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "London, England",
                description = "Classic British cityscape",
                prompt = "Transform environment to London England, red telephone boxes, double-decker buses, Big Ben visible, British architecture, rainy atmosphere, English aesthetic"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Amsterdam, Netherlands",
                description = "Dutch canals and bicycles",
                prompt = "Transform environment to Amsterdam, Dutch canal houses, bicycles everywhere, narrow streets, tulips, Netherlands architecture, European canal city aesthetic"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Dubai, UAE",
                description = "Futuristic luxury architecture",
                prompt = "Transform environment to Dubai, modern futuristic architecture, luxury buildings, desert heat, Middle Eastern modern aesthetic, opulent design, Burj Khalifa style"
            });
            
            // Fantasy Environments
            biomeOptions.Add(new BiomeOption
            {
                name = "Enchanted Forest",
                description = "Magical woodland with mystical creatures",
                prompt = "Transform environment to enchanted magical forest, glowing mushrooms, mystical atmosphere, fairy lights, magical creatures, ethereal lighting, fantasy woodland"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Volcanic Landscape",
                description = "Lava flows and volcanic rock",
                prompt = "Transform environment to volcanic landscape, flowing lava, volcanic rock formations, orange glow, smoky atmosphere, active volcano environment, molten lava"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Alien Planet",
                description = "Extraterrestrial landscape",
                prompt = "Transform environment to alien planet, strange alien vegetation, unusual rock formations, otherworldly colors, sci-fi atmosphere, extraterrestrial landscape, alien world"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Crystal Cave",
                description = "Glowing crystalline cavern",
                prompt = "Transform environment to crystal cave, large glowing crystals, gemstone formations, underground cavern, magical lighting, crystalline structures, mystical cave"
            });
            
            // Seasonal Variations
            biomeOptions.Add(new BiomeOption
            {
                name = "Autumn Forest",
                description = "Fall colors and falling leaves",
                prompt = "Transform environment to autumn forest, orange and red leaves, falling foliage, autumn colors, crisp fall atmosphere, seasonal transformation, golden hour"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Winter Wonderland",
                description = "Snowy Christmas atmosphere",
                prompt = "Transform environment to winter wonderland, heavy snow, Christmas lights, icicles, festive atmosphere, winter holiday decorations, snowy landscape"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Spring Meadow",
                description = "Colorful flowers and greenery",
                prompt = "Transform environment to spring meadow, colorful wildflowers, fresh green grass, blooming flowers, spring atmosphere, vibrant colors, new growth"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Summer Beach",
                description = "Sunny seaside paradise",
                prompt = "Transform environment to summer beach, sandy shore, ocean waves, palm trees, beach umbrellas, sunny day, tropical beach paradise, coastal atmosphere"
            });
            
            // Historical Settings
            biomeOptions.Add(new BiomeOption
            {
                name = "Ancient Greece",
                description = "Classical marble temples",
                prompt = "Transform environment to ancient Greece, marble columns, Greek temples, classical architecture, Mediterranean landscape, ancient Greek aesthetic, Parthenon style"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Medieval Castle",
                description = "Stone fortress and courtyard",
                prompt = "Transform environment to medieval castle courtyard, stone walls, castle towers, medieval architecture, banners and flags, fortress aesthetic, Middle Ages style"
            });
            
            biomeOptions.Add(new BiomeOption
            {
                name = "Wild West Town",
                description = "Frontier saloon and dusty streets",
                prompt = "Transform environment to Wild West town, wooden saloon buildings, dusty dirt road, cowboy aesthetic, Western frontier style, 1800s American West"
            });
        }
        
        public void Activate()
        {
            if (biomePanel != null)
            {
                biomePanel.SetActive(true);
            }
            
            MenuManager menuManager = FindFirstObjectByType<MenuManager>();
            if (menuManager != null)
            {
                webRtcConnection = menuManager.GetWebRTCConnection();
            }
            
            CreateBiomeItems();
            UpdateDisplay();
            
            if (instructionText != null)
            {
                instructionText.text = "Joystick: Navigate | Right Trigger: Transform Environment";
            }
        }
        
        private void OnDisable()
        {
            if (biomePanel != null)
            {
                biomePanel.SetActive(false);
            }
        }
        
        private void CreateBiomeItems()
        {
            if (biomeListContainer == null) return;
            
            // Clear existing items
            foreach (Transform child in biomeListContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Create UI elements for each biome option
            for (int i = 0; i < biomeOptions.Count; i++)
            {
                if (biomeItemPrefab != null)
                {
                    GameObject itemObj = Instantiate(biomeItemPrefab, biomeListContainer);
                    biomeOptions[i].gameObject = itemObj;
                    
                    TMP_Text nameText = itemObj.transform.Find("Name")?.GetComponent<TMP_Text>();
                    TMP_Text descText = itemObj.transform.Find("Description")?.GetComponent<TMP_Text>();
                    
                    if (nameText != null) nameText.text = biomeOptions[i].name;
                    if (descText != null) descText.text = biomeOptions[i].description;
                }
            }
        }
        
        private void UpdateDisplay()
        {
            if (categoryText != null)
            {
                categoryText.text = "Biomes & Countries - Environment Selection";
            }
            
            for (int i = 0; i < biomeOptions.Count; i++)
            {
                if (biomeOptions[i].gameObject != null)
                {
                    Image background = biomeOptions[i].gameObject.GetComponent<Image>();
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
            if (selectedIndex < 0) selectedIndex = biomeOptions.Count - 1;
            UpdateDisplay();
        }
        
        public void NavigateDown()
        {
            selectedIndex++;
            if (selectedIndex >= biomeOptions.Count) selectedIndex = 0;
            UpdateDisplay();
        }
        
        public void Confirm()
        {
            if (selectedIndex >= 0 && selectedIndex < biomeOptions.Count)
            {
                BiomeOption selected = biomeOptions[selectedIndex];
                if (webRtcConnection != null)
                {
                    Debug.Log($"Biome Transform - {selected.name} - Prompt: {selected.prompt}");
                    webRtcConnection.SendCustomPrompt(selected.prompt);
                }
            }
        }
    }
}

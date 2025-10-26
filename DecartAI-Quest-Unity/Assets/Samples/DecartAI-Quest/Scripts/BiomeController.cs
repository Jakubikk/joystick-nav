using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Biome/Country transformation feature allowing users to view their environment
    /// as if it were in different geographic locations or natural biomes.
    /// </summary>
    public class BiomeController : MonoBehaviour
    {
        [System.Serializable]
        public class BiomeOption
        {
            public string name;
            public string prompt;
        }

        [Header("UI References")]
        [SerializeField] private TMP_Text biomeNameText;
        [SerializeField] private TMP_Text biomeDescriptionText;
        [SerializeField] private List<TMP_Text> biomeMenuTexts = new List<TMP_Text>();
        
        [Header("Navigation Colors")]
        [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.8f);
        [SerializeField] private Color selectedColor = new Color(0f, 1f, 1f, 1f);
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private List<BiomeOption> biomeOptions = new List<BiomeOption>();
        private int selectedIndex = 0;
        private float navigationCooldown = 0.2f;
        private float lastNavigationTime = 0f;

        private void Start()
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
            biomeOptions = new List<BiomeOption>
            {
                // Natural Biomes
                new BiomeOption 
                { 
                    name = "Tropical Rainforest", 
                    prompt = "Transform the environment into a lush tropical rainforest, dense jungle vegetation, vibrant green leaves, exotic plants, humid atmosphere, dappled sunlight through canopy, tropical wildlife sounds"
                },
                new BiomeOption 
                { 
                    name = "Arctic Tundra", 
                    prompt = "Transform the environment into arctic tundra, snow-covered landscape, ice and frost everywhere, cold blue lighting, frozen terrain, icicles, aurora borealis in sky, arctic wilderness"
                },
                new BiomeOption 
                { 
                    name = "Sahara Desert", 
                    prompt = "Transform the environment into Sahara desert, endless sand dunes, golden sandy landscape, hot sun, clear blue sky, desert heat shimmer, sparse desert vegetation, arid climate"
                },
                new BiomeOption 
                { 
                    name = "Amazon Jungle", 
                    prompt = "Transform the environment into Amazon jungle, dense tropical vegetation, massive trees, hanging vines, exotic flowers, misty atmosphere, rich biodiversity, jungle canopy"
                },
                new BiomeOption 
                { 
                    name = "Coral Reef", 
                    prompt = "Transform the environment into underwater coral reef, colorful coral formations, tropical fish swimming, aquatic plants, blue water, sunlight filtering through water, ocean atmosphere"
                },
                new BiomeOption 
                { 
                    name = "Mountain Peak", 
                    prompt = "Transform the environment into mountain peak setting, rocky mountain terrain, snow-capped peaks, thin mountain air, dramatic cliffs, alpine vegetation, panoramic mountain views"
                },
                new BiomeOption 
                { 
                    name = "Cherry Blossom Grove", 
                    prompt = "Transform the environment into Japanese cherry blossom grove, beautiful sakura trees in full bloom, pink petals falling, serene atmosphere, traditional Japanese aesthetic, spring season"
                },
                
                // Countries/Locations
                new BiomeOption 
                { 
                    name = "Tokyo, Japan", 
                    prompt = "Transform the environment into Tokyo cityscape, neon signs in Japanese, modern skyscrapers, traditional elements mixed with futuristic tech, busy urban atmosphere, cherry blossom accents"
                },
                new BiomeOption 
                { 
                    name = "Paris, France", 
                    prompt = "Transform the environment into Parisian setting, elegant French architecture, cobblestone streets, street cafes, artistic atmosphere, romantic lighting, European charm, French aesthetic"
                },
                new BiomeOption 
                { 
                    name = "Ancient Egypt", 
                    prompt = "Transform the environment into ancient Egyptian setting, sandstone pyramids, hieroglyphics, sphinx statues, palm trees, desert sand, golden sunlight, pharaonic architecture"
                },
                new BiomeOption 
                { 
                    name = "New York City", 
                    prompt = "Transform the environment into New York City, towering skyscrapers, yellow taxi cabs, busy streets, urban energy, American flags, modern metropolitan atmosphere, iconic NYC aesthetic"
                },
                new BiomeOption 
                { 
                    name = "Venice, Italy", 
                    prompt = "Transform the environment into Venice canals, water channels, gondolas, Italian Renaissance architecture, bridges, romantic atmosphere, Mediterranean colors, Venetian style"
                },
                new BiomeOption 
                { 
                    name = "Morocco Bazaar", 
                    prompt = "Transform the environment into Moroccan marketplace, colorful market stalls, intricate tilework, lanterns, spice vendors, Middle Eastern architecture, vibrant textiles, exotic atmosphere"
                },
                new BiomeOption 
                { 
                    name = "Iceland Landscape", 
                    prompt = "Transform the environment into Icelandic landscape, volcanic rock formations, geysers, hot springs, moss-covered lava fields, dramatic Nordic scenery, mystical atmosphere"
                },
                new BiomeOption 
                { 
                    name = "Greek Islands", 
                    prompt = "Transform the environment into Greek island setting, white-washed buildings, blue domed roofs, Mediterranean sea views, sunny atmosphere, Greek architecture, coastal paradise"
                },
                new BiomeOption 
                { 
                    name = "African Savanna", 
                    prompt = "Transform the environment into African savanna, golden grass plains, acacia trees, warm sunset lighting, wildlife silhouettes, vast open landscape, safari atmosphere"
                },
                new BiomeOption 
                { 
                    name = "London, England", 
                    prompt = "Transform the environment into London setting, British architecture, red telephone boxes, double-decker buses, Victorian buildings, foggy atmosphere, classic English style"
                },
                new BiomeOption 
                { 
                    name = "Australian Outback", 
                    prompt = "Transform the environment into Australian outback, red desert earth, unique rock formations, eucalyptus trees, rugged terrain, clear blue sky, distinctive Australian landscape"
                },
                new BiomeOption 
                { 
                    name = "Swiss Alps", 
                    prompt = "Transform the environment into Swiss Alps, snow-covered mountains, alpine chalets, pristine nature, pine forests, crystal clear air, picturesque mountain village setting"
                },
                new BiomeOption 
                { 
                    name = "Bali, Indonesia", 
                    prompt = "Transform the environment into Balinese setting, traditional temples, rice terraces, tropical gardens, stone carvings, incense atmosphere, Southeast Asian aesthetic, spiritual ambiance"
                },
                new BiomeOption 
                { 
                    name = "Dubai Luxury", 
                    prompt = "Transform the environment into luxury Dubai setting, ultra-modern skyscrapers, gold accents, opulent architecture, palm trees, desert meets luxury, futuristic Middle Eastern style"
                }
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
            if (Time.time - lastNavigationTime < navigationCooldown)
                return;

            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            if (joystickInput.y > 0.5f)
            {
                NavigateBiome(-1);
                lastNavigationTime = Time.time;
            }
            else if (joystickInput.y < -0.5f)
            {
                NavigateBiome(1);
                lastNavigationTime = Time.time;
            }
        }

        private void HandleSelection()
        {
            // Right trigger to confirm and apply biome
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyBiome();
            }
        }

        private void NavigateBiome(int direction)
        {
            selectedIndex = (selectedIndex + direction + biomeOptions.Count) % biomeOptions.Count;
            UpdateDisplay();
        }

        private void ApplyBiome()
        {
            if (webRtcConnection == null || selectedIndex >= biomeOptions.Count)
                return;

            BiomeOption selected = biomeOptions[selectedIndex];
            webRtcConnection.SendCustomPrompt(selected.prompt);
            
            Debug.Log($"Applying biome: {selected.name}");
        }

        private void UpdateDisplay()
        {
            if (selectedIndex >= biomeOptions.Count)
                return;

            BiomeOption selected = biomeOptions[selectedIndex];
            
            if (biomeNameText != null)
            {
                biomeNameText.text = selected.name;
            }
            
            if (biomeDescriptionText != null)
            {
                biomeDescriptionText.text = $"{selectedIndex + 1}/{biomeOptions.Count}";
            }

            UpdateMenuHighlight();
        }

        private void UpdateMenuHighlight()
        {
            for (int i = 0; i < biomeMenuTexts.Count && i < biomeOptions.Count; i++)
            {
                if (biomeMenuTexts[i] != null)
                {
                    biomeMenuTexts[i].text = biomeOptions[i].name;
                    biomeMenuTexts[i].color = (i == selectedIndex) ? selectedColor : normalColor;
                }
            }
        }

        public string GetCurrentBiomeName()
        {
            if (selectedIndex >= 0 && selectedIndex < biomeOptions.Count)
            {
                return biomeOptions[selectedIndex].name;
            }
            return "";
        }
    }
}

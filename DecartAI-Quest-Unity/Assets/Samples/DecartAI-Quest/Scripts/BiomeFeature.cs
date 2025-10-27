using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Controls the Biome/Country Transform feature allowing users to view their room as if it was in different locations/biomes.
    /// Uses Decart Mirage model for environment transformation.
    /// </summary>
    public class BiomeFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Transform biomeListContainer;
        [SerializeField] private GameObject biomeItemPrefab;
        [SerializeField] private TMP_Text descriptionText;
        
        [Header("WebRTC Integration")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Navigation Settings")]
        [SerializeField] private float navigationCooldown = 0.25f;
        
        private List<BiomeData> biomes;
        private List<GameObject> biomeUIItems;
        private int selectedIndex = 0;
        private float lastNavigationTime = 0f;
        
        [Serializable]
        private class BiomeData
        {
            public string name;
            public string category;
            public string description;
            public string decartPrompt;
        }
        
        private void Start()
        {
            InitializeBiomes();
            BuildBiomeUI();
        }
        
        private void InitializeBiomes()
        {
            biomes = new List<BiomeData>
            {
                // Natural Biomes
                new BiomeData { name = "Tropical Rainforest", category = "Nature", 
                    description = "Lush jungle with exotic plants and vibrant wildlife",
                    decartPrompt = "Transform the environment into a tropical rainforest with dense jungle vegetation, exotic plants, vines hanging from ceiling, vibrant green foliage, tropical flowers, misty atmosphere, and natural wildlife ambiance" },
                new BiomeData { name = "Arctic Tundra", category = "Nature", 
                    description = "Frozen landscape with ice and snow",
                    decartPrompt = "Transform the environment into Arctic tundra with snow-covered surfaces, ice formations, frost on windows, aurora borealis lighting, icicles, frozen landscape, cold blue tones, and winter atmosphere" },
                new BiomeData { name = "Desert Oasis", category = "Nature", 
                    description = "Sandy desert with palm trees and water",
                    decartPrompt = "Transform the environment into a desert oasis with golden sand dunes, palm trees, small pools of water, warm sunlight, terracotta colors, desert plants, and Middle Eastern atmosphere" },
                new BiomeData { name = "Underwater Ocean", category = "Nature", 
                    description = "Beneath the sea with coral and marine life",
                    decartPrompt = "Transform the environment into underwater ocean scene with coral reefs, colorful fish swimming, blue-green water effect, sunlight filtering from above, sea plants, bubbles, and marine atmosphere" },
                new BiomeData { name = "Mountain Cave", category = "Nature", 
                    description = "Inside a rocky cave with crystals",
                    decartPrompt = "Transform the environment into a mountain cave with rocky walls, stalactites and stalagmites, glowing crystals, dim lighting, natural stone formations, underground atmosphere, and mystical ambiance" },
                new BiomeData { name = "Bamboo Forest", category = "Nature", 
                    description = "Serene Asian bamboo grove",
                    decartPrompt = "Transform the environment into a peaceful bamboo forest with tall bamboo stalks, dappled sunlight, zen garden elements, stone pathways, soft green tones, and tranquil Asian atmosphere" },
                
                // Country/City Locations
                new BiomeData { name = "Japanese Garden", category = "Countries", 
                    description = "Traditional Zen garden in Japan",
                    decartPrompt = "Transform the environment to a traditional Japanese garden with cherry blossom trees, zen rock garden, koi pond, wooden bridges, paper lanterns, pagoda structures, and peaceful Eastern aesthetic" },
                new BiomeData { name = "Parisian Cafe", category = "Countries", 
                    description = "Charming outdoor cafe in Paris",
                    decartPrompt = "Transform the environment into a Parisian cafe with wrought iron tables, bistro chairs, Eiffel Tower view, French architecture, warm lighting, flower boxes, cobblestone streets, and romantic European atmosphere" },
                new BiomeData { name = "New York Loft", category = "Countries", 
                    description = "Industrial loft in Manhattan",
                    decartPrompt = "Transform the environment into a New York City loft with exposed brick walls, large industrial windows, city skyline view, modern furniture, Edison bulb lighting, and urban American aesthetic" },
                new BiomeData { name = "Egyptian Temple", category = "Countries", 
                    description = "Ancient temple near pyramids",
                    decartPrompt = "Transform the environment into an ancient Egyptian temple with hieroglyphic walls, golden statues, stone pillars, sand-colored stone, torch lighting, pyramid views, and archaeological atmosphere" },
                new BiomeData { name = "Moroccan Palace", category = "Countries", 
                    description = "Ornate palace in Morocco",
                    decartPrompt = "Transform the environment into a Moroccan palace with intricate tile mosaics, arched doorways, colorful textiles, hanging lanterns, cushioned seating, ornate patterns, and Middle Eastern luxury" },
                new BiomeData { name = "Scandinavian Cabin", category = "Countries", 
                    description = "Cozy cabin in Nordic woods",
                    decartPrompt = "Transform the environment into a Scandinavian cabin with white walls, wooden floors, minimalist design, warm fireplace, hygge atmosphere, Nordic furniture, pine trees visible outside, and cozy winter aesthetic" },
                new BiomeData { name = "Indian Palace", category = "Countries", 
                    description = "Colorful Maharaja palace",
                    decartPrompt = "Transform the environment into an Indian palace with vibrant colors, intricate carvings, marble columns, ornate rugs, silk curtains, golden decorations, mandala patterns, and royal South Asian aesthetic" },
                new BiomeData { name = "Greek Island", category = "Countries", 
                    description = "White buildings by Mediterranean sea",
                    decartPrompt = "Transform the environment into a Greek island setting with white-washed walls, blue domed roofs, ocean views, sunny Mediterranean atmosphere, terracotta pots, bright natural light, and coastal Greek aesthetic" },
                
                // Fantasy Locations
                new BiomeData { name = "Magical Forest", category = "Fantasy", 
                    description = "Enchanted woods with glowing elements",
                    decartPrompt = "Transform the environment into a magical enchanted forest with glowing mushrooms, fairy lights, mystical fog, ancient trees with faces, floating sparkles, ethereal atmosphere, and fantasy woodland setting" },
                new BiomeData { name = "Space Station", category = "Fantasy", 
                    description = "Futuristic orbital habitat",
                    decartPrompt = "Transform the environment into a futuristic space station with metallic walls, holographic displays, window views of Earth and stars, sleek technology, LED lighting, zero-gravity aesthetics, and sci-fi atmosphere" },
                new BiomeData { name = "Cloud Palace", category = "Fantasy", 
                    description = "Floating castle in the sky",
                    decartPrompt = "Transform the environment into a cloud palace floating in the sky with fluffy clouds, celestial atmosphere, golden pillars, ethereal lighting, views of endless sky, angelic aesthetics, and heavenly design" },
                new BiomeData { name = "Steampunk Workshop", category = "Fantasy", 
                    description = "Victorian-era mechanical workshop",
                    decartPrompt = "Transform the environment into a steampunk workshop with brass gears, copper pipes, Victorian machinery, vintage gauges, warm industrial lighting, leather and wood furniture, and retro-futuristic aesthetic" },
                new BiomeData { name = "Cyberpunk City", category = "Fantasy", 
                    description = "Neon-lit futuristic metropolis",
                    decartPrompt = "Transform the environment into a cyberpunk city with neon lights, holographic advertisements, rainy night atmosphere, Asian-inspired architecture, purple and blue lighting, tech noir aesthetic, and dystopian future setting" },
                new BiomeData { name = "Medieval Castle", category = "Fantasy", 
                    description = "Stone fortress from the Middle Ages",
                    decartPrompt = "Transform the environment into a medieval castle interior with stone walls, torch lighting, tapestries, wooden furniture, suits of armor, castle architecture, gothic elements, and historical fantasy atmosphere" },
                
                // Seasonal/Weather
                new BiomeData { name = "Cherry Blossom Spring", category = "Seasonal", 
                    description = "Japanese spring with pink blossoms",
                    decartPrompt = "Transform the environment into a spring scene with cherry blossom trees in full bloom, pink petals floating through air, soft pastel colors, fresh green grass, warm sunlight, and peaceful spring atmosphere" },
                new BiomeData { name = "Autumn Forest", category = "Seasonal", 
                    description = "Fall colors with golden leaves",
                    decartPrompt = "Transform the environment into an autumn forest with red, orange, and yellow leaves, golden hour lighting, falling leaves, cozy warm tones, harvest atmosphere, and peaceful fall scenery" },
                new BiomeData { name = "Winter Wonderland", category = "Seasonal", 
                    description = "Snowy holiday scene",
                    decartPrompt = "Transform the environment into a winter wonderland with fresh snow, twinkling lights, Christmas decorations, warm indoor lighting, frost on windows, snowflakes, and festive holiday atmosphere" },
                new BiomeData { name = "Rainy Day", category = "Seasonal", 
                    description = "Cozy rain with water droplets",
                    decartPrompt = "Transform the environment into a rainy day scene with water droplets on windows, grey clouds visible outside, cozy indoor lighting, wet surfaces with reflections, peaceful rain atmosphere, and contemplative mood" }
            };
        }
        
        private void BuildBiomeUI()
        {
            biomeUIItems = new List<GameObject>();
            
            if (descriptionText != null)
            {
                descriptionText.text = "Select a biome or location to transform your environment";
            }
            
            foreach (var biome in biomes)
            {
                GameObject uiItem = CreateBiomeUIItem(biome);
                biomeUIItems.Add(uiItem);
            }
            
            UpdateSelection();
        }
        
        private GameObject CreateBiomeUIItem(BiomeData biome)
        {
            GameObject itemObj = new GameObject($"Biome_{biome.name}");
            itemObj.transform.SetParent(biomeListContainer, false);
            
            RectTransform rect = itemObj.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(700, 70);
            
            TMP_Text text = itemObj.AddComponent<TextMeshProUGUI>();
            text.text = $"[{biome.category}] {biome.name}";
            text.fontSize = 32;
            text.color = Color.white;
            text.alignment = TextAlignmentOptions.Left;
            
            return itemObj;
        }
        
        private void Update()
        {
            if (!gameObject.activeSelf) return;
            
            HandleNavigation();
            HandleSelection();
            UpdateDescription();
        }
        
        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown) return;
            
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            if (joystickInput.y > 0.5f)
            {
                NavigateUp();
                lastNavigationTime = Time.time;
            }
            else if (joystickInput.y < -0.5f)
            {
                NavigateDown();
                lastNavigationTime = Time.time;
            }
        }
        
        private void HandleSelection()
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyBiome();
            }
        }
        
        private void NavigateUp()
        {
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = biomes.Count - 1;
            }
            UpdateSelection();
        }
        
        private void NavigateDown()
        {
            selectedIndex++;
            if (selectedIndex >= biomes.Count)
            {
                selectedIndex = 0;
            }
            UpdateSelection();
        }
        
        private void UpdateSelection()
        {
            for (int i = 0; i < biomeUIItems.Count; i++)
            {
                TMP_Text text = biomeUIItems[i].GetComponent<TextMeshProUGUI>();
                if (text != null)
                {
                    text.color = (i == selectedIndex) ? Color.green : Color.white;
                    text.fontSize = (i == selectedIndex) ? 36 : 32;
                }
            }
        }
        
        private void UpdateDescription()
        {
            if (descriptionText != null && selectedIndex >= 0 && selectedIndex < biomes.Count)
            {
                BiomeData biome = biomes[selectedIndex];
                descriptionText.text = $"{biome.name}\n{biome.description}";
            }
        }
        
        private void ApplyBiome()
        {
            if (selectedIndex >= 0 && selectedIndex < biomes.Count)
            {
                BiomeData biome = biomes[selectedIndex];
                
                if (webRtcConnection != null)
                {
                    Debug.Log($"Applying biome: {biome.name}");
                    webRtcConnection.SendCustomPrompt(biome.decartPrompt);
                }
                else
                {
                    Debug.LogWarning("WebRTC connection not available");
                }
            }
        }
        
        private void OnEnable()
        {
            Debug.Log("Biome Transform feature activated");
            selectedIndex = 0;
            if (biomeUIItems != null && biomeUIItems.Count > 0)
            {
                UpdateSelection();
            }
        }
    }
}

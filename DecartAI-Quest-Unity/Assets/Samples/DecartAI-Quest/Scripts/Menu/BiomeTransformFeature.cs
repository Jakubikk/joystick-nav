using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Biome Transform feature - transform your room to look like different countries or biomes.
    /// Uses Decart AI Mirage model for environment transformation.
    /// </summary>
    public class BiomeTransformFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject featurePanel;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text currentBiomeText;
        [SerializeField] private TMP_Text instructionsText;
        [SerializeField] private Transform biomeListContainer;
        [SerializeField] private GameObject listItemPrefab;

        [Header("WebRTC")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        [Header("Settings")]
        [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.6f);
        [SerializeField] private Color selectedColor = new Color(0.8f, 0.4f, 0.1f, 1f);
        [SerializeField] private float navigationCooldown = 0.2f;

        private MenuManager menuManager;
        private List<BiomeItem> biomeItems = new List<BiomeItem>();
        private int currentIndex = 0;
        private bool isActive = false;
        private float lastNavigationTime = 0f;

        private class BiomeItem
        {
            public string name;
            public string prompt;
            public GameObject uiObject;
            public TMP_Text text;
            public Image background;
        }

        // Biome/Country transformation options
        private Dictionary<string, string> biomeOptions = new Dictionary<string, string>()
        {
            // Countries & Regions
            { "Japan - Cherry Blossoms", "Transform to Japanese environment with cherry blossom trees, traditional architecture, pagodas, paper lanterns, zen gardens, and peaceful sakura atmosphere" },
            { "China - Imperial Palace", "Transform to Chinese imperial palace with red and gold decorations, traditional architecture, dragon motifs, ornate details, and majestic atmosphere" },
            { "Paris, France", "Transform to Parisian environment with Eiffel Tower views, French cafe culture, elegant architecture, cobblestone streets, and romantic atmosphere" },
            { "Venice, Italy", "Transform to Venetian canals with gondolas, Renaissance architecture, bridges, water reflections, and Italian charm" },
            { "Dubai, UAE", "Transform to modern Dubai with futuristic skyscrapers, luxury architecture, gold accents, desert views, and opulent atmosphere" },
            { "New York City", "Transform to NYC environment with skyscrapers, urban streets, yellow cabs, busy atmosphere, and metropolitan energy" },
            { "California Beach", "Transform to California beach environment with palm trees, sandy beaches, surfboards, sunset colors, and laid-back coastal vibes" },
            { "London, England", "Transform to London with Big Ben, red telephone boxes, traditional British architecture, fog, and classic English atmosphere" },
            { "Egypt - Pyramids", "Transform to ancient Egyptian environment with pyramids, hieroglyphics, sand dunes, sphinx statues, and desert atmosphere" },
            { "Greece - Santorini", "Transform to Greek island of Santorini with white and blue buildings, Mediterranean Sea views, sunny atmosphere, and Aegean charm" },
            { "India - Taj Mahal", "Transform to Indian environment with ornate architecture, vibrant colors, intricate patterns, marigolds, and cultural richness" },
            { "Iceland - Northern Lights", "Transform to Iceland with aurora borealis, snow-covered landscape, volcanic rocks, glaciers, and mystical Nordic atmosphere" },
            { "Australia - Outback", "Transform to Australian outback with red sand, unique rock formations, desert plants, vast open spaces, and rugged landscape" },
            { "Brazil - Rainforest", "Transform to Brazilian rainforest with lush vegetation, tropical birds, vibrant colors, waterfalls, and exotic jungle atmosphere" },
            { "Morocco - Marrakech", "Transform to Moroccan environment with colorful tiles, archways, lanterns, bazaar atmosphere, and North African charm" },
            
            // Natural Biomes
            { "Tropical Rainforest", "Transform to dense tropical rainforest with massive trees, hanging vines, exotic flowers, mist, wildlife sounds, and lush greenery" },
            { "Arctic Tundra", "Transform to Arctic tundra with snow and ice, frozen landscape, aurora borealis, icebergs, and frigid polar atmosphere" },
            { "Desert Oasis", "Transform to desert oasis with palm trees, water spring, sand dunes, cacti, warm sunlight, and Middle Eastern atmosphere" },
            { "Coral Reef Underwater", "Transform to underwater coral reef environment with colorful fish, coral formations, blue water, sea plants, and marine life" },
            { "Alpine Mountains", "Transform to Alpine mountain environment with snow-capped peaks, evergreen trees, wooden chalets, clear air, and mountain atmosphere" },
            { "African Savanna", "Transform to African savanna with acacia trees, golden grass, safari atmosphere, warm sunset, and wild open plains" },
            { "Bamboo Forest", "Transform to serene bamboo forest with towering bamboo stalks, filtered green light, peaceful atmosphere, and Asian zen ambiance" },
            { "Autumn Forest", "Transform to autumn forest with colorful fall leaves, orange and red trees, crisp air, fallen leaves on ground, and cozy atmosphere" },
            { "Spring Meadow", "Transform to spring meadow with wildflowers, fresh green grass, butterflies, blue sky, sunshine, and cheerful atmosphere" },
            { "Winter Wonderland", "Transform to winter wonderland with heavy snow, icicles, frozen trees, soft snowfall, and magical snowy atmosphere" },
            { "Volcanic Landscape", "Transform to volcanic landscape with lava flows, rocky terrain, ash, smoke, dramatic lighting, and intense fiery atmosphere" },
            { "Canyon Valley", "Transform to grand canyon environment with red rock formations, desert landscape, deep valleys, clear sky, and Western atmosphere" },
            { "Mangrove Swamp", "Transform to mangrove swamp with tangled roots, brackish water, tropical vegetation, wildlife, and mysterious wetland atmosphere" },
            { "Pine Forest", "Transform to pine forest with tall evergreen trees, forest floor, dappled sunlight, fresh air, and peaceful woodland atmosphere" },
            
            // Fantasy Biomes
            { "Magical Fairy Forest", "Transform to magical fairy forest with glowing mushrooms, sparkles, ethereal light, mystical creatures, and enchanted atmosphere" },
            { "Crystal Cavern", "Transform to crystal cavern with glowing crystals, underground caves, mineral formations, magical lighting, and fantasy underground world" },
            { "Floating Islands", "Transform to floating islands in the sky with waterfalls cascading down, clouds, fantasy architecture, and surreal aerial atmosphere" },
            { "Bioluminescent Forest", "Transform to bioluminescent alien forest with glowing plants, neon colors, exotic vegetation, and otherworldly sci-fi atmosphere" },
            { "Mushroom Kingdom", "Transform to giant mushroom kingdom with oversized mushrooms, whimsical plants, fantasy colors, and fairy tale atmosphere" },
        };

        private void Awake()
        {
            menuManager = FindFirstObjectByType<MenuManager>();
            
            if (webRTCConnection == null)
            {
                webRTCConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            if (featurePanel != null)
                featurePanel.SetActive(false);
        }

        public void Activate()
        {
            isActive = true;
            
            if (featurePanel != null)
                featurePanel.SetActive(true);

            if (titleText != null)
                titleText.text = "Biome Transform";

            if (instructionsText != null)
                instructionsText.text = "Joystick Up/Down: Navigate | Right Trigger: Transform | Left Trigger: Back";

            InitializeBiomeList();
            UpdateSelection();
        }

        public void Deactivate()
        {
            isActive = false;
            
            if (featurePanel != null)
                featurePanel.SetActive(false);

            ClearBiomeList();
        }

        private void InitializeBiomeList()
        {
            ClearBiomeList();

            foreach (var option in biomeOptions)
            {
                GameObject itemObj = Instantiate(listItemPrefab, biomeListContainer);
                TMP_Text itemText = itemObj.GetComponentInChildren<TMP_Text>();
                Image itemBg = itemObj.GetComponent<Image>();

                if (itemText != null)
                    itemText.text = option.Key;

                BiomeItem item = new BiomeItem
                {
                    name = option.Key,
                    prompt = option.Value,
                    uiObject = itemObj,
                    text = itemText,
                    background = itemBg
                };

                biomeItems.Add(item);
            }

            currentIndex = 0;
        }

        private void ClearBiomeList()
        {
            foreach (var item in biomeItems)
            {
                if (item.uiObject != null)
                    Destroy(item.uiObject);
            }
            biomeItems.Clear();
        }

        private void Update()
        {
            if (!isActive)
                return;

            HandleNavigation();
            HandleSelection();
            HandleBack();
        }

        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown)
                return;

            Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            if (joystick.y > 0.5f)
            {
                currentIndex--;
                if (currentIndex < 0)
                    currentIndex = biomeItems.Count - 1;
                
                lastNavigationTime = Time.time;
                UpdateSelection();
            }
            else if (joystick.y < -0.5f)
            {
                currentIndex++;
                if (currentIndex >= biomeItems.Count)
                    currentIndex = 0;
                
                lastNavigationTime = Time.time;
                UpdateSelection();
            }
        }

        private void HandleSelection()
        {
            // Right trigger to apply biome
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                if (currentIndex >= 0 && currentIndex < biomeItems.Count)
                {
                    ApplyBiome(biomeItems[currentIndex]);
                }
            }
        }

        private void HandleBack()
        {
            // Left trigger to go back
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (menuManager != null)
                    menuManager.ReturnToMainMenu();
            }
        }

        private void UpdateSelection()
        {
            for (int i = 0; i < biomeItems.Count; i++)
            {
                if (biomeItems[i].background != null)
                {
                    biomeItems[i].background.color = (i == currentIndex) ? selectedColor : normalColor;
                }
                
                if (biomeItems[i].text != null)
                {
                    biomeItems[i].text.fontStyle = (i == currentIndex) ? FontStyles.Bold : FontStyles.Normal;
                }
            }

            if (currentBiomeText != null && currentIndex >= 0 && currentIndex < biomeItems.Count)
            {
                currentBiomeText.text = $"Selected: {biomeItems[currentIndex].name}";
            }
        }

        private void ApplyBiome(BiomeItem item)
        {
            if (item == null || webRTCConnection == null)
                return;

            Debug.Log($"Biome Transform: Applying {item.name} with prompt: {item.prompt}");
            webRTCConnection.SendCustomPrompt(item.prompt);

            if (currentBiomeText != null)
            {
                currentBiomeText.text = $"Current Biome: {item.name} ✓";
            }
        }
    }
}

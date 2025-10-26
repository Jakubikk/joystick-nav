using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Biome Transformation feature controller.
    /// Transforms the environment to different countries and biomes.
    /// Uses Mirage model for complete world transformations.
    /// </summary>
    public class BiomeController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject biomeUI;
        [SerializeField] private Transform biomeOptionsContainer;
        [SerializeField] private GameObject biomeOptionPrefab;
        [SerializeField] private TMP_Text selectedBiomeText;
        [SerializeField] private TMP_Text instructionsText;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        private bool isActive = false;
        private int currentBiomeIndex = 0;
        private List<BiomeOption> biomeOptions = new List<BiomeOption>();
        private float inputCooldown = 0f;
        private const float INPUT_DELAY = 0.2f;

        private class BiomeOption
        {
            public string name;
            public string description;
            public string aiPrompt;
            public GameObject uiElement;
        }

        private void Awake()
        {
            if (biomeUI != null)
                biomeUI.SetActive(false);

            InitializeBiomeOptions();
        }

        private void InitializeBiomeOptions()
        {
            biomeOptions.Clear();

            // Countries & Cities
            AddBiomeOption("Japan - Kyoto", 
                "Traditional Japanese temples, cherry blossoms, pagodas",
                "Transform into Kyoto Japan world with traditional wooden temples, cherry blossom trees, stone pathways, pagodas, zen gardens, peaceful Japanese aesthetic, lanterns, bamboo forests");

            AddBiomeOption("China - Beijing", 
                "Ancient Chinese architecture, imperial palaces",
                "Transform into Beijing China world with imperial Chinese architecture, red walls, golden roofs, traditional courtyard buildings, Chinese lanterns, dragon motifs, ancient Asian aesthetic");

            AddBiomeOption("France - Paris", 
                "Romantic Parisian streets and cafes",
                "Transform into Parisian world with Haussmann architecture, cobblestone streets, sidewalk cafes, wrought iron balconies, Art Nouveau details, romantic French atmosphere, classic Parisian charm");

            AddBiomeOption("Italy - Venice", 
                "Canals, gondolas, Renaissance architecture",
                "Transform into Venice Italy world with Renaissance architecture, water canals, gondolas, ornate bridges, terracotta roofs, Italian piazzas, Mediterranean atmosphere, marble and stone buildings");

            AddBiomeOption("Dubai - Luxury", 
                "Ultra-modern skyscrapers and luxury",
                "Transform into Dubai luxury world with towering glass skyscrapers, gold accents, modern luxury architecture, palm trees, pristine white buildings, opulent Middle Eastern design, futuristic cityscape");

            AddBiomeOption("India - Taj Mahal Style", 
                "Ornate Mughal architecture and vibrant colors",
                "Transform into Indian Mughal world with ornate marble architecture, intricate geometric patterns, vibrant colors, decorative arches, domed structures, traditional Indian palace aesthetic");

            AddBiomeOption("Greece - Santorini", 
                "White buildings, blue domes, Mediterranean views",
                "Transform into Greek Santorini world with white-washed buildings, blue domed churches, Mediterranean architecture, terracotta pots, bougainvillea flowers, scenic coastal views, Greek island paradise");

            AddBiomeOption("Egypt - Ancient Pyramids", 
                "Ancient Egyptian temples and hieroglyphics",
                "Transform into ancient Egyptian world with sandstone pyramids, hieroglyphic carvings, sphinx statues, golden accents, desert sands, palm trees, pharaonic architecture, mystical ancient Egypt");

            // Natural Biomes
            AddBiomeOption("Tropical Rainforest", 
                "Lush jungle with exotic plants and wildlife",
                "Transform into tropical rainforest biome with dense jungle vegetation, massive trees, hanging vines, exotic colorful plants, misty atmosphere, waterfalls, vibrant green foliage, humid jungle environment");

            AddBiomeOption("Arctic Tundra", 
                "Frozen wilderness with ice and snow",
                "Transform into Arctic tundra biome with snow-covered landscape, ice formations, glaciers, northern lights in sky, sparse vegetation, frozen terrain, pristine white environment, extreme cold atmosphere");

            AddBiomeOption("African Savanna", 
                "Golden grasslands with acacia trees",
                "Transform into African savanna biome with golden grasslands, scattered acacia trees, warm sunset lighting, vast open plains, red earth, safari landscape, wild African environment");

            AddBiomeOption("Coral Reef Underwater", 
                "Vibrant underwater coral garden",
                "Transform into underwater coral reef world with colorful coral formations, tropical fish, crystal clear blue water, sunlight filtering through water, marine plants, aquatic paradise, ocean floor scenery");

            AddBiomeOption("Desert Oasis", 
                "Sandy dunes with palm-filled oasis",
                "Transform into desert oasis biome with golden sand dunes, palm tree cluster, clear blue water pool, sun-bleached rocks, arid landscape with life-giving water, Middle Eastern desert atmosphere");

            AddBiomeOption("Mountain Alpine", 
                "Snow-capped peaks and pine forests",
                "Transform into alpine mountain biome with snow-capped peaks, evergreen forests, rocky cliffs, fresh mountain air atmosphere, wooden chalets, pristine nature, Swiss Alps aesthetic");

            AddBiomeOption("Bamboo Forest", 
                "Serene Asian bamboo grove",
                "Transform into bamboo forest biome with towering green bamboo stalks, dappled sunlight, zen atmosphere, stone pathways, peaceful Asian forest, misty morning light, natural sanctuary");

            AddBiomeOption("Autumn Forest", 
                "Fall foliage with red and gold leaves",
                "Transform into autumn forest biome with red, orange, and golden leaves, falling foliage, crisp atmosphere, fallen leaves on ground, warm sunset lighting, cozy fall season aesthetic");

            AddBiomeOption("Beach Paradise", 
                "Tropical white sand beach",
                "Transform into tropical beach paradise with white sand beaches, turquoise water, palm trees swaying, clear blue sky, thatched umbrellas, coastal paradise, vacation resort atmosphere");

            // Fantasy & Themed
            AddBiomeOption("Cyberpunk City", 
                "Neon-lit futuristic metropolis",
                "Transform into cyberpunk world with neon lights, holographic advertisements, towering skyscrapers, rain-slicked streets, futuristic technology, purple and blue neon glow, dystopian urban future");

            AddBiomeOption("Steampunk Victorian", 
                "Victorian era with brass machinery",
                "Transform into steampunk world with Victorian architecture, exposed brass gears, copper pipes, steam vents, clockwork mechanisms, industrial revolution aesthetic, bronze and sepia tones");

            AddBiomeOption("Magical Fantasy Realm", 
                "Enchanted forest with mystical elements",
                "Transform into magical fantasy world with glowing mushrooms, floating crystals, ethereal lighting, mystical fog, enchanted trees, fairy lights, magical particles in air, otherworldly beauty");

            AddBiomeOption("Space Station Interior", 
                "Futuristic space habitat",
                "Transform into space station interior with white metallic walls, holographic displays, futuristic technology, LED lighting strips, zero-gravity design, sci-fi spacecraft aesthetic, advanced human habitat");

            AddBiomeOption("Minecraft World", 
                "Blocky cubic Minecraft aesthetic",
                "Transform into Minecraft world with blocky cubic structures, pixelated textures, square trees, crafted building materials, game-like rendering, low-poly aesthetic, voxel-based environment");

            AddBiomeOption("LEGO World", 
                "Everything made of LEGO bricks",
                "Transform into LEGO world where everything is made of colorful plastic LEGO bricks, snapping together, glossy plastic finish, toy construction aesthetic, vibrant primary colors");

            AddBiomeOption("Candy Land", 
                "Sweet treats and sugary landscape",
                "Transform into candy land world with buildings made of candy, lollipop trees, frosting decorations, pastel colors, sweet treats everywhere, whimsical dessert paradise, sugar-coated environment");

            AddBiomeOption("Frozen Ice Kingdom", 
                "Magical frozen palace and ice crystals",
                "Transform into frozen ice kingdom with ice crystal formations, snow palaces, aurora borealis, frozen waterfalls, glacial architecture, magical winter wonderland, Frozen movie aesthetic");
        }

        private void AddBiomeOption(string name, string description, string aiPrompt)
        {
            var option = new BiomeOption
            {
                name = name,
                description = description,
                aiPrompt = aiPrompt
            };

            if (biomeOptionPrefab != null && biomeOptionsContainer != null)
            {
                option.uiElement = Instantiate(biomeOptionPrefab, biomeOptionsContainer);
                var text = option.uiElement.GetComponentInChildren<TMP_Text>();
                if (text != null)
                {
                    text.text = name;
                }
            }

            biomeOptions.Add(option);
        }

        private void Update()
        {
            if (!isActive) return;

            if (inputCooldown > 0)
            {
                inputCooldown -= Time.deltaTime;
                return;
            }

            HandleNavigation();
            HandleSelection();
        }

        private void HandleNavigation()
        {
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            // Navigate up
            if (joystickInput.y > 0.7f)
            {
                currentBiomeIndex--;
                if (currentBiomeIndex < 0) currentBiomeIndex = biomeOptions.Count - 1;
                UpdateBiomeDisplay();
                inputCooldown = INPUT_DELAY;
            }
            // Navigate down
            else if (joystickInput.y < -0.7f)
            {
                currentBiomeIndex++;
                if (currentBiomeIndex >= biomeOptions.Count) currentBiomeIndex = 0;
                UpdateBiomeDisplay();
                inputCooldown = INPUT_DELAY;
            }
        }

        private void HandleSelection()
        {
            // Right trigger to apply biome transformation
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                ApplyBiomeTransformation();
                inputCooldown = INPUT_DELAY;
            }
        }

        private void UpdateBiomeDisplay()
        {
            if (biomeOptions.Count == 0) return;

            // Update selection highlighting
            for (int i = 0; i < biomeOptions.Count; i++)
            {
                if (biomeOptions[i].uiElement != null)
                {
                    var text = biomeOptions[i].uiElement.GetComponentInChildren<TMP_Text>();
                    if (text != null)
                    {
                        text.color = (i == currentBiomeIndex) ? Color.yellow : Color.white;
                        text.fontStyle = (i == currentBiomeIndex) ? FontStyles.Bold : FontStyles.Normal;
                    }
                }
            }

            // Update selected biome text
            if (currentBiomeIndex < biomeOptions.Count)
            {
                if (selectedBiomeText != null)
                {
                    selectedBiomeText.text = $"<b>{biomeOptions[currentBiomeIndex].name}</b>\n{biomeOptions[currentBiomeIndex].description}";
                }
            }
        }

        private void ApplyBiomeTransformation()
        {
            if (currentBiomeIndex >= biomeOptions.Count) return;

            var selectedBiome = biomeOptions[currentBiomeIndex];
            
            if (webRTCConnection != null)
            {
                webRTCConnection.SendCustomPrompt(selectedBiome.aiPrompt);
                Debug.Log($"BiomeController: Applied {selectedBiome.name} transformation");
            }
            else
            {
                Debug.LogWarning("BiomeController: WebRTC connection not set");
            }
        }

        public void Activate()
        {
            isActive = true;
            if (biomeUI != null)
                biomeUI.SetActive(true);

            UpdateBiomeDisplay();

            if (instructionsText != null)
            {
                instructionsText.text = "Joystick Up/Down: Browse biomes\nRight Trigger: Transform environment";
            }

            Debug.Log("BiomeController: Activated");
        }

        public void Deactivate()
        {
            isActive = false;
            if (biomeUI != null)
                biomeUI.SetActive(false);
            
            Debug.Log("BiomeController: Deactivated");
        }
    }
}

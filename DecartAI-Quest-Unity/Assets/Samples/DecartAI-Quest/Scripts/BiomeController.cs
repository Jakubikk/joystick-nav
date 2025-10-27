using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Biome Transformation feature - transforms room to different countries/environments
    /// Uses Decart AI's Mirage model for environment transformation
    /// </summary>
    public class BiomeController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private RectTransform biomeOptionsContainer;
        [SerializeField] private GameObject biomeItemPrefab;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text selectedBiomeText;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        private List<BiomeOption> biomeOptions;
        private List<GameObject> biomeItemObjects;
        private int currentSelectionIndex = 0;

        [System.Serializable]
        private class BiomeOption
        {
            public string Name;
            public string Description;
            public string PromptTemplate;

            public BiomeOption(string name, string description, string template)
            {
                Name = name;
                Description = description;
                PromptTemplate = template;
            }
        }

        private void Awake()
        {
            if (webRTCConnection == null)
            {
                webRTCConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            InitializeBiomeOptions();
            biomeItemObjects = new List<GameObject>();
        }

        private void InitializeBiomeOptions()
        {
            biomeOptions = new List<BiomeOption>
            {
                new BiomeOption("Japanese Garden", "Serene Zen garden with cherry blossoms",
                    "Transform the environment into a traditional Japanese garden: cherry blossom trees, bamboo, stone lanterns, koi pond, wooden bridges, zen rock gardens, soft pink petals falling, peaceful Japanese aesthetic"),
                
                new BiomeOption("Tropical Paradise", "Exotic island environment",
                    "Transform the environment into a tropical island paradise: palm trees, white sand, turquoise ocean views, exotic flowers, tiki torches, bamboo furniture, vibrant tropical plants, warm sunny atmosphere"),
                
                new BiomeOption("Arctic Tundra", "Frozen northern wilderness",
                    "Transform the environment into arctic tundra: snow-covered landscape, ice formations, northern lights in the sky, frozen trees, white and blue color palette, crisp cold atmosphere, winter wonderland"),
                
                new BiomeOption("Desert Oasis", "Middle Eastern desert setting",
                    "Transform the environment into a desert oasis: warm sand dunes, palm trees, Middle Eastern architecture, decorative tiles, flowing fabrics, ornate carpets, golden sunlight, Arabian aesthetic"),
                
                new BiomeOption("English Countryside", "Classic British pastoral scene",
                    "Transform the environment into English countryside: rolling green hills, stone cottages, hedgerows, wildflowers, cloudy skies, pastoral landscape, traditional British charm"),
                
                new BiomeOption("Amazon Rainforest", "Dense tropical jungle",
                    "Transform the environment into Amazon rainforest: lush dense jungle, hanging vines, exotic plants, vibrant green foliage, tropical birds, humid atmosphere, jungle canopy, natural wilderness"),
                
                new BiomeOption("Alpine Mountain Lodge", "Swiss mountain retreat",
                    "Transform the environment into an Alpine mountain lodge: wooden chalet interior, mountain views, stone fireplace, cozy furs, rustic wood furniture, snow-capped peaks visible, warm cabin atmosphere"),
                
                new BiomeOption("Mediterranean Villa", "Coastal Italian setting",
                    "Transform the environment into a Mediterranean villa: terracotta tiles, white stucco walls, blue shutters, olive trees, grapevines, coastal views, warm sunlight, Italian coastal charm"),
                
                new BiomeOption("African Savanna", "Wide open grasslands",
                    "Transform the environment into African savanna: golden grass plains, acacia trees, warm sunset colors, distant mountains, safari lodge aesthetic, earthy tones, wild landscape"),
                
                new BiomeOption("Scandinavian Interior", "Modern Nordic design",
                    "Transform the environment into Scandinavian interior: minimalist design, light wood furniture, white walls, cozy textiles, hygge atmosphere, clean lines, natural materials, Nordic simplicity"),
                
                new BiomeOption("Moroccan Riad", "North African courtyard",
                    "Transform the environment into a Moroccan riad: colorful mosaic tiles, ornate arches, hanging lanterns, geometric patterns, cushioned seating, fountain centerpiece, rich colors, exotic atmosphere"),
                
                new BiomeOption("New York Loft", "Urban industrial space",
                    "Transform the environment into a New York loft: exposed brick walls, industrial windows, metal beams, urban skyline views, modern furniture, concrete floors, city atmosphere"),
                
                new BiomeOption("Balinese Temple", "Indonesian sacred space",
                    "Transform the environment into a Balinese temple: stone carvings, tropical plants, incense smoke, water features, traditional statues, ornate doorways, spiritual atmosphere, Southeast Asian aesthetic"),
                
                new BiomeOption("Icelandic Geothermal", "Volcanic landscape",
                    "Transform the environment into Icelandic geothermal landscape: steaming hot springs, volcanic rocks, moss-covered lava fields, dramatic sky, geothermal pools, unique Nordic nature"),
                
                new BiomeOption("French Château", "Elegant palace interior",
                    "Transform the environment into a French château: ornate gold details, crystal chandeliers, marble floors, baroque furniture, tall windows, elegant drapes, classical European luxury"),
                
                new BiomeOption("Australian Outback", "Red desert landscape",
                    "Transform the environment into Australian outback: red earth, rugged terrain, eucalyptus trees, vast open space, warm earthy tones, rustic bush aesthetic, unique Australian landscape"),
                
                new BiomeOption("Underwater Reef", "Ocean floor environment",
                    "Transform the environment into an underwater coral reef: colorful coral formations, tropical fish, aqua blue water, sunlight filtering through water, sea plants, marine life, underwater atmosphere"),
                
                new BiomeOption("Space Station", "Futuristic orbital habitat",
                    "Transform the environment into a space station: high-tech panels, viewing windows showing stars, metallic surfaces, LED lighting, futuristic controls, zero-gravity aesthetic, sci-fi atmosphere")
            };
        }

        private void Start()
        {
            CreateBiomeItems();
            UpdateDescriptionText();
        }

        private void CreateBiomeItems()
        {
            if (biomeItemPrefab == null || biomeOptionsContainer == null)
            {
                Debug.LogError("BiomeController: Prefab or container not assigned");
                return;
            }

            // Clear existing items
            foreach (Transform child in biomeOptionsContainer)
            {
                Destroy(child.gameObject);
            }
            biomeItemObjects.Clear();

            // Create biome option items
            for (int i = 0; i < biomeOptions.Count; i++)
            {
                GameObject item = Instantiate(biomeItemPrefab, biomeOptionsContainer);
                TMP_Text itemText = item.GetComponentInChildren<TMP_Text>();
                if (itemText != null)
                {
                    itemText.text = biomeOptions[i].Name;
                }
                biomeItemObjects.Add(item);
            }

            UpdateSelection();
        }

        private void Update()
        {
            if (!gameObject.activeSelf) return;

            HandleInput();
        }

        private void HandleInput()
        {
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            // Joystick up/down navigation
            if (joystickInput.y > 0.5f && !IsJoystickCooldown())
            {
                NavigateUp();
                StartJoystickCooldown();
            }
            else if (joystickInput.y < -0.5f && !IsJoystickCooldown())
            {
                NavigateDown();
                StartJoystickCooldown();
            }

            // Right trigger to confirm and apply biome
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplySelectedBiome();
            }
        }

        private float joystickCooldownTime = 0f;
        private const float JOYSTICK_COOLDOWN = 0.2f;

        private bool IsJoystickCooldown()
        {
            return Time.time < joystickCooldownTime;
        }

        private void StartJoystickCooldown()
        {
            joystickCooldownTime = Time.time + JOYSTICK_COOLDOWN;
        }

        private void NavigateUp()
        {
            if (biomeOptions.Count == 0) return;

            currentSelectionIndex--;
            if (currentSelectionIndex < 0)
            {
                currentSelectionIndex = biomeOptions.Count - 1;
            }
            UpdateSelection();
        }

        private void NavigateDown()
        {
            if (biomeOptions.Count == 0) return;

            currentSelectionIndex++;
            if (currentSelectionIndex >= biomeOptions.Count)
            {
                currentSelectionIndex = 0;
            }
            UpdateSelection();
        }

        private void UpdateSelection()
        {
            if (biomeOptions.Count == 0 || biomeItemObjects.Count == 0) return;

            // Update visual indication
            for (int i = 0; i < biomeItemObjects.Count; i++)
            {
                Image bgImage = biomeItemObjects[i].GetComponent<Image>();
                if (bgImage != null)
                {
                    bgImage.color = (i == currentSelectionIndex) ? 
                        new Color(0.2f, 0.6f, 1f, 0.8f) : 
                        new Color(0.1f, 0.1f, 0.1f, 0.7f);
                }
            }

            // Update selected biome text
            if (selectedBiomeText != null && currentSelectionIndex < biomeOptions.Count)
            {
                selectedBiomeText.text = $"{biomeOptions[currentSelectionIndex].Name}\n" +
                                        $"{biomeOptions[currentSelectionIndex].Description}";
            }
        }

        private void ApplySelectedBiome()
        {
            if (webRTCConnection == null)
            {
                Debug.LogError("BiomeController: WebRTC connection not found");
                return;
            }

            if (currentSelectionIndex >= biomeOptions.Count) return;

            BiomeOption selected = biomeOptions[currentSelectionIndex];
            Debug.Log($"BiomeController: Applying {selected.Name}");

            webRTCConnection.SendCustomPrompt(selected.PromptTemplate);
        }

        private void UpdateDescriptionText()
        {
            if (descriptionText != null)
            {
                descriptionText.text = "Use joystick up/down to browse environments. " +
                                      "Press right trigger to transform your room.";
            }
        }

        public void OnPanelOpened()
        {
            Debug.Log("BiomeController: Panel opened");
            UpdateSelection();
        }

        public void OnPanelClosed()
        {
            Debug.Log("BiomeController: Panel closed");
        }
    }
}

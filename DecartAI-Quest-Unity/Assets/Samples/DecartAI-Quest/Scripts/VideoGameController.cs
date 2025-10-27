using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Video Game Style feature - transforms environment to look like popular video games
    /// Uses Decart AI's Mirage model for artistic style transformation
    /// </summary>
    public class VideoGameController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private RectTransform gameStylesContainer;
        [SerializeField] private GameObject gameStyleItemPrefab;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text selectedGameText;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        private List<GameStyleOption> gameStyleOptions;
        private List<GameObject> gameStyleItemObjects;
        private int currentSelectionIndex = 0;

        [System.Serializable]
        private class GameStyleOption
        {
            public string Name;
            public string Description;
            public string PromptTemplate;

            public GameStyleOption(string name, string description, string template)
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

            InitializeGameStyleOptions();
            gameStyleItemObjects = new List<GameObject>();
        }

        private void InitializeGameStyleOptions()
        {
            gameStyleOptions = new List<GameStyleOption>
            {
                new GameStyleOption("Minecraft", "Blocky voxel world",
                    "Transform the environment into Minecraft style: cubic blocks, pixelated textures, voxel-based geometry, grass blocks, stone blocks, blocky trees, low-resolution textures, sandbox game aesthetic"),
                
                new GameStyleOption("The Legend of Zelda", "Cel-shaded adventure",
                    "Transform the environment into The Legend of Zelda style: cel-shaded graphics, vibrant colors, cartoon outlines, fantasy adventure aesthetic, stylized proportions, hero's journey atmosphere"),
                
                new GameStyleOption("Cyberpunk 2077", "Futuristic neon city",
                    "Transform the environment into Cyberpunk 2077 style: neon lights, holographic advertisements, futuristic tech, dystopian urban setting, rain-slicked surfaces, cyberpunk aesthetic, night city atmosphere"),
                
                new GameStyleOption("Animal Crossing", "Cute cartoon island",
                    "Transform the environment into Animal Crossing style: soft pastel colors, cute rounded shapes, friendly cartoon aesthetic, cozy village vibes, charming simplified details, wholesome atmosphere"),
                
                new GameStyleOption("Dark Souls", "Gothic dark fantasy",
                    "Transform the environment into Dark Souls style: gothic architecture, dark atmospheric lighting, medieval stone structures, ominous shadows, weathered textures, epic fantasy, challenging atmosphere"),
                
                new GameStyleOption("Borderlands", "Cell-shaded comic book",
                    "Transform the environment into Borderlands style: thick black outlines, cell-shaded rendering, comic book aesthetic, exaggerated features, bold colors, hand-drawn look, stylized action game vibe"),
                
                new GameStyleOption("Portal", "Sleek test chamber",
                    "Transform the environment into Portal style: clean white panels, testing facility aesthetic, minimalist scientific design, subtle orange and blue accents, sterile laboratory atmosphere, puzzle game setting"),
                
                new GameStyleOption("Fallout", "Post-apocalyptic wasteland",
                    "Transform the environment into Fallout style: retro-futuristic 1950s design, post-nuclear wasteland, rusted metals, vintage technology, atomic age aesthetic, survival atmosphere, desolate landscape"),
                
                new GameStyleOption("Super Mario", "Colorful platform world",
                    "Transform the environment into Super Mario style: bright primary colors, simple geometric shapes, cheerful atmosphere, platform game aesthetic, coins and blocks, playful cartoon design"),
                
                new GameStyleOption("Grand Theft Auto", "Urban open world",
                    "Transform the environment into GTA style: realistic urban environment, detailed city streets, modern architecture, street culture aesthetic, contemporary realistic graphics, open world city vibe"),
                
                new GameStyleOption("Stardew Valley", "Pixel art farm",
                    "Transform the environment into Stardew Valley style: charming pixel art, 16-bit graphics, cozy farming aesthetic, pastoral countryside, warm colors, indie game charm, relaxing atmosphere"),
                
                new GameStyleOption("Fortnite", "Vibrant battle royale",
                    "Transform the environment into Fortnite style: colorful stylized graphics, cartoonish proportions, vibrant textures, playful aesthetic, modern cartoon rendering, energetic atmosphere"),
                
                new GameStyleOption("Halo", "Sci-fi military installation",
                    "Transform the environment into Halo style: futuristic military architecture, sleek metal surfaces, alien technology, sci-fi aesthetic, military base design, epic space opera atmosphere"),
                
                new GameStyleOption("Journey", "Artistic desert expanse",
                    "Transform the environment into Journey style: minimalist artistic design, flowing sand, warm golden tones, ethereal atmosphere, emotional artistry, contemplative aesthetic, stunning visual poetry"),
                
                new GameStyleOption("Bioshock", "Art Deco underwater city",
                    "Transform the environment into Bioshock style: 1960s Art Deco design, underwater city aesthetic, retro-futuristic elements, ornate details, dystopian atmosphere, vintage luxury, steampunk elements"),
                
                new GameStyleOption("Doom", "Industrial hellscape",
                    "Transform the environment into Doom style: industrial metal architecture, demonic elements, dark sci-fi horror, aggressive design, brutal atmosphere, heavy metal aesthetic, hellish environment"),
                
                new GameStyleOption("Overwatch", "Bright hero shooter",
                    "Transform the environment into Overwatch style: clean stylized graphics, bright vibrant colors, futuristic but friendly design, optimistic sci-fi aesthetic, heroic atmosphere, polished cartoon realism"),
                
                new GameStyleOption("Resident Evil", "Survival horror mansion",
                    "Transform the environment into Resident Evil style: dark moody lighting, gothic mansion aesthetic, horror atmosphere, detailed realistic graphics, eerie shadows, survival horror setting, tense environment"),
                
                new GameStyleOption("The Sims", "Everyday life simulation",
                    "Transform the environment into The Sims style: clean simplified graphics, suburban home aesthetic, bright cheerful colors, life simulation design, everyday objects, cozy domestic atmosphere"),
                
                new GameStyleOption("Red Dead Redemption", "Wild West frontier",
                    "Transform the environment into Red Dead Redemption style: realistic western aesthetic, rustic frontier design, natural lighting, dusty atmosphere, old west architecture, authentic period details")
            };
        }

        private void Start()
        {
            CreateGameStyleItems();
            UpdateDescriptionText();
        }

        private void CreateGameStyleItems()
        {
            if (gameStyleItemPrefab == null || gameStylesContainer == null)
            {
                Debug.LogError("VideoGameController: Prefab or container not assigned");
                return;
            }

            // Clear existing items
            foreach (Transform child in gameStylesContainer)
            {
                Destroy(child.gameObject);
            }
            gameStyleItemObjects.Clear();

            // Create game style option items
            for (int i = 0; i < gameStyleOptions.Count; i++)
            {
                GameObject item = Instantiate(gameStyleItemPrefab, gameStylesContainer);
                TMP_Text itemText = item.GetComponentInChildren<TMP_Text>();
                if (itemText != null)
                {
                    itemText.text = gameStyleOptions[i].Name;
                }
                gameStyleItemObjects.Add(item);
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

            // Right trigger to confirm and apply game style
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplySelectedGameStyle();
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
            if (gameStyleOptions.Count == 0) return;

            currentSelectionIndex--;
            if (currentSelectionIndex < 0)
            {
                currentSelectionIndex = gameStyleOptions.Count - 1;
            }
            UpdateSelection();
        }

        private void NavigateDown()
        {
            if (gameStyleOptions.Count == 0) return;

            currentSelectionIndex++;
            if (currentSelectionIndex >= gameStyleOptions.Count)
            {
                currentSelectionIndex = 0;
            }
            UpdateSelection();
        }

        private void UpdateSelection()
        {
            if (gameStyleOptions.Count == 0 || gameStyleItemObjects.Count == 0) return;

            // Update visual indication
            for (int i = 0; i < gameStyleItemObjects.Count; i++)
            {
                Image bgImage = gameStyleItemObjects[i].GetComponent<Image>();
                if (bgImage != null)
                {
                    bgImage.color = (i == currentSelectionIndex) ? 
                        new Color(0.2f, 0.6f, 1f, 0.8f) : 
                        new Color(0.1f, 0.1f, 0.1f, 0.7f);
                }
            }

            // Update selected game style text
            if (selectedGameText != null && currentSelectionIndex < gameStyleOptions.Count)
            {
                selectedGameText.text = $"{gameStyleOptions[currentSelectionIndex].Name}\n" +
                                       $"{gameStyleOptions[currentSelectionIndex].Description}";
            }
        }

        private void ApplySelectedGameStyle()
        {
            if (webRTCConnection == null)
            {
                Debug.LogError("VideoGameController: WebRTC connection not found");
                return;
            }

            if (currentSelectionIndex >= gameStyleOptions.Count) return;

            GameStyleOption selected = gameStyleOptions[currentSelectionIndex];
            Debug.Log($"VideoGameController: Applying {selected.Name} style");

            webRTCConnection.SendCustomPrompt(selected.PromptTemplate);
        }

        private void UpdateDescriptionText()
        {
            if (descriptionText != null)
            {
                descriptionText.text = "Use joystick up/down to browse game styles. " +
                                      "Press right trigger to transform your environment.";
            }
        }

        public void OnPanelOpened()
        {
            Debug.Log("VideoGameController: Panel opened");
            UpdateSelection();
        }

        public void OnPanelClosed()
        {
            Debug.Log("VideoGameController: Panel closed");
        }
    }
}

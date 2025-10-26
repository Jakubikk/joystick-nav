using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Video Game Style transformation feature allowing users to view their environment
    /// as if it were rendered in different video game aesthetics.
    /// </summary>
    public class GameStyleController : MonoBehaviour
    {
        [System.Serializable]
        public class GameStyleOption
        {
            public string name;
            public string prompt;
        }

        [Header("UI References")]
        [SerializeField] private TMP_Text gameStyleNameText;
        [SerializeField] private TMP_Text gameStyleDescriptionText;
        [SerializeField] private List<TMP_Text> gameStyleMenuTexts = new List<TMP_Text>();
        
        [Header("Navigation Colors")]
        [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.8f);
        [SerializeField] private Color selectedColor = new Color(0f, 1f, 1f, 1f);
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private List<GameStyleOption> gameStyleOptions = new List<GameStyleOption>();
        private int selectedIndex = 0;
        private float navigationCooldown = 0.2f;
        private float lastNavigationTime = 0f;

        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            InitializeGameStyleOptions();
            UpdateDisplay();
        }

        private void InitializeGameStyleOptions()
        {
            gameStyleOptions = new List<GameStyleOption>
            {
                new GameStyleOption 
                { 
                    name = "Minecraft", 
                    prompt = "Transform into Minecraft style, blocky voxel world, pixelated textures, cube-shaped objects, low-poly aesthetic, Minecraft block textures, blocky terrain"
                },
                new GameStyleOption 
                { 
                    name = "Legend of Zelda", 
                    prompt = "Transform into Legend of Zelda style, vibrant cel-shaded graphics, fantasy adventure aesthetic, colorful cartoon rendering, Zelda art style, heroic fantasy atmosphere"
                },
                new GameStyleOption 
                { 
                    name = "Grand Theft Auto", 
                    prompt = "Transform into GTA style, urban crime aesthetic, saturated colors, realistic urban setting, GTA graphics style, modern city atmosphere, dramatic lighting"
                },
                new GameStyleOption 
                { 
                    name = "Fortnite", 
                    prompt = "Transform into Fortnite style, vibrant cartoon graphics, cel-shaded characters, colorful battle royale aesthetic, playful art style, bold outlines"
                },
                new GameStyleOption 
                { 
                    name = "Cyberpunk 2077", 
                    prompt = "Transform into Cyberpunk 2077 style, neon-lit dystopian city, futuristic cyberpunk aesthetic, holographic displays, neon colors, high-tech urban environment, rain-slicked streets"
                },
                new GameStyleOption 
                { 
                    name = "The Sims", 
                    prompt = "Transform into The Sims style, casual life simulation aesthetic, bright cheerful colors, simplified character designs, suburban setting, Sims art style"
                },
                new GameStyleOption 
                { 
                    name = "Super Mario", 
                    prompt = "Transform into Super Mario style, colorful Nintendo aesthetic, vibrant primary colors, whimsical cartoon world, mushroom kingdom style, playful atmosphere"
                },
                new GameStyleOption 
                { 
                    name = "Dark Souls", 
                    prompt = "Transform into Dark Souls style, dark medieval fantasy, gothic architecture, ominous atmosphere, moody lighting, stone castles, dark souls aesthetic, dramatic shadows"
                },
                new GameStyleOption 
                { 
                    name = "Animal Crossing", 
                    prompt = "Transform into Animal Crossing style, cute wholesome aesthetic, pastel colors, adorable simplified designs, cozy village atmosphere, Nintendo charm"
                },
                new GameStyleOption 
                { 
                    name = "Halo", 
                    prompt = "Transform into Halo style, sci-fi military aesthetic, futuristic alien architecture, UNSC technology, space marine atmosphere, metallic surfaces, sci-fi warfare setting"
                },
                new GameStyleOption 
                { 
                    name = "Fallout", 
                    prompt = "Transform into Fallout style, post-apocalyptic wasteland, retro-futuristic 1950s aesthetic, nuclear devastation, rusted metal, Vault-Tec style, vintage sci-fi"
                },
                new GameStyleOption 
                { 
                    name = "The Witcher", 
                    prompt = "Transform into The Witcher style, dark fantasy medieval world, Slavic folklore aesthetic, gritty realistic fantasy, monsters and magic atmosphere, medieval European setting"
                },
                new GameStyleOption 
                { 
                    name = "Final Fantasy", 
                    prompt = "Transform into Final Fantasy style, epic JRPG aesthetic, fantasy steampunk blend, crystal magic, elaborate armor designs, Final Fantasy art direction, dramatic anime style"
                },
                new GameStyleOption 
                { 
                    name = "Pokemon", 
                    prompt = "Transform into Pokemon style, vibrant anime aesthetic, cute creature companions, Japanese RPG art style, colorful world, Pokemon trainer atmosphere"
                },
                new GameStyleOption 
                { 
                    name = "Borderlands", 
                    prompt = "Transform into Borderlands style, cel-shaded comic book aesthetic, thick black outlines, comic-style rendering, hand-drawn look, vibrant wasteland colors"
                },
                new GameStyleOption 
                { 
                    name = "Red Dead Redemption", 
                    prompt = "Transform into Red Dead Redemption style, wild west aesthetic, dusty frontier setting, realistic western atmosphere, cowboy era, old west towns, rustic environment"
                },
                new GameStyleOption 
                { 
                    name = "Skyrim", 
                    prompt = "Transform into Skyrim style, Nordic fantasy world, snowy mountains, medieval fantasy architecture, Elder Scrolls aesthetic, dragon lore atmosphere, Viking-inspired setting"
                },
                new GameStyleOption 
                { 
                    name = "Assassin's Creed", 
                    prompt = "Transform into Assassin's Creed style, historical period setting, parkour-friendly architecture, ancient cities, assassin aesthetic, historical accuracy with style"
                },
                new GameStyleOption 
                { 
                    name = "Portal", 
                    prompt = "Transform into Portal style, sterile test chamber aesthetic, white panels, Aperture Science facility, minimalist sci-fi, clean laboratory environment, portal gun atmosphere"
                },
                new GameStyleOption 
                { 
                    name = "Overwatch", 
                    prompt = "Transform into Overwatch style, vibrant hero shooter aesthetic, futuristic optimistic world, colorful character designs, Blizzard art style, dynamic action atmosphere"
                },
                new GameStyleOption 
                { 
                    name = "Roblox", 
                    prompt = "Transform into Roblox style, blocky simplified characters, bright primary colors, low-poly aesthetic, playful cubic world, Lego-like characters"
                },
                new GameStyleOption 
                { 
                    name = "Among Us", 
                    prompt = "Transform into Among Us style, cute minimalist characters, simple rounded shapes, space station setting, colorful crewmate aesthetic, social deduction atmosphere"
                },
                new GameStyleOption 
                { 
                    name = "Resident Evil", 
                    prompt = "Transform into Resident Evil style, survival horror aesthetic, dark moody lighting, zombie outbreak atmosphere, abandoned facilities, biohazard warning signs, horror game mood"
                },
                new GameStyleOption 
                { 
                    name = "Street Fighter", 
                    prompt = "Transform into Street Fighter style, vibrant fighting game aesthetic, bold anime-inspired characters, dynamic action poses, martial arts atmosphere, arcade fighter style"
                },
                new GameStyleOption 
                { 
                    name = "LEGO Games", 
                    prompt = "Transform into LEGO games style, everything made of LEGO bricks, plastic toy aesthetic, colorful LEGO pieces, blocky construction, playful brick-built world"
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
                NavigateGameStyle(-1);
                lastNavigationTime = Time.time;
            }
            else if (joystickInput.y < -0.5f)
            {
                NavigateGameStyle(1);
                lastNavigationTime = Time.time;
            }
        }

        private void HandleSelection()
        {
            // Right trigger to confirm and apply game style
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyGameStyle();
            }
        }

        private void NavigateGameStyle(int direction)
        {
            selectedIndex = (selectedIndex + direction + gameStyleOptions.Count) % gameStyleOptions.Count;
            UpdateDisplay();
        }

        private void ApplyGameStyle()
        {
            if (webRtcConnection == null || selectedIndex >= gameStyleOptions.Count)
                return;

            GameStyleOption selected = gameStyleOptions[selectedIndex];
            webRtcConnection.SendCustomPrompt(selected.prompt);
            
            Debug.Log($"Applying game style: {selected.name}");
        }

        private void UpdateDisplay()
        {
            if (selectedIndex >= gameStyleOptions.Count)
                return;

            GameStyleOption selected = gameStyleOptions[selectedIndex];
            
            if (gameStyleNameText != null)
            {
                gameStyleNameText.text = selected.name;
            }
            
            if (gameStyleDescriptionText != null)
            {
                gameStyleDescriptionText.text = $"{selectedIndex + 1}/{gameStyleOptions.Count}";
            }

            UpdateMenuHighlight();
        }

        private void UpdateMenuHighlight()
        {
            for (int i = 0; i < gameStyleMenuTexts.Count && i < gameStyleOptions.Count; i++)
            {
                if (gameStyleMenuTexts[i] != null)
                {
                    gameStyleMenuTexts[i].text = gameStyleOptions[i].name;
                    gameStyleMenuTexts[i].color = (i == selectedIndex) ? selectedColor : normalColor;
                }
            }
        }

        public string GetCurrentGameStyleName()
        {
            if (selectedIndex >= 0 && selectedIndex < gameStyleOptions.Count)
            {
                return gameStyleOptions[selectedIndex].name;
            }
            return "";
        }
    }
}

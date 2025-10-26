using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Virtual Dressing Room feature allowing users to try on different clothing styles.
    /// Uses the Lucy model for person transformation while maintaining user identity.
    /// </summary>
    public class ClothingController : MonoBehaviour
    {
        [System.Serializable]
        public class ClothingOption
        {
            public string name;
            public string prompt;
        }

        [Header("UI References")]
        [SerializeField] private TMP_Text clothingNameText;
        [SerializeField] private TMP_Text clothingDescriptionText;
        [SerializeField] private List<TMP_Text> clothingMenuTexts = new List<TMP_Text>();
        
        [Header("Navigation Colors")]
        [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.8f);
        [SerializeField] private Color selectedColor = new Color(0f, 1f, 1f, 1f);
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private List<ClothingOption> clothingOptions = new List<ClothingOption>();
        private int selectedIndex = 0;
        private float navigationCooldown = 0.2f;
        private float lastNavigationTime = 0f;

        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            InitializeClothingOptions();
            UpdateDisplay();
        }

        private void InitializeClothingOptions()
        {
            clothingOptions = new List<ClothingOption>
            {
                new ClothingOption 
                { 
                    name = "Business Formal", 
                    prompt = "Change the person's outfit to a professional business suit, tailored charcoal gray suit, crisp white dress shirt, silk tie, polished leather shoes, formal professional attire"
                },
                new ClothingOption 
                { 
                    name = "Casual Streetwear", 
                    prompt = "Change the person's outfit to casual streetwear, comfortable hoodie, denim jeans, sneakers, modern urban style, relaxed fit"
                },
                new ClothingOption 
                { 
                    name = "Athletic Sportswear", 
                    prompt = "Change the person's outfit to athletic sportswear, moisture-wicking sports jersey, athletic shorts, running shoes, sporty fitness attire"
                },
                new ClothingOption 
                { 
                    name = "Formal Evening Wear", 
                    prompt = "Change the person's outfit to elegant evening wear, sophisticated evening gown or tuxedo, formal dress attire, elegant accessories, refined style"
                },
                new ClothingOption 
                { 
                    name = "Traditional Kimono", 
                    prompt = "Change the person's outfit to traditional Japanese kimono, silk fabric with intricate patterns, wide sleeves, obi belt, elegant traditional design"
                },
                new ClothingOption 
                { 
                    name = "Medieval Knight", 
                    prompt = "Change the person's outfit to medieval knight armor, metallic plate armor, chainmail, helmet with visor, armored gauntlets, knight attire"
                },
                new ClothingOption 
                { 
                    name = "Superhero Costume", 
                    prompt = "Change the person's outfit to superhero costume, bold colors, cape, mask, emblematic chest symbol, heroic athletic suit"
                },
                new ClothingOption 
                { 
                    name = "Pirate Outfit", 
                    prompt = "Change the person's outfit to pirate clothing, tricorn hat, long coat, boots, sash, swashbuckling pirate attire"
                },
                new ClothingOption 
                { 
                    name = "Doctor's Coat", 
                    prompt = "Change the person's outfit to medical professional attire, white doctor's coat, stethoscope, scrubs underneath, professional medical wear"
                },
                new ClothingOption 
                { 
                    name = "Astronaut Suit", 
                    prompt = "Change the person's outfit to astronaut space suit, white pressurized suit, helmet with gold visor, NASA patches, space exploration gear"
                },
                new ClothingOption 
                { 
                    name = "Victorian Era", 
                    prompt = "Change the person's outfit to Victorian era clothing, elaborate period dress or suit, corset or waistcoat, top hat or bonnet, Victorian style"
                },
                new ClothingOption 
                { 
                    name = "Summer Beach", 
                    prompt = "Change the person's outfit to summer beach attire, light swimwear, beach shirt or cover-up, sunglasses, casual summer style"
                },
                new ClothingOption 
                { 
                    name = "Winter Coat", 
                    prompt = "Change the person's outfit to warm winter clothing, thick parka or winter coat, scarf, gloves, winter boots, cozy winter attire"
                },
                new ClothingOption 
                { 
                    name = "Chef Uniform", 
                    prompt = "Change the person's outfit to chef's uniform, white double-breasted chef coat, chef's hat, checkered pants, professional culinary attire"
                },
                new ClothingOption 
                { 
                    name = "Rock Star", 
                    prompt = "Change the person's outfit to rock star style, leather jacket, ripped jeans, band t-shirt, edgy accessories, rock musician aesthetic"
                },
                new ClothingOption 
                { 
                    name = "Cowboy Outfit", 
                    prompt = "Change the person's outfit to western cowboy attire, cowboy hat, denim jeans, boots with spurs, plaid shirt, leather vest"
                },
                new ClothingOption 
                { 
                    name = "Cyberpunk Style", 
                    prompt = "Change the person's outfit to cyberpunk fashion, neon accents, futuristic tech wear, augmented reality glasses, high-tech street style"
                },
                new ClothingOption 
                { 
                    name = "Fantasy Wizard", 
                    prompt = "Change the person's outfit to fantasy wizard robes, flowing magical robes, pointed wizard hat, mystical staff, arcane symbols"
                },
                new ClothingOption 
                { 
                    name = "1920s Flapper", 
                    prompt = "Change the person's outfit to 1920s flapper style, fringed dress or pinstripe suit, feathered headband or fedora, art deco accessories"
                },
                new ClothingOption 
                { 
                    name = "Safari Explorer", 
                    prompt = "Change the person's outfit to safari explorer attire, khaki shirt and pants, safari hat, boots, vest with pockets, adventure gear"
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
                NavigateClothing(-1);
                lastNavigationTime = Time.time;
            }
            else if (joystickInput.y < -0.5f)
            {
                NavigateClothing(1);
                lastNavigationTime = Time.time;
            }
        }

        private void HandleSelection()
        {
            // Right trigger to confirm and apply clothing
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyClothing();
            }
        }

        private void NavigateClothing(int direction)
        {
            selectedIndex = (selectedIndex + direction + clothingOptions.Count) % clothingOptions.Count;
            UpdateDisplay();
        }

        private void ApplyClothing()
        {
            if (webRtcConnection == null || selectedIndex >= clothingOptions.Count)
                return;

            ClothingOption selected = clothingOptions[selectedIndex];
            webRtcConnection.SendCustomPrompt(selected.prompt);
            
            Debug.Log($"Applying clothing: {selected.name}");
        }

        private void UpdateDisplay()
        {
            if (selectedIndex >= clothingOptions.Count)
                return;

            ClothingOption selected = clothingOptions[selectedIndex];
            
            if (clothingNameText != null)
            {
                clothingNameText.text = selected.name;
            }
            
            if (clothingDescriptionText != null)
            {
                clothingDescriptionText.text = $"{selectedIndex + 1}/{clothingOptions.Count}";
            }

            UpdateMenuHighlight();
        }

        private void UpdateMenuHighlight()
        {
            for (int i = 0; i < clothingMenuTexts.Count && i < clothingOptions.Count; i++)
            {
                if (clothingMenuTexts[i] != null)
                {
                    clothingMenuTexts[i].text = clothingOptions[i].name;
                    clothingMenuTexts[i].color = (i == selectedIndex) ? selectedColor : normalColor;
                }
            }
        }

        public string GetCurrentClothingName()
        {
            if (selectedIndex >= 0 && selectedIndex < clothingOptions.Count)
            {
                return clothingOptions[selectedIndex].name;
            }
            return "";
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Virtual Mirror feature controller.
    /// Allows users to try on different clothing items virtually.
    /// Uses Lucy model for precise clothing transformations while preserving user identity.
    /// </summary>
    public class VirtualMirrorController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject virtualMirrorUI;
        [SerializeField] private Transform clothingOptionsContainer;
        [SerializeField] private GameObject clothingOptionPrefab;
        [SerializeField] private TMP_Text selectedClothingText;
        [SerializeField] private TMP_Text instructionsText;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        private bool isActive = false;
        private int currentClothingIndex = 0;
        private List<ClothingOption> clothingOptions = new List<ClothingOption>();
        private float inputCooldown = 0f;
        private const float INPUT_DELAY = 0.2f;

        private class ClothingOption
        {
            public string name;
            public string description;
            public string aiPrompt;
            public GameObject uiElement;
        }

        private void Awake()
        {
            if (virtualMirrorUI != null)
                virtualMirrorUI.SetActive(false);

            InitializeClothingOptions();
        }

        private void InitializeClothingOptions()
        {
            clothingOptions.Clear();

            // Professional & Formal Wear
            AddClothingOption("Business Suit (Gray)", 
                "Professional charcoal gray business suit",
                "Change the outfit to a tailored charcoal gray business suit with slim fit blazer, matching dress pants, crisp white shirt, elegant tie, polished formal look, professional corporate attire");

            AddClothingOption("Tuxedo (Black)", 
                "Elegant black tuxedo for formal events",
                "Change the outfit to a classic black tuxedo with silk lapels, bow tie, white dress shirt with cufflinks, formal evening wear, elegant and sophisticated");

            AddClothingOption("Evening Dress (Black)", 
                "Sophisticated black evening gown",
                "Dress the person in an elegant black evening gown with flowing fabric, sophisticated cut, formal evening elegance, red carpet style");

            AddClothingOption("Wedding Dress", 
                "Beautiful white wedding gown",
                "Change the outfit to a stunning white wedding dress with intricate lace details, flowing train, elegant bridal veil, pearl accents, romantic and ethereal");

            // Casual & Contemporary
            AddClothingOption("Leather Jacket & Jeans", 
                "Cool leather biker jacket with jeans",
                "Change the outfit to a black leather biker jacket with silver zippers, worn texture, blue denim jeans, casual boots, rebellious and edgy street style");

            AddClothingOption("Summer Dress (Floral)", 
                "Light and airy floral summer dress",
                "Dress the person in a light floral summer dress with pastel colors, flowing fabric, delicate flower patterns, breezy and comfortable warm weather style");

            AddClothingOption("Casual Hoodie & Joggers", 
                "Comfortable streetwear outfit",
                "Change the outfit to a comfortable gray hoodie with matching joggers, modern streetwear style, relaxed fit, trendy sneakers, urban casual look");

            AddClothingOption("Denim Jacket & Shorts", 
                "Classic denim on denim look",
                "Change to a light blue denim jacket over white t-shirt, denim shorts, casual summer vibe, laid-back California style");

            // Cultural & Traditional
            AddClothingOption("Kimono (Japanese)", 
                "Traditional Japanese silk kimono",
                "Change the outfit to a beautiful silk kimono with cherry blossom patterns, wide sleeves, traditional obi belt, elegant Japanese formal wear, intricate floral embroidery");

            AddClothingOption("Sari (Indian)", 
                "Elegant traditional Indian sari",
                "Dress the person in an elegant silk sari with golden embroidery, vibrant colors, traditional Indian draping style, ornate jewelry, cultural formal wear");

            AddClothingOption("Kilt & Highland Dress", 
                "Scottish Highland formal wear",
                "Change to traditional Scottish Highland dress with tartan kilt, sporran, dress jacket, knee-high socks, formal Celtic attire");

            // Historical & Fantasy
            AddClothingOption("Medieval Knight Armor", 
                "Full plate armor of a medieval knight",
                "Change the outfit to medieval knight's armor with metallic plate armor, chain mail, engraved details, heraldic symbols, polished steel finish, royal guard appearance");

            AddClothingOption("Victorian Gentleman", 
                "19th century Victorian gentleman's attire",
                "Change to Victorian era gentleman's outfit with tailcoat, waistcoat, cravat, top hat, walking cane, formal 1880s style, sepia tone aesthetic");

            AddClothingOption("Renaissance Noble", 
                "Luxurious Renaissance period clothing",
                "Change to Renaissance noble outfit with rich velvet doublet, puffed sleeves, ornate embroidery, ruffled collar, gold trim, royal court fashion");

            AddClothingOption("Ancient Roman Toga", 
                "Classical Roman senator's toga",
                "Change to ancient Roman toga with white draped fabric, purple stripe indicating status, laurel wreath crown, classical antiquity style, dignified senator appearance");

            // Occupational
            AddClothingOption("Chef Uniform", 
                "Professional chef's whites",
                "Dress the person in a white chef's uniform with double-breasted jacket, chef's hat (toque), black and white checkered pants, professional kitchen attire");

            AddClothingOption("Doctor's Coat", 
                "Medical professional white coat",
                "Change to professional doctor's white coat over business attire, stethoscope around neck, medical badge, clinical professional appearance");

            AddClothingOption("Police Uniform", 
                "Professional police officer uniform",
                "Change to police officer uniform with navy blue shirt, badge, duty belt, professional law enforcement attire, authoritative appearance");

            AddClothingOption("Firefighter Gear", 
                "Full firefighter protective equipment",
                "Change to firefighter protective gear with heat-resistant jacket, reflective stripes, helmet with face shield, heavy-duty boots, heroic emergency responder look");

            // Sports & Athletic
            AddClothingOption("Basketball Jersey", 
                "Professional basketball uniform",
                "Change to basketball jersey with team colors, matching shorts, athletic shoes, sporty number on front and back, professional athlete appearance");

            AddClothingOption("Soccer Kit", 
                "Professional soccer/football uniform",
                "Change to soccer kit with team jersey, shorts, long socks, cleats, athletic sportswear, professional football player look");

            AddClothingOption("Yoga Outfit", 
                "Comfortable athletic yoga wear",
                "Change to modern athletic yoga outfit with form-fitting leggings, sports top, comfortable breathable fabric, active lifestyle aesthetic");

            // Fantasy & Costume
            AddClothingOption("Superhero Cape & Suit", 
                "Classic superhero costume with cape",
                "Transform into superhero with vibrant colored spandex suit, flowing cape, emblem on chest, heroic mask, comic book style appearance");

            AddClothingOption("Wizard Robes", 
                "Mystical wizard's robes and hat",
                "Change to wizard robes with long flowing purple robe, pointed wizard hat with stars, magical staff, mystical fantasy appearance, arcane scholar look");

            AddClothingOption("Space Suit (Astronaut)", 
                "NASA astronaut space suit",
                "Change to astronaut space suit with white protective suit, helmet with gold visor, NASA patches, oxygen pack, futuristic space explorer appearance");

            AddClothingOption("Pirate Captain", 
                "Swashbuckling pirate captain outfit",
                "Change to pirate captain costume with tricorn hat with feather, long coat with gold trim, ruffled shirt, leather boots, cutlass at belt, adventurous seafarer look");
        }

        private void AddClothingOption(string name, string description, string aiPrompt)
        {
            var option = new ClothingOption
            {
                name = name,
                description = description,
                aiPrompt = aiPrompt
            };

            if (clothingOptionPrefab != null && clothingOptionsContainer != null)
            {
                option.uiElement = Instantiate(clothingOptionPrefab, clothingOptionsContainer);
                var text = option.uiElement.GetComponentInChildren<TMP_Text>();
                if (text != null)
                {
                    text.text = name;
                }
            }

            clothingOptions.Add(option);
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
                currentClothingIndex--;
                if (currentClothingIndex < 0) currentClothingIndex = clothingOptions.Count - 1;
                UpdateClothingDisplay();
                inputCooldown = INPUT_DELAY;
            }
            // Navigate down
            else if (joystickInput.y < -0.7f)
            {
                currentClothingIndex++;
                if (currentClothingIndex >= clothingOptions.Count) currentClothingIndex = 0;
                UpdateClothingDisplay();
                inputCooldown = INPUT_DELAY;
            }
        }

        private void HandleSelection()
        {
            // Right trigger to try on selected clothing
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                TryOnClothing();
                inputCooldown = INPUT_DELAY;
            }
        }

        private void UpdateClothingDisplay()
        {
            if (clothingOptions.Count == 0) return;

            // Update selection highlighting
            for (int i = 0; i < clothingOptions.Count; i++)
            {
                if (clothingOptions[i].uiElement != null)
                {
                    var text = clothingOptions[i].uiElement.GetComponentInChildren<TMP_Text>();
                    if (text != null)
                    {
                        text.color = (i == currentClothingIndex) ? Color.yellow : Color.white;
                        text.fontStyle = (i == currentClothingIndex) ? FontStyles.Bold : FontStyles.Normal;
                    }
                }
            }

            // Update selected clothing text
            if (currentClothingIndex < clothingOptions.Count)
            {
                if (selectedClothingText != null)
                {
                    selectedClothingText.text = $"<b>{clothingOptions[currentClothingIndex].name}</b>\n{clothingOptions[currentClothingIndex].description}";
                }
            }
        }

        private void TryOnClothing()
        {
            if (currentClothingIndex >= clothingOptions.Count) return;

            var selectedOption = clothingOptions[currentClothingIndex];
            
            if (webRTCConnection != null)
            {
                webRTCConnection.SendCustomPrompt(selectedOption.aiPrompt);
                Debug.Log($"VirtualMirrorController: Trying on {selectedOption.name}");
            }
            else
            {
                Debug.LogWarning("VirtualMirrorController: WebRTC connection not set");
            }
        }

        public void Activate()
        {
            isActive = true;
            if (virtualMirrorUI != null)
                virtualMirrorUI.SetActive(true);

            UpdateClothingDisplay();

            if (instructionsText != null)
            {
                instructionsText.text = "Stand in front of a mirror\nJoystick Up/Down: Browse clothing\nRight Trigger: Try on outfit";
            }

            Debug.Log("VirtualMirrorController: Activated");
        }

        public void Deactivate()
        {
            isActive = false;
            if (virtualMirrorUI != null)
                virtualMirrorUI.SetActive(false);
            
            Debug.Log("VirtualMirrorController: Deactivated");
        }
    }
}

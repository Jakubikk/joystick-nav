using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Virtual Try-On feature - allows users to try on different clothing types
    /// Uses Decart AI's Lucy model for person transformation
    /// </summary>
    public class VirtualTryOnController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private RectTransform clothingOptionsContainer;
        [SerializeField] private GameObject clothingItemPrefab;
        [SerializeField] private TMP_Text instructionText;
        [SerializeField] private TMP_Text selectedClothingText;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        private List<ClothingOption> clothingOptions;
        private List<GameObject> clothingItemObjects;
        private int currentSelectionIndex = 0;

        [System.Serializable]
        private class ClothingOption
        {
            public string Name;
            public string Description;
            public string PromptTemplate;

            public ClothingOption(string name, string description, string template)
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

            InitializeClothingOptions();
            clothingItemObjects = new List<GameObject>();
        }

        private void InitializeClothingOptions()
        {
            clothingOptions = new List<ClothingOption>
            {
                new ClothingOption("Business Suit", "Professional business attire",
                    "Change the person's clothing to a tailored charcoal gray business suit with crisp white shirt, dark tie, polished leather shoes, professional appearance"),
                
                new ClothingOption("Casual Wear", "Comfortable everyday clothing",
                    "Change the person's clothing to casual wear: comfortable jeans, plain t-shirt, sneakers, relaxed fit, modern casual style"),
                
                new ClothingOption("Formal Dress", "Elegant evening wear",
                    "Change the person's clothing to an elegant formal dress, flowing fabric, sophisticated style, evening wear with refined details"),
                
                new ClothingOption("Athletic Wear", "Sports and fitness clothing",
                    "Change the person's clothing to athletic wear: fitted sports top, athletic leggings, running shoes, moisture-wicking fabric, active wear style"),
                
                new ClothingOption("Traditional Kimono", "Japanese traditional clothing",
                    "Change the person's clothing to a traditional silk kimono with wide flowing sleeves, obi belt, cherry blossom patterns, elegant Japanese traditional style"),
                
                new ClothingOption("Medieval Armor", "Knight's battle armor",
                    "Change the person's clothing to full medieval knight armor: polished steel plate armor, chainmail underneath, metal helmet, sword and shield, heroic knight appearance"),
                
                new ClothingOption("Wizard Robes", "Magical mystical robes",
                    "Change the person's clothing to flowing wizard robes: deep purple fabric with gold trim, pointed hat, mystical symbols, long sleeves, magical appearance"),
                
                new ClothingOption("Superhero Suit", "Comic book hero costume",
                    "Change the person's clothing to a sleek superhero suit: form-fitting costume with cape, bold colors, heroic emblem on chest, mask, superhero appearance"),
                
                new ClothingOption("Cowboy Outfit", "Wild West attire",
                    "Change the person's clothing to classic cowboy outfit: worn leather vest, checkered shirt, denim jeans, cowboy boots, wide-brimmed hat, western style"),
                
                new ClothingOption("Astronaut Suit", "Space exploration gear",
                    "Change the person's clothing to a modern astronaut suit: white pressurized spacesuit, reflective helmet visor, NASA patches, life support pack, futuristic space gear"),
                
                new ClothingOption("Victorian Gown", "19th century formal wear",
                    "Change the person's clothing to elegant Victorian era gown: corseted bodice, full skirt with layers, lace details, period-appropriate jewelry, 1800s fashion"),
                
                new ClothingOption("Cyberpunk Outfit", "Futuristic street wear",
                    "Change the person's clothing to cyberpunk style: neon-accented jacket, tech-enhanced clothing, LED strips, futuristic accessories, dystopian fashion"),
                
                new ClothingOption("Chef Uniform", "Professional culinary attire",
                    "Change the person's clothing to chef uniform: crisp white double-breasted chef coat, checkered pants, professional apron, chef's hat, culinary professional look"),
                
                new ClothingOption("Pirate Costume", "Swashbuckling buccaneer",
                    "Change the person's clothing to classic pirate outfit: tricorn hat, loose white shirt, leather vest, belt with buckle, boots, eye patch, adventurous pirate style"),
                
                new ClothingOption("Beach Wear", "Summer vacation clothing",
                    "Change the person's clothing to beach wear: light summer outfit, swim trunks or swimsuit cover-up, sandals, sunglasses, relaxed vacation style")
            };
        }

        private void Start()
        {
            CreateClothingItems();
            UpdateInstructionText();
        }

        private void CreateClothingItems()
        {
            if (clothingItemPrefab == null || clothingOptionsContainer == null)
            {
                Debug.LogError("VirtualTryOnController: Prefab or container not assigned");
                return;
            }

            // Clear existing items
            foreach (Transform child in clothingOptionsContainer)
            {
                Destroy(child.gameObject);
            }
            clothingItemObjects.Clear();

            // Create clothing option items
            for (int i = 0; i < clothingOptions.Count; i++)
            {
                GameObject item = Instantiate(clothingItemPrefab, clothingOptionsContainer);
                TMP_Text itemText = item.GetComponentInChildren<TMP_Text>();
                if (itemText != null)
                {
                    itemText.text = clothingOptions[i].Name;
                }
                clothingItemObjects.Add(item);
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

            // Right trigger to confirm and apply clothing
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplySelectedClothing();
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
            if (clothingOptions.Count == 0) return;

            currentSelectionIndex--;
            if (currentSelectionIndex < 0)
            {
                currentSelectionIndex = clothingOptions.Count - 1;
            }
            UpdateSelection();
        }

        private void NavigateDown()
        {
            if (clothingOptions.Count == 0) return;

            currentSelectionIndex++;
            if (currentSelectionIndex >= clothingOptions.Count)
            {
                currentSelectionIndex = 0;
            }
            UpdateSelection();
        }

        private void UpdateSelection()
        {
            if (clothingOptions.Count == 0 || clothingItemObjects.Count == 0) return;

            // Update visual indication
            for (int i = 0; i < clothingItemObjects.Count; i++)
            {
                Image bgImage = clothingItemObjects[i].GetComponent<Image>();
                if (bgImage != null)
                {
                    bgImage.color = (i == currentSelectionIndex) ? 
                        new Color(0.2f, 0.6f, 1f, 0.8f) : 
                        new Color(0.1f, 0.1f, 0.1f, 0.7f);
                }
            }

            // Update selected clothing text
            if (selectedClothingText != null && currentSelectionIndex < clothingOptions.Count)
            {
                selectedClothingText.text = $"{clothingOptions[currentSelectionIndex].Name}\n" +
                                          $"{clothingOptions[currentSelectionIndex].Description}";
            }
        }

        private void ApplySelectedClothing()
        {
            if (webRTCConnection == null)
            {
                Debug.LogError("VirtualTryOnController: WebRTC connection not found");
                return;
            }

            if (currentSelectionIndex >= clothingOptions.Count) return;

            ClothingOption selected = clothingOptions[currentSelectionIndex];
            Debug.Log($"VirtualTryOnController: Applying {selected.Name}");

            webRTCConnection.SendCustomPrompt(selected.PromptTemplate);
        }

        private void UpdateInstructionText()
        {
            if (instructionText != null)
            {
                instructionText.text = "Stand in front of a mirror. Use joystick up/down to browse clothing. " +
                                      "Press right trigger to try on selected outfit.";
            }
        }

        public void OnPanelOpened()
        {
            Debug.Log("VirtualTryOnController: Panel opened");
            UpdateSelection();
            UpdateInstructionText();
        }

        public void OnPanelClosed()
        {
            Debug.Log("VirtualTryOnController: Panel closed");
        }
    }
}

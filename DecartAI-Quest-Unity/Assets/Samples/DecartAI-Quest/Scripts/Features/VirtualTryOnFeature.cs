using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Virtual Try-On feature - allows users to stand in front of a mirror and try on different clothing
    /// Uses Decart Lucy model for precise clothing changes while preserving identity
    /// </summary>
    public class VirtualTryOnFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text selectedClothingText;
        [SerializeField] private TMP_Text categoryText;
        [SerializeField] private TMP_Text instructionsText;
        [SerializeField] private Transform clothingListContainer;
        [SerializeField] private GameObject clothingItemPrefab;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private int currentCategoryIndex = 0;
        private int currentClothingIndex = 0;
        private float joystickCooldown = 0f;
        private const float JOYSTICK_COOLDOWN_TIME = 0.3f;
        
        private enum ClothingCategory
        {
            Tops,
            Bottoms,
            Dresses,
            Outerwear,
            Costumes,
            Accessories
        }
        
        private ClothingCategory currentCategory = ClothingCategory.Tops;
        
        // Clothing database organized by category
        private Dictionary<ClothingCategory, List<(string name, string prompt)>> clothingDatabase = new Dictionary<ClothingCategory, List<(string, string)>>()
        {
            {
                ClothingCategory.Tops, new List<(string, string)>()
                {
                    ("White T-Shirt", "Change the shirt to a plain white cotton t-shirt with crew neck, casual fit, soft fabric texture"),
                    ("Black Polo", "Change the shirt to a black polo shirt with collar, button placket, textured cotton fabric"),
                    ("Red Sweater", "Change the top to a deep red knit sweater, cable-knit texture, warm cozy appearance"),
                    ("Blue Button-Up", "Change the shirt to a light blue button-up dress shirt, crisp cotton, professional look"),
                    ("Graphic Tee", "Change the shirt to a vintage graphic t-shirt with artistic design, worn-in look, comfortable fit"),
                    ("Striped Shirt", "Change the shirt to a navy and white striped sailor shirt, horizontal stripes, casual maritime style"),
                    ("Hoodie", "Change the top to a gray pullover hoodie, soft fleece interior, drawstring hood, kangaroo pocket")
                }
            },
            {
                ClothingCategory.Bottoms, new List<(string, string)>()
                {
                    ("Blue Jeans", "Change the pants to classic blue denim jeans, slightly worn texture, straight leg fit"),
                    ("Black Slacks", "Change the pants to formal black dress slacks, crisp pressed fabric, tailored fit"),
                    ("Khaki Chinos", "Change the pants to beige khaki chinos, casual business style, slight taper at ankle"),
                    ("Cargo Pants", "Change the pants to olive green cargo pants with side pockets, military-inspired, durable fabric"),
                    ("Athletic Shorts", "Change the bottoms to black athletic shorts, moisture-wicking fabric, above-knee length"),
                    ("Denim Shorts", "Change the bottoms to light-wash denim shorts, frayed hem, casual summer style")
                }
            },
            {
                ClothingCategory.Dresses, new List<(string, string)>()
                {
                    ("Summer Dress", "Change the outfit to a light floral summer dress, flowing fabric, knee-length, pastel colors"),
                    ("Evening Gown", "Change the outfit to an elegant black evening gown, floor-length, satin fabric, sophisticated design"),
                    ("Wedding Dress", "Change the outfit to a classic white wedding dress, lace details, flowing train, romantic style"),
                    ("Cocktail Dress", "Change the outfit to a red cocktail dress, above-knee length, fitted silhouette, party-ready"),
                    ("Casual Sundress", "Change the outfit to a yellow sundress, sleeveless, cotton fabric, relaxed fit, summer vibe")
                }
            },
            {
                ClothingCategory.Outerwear, new List<(string, string)>()
                {
                    ("Leather Jacket", "Add a black leather biker jacket, silver zippers, distressed texture, cool attitude"),
                    ("Denim Jacket", "Add a classic blue denim jacket, vintage wash, button-front, casual iconic style"),
                    ("Trench Coat", "Add a beige trench coat, double-breasted, belted waist, detective movie aesthetic"),
                    ("Bomber Jacket", "Add a dark green bomber jacket, ribbed cuffs and collar, flight jacket style"),
                    ("Blazer", "Add a navy blue blazer, tailored fit, professional business look, single-breasted"),
                    ("Puffer Jacket", "Add a black puffer jacket, quilted down-filled, warm winter wear, high collar"),
                    ("Cardigan", "Add a cream-colored cardigan sweater, button-front, cozy knit, comfortable layering piece")
                }
            },
            {
                ClothingCategory.Costumes, new List<(string, string)>()
                {
                    ("Superhero - Spider-Man", "Transform the person into Spider-Man, red and blue suit, web pattern, heroic pose"),
                    ("Medieval Knight", "Change the outfit to medieval knight armor, metallic steel, engraved details, chainmail"),
                    ("Pirate Captain", "Change the outfit to pirate captain costume, tricorn hat, long coat, boots, swashbuckling style"),
                    ("Astronaut", "Change the outfit to NASA astronaut suit, white spacesuit, helmet, mission patches"),
                    ("Chef Uniform", "Change to white chef uniform with tall toque hat, double-breasted jacket, professional kitchen attire"),
                    ("Tuxedo", "Change the outfit to a classic black tuxedo, bow tie, crisp white shirt, formal elegant style"),
                    ("Anime Character", "Transform the person into a 2D anime character style with large expressive eyes"),
                    ("Victorian Gentleman", "Change to Victorian era gentleman outfit, top hat, waistcoat, pocket watch chain, formal 1800s style"),
                    ("Samurai", "Change the outfit to traditional samurai armor, Japanese warrior, ornate plating, ceremonial style")
                }
            },
            {
                ClothingCategory.Accessories, new List<(string, string)>()
                {
                    ("Sunglasses", "Add stylish aviator sunglasses on the person's face, gold frames, reflective lenses"),
                    ("Baseball Cap", "Add a navy blue baseball cap on the person's head, curved brim, casual sporty look"),
                    ("Backpack", "Add a brown leather backpack on the person's shoulders, vintage style, travel-ready"),
                    ("Scarf", "Add a red wool scarf around the person's neck, cozy winter accessory, draped elegantly"),
                    ("Watch", "Add a luxury silver watch on the person's wrist, metallic band, sophisticated timepiece"),
                    ("Necklace", "Add a gold chain necklace around the person's neck, elegant jewelry piece"),
                    ("Hat - Fedora", "Add a black fedora hat on the person's head, classic style, sophisticated brim")
                }
            }
        };
        
        private void OnEnable()
        {
            InitializeFeature();
        }
        
        private void InitializeFeature()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            // Ensure Lucy model is selected for clothing try-on
            if (webRtcConnection != null)
            {
                webRtcConnection.SetModelChoice(true); // true = Lucy model
            }
            
            currentCategory = ClothingCategory.Tops;
            currentClothingIndex = 0;
            
            UpdateDisplay();
            
            if (instructionsText != null)
            {
                instructionsText.text = "Stand in front of a mirror\n" +
                                       "Joystick LEFT/RIGHT: Change category\n" +
                                       "Joystick UP/DOWN: Select clothing\n" +
                                       "Right Trigger: Try on clothing\n" +
                                       "Left Trigger: Return to menu";
            }
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            // Update cooldown
            if (joystickCooldown > 0)
            {
                joystickCooldown -= Time.deltaTime;
            }
            
            if (joystickCooldown <= 0)
            {
                Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                // Left/Right - Change category
                if (joystick.x < -0.5f) // Left
                {
                    currentCategoryIndex--;
                    if (currentCategoryIndex < 0)
                        currentCategoryIndex = System.Enum.GetValues(typeof(ClothingCategory)).Length - 1;
                    
                    currentCategory = (ClothingCategory)currentCategoryIndex;
                    currentClothingIndex = 0;
                    UpdateDisplay();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
                else if (joystick.x > 0.5f) // Right
                {
                    currentCategoryIndex++;
                    if (currentCategoryIndex >= System.Enum.GetValues(typeof(ClothingCategory)).Length)
                        currentCategoryIndex = 0;
                    
                    currentCategory = (ClothingCategory)currentCategoryIndex;
                    currentClothingIndex = 0;
                    UpdateDisplay();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
                
                // Up/Down - Navigate clothing items
                if (joystick.y > 0.5f) // Up
                {
                    var items = clothingDatabase[currentCategory];
                    currentClothingIndex--;
                    if (currentClothingIndex < 0)
                        currentClothingIndex = items.Count - 1;
                    
                    UpdateDisplay();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
                else if (joystick.y < -0.5f) // Down
                {
                    var items = clothingDatabase[currentCategory];
                    currentClothingIndex++;
                    if (currentClothingIndex >= items.Count)
                        currentClothingIndex = 0;
                    
                    UpdateDisplay();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
            }
            
            // Right Trigger = Try on clothing
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                TryOnClothing();
            }
        }
        
        private void UpdateDisplay()
        {
            if (categoryText != null)
            {
                categoryText.text = $"Category: {currentCategory}";
            }
            
            var items = clothingDatabase[currentCategory];
            if (selectedClothingText != null && currentClothingIndex < items.Count)
            {
                var item = items[currentClothingIndex];
                selectedClothingText.text = $"{item.name}\n({currentClothingIndex + 1}/{items.Count})";
            }
        }
        
        private void TryOnClothing()
        {
            if (webRtcConnection == null)
            {
                Debug.LogError("VirtualTryOnFeature: WebRTCConnection not found!");
                return;
            }
            
            var items = clothingDatabase[currentCategory];
            if (currentClothingIndex >= items.Count)
            {
                Debug.LogError("VirtualTryOnFeature: Invalid clothing index!");
                return;
            }
            
            var selectedItem = items[currentClothingIndex];
            string prompt = selectedItem.prompt;
            
            Debug.Log($"Trying on: {selectedItem.name} - Prompt: {prompt}");
            
            // Send the prompt to Decart AI (Lucy model)
            webRtcConnection.SendCustomPrompt(prompt);
            
            // Provide visual feedback
            if (selectedClothingText != null)
            {
                selectedClothingText.text = $"{selectedItem.name}\n<color=green>✓ Trying on...</color>";
            }
        }
    }
}

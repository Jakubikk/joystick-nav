using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls virtual mirror clothing try-on feature
    /// User stands in front of mirror and can select different clothing to see themselves wearing it
    /// </summary>
    public class ClothingController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private List<TMP_Text> clothingMenuItems;
        [SerializeField] private TMP_Text selectedClothingText;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color selectedColor = Color.yellow;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Navigation Settings")]
        [SerializeField] private float joystickDeadzone = 0.5f;
        [SerializeField] private float navigationCooldown = 0.3f;
        
        private int currentClothingIndex = 0;
        private float lastNavigationTime = 0f;
        private Dictionary<string, string> clothingPrompts;
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            InitializeClothingPrompts();
            UpdateClothingHighlight();
        }
        
        private void InitializeClothingPrompts()
        {
            // Define clothing transformation prompts optimized for Decart Lucy model
            clothingPrompts = new Dictionary<string, string>
            {
                // Formal Wear
                { "Business Suit", "Change the outfit to a professional charcoal gray business suit with crisp white shirt, silk tie, tailored fit, and polished dress shoes" },
                { "Evening Gown", "Change the outfit to an elegant floor-length evening gown in deep burgundy silk with flowing fabric, delicate straps, and sophisticated draping" },
                { "Tuxedo", "Change the outfit to a classic black tuxedo with satin lapels, bow tie, white dress shirt, cummerbund, and patent leather shoes" },
                { "Cocktail Dress", "Change the outfit to a stylish cocktail dress in midnight blue with fitted bodice, knee-length skirt, and elegant accessories" },
                
                // Casual Wear
                { "Jeans & T-Shirt", "Change the outfit to casual blue jeans with a comfortable cotton t-shirt, sneakers, and relaxed fit" },
                { "Summer Dress", "Change the outfit to a light floral summer dress with flowing fabric, short sleeves, and comfortable sandals" },
                { "Hoodie & Joggers", "Change the outfit to a comfortable gray hoodie with matching jogger pants and athletic sneakers" },
                { "Casual Blazer", "Change the outfit to smart casual style with blazer over casual shirt, chinos, and loafers" },
                
                // Athletic Wear
                { "Gym Outfit", "Change the outfit to athletic wear with moisture-wicking tank top, fitted leggings, and running shoes" },
                { "Yoga Attire", "Change the outfit to yoga pants with supportive sports bra top and lightweight training shoes" },
                { "Running Gear", "Change the outfit to running shorts, breathable running shirt, and professional running shoes" },
                { "Sports Jersey", "Change the outfit to team sports jersey with athletic shorts and cleats" },
                
                // Professional Uniforms
                { "Chef Uniform", "Change the outfit to professional chef whites with double-breasted jacket, checkered pants, apron, and chef's hat" },
                { "Medical Scrubs", "Change the outfit to medical scrubs in teal color with comfortable fit, stethoscope, and medical badge" },
                { "Police Uniform", "Change the outfit to police uniform with navy blue shirt, badge, utility belt, and tactical pants" },
                { "Firefighter Gear", "Change the outfit to firefighter turnout gear with reflective stripes, helmet, and protective equipment" },
                
                // Cultural & Traditional
                { "Kimono", "Change the outfit to traditional Japanese kimono with silk fabric, elegant obi belt, intricate patterns, and wooden sandals" },
                { "Indian Sari", "Change the outfit to beautiful Indian sari with vibrant colors, gold embroidery, and traditional draping" },
                { "Scottish Kilt", "Change the outfit to traditional Scottish kilt with tartan pattern, sporran, dress shirt, and jacket" },
                { "Arabian Thobe", "Change the outfit to traditional white Arabian thobe with flowing fabric and head covering" },
                
                // Fantasy & Costume
                { "Medieval Knight", "Change the outfit to medieval knight armor with metallic plate armor, chainmail, helmet, and sword" },
                { "Wizard Robes", "Change the outfit to flowing wizard robes in deep purple with mystical symbols, pointed hat, and staff" },
                { "Pirate Captain", "Change the outfit to pirate captain attire with leather coat, tricorn hat, boots, and sword belt" },
                { "Superhero Suit", "Change the outfit to sleek superhero costume with cape, mask, and form-fitting suit in bold colors" },
                
                // Seasonal Wear
                { "Winter Coat", "Change the outfit to heavy winter coat with fur-lined hood, warm layers, scarf, gloves, and boots" },
                { "Rain Jacket", "Change the outfit to waterproof rain jacket with hood, rain pants, and rubber boots" },
                { "Beach Wear", "Change the outfit to beach attire with swimwear, light cover-up, sun hat, and flip-flops" },
                { "Ski Outfit", "Change the outfit to ski gear with insulated jacket, snow pants, goggles, and winter boots" },
                
                // Vintage Styles
                { "1920s Flapper", "Change the outfit to 1920s flapper dress with fringe, beaded details, headband, and art deco jewelry" },
                { "1950s Rockabilly", "Change the outfit to 1950s rockabilly style with poodle skirt, fitted cardigan, saddle shoes, and ponytail" },
                { "1970s Disco", "Change the outfit to 1970s disco attire with bell-bottoms, platform shoes, sequined shirt, and wide collar" },
                { "1980s Punk", "Change the outfit to 1980s punk style with leather jacket, ripped jeans, band t-shirt, and studded accessories" },
                
                // Formal Events
                { "Wedding Dress", "Change the outfit to elegant white wedding dress with lace details, long train, veil, and bridal accessories" },
                { "Prom Dress", "Change the outfit to glamorous prom dress with sequins, flowing skirt, and elegant styling" },
                { "Ball Gown", "Change the outfit to luxurious ball gown with full skirt, corset bodice, and royal styling" },
                { "Military Dress", "Change the outfit to formal military dress uniform with medals, insignia, and ceremonial details" }
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
            if (Time.time - lastNavigationTime < navigationCooldown) return;
            if (clothingMenuItems == null || clothingMenuItems.Count == 0) return;
            
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            // Up navigation
            if (joystickInput.y > joystickDeadzone)
            {
                currentClothingIndex--;
                if (currentClothingIndex < 0)
                {
                    currentClothingIndex = clothingMenuItems.Count - 1;
                }
                UpdateClothingHighlight();
                lastNavigationTime = Time.time;
            }
            // Down navigation
            else if (joystickInput.y < -joystickDeadzone)
            {
                currentClothingIndex++;
                if (currentClothingIndex >= clothingMenuItems.Count)
                {
                    currentClothingIndex = 0;
                }
                UpdateClothingHighlight();
                lastNavigationTime = Time.time;
            }
        }
        
        private void HandleSelection()
        {
            // Right trigger to select clothing
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyClothing();
            }
        }
        
        private void UpdateClothingHighlight()
        {
            if (clothingMenuItems == null) return;
            
            for (int i = 0; i < clothingMenuItems.Count; i++)
            {
                if (clothingMenuItems[i] != null)
                {
                    clothingMenuItems[i].color = (i == currentClothingIndex) ? selectedColor : normalColor;
                }
            }
            
            if (selectedClothingText != null && clothingMenuItems.Count > 0 && currentClothingIndex < clothingMenuItems.Count)
            {
                string clothingName = clothingMenuItems[currentClothingIndex].text;
                selectedClothingText.text = $"Selected: {clothingName}";
            }
        }
        
        private void ApplyClothing()
        {
            if (webRtcConnection == null || clothingMenuItems == null || clothingMenuItems.Count == 0) return;
            if (currentClothingIndex < 0 || currentClothingIndex >= clothingMenuItems.Count) return;
            
            string clothingName = clothingMenuItems[currentClothingIndex].text;
            
            if (clothingPrompts.ContainsKey(clothingName))
            {
                string prompt = clothingPrompts[clothingName];
                webRtcConnection.SendCustomPrompt(prompt);
                Debug.Log($"Virtual Mirror: Applied clothing - {clothingName}");
                
                if (selectedClothingText != null)
                {
                    selectedClothingText.text = $"Applying: {clothingName}";
                }
            }
            else
            {
                Debug.LogWarning($"Virtual Mirror: No prompt found for {clothingName}");
            }
        }
    }
}

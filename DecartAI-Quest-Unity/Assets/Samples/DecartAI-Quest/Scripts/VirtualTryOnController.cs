using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Virtual Try-On feature - allows users to try on different clothing
    /// virtually using the camera feed as a mirror.
    /// </summary>
    public class VirtualTryOnController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        [SerializeField] private TMP_Text selectedClothingText;
        [SerializeField] private TMP_Text instructionText;
        
        [Header("Clothing Menu")]
        [SerializeField] private List<Button> clothingButtons = new List<Button>();
        
        [Header("Navigation")]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color highlightedColor = Color.cyan;
        
        private int currentClothingIndex = 0;
        private float lastNavigationTime = 0f;
        private const float navigationCooldown = 0.25f;
        
        // Clothing categories and their transformation prompts
        private static readonly Dictionary<string, string> clothingOptions = new Dictionary<string, string>
        {
            // Formal Wear
            { "Tuxedo", "Change the outfit to a classic black tuxedo with bow tie, silk lapels, crisp white shirt, polished dress shoes, elegant formal attire" },
            { "Evening Gown", "Change to an elegant floor-length evening gown in deep burgundy with flowing fabric, elegant draping, sophisticated formal dress" },
            { "Business Suit", "Change to a tailored charcoal gray business suit with slim fit blazer, matching dress pants, white dress shirt, professional tie" },
            { "Cocktail Dress", "Change to a chic little black cocktail dress, knee-length, fitted silhouette, elegant party attire" },
            
            // Traditional/Cultural Wear
            { "Kimono", "Change to a traditional Japanese silk kimono with cherry blossom patterns, wide sleeves, obi belt, elegant flowing fabric" },
            { "Sari", "Change to a traditional Indian sari with vibrant silk fabric, gold embroidery, intricate patterns, draped elegantly" },
            { "Hanbok", "Change to a traditional Korean hanbok with vibrant colors, flowing skirt, elegant ribbons, cultural attire" },
            { "Kilt", "Change to traditional Scottish kilt with tartan pattern, sporran, dress jacket, complete Highland dress" },
            
            // Casual Wear
            { "Denim Jacket", "Change to a classic blue denim jacket with worn texture, vintage look, paired with casual jeans, relaxed fit" },
            { "Leather Jacket", "Change to a black leather biker jacket with silver zippers, worn textures, rugged rebellious style" },
            { "Hoodie", "Change to a comfortable oversized hoodie in gray, casual streetwear style, relaxed fit, modern urban look" },
            { "Sundress", "Change to a light floral summer sundress, flowing fabric, bright cheerful patterns, casual beach style" },
            
            // Sports/Athletic Wear
            { "Soccer Jersey", "Change to a professional soccer jersey with team colors, athletic fit, sports fabric, soccer player appearance" },
            { "Basketball Jersey", "Change to an NBA-style basketball jersey with team number, athletic shorts, basketball player look" },
            { "Yoga Outfit", "Change to fitted yoga pants and sports top, athletic activewear, fitness enthusiast style" },
            { "Running Gear", "Change to professional running attire with moisture-wicking fabric, athletic fit, runner's outfit" },
            
            // Professional Uniforms
            { "Chef Uniform", "Change to a white chef's uniform with double-breasted jacket, chef's hat, professional culinary attire" },
            { "Doctor's Coat", "Change to a white medical doctor's coat with stethoscope, professional medical attire, physician appearance" },
            { "Police Uniform", "Change to a police officer's uniform with badge, duty belt, professional law enforcement attire" },
            { "Pilot Uniform", "Change to an airline pilot's uniform with captain's hat, wings badge, professional aviation attire" },
            
            // Fantasy/Costume
            { "Medieval Knight Armor", "Change to full medieval knight's armor with metallic reflections, engraved details, chainmail, helmet" },
            { "Pirate Costume", "Change to a swashbuckling pirate outfit with tricorn hat, long coat, boots, adventurous pirate style" },
            { "Superhero Costume", "Change to a sleek superhero costume with cape, mask, bold colors, heroic appearance" },
            { "Victorian Dress", "Change to an elaborate Victorian era dress with corset, bustle, lace details, period costume" },
            
            // Modern Fashion
            { "Streetwear", "Change to trendy streetwear with designer hoodie, joggers, sneakers, urban fashion style" },
            { "Punk Rock", "Change to punk rock style with ripped jeans, studded leather jacket, band t-shirt, rebellious fashion" },
            { "Bohemian", "Change to bohemian style with flowing fabrics, ethnic patterns, layered clothing, free-spirited fashion" },
            { "Minimalist", "Change to minimalist modern fashion with clean lines, neutral colors, simple elegant design" },
            
            // Seasonal
            { "Winter Coat", "Change to a warm winter coat with fur-lined hood, heavy insulation, cold weather gear" },
            { "Raincoat", "Change to a yellow rain slicker with waterproof material, rain boots, rainy day attire" },
            { "Beach Wear", "Change to casual beach outfit with shorts, tank top, flip flops, summer vacation style" },
            { "Ski Gear", "Change to full ski gear with insulated jacket, ski pants, goggles, winter sports outfit" },
        };
        
        private List<string> clothingKeys = new List<string>();
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            // Convert dictionary keys to list for indexing
            clothingKeys = new List<string>(clothingOptions.Keys);
            
            if (instructionText != null)
            {
                instructionText.text = "Stand in front of the camera like a mirror\nUse joystick to browse clothing\nPress right trigger to try on";
            }
            
            UpdateClothingHighlight();
            UpdateSelectedClothingDisplay();
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            // Navigation with cooldown
            if (Time.time - lastNavigationTime >= navigationCooldown)
            {
                Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                if (thumbstick.y > 0.5f) // Up
                {
                    NavigatePrevious();
                    lastNavigationTime = Time.time;
                }
                else if (thumbstick.y < -0.5f) // Down
                {
                    NavigateNext();
                    lastNavigationTime = Time.time;
                }
            }
            
            // Right trigger - Try on selected clothing
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                TryOnSelectedClothing();
            }
        }
        
        private void NavigateNext()
        {
            currentClothingIndex++;
            if (currentClothingIndex >= clothingKeys.Count)
            {
                currentClothingIndex = 0;
            }
            UpdateClothingHighlight();
            UpdateSelectedClothingDisplay();
        }
        
        private void NavigatePrevious()
        {
            currentClothingIndex--;
            if (currentClothingIndex < 0)
            {
                currentClothingIndex = clothingKeys.Count - 1;
            }
            UpdateClothingHighlight();
            UpdateSelectedClothingDisplay();
        }
        
        private void UpdateClothingHighlight()
        {
            // Update button colors if we have button references
            for (int i = 0; i < clothingButtons.Count && i < clothingKeys.Count; i++)
            {
                if (clothingButtons[i] != null)
                {
                    var text = clothingButtons[i].GetComponentInChildren<TMP_Text>();
                    if (text != null)
                    {
                        text.color = (i == currentClothingIndex) ? highlightedColor : normalColor;
                    }
                }
            }
        }
        
        private void UpdateSelectedClothingDisplay()
        {
            if (selectedClothingText != null && currentClothingIndex >= 0 && currentClothingIndex < clothingKeys.Count)
            {
                selectedClothingText.text = $"Selected: {clothingKeys[currentClothingIndex]}";
            }
        }
        
        private void TryOnSelectedClothing()
        {
            if (currentClothingIndex >= 0 && currentClothingIndex < clothingKeys.Count)
            {
                string clothingName = clothingKeys[currentClothingIndex];
                string prompt = clothingOptions[clothingName];
                
                if (webRtcConnection != null)
                {
                    webRtcConnection.SendCustomPrompt(prompt);
                    Debug.Log($"VirtualTryOnController: Trying on {clothingName}: {prompt}");
                }
            }
        }
        
        public void SelectClothing(string clothingName)
        {
            int index = clothingKeys.IndexOf(clothingName);
            if (index >= 0)
            {
                currentClothingIndex = index;
                UpdateClothingHighlight();
                UpdateSelectedClothingDisplay();
            }
        }
        
        public void TryOnClothing(string clothingName)
        {
            if (clothingOptions.TryGetValue(clothingName, out string prompt))
            {
                if (webRtcConnection != null)
                {
                    webRtcConnection.SendCustomPrompt(prompt);
                    Debug.Log($"VirtualTryOnController: Trying on {clothingName}");
                }
            }
        }
    }
}

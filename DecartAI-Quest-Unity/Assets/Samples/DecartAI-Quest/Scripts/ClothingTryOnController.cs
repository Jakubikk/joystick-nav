using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Handles virtual clothing try-on feature using Lucy model for person transformation
    /// Allows users to see themselves in different clothing styles
    /// </summary>
    public class ClothingTryOnController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Transform clothingListContainer;
        [SerializeField] private GameObject clothingItemPrefab;
        [SerializeField] private TMP_Text selectedClothingText;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private List<ClothingOption> clothingOptions = new List<ClothingOption>();
        private int selectedIndex = 0;
        private float joystickCooldown = 0f;
        private const float JOYSTICK_COOLDOWN_TIME = 0.3f;
        
        private class ClothingOption
        {
            public string Name;
            public string Prompt;
            public GameObject UIElement;
        }
        
        private void OnEnable()
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
            clothingOptions.Clear();
            
            // Formal wear
            AddClothingOption("Business Suit", 
                "Change the person's outfit to a tailored business suit, charcoal gray wool, slim fit blazer, matching dress pants, crisp white dress shirt, silk tie, polished leather shoes, professional appearance");
            
            AddClothingOption("Evening Gown", 
                "Change the person's outfit to an elegant evening gown, flowing silk fabric, floor-length, rich burgundy color, delicate beading, off-shoulder design, formal and glamorous");
            
            AddClothingOption("Tuxedo", 
                "Change the person's outfit to a classic black tuxedo, satin lapels, bow tie, cummerbund, patent leather shoes, white dress shirt, cufflinks, formal evening wear");
            
            // Casual wear
            AddClothingOption("Casual Jeans & T-Shirt", 
                "Change the person's outfit to casual wear, blue denim jeans, white cotton t-shirt, comfortable sneakers, relaxed fit, everyday style");
            
            AddClothingOption("Summer Dress", 
                "Change the person's outfit to a light summer dress, floral pattern, flowing fabric, pastel colors, comfortable sandals, breezy and casual");
            
            AddClothingOption("Hoodie & Joggers", 
                "Change the person's outfit to athletic casual wear, gray hoodie, black jogger pants, white sneakers, comfortable sporty style");
            
            // Professional
            AddClothingOption("Doctor's Scrubs", 
                "Change the person's outfit to medical scrubs, blue or green surgical scrubs, comfortable medical shoes, stethoscope around neck, professional medical attire");
            
            AddClothingOption("Chef's Uniform", 
                "Change the person's outfit to professional chef whites, white double-breasted chef coat, chef's hat, black and white checkered pants, apron");
            
            // Cultural & Traditional
            AddClothingOption("Japanese Kimono", 
                "Change the person's outfit to a traditional Japanese kimono, silk fabric, cherry blossom pattern, wide flowing sleeves, obi belt, traditional wooden sandals");
            
            AddClothingOption("Scottish Kilt", 
                "Change the person's outfit to traditional Scottish attire, tartan kilt, sporran, dress shirt, jacket, knee-high socks, formal Highland dress");
            
            AddClothingOption("Indian Sari", 
                "Change the person's outfit to a traditional Indian sari, vibrant silk fabric, intricate gold embroidery, draped elegantly, colorful and ornate");
            
            // Costumes & Fantasy
            AddClothingOption("Superhero Costume", 
                "Change the person's outfit to a superhero costume, vibrant spandex suit, cape flowing behind, emblem on chest, mask, boots, heroic appearance");
            
            AddClothingOption("Medieval Knight Armor", 
                "Change the person's outfit to medieval knight armor, shining plate armor, chainmail, helmet with visor, sword at side, shield, medieval warrior");
            
            AddClothingOption("Wizard Robes", 
                "Change the person's outfit to wizard robes, flowing purple and blue robes, star patterns, pointed wizard hat, long staff in hand, mystical appearance");
            
            AddClothingOption("Pirate Outfit", 
                "Change the person's outfit to pirate costume, tricorn hat, eye patch, long coat, ruffled shirt, leather belt with sword, boots, swashbuckling style");
            
            // Sports
            AddClothingOption("Basketball Jersey", 
                "Change the person's outfit to basketball uniform, team jersey, basketball shorts, high-top sneakers, athletic wear, sporty appearance");
            
            AddClothingOption("Soccer Kit", 
                "Change the person's outfit to soccer uniform, team jersey, shorts, soccer cleats, shin guards, athletic sports attire");
            
            // Historical
            AddClothingOption("1920s Flapper Dress", 
                "Change the person's outfit to 1920s flapper dress, fringed hem, beaded details, cloche hat, pearls, art deco style, vintage glamour");
            
            AddClothingOption("Victorian Dress", 
                "Change the person's outfit to Victorian era dress, elaborate gown, corseted waist, full skirt, lace details, period-accurate 1800s fashion");
            
            // Modern Fashion
            AddClothingOption("Leather Jacket & Jeans", 
                "Change the person's outfit to edgy modern style, black leather jacket, distressed jeans, boots, rock and roll aesthetic");
            
            AddClothingOption("Bohemian Style", 
                "Change the person's outfit to bohemian fashion, flowing patterned skirt, loose blouse, layered jewelry, comfortable sandals, free-spirited style");
        }
        
        private void AddClothingOption(string name, string prompt)
        {
            clothingOptions.Add(new ClothingOption
            {
                Name = name,
                Prompt = prompt
            });
        }
        
        private void Update()
        {
            if (joystickCooldown > 0)
            {
                joystickCooldown -= Time.deltaTime;
            }
            else
            {
                Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                if (joystick.y > 0.5f) // Up
                {
                    NavigateUp();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
                else if (joystick.y < -0.5f) // Down
                {
                    NavigateDown();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
            }
            
            // Right trigger to apply selected clothing
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyClothing();
            }
        }
        
        private void NavigateUp()
        {
            selectedIndex--;
            if (selectedIndex < 0)
                selectedIndex = clothingOptions.Count - 1;
            UpdateDisplay();
        }
        
        private void NavigateDown()
        {
            selectedIndex++;
            if (selectedIndex >= clothingOptions.Count)
                selectedIndex = 0;
            UpdateDisplay();
        }
        
        private void UpdateDisplay()
        {
            // Clear existing UI
            foreach (Transform child in clothingListContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Create UI elements
            for (int i = 0; i < clothingOptions.Count; i++)
            {
                GameObject itemObj = Instantiate(clothingItemPrefab, clothingListContainer);
                TMP_Text itemText = itemObj.GetComponentInChildren<TMP_Text>();
                
                if (itemText != null)
                {
                    itemText.text = clothingOptions[i].Name;
                    
                    if (i == selectedIndex)
                    {
                        itemText.color = Color.yellow;
                        itemText.fontSize = 26;
                    }
                    else
                    {
                        itemText.color = Color.white;
                        itemText.fontSize = 22;
                    }
                }
                
                clothingOptions[i].UIElement = itemObj;
            }
            
            if (selectedClothingText != null)
            {
                selectedClothingText.text = $"Selected: {clothingOptions[selectedIndex].Name}";
            }
        }
        
        private void ApplyClothing()
        {
            if (webRtcConnection == null || selectedIndex >= clothingOptions.Count) return;
            
            string prompt = clothingOptions[selectedIndex].Prompt;
            webRtcConnection.SendCustomPrompt(prompt);
            
            Debug.Log($"Clothing Try-On: Applied {clothingOptions[selectedIndex].Name}");
        }
    }
}

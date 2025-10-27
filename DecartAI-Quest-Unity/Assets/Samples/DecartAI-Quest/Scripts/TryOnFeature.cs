using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Controls the Virtual Try-On feature allowing users to try on different clothing items virtually.
    /// Uses Decart Lucy model for person transformation.
    /// </summary>
    public class TryOnFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Transform clothingListContainer;
        [SerializeField] private GameObject clothingItemPrefab;
        [SerializeField] private TMP_Text instructionText;
        
        [Header("WebRTC Integration")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Navigation Settings")]
        [SerializeField] private float navigationCooldown = 0.25f;
        
        private List<ClothingItem> clothingItems;
        private List<GameObject> clothingUIItems;
        private int selectedIndex = 0;
        private float lastNavigationTime = 0f;
        
        [Serializable]
        private class ClothingItem
        {
            public string name;
            public string category;
            public string decartPrompt;
        }
        
        private void Start()
        {
            InitializeClothingItems();
            BuildClothingUI();
        }
        
        private void InitializeClothingItems()
        {
            clothingItems = new List<ClothingItem>
            {
                // Formal Wear
                new ClothingItem { name = "Classic Tuxedo", category = "Formal", 
                    decartPrompt = "Change the outfit to a classic black tuxedo with white dress shirt, black bow tie, satin lapels, and polished appearance" },
                new ClothingItem { name = "Elegant Evening Gown", category = "Formal", 
                    decartPrompt = "Change the outfit to an elegant floor-length evening gown with flowing fabric, sophisticated draping, and luxurious shimmer" },
                new ClothingItem { name = "Business Suit", category = "Formal", 
                    decartPrompt = "Change the outfit to a professional business suit with tailored blazer, dress pants, crisp white shirt, and elegant tie" },
                new ClothingItem { name = "Cocktail Dress", category = "Formal", 
                    decartPrompt = "Change the outfit to a stylish cocktail dress with fitted bodice, knee-length skirt, and modern elegant design" },
                
                // Casual Wear
                new ClothingItem { name = "Leather Jacket & Jeans", category = "Casual", 
                    decartPrompt = "Change the outfit to a black leather jacket with white t-shirt underneath and classic blue jeans with casual sneakers" },
                new ClothingItem { name = "Summer Dress", category = "Casual", 
                    decartPrompt = "Change the outfit to a light floral summer dress with flowing fabric, bright colors, and comfortable sandals" },
                new ClothingItem { name = "Hoodie & Joggers", category = "Casual", 
                    decartPrompt = "Change the outfit to a comfortable hoodie with matching jogger pants and athletic sneakers in a modern streetwear style" },
                new ClothingItem { name = "Denim Jacket Outfit", category = "Casual", 
                    decartPrompt = "Change the outfit to a classic denim jacket with casual t-shirt, comfortable jeans, and trendy sneakers" },
                
                // Historical Costumes
                new ClothingItem { name = "Medieval Knight Armor", category = "Historical", 
                    decartPrompt = "Change the outfit to full medieval knight armor with metallic reflections, engraved details, chainmail, and heraldic emblem" },
                new ClothingItem { name = "Victorian Gentleman", category = "Historical", 
                    decartPrompt = "Change the outfit to Victorian era gentleman attire with waistcoat, pocket watch, top hat, and formal coat" },
                new ClothingItem { name = "Renaissance Noble", category = "Historical", 
                    decartPrompt = "Change the outfit to Renaissance noble clothing with rich velvet fabrics, ornate patterns, ruffled collar, and elegant accessories" },
                new ClothingItem { name = "Ancient Roman Toga", category = "Historical", 
                    decartPrompt = "Change the outfit to ancient Roman toga with draped white fabric, golden trim, laurel wreath, and sandals" },
                
                // Fantasy & Cosplay
                new ClothingItem { name = "Wizard Robes", category = "Fantasy", 
                    decartPrompt = "Change the outfit to mystical wizard robes with flowing purple fabric, star patterns, pointed hat, and magical staff appearance" },
                new ClothingItem { name = "Superhero Suit", category = "Fantasy", 
                    decartPrompt = "Change the outfit to a sleek superhero suit with bold colors, cape, emblem on chest, and heroic design" },
                new ClothingItem { name = "Samurai Armor", category = "Fantasy", 
                    decartPrompt = "Change the outfit to traditional samurai armor with lacquered plates, ornate helmet, katana sword, and ceremonial details" },
                new ClothingItem { name = "Elven Attire", category = "Fantasy", 
                    decartPrompt = "Change the outfit to elegant elven clothing with flowing green and gold fabrics, leaf motifs, and ethereal design" },
                
                // Professional Uniforms
                new ClothingItem { name = "Chef Uniform", category = "Professional", 
                    decartPrompt = "Change the outfit to a professional white chef uniform with double-breasted jacket, tall chef hat, and apron" },
                new ClothingItem { name = "Doctor's Coat", category = "Professional", 
                    decartPrompt = "Change the outfit to a medical doctor's white lab coat with stethoscope, professional attire underneath, and name tag" },
                new ClothingItem { name = "Police Uniform", category = "Professional", 
                    decartPrompt = "Change the outfit to a police officer uniform with badge, official patches, duty belt, and professional appearance" },
                new ClothingItem { name = "Pilot Uniform", category = "Professional", 
                    decartPrompt = "Change the outfit to an airline pilot uniform with navy jacket, gold stripes, aviator wings, and captain's hat" },
                
                // Cultural & Traditional
                new ClothingItem { name = "Japanese Kimono", category = "Cultural", 
                    decartPrompt = "Change the outfit to a traditional Japanese kimono with silk fabric, cherry blossom patterns, obi belt, and elegant design" },
                new ClothingItem { name = "Indian Sari", category = "Cultural", 
                    decartPrompt = "Change the outfit to a beautiful Indian sari with vibrant colors, intricate patterns, elegant draping, and traditional jewelry" },
                new ClothingItem { name = "Scottish Kilt", category = "Cultural", 
                    decartPrompt = "Change the outfit to traditional Scottish Highland attire with tartan kilt, sporran, jacket, and knee-high socks" },
                new ClothingItem { name = "Arabian Thobe", category = "Cultural", 
                    decartPrompt = "Change the outfit to traditional Arabian thobe with flowing white fabric, decorative collar, and elegant headwear" },
                
                // Sports & Athletic
                new ClothingItem { name = "Basketball Jersey", category = "Sports", 
                    decartPrompt = "Change the outfit to a basketball jersey with team colors, number on front and back, matching shorts, and athletic shoes" },
                new ClothingItem { name = "Soccer Kit", category = "Sports", 
                    decartPrompt = "Change the outfit to a professional soccer kit with team jersey, shorts, shin guards, cleats, and sporty design" },
                new ClothingItem { name = "Tennis Outfit", category = "Sports", 
                    decartPrompt = "Change the outfit to classic tennis whites with polo shirt, pleated skirt or shorts, visor, and tennis shoes" },
                new ClothingItem { name = "Cycling Gear", category = "Sports", 
                    decartPrompt = "Change the outfit to professional cycling gear with aerodynamic jersey, padded shorts, helmet, and bright team colors" }
            };
        }
        
        private void BuildClothingUI()
        {
            clothingUIItems = new List<GameObject>();
            
            if (instructionText != null)
            {
                instructionText.text = "Stand in front of a mirror, select clothing, and press RIGHT TRIGGER to try on";
            }
            
            foreach (var item in clothingItems)
            {
                GameObject uiItem = CreateClothingUIItem(item);
                clothingUIItems.Add(uiItem);
            }
            
            UpdateSelection();
        }
        
        private GameObject CreateClothingUIItem(ClothingItem item)
        {
            GameObject itemObj = new GameObject($"Clothing_{item.name}");
            itemObj.transform.SetParent(clothingListContainer, false);
            
            RectTransform rect = itemObj.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(700, 70);
            
            TMP_Text text = itemObj.AddComponent<TextMeshProUGUI>();
            text.text = $"{item.category}: {item.name}";
            text.fontSize = 32;
            text.color = Color.white;
            text.alignment = TextAlignmentOptions.Left;
            
            return itemObj;
        }
        
        private void Update()
        {
            if (!gameObject.activeSelf) return;
            
            HandleNavigation();
            HandleSelection();
        }
        
        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown) return;
            
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            if (joystickInput.y > 0.5f)
            {
                NavigateUp();
                lastNavigationTime = Time.time;
            }
            else if (joystickInput.y < -0.5f)
            {
                NavigateDown();
                lastNavigationTime = Time.time;
            }
        }
        
        private void HandleSelection()
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyClothing();
            }
        }
        
        private void NavigateUp()
        {
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = clothingItems.Count - 1;
            }
            UpdateSelection();
        }
        
        private void NavigateDown()
        {
            selectedIndex++;
            if (selectedIndex >= clothingItems.Count)
            {
                selectedIndex = 0;
            }
            UpdateSelection();
        }
        
        private void UpdateSelection()
        {
            for (int i = 0; i < clothingUIItems.Count; i++)
            {
                TMP_Text text = clothingUIItems[i].GetComponent<TextMeshProUGUI>();
                if (text != null)
                {
                    text.color = (i == selectedIndex) ? Color.cyan : Color.white;
                    text.fontSize = (i == selectedIndex) ? 36 : 32;
                }
            }
        }
        
        private void ApplyClothing()
        {
            if (selectedIndex >= 0 && selectedIndex < clothingItems.Count)
            {
                ClothingItem item = clothingItems[selectedIndex];
                
                if (webRtcConnection != null)
                {
                    Debug.Log($"Applying clothing: {item.name}");
                    webRtcConnection.SendCustomPrompt(item.decartPrompt);
                }
                else
                {
                    Debug.LogWarning("WebRTC connection not available");
                }
            }
        }
        
        private void OnEnable()
        {
            Debug.Log("Virtual Try-On feature activated");
            selectedIndex = 0;
            if (clothingUIItems != null && clothingUIItems.Count > 0)
            {
                UpdateSelection();
            }
        }
    }
}

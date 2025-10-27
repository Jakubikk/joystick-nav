using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Virtual Mirror feature - allows trying on different clothing styles
    /// Uses Decart Lucy model for person-focused transformations
    /// </summary>
    public class VirtualMirrorController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject mirrorPanel;
        [SerializeField] private Transform clothingListContainer;
        [SerializeField] private GameObject clothingItemPrefab;
        [SerializeField] private TMP_Text categoryText;
        [SerializeField] private TMP_Text instructionText;
        
        private WebRTCConnection webRtcConnection;
        private int selectedIndex = 0;
        private List<ClothingOption> clothingOptions = new List<ClothingOption>();
        
        private class ClothingOption
        {
            public string name;
            public string description;
            public string prompt;
            public GameObject gameObject;
        }
        
        private void Awake()
        {
            InitializeClothingOptions();
        }
        
        private void InitializeClothingOptions()
        {
            clothingOptions.Clear();
            
            // Casual Wear
            clothingOptions.Add(new ClothingOption
            {
                name = "Casual T-Shirt & Jeans",
                description = "Comfortable everyday wear",
                prompt = "Change the person's outfit to casual wear, plain cotton t-shirt, blue denim jeans, sneakers, relaxed fit, modern casual style"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Hoodie & Joggers",
                description = "Sporty casual comfort",
                prompt = "Change the person's outfit to athletic casual, oversized hoodie, jogger pants, running shoes, sporty comfortable style"
            });
            
            // Formal Wear
            clothingOptions.Add(new ClothingOption
            {
                name = "Business Suit",
                description = "Professional formal attire",
                prompt = "Change the person's outfit to business formal, tailored suit, dress shirt, tie, polished leather shoes, professional appearance"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Elegant Evening Dress",
                description = "Sophisticated formal wear",
                prompt = "Change the person's outfit to elegant evening wear, flowing formal dress, sophisticated style, elegant fabric, formal shoes"
            });
            
            // Historical Costumes
            clothingOptions.Add(new ClothingOption
            {
                name = "Medieval Knight",
                description = "Armored medieval warrior",
                prompt = "Change the person's outfit to medieval knight armor, chainmail, metal plates, helmet with visor, sword at side, royal crest"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Victorian Era",
                description = "19th century elegance",
                prompt = "Change the person's outfit to Victorian era clothing, elaborate dress or suit, Victorian fashion details, top hat or bonnet, ornate patterns"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Ancient Roman",
                description = "Classical toga and sandals",
                prompt = "Change the person's outfit to ancient Roman style, white toga with purple trim, leather sandals, laurel crown, classical Roman aesthetic"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "1920s Flapper",
                description = "Roaring twenties style",
                prompt = "Change the person's outfit to 1920s flapper style, beaded dress, feather headband, pearls, art deco patterns, jazz age fashion"
            });
            
            // Fantasy & Sci-Fi
            clothingOptions.Add(new ClothingOption
            {
                name = "Wizard Robes",
                description = "Mystical magical attire",
                prompt = "Change the person's outfit to wizard robes, long flowing robe, pointed hat, mystical symbols, staff, magical appearance"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Space Suit",
                description = "Futuristic astronaut gear",
                prompt = "Change the person's outfit to futuristic space suit, high-tech spacesuit, helmet with visor, NASA-style patches, sci-fi astronaut aesthetic"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Cyberpunk Outfit",
                description = "Neon-lit future style",
                prompt = "Change the person's outfit to cyberpunk style, neon-accented jacket, tech accessories, futuristic streetwear, glowing elements, dystopian fashion"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Superhero Suit",
                description = "Comic book hero costume",
                prompt = "Change the person's outfit to superhero costume, spandex suit, cape, mask, emblem on chest, heroic appearance, comic book style"
            });
            
            // Cultural & Traditional
            clothingOptions.Add(new ClothingOption
            {
                name = "Traditional Kimono",
                description = "Japanese formal wear",
                prompt = "Change the person's outfit to traditional Japanese kimono, silk fabric, floral patterns, obi sash, geta sandals, elegant Japanese style"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Scottish Kilt",
                description = "Traditional Highland dress",
                prompt = "Change the person's outfit to Scottish Highland dress, tartan kilt, sporran, knee socks, Scottish beret, traditional Celtic style"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Indian Sari",
                description = "Traditional Indian attire",
                prompt = "Change the person's outfit to traditional Indian sari, vibrant colored fabric, ornate patterns, jewelry, elegant draping, traditional Indian style"
            });
            
            // Sports & Athletic
            clothingOptions.Add(new ClothingOption
            {
                name = "Football/Soccer Jersey",
                description = "Professional sports uniform",
                prompt = "Change the person's outfit to football soccer jersey, athletic shorts, cleats, team colors, professional sports uniform"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Basketball Uniform",
                description = "Professional basketball gear",
                prompt = "Change the person's outfit to basketball uniform, jersey tank top, athletic shorts, basketball shoes, team number, professional sports style"
            });
            
            // Professions
            clothingOptions.Add(new ClothingOption
            {
                name = "Doctor Scrubs",
                description = "Medical professional attire",
                prompt = "Change the person's outfit to medical scrubs, doctor's coat, stethoscope, professional medical appearance, hospital attire"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Chef Uniform",
                description = "Professional culinary attire",
                prompt = "Change the person's outfit to chef uniform, white chef's jacket, chef's hat, apron, professional culinary appearance"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Police Uniform",
                description = "Law enforcement attire",
                prompt = "Change the person's outfit to police uniform, badge, utility belt, police cap, professional law enforcement appearance"
            });
            
            // Character Costumes
            clothingOptions.Add(new ClothingOption
            {
                name = "Pirate Costume",
                description = "Swashbuckling seafarer",
                prompt = "Change the person's outfit to pirate costume, tricorn hat, eye patch, pirate coat, boots, sword, nautical pirate style"
            });
            
            clothingOptions.Add(new ClothingOption
            {
                name = "Cowboy Outfit",
                description = "Wild West style",
                prompt = "Change the person's outfit to cowboy style, cowboy hat, boots with spurs, vest, bandana, leather chaps, Western frontier aesthetic"
            });
        }
        
        public void Activate()
        {
            if (mirrorPanel != null)
            {
                mirrorPanel.SetActive(true);
            }
            
            MenuManager menuManager = FindFirstObjectByType<MenuManager>();
            if (menuManager != null)
            {
                webRtcConnection = menuManager.GetWebRTCConnection();
            }
            
            CreateClothingItems();
            UpdateDisplay();
            
            if (instructionText != null)
            {
                instructionText.text = "Stand in front of mirror | Joystick: Navigate | Right Trigger: Try On";
            }
        }
        
        private void OnDisable()
        {
            if (mirrorPanel != null)
            {
                mirrorPanel.SetActive(false);
            }
        }
        
        private void CreateClothingItems()
        {
            if (clothingListContainer == null) return;
            
            // Clear existing items
            foreach (Transform child in clothingListContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Create UI elements for each clothing option
            for (int i = 0; i < clothingOptions.Count; i++)
            {
                if (clothingItemPrefab != null)
                {
                    GameObject itemObj = Instantiate(clothingItemPrefab, clothingListContainer);
                    clothingOptions[i].gameObject = itemObj;
                    
                    TMP_Text nameText = itemObj.transform.Find("Name")?.GetComponent<TMP_Text>();
                    TMP_Text descText = itemObj.transform.Find("Description")?.GetComponent<TMP_Text>();
                    
                    if (nameText != null) nameText.text = clothingOptions[i].name;
                    if (descText != null) descText.text = clothingOptions[i].description;
                }
            }
        }
        
        private void UpdateDisplay()
        {
            if (categoryText != null)
            {
                categoryText.text = "Virtual Mirror - Clothing Selection";
            }
            
            for (int i = 0; i < clothingOptions.Count; i++)
            {
                if (clothingOptions[i].gameObject != null)
                {
                    Image background = clothingOptions[i].gameObject.GetComponent<Image>();
                    if (background != null)
                    {
                        background.color = (i == selectedIndex) ? 
                            new Color(0.3f, 0.5f, 0.8f, 0.8f) : 
                            new Color(0.2f, 0.2f, 0.2f, 0.6f);
                    }
                }
            }
        }
        
        public void NavigateUp()
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = clothingOptions.Count - 1;
            UpdateDisplay();
        }
        
        public void NavigateDown()
        {
            selectedIndex++;
            if (selectedIndex >= clothingOptions.Count) selectedIndex = 0;
            UpdateDisplay();
        }
        
        public void Confirm()
        {
            if (selectedIndex >= 0 && selectedIndex < clothingOptions.Count)
            {
                ClothingOption selected = clothingOptions[selectedIndex];
                if (webRtcConnection != null)
                {
                    Debug.Log($"Virtual Mirror - Trying on: {selected.name} - Prompt: {selected.prompt}");
                    webRtcConnection.SendCustomPrompt(selected.prompt);
                }
            }
        }
    }
}

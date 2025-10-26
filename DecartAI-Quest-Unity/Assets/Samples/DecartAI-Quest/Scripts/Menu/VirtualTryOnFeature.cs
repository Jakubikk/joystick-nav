using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Virtual Try-On feature - allows users to try different clothing types in front of a mirror.
    /// Uses Decart AI Lucy model for person transformation.
    /// </summary>
    public class VirtualTryOnFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject featurePanel;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text currentItemText;
        [SerializeField] private TMP_Text instructionsText;
        [SerializeField] private Transform clothingListContainer;
        [SerializeField] private GameObject listItemPrefab;

        [Header("WebRTC")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        [Header("Settings")]
        [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.6f);
        [SerializeField] private Color selectedColor = new Color(0.2f, 0.8f, 0.3f, 1f);
        [SerializeField] private float navigationCooldown = 0.2f;

        private MenuManager menuManager;
        private List<ClothingItem> clothingItems = new List<ClothingItem>();
        private int currentIndex = 0;
        private bool isActive = false;
        private float lastNavigationTime = 0f;

        private class ClothingItem
        {
            public string name;
            public string prompt;
            public GameObject uiObject;
            public TMP_Text text;
            public Image background;
        }

        // Clothing options with Decart prompts
        private Dictionary<string, string> clothingOptions = new Dictionary<string, string>()
        {
            { "Business Suit", "Change the outfit to a professional business suit with a crisp white shirt, navy blue jacket, and matching trousers, polished leather shoes" },
            { "Tuxedo", "Change the outfit to an elegant black tuxedo with satin lapels, bow tie, white dress shirt, and polished dress shoes" },
            { "Wedding Dress", "Change the outfit to a beautiful white wedding dress with lace details, flowing train, and elegant veil" },
            { "Evening Gown", "Change the outfit to an elegant black evening gown with sequins, flowing fabric, and sophisticated silhouette" },
            { "Casual Jeans & T-Shirt", "Change the outfit to casual denim jeans and a comfortable t-shirt with sneakers" },
            { "Leather Jacket Outfit", "Change the outfit to a black leather biker jacket with jeans, band t-shirt, and boots for a rock style" },
            { "Summer Dress", "Change the outfit to a light floral summer dress with sandals, breezy and colorful" },
            { "Winter Coat", "Change the outfit to a warm winter coat with scarf, gloves, boots, and layered clothing" },
            { "Sports Gear", "Change the outfit to athletic sports gear with running shoes, moisture-wicking shirt, and athletic shorts" },
            { "Chef Uniform", "Change the outfit to a white chef uniform with a tall chef's hat, apron, and professional cooking attire" },
            { "Doctor's Coat", "Change the outfit to a white doctor's lab coat with stethoscope, professional medical attire underneath" },
            { "Police Uniform", "Change the outfit to a police officer uniform with badge, utility belt, and professional law enforcement attire" },
            { "Firefighter Gear", "Change the outfit to firefighter protective gear with helmet, reflective stripes, and heavy-duty uniform" },
            { "Medieval Knight Armor", "Change the outfit to shining medieval knight armor with chainmail, metal plates, helmet, and sword" },
            { "Samurai Armor", "Change the outfit to traditional Japanese samurai armor with ornate helmet, layered plates, and katana" },
            { "Viking Warrior", "Change the outfit to Viking warrior attire with fur cape, leather armor, helmet with horns, and battle axe" },
            { "Pirate Costume", "Change the outfit to pirate costume with tricorn hat, leather boots, vest, and cutlass sword" },
            { "Wizard Robes", "Change the outfit to flowing wizard robes with stars and moons, pointed hat, and mystical staff" },
            { "Superhero Suit", "Change the outfit to a sleek superhero suit with cape, mask, and heroic emblem on the chest" },
            { "Astronaut Suit", "Change the outfit to a full NASA astronaut suit with helmet, life support pack, and space gear" },
            { "Scuba Diving Gear", "Change the outfit to complete scuba diving gear with wetsuit, oxygen tank, mask, and fins" },
            { "Traditional Kimono", "Change the outfit to an elegant Japanese kimono with obi belt, wooden sandals, and traditional patterns" },
            { "Indian Sari", "Change the outfit to a beautiful Indian sari with vibrant colors, gold embroidery, and traditional jewelry" },
            { "Scottish Kilt", "Change the outfit to traditional Scottish kilt with tartan pattern, sporran, and Highland dress" },
            { "Cowboy Outfit", "Change the outfit to Wild West cowboy attire with hat, boots, spurs, vest, and bandana" },
            { "1920s Flapper Dress", "Change the outfit to a 1920s flapper dress with fringe, sequins, headband, and long gloves" },
            { "Victorian Era Dress", "Change the outfit to Victorian era dress with corset, layered skirts, lace, and period accessories" },
            { "Gothic Style", "Change the outfit to gothic style clothing with black lace, leather, chains, and dark Victorian elements" },
            { "Steampunk Outfit", "Change the outfit to steampunk attire with brass goggles, gears, leather, corset, and Victorian sci-fi elements" },
            { "Cyberpunk Style", "Change the outfit to cyberpunk style with neon accents, tech wear, futuristic jacket, and high-tech accessories" },
        };

        private void Awake()
        {
            menuManager = FindFirstObjectByType<MenuManager>();
            
            if (webRTCConnection == null)
            {
                webRTCConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            if (featurePanel != null)
                featurePanel.SetActive(false);
        }

        public void Activate()
        {
            isActive = true;
            
            if (featurePanel != null)
                featurePanel.SetActive(true);

            if (titleText != null)
                titleText.text = "Virtual Try-On";

            if (instructionsText != null)
                instructionsText.text = "Joystick Up/Down: Navigate | Right Trigger: Try On | Left Trigger: Back";

            InitializeClothingList();
            UpdateSelection();
        }

        public void Deactivate()
        {
            isActive = false;
            
            if (featurePanel != null)
                featurePanel.SetActive(false);

            ClearClothingList();
        }

        private void InitializeClothingList()
        {
            ClearClothingList();

            foreach (var option in clothingOptions)
            {
                GameObject itemObj = Instantiate(listItemPrefab, clothingListContainer);
                TMP_Text itemText = itemObj.GetComponentInChildren<TMP_Text>();
                Image itemBg = itemObj.GetComponent<Image>();

                if (itemText != null)
                    itemText.text = option.Key;

                ClothingItem item = new ClothingItem
                {
                    name = option.Key,
                    prompt = option.Value,
                    uiObject = itemObj,
                    text = itemText,
                    background = itemBg
                };

                clothingItems.Add(item);
            }

            currentIndex = 0;
        }

        private void ClearClothingList()
        {
            foreach (var item in clothingItems)
            {
                if (item.uiObject != null)
                    Destroy(item.uiObject);
            }
            clothingItems.Clear();
        }

        private void Update()
        {
            if (!isActive)
                return;

            HandleNavigation();
            HandleSelection();
            HandleBack();
        }

        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown)
                return;

            Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            if (joystick.y > 0.5f)
            {
                currentIndex--;
                if (currentIndex < 0)
                    currentIndex = clothingItems.Count - 1;
                
                lastNavigationTime = Time.time;
                UpdateSelection();
            }
            else if (joystick.y < -0.5f)
            {
                currentIndex++;
                if (currentIndex >= clothingItems.Count)
                    currentIndex = 0;
                
                lastNavigationTime = Time.time;
                UpdateSelection();
            }
        }

        private void HandleSelection()
        {
            // Right trigger to try on clothing
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                if (currentIndex >= 0 && currentIndex < clothingItems.Count)
                {
                    ApplyClothing(clothingItems[currentIndex]);
                }
            }
        }

        private void HandleBack()
        {
            // Left trigger to go back
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (menuManager != null)
                    menuManager.ReturnToMainMenu();
            }
        }

        private void UpdateSelection()
        {
            for (int i = 0; i < clothingItems.Count; i++)
            {
                if (clothingItems[i].background != null)
                {
                    clothingItems[i].background.color = (i == currentIndex) ? selectedColor : normalColor;
                }
                
                if (clothingItems[i].text != null)
                {
                    clothingItems[i].text.fontStyle = (i == currentIndex) ? FontStyles.Bold : FontStyles.Normal;
                }
            }

            if (currentItemText != null && currentIndex >= 0 && currentIndex < clothingItems.Count)
            {
                currentItemText.text = $"Selected: {clothingItems[currentIndex].name}";
            }
        }

        private void ApplyClothing(ClothingItem item)
        {
            if (item == null || webRTCConnection == null)
                return;

            Debug.Log($"Virtual Try-On: Applying {item.name} with prompt: {item.prompt}");
            webRTCConnection.SendCustomPrompt(item.prompt);

            if (currentItemText != null)
            {
                currentItemText.text = $"Wearing: {item.name} ✓";
            }
        }
    }
}

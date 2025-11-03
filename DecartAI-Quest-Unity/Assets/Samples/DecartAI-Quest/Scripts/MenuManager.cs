using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Manages the main menu system with joystick navigation
    /// Navigation: Joystick Up/Down to select, Right Trigger to confirm, Left Trigger to go back, Hamburger to show/hide
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu References")]
        [SerializeField] private GameObject menuRootObject;
        [SerializeField] private Transform menuItemsContainer;
        [SerializeField] private TMP_Text menuTitleText;
        
        [Header("Menu Item Prefab")]
        [SerializeField] private GameObject menuItemPrefab;
        
        [Header("Submenu Panels")]
        [SerializeField] private GameObject timeTravelPanel;
        [SerializeField] private GameObject clothingPanel;
        [SerializeField] private GameObject biomePanel;
        [SerializeField] private GameObject gameWorldPanel;
        [SerializeField] private GameObject customPromptPanel;
        
        private List<MenuItemData> currentMenuItems = new List<MenuItemData>();
        private int selectedIndex = 0;
        private bool menuVisible = true;
        private MenuState currentState = MenuState.MainMenu;
        
        private float joystickCooldown = 0f;
        private const float JOYSTICK_COOLDOWN_TIME = 0.3f;
        
        private enum MenuState
        {
            MainMenu,
            TimeTravel,
            Clothing,
            Biome,
            GameWorld,
            CustomPrompt
        }
        
        private class MenuItemData
        {
            public string Name;
            public System.Action OnSelect;
            public GameObject UIElement;
        }
        
        private void Start()
        {
            InitializeMainMenu();
            UpdateMenuDisplay();
        }
        
        private void Update()
        {
            // Hamburger button (Start/Menu) to toggle menu visibility
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                ToggleMenuVisibility();
            }
            
            if (!menuVisible) return;
            
            // Handle joystick navigation with cooldown
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
            
            // Right trigger to confirm selection
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ConfirmSelection();
            }
            
            // Left trigger to go back
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                GoBack();
            }
        }
        
        private void InitializeMainMenu()
        {
            currentMenuItems.Clear();
            
            // Main menu options
            AddMenuItem("Time Travel", () => OpenTimeTravel());
            AddMenuItem("Virtual Clothing Try-On", () => OpenClothing());
            AddMenuItem("Biome Transformation", () => OpenBiome());
            AddMenuItem("Video Game World", () => OpenGameWorld());
            AddMenuItem("Custom Prompt", () => OpenCustomPrompt());
        }
        
        private void AddMenuItem(string name, System.Action onSelect)
        {
            MenuItemData item = new MenuItemData
            {
                Name = name,
                OnSelect = onSelect
            };
            currentMenuItems.Add(item);
        }
        
        private void NavigateUp()
        {
            selectedIndex--;
            if (selectedIndex < 0)
                selectedIndex = currentMenuItems.Count - 1;
            UpdateMenuDisplay();
        }
        
        private void NavigateDown()
        {
            selectedIndex++;
            if (selectedIndex >= currentMenuItems.Count)
                selectedIndex = 0;
            UpdateMenuDisplay();
        }
        
        private void ConfirmSelection()
        {
            if (currentMenuItems.Count > 0 && selectedIndex < currentMenuItems.Count)
            {
                currentMenuItems[selectedIndex].OnSelect?.Invoke();
            }
        }
        
        private void GoBack()
        {
            if (currentState != MenuState.MainMenu)
            {
                // Go back to main menu
                HideAllPanels();
                currentState = MenuState.MainMenu;
                InitializeMainMenu();
                UpdateMenuDisplay();
            }
        }
        
        private void ToggleMenuVisibility()
        {
            menuVisible = !menuVisible;
            if (menuRootObject != null)
            {
                menuRootObject.SetActive(menuVisible);
            }
        }
        
        private void UpdateMenuDisplay()
        {
            // Clear existing UI elements
            foreach (Transform child in menuItemsContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Create UI elements for current menu
            for (int i = 0; i < currentMenuItems.Count; i++)
            {
                GameObject itemObj = Instantiate(menuItemPrefab, menuItemsContainer);
                TMP_Text itemText = itemObj.GetComponentInChildren<TMP_Text>();
                
                if (itemText != null)
                {
                    itemText.text = currentMenuItems[i].Name;
                    
                    // Highlight selected item
                    if (i == selectedIndex)
                    {
                        itemText.color = Color.yellow;
                        itemText.fontSize = 28;
                    }
                    else
                    {
                        itemText.color = Color.white;
                        itemText.fontSize = 24;
                    }
                }
                
                currentMenuItems[i].UIElement = itemObj;
            }
        }
        
        private void HideAllPanels()
        {
            if (timeTravelPanel != null) timeTravelPanel.SetActive(false);
            if (clothingPanel != null) clothingPanel.SetActive(false);
            if (biomePanel != null) biomePanel.SetActive(false);
            if (gameWorldPanel != null) gameWorldPanel.SetActive(false);
            if (customPromptPanel != null) customPromptPanel.SetActive(false);
        }
        
        private void OpenTimeTravel()
        {
            currentState = MenuState.TimeTravel;
            HideAllPanels();
            if (timeTravelPanel != null)
                timeTravelPanel.SetActive(true);
        }
        
        private void OpenClothing()
        {
            currentState = MenuState.Clothing;
            HideAllPanels();
            if (clothingPanel != null)
                clothingPanel.SetActive(true);
        }
        
        private void OpenBiome()
        {
            currentState = MenuState.Biome;
            HideAllPanels();
            if (biomePanel != null)
                biomePanel.SetActive(true);
        }
        
        private void OpenGameWorld()
        {
            currentState = MenuState.GameWorld;
            HideAllPanels();
            if (gameWorldPanel != null)
                gameWorldPanel.SetActive(true);
        }
        
        private void OpenCustomPrompt()
        {
            currentState = MenuState.CustomPrompt;
            HideAllPanels();
            if (customPromptPanel != null)
                customPromptPanel.SetActive(true);
        }
    }
}

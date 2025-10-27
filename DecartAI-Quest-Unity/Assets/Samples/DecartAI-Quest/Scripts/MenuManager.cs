using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Manages the main menu navigation and feature selection for the Decart Quest app.
    /// Navigation: Left trigger = back, Right trigger = confirm, Joystick up/down = navigate, Hamburger button = toggle menu
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu UI References")]
        [SerializeField] private GameObject menuCanvas;
        [SerializeField] private TMP_Text menuTitleText;
        [SerializeField] private Transform menuItemsContainer;
        [SerializeField] private GameObject menuItemPrefab;
        
        [Header("Feature Panels")]
        [SerializeField] private GameObject timeTravelPanel;
        [SerializeField] private GameObject tryOnPanel;
        [SerializeField] private GameObject biomePanel;
        [SerializeField] private GameObject videoGamePanel;
        [SerializeField] private GameObject customPromptPanel;
        
        [Header("WebRTC Integration")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Navigation Settings")]
        [SerializeField] private float joystickDeadzone = 0.5f;
        [SerializeField] private float navigationCooldown = 0.25f;
        
        private List<MenuItem> mainMenuItems = new List<MenuItem>();
        private List<MenuItem> currentMenuItems = new List<MenuItem>();
        private int selectedIndex = 0;
        private float lastNavigationTime = 0f;
        private bool menuVisible = true;
        private MenuState currentState = MenuState.MainMenu;
        
        private enum MenuState
        {
            MainMenu,
            TimeTravel,
            TryOn,
            Biome,
            VideoGame,
            CustomPrompt
        }
        
        [Serializable]
        private class MenuItem
        {
            public string displayName;
            public MenuState targetState;
            public Action onSelect;
            public GameObject uiObject;
        }
        
        private void Start()
        {
            InitializeMenu();
            ShowMainMenu();
        }
        
        private void InitializeMenu()
        {
            // Create main menu items
            mainMenuItems = new List<MenuItem>
            {
                new MenuItem { displayName = "Time Travel", targetState = MenuState.TimeTravel },
                new MenuItem { displayName = "Virtual Try-On", targetState = MenuState.TryOn },
                new MenuItem { displayName = "Biome Transform", targetState = MenuState.Biome },
                new MenuItem { displayName = "Video Game Style", targetState = MenuState.VideoGame },
                new MenuItem { displayName = "Custom Prompt", targetState = MenuState.CustomPrompt }
            };
            
            // Hide all feature panels initially
            HideAllPanels();
        }
        
        private void Update()
        {
            HandleMenuToggle();
            
            if (!menuVisible)
            {
                return;
            }
            
            HandleNavigation();
            HandleSelection();
            HandleBack();
        }
        
        private void HandleMenuToggle()
        {
            // Hamburger button (Start button on Quest controllers)
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                menuVisible = !menuVisible;
                menuCanvas.SetActive(menuVisible);
                Debug.Log($"Menu visibility: {menuVisible}");
            }
        }
        
        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown)
            {
                return;
            }
            
            // Get joystick input (right joystick vertical axis)
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            if (Mathf.Abs(joystickInput.y) > joystickDeadzone)
            {
                if (joystickInput.y > joystickDeadzone)
                {
                    // Joystick up - move selection up
                    NavigateUp();
                    lastNavigationTime = Time.time;
                }
                else if (joystickInput.y < -joystickDeadzone)
                {
                    // Joystick down - move selection down
                    NavigateDown();
                    lastNavigationTime = Time.time;
                }
            }
        }
        
        private void HandleSelection()
        {
            // Right trigger for confirm
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ConfirmSelection();
            }
        }
        
        private void HandleBack()
        {
            // Left trigger for back
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                GoBack();
            }
        }
        
        private void NavigateUp()
        {
            if (currentMenuItems.Count == 0) return;
            
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = currentMenuItems.Count - 1;
            }
            UpdateMenuSelection();
        }
        
        private void NavigateDown()
        {
            if (currentMenuItems.Count == 0) return;
            
            selectedIndex++;
            if (selectedIndex >= currentMenuItems.Count)
            {
                selectedIndex = 0;
            }
            UpdateMenuSelection();
        }
        
        private void ConfirmSelection()
        {
            if (currentMenuItems.Count == 0 || selectedIndex >= currentMenuItems.Count)
            {
                return;
            }
            
            MenuItem selected = currentMenuItems[selectedIndex];
            
            switch (selected.targetState)
            {
                case MenuState.TimeTravel:
                    ShowTimeTravelFeature();
                    break;
                case MenuState.TryOn:
                    ShowTryOnFeature();
                    break;
                case MenuState.Biome:
                    ShowBiomeFeature();
                    break;
                case MenuState.VideoGame:
                    ShowVideoGameFeature();
                    break;
                case MenuState.CustomPrompt:
                    ShowCustomPromptFeature();
                    break;
            }
            
            selected.onSelect?.Invoke();
        }
        
        private void GoBack()
        {
            if (currentState != MenuState.MainMenu)
            {
                ShowMainMenu();
            }
        }
        
        private void UpdateMenuSelection()
        {
            for (int i = 0; i < currentMenuItems.Count; i++)
            {
                if (currentMenuItems[i].uiObject != null)
                {
                    // Visual feedback for selected item
                    TMP_Text text = currentMenuItems[i].uiObject.GetComponentInChildren<TMP_Text>();
                    if (text != null)
                    {
                        text.color = (i == selectedIndex) ? Color.yellow : Color.white;
                        text.fontSize = (i == selectedIndex) ? 48 : 40;
                    }
                }
            }
        }
        
        private void ShowMainMenu()
        {
            currentState = MenuState.MainMenu;
            HideAllPanels();
            
            menuTitleText.text = "Decart XR - Main Menu";
            currentMenuItems = mainMenuItems;
            selectedIndex = 0;
            
            RebuildMenuUI();
        }
        
        private void RebuildMenuUI()
        {
            // Clear existing menu items
            foreach (Transform child in menuItemsContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Create new menu items
            for (int i = 0; i < currentMenuItems.Count; i++)
            {
                GameObject itemObj = CreateMenuItem(currentMenuItems[i].displayName);
                currentMenuItems[i].uiObject = itemObj;
            }
            
            UpdateMenuSelection();
        }
        
        private GameObject CreateMenuItem(string text)
        {
            GameObject item = new GameObject($"MenuItem_{text}");
            item.transform.SetParent(menuItemsContainer, false);
            
            // Add RectTransform
            RectTransform rect = item.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(800, 80);
            
            // Add Text
            TMP_Text tmpText = item.AddComponent<TextMeshProUGUI>();
            tmpText.text = text;
            tmpText.fontSize = 40;
            tmpText.color = Color.white;
            tmpText.alignment = TextAlignmentOptions.Center;
            
            return item;
        }
        
        private void HideAllPanels()
        {
            if (timeTravelPanel != null) timeTravelPanel.SetActive(false);
            if (tryOnPanel != null) tryOnPanel.SetActive(false);
            if (biomePanel != null) biomePanel.SetActive(false);
            if (videoGamePanel != null) videoGamePanel.SetActive(false);
            if (customPromptPanel != null) customPromptPanel.SetActive(false);
        }
        
        private void ShowTimeTravelFeature()
        {
            currentState = MenuState.TimeTravel;
            HideAllPanels();
            if (timeTravelPanel != null)
            {
                timeTravelPanel.SetActive(true);
            }
            menuTitleText.text = "Time Travel";
        }
        
        private void ShowTryOnFeature()
        {
            currentState = MenuState.TryOn;
            HideAllPanels();
            if (tryOnPanel != null)
            {
                tryOnPanel.SetActive(true);
            }
            menuTitleText.text = "Virtual Try-On";
        }
        
        private void ShowBiomeFeature()
        {
            currentState = MenuState.Biome;
            HideAllPanels();
            if (biomePanel != null)
            {
                biomePanel.SetActive(true);
            }
            menuTitleText.text = "Biome Transform";
        }
        
        private void ShowVideoGameFeature()
        {
            currentState = MenuState.VideoGame;
            HideAllPanels();
            if (videoGamePanel != null)
            {
                videoGamePanel.SetActive(true);
            }
            menuTitleText.text = "Video Game Style";
        }
        
        private void ShowCustomPromptFeature()
        {
            currentState = MenuState.CustomPrompt;
            HideAllPanels();
            if (customPromptPanel != null)
            {
                customPromptPanel.SetActive(true);
            }
            menuTitleText.text = "Custom Prompt";
        }
    }
}

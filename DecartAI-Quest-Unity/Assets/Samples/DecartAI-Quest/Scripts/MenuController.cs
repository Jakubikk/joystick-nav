using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Main menu controller for Meta Quest 3 XR application
    /// Navigation: Joystick up/down to navigate, Right trigger to confirm, Left trigger to go back, Hamburger to toggle menu
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        [Header("Menu References")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject timeTravelPanel;
        [SerializeField] private GameObject clothingPanel;
        [SerializeField] private GameObject biomePanel;
        [SerializeField] private GameObject videoGamePanel;
        [SerializeField] private GameObject customPromptPanel;
        
        [Header("Main Menu Items")]
        [SerializeField] private List<TMP_Text> menuItems;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color selectedColor = Color.yellow;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("UI Settings")]
        [SerializeField] private float joystickDeadzone = 0.5f;
        [SerializeField] private float navigationCooldown = 0.3f;
        
        private int currentMenuIndex = 0;
        private float lastNavigationTime = 0f;
        private bool menuVisible = true;
        private MenuState currentState = MenuState.MainMenu;
        
        private enum MenuState
        {
            MainMenu,
            TimeTravel,
            Clothing,
            Biome,
            VideoGame,
            CustomPrompt
        }
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            InitializeMenus();
            ShowMainMenu();
        }
        
        private void InitializeMenus()
        {
            // Hide all panels initially
            if (timeTravelPanel) timeTravelPanel.SetActive(false);
            if (clothingPanel) clothingPanel.SetActive(false);
            if (biomePanel) biomePanel.SetActive(false);
            if (videoGamePanel) videoGamePanel.SetActive(false);
            if (customPromptPanel) customPromptPanel.SetActive(false);
            
            UpdateMenuHighlight();
        }
        
        private void Update()
        {
            HandleMenuToggle();
            
            if (!menuVisible) return;
            
            HandleNavigation();
            HandleConfirm();
            HandleBack();
        }
        
        private void HandleMenuToggle()
        {
            // Hamburger button (Start button on Quest) to toggle menu visibility
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                menuVisible = !menuVisible;
                
                if (mainMenuPanel)
                {
                    mainMenuPanel.SetActive(menuVisible);
                }
                
                // Hide all sub-panels when toggling menu
                if (!menuVisible)
                {
                    HideAllSubPanels();
                }
            }
        }
        
        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown) return;
            
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            // Up navigation
            if (joystickInput.y > joystickDeadzone)
            {
                NavigateUp();
                lastNavigationTime = Time.time;
            }
            // Down navigation
            else if (joystickInput.y < -joystickDeadzone)
            {
                NavigateDown();
                lastNavigationTime = Time.time;
            }
        }
        
        private void HandleConfirm()
        {
            // Right trigger to confirm selection
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ConfirmSelection();
            }
        }
        
        private void HandleBack()
        {
            // Left trigger to go back
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                GoBack();
            }
        }
        
        private void NavigateUp()
        {
            if (currentState == MenuState.MainMenu && menuItems != null && menuItems.Count > 0)
            {
                currentMenuIndex--;
                if (currentMenuIndex < 0)
                {
                    currentMenuIndex = menuItems.Count - 1;
                }
                UpdateMenuHighlight();
            }
        }
        
        private void NavigateDown()
        {
            if (currentState == MenuState.MainMenu && menuItems != null && menuItems.Count > 0)
            {
                currentMenuIndex++;
                if (currentMenuIndex >= menuItems.Count)
                {
                    currentMenuIndex = 0;
                }
                UpdateMenuHighlight();
            }
        }
        
        private void UpdateMenuHighlight()
        {
            if (menuItems == null) return;
            
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i] != null)
                {
                    menuItems[i].color = (i == currentMenuIndex) ? selectedColor : normalColor;
                }
            }
        }
        
        private void ConfirmSelection()
        {
            if (currentState == MenuState.MainMenu)
            {
                switch (currentMenuIndex)
                {
                    case 0: // Time Travel
                        ShowTimeTravel();
                        break;
                    case 1: // Virtual Mirror
                        ShowClothing();
                        break;
                    case 2: // Biome/Country
                        ShowBiome();
                        break;
                    case 3: // Video Game Style
                        ShowVideoGame();
                        break;
                    case 4: // Custom Prompt
                        ShowCustomPrompt();
                        break;
                }
            }
        }
        
        private void GoBack()
        {
            if (currentState != MenuState.MainMenu)
            {
                ShowMainMenu();
            }
        }
        
        private void ShowMainMenu()
        {
            currentState = MenuState.MainMenu;
            HideAllSubPanels();
            
            if (mainMenuPanel)
            {
                mainMenuPanel.SetActive(true);
            }
            
            UpdateMenuHighlight();
        }
        
        private void ShowTimeTravel()
        {
            currentState = MenuState.TimeTravel;
            HideAllSubPanels();
            
            if (timeTravelPanel)
            {
                timeTravelPanel.SetActive(true);
            }
            
            if (mainMenuPanel)
            {
                mainMenuPanel.SetActive(false);
            }
        }
        
        private void ShowClothing()
        {
            currentState = MenuState.Clothing;
            HideAllSubPanels();
            
            if (clothingPanel)
            {
                clothingPanel.SetActive(true);
            }
            
            if (mainMenuPanel)
            {
                mainMenuPanel.SetActive(false);
            }
        }
        
        private void ShowBiome()
        {
            currentState = MenuState.Biome;
            HideAllSubPanels();
            
            if (biomePanel)
            {
                biomePanel.SetActive(true);
            }
            
            if (mainMenuPanel)
            {
                mainMenuPanel.SetActive(false);
            }
        }
        
        private void ShowVideoGame()
        {
            currentState = MenuState.VideoGame;
            HideAllSubPanels();
            
            if (videoGamePanel)
            {
                videoGamePanel.SetActive(true);
            }
            
            if (mainMenuPanel)
            {
                mainMenuPanel.SetActive(false);
            }
        }
        
        private void ShowCustomPrompt()
        {
            currentState = MenuState.CustomPrompt;
            HideAllSubPanels();
            
            if (customPromptPanel)
            {
                customPromptPanel.SetActive(true);
            }
            
            if (mainMenuPanel)
            {
                mainMenuPanel.SetActive(false);
            }
        }
        
        private void HideAllSubPanels()
        {
            if (timeTravelPanel) timeTravelPanel.SetActive(false);
            if (clothingPanel) clothingPanel.SetActive(false);
            if (biomePanel) biomePanel.SetActive(false);
            if (videoGamePanel) videoGamePanel.SetActive(false);
            if (customPromptPanel) customPromptPanel.SetActive(false);
        }
    }
}

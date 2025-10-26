using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Manages the menu system with joystick navigation for the Quest application.
    /// Controls: Left Trigger (back), Right Trigger (confirm), Joystick Up/Down (navigate), Hamburger button (toggle menu)
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu References")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject timeTravelPanel;
        [SerializeField] private GameObject virtualTryOnPanel;
        [SerializeField] private GameObject biomePanel;
        [SerializeField] private GameObject videoGamePanel;
        [SerializeField] private GameObject customPromptPanel;
        
        [Header("Main Menu Items")]
        [SerializeField] private List<Button> mainMenuButtons = new List<Button>();
        [SerializeField] private TMP_Text statusText;
        
        [Header("Navigation")]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color highlightedColor = Color.yellow;
        [SerializeField] private float navigationCooldown = 0.25f;
        
        private int currentMenuIndex = 0;
        private float lastNavigationTime = 0f;
        private bool menuVisible = true;
        private MenuState currentState = MenuState.MainMenu;
        
        private enum MenuState
        {
            MainMenu,
            TimeTravel,
            VirtualTryOn,
            Biome,
            VideoGame,
            CustomPrompt
        }
        
        private void Start()
        {
            InitializeMenu();
            ShowMainMenu();
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void InitializeMenu()
        {
            // Hide all panels except main menu initially
            if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
            if (timeTravelPanel != null) timeTravelPanel.SetActive(false);
            if (virtualTryOnPanel != null) virtualTryOnPanel.SetActive(false);
            if (biomePanel != null) biomePanel.SetActive(false);
            if (videoGamePanel != null) videoGamePanel.SetActive(false);
            if (customPromptPanel != null) customPromptPanel.SetActive(false);
            
            // Highlight first menu item
            UpdateMenuHighlight();
        }
        
        private void HandleInput()
        {
            // Hamburger button (Start button on Quest) - Toggle menu visibility
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                ToggleMenuVisibility();
            }
            
            if (!menuVisible) return;
            
            // Navigation with cooldown
            if (Time.time - lastNavigationTime >= navigationCooldown)
            {
                // Joystick Up/Down for navigation
                Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                if (thumbstick.y > 0.5f) // Up
                {
                    NavigateUp();
                    lastNavigationTime = Time.time;
                }
                else if (thumbstick.y < -0.5f) // Down
                {
                    NavigateDown();
                    lastNavigationTime = Time.time;
                }
            }
            
            // Right Trigger - Confirm
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                ConfirmSelection();
            }
            
            // Left Trigger - Back
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                GoBack();
            }
        }
        
        private void NavigateUp()
        {
            if (currentState == MenuState.MainMenu && mainMenuButtons.Count > 0)
            {
                currentMenuIndex--;
                if (currentMenuIndex < 0) currentMenuIndex = mainMenuButtons.Count - 1;
                UpdateMenuHighlight();
            }
        }
        
        private void NavigateDown()
        {
            if (currentState == MenuState.MainMenu && mainMenuButtons.Count > 0)
            {
                currentMenuIndex++;
                if (currentMenuIndex >= mainMenuButtons.Count) currentMenuIndex = 0;
                UpdateMenuHighlight();
            }
        }
        
        private void UpdateMenuHighlight()
        {
            if (currentState != MenuState.MainMenu) return;
            
            for (int i = 0; i < mainMenuButtons.Count; i++)
            {
                if (mainMenuButtons[i] != null)
                {
                    var text = mainMenuButtons[i].GetComponentInChildren<TMP_Text>();
                    if (text != null)
                    {
                        text.color = (i == currentMenuIndex) ? highlightedColor : normalColor;
                    }
                }
            }
        }
        
        private void ConfirmSelection()
        {
            if (currentState == MenuState.MainMenu && mainMenuButtons.Count > 0)
            {
                mainMenuButtons[currentMenuIndex].onClick.Invoke();
            }
        }
        
        private void GoBack()
        {
            switch (currentState)
            {
                case MenuState.MainMenu:
                    // Already at main menu, do nothing or exit app
                    break;
                    
                case MenuState.TimeTravel:
                case MenuState.VirtualTryOn:
                case MenuState.Biome:
                case MenuState.VideoGame:
                case MenuState.CustomPrompt:
                    ShowMainMenu();
                    break;
            }
        }
        
        private void ToggleMenuVisibility()
        {
            menuVisible = !menuVisible;
            
            if (mainMenuPanel != null) mainMenuPanel.SetActive(menuVisible && currentState == MenuState.MainMenu);
            if (timeTravelPanel != null) timeTravelPanel.SetActive(menuVisible && currentState == MenuState.TimeTravel);
            if (virtualTryOnPanel != null) virtualTryOnPanel.SetActive(menuVisible && currentState == MenuState.VirtualTryOn);
            if (biomePanel != null) biomePanel.SetActive(menuVisible && currentState == MenuState.Biome);
            if (videoGamePanel != null) videoGamePanel.SetActive(menuVisible && currentState == MenuState.VideoGame);
            if (customPromptPanel != null) customPromptPanel.SetActive(menuVisible && currentState == MenuState.CustomPrompt);
            
            if (statusText != null)
            {
                statusText.text = menuVisible ? "" : "Press Start to show menu";
            }
        }
        
        public void ShowMainMenu()
        {
            currentState = MenuState.MainMenu;
            currentMenuIndex = 0;
            
            if (mainMenuPanel != null) mainMenuPanel.SetActive(menuVisible);
            if (timeTravelPanel != null) timeTravelPanel.SetActive(false);
            if (virtualTryOnPanel != null) virtualTryOnPanel.SetActive(false);
            if (biomePanel != null) biomePanel.SetActive(false);
            if (videoGamePanel != null) videoGamePanel.SetActive(false);
            if (customPromptPanel != null) customPromptPanel.SetActive(false);
            
            UpdateMenuHighlight();
        }
        
        public void ShowTimeTravel()
        {
            currentState = MenuState.TimeTravel;
            
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (timeTravelPanel != null) timeTravelPanel.SetActive(menuVisible);
        }
        
        public void ShowVirtualTryOn()
        {
            currentState = MenuState.VirtualTryOn;
            
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (virtualTryOnPanel != null) virtualTryOnPanel.SetActive(menuVisible);
        }
        
        public void ShowBiome()
        {
            currentState = MenuState.Biome;
            
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (biomePanel != null) biomePanel.SetActive(menuVisible);
        }
        
        public void ShowVideoGame()
        {
            currentState = MenuState.VideoGame;
            
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (videoGamePanel != null) videoGamePanel.SetActive(menuVisible);
        }
        
        public void ShowCustomPrompt()
        {
            currentState = MenuState.CustomPrompt;
            
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (customPromptPanel != null) customPromptPanel.SetActive(menuVisible);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Manages the main menu navigation and feature selection for the DecartXR app.
    /// Handles joystick navigation (up/down), trigger actions (confirm/back), and menu visibility.
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        public enum MenuState
        {
            MainMenu,
            TimeTravel,
            Clothing,
            Biome,
            GameStyle,
            CustomInput
        }

        [Header("UI References")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject timeTravelPanel;
        [SerializeField] private GameObject clothingPanel;
        [SerializeField] private GameObject biomePanel;
        [SerializeField] private GameObject gameStylePanel;
        [SerializeField] private GameObject customInputPanel;
        
        [Header("Main Menu Items")]
        [SerializeField] private List<Button> mainMenuButtons = new List<Button>();
        [SerializeField] private TMP_Text[] mainMenuTexts;
        
        [Header("Navigation Colors")]
        [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.8f);
        [SerializeField] private Color selectedColor = new Color(0f, 1f, 1f, 1f);
        
        private MenuState currentState = MenuState.MainMenu;
        private int selectedIndex = 0;
        private bool menuVisible = true;
        private float navigationCooldown = 0.2f;
        private float lastNavigationTime = 0f;

        private void Start()
        {
            ShowMenu(MenuState.MainMenu);
            UpdateMenuSelection();
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
            // Hamburger button (Menu button on Quest controllers)
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                menuVisible = !menuVisible;
                SetAllPanelsActive(menuVisible);
            }
        }

        private void HandleNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown)
                return;

            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            // Joystick up - navigate up
            if (joystickInput.y > 0.5f)
            {
                NavigateMenu(-1);
                lastNavigationTime = Time.time;
            }
            // Joystick down - navigate down
            else if (joystickInput.y < -0.5f)
            {
                NavigateMenu(1);
                lastNavigationTime = Time.time;
            }
        }

        private void HandleConfirm()
        {
            // Right trigger - confirm selection
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ConfirmSelection();
            }
        }

        private void HandleBack()
        {
            // Left trigger - go back
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                GoBack();
            }
        }

        private void NavigateMenu(int direction)
        {
            int itemCount = GetCurrentMenuItemCount();
            selectedIndex = (selectedIndex + direction + itemCount) % itemCount;
            UpdateMenuSelection();
        }

        private void ConfirmSelection()
        {
            switch (currentState)
            {
                case MenuState.MainMenu:
                    OpenSubMenu(selectedIndex);
                    break;
                case MenuState.TimeTravel:
                case MenuState.Clothing:
                case MenuState.Biome:
                case MenuState.GameStyle:
                case MenuState.CustomInput:
                    SelectSubMenuItem();
                    break;
            }
        }

        private void GoBack()
        {
            if (currentState != MenuState.MainMenu)
            {
                ShowMenu(MenuState.MainMenu);
                selectedIndex = 0;
                UpdateMenuSelection();
            }
        }

        private void OpenSubMenu(int index)
        {
            MenuState newState = MenuState.MainMenu;
            
            switch (index)
            {
                case 0:
                    newState = MenuState.TimeTravel;
                    break;
                case 1:
                    newState = MenuState.Clothing;
                    break;
                case 2:
                    newState = MenuState.Biome;
                    break;
                case 3:
                    newState = MenuState.GameStyle;
                    break;
                case 4:
                    newState = MenuState.CustomInput;
                    break;
            }
            
            ShowMenu(newState);
            selectedIndex = 0;
            UpdateMenuSelection();
        }

        private void SelectSubMenuItem()
        {
            // This will be handled by specific feature managers
            // Send event or call method on the appropriate feature controller
        }

        private void ShowMenu(MenuState state)
        {
            currentState = state;
            
            // Hide all panels first
            mainMenuPanel?.SetActive(false);
            timeTravelPanel?.SetActive(false);
            clothingPanel?.SetActive(false);
            biomePanel?.SetActive(false);
            gameStylePanel?.SetActive(false);
            customInputPanel?.SetActive(false);
            
            // Show the selected panel
            switch (state)
            {
                case MenuState.MainMenu:
                    mainMenuPanel?.SetActive(true);
                    break;
                case MenuState.TimeTravel:
                    timeTravelPanel?.SetActive(true);
                    break;
                case MenuState.Clothing:
                    clothingPanel?.SetActive(true);
                    break;
                case MenuState.Biome:
                    biomePanel?.SetActive(true);
                    break;
                case MenuState.GameStyle:
                    gameStylePanel?.SetActive(true);
                    break;
                case MenuState.CustomInput:
                    customInputPanel?.SetActive(true);
                    break;
            }
        }

        private void SetAllPanelsActive(bool active)
        {
            if (currentState == MenuState.MainMenu)
                mainMenuPanel?.SetActive(active);
            else if (currentState == MenuState.TimeTravel)
                timeTravelPanel?.SetActive(active);
            else if (currentState == MenuState.Clothing)
                clothingPanel?.SetActive(active);
            else if (currentState == MenuState.Biome)
                biomePanel?.SetActive(active);
            else if (currentState == MenuState.GameStyle)
                gameStylePanel?.SetActive(active);
            else if (currentState == MenuState.CustomInput)
                customInputPanel?.SetActive(active);
        }

        private void UpdateMenuSelection()
        {
            if (currentState == MenuState.MainMenu && mainMenuTexts != null)
            {
                for (int i = 0; i < mainMenuTexts.Length; i++)
                {
                    if (mainMenuTexts[i] != null)
                    {
                        mainMenuTexts[i].color = (i == selectedIndex) ? selectedColor : normalColor;
                    }
                }
            }
        }

        private int GetCurrentMenuItemCount()
        {
            switch (currentState)
            {
                case MenuState.MainMenu:
                    return 5; // Time Travel, Clothing, Biome, Game Style, Custom Input
                default:
                    return 1;
            }
        }

        public int GetSelectedIndex()
        {
            return selectedIndex;
        }

        public MenuState GetCurrentState()
        {
            return currentState;
        }
    }
}

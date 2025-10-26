using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Time Travel feature - allows users to view environment in different time periods using a year slider.
    /// Uses Decart AI to transform the environment based on historical era.
    /// </summary>
    public class TimeTravelFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject featurePanel;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text yearText;
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text instructionsText;

        [Header("WebRTC")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        [Header("Settings")]
        [SerializeField] private int minYear = 1800;
        [SerializeField] private int maxYear = 2100;
        [SerializeField] private int currentYear = 2024;
        [SerializeField] private float sliderSpeed = 50f;

        private MenuManager menuManager;
        private int selectedYear;
        private bool isActive = false;
        private bool promptSent = false;

        // Time period definitions with corresponding prompts
        private Dictionary<string, (int startYear, int endYear, string prompt)> timePeriods = new Dictionary<string, (int, int, string)>()
        {
            { "1800s Colonial Era", (1800, 1850, "Transform to 1800s colonial era with cobblestone streets, gas lamps, horse carriages, Victorian architecture, and period clothing") },
            { "1850s Industrial Revolution", (1850, 1900, "Transform to 1850s industrial revolution with steam engines, factory smokestacks, brick buildings, iron structures, and workers in period attire") },
            { "1900s Early 20th Century", (1900, 1920, "Transform to early 1900s with Art Nouveau style, early automobiles, elegant fashion, vintage streetlights, and Belle Époque architecture") },
            { "1920s Roaring Twenties", (1920, 1930, "Transform to 1920s Art Deco style with jazz age aesthetic, flapper fashion, vintage cars, elegant geometric designs, and golden era glamour") },
            { "1930s-1940s War Era", (1930, 1950, "Transform to 1930s-1940s wartime era with utilitarian design, vintage military vehicles, retro signage, period architecture, and sepia-toned atmosphere") },
            { "1950s Post-War Boom", (1950, 1960, "Transform to 1950s with pastel colors, chrome accents, classic diners, vintage cars with tail fins, sock hop aesthetic, and atomic age design") },
            { "1960s Space Age", (1960, 1970, "Transform to 1960s space age with mod design, futuristic optimism, bright colors, psychedelic patterns, and retro-futuristic architecture") },
            { "1970s Disco Era", (1970, 1980, "Transform to 1970s disco era with bold patterns, warm orange and brown tones, shag carpeting aesthetic, vintage technology, and groovy vibes") },
            { "1980s Neon Era", (1980, 1990, "Transform to 1980s with neon lights, synthwave aesthetic, geometric patterns, vibrant colors, retro technology, and Miami Vice style") },
            { "1990s Digital Dawn", (1990, 2000, "Transform to 1990s with early digital aesthetic, grunge style, CRT monitors, chunky technology, and Y2K vibes") },
            { "2000s Millennium", (2000, 2010, "Transform to 2000s millennium era with modern architecture, early smartphones, contemporary design, and sleek minimalism") },
            { "2010s Modern Era", (2010, 2020, "Transform to 2010s modern era with clean contemporary design, digital screens everywhere, modern architecture, and current technology") },
            { "2020s Present Day", (2020, 2030, "Transform to ultra-realistic present day 2020s with current architecture, modern technology, contemporary design, and photorealistic quality") },
            { "2040s Near Future", (2030, 2050, "Transform to near future 2040s with advanced technology, clean energy, sleek architecture, holographic displays, and sustainable design") },
            { "2060s Advanced Future", (2050, 2070, "Transform to 2060s advanced future with flying vehicles, towering megastructures, neon-lit cities, advanced robotics, and cyberpunk elements") },
            { "2080s Far Future", (2070, 2090, "Transform to 2080s far future with massive space elevators, orbital structures, ultra-futuristic architecture, AI everywhere, and sci-fi cityscape") },
            { "2100s Distant Future", (2090, 2100, "Transform to distant future 2100s with completely transformed world, otherworldly architecture, advanced alien-like technology, and utopian/dystopian sci-fi environment") },
        };

        private void Awake()
        {
            menuManager = FindFirstObjectByType<MenuManager>();
            
            if (webRTCConnection == null)
            {
                webRTCConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            selectedYear = currentYear;
            
            if (featurePanel != null)
                featurePanel.SetActive(false);
        }

        public void Activate()
        {
            isActive = true;
            promptSent = false;
            
            if (featurePanel != null)
                featurePanel.SetActive(true);

            if (titleText != null)
                titleText.text = "Time Travel";

            if (instructionsText != null)
                instructionsText.text = "Joystick Up/Down: Adjust Year | Right Trigger: Apply | Left Trigger: Back";

            // Setup slider
            if (yearSlider != null)
            {
                yearSlider.minValue = minYear;
                yearSlider.maxValue = maxYear;
                yearSlider.value = currentYear;
                yearSlider.wholeNumbers = true;
            }

            selectedYear = currentYear;
            UpdateYearDisplay();
        }

        public void Deactivate()
        {
            isActive = false;
            
            if (featurePanel != null)
                featurePanel.SetActive(false);
        }

        private void Update()
        {
            if (!isActive)
                return;

            HandleSliderInput();
            HandleConfirm();
            HandleBack();
        }

        private void HandleSliderInput()
        {
            Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            if (Mathf.Abs(joystick.y) > 0.3f)
            {
                float delta = joystick.y * sliderSpeed * Time.deltaTime;
                selectedYear = Mathf.Clamp(selectedYear + Mathf.RoundToInt(delta), minYear, maxYear);
                
                if (yearSlider != null)
                    yearSlider.value = selectedYear;

                UpdateYearDisplay();
                promptSent = false;
            }
        }

        private void HandleConfirm()
        {
            // Right trigger to apply time travel
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyTimePeriod();
            }
        }

        private void HandleBack()
        {
            // Left trigger to go back to main menu
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (menuManager != null)
                    menuManager.ReturnToMainMenu();
            }
        }

        private void UpdateYearDisplay()
        {
            if (yearText != null)
            {
                yearText.text = $"Year: {selectedYear}";
            }

            // Find and display the corresponding time period
            string periodInfo = GetTimePeriodInfo(selectedYear);
            if (descriptionText != null)
            {
                descriptionText.text = periodInfo;
            }
        }

        private string GetTimePeriodInfo(int year)
        {
            foreach (var period in timePeriods)
            {
                if (year >= period.Value.startYear && year <= period.Value.endYear)
                {
                    return $"{period.Key} ({period.Value.startYear}-{period.Value.endYear})";
                }
            }
            return "Unknown Time Period";
        }

        private void ApplyTimePeriod()
        {
            if (promptSent)
                return;

            string prompt = GetPromptForYear(selectedYear);
            
            if (!string.IsNullOrEmpty(prompt) && webRTCConnection != null)
            {
                Debug.Log($"Time Travel: Applying year {selectedYear} with prompt: {prompt}");
                webRTCConnection.SendCustomPrompt(prompt);
                promptSent = true;
                
                if (descriptionText != null)
                {
                    descriptionText.text += "\n<color=green>✓ Time period applied!</color>";
                }
            }
        }

        private string GetPromptForYear(int year)
        {
            foreach (var period in timePeriods.Values)
            {
                if (year >= period.startYear && year <= period.endYear)
                {
                    return period.prompt;
                }
            }
            return "";
        }
    }
}

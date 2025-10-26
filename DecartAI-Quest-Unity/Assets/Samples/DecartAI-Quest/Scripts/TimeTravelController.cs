using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Time Travel feature - allows users to view their environment 
    /// as it would appear in different historical time periods using a year slider.
    /// </summary>
    public class TimeTravelController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearDisplayText;
        [SerializeField] private TMP_Text descriptionText;
        
        [Header("Year Range")]
        [SerializeField] private int minYear = 1500;
        [SerializeField] private int maxYear = 2500;
        
        private int currentYear;
        private float lastSliderChangeTime;
        private const float sliderCooldown = 0.5f;
        
        // Time period ranges and their visual descriptions
        private static readonly Dictionary<string, (int startYear, int endYear, string prompt)> timePeriods = new Dictionary<string, (int, int, string)>
        {
            // Historical Periods
            { "Renaissance", (1400, 1600, "Transform the environment to Renaissance period with ornate architecture, cobblestone streets, classical art, oil paintings on walls, candlelit interiors, period furniture") },
            { "Baroque", (1600, 1750, "Transform to Baroque era with grand palaces, gold decorations, elaborate chandeliers, marble floors, ornate mirrors, rich tapestries, dramatic lighting") },
            { "Victorian", (1837, 1901, "Transform to Victorian era with gas lamps, brick buildings, Victorian furniture, floral wallpaper, heavy curtains, ornate woodwork, industrial revolution elements") },
            { "Roaring Twenties", (1920, 1929, "Transform to 1920s Art Deco style with geometric patterns, jazz club aesthetics, vintage cars, flapper fashion elements, elegant furnishings, brass accents") },
            { "1950s", (1950, 1959, "Transform to 1950s with mid-century modern furniture, pastel colors, vintage appliances, retro diners, classic cars, atomic age design") },
            { "1980s", (1980, 1989, "Transform to 1980s with neon colors, geometric patterns, arcade aesthetics, vintage electronics, bold colors, synthwave atmosphere") },
            { "Modern Day", (2000, 2024, "Transform to contemporary modern style with minimalist design, smart home elements, sleek furniture, LED lighting, modern technology") },
            
            // Future Periods
            { "Near Future", (2025, 2050, "Transform to near future with advanced technology, holographic displays, smart surfaces, clean energy, sustainable design, minimalist tech aesthetics") },
            { "Cyberpunk Future", (2050, 2100, "Transform to cyberpunk future with neon signs, holographic advertisements, high-tech low-life aesthetic, rain-soaked streets, megacity atmosphere") },
            { "Space Age", (2100, 2200, "Transform to space age with sleek metallic surfaces, antigravity elements, space station aesthetics, futuristic materials, bioluminescent lighting") },
            { "Far Future", (2200, 2500, "Transform to far future with advanced alien-like architecture, energy fields, crystalline structures, otherworldly designs, transcendent technology") },
            
            // Ancient Periods
            { "Medieval", (1000, 1400, "Transform to medieval period with stone castles, torch lighting, tapestries, wooden furniture, suits of armor, gothic architecture") },
            { "Ancient Rome", (100, 400, "Transform to Ancient Rome with marble columns, roman architecture, mosaic floors, toga-clad figures, classical sculptures, forum aesthetics") },
            { "Ancient Egypt", (-3000, -30, "Transform to Ancient Egypt with hieroglyphics, sandstone architecture, golden accents, pharaonic decorations, pyramid structures, desert atmosphere") },
            { "Stone Age", (-10000, -3000, "Transform to Stone Age with cave dwellings, primitive tools, natural rock formations, fire pits, animal hide decorations, prehistoric atmosphere") },
        };
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            if (yearSlider != null)
            {
                yearSlider.minValue = minYear;
                yearSlider.maxValue = maxYear;
                yearSlider.value = DateTime.Now.Year; // Start at current year
                yearSlider.onValueChanged.AddListener(OnYearSliderChanged);
            }
            
            UpdateYearDisplay((int)yearSlider.value);
        }
        
        private void Update()
        {
            // Allow joystick control of slider
            Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            if (Mathf.Abs(thumbstick.x) > 0.3f && Time.time - lastSliderChangeTime > sliderCooldown)
            {
                int yearChange = (int)(thumbstick.x * 10); // Change by increments of 10 years
                int newYear = Mathf.Clamp(currentYear + yearChange, minYear, maxYear);
                
                if (yearSlider != null)
                {
                    yearSlider.value = newYear;
                }
                
                lastSliderChangeTime = Time.time;
            }
        }
        
        private void OnYearSliderChanged(float value)
        {
            int year = Mathf.RoundToInt(value);
            UpdateYearDisplay(year);
        }
        
        private void UpdateYearDisplay(int year)
        {
            currentYear = year;
            
            if (yearDisplayText != null)
            {
                yearDisplayText.text = year > 0 ? $"Year: {year}" : $"Year: {Mathf.Abs(year)} BCE";
            }
            
            // Find the appropriate time period and update description
            string periodName = "Unknown Period";
            string periodPrompt = "";
            
            foreach (var period in timePeriods)
            {
                if (year >= period.Value.startYear && year <= period.Value.endYear)
                {
                    periodName = period.Key;
                    periodPrompt = period.Value.prompt;
                    break;
                }
            }
            
            if (descriptionText != null)
            {
                descriptionText.text = $"Period: {periodName}";
            }
            
            // Apply the transformation if we have a WebRTC connection
            if (!string.IsNullOrEmpty(periodPrompt) && webRtcConnection != null)
            {
                ApplyTimePeriodTransformation(periodPrompt);
            }
        }
        
        private void ApplyTimePeriodTransformation(string prompt)
        {
            if (webRtcConnection != null)
            {
                webRtcConnection.SendCustomPrompt(prompt);
                Debug.Log($"TimeTravelController: Applied transformation for year {currentYear}: {prompt}");
            }
        }
        
        public void SetYear(int year)
        {
            if (yearSlider != null)
            {
                yearSlider.value = year;
            }
        }
        
        public int GetCurrentYear()
        {
            return currentYear;
        }
    }
}

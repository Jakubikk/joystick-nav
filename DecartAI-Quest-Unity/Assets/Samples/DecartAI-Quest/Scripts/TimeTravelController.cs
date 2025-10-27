using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Time Travel feature - allows viewing environment in different historical periods
    /// Uses slider to select year and generates appropriate prompts for Decart Mirage model
    /// </summary>
    public class TimeTravelController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject timeTravelPanel;
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearText;
        [SerializeField] private TMP_Text eraNameText;
        [SerializeField] private TMP_Text descriptionText;
        
        [Header("Settings")]
        [SerializeField] private int minYear = 1800;
        [SerializeField] private int maxYear = 2200;
        [SerializeField] private int yearStep = 10;
        
        private WebRTCConnection webRtcConnection;
        private int currentYear;
        private Dictionary<string, YearRange> historicalEras;
        
        private class YearRange
        {
            public string name;
            public string description;
            public string prompt;
            public int startYear;
            public int endYear;
            
            public bool Contains(int year)
            {
                return year >= startYear && year <= endYear;
            }
        }
        
        private void Awake()
        {
            InitializeHistoricalEras();
            currentYear = System.DateTime.Now.Year;
        }
        
        private void InitializeHistoricalEras()
        {
            historicalEras = new Dictionary<string, YearRange>
            {
                ["colonial"] = new YearRange
                {
                    name = "Colonial Era",
                    description = "Colonial architecture, horse-drawn carriages, cobblestone streets",
                    prompt = "Transform to colonial era 1700s style, cobblestone streets, gas lamps, horse carriages, colonial architecture, wooden buildings, dirt roads",
                    startYear = 1700,
                    endYear = 1799
                },
                ["victorian"] = new YearRange
                {
                    name = "Victorian Era",
                    description = "Victorian architecture, steam trains, gas street lamps",
                    prompt = "Transform to Victorian era 1800s, ornate Victorian architecture, gas street lamps, horse-drawn carriages, Victorian fashion, cobblestone streets, industrial smoke",
                    startYear = 1800,
                    endYear = 1899
                },
                ["earlyModern"] = new YearRange
                {
                    name = "Early Modern",
                    description = "Art deco buildings, vintage cars, early electric lights",
                    prompt = "Transform to early 1900s modern era, art deco architecture, vintage automobiles, early electric street lights, fedora hats, classic storefronts",
                    startYear = 1900,
                    endYear = 1949
                },
                ["midCentury"] = new YearRange
                {
                    name = "Mid-Century Modern",
                    description = "Classic cars, neon signs, retro aesthetic",
                    prompt = "Transform to 1950s-1960s retro style, classic vintage cars, neon signs, mid-century modern architecture, vibrant colors, retro diners, vintage fashion",
                    startYear = 1950,
                    endYear = 1969
                },
                ["disco"] = new YearRange
                {
                    name = "Disco Era",
                    description = "Bright colors, disco balls, funky patterns",
                    prompt = "Transform to 1970s disco era style, bright vibrant colors, funky patterns, disco ball reflections, bell-bottom pants, platform shoes, groovy aesthetic",
                    startYear = 1970,
                    endYear = 1979
                },
                ["eighties"] = new YearRange
                {
                    name = "Neon Eighties",
                    description = "Neon lights, synthwave aesthetic, bold colors",
                    prompt = "Transform to 1980s aesthetic, neon lights, synthwave colors, geometric patterns, bold bright colors, arcade machines, cassette tapes, retro technology",
                    startYear = 1980,
                    endYear = 1989
                },
                ["nineties"] = new YearRange
                {
                    name = "90s Nostalgia",
                    description = "Grunge aesthetic, early tech, CRT monitors",
                    prompt = "Transform to 1990s style, grunge aesthetic, early computers, CRT monitors, VHS tapes, mall culture, baggy clothes, chunky technology",
                    startYear = 1990,
                    endYear = 1999
                },
                ["earlyDigital"] = new YearRange
                {
                    name = "Early Digital Age",
                    description = "Early smartphones, flat screens, modern cars",
                    prompt = "Transform to early 2000s digital age, flip phones, early flat screen displays, Y2K aesthetic, modern cars, digital billboards, early social media era",
                    startYear = 2000,
                    endYear = 2009
                },
                ["modern"] = new YearRange
                {
                    name = "Modern Era",
                    description = "Smartphones, LED screens, contemporary design",
                    prompt = "Transform to modern 2010s style, smartphones everywhere, LED screens, contemporary architecture, electric vehicles, modern minimalist design, digital displays",
                    startYear = 2010,
                    endYear = 2024
                },
                ["nearFuture"] = new YearRange
                {
                    name = "Near Future",
                    description = "Advanced tech, sleek design, holographic displays",
                    prompt = "Transform to near future 2025-2050 style, holographic displays, autonomous vehicles, sleek futuristic architecture, advanced technology, clean energy, smart city infrastructure",
                    startYear = 2025,
                    endYear = 2050
                },
                ["midFuture"] = new YearRange
                {
                    name = "Mid Future",
                    description = "Flying vehicles, mega structures, advanced AI",
                    prompt = "Transform to mid-future 2051-2100 cyberpunk style, flying vehicles, massive skyscrapers, neon lit megacity, advanced robotics, holographic advertisements, cybernetic enhancements",
                    startYear = 2051,
                    endYear = 2100
                },
                ["farFuture"] = new YearRange
                {
                    name = "Distant Future",
                    description = "Space age, crystalline structures, ultra-advanced civilization",
                    prompt = "Transform to distant future 2100+ style, space age architecture, crystalline structures, floating platforms, energy fields, ultra-advanced technology, utopian sci-fi aesthetic",
                    startYear = 2101,
                    endYear = 2300
                }
            };
        }
        
        public void Activate()
        {
            if (timeTravelPanel != null)
            {
                timeTravelPanel.SetActive(true);
            }
            
            MenuManager menuManager = FindFirstObjectByType<MenuManager>();
            if (menuManager != null)
            {
                webRtcConnection = menuManager.GetWebRTCConnection();
            }
            
            if (yearSlider != null)
            {
                yearSlider.minValue = minYear;
                yearSlider.maxValue = maxYear;
                yearSlider.value = currentYear;
                yearSlider.onValueChanged.AddListener(OnYearChanged);
            }
            
            UpdateDisplay();
        }
        
        private void OnDisable()
        {
            if (yearSlider != null)
            {
                yearSlider.onValueChanged.RemoveListener(OnYearChanged);
            }
            
            if (timeTravelPanel != null)
            {
                timeTravelPanel.SetActive(false);
            }
        }
        
        private void OnYearChanged(float value)
        {
            // Round to nearest step
            currentYear = Mathf.RoundToInt(value / yearStep) * yearStep;
            UpdateDisplay();
        }
        
        private void UpdateDisplay()
        {
            if (yearText != null)
            {
                yearText.text = currentYear.ToString();
            }
            
            YearRange era = GetEraForYear(currentYear);
            
            if (eraNameText != null && era != null)
            {
                eraNameText.text = era.name;
            }
            
            if (descriptionText != null && era != null)
            {
                descriptionText.text = era.description;
            }
        }
        
        private YearRange GetEraForYear(int year)
        {
            foreach (var era in historicalEras.Values)
            {
                if (era.Contains(year))
                {
                    return era;
                }
            }
            return null;
        }
        
        public void NavigateUp()
        {
            currentYear = Mathf.Min(currentYear + yearStep, maxYear);
            if (yearSlider != null)
            {
                yearSlider.value = currentYear;
            }
            UpdateDisplay();
        }
        
        public void NavigateDown()
        {
            currentYear = Mathf.Max(currentYear - yearStep, minYear);
            if (yearSlider != null)
            {
                yearSlider.value = currentYear;
            }
            UpdateDisplay();
        }
        
        public void Confirm()
        {
            YearRange era = GetEraForYear(currentYear);
            if (era != null && webRtcConnection != null)
            {
                Debug.Log($"Time Travel to {currentYear}: {era.name} - Sending prompt: {era.prompt}");
                webRtcConnection.SendCustomPrompt(era.prompt);
            }
        }
    }
}

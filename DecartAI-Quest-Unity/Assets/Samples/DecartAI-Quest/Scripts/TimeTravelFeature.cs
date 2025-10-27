using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Controls the Time Travel feature allowing users to view their environment in different historical periods.
    /// Uses a slider to select year and Decart Mirage model for transformation.
    /// </summary>
    public class TimeTravelFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Button applyButton;
        
        [Header("WebRTC Integration")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Time Period Settings")]
        [SerializeField] private int minYear = 1800;
        [SerializeField] private int maxYear = 2200;
        [SerializeField] private int yearStep = 10;
        
        private Dictionary<int, TimeperiodData> timePeriods;
        private int currentYear;
        
        [Serializable]
        private class TimeperiodData
        {
            public int year;
            public string description;
            public string decartPrompt;
        }
        
        private void Start()
        {
            InitializeTimePeriods();
            SetupUI();
        }
        
        private void InitializeTimePeriods()
        {
            timePeriods = new Dictionary<int, TimeperiodData>
            {
                { 1800, new TimeperiodData { year = 1800, description = "Early Industrial Era", 
                    decartPrompt = "Transform environment to look like early 1800s industrial era with gas lamps, cobblestone streets, horse carriages, and Victorian architecture" } },
                { 1850, new TimeperiodData { year = 1850, description = "Victorian Age", 
                    decartPrompt = "Transform environment to Victorian era with ornate architecture, gas lighting, formal gardens, and period appropriate furnishings" } },
                { 1900, new TimeperiodData { year = 1900, description = "Turn of the Century", 
                    decartPrompt = "Transform environment to early 1900s with Art Nouveau style, elegant street lamps, vintage storefronts, and belle époque aesthetics" } },
                { 1920, new TimeperiodData { year = 1920, description = "Roaring Twenties", 
                    decartPrompt = "Transform environment to 1920s Art Deco style with jazz age aesthetics, elegant geometric patterns, luxurious materials, and golden age glamour" } },
                { 1950, new TimeperiodData { year = 1950, description = "Mid-Century Modern", 
                    decartPrompt = "Transform environment to 1950s mid-century modern style with clean lines, pastel colors, retro furniture, and atomic age design elements" } },
                { 1980, new TimeperiodData { year = 1980, description = "Retro 80s", 
                    decartPrompt = "Transform environment to 1980s aesthetic with neon colors, geometric patterns, synth wave vibes, retro technology, and bold design" } },
                { 2000, new TimeperiodData { year = 2000, description = "Y2K Era", 
                    decartPrompt = "Transform environment to early 2000s Y2K aesthetic with sleek technology, minimalist design, chrome accents, and digital age elements" } },
                { 2020, new TimeperiodData { year = 2020, description = "Modern Day", 
                    decartPrompt = "Transform environment to modern contemporary style with smart technology, minimalist design, sustainable materials, and current architectural trends" } },
                { 2050, new TimeperiodData { year = 2050, description = "Near Future", 
                    decartPrompt = "Transform environment to near future 2050 with advanced technology, holographic displays, sustainable smart cities, clean energy, and futuristic architecture" } },
                { 2100, new TimeperiodData { year = 2100, description = "Advanced Future", 
                    decartPrompt = "Transform environment to far future with highly advanced technology, flying vehicles, AI integration, bio-luminescent lighting, and utopian cityscapes" } },
                { 2150, new TimeperiodData { year = 2150, description = "Space Age", 
                    decartPrompt = "Transform environment to space age civilization with zero-gravity elements, transparent domes, planetary views, advanced robotics, and interstellar aesthetics" } },
                { 2200, new TimeperiodData { year = 2200, description = "Post-Human Era", 
                    decartPrompt = "Transform environment to post-human future with cybernetic integration, quantum technology, reality-bending architecture, and transcendent design" } }
            };
        }
        
        private void SetupUI()
        {
            if (yearSlider != null)
            {
                yearSlider.minValue = minYear;
                yearSlider.maxValue = maxYear;
                yearSlider.wholeNumbers = true;
                yearSlider.value = 2020; // Start at modern day
                yearSlider.onValueChanged.AddListener(OnYearChanged);
            }
            
            if (applyButton != null)
            {
                applyButton.onClick.AddListener(ApplyTimePeriod);
            }
            
            UpdateYearDisplay(2020);
        }
        
        private void Update()
        {
            // Allow joystick control of slider when this panel is active
            if (gameObject.activeSelf)
            {
                HandleJoystickSliderControl();
            }
        }
        
        private void HandleJoystickSliderControl()
        {
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            
            if (Mathf.Abs(joystickInput.x) > 0.5f)
            {
                float change = joystickInput.x * yearStep * Time.deltaTime * 2f;
                if (yearSlider != null)
                {
                    yearSlider.value += change;
                }
            }
        }
        
        private void OnYearChanged(float value)
        {
            int year = Mathf.RoundToInt(value / yearStep) * yearStep;
            UpdateYearDisplay(year);
        }
        
        private void UpdateYearDisplay(int year)
        {
            currentYear = year;
            
            if (yearText != null)
            {
                yearText.text = $"Year: {year}";
            }
            
            if (descriptionText != null)
            {
                TimeperiodData period = GetClosestTimePeriod(year);
                descriptionText.text = period?.description ?? "Unknown Era";
            }
        }
        
        private TimeperiodData GetClosestTimePeriod(int targetYear)
        {
            int closestYear = minYear;
            int minDifference = int.MaxValue;
            
            foreach (var year in timePeriods.Keys)
            {
                int difference = Mathf.Abs(year - targetYear);
                if (difference < minDifference)
                {
                    minDifference = difference;
                    closestYear = year;
                }
            }
            
            return timePeriods.ContainsKey(closestYear) ? timePeriods[closestYear] : null;
        }
        
        private void ApplyTimePeriod()
        {
            TimeperiodData period = GetClosestTimePeriod(currentYear);
            
            if (period != null && webRtcConnection != null)
            {
                Debug.Log($"Applying time period: {period.year} - {period.description}");
                webRtcConnection.SendCustomPrompt(period.decartPrompt);
            }
            else
            {
                Debug.LogWarning("Unable to apply time period - missing data or WebRTC connection");
            }
        }
        
        private void OnEnable()
        {
            Debug.Log("Time Travel feature activated");
        }
        
        private void OnDisable()
        {
            Debug.Log("Time Travel feature deactivated");
        }
    }
}

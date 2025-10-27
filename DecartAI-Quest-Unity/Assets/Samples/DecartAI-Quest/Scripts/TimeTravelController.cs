using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;
using System.Collections.Generic;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls time travel feature with slider to view environment in different historical periods
    /// </summary>
    public class TimeTravelController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearText;
        [SerializeField] private TMP_Text eraDescriptionText;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Time Travel Settings")]
        [SerializeField] private int minYear = 1500;
        [SerializeField] private int maxYear = 2200;
        [SerializeField] private float sliderSensitivity = 0.01f;
        
        private int currentYear;
        private Dictionary<int, string> eraPrompts;
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            InitializeEraPrompts();
            InitializeSlider();
        }
        
        private void InitializeEraPrompts()
        {
            // Define prompts for different historical eras
            eraPrompts = new Dictionary<int, string>
            {
                // Medieval period
                { 1500, "Transform the environment into medieval times with stone castles, torches, and cobblestone streets" },
                { 1600, "Transform into 17th century baroque style with ornate architecture and candlelit interiors" },
                { 1700, "Transform into 18th century colonial era with horse carriages and gas lamps" },
                
                // Industrial revolution
                { 1800, "Transform into Victorian era with steam-powered machinery, brick buildings, and gas street lamps" },
                { 1850, "Transform into industrial revolution setting with factories, steam trains, and iron structures" },
                { 1900, "Transform into early 1900s with vintage automobiles, electric lights, and art nouveau architecture" },
                
                // Early 20th century
                { 1920, "Transform into 1920s art deco style with jazz age aesthetics, flapper era decoration" },
                { 1940, "Transform into 1940s wartime era with vintage military aesthetics and period-appropriate technology" },
                { 1950, "Transform into 1950s retro-futuristic style with chrome details, pastel colors, and vintage appliances" },
                
                // Late 20th century
                { 1960, "Transform into 1960s mod style with bright colors, psychedelic patterns, and space age design" },
                { 1970, "Transform into 1970s disco era with shag carpets, wood paneling, and vintage electronics" },
                { 1980, "Transform into 1980s neon aesthetic with synthwave colors, arcade machines, and retro technology" },
                { 1990, "Transform into 1990s grunge era with minimalist design and early computer technology" },
                
                // 21st century
                { 2000, "Transform into Y2K aesthetic with early 2000s technology and millennium design trends" },
                { 2010, "Transform into modern contemporary style with smart technology and clean minimalist design" },
                { 2020, "Transform into current era with latest technology, sustainable design, and modern aesthetics" },
                
                // Near future
                { 2030, "Transform into near-future setting with advanced holographic displays and renewable energy integration" },
                { 2050, "Transform into mid-21st century with flying vehicles, automated systems, and futuristic architecture" },
                { 2075, "Transform into advanced future with AI-integrated environments and seamless technology" },
                
                // Far future
                { 2100, "Transform into 22nd century with space-age technology, anti-gravity vehicles, and crystalline architecture" },
                { 2150, "Transform into distant future with utopian sci-fi aesthetics and advanced alien-inspired design" },
                { 2200, "Transform into far future with post-scarcity society aesthetics and otherworldly architecture" }
            };
        }
        
        private void InitializeSlider()
        {
            if (yearSlider != null)
            {
                yearSlider.minValue = minYear;
                yearSlider.maxValue = maxYear;
                yearSlider.value = 2020; // Start at current era
                currentYear = 2020;
                
                yearSlider.onValueChanged.AddListener(OnSliderValueChanged);
            }
            
            UpdateYearDisplay();
        }
        
        private void Update()
        {
            HandleJoystickSliderControl();
        }
        
        private void HandleJoystickSliderControl()
        {
            if (!gameObject.activeInHierarchy || yearSlider == null) return;
            
            // Use right joystick left/right to control slider
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            
            if (Mathf.Abs(joystickInput.x) > 0.1f)
            {
                float newValue = yearSlider.value + (joystickInput.x * sliderSensitivity * (maxYear - minYear));
                yearSlider.value = Mathf.Clamp(newValue, minYear, maxYear);
            }
        }
        
        private void OnSliderValueChanged(float value)
        {
            int newYear = Mathf.RoundToInt(value);
            
            // Round to nearest decade for cleaner display
            newYear = Mathf.RoundToInt(newYear / 10f) * 10;
            
            if (newYear != currentYear)
            {
                currentYear = newYear;
                UpdateYearDisplay();
                ApplyTimePeriodTransformation();
            }
        }
        
        private void UpdateYearDisplay()
        {
            if (yearText != null)
            {
                yearText.text = $"Year: {currentYear}";
            }
            
            if (eraDescriptionText != null)
            {
                eraDescriptionText.text = GetEraDescription(currentYear);
            }
        }
        
        private string GetEraDescription(int year)
        {
            if (year < 1700) return "Medieval Era";
            if (year < 1800) return "Age of Enlightenment";
            if (year < 1900) return "Industrial Revolution";
            if (year < 1920) return "Victorian Era";
            if (year < 1940) return "Art Deco Era";
            if (year < 1960) return "Mid-Century Modern";
            if (year < 1980) return "Space Age";
            if (year < 2000) return "Digital Revolution";
            if (year < 2020) return "Information Age";
            if (year < 2050) return "Near Future";
            if (year < 2100) return "Advanced Future";
            return "Far Future";
        }
        
        private void ApplyTimePeriodTransformation()
        {
            if (webRtcConnection == null) return;
            
            // Find the closest era prompt
            int closestYear = GetClosestEraYear(currentYear);
            
            if (eraPrompts.ContainsKey(closestYear))
            {
                string prompt = eraPrompts[closestYear];
                webRtcConnection.SendCustomPrompt(prompt);
                Debug.Log($"Time Travel: Applied transformation for year {currentYear} using prompt for {closestYear}");
            }
        }
        
        private int GetClosestEraYear(int targetYear)
        {
            int closest = minYear;
            int minDiff = Mathf.Abs(targetYear - minYear);
            
            foreach (int year in eraPrompts.Keys)
            {
                int diff = Mathf.Abs(targetYear - year);
                if (diff < minDiff)
                {
                    minDiff = diff;
                    closest = year;
                }
            }
            
            return closest;
        }
        
        private void OnDestroy()
        {
            if (yearSlider != null)
            {
                yearSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
            }
        }
    }
}

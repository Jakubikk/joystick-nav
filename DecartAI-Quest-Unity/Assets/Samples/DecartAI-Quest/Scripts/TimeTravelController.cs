using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Time Travel feature which allows users to view their environment
    /// as it would appear in different historical eras using a year slider.
    /// </summary>
    public class TimeTravelController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearDisplayText;
        [SerializeField] private TMP_Text eraDescriptionText;
        
        [Header("Settings")]
        [SerializeField] private int minYear = 1000;
        [SerializeField] private int maxYear = 3000;
        [SerializeField] private int currentYear = 2025;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private float sliderUpdateCooldown = 0.5f;
        private float lastSliderUpdateTime = 0f;
        private int lastSentYear = 2025;

        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            InitializeSlider();
        }

        private void InitializeSlider()
        {
            if (yearSlider != null)
            {
                yearSlider.minValue = minYear;
                yearSlider.maxValue = maxYear;
                yearSlider.value = currentYear;
                yearSlider.wholeNumbers = true;
            }
            
            UpdateYearDisplay(currentYear);
        }

        private void Update()
        {
            HandleSliderInput();
            UpdateSliderFromJoystick();
        }

        private void HandleSliderInput()
        {
            if (yearSlider == null) return;

            int newYear = Mathf.RoundToInt(yearSlider.value);
            
            if (newYear != currentYear)
            {
                currentYear = newYear;
                UpdateYearDisplay(currentYear);
                
                // Send prompt if enough time has passed
                if (Time.time - lastSliderUpdateTime > sliderUpdateCooldown)
                {
                    SendTimeTravelPrompt(currentYear);
                    lastSliderUpdateTime = Time.time;
                }
            }
        }

        private void UpdateSliderFromJoystick()
        {
            // Use horizontal joystick for fine control
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            if (Mathf.Abs(joystickInput.x) > 0.3f)
            {
                float increment = joystickInput.x * 10f * Time.deltaTime; // 10 years per second
                yearSlider.value += increment;
            }
        }

        private void UpdateYearDisplay(int year)
        {
            if (yearDisplayText != null)
            {
                yearDisplayText.text = $"Year: {year}";
            }
            
            if (eraDescriptionText != null)
            {
                eraDescriptionText.text = GetEraDescription(year);
            }
        }

        private void SendTimeTravelPrompt(int year)
        {
            if (webRtcConnection == null || year == lastSentYear)
                return;

            string prompt = GenerateTimeTravelPrompt(year);
            webRtcConnection.SendCustomPrompt(prompt);
            lastSentYear = year;
            
            Debug.Log($"Time Travel: Sending prompt for year {year}");
        }

        private string GenerateTimeTravelPrompt(int year)
        {
            if (year < 1500)
            {
                return "Medieval times, stone castles, dirt roads, wooden structures, torches, people in medieval clothing, horses and carts, rustic atmosphere";
            }
            else if (year < 1700)
            {
                return "Renaissance era, cobblestone streets, baroque architecture, horse-drawn carriages, people in renaissance attire, candles and lanterns";
            }
            else if (year < 1850)
            {
                return "Colonial period, brick buildings, gas lamps, wooden wagons, people in 18th century clothing, colonial architecture";
            }
            else if (year < 1920)
            {
                return "Victorian era, industrial revolution aesthetic, steam engines, early automobiles, gas street lamps, Victorian architecture, people in Victorian clothing";
            }
            else if (year < 1950)
            {
                return "Early 20th century, art deco style, vintage cars from 1920s-1940s, old-fashioned street lamps, retro architecture, people in period clothing";
            }
            else if (year < 1980)
            {
                return "Mid-century modern aesthetic, 1950s-1970s style cars, retro signage, classic architecture, vintage technology, people in period fashion";
            }
            else if (year < 2000)
            {
                return "1980s-1990s aesthetic, vintage electronics, retro technology, period-appropriate vehicles, classic modern architecture";
            }
            else if (year <= 2030)
            {
                return "Contemporary modern setting, current technology, modern vehicles, contemporary architecture, present-day atmosphere";
            }
            else if (year < 2100)
            {
                return "Near-future setting, sleek modern design, electric vehicles, solar panels, advanced technology, sustainable architecture, holographic displays";
            }
            else if (year < 2200)
            {
                return "Advanced future, futuristic architecture, flying vehicles, neon lights, high-tech cityscape, cyberpunk aesthetic, advanced robotics";
            }
            else if (year < 2500)
            {
                return "Far future, ultra-advanced technology, massive skyscrapers, hovering vehicles, AI integration everywhere, gleaming metallic surfaces, holographic interfaces";
            }
            else
            {
                return "Distant future, space-age civilization, crystal structures, anti-gravity technology, ethereal lighting, utopian sci-fi setting, advanced alien-like architecture";
            }
        }

        private string GetEraDescription(int year)
        {
            if (year < 1500)
                return "Medieval Times";
            else if (year < 1700)
                return "Renaissance Era";
            else if (year < 1850)
                return "Colonial Period";
            else if (year < 1920)
                return "Victorian Era";
            else if (year < 1950)
                return "Early 20th Century";
            else if (year < 1980)
                return "Mid-Century Modern";
            else if (year < 2000)
                return "Late 20th Century";
            else if (year <= 2030)
                return "Present Day";
            else if (year < 2100)
                return "Near Future";
            else if (year < 2200)
                return "Advanced Future";
            else if (year < 2500)
                return "Far Future";
            else
                return "Distant Future";
        }

        public void SetYear(int year)
        {
            currentYear = Mathf.Clamp(year, minYear, maxYear);
            if (yearSlider != null)
            {
                yearSlider.value = currentYear;
            }
            UpdateYearDisplay(currentYear);
        }
    }
}

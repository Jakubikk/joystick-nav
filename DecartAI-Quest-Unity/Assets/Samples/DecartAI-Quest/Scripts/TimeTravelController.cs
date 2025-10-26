using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Handles time travel feature - allows users to see their environment as it would appear in different time periods
    /// </summary>
    public class TimeTravelController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearDisplayText;
        [SerializeField] private TMP_Text descriptionText;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Year Range")]
        [SerializeField] private int minYear = 1800;
        [SerializeField] private int maxYear = 2100;
        
        private int currentYear;
        private float sliderCooldown = 0f;
        private const float SLIDER_COOLDOWN = 0.2f;
        
        private void OnEnable()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            InitializeSlider();
            currentYear = System.DateTime.Now.Year;
            UpdateDisplay();
        }
        
        private void InitializeSlider()
        {
            if (yearSlider != null)
            {
                yearSlider.minValue = minYear;
                yearSlider.maxValue = maxYear;
                yearSlider.value = System.DateTime.Now.Year;
                yearSlider.wholeNumbers = true;
            }
        }
        
        private void Update()
        {
            if (sliderCooldown > 0)
            {
                sliderCooldown -= Time.deltaTime;
                return;
            }
            
            // Use joystick to adjust year
            Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            if (Mathf.Abs(joystick.x) > 0.5f)
            {
                int yearChange = joystick.x > 0 ? 10 : -10;
                currentYear = Mathf.Clamp(currentYear + yearChange, minYear, maxYear);
                
                if (yearSlider != null)
                    yearSlider.value = currentYear;
                
                UpdateDisplay();
                sliderCooldown = SLIDER_COOLDOWN;
            }
            
            // Right trigger to apply the time travel transformation
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyTimePeriod();
            }
        }
        
        private void UpdateDisplay()
        {
            if (yearDisplayText != null)
            {
                yearDisplayText.text = $"Year: {currentYear}";
            }
            
            if (descriptionText != null)
            {
                descriptionText.text = GetTimePeriodDescription(currentYear);
            }
        }
        
        private void ApplyTimePeriod()
        {
            if (webRtcConnection == null) return;
            
            string prompt = GenerateTimePeriodPrompt(currentYear);
            webRtcConnection.SendCustomPrompt(prompt);
            
            Debug.Log($"Time Travel: Applied year {currentYear} with prompt: {prompt}");
        }
        
        private string GenerateTimePeriodPrompt(int year)
        {
            // Generate appropriate prompt based on year
            if (year >= 2050)
            {
                return "Transform to futuristic environment with holographic displays, flying vehicles, advanced architecture, neon lights, sleek metallic surfaces, high-tech gadgets, sci-fi aesthetic";
            }
            else if (year >= 2000)
            {
                return "Transform to modern contemporary setting with current technology, contemporary furniture, modern architecture, digital screens, clean minimalist design";
            }
            else if (year >= 1980)
            {
                return "Transform to 1980s aesthetic with retro technology, CRT televisions, vintage computers, bright neon colors, wood paneling, cassette tapes, analog equipment";
            }
            else if (year >= 1950)
            {
                return "Transform to mid-century modern style with vintage 1950s-60s furniture, rotary phones, black and white TV, retro appliances, pastel colors, classic automobiles visible";
            }
            else if (year >= 1920)
            {
                return "Transform to early 20th century with antique furniture, art deco style, vintage telephones, gramophones, old-fashioned lamps, classic interior design, sepia tones";
            }
            else if (year >= 1850)
            {
                return "Transform to Victorian era with ornate furniture, oil lamps, candles, vintage wallpaper, antique decorations, classical paintings, horse-drawn carriages outside, period clothing";
            }
            else
            {
                return "Transform to 1800s historical setting with period-accurate furniture, candle lighting, old books, vintage interior, historical atmosphere, rustic materials, aged textures";
            }
        }
        
        private string GetTimePeriodDescription(int year)
        {
            if (year >= 2050)
                return "Futuristic - Advanced technology and sci-fi aesthetics";
            else if (year >= 2000)
                return "Modern Era - Contemporary design and current technology";
            else if (year >= 1980)
                return "1980s - Retro tech and neon aesthetic";
            else if (year >= 1950)
                return "Mid-Century - Classic 1950s-60s style";
            else if (year >= 1920)
                return "Early 1900s - Art Deco and vintage charm";
            else if (year >= 1850)
                return "Victorian Era - Ornate and classical design";
            else
                return "1800s - Historical period setting";
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Time Travel feature - allows users to view their environment in different historical years
    /// Uses a slider to select the year and transforms the environment accordingly
    /// </summary>
    public class TimeTravelFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text instructionsText;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Year Range")]
        [SerializeField] private int minYear = 1800;
        [SerializeField] private int maxYear = 2100;
        
        private int currentYear;
        private float joystickCooldown = 0f;
        private const float JOYSTICK_COOLDOWN_TIME = 0.2f;
        
        // Predefined time periods with descriptions
        private readonly (int year, string description, string prompt)[] timePeriods = new (int, string, string)[]
        {
            (1800, "Early 1800s", "Transform the environment to look like the early 1800s, with gas lamps, cobblestone streets, horse carriages, and Victorian architecture"),
            (1850, "Victorian Era", "Transform the environment to the Victorian era with ornate architecture, steam-powered machinery, gas street lights, and industrial revolution aesthetics"),
            (1900, "Turn of Century", "Transform the environment to 1900s style with early electric lights, vintage automobiles, Art Nouveau architecture, and Edwardian elegance"),
            (1920, "Roaring Twenties", "Transform the environment to the 1920s with Art Deco design, jazz age glamour, vintage cars, and prohibition-era ambiance"),
            (1950, "Post-War Era", "Transform the environment to 1950s style with mid-century modern design, chrome accents, pastel colors, classic diners, and retro automobiles"),
            (1970, "Disco Era", "Transform the environment to the 1970s with disco aesthetics, vibrant colors, vintage electronics, and groovy retro design"),
            (1990, "Digital Age Begin", "Transform the environment to 1990s style with early digital displays, neon signs, vintage computers, and urban tech aesthetics"),
            (2000, "Y2K Era", "Transform the environment to the Y2K era with futuristic metallic surfaces, digital displays, modern technology, and millennial design"),
            (2025, "Present Day", "Transform the environment to contemporary ultra-realistic modern aesthetics with current architecture and technology"),
            (2050, "Near Future", "Transform the environment to near-future 2050 with advanced holographic displays, sleek architecture, renewable energy visible, and sustainable design"),
            (2075, "Advanced Future", "Transform the environment to 2075 with flying vehicles, holographic interfaces, bio-luminescent plants, and advanced futuristic architecture"),
            (2100, "Far Future", "Transform the environment to far future 2100 with cyberpunk aesthetics, neon-lit megastructures, advanced AI interfaces, and sci-fi architecture")
        };
        
        private void OnEnable()
        {
            InitializeFeature();
        }
        
        private void InitializeFeature()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            if (yearSlider != null)
            {
                yearSlider.minValue = minYear;
                yearSlider.maxValue = maxYear;
                yearSlider.value = 2025; // Start at present day
                yearSlider.onValueChanged.AddListener(OnSliderValueChanged);
            }
            
            currentYear = 2025;
            UpdateDisplay();
            
            if (instructionsText != null)
            {
                instructionsText.text = "Use joystick UP/DOWN to change year\nRight Trigger to apply time transformation\nLeft Trigger to return to menu";
            }
        }
        
        private void OnDisable()
        {
            if (yearSlider != null)
            {
                yearSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
            }
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            // Update cooldown
            if (joystickCooldown > 0)
            {
                joystickCooldown -= Time.deltaTime;
            }
            
            // Joystick navigation for year selection
            if (joystickCooldown <= 0)
            {
                Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                if (joystick.y > 0.5f) // Up - increase year by 25
                {
                    currentYear += 25;
                    if (currentYear > maxYear)
                        currentYear = maxYear;
                    
                    if (yearSlider != null)
                        yearSlider.value = currentYear;
                    
                    UpdateDisplay();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
                else if (joystick.y < -0.5f) // Down - decrease year by 25
                {
                    currentYear -= 25;
                    if (currentYear < minYear)
                        currentYear = minYear;
                    
                    if (yearSlider != null)
                        yearSlider.value = currentYear;
                    
                    UpdateDisplay();
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
            }
            
            // Right Trigger = Apply time transformation
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                ApplyTimeTransformation();
            }
        }
        
        private void OnSliderValueChanged(float value)
        {
            currentYear = Mathf.RoundToInt(value);
            UpdateDisplay();
        }
        
        private void UpdateDisplay()
        {
            if (yearText != null)
            {
                yearText.text = $"Year: {currentYear}";
            }
            
            // Find the closest time period
            var closestPeriod = GetClosestTimePeriod(currentYear);
            
            if (descriptionText != null)
            {
                descriptionText.text = closestPeriod.description;
            }
        }
        
        private (int year, string description, string prompt) GetClosestTimePeriod(int year)
        {
            var closest = timePeriods[0];
            int minDiff = Mathf.Abs(year - closest.year);
            
            foreach (var period in timePeriods)
            {
                int diff = Mathf.Abs(year - period.year);
                if (diff < minDiff)
                {
                    minDiff = diff;
                    closest = period;
                }
            }
            
            return closest;
        }
        
        private void ApplyTimeTransformation()
        {
            if (webRtcConnection == null)
            {
                Debug.LogError("TimeTravelFeature: WebRTCConnection not found!");
                return;
            }
            
            var timePeriod = GetClosestTimePeriod(currentYear);
            string prompt = timePeriod.prompt;
            
            Debug.Log($"Applying time transformation to year {currentYear}: {prompt}");
            
            // Send the prompt to Decart AI
            webRtcConnection.SendCustomPrompt(prompt);
            
            // Provide feedback
            if (descriptionText != null)
            {
                descriptionText.text = $"{timePeriod.description}\n<color=green>✓ Transformation applied!</color>";
            }
        }
    }
}

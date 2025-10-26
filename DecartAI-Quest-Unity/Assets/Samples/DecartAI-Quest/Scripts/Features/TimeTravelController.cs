using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Time Travel feature controller.
    /// Allows users to slide through different historical eras and see their environment transformed.
    /// Uses Mirage model for world transformations.
    /// </summary>
    public class TimeTravelController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject timeTravelUI;
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearText;
        [SerializeField] private TMP_Text eraDescriptionText;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        private bool isActive = false;
        private int currentYear = 2024;
        private const int MIN_YEAR = 1800;
        private const int MAX_YEAR = 2100;
        private float sliderCooldown = 0f;
        private const float SLIDER_UPDATE_DELAY = 0.3f;

        // Era definitions with AI prompts
        private struct Era
        {
            public int startYear;
            public int endYear;
            public string name;
            public string description;
            public string aiPrompt;
        }

        private Era[] eras = new Era[]
        {
            new Era { startYear = 1800, endYear = 1850, name = "Early Industrial Revolution",
                     description = "Coal-powered factories, steam engines, early railways",
                     aiPrompt = "Transform the world into early 1800s industrial revolution era with coal smoke, brick factories, steam engines, cobblestone streets, gas lamps, Victorian architecture" },
            
            new Era { startYear = 1851, endYear = 1900, name = "Victorian Era",
                     description = "Victorian architecture, gas streetlights, horse carriages",
                     aiPrompt = "Transform into Victorian era 1880s world with ornate Victorian buildings, gas streetlights, horse-drawn carriages, cobblestone roads, top hats and formal dress, sepia tones" },
            
            new Era { startYear = 1901, endYear = 1920, name = "Edwardian & WWI Era",
                     description = "Early automobiles, Art Nouveau, the Great War",
                     aiPrompt = "Transform into early 1900s Edwardian era with Art Nouveau architecture, early automobiles, vintage street fashion, sepia photography aesthetic, elegant belle époque style" },
            
            new Era { startYear = 1921, endYear = 1940, name = "Roaring Twenties & Art Deco",
                     description = "Jazz age, flappers, Art Deco, the Great Depression",
                     aiPrompt = "Transform into 1920s-1930s Art Deco era with geometric patterns, chrome and glass buildings, vintage cars, jazz age atmosphere, black and white film noir aesthetic" },
            
            new Era { startYear = 1941, endYear = 1960, name = "Mid-Century Modern",
                     description = "Post-war optimism, classic cars, diners, suburban boom",
                     aiPrompt = "Transform into 1950s mid-century modern world with chrome accents, pastel colors, classic American cars, retro diners, atomic age design, vintage Americana" },
            
            new Era { startYear = 1961, endYear = 1980, name = "Space Age & Disco",
                     description = "Moon landing, psychedelic colors, disco, early computers",
                     aiPrompt = "Transform into 1970s retro world with psychedelic colors, disco aesthetics, vintage technology, groovy patterns, retro futurism, bell bottoms era" },
            
            new Era { startYear = 1981, endYear = 2000, name = "Digital Revolution",
                     description = "Early internet, neon aesthetics, arcade culture",
                     aiPrompt = "Transform into 1980s-1990s retrofuturism with neon lights, vaporwave aesthetics, grid patterns, vintage computers, arcade culture, synthwave vibes, VHS quality" },
            
            new Era { startYear = 2001, endYear = 2024, name = "Modern Era",
                     description = "Current day - smartphones, digital life, contemporary design",
                     aiPrompt = "Ultra realistic modern world, contemporary architecture, current fashion, digital displays, clean design, photorealistic" },
            
            new Era { startYear = 2025, endYear = 2050, name = "Near Future",
                     description = "Advanced tech, sustainable cities, AI integration",
                     aiPrompt = "Transform into near future 2040s with sleek sustainable architecture, holographic displays, advanced clean technology, green cities, minimalist futuristic design" },
            
            new Era { startYear = 2051, endYear = 2100, name = "Far Future",
                     description = "Cyberpunk cities, flying vehicles, advanced AI",
                     aiPrompt = "Transform into far future cyberpunk 2080s with towering megastructures, neon-lit streets, holographic advertisements, flying vehicles, advanced cybernetic technology, sci-fi metropolis" }
        };

        private void Awake()
        {
            if (timeTravelUI != null)
                timeTravelUI.SetActive(false);

            if (yearSlider != null)
            {
                yearSlider.minValue = MIN_YEAR;
                yearSlider.maxValue = MAX_YEAR;
                yearSlider.value = currentYear;
                yearSlider.wholeNumbers = true;
            }
        }

        private void Update()
        {
            if (!isActive) return;

            if (sliderCooldown > 0)
            {
                sliderCooldown -= Time.deltaTime;
                return;
            }

            HandleJoystickSlider();
        }

        private void HandleJoystickSlider()
        {
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            // Use joystick left/right to adjust year slider
            if (Mathf.Abs(joystickInput.x) > 0.3f)
            {
                int yearChange = (int)(joystickInput.x * 10); // Faster movement with more tilt
                currentYear = Mathf.Clamp(currentYear + yearChange, MIN_YEAR, MAX_YEAR);
                
                if (yearSlider != null)
                    yearSlider.value = currentYear;
                
                UpdateYearDisplay();
                sliderCooldown = SLIDER_UPDATE_DELAY;
            }

            // Right trigger to apply the time travel transformation
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                ApplyTimeTravelTransformation();
            }
        }

        private void UpdateYearDisplay()
        {
            if (yearText != null)
                yearText.text = currentYear.ToString();

            Era currentEra = GetEraForYear(currentYear);
            
            if (eraDescriptionText != null)
            {
                eraDescriptionText.text = $"<b>{currentEra.name}</b>\n{currentEra.description}";
            }
        }

        private Era GetEraForYear(int year)
        {
            foreach (var era in eras)
            {
                if (year >= era.startYear && year <= era.endYear)
                    return era;
            }
            return eras[eras.Length - 1]; // Default to last era
        }

        private void ApplyTimeTravelTransformation()
        {
            Era currentEra = GetEraForYear(currentYear);
            
            if (webRTCConnection != null)
            {
                webRTCConnection.SendCustomPrompt(currentEra.aiPrompt);
                Debug.Log($"TimeTravelController: Applied transformation for year {currentYear} - {currentEra.name}");
            }
            else
            {
                Debug.LogWarning("TimeTravelController: WebRTC connection not set");
            }
        }

        public void Activate()
        {
            isActive = true;
            if (timeTravelUI != null)
                timeTravelUI.SetActive(true);
            
            UpdateYearDisplay();
            Debug.Log("TimeTravelController: Activated");
        }

        public void Deactivate()
        {
            isActive = false;
            if (timeTravelUI != null)
                timeTravelUI.SetActive(false);
            
            Debug.Log("TimeTravelController: Deactivated");
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Controls the Time Travel year slider with joystick input
    /// </summary>
    public class TimeTravelSliderController : MonoBehaviour
    {
        [Header("Slider Components")]
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearDisplayText;
        [SerializeField] private TMP_Text eraDescriptionText;
        
        [Header("Slider Settings")]
        [SerializeField] private float minYear = 1800f;
        [SerializeField] private float maxYear = 2200f;
        [SerializeField] private float changeSpeed = 10f; // Years per second when holding joystick
        
        private bool isActive = false;
        
        private void Start()
        {
            if (yearSlider != null)
            {
                yearSlider.minValue = minYear;
                yearSlider.maxValue = maxYear;
                yearSlider.value = 2024f;
                yearSlider.wholeNumbers = true;
                
                yearSlider.onValueChanged.AddListener(OnSliderValueChanged);
            }
            
            UpdateDisplay();
        }
        
        private void Update()
        {
            if (!isActive) return;
            
            // Get joystick input
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            
            if (Mathf.Abs(joystickInput.x) > 0.3f && yearSlider != null)
            {
                // Move slider with joystick
                float delta = joystickInput.x * changeSpeed * Time.deltaTime;
                yearSlider.value = Mathf.Clamp(yearSlider.value + delta, minYear, maxYear);
            }
        }
        
        public void SetActive(bool active)
        {
            isActive = active;
            if (active)
            {
                UpdateDisplay();
            }
        }
        
        private void OnSliderValueChanged(float value)
        {
            UpdateDisplay();
        }
        
        private void UpdateDisplay()
        {
            if (yearSlider == null) return;
            
            int year = Mathf.RoundToInt(yearSlider.value);
            
            // Update year text
            if (yearDisplayText != null)
            {
                yearDisplayText.text = year.ToString();
            }
            
            // Update era description
            if (eraDescriptionText != null)
            {
                eraDescriptionText.text = GetEraDescription(year);
            }
        }
        
        public int GetCurrentYear()
        {
            if (yearSlider == null) return 2024;
            return Mathf.RoundToInt(yearSlider.value);
        }
        
        private string GetEraDescription(int year)
        {
            if (year < 1850)
                return "Early Industrial Era - Horse carriages, gas lamps, cobblestone streets";
            else if (year < 1900)
                return "Victorian Era - Steam engines, telegraph, early industrialization";
            else if (year < 1920)
                return "Early 20th Century - Model T cars, electric lights, urban growth";
            else if (year < 1940)
                return "Roaring 20s & 30s - Jazz age, art deco, early aviation";
            else if (year < 1960)
                return "Post-War Era - Classic cars, suburban expansion, optimism";
            else if (year < 1980)
                return "Mid-Century Modern - Space race, counterculture, color TV";
            else if (year < 2000)
                return "Late 20th Century - Personal computers, arcade games, vibrant culture";
            else if (year < 2010)
                return "Early 2000s - Internet age, flip phones, Y2K aesthetic";
            else if (year < 2020)
                return "2010s - Smartphones, social media, modern technology";
            else if (year <= 2024)
                return "Present Day - AI, electric vehicles, current technology";
            else if (year < 2050)
                return "Near Future - Clean energy, autonomous vehicles, smart cities";
            else if (year < 2100)
                return "Mid Future - Flying cars, holographic displays, advanced AI";
            else if (year < 2150)
                return "Far Future - Space colonies, fusion power, nanotechnology";
            else
                return "Distant Future - Post-scarcity, interstellar travel, advanced civilization";
        }
    }
}

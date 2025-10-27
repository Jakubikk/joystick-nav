using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Time Travel feature - allows users to view environment in different years
    /// Uses Decart AI's realtime video transformation to show historical or future versions
    /// </summary>
    public class TimeTravelController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider yearSlider;
        [SerializeField] private TMP_Text yearDisplayText;
        [SerializeField] private TMP_Text periodDescriptionText;
        [SerializeField] private Button applyButton;

        [Header("Year Range")]
        [SerializeField] private int minYear = 1800;
        [SerializeField] private int maxYear = 2200;
        [SerializeField] private int currentYear = 2025;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        private int selectedYear;

        private void Awake()
        {
            if (webRTCConnection == null)
            {
                webRTCConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            selectedYear = currentYear;
        }

        private void Start()
        {
            SetupSlider();
            SetupButton();
            UpdateYearDisplay();
        }

        private void SetupSlider()
        {
            if (yearSlider != null)
            {
                yearSlider.minValue = minYear;
                yearSlider.maxValue = maxYear;
                yearSlider.value = currentYear;
                yearSlider.wholeNumbers = true;
                yearSlider.onValueChanged.AddListener(OnYearChanged);
            }
        }

        private void SetupButton()
        {
            if (applyButton != null)
            {
                applyButton.onClick.AddListener(ApplyTimeTravelTransformation);
            }
        }

        private void Update()
        {
            // Allow joystick to control slider when panel is active
            if (gameObject.activeSelf)
            {
                HandleSliderInput();
            }
        }

        private void HandleSliderInput()
        {
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            // Left/right on joystick to adjust year
            if (Mathf.Abs(joystickInput.x) > 0.3f)
            {
                float adjustment = joystickInput.x * Time.deltaTime * 50f;
                if (yearSlider != null)
                {
                    yearSlider.value += adjustment;
                }
            }

            // Right trigger to apply
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyTimeTravelTransformation();
            }
        }

        private void OnYearChanged(float value)
        {
            selectedYear = Mathf.RoundToInt(value);
            UpdateYearDisplay();
        }

        private void UpdateYearDisplay()
        {
            if (yearDisplayText != null)
            {
                yearDisplayText.text = $"Year: {selectedYear}";
            }

            if (periodDescriptionText != null)
            {
                periodDescriptionText.text = GetPeriodDescription(selectedYear);
            }
        }

        private string GetPeriodDescription(int year)
        {
            if (year < 1850) return "Early Industrial Era";
            if (year < 1900) return "Victorian Era";
            if (year < 1920) return "Edwardian Era";
            if (year < 1950) return "Early 20th Century";
            if (year < 1980) return "Mid 20th Century";
            if (year < 2000) return "Late 20th Century";
            if (year < 2030) return "Modern Era";
            if (year < 2100) return "Near Future";
            return "Distant Future";
        }

        private void ApplyTimeTravelTransformation()
        {
            if (webRTCConnection == null)
            {
                Debug.LogError("TimeTravelController: WebRTC connection not found");
                return;
            }

            string prompt = GenerateTimeTravelPrompt(selectedYear);
            Debug.Log($"TimeTravelController: Applying transformation for year {selectedYear}");
            
            webRTCConnection.SendCustomPrompt(prompt);
        }

        private string GenerateTimeTravelPrompt(int year)
        {
            string period = GetPeriodDescription(year);
            string architectureStyle = GetArchitecturalStyle(year);
            string technologyLevel = GetTechnologyLevel(year);
            string atmosphere = GetAtmosphere(year);

            return $"Transform the environment to look like the year {year}, {period}. " +
                   $"Show {architectureStyle} architecture, {technologyLevel} technology level, " +
                   $"with {atmosphere} atmosphere. Maintain the overall layout but change materials, " +
                   $"objects, and styling to match the time period authentically.";
        }

        private string GetArchitecturalStyle(int year)
        {
            if (year < 1850) return "Georgian and early industrial";
            if (year < 1900) return "Victorian";
            if (year < 1930) return "Art Nouveau and Edwardian";
            if (year < 1950) return "Art Deco and early modernist";
            if (year < 1970) return "Mid-century modern";
            if (year < 1990) return "Postmodern";
            if (year < 2010) return "Contemporary";
            if (year < 2050) return "Eco-futuristic and sustainable";
            return "Advanced futuristic and high-tech";
        }

        private string GetTechnologyLevel(int year)
        {
            if (year < 1850) return "pre-industrial with gas lamps";
            if (year < 1900) return "early electric with Edison bulbs";
            if (year < 1950) return "early 20th century mechanical";
            if (year < 1980) return "analog electronic";
            if (year < 2000) return "early digital";
            if (year < 2020) return "modern digital with screens";
            if (year < 2050) return "advanced holographic displays";
            return "futuristic AI-integrated systems";
        }

        private string GetAtmosphere(int year)
        {
            if (year < 1900) return "warm gas-lit Victorian";
            if (year < 1950) return "industrial and mechanical";
            if (year < 2000) return "modern and functional";
            if (year < 2030) return "sleek and minimalist";
            if (year < 2100) return "clean and sustainable with advanced lighting";
            return "ethereal futuristic with ambient AI lighting";
        }

        public void OnPanelOpened()
        {
            Debug.Log("TimeTravelController: Panel opened");
            UpdateYearDisplay();
        }

        public void OnPanelClosed()
        {
            Debug.Log("TimeTravelController: Panel closed");
        }

        private void OnDestroy()
        {
            if (yearSlider != null)
            {
                yearSlider.onValueChanged.RemoveListener(OnYearChanged);
            }

            if (applyButton != null)
            {
                applyButton.onClick.RemoveListener(ApplyTimeTravelTransformation);
            }
        }
    }
}

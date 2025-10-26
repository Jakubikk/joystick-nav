using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Custom Prompt feature - allows users to type custom prompts using Meta's built-in keyboard.
    /// No voice-to-text, keyboard input only.
    /// </summary>
    public class CustomPromptFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject featurePanel;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_InputField promptInputField;
        [SerializeField] private TMP_Text instructionsText;
        [SerializeField] private TMP_Text statusText;
        [SerializeField] private Button applyButton;
        [SerializeField] private Button clearButton;

        [Header("WebRTC")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        [Header("Preset Prompts")]
        [SerializeField] private Transform presetListContainer;
        [SerializeField] private GameObject presetItemPrefab;

        [Header("Settings")]
        [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.6f);
        [SerializeField] private Color selectedColor = new Color(0.6f, 0.2f, 0.8f, 1f);
        [SerializeField] private float navigationCooldown = 0.2f;

        private MenuManager menuManager;
        private bool isActive = false;
        private string currentPrompt = "";
        private List<PresetItem> presetItems = new List<PresetItem>();
        private int currentPresetIndex = -1;
        private float lastNavigationTime = 0f;
        private bool isEditingText = false;

        private class PresetItem
        {
            public string name;
            public string prompt;
            public GameObject uiObject;
            public TMP_Text text;
            public Image background;
        }

        // Preset example prompts
        private Dictionary<string, string> presetPrompts = new Dictionary<string, string>()
        {
            { "Cyberpunk Night City", "Transform to cyberpunk night city with neon lights, holographic ads, rain-slicked streets, and futuristic atmosphere" },
            { "Medieval Castle", "Transform to medieval castle with stone walls, torches, banners, gothic architecture, and fantasy atmosphere" },
            { "Underwater World", "Transform to underwater world with coral reefs, fish swimming, blue water, and ocean atmosphere" },
            { "Space Station", "Transform to futuristic space station with metal corridors, windows showing stars, advanced technology, and sci-fi atmosphere" },
            { "Haunted Mansion", "Transform to spooky haunted mansion with cobwebs, dim lighting, old furniture, ghostly atmosphere, and gothic horror" },
            { "Tropical Beach", "Transform to tropical beach paradise with palm trees, white sand, blue ocean, sunshine, and relaxing atmosphere" },
            { "Snowy Mountain Lodge", "Transform to cozy mountain lodge with snow outside, warm fireplace, wooden interior, and winter atmosphere" },
            { "Ancient Egypt", "Transform to ancient Egyptian temple with hieroglyphics, golden statues, sand, and pharaoh atmosphere" },
            { "Steampunk Workshop", "Transform to steampunk workshop with brass gears, steam pipes, Victorian machinery, and industrial atmosphere" },
            { "Enchanted Forest", "Transform to magical enchanted forest with glowing mushrooms, fairy lights, mystical creatures, and fantasy atmosphere" },
        };

        private void Awake()
        {
            menuManager = FindFirstObjectByType<MenuManager>();
            
            if (webRTCConnection == null)
            {
                webRTCConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            if (featurePanel != null)
                featurePanel.SetActive(false);

            // Setup button listeners
            if (applyButton != null)
                applyButton.onClick.AddListener(ApplyCustomPrompt);
            
            if (clearButton != null)
                clearButton.onClick.AddListener(ClearPrompt);

            // Setup input field listeners
            if (promptInputField != null)
            {
                promptInputField.onSelect.AddListener(OnInputFieldSelected);
                promptInputField.onDeselect.AddListener(OnInputFieldDeselected);
            }
        }

        public void Activate()
        {
            isActive = true;
            isEditingText = false;
            
            if (featurePanel != null)
                featurePanel.SetActive(true);

            if (titleText != null)
                titleText.text = "Custom Prompt";

            if (instructionsText != null)
                instructionsText.text = "Select input field to type, or choose preset. Right Trigger: Apply | Left Trigger: Back";

            if (statusText != null)
                statusText.text = "Type your custom prompt or select a preset";

            InitializePresetList();
            UpdatePresetSelection();
        }

        public void Deactivate()
        {
            isActive = false;
            
            if (featurePanel != null)
                featurePanel.SetActive(false);

            ClearPresetList();
        }

        private void InitializePresetList()
        {
            ClearPresetList();

            foreach (var preset in presetPrompts)
            {
                GameObject itemObj = Instantiate(presetItemPrefab, presetListContainer);
                TMP_Text itemText = itemObj.GetComponentInChildren<TMP_Text>();
                Image itemBg = itemObj.GetComponent<Image>();
                Button itemButton = itemObj.GetComponent<Button>();

                if (itemText != null)
                    itemText.text = preset.Key;

                PresetItem item = new PresetItem
                {
                    name = preset.Key,
                    prompt = preset.Value,
                    uiObject = itemObj,
                    text = itemText,
                    background = itemBg
                };

                // Add button click listener
                if (itemButton != null)
                {
                    string promptValue = preset.Value;
                    itemButton.onClick.AddListener(() => SelectPreset(promptValue));
                }

                presetItems.Add(item);
            }

            currentPresetIndex = -1;
        }

        private void ClearPresetList()
        {
            foreach (var item in presetItems)
            {
                if (item.uiObject != null)
                    Destroy(item.uiObject);
            }
            presetItems.Clear();
        }

        private void Update()
        {
            if (!isActive)
                return;

            // Don't handle navigation if we're editing text
            if (!isEditingText)
            {
                HandlePresetNavigation();
                HandleConfirm();
            }
            
            HandleBack();
        }

        private void HandlePresetNavigation()
        {
            if (Time.time - lastNavigationTime < navigationCooldown)
                return;

            Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            if (joystick.y > 0.5f)
            {
                currentPresetIndex--;
                if (currentPresetIndex < -1)
                    currentPresetIndex = presetItems.Count - 1;
                
                lastNavigationTime = Time.time;
                UpdatePresetSelection();
            }
            else if (joystick.y < -0.5f)
            {
                currentPresetIndex++;
                if (currentPresetIndex >= presetItems.Count)
                    currentPresetIndex = -1;
                
                lastNavigationTime = Time.time;
                UpdatePresetSelection();
            }
        }

        private void HandleConfirm()
        {
            // Right trigger to apply prompt
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                // If a preset is selected, use it
                if (currentPresetIndex >= 0 && currentPresetIndex < presetItems.Count)
                {
                    SelectPreset(presetItems[currentPresetIndex].prompt);
                }
                // Otherwise apply the typed prompt
                else
                {
                    ApplyCustomPrompt();
                }
            }
        }

        private void HandleBack()
        {
            // Left trigger to go back
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                // If editing, just deselect the field
                if (isEditingText)
                {
                    if (promptInputField != null)
                        promptInputField.DeactivateInputField();
                    isEditingText = false;
                }
                // Otherwise return to menu
                else if (menuManager != null)
                {
                    menuManager.ReturnToMainMenu();
                }
            }
        }

        private void UpdatePresetSelection()
        {
            for (int i = 0; i < presetItems.Count; i++)
            {
                if (presetItems[i].background != null)
                {
                    presetItems[i].background.color = (i == currentPresetIndex) ? selectedColor : normalColor;
                }
                
                if (presetItems[i].text != null)
                {
                    presetItems[i].text.fontStyle = (i == currentPresetIndex) ? FontStyles.Bold : FontStyles.Normal;
                }
            }
        }

        private void SelectPreset(string prompt)
        {
            if (promptInputField != null)
            {
                promptInputField.text = prompt;
            }
            currentPrompt = prompt;
            
            if (statusText != null)
            {
                statusText.text = "Preset selected. Press Right Trigger to apply.";
            }
        }

        private void ApplyCustomPrompt()
        {
            if (promptInputField != null)
            {
                currentPrompt = promptInputField.text;
            }

            if (string.IsNullOrWhiteSpace(currentPrompt))
            {
                if (statusText != null)
                    statusText.text = "<color=red>Please enter a prompt first!</color>";
                return;
            }

            if (webRTCConnection != null)
            {
                Debug.Log($"Custom Prompt: Applying prompt: {currentPrompt}");
                webRTCConnection.SendCustomPrompt(currentPrompt);
                
                if (statusText != null)
                {
                    statusText.text = $"<color=green>✓ Prompt applied: {currentPrompt}</color>";
                }
            }
            else
            {
                if (statusText != null)
                    statusText.text = "<color=red>WebRTC connection not available!</color>";
            }
        }

        private void ClearPrompt()
        {
            if (promptInputField != null)
            {
                promptInputField.text = "";
            }
            currentPrompt = "";
            
            if (statusText != null)
            {
                statusText.text = "Prompt cleared. Enter a new prompt.";
            }
        }

        private void OnInputFieldSelected(string value)
        {
            isEditingText = true;
            Debug.Log("Input field selected - Meta keyboard should appear");
        }

        private void OnInputFieldDeselected(string value)
        {
            isEditingText = false;
            currentPrompt = value;
            Debug.Log($"Input field deselected - Text: {value}");
        }

        private void OnDestroy()
        {
            if (applyButton != null)
                applyButton.onClick.RemoveListener(ApplyCustomPrompt);
            
            if (clearButton != null)
                clearButton.onClick.RemoveListener(ClearPrompt);
        }
    }
}

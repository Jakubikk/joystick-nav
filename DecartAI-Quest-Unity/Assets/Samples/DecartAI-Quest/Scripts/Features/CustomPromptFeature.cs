using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Custom Prompt feature - allows users to type their own prompts using Meta's built-in keyboard
    /// Supports both Mirage and Lucy models
    /// </summary>
    public class CustomPromptFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_InputField promptInputField;
        [SerializeField] private TMP_Text instructionsText;
        [SerializeField] private TMP_Text modelText;
        [SerializeField] private Button mirageButton;
        [SerializeField] private Button lucyButton;
        [SerializeField] private Button applyButton;
        [SerializeField] private TMP_Text statusText;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Model Selection")]
        [SerializeField] private bool useLucyModel = false;
        
        private float joystickCooldown = 0f;
        private const float JOYSTICK_COOLDOWN_TIME = 0.3f;
        private string currentPrompt = "";
        
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
            
            // Setup input field to use Meta's keyboard
            if (promptInputField != null)
            {
                promptInputField.text = "";
                promptInputField.onSelect.AddListener(OnInputFieldSelected);
                promptInputField.onEndEdit.AddListener(OnInputFieldEndEdit);
                
                // Configure input field for VR
                promptInputField.lineType = TMP_InputField.LineType.MultiLineNewline;
                promptInputField.characterLimit = 500;
            }
            
            // Setup model buttons
            if (mirageButton != null)
            {
                mirageButton.onClick.AddListener(() => SelectModel(false));
            }
            
            if (lucyButton != null)
            {
                lucyButton.onClick.AddListener(() => SelectModel(true));
            }
            
            if (applyButton != null)
            {
                applyButton.onClick.AddListener(ApplyCustomPrompt);
            }
            
            UpdateModelDisplay();
            
            if (instructionsText != null)
            {
                instructionsText.text = "1. Select model (Mirage for environments, Lucy for people)\n" +
                                       "2. Tap text field to open Meta keyboard\n" +
                                       "3. Type your custom prompt\n" +
                                       "4. Right Trigger or Apply button to send\n" +
                                       "Left Trigger: Return to menu";
            }
            
            if (statusText != null)
            {
                statusText.text = "";
            }
        }
        
        private void OnDisable()
        {
            if (promptInputField != null)
            {
                promptInputField.onSelect.RemoveListener(OnInputFieldSelected);
                promptInputField.onEndEdit.RemoveListener(OnInputFieldEndEdit);
            }
            
            if (mirageButton != null)
            {
                mirageButton.onClick.RemoveAllListeners();
            }
            
            if (lucyButton != null)
            {
                lucyButton.onClick.RemoveAllListeners();
            }
            
            if (applyButton != null)
            {
                applyButton.onClick.RemoveAllListeners();
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
            
            // Joystick Up/Down to toggle between Mirage and Lucy
            if (joystickCooldown <= 0)
            {
                Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                
                if (Mathf.Abs(joystick.y) > 0.5f)
                {
                    useLucyModel = !useLucyModel;
                    SelectModel(useLucyModel);
                    joystickCooldown = JOYSTICK_COOLDOWN_TIME;
                }
            }
            
            // Right Trigger = Apply custom prompt
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                ApplyCustomPrompt();
            }
            
            // Button B = Open keyboard (alternative to clicking input field)
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                OpenKeyboard();
            }
        }
        
        private void OnInputFieldSelected(string text)
        {
            Debug.Log("CustomPromptFeature: Input field selected, Meta keyboard should appear");
            
            // Meta's keyboard should automatically appear when input field is selected
            // This is handled by Unity's integration with the Meta platform
            if (statusText != null)
            {
                statusText.text = "Keyboard opened - Type your prompt";
            }
        }
        
        private void OnInputFieldEndEdit(string text)
        {
            currentPrompt = text.Trim();
            Debug.Log($"CustomPromptFeature: Input ended with text: {currentPrompt}");
            
            if (statusText != null && !string.IsNullOrEmpty(currentPrompt))
            {
                statusText.text = "Prompt ready! Press Right Trigger or Apply to send";
            }
        }
        
        private void OpenKeyboard()
        {
            if (promptInputField != null)
            {
                promptInputField.ActivateInputField();
                promptInputField.Select();
            }
        }
        
        private void SelectModel(bool isLucy)
        {
            useLucyModel = isLucy;
            
            if (webRtcConnection != null)
            {
                webRtcConnection.SetModelChoice(useLucyModel);
            }
            
            UpdateModelDisplay();
            
            Debug.Log($"CustomPromptFeature: Switched to {(useLucyModel ? "Lucy" : "Mirage")} model");
        }
        
        private void UpdateModelDisplay()
        {
            if (modelText != null)
            {
                string modelName = useLucyModel ? "Lucy (People & Clothing)" : "Mirage (Environments)";
                modelText.text = $"Model: {modelName}";
            }
            
            // Highlight selected button
            if (mirageButton != null)
            {
                var colors = mirageButton.colors;
                colors.normalColor = useLucyModel ? Color.gray : new Color(0.4f, 0.6f, 1.0f);
                mirageButton.colors = colors;
            }
            
            if (lucyButton != null)
            {
                var colors = lucyButton.colors;
                colors.normalColor = useLucyModel ? new Color(0.4f, 0.6f, 1.0f) : Color.gray;
                lucyButton.colors = colors;
            }
        }
        
        private void ApplyCustomPrompt()
        {
            if (webRtcConnection == null)
            {
                Debug.LogError("CustomPromptFeature: WebRTCConnection not found!");
                if (statusText != null)
                {
                    statusText.text = "<color=red>Error: WebRTC connection not found</color>";
                }
                return;
            }
            
            // Get current text from input field
            if (promptInputField != null)
            {
                currentPrompt = promptInputField.text.Trim();
            }
            
            if (string.IsNullOrEmpty(currentPrompt))
            {
                Debug.LogWarning("CustomPromptFeature: Empty prompt");
                if (statusText != null)
                {
                    statusText.text = "<color=yellow>Please enter a prompt first</color>";
                }
                return;
            }
            
            Debug.Log($"CustomPromptFeature: Sending custom prompt with {(useLucyModel ? "Lucy" : "Mirage")} model: {currentPrompt}");
            
            // Send the prompt to Decart AI
            webRtcConnection.SendCustomPrompt(currentPrompt);
            
            // Provide visual feedback
            if (statusText != null)
            {
                statusText.text = $"<color=green>✓ Sent to {(useLucyModel ? "Lucy" : "Mirage")}:</color>\n{currentPrompt}";
            }
        }
        
        /// <summary>
        /// Public method to set prompt programmatically (for future extensions)
        /// </summary>
        public void SetPrompt(string prompt)
        {
            if (promptInputField != null)
            {
                promptInputField.text = prompt;
                currentPrompt = prompt;
            }
        }
        
        /// <summary>
        /// Public method to clear the prompt
        /// </summary>
        public void ClearPrompt()
        {
            if (promptInputField != null)
            {
                promptInputField.text = "";
                currentPrompt = "";
            }
            
            if (statusText != null)
            {
                statusText.text = "";
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls custom prompt input feature using Meta Quest virtual keyboard
    /// Allows user to type custom transformation prompts
    /// </summary>
    public class CustomPromptController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_InputField promptInputField;
        [SerializeField] private TMP_Text instructionText;
        [SerializeField] private TMP_Text lastPromptText;
        [SerializeField] private Button submitButton;
        [SerializeField] private Button clearButton;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Keyboard Settings")]
        [SerializeField] private string placeholderText = "Enter your custom transformation prompt...";
        [SerializeField] private int maxCharacters = 500;
        
        private string currentPrompt = "";
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            InitializeUI();
        }
        
        private void InitializeUI()
        {
            if (promptInputField != null)
            {
                promptInputField.characterLimit = maxCharacters;
                promptInputField.text = "";
                
                // Set placeholder text
                if (promptInputField.placeholder is TMP_Text placeholder)
                {
                    placeholder.text = placeholderText;
                }
                
                // Add listener for when user finishes editing
                promptInputField.onEndEdit.AddListener(OnPromptTextChanged);
                promptInputField.onValueChanged.AddListener(OnPromptValueChanged);
            }
            
            if (submitButton != null)
            {
                submitButton.onClick.AddListener(SubmitPrompt);
            }
            
            if (clearButton != null)
            {
                clearButton.onClick.AddListener(ClearPrompt);
            }
            
            if (instructionText != null)
            {
                instructionText.text = "Tap the text field to open the virtual keyboard.\nPress Right Trigger or Submit button to apply your prompt.";
            }
        }
        
        private void Update()
        {
            if (!gameObject.activeInHierarchy) return;
            
            HandleSubmitShortcut();
            HandleKeyboardActivation();
        }
        
        private void HandleKeyboardActivation()
        {
            // When user presses right trigger while hovering over input field, open keyboard
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && promptInputField != null)
            {
                // Check if we should activate the keyboard
                if (!promptInputField.isFocused)
                {
                    ActivateKeyboard();
                }
            }
        }
        
        private void HandleSubmitShortcut()
        {
            // Right trigger can also submit when not focused on input field
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && promptInputField != null)
            {
                if (!promptInputField.isFocused && !string.IsNullOrWhiteSpace(currentPrompt))
                {
                    SubmitPrompt();
                }
            }
        }
        
        private void ActivateKeyboard()
        {
            if (promptInputField == null) return;
            
            // Activate the input field to show the Quest keyboard
            promptInputField.ActivateInputField();
            
            // On Quest, this will automatically open the system keyboard
            Debug.Log("Custom Prompt: Activating virtual keyboard");
        }
        
        private void OnPromptValueChanged(string value)
        {
            currentPrompt = value;
        }
        
        private void OnPromptTextChanged(string value)
        {
            currentPrompt = value;
            Debug.Log($"Custom Prompt: Text changed to: {value}");
        }
        
        public void SubmitPrompt()
        {
            if (string.IsNullOrWhiteSpace(currentPrompt))
            {
                Debug.LogWarning("Custom Prompt: Cannot submit empty prompt");
                return;
            }
            
            if (webRtcConnection == null)
            {
                Debug.LogError("Custom Prompt: WebRTC connection not found");
                return;
            }
            
            // Send the custom prompt to Decart AI
            webRtcConnection.SendCustomPrompt(currentPrompt);
            Debug.Log($"Custom Prompt: Submitted - {currentPrompt}");
            
            // Update last prompt text
            if (lastPromptText != null)
            {
                lastPromptText.text = $"Last: {currentPrompt}";
            }
            
            // Optionally clear the input after submission
            // ClearPrompt();
        }
        
        public void ClearPrompt()
        {
            currentPrompt = "";
            
            if (promptInputField != null)
            {
                promptInputField.text = "";
            }
            
            Debug.Log("Custom Prompt: Cleared");
        }
        
        /// <summary>
        /// Public method to set a prompt programmatically (useful for preset buttons)
        /// </summary>
        public void SetPrompt(string prompt)
        {
            currentPrompt = prompt;
            
            if (promptInputField != null)
            {
                promptInputField.text = prompt;
            }
        }
        
        /// <summary>
        /// Public method to submit a quick preset prompt
        /// </summary>
        public void SubmitQuickPrompt(string prompt)
        {
            SetPrompt(prompt);
            SubmitPrompt();
        }
        
        private void OnDestroy()
        {
            if (promptInputField != null)
            {
                promptInputField.onEndEdit.RemoveListener(OnPromptTextChanged);
                promptInputField.onValueChanged.RemoveListener(OnPromptValueChanged);
            }
            
            if (submitButton != null)
            {
                submitButton.onClick.RemoveListener(SubmitPrompt);
            }
            
            if (clearButton != null)
            {
                clearButton.onClick.RemoveListener(ClearPrompt);
            }
        }
    }
}

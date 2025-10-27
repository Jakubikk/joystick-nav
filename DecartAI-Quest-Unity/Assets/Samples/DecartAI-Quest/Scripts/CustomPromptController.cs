using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Custom Prompt feature - allows users to enter custom text prompts
    /// using the Meta Quest keyboard for free-form transformations.
    /// </summary>
    public class CustomPromptController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        [SerializeField] private TMP_InputField promptInputField;
        [SerializeField] private TMP_Text instructionText;
        [SerializeField] private TMP_Text lastPromptText;
        [SerializeField] private Button submitButton;
        
        [Header("Settings")]
        [SerializeField] private int maxPromptLength = 200;
        
        private string currentPrompt = "";
        
        private void Start()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            if (instructionText != null)
            {
                instructionText.text = "Enter your custom prompt below\nPress right trigger or click Submit to apply";
            }
            
            // Setup input field
            if (promptInputField != null)
            {
                promptInputField.characterLimit = maxPromptLength;
                promptInputField.onValueChanged.AddListener(OnPromptTextChanged);
                promptInputField.onEndEdit.AddListener(OnPromptEndEdit);
                
                // Enable the Meta Quest keyboard
                promptInputField.keyboardType = TouchScreenKeyboardType.Default;
            }
            
            // Setup submit button
            if (submitButton != null)
            {
                submitButton.onClick.AddListener(SubmitPrompt);
            }
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            // Right trigger - Submit prompt
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                SubmitPrompt();
            }
            
            // A button - Focus input field to open keyboard
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                FocusInputField();
            }
        }
        
        private void OnPromptTextChanged(string newText)
        {
            currentPrompt = newText;
        }
        
        private void OnPromptEndEdit(string finalText)
        {
            currentPrompt = finalText;
        }
        
        public void FocusInputField()
        {
            if (promptInputField != null)
            {
                promptInputField.Select();
                promptInputField.ActivateInputField();
            }
        }
        
        public void SubmitPrompt()
        {
            if (string.IsNullOrWhiteSpace(currentPrompt))
            {
                Debug.LogWarning("CustomPromptController: Cannot submit empty prompt");
                return;
            }
            
            if (webRtcConnection != null)
            {
                webRtcConnection.SendCustomPrompt(currentPrompt);
                
                if (lastPromptText != null)
                {
                    lastPromptText.text = $"Last prompt: {currentPrompt}";
                }
                
                Debug.Log($"CustomPromptController: Submitted custom prompt: {currentPrompt}");
            }
        }
        
        public void ClearPrompt()
        {
            currentPrompt = "";
            if (promptInputField != null)
            {
                promptInputField.text = "";
            }
        }
        
        public void SetPrompt(string prompt)
        {
            currentPrompt = prompt;
            if (promptInputField != null)
            {
                promptInputField.text = prompt;
            }
        }
        
        public string GetCurrentPrompt()
        {
            return currentPrompt;
        }
    }
}

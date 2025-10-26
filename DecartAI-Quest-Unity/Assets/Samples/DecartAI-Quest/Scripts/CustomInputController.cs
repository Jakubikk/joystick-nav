using UnityEngine;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Custom Input feature allowing users to type their own prompts
    /// using the Meta Quest virtual keyboard.
    /// </summary>
    public class CustomInputController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text inputDisplayText;
        [SerializeField] private TMP_Text instructionText;
        [SerializeField] private TMP_InputField inputField;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        private string currentInput = "";
        private bool keyboardOpen = false;

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
            if (instructionText != null)
            {
                instructionText.text = "Press Right Trigger to open keyboard\nType your custom prompt and press Enter to apply";
            }
            
            if (inputDisplayText != null)
            {
                inputDisplayText.text = "No custom prompt entered";
            }
        }

        private void Update()
        {
            if (!gameObject.activeInHierarchy) return;

            HandleKeyboardToggle();
            HandleSubmit();
            UpdateDisplay();
        }

        private void HandleKeyboardToggle()
        {
            // Right trigger opens the keyboard
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && !keyboardOpen)
            {
                OpenKeyboard();
            }
        }

        private void HandleSubmit()
        {
            // A button or Enter key to submit
            if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Return))
            {
                SubmitPrompt();
            }
        }

        private void OpenKeyboard()
        {
            if (inputField == null)
            {
                Debug.LogWarning("CustomInputController: InputField not assigned!");
                return;
            }

            // Activate the input field which will trigger Meta's virtual keyboard
            inputField.ActivateInputField();
            keyboardOpen = true;
            
            Debug.Log("Opening Meta Quest virtual keyboard");
        }

        private void SubmitPrompt()
        {
            if (inputField == null)
                return;

            currentInput = inputField.text;
            
            if (string.IsNullOrWhiteSpace(currentInput))
            {
                Debug.LogWarning("CustomInputController: Cannot submit empty prompt");
                return;
            }

            if (webRtcConnection == null)
            {
                Debug.LogWarning("CustomInputController: WebRTC connection not available");
                return;
            }

            // Send the custom prompt to Decart
            webRtcConnection.SendCustomPrompt(currentInput);
            
            Debug.Log($"Submitted custom prompt: {currentInput}");
            
            // Close keyboard
            keyboardOpen = false;
            inputField.DeactivateInputField();
        }

        private void UpdateDisplay()
        {
            if (inputDisplayText == null || inputField == null)
                return;

            string displayText = inputField.text;
            
            if (string.IsNullOrWhiteSpace(displayText))
            {
                inputDisplayText.text = "Type your custom prompt...";
            }
            else
            {
                // Show preview of the text being typed
                inputDisplayText.text = displayText;
            }
        }

        public void ClearInput()
        {
            if (inputField != null)
            {
                inputField.text = "";
                currentInput = "";
            }
        }

        public string GetCurrentInput()
        {
            return currentInput;
        }

        // This can be called from UI button as well
        public void OnSubmitButtonPressed()
        {
            SubmitPrompt();
        }
    }
}

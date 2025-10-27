using UnityEngine;
using TMPro;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Controls the Custom Prompt feature - allows users to type their own transformation prompts
    /// Uses Meta's built-in keyboard for text input
    /// </summary>
    public class CustomPromptController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_InputField promptInputField;
        [SerializeField] private TMP_Text instructionText;
        [SerializeField] private TMP_Text currentPromptText;
        [SerializeField] private UnityEngine.UI.Button openKeyboardButton;
        [SerializeField] private UnityEngine.UI.Button applyButton;
        [SerializeField] private UnityEngine.UI.Button clearButton;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        [Header("Suggested Prompts")]
        [SerializeField] private TMP_Text suggestedPromptsText;

        private string currentPrompt = "";

        private void Awake()
        {
            if (webRTCConnection == null)
            {
                webRTCConnection = FindFirstObjectByType<WebRTCConnection>();
            }
        }

        private void Start()
        {
            SetupUI();
            UpdateInstructionText();
            ShowSuggestedPrompts();
        }

        private void SetupUI()
        {
            if (promptInputField != null)
            {
                promptInputField.onValueChanged.AddListener(OnPromptTextChanged);
                // Configure for Meta keyboard
                promptInputField.contentType = TMP_InputField.ContentType.Standard;
                promptInputField.lineType = TMP_InputField.LineType.MultiLineNewline;
            }

            if (openKeyboardButton != null)
            {
                openKeyboardButton.onClick.AddListener(OpenMetaKeyboard);
            }

            if (applyButton != null)
            {
                applyButton.onClick.AddListener(ApplyCustomPrompt);
            }

            if (clearButton != null)
            {
                clearButton.onClick.AddListener(ClearPrompt);
            }
        }

        private void Update()
        {
            if (!gameObject.activeSelf) return;

            HandleInput();
        }

        private void HandleInput()
        {
            // Right trigger to apply prompt
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ApplyCustomPrompt();
            }

            // Y button (Button.Four) to open keyboard
            if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                OpenMetaKeyboard();
            }

            // X button (Button.Three) to clear
            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                ClearPrompt();
            }
        }

        private void OpenMetaKeyboard()
        {
            if (promptInputField != null)
            {
                // Activate the input field to trigger Meta's system keyboard
                promptInputField.ActivateInputField();
                
                // For Quest, this will automatically open the system keyboard
                Debug.Log("CustomPromptController: Opening Meta keyboard");
            }
        }

        private void OnPromptTextChanged(string newText)
        {
            currentPrompt = newText;
            
            if (currentPromptText != null)
            {
                currentPromptText.text = string.IsNullOrEmpty(currentPrompt) ? 
                    "No prompt entered" : 
                    $"Current: {currentPrompt}";
            }
        }

        private void ApplyCustomPrompt()
        {
            if (string.IsNullOrWhiteSpace(currentPrompt))
            {
                Debug.LogWarning("CustomPromptController: Cannot apply empty prompt");
                return;
            }

            if (webRTCConnection == null)
            {
                Debug.LogError("CustomPromptController: WebRTC connection not found");
                return;
            }

            Debug.Log($"CustomPromptController: Applying custom prompt: {currentPrompt}");
            webRTCConnection.SendCustomPrompt(currentPrompt);
        }

        private void ClearPrompt()
        {
            currentPrompt = "";
            if (promptInputField != null)
            {
                promptInputField.text = "";
            }
            OnPromptTextChanged("");
            Debug.Log("CustomPromptController: Prompt cleared");
        }

        private void UpdateInstructionText()
        {
            if (instructionText != null)
            {
                instructionText.text = "Type your custom transformation prompt using the Meta keyboard.\n\n" +
                                      "Controls:\n" +
                                      "• Y Button or tap field: Open keyboard\n" +
                                      "• Right Trigger: Apply prompt\n" +
                                      "• X Button: Clear prompt";
            }
        }

        private void ShowSuggestedPrompts()
        {
            if (suggestedPromptsText != null)
            {
                suggestedPromptsText.text = "Example Prompts:\n\n" +
                    "• Transform into a magical fairy tale forest with glowing mushrooms\n" +
                    "• Make everything look like it's made of candy and sweets\n" +
                    "• Transform the room into a futuristic holographic command center\n" +
                    "• Make it look like an ancient Egyptian temple with hieroglyphics\n" +
                    "• Transform into a cozy hobbit hole with round doors and warm lighting\n" +
                    "• Make everything appear as if underwater in a coral reef\n" +
                    "• Transform the space into a steampunk workshop with brass gears\n" +
                    "• Make it look like a medieval castle throne room\n" +
                    "• Transform into a neon-lit cyberpunk alleyway\n" +
                    "• Make everything look like a watercolor painting";
            }
        }

        public void OnPanelOpened()
        {
            Debug.Log("CustomPromptController: Panel opened");
            UpdateInstructionText();
        }

        public void OnPanelClosed()
        {
            Debug.Log("CustomPromptController: Panel closed");
        }

        private void OnDestroy()
        {
            if (promptInputField != null)
            {
                promptInputField.onValueChanged.RemoveListener(OnPromptTextChanged);
            }

            if (openKeyboardButton != null)
            {
                openKeyboardButton.onClick.RemoveListener(OpenMetaKeyboard);
            }

            if (applyButton != null)
            {
                applyButton.onClick.RemoveListener(ApplyCustomPrompt);
            }

            if (clearButton != null)
            {
                clearButton.onClick.RemoveListener(ClearPrompt);
            }
        }
    }
}

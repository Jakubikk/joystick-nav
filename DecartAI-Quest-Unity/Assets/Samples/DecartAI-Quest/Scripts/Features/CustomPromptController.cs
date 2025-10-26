using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Custom Prompt feature controller.
    /// Allows users to type their own AI transformation prompts using Meta's built-in keyboard.
    /// Works with both Mirage (world) and Lucy (person) models.
    /// </summary>
    public class CustomPromptController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject customPromptUI;
        [SerializeField] private TMP_InputField promptInputField;
        [SerializeField] private TMP_Text currentPromptText;
        [SerializeField] private TMP_Text instructionsText;
        [SerializeField] private Button applyButton;

        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRTCConnection;

        [Header("Keyboard Settings")]
        [SerializeField] private TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.Default;

        private bool isActive = false;
        private TouchScreenKeyboard keyboard;
        private string currentPrompt = "";
        private float inputCooldown = 0f;
        private const float INPUT_DELAY = 0.2f;

        private void Awake()
        {
            if (customPromptUI != null)
                customPromptUI.SetActive(false);

            if (promptInputField != null)
            {
                promptInputField.onEndEdit.AddListener(OnPromptEntered);
            }

            if (applyButton != null)
            {
                applyButton.onClick.AddListener(ApplyCustomPrompt);
            }
        }

        private void Update()
        {
            if (!isActive) return;

            if (inputCooldown > 0)
            {
                inputCooldown -= Time.deltaTime;
                return;
            }

            HandleKeyboardInput();
            HandleQuickApply();
        }

        private void HandleKeyboardInput()
        {
            // Open Meta keyboard when user presses right trigger on input field
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (promptInputField != null)
                {
                    OpenMetaKeyboard();
                    inputCooldown = INPUT_DELAY;
                }
            }
        }

        private void HandleQuickApply()
        {
            // Secondary index trigger (left trigger in this context, but we'll use grip) for quick apply
            // Actually, we want right trigger to apply, left trigger goes back (handled by MenuSystem)
            // So we'll use the A button for apply as an alternative
            if (OVRInput.GetDown(OVRInput.Button.One)) // A button
            {
                ApplyCustomPrompt();
                inputCooldown = INPUT_DELAY;
            }
        }

        private void OpenMetaKeyboard()
        {
            // Use Meta's built-in keyboard for VR text input
            keyboard = TouchScreenKeyboard.Open(
                currentPrompt, 
                keyboardType, 
                false,  // autocorrection
                false,  // multiline
                false,  // secure
                false,  // alert
                "Enter your AI transformation prompt"
            );

            Debug.Log("CustomPromptController: Opened Meta keyboard");
        }

        private void OnPromptEntered(string prompt)
        {
            currentPrompt = prompt;
            UpdatePromptDisplay();
            Debug.Log($"CustomPromptController: Prompt entered: {prompt}");
        }

        private void UpdatePromptDisplay()
        {
            if (currentPromptText != null)
            {
                if (string.IsNullOrEmpty(currentPrompt))
                {
                    currentPromptText.text = "<i>No prompt entered yet</i>";
                    currentPromptText.color = Color.gray;
                }
                else
                {
                    currentPromptText.text = $"\"{currentPrompt}\"";
                    currentPromptText.color = Color.white;
                }
            }

            if (promptInputField != null)
            {
                promptInputField.text = currentPrompt;
            }
        }

        private void ApplyCustomPrompt()
        {
            if (string.IsNullOrEmpty(currentPrompt))
            {
                Debug.LogWarning("CustomPromptController: No prompt to apply");
                return;
            }

            if (webRTCConnection != null)
            {
                webRTCConnection.SendCustomPrompt(currentPrompt);
                Debug.Log($"CustomPromptController: Applied custom prompt: {currentPrompt}");
            }
            else
            {
                Debug.LogWarning("CustomPromptController: WebRTC connection not set");
            }
        }

        public void Activate()
        {
            isActive = true;
            if (customPromptUI != null)
                customPromptUI.SetActive(true);

            UpdatePromptDisplay();

            if (instructionsText != null)
            {
                instructionsText.text = 
                    "<b>Custom AI Prompt</b>\n\n" +
                    "Right Trigger: Open keyboard\n" +
                    "A Button: Apply prompt\n\n" +
                    "<size=12>Examples:\n" +
                    "• \"Make everything look like a Studio Ghibli movie\"\n" +
                    "• \"Transform me into a medieval warrior\"\n" +
                    "• \"Add floating lanterns everywhere\"\n" +
                    "• \"Make my room look like it's underwater\"</size>";
            }

            Debug.Log("CustomPromptController: Activated");
        }

        public void Deactivate()
        {
            isActive = false;
            if (customPromptUI != null)
                customPromptUI.SetActive(false);
            
            Debug.Log("CustomPromptController: Deactivated");
        }

        private void OnDestroy()
        {
            if (promptInputField != null)
            {
                promptInputField.onEndEdit.RemoveListener(OnPromptEntered);
            }

            if (applyButton != null)
            {
                applyButton.onClick.RemoveListener(ApplyCustomPrompt);
            }
        }

        /// <summary>
        /// Allows external components to set a prompt programmatically
        /// </summary>
        public void SetPrompt(string prompt)
        {
            currentPrompt = prompt;
            UpdatePromptDisplay();
        }

        /// <summary>
        /// Gets the current prompt
        /// </summary>
        public string GetPrompt()
        {
            return currentPrompt;
        }

        /// <summary>
        /// Clears the current prompt
        /// </summary>
        public void ClearPrompt()
        {
            currentPrompt = "";
            UpdatePromptDisplay();
        }
    }
}

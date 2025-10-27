using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Handles Meta Quest keyboard input for custom prompts
    /// </summary>
    public class KeyboardInputManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button submitButton;
        [SerializeField] private GameObject keyboardPanel;

        [Header("Settings")]
        [SerializeField] private string placeholderText = "Enter custom prompt...";
        [SerializeField] private int maxCharacters = 200;

        private TouchScreenKeyboard keyboard;
        private MenuSystem menuSystem;

        private void Start()
        {
            if (inputField != null)
            {
                inputField.placeholder.GetComponent<TMP_Text>().text = placeholderText;
                inputField.characterLimit = maxCharacters;
            }

            if (submitButton != null)
            {
                submitButton.onClick.AddListener(OnSubmitClicked);
            }

            menuSystem = FindFirstObjectByType<MenuSystem>();
        }

        public void OpenKeyboard()
        {
            if (keyboardPanel != null)
            {
                keyboardPanel.SetActive(true);
            }

            // Open Meta's native keyboard
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, placeholderText);
        }

        public void CloseKeyboard()
        {
            if (keyboardPanel != null)
            {
                keyboardPanel.SetActive(false);
            }

            if (keyboard != null)
            {
                keyboard.active = false;
            }
        }

        private void Update()
        {
            // Update input field with keyboard text
            if (keyboard != null && keyboard.active && inputField != null)
            {
                inputField.text = keyboard.text;
            }

            // Auto-close when keyboard is done
            if (keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                if (!string.IsNullOrEmpty(keyboard.text))
                {
                    OnSubmitClicked();
                }
                keyboard = null;
            }
            else if (keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Canceled)
            {
                keyboard = null;
            }
        }

        private void OnSubmitClicked()
        {
            if (inputField == null) return;

            string prompt = inputField.text.Trim();
            
            if (string.IsNullOrEmpty(prompt))
            {
                Debug.LogWarning("KeyboardInputManager: Empty prompt, not submitting");
                return;
            }

            // Send to menu system which will forward to WebRTC
            if (menuSystem != null)
            {
                menuSystem.SubmitCustomPromptFromKeyboard(prompt);
            }

            // Clear input field
            inputField.text = "";
            
            // Close keyboard
            CloseKeyboard();

            Debug.Log($"KeyboardInputManager: Submitted prompt - {prompt}");
        }

        public void ClearInput()
        {
            if (inputField != null)
            {
                inputField.text = "";
            }
        }
    }
}

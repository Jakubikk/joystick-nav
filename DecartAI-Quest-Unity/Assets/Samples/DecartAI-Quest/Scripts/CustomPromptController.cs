using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Custom Prompt feature - allows user to type custom transformation prompts
    /// Uses Meta's system keyboard for text input
    /// </summary>
    public class CustomPromptController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject customPanel;
        [SerializeField] private TMP_Text instructionText;
        [SerializeField] private TMP_Text currentPromptText;
        [SerializeField] private TMP_Text historyText;
        [SerializeField] private Button openKeyboardButton;
        
        private WebRTCConnection webRtcConnection;
        private string currentPrompt = "";
        private System.Collections.Generic.List<string> promptHistory = new System.Collections.Generic.List<string>();
        private int maxHistoryItems = 5;
        
        private void Awake()
        {
            if (openKeyboardButton != null)
            {
                openKeyboardButton.onClick.AddListener(OpenSystemKeyboard);
            }
        }
        
        public void Activate()
        {
            if (customPanel != null)
            {
                customPanel.SetActive(true);
            }
            
            MenuManager menuManager = FindFirstObjectByType<MenuManager>();
            if (menuManager != null)
            {
                webRtcConnection = menuManager.GetWebRTCConnection();
            }
            
            UpdateDisplay();
            
            if (instructionText != null)
            {
                instructionText.text = "Right Trigger: Open Keyboard | Type your custom prompt";
            }
        }
        
        private void OnDisable()
        {
            if (customPanel != null)
            {
                customPanel.SetActive(false);
            }
        }
        
        private void UpdateDisplay()
        {
            if (currentPromptText != null)
            {
                if (string.IsNullOrEmpty(currentPrompt))
                {
                    currentPromptText.text = "<color=#888888>No prompt entered yet...</color>";
                }
                else
                {
                    currentPromptText.text = $"Current: {currentPrompt}";
                }
            }
            
            if (historyText != null)
            {
                if (promptHistory.Count == 0)
                {
                    historyText.text = "<color=#888888>No prompt history</color>";
                }
                else
                {
                    string history = "Recent Prompts:\n";
                    for (int i = promptHistory.Count - 1; i >= 0; i--)
                    {
                        history += $"• {promptHistory[i]}\n";
                    }
                    historyText.text = history;
                }
            }
        }
        
        public void NavigateUp()
        {
            // Could be used to navigate through prompt history
            // For now, just opens keyboard
            OpenSystemKeyboard();
        }
        
        public void NavigateDown()
        {
            // Could be used to navigate through prompt history
            // For now, just opens keyboard
            OpenSystemKeyboard();
        }
        
        public void Confirm()
        {
            if (!string.IsNullOrEmpty(currentPrompt))
            {
                SendCurrentPrompt();
            }
            else
            {
                OpenSystemKeyboard();
            }
        }
        
        private void OpenSystemKeyboard()
        {
            Debug.Log("Opening Meta system keyboard...");
            
            // Use Unity's TouchScreenKeyboard which works with Meta Quest's system keyboard
            TouchScreenKeyboard keyboard = TouchScreenKeyboard.Open(
                currentPrompt,
                TouchScreenKeyboardType.Default,
                false,
                false,
                false,
                false,
                "Enter your custom transformation prompt"
            );
            
            // Start coroutine to monitor keyboard input
            StartCoroutine(MonitorKeyboard(keyboard));
        }
        
        private System.Collections.IEnumerator MonitorKeyboard(TouchScreenKeyboard keyboard)
        {
            // Wait for keyboard to be dismissed
            while (keyboard.status == TouchScreenKeyboard.Status.Visible)
            {
                yield return null;
            }
            
            // Check if user entered text
            if (keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                string newText = keyboard.text;
                
                if (!string.IsNullOrEmpty(newText))
                {
                    currentPrompt = newText;
                    Debug.Log($"User entered prompt: {currentPrompt}");
                    UpdateDisplay();
                    
                    // Optionally auto-send when keyboard is closed
                    // SendCurrentPrompt();
                }
            }
            else if (keyboard.status == TouchScreenKeyboard.Status.Canceled)
            {
                Debug.Log("Keyboard input cancelled");
            }
        }
        
        private void SendCurrentPrompt()
        {
            if (string.IsNullOrEmpty(currentPrompt))
            {
                Debug.LogWarning("Cannot send empty prompt");
                return;
            }
            
            if (webRtcConnection != null)
            {
                Debug.Log($"Sending custom prompt: {currentPrompt}");
                webRtcConnection.SendCustomPrompt(currentPrompt);
                
                // Add to history
                AddToHistory(currentPrompt);
                
                // Clear current prompt for next input
                // currentPrompt = "";
                UpdateDisplay();
            }
            else
            {
                Debug.LogError("WebRTC connection not available");
            }
        }
        
        private void AddToHistory(string prompt)
        {
            // Add to history, maintaining max size
            promptHistory.Add(prompt);
            
            if (promptHistory.Count > maxHistoryItems)
            {
                promptHistory.RemoveAt(0);
            }
        }
        
        /// <summary>
        /// Alternative method using OVRInput to trigger keyboard
        /// This can be called from MenuManager's confirm action
        /// </summary>
        public void TriggerKeyboardInput()
        {
            OpenSystemKeyboard();
        }
        
        /// <summary>
        /// Quick prompt presets that users can select
        /// </summary>
        public void UseQuickPrompt(string prompt)
        {
            currentPrompt = prompt;
            UpdateDisplay();
            SendCurrentPrompt();
        }
        
        // Some example quick prompts that could be exposed as buttons
        public void QuickPromptAnime()
        {
            UseQuickPrompt("Transform to anime cartoon style with vibrant colors and bold outlines");
        }
        
        public void QuickPromptOilPainting()
        {
            UseQuickPrompt("Transform to oil painting style with visible brush strokes and rich colors");
        }
        
        public void QuickPromptCyberpunk()
        {
            UseQuickPrompt("Transform to cyberpunk style with neon lights and futuristic atmosphere");
        }
        
        public void QuickPromptWatercolor()
        {
            UseQuickPrompt("Transform to watercolor painting style with soft flowing colors");
        }
    }
}

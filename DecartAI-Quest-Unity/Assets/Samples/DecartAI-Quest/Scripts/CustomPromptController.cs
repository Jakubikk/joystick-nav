using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Handles custom prompt input using Meta keyboard
    /// Allows users to type their own transformation prompts
    /// </summary>
    public class CustomPromptController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_InputField promptInputField;
        [SerializeField] private TMP_Text instructionText;
        [SerializeField] private TMP_Text statusText;
        [SerializeField] private Button submitButton;
        [SerializeField] private Button clearButton;
        
        [Header("WebRTC Connection")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Recent Prompts")]
        [SerializeField] private Transform recentPromptsContainer;
        [SerializeField] private GameObject recentPromptItemPrefab;
        [SerializeField] private int maxRecentPrompts = 5;
        
        private System.Collections.Generic.List<string> recentPrompts = new System.Collections.Generic.List<string>();
        
        private void OnEnable()
        {
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            SetupUI();
            UpdateRecentPromptsList();
        }
        
        private void SetupUI()
        {
            if (instructionText != null)
            {
                instructionText.text = "Enter your custom transformation prompt below.\n" +
                                      "Right Trigger: Submit | Left Trigger: Back to Menu";
            }
            
            if (promptInputField != null)
            {
                promptInputField.text = "";
                promptInputField.placeholder.GetComponent<TMP_Text>().text = 
                    "E.g., Transform into a magical forest with glowing mushrooms...";
                
                // Set up the input field to use the Meta keyboard
                promptInputField.shouldHideMobileInput = false;
                promptInputField.onSelect.AddListener(OnInputFieldSelected);
            }
            
            if (submitButton != null)
            {
                submitButton.onClick.AddListener(SubmitPrompt);
            }
            
            if (clearButton != null)
            {
                clearButton.onClick.AddListener(ClearPrompt);
            }
            
            UpdateStatus("");
        }
        
        private void OnInputFieldSelected(string text)
        {
            // This will trigger the Meta Quest keyboard to appear
            Debug.Log("Input field selected - Meta keyboard should appear");
        }
        
        private void Update()
        {
            // Right trigger to submit
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                SubmitPrompt();
            }
            
            // Alternative: Press A button to submit
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                SubmitPrompt();
            }
            
            // B button to clear
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                ClearPrompt();
            }
        }
        
        private void SubmitPrompt()
        {
            if (promptInputField == null || string.IsNullOrWhiteSpace(promptInputField.text))
            {
                UpdateStatus("Please enter a prompt first!");
                return;
            }
            
            string prompt = promptInputField.text.Trim();
            
            if (webRtcConnection != null)
            {
                webRtcConnection.SendCustomPrompt(prompt);
                UpdateStatus($"Submitted: {prompt}");
                
                // Add to recent prompts
                AddToRecentPrompts(prompt);
                
                // Clear the input field
                promptInputField.text = "";
                
                Debug.Log($"Custom Prompt Submitted: {prompt}");
            }
            else
            {
                UpdateStatus("Error: WebRTC connection not available");
            }
        }
        
        private void ClearPrompt()
        {
            if (promptInputField != null)
            {
                promptInputField.text = "";
                UpdateStatus("Prompt cleared");
            }
        }
        
        private void UpdateStatus(string message)
        {
            if (statusText != null)
            {
                statusText.text = message;
            }
        }
        
        private void AddToRecentPrompts(string prompt)
        {
            // Add to beginning of list
            if (!recentPrompts.Contains(prompt))
            {
                recentPrompts.Insert(0, prompt);
                
                // Keep only the most recent prompts
                if (recentPrompts.Count > maxRecentPrompts)
                {
                    recentPrompts.RemoveAt(recentPrompts.Count - 1);
                }
                
                UpdateRecentPromptsList();
            }
        }
        
        private void UpdateRecentPromptsList()
        {
            if (recentPromptsContainer == null || recentPromptItemPrefab == null)
                return;
            
            // Clear existing items
            foreach (Transform child in recentPromptsContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Create new items
            foreach (string prompt in recentPrompts)
            {
                GameObject itemObj = Instantiate(recentPromptItemPrefab, recentPromptsContainer);
                TMP_Text itemText = itemObj.GetComponentInChildren<TMP_Text>();
                
                if (itemText != null)
                {
                    // Truncate long prompts for display
                    string displayText = prompt.Length > 50 ? prompt.Substring(0, 47) + "..." : prompt;
                    itemText.text = displayText;
                }
                
                // Add click handler to reuse recent prompt
                Button itemButton = itemObj.GetComponent<Button>();
                if (itemButton != null)
                {
                    string capturedPrompt = prompt; // Capture for lambda
                    itemButton.onClick.AddListener(() => ReusePrompt(capturedPrompt));
                }
            }
        }
        
        private void ReusePrompt(string prompt)
        {
            if (promptInputField != null)
            {
                promptInputField.text = prompt;
                UpdateStatus("Prompt loaded from recent history");
            }
        }
        
        private void OnDisable()
        {
            // Clean up listeners
            if (promptInputField != null)
            {
                promptInputField.onSelect.RemoveListener(OnInputFieldSelected);
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

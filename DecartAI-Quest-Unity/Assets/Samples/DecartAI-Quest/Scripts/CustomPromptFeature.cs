using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebRTC;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Controls the Custom Prompt feature allowing users to type custom prompts using the Meta keyboard.
    /// Integrates with Decart API for custom transformations.
    /// </summary>
    public class CustomPromptFeature : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_InputField promptInputField;
        [SerializeField] private Button openKeyboardButton;
        [SerializeField] private Button submitButton;
        [SerializeField] private Button clearButton;
        [SerializeField] private TMP_Text statusText;
        [SerializeField] private TMP_Text instructionText;
        
        [Header("WebRTC Integration")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        
        [Header("Keyboard Settings")]
        [SerializeField] private string placeholderText = "Enter your custom transformation prompt...";
        
        private string currentPrompt = "";
        
        private void Start()
        {
            SetupUI();
        }
        
        private void SetupUI()
        {
            if (instructionText != null)
            {
                instructionText.text = "Type a custom prompt to transform your environment\n" +
                                      "Press RIGHT TRIGGER to open keyboard\n" +
                                      "Examples: 'Make everything look like candy', 'Turn into a spaceship interior'";
            }
            
            if (promptInputField != null)
            {
                promptInputField.placeholder.GetComponent<TMP_Text>().text = placeholderText;
                promptInputField.onValueChanged.AddListener(OnPromptChanged);
                promptInputField.characterLimit = 500;
            }
            
            if (openKeyboardButton != null)
            {
                openKeyboardButton.onClick.AddListener(OpenMetaKeyboard);
            }
            
            if (submitButton != null)
            {
                submitButton.onClick.AddListener(SubmitPrompt);
            }
            
            if (clearButton != null)
            {
                clearButton.onClick.AddListener(ClearPrompt);
            }
            
            UpdateStatus("Ready - Enter a custom prompt");
        }
        
        private void Update()
        {
            if (!gameObject.activeSelf) return;
            
            // Right trigger opens keyboard
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                OpenMetaKeyboard();
            }
            
            // A button submits prompt
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                SubmitPrompt();
            }
            
            // B button clears prompt
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                ClearPrompt();
            }
        }
        
        private void OnPromptChanged(string newText)
        {
            currentPrompt = newText;
        }
        
        /// <summary>
        /// Opens the Meta Quest system keyboard for text input.
        /// </summary>
        private void OpenMetaKeyboard()
        {
            if (promptInputField != null)
            {
                // Focus the input field which should trigger the system keyboard on Quest
                promptInputField.Select();
                promptInputField.ActivateInputField();
                
                UpdateStatus("Keyboard opened - Type your prompt");
                
                // Alternative method using TouchScreenKeyboard for Quest
                #if UNITY_ANDROID && !UNITY_EDITOR
                TouchScreenKeyboard.Open(
                    currentPrompt, 
                    TouchScreenKeyboardType.Default, 
                    false, 
                    false, 
                    false, 
                    false, 
                    placeholderText
                );
                #endif
            }
        }
        
        /// <summary>
        /// Submits the current prompt to the Decart API.
        /// </summary>
        private void SubmitPrompt()
        {
            if (string.IsNullOrWhiteSpace(currentPrompt))
            {
                UpdateStatus("Error: Please enter a prompt first");
                return;
            }
            
            if (webRtcConnection == null)
            {
                UpdateStatus("Error: WebRTC connection not available");
                Debug.LogError("WebRTC connection is null");
                return;
            }
            
            // Send prompt to Decart
            Debug.Log($"Submitting custom prompt: {currentPrompt}");
            webRtcConnection.SendCustomPrompt(currentPrompt);
            
            UpdateStatus($"Applying: {TruncatePrompt(currentPrompt, 50)}");
            
            // Optional: Clear after submitting
            // ClearPrompt();
        }
        
        /// <summary>
        /// Clears the current prompt text.
        /// </summary>
        private void ClearPrompt()
        {
            currentPrompt = "";
            
            if (promptInputField != null)
            {
                promptInputField.text = "";
            }
            
            UpdateStatus("Prompt cleared - Enter a new prompt");
        }
        
        /// <summary>
        /// Updates the status text display.
        /// </summary>
        private void UpdateStatus(string message)
        {
            if (statusText != null)
            {
                statusText.text = message;
            }
            
            Debug.Log($"CustomPrompt Status: {message}");
        }
        
        /// <summary>
        /// Truncates a prompt string for display purposes.
        /// </summary>
        private string TruncatePrompt(string prompt, int maxLength)
        {
            if (string.IsNullOrEmpty(prompt))
            {
                return "";
            }
            
            if (prompt.Length <= maxLength)
            {
                return prompt;
            }
            
            return prompt.Substring(0, maxLength) + "...";
        }
        
        /// <summary>
        /// Provides some example prompts for quick access.
        /// </summary>
        public void UseExamplePrompt(int exampleIndex)
        {
            string[] examples = new string[]
            {
                "Transform everything into a magical candy land with chocolate walls and lollipop trees",
                "Make the environment look like it's underwater with coral and swimming fish",
                "Turn the room into a cozy library with wooden bookshelves and warm lighting",
                "Transform into a spaceship interior with holographic controls and star views",
                "Make everything look like it's made of glass with crystal clear transparency",
                "Turn the environment into a wild west saloon with wooden floors and old furniture",
                "Transform into an ancient Egyptian tomb with hieroglyphics and golden treasures",
                "Make it look like a tropical tiki bar with bamboo and palm leaf decorations",
                "Turn everything into a winter ice palace with frozen architecture and snow",
                "Transform into a steampunk laboratory with brass gears and vintage machinery"
            };
            
            if (exampleIndex >= 0 && exampleIndex < examples.Length)
            {
                currentPrompt = examples[exampleIndex];
                
                if (promptInputField != null)
                {
                    promptInputField.text = currentPrompt;
                }
                
                UpdateStatus($"Example loaded: {TruncatePrompt(currentPrompt, 40)}");
            }
        }
        
        private void OnEnable()
        {
            Debug.Log("Custom Prompt feature activated");
            UpdateStatus("Ready - Enter a custom prompt");
        }
        
        private void OnDisable()
        {
            Debug.Log("Custom Prompt feature deactivated");
        }
    }
}

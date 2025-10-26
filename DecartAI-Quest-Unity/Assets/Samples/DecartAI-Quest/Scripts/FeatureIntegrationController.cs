using UnityEngine;
using SimpleWebRTC;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Main integration controller that coordinates all feature controllers
    /// and manages the overall app state and navigation flow.
    /// </summary>
    public class FeatureIntegrationController : MonoBehaviour
    {
        [Header("Feature Controllers")]
        [SerializeField] private MenuManager menuManager;
        [SerializeField] private TimeTravelController timeTravelController;
        [SerializeField] private ClothingController clothingController;
        [SerializeField] private BiomeController biomeController;
        [SerializeField] private GameStyleController gameStyleController;
        [SerializeField] private CustomInputController customInputController;
        
        [Header("WebRTC")]
        [SerializeField] private WebRTCConnection webRtcConnection;
        [SerializeField] private WebRTCController webRtcController;

        private void Start()
        {
            InitializeControllers();
        }

        private void InitializeControllers()
        {
            // Find WebRTC components if not assigned
            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }

            if (webRtcController == null)
            {
                webRtcController = FindFirstObjectByType<WebRTCController>();
            }

            // Find menu manager if not assigned
            if (menuManager == null)
            {
                menuManager = FindFirstObjectByType<MenuManager>();
            }

            // Find all feature controllers if not assigned
            if (timeTravelController == null)
            {
                timeTravelController = FindFirstObjectByType<TimeTravelController>();
            }

            if (clothingController == null)
            {
                clothingController = FindFirstObjectByType<ClothingController>();
            }

            if (biomeController == null)
            {
                biomeController = FindFirstObjectByType<BiomeController>();
            }

            if (gameStyleController == null)
            {
                gameStyleController = FindFirstObjectByType<GameStyleController>();
            }

            if (customInputController == null)
            {
                customInputController = FindFirstObjectByType<CustomInputController>();
            }

            Debug.Log("FeatureIntegrationController: All controllers initialized.");
        }

        private void Update()
        {
            // Monitor active feature and provide global controls if needed
            UpdateActiveFeature();
        }

        private void UpdateActiveFeature()
        {
            if (menuManager == null)
                return;

            // You can add global state management here if needed
            // For example, disabling WebRTCController's default input when menu is active
        }

        /// <summary>
        /// Public method to send a prompt from any feature controller
        /// </summary>
        public void SendPromptToDecart(string prompt)
        {
            if (webRtcConnection != null)
            {
                webRtcConnection.SendCustomPrompt(prompt);
                Debug.Log($"FeatureIntegrationController: Sent prompt - {prompt}");
            }
            else
            {
                Debug.LogWarning("FeatureIntegrationController: WebRTC connection not available!");
            }
        }

        /// <summary>
        /// Get the active WebRTC connection for feature controllers
        /// </summary>
        public WebRTCConnection GetWebRTCConnection()
        {
            return webRtcConnection;
        }

        /// <summary>
        /// Queue a custom prompt through the main WebRTC controller
        /// </summary>
        public void QueuePrompt(string prompt)
        {
            if (webRtcController != null)
            {
                webRtcController.QueueCustomPrompt(prompt);
            }
            else if (webRtcConnection != null)
            {
                webRtcConnection.SendCustomPrompt(prompt);
            }
        }
    }
}

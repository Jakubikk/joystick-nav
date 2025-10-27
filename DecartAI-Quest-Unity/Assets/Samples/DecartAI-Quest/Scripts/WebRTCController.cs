using PassthroughCameraSamples;
using SimpleWebRTC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Main WebRTC controller - manages camera feed and video processing
    /// Voice features removed as per requirements
    /// </summary>
    public class WebRTCController : MonoBehaviour
    {
        [Tooltip("UI RawImage where the local passthrough camera feed will be displayed.")]
        [SerializeField] private RawImage canvasRawImage;

        [Tooltip("UI Text element that shows the current prompt fed to the Decart model.")]
        [SerializeField] private TMP_Text promptNameText;

        [Tooltip("Reference to the WebRTCConnection handling signaling and video streaming.")]
        [SerializeField] private WebRTCConnection webRtcConnection;

        [Tooltip("Manager for controlling the passthrough WebCamTexture on Quest devices.")]
        [SerializeField] private WebCamTextureManager passthroughCameraManager;
        
        [Tooltip("Reference to the MenuManager for the new menu system.")]
        [SerializeField] private MenuManager menuManager;

        private bool _videoReceivedAndReady;
        private WebCamTexture _webcamTexture;
        private readonly Queue<string> _promptQueue = new();

        private IEnumerator Start()
        {
            if (passthroughCameraManager == null)
            {
                passthroughCameraManager = FindFirstObjectByType<WebCamTextureManager>();
            }

            if (webRtcConnection == null)
            {
                webRtcConnection = FindFirstObjectByType<WebRTCConnection>();
            }
            
            if (menuManager == null)
            {
                menuManager = FindFirstObjectByType<MenuManager>();
            }

            if (passthroughCameraManager == null || webRtcConnection == null)
            {
                Debug.LogError("WebRTCController: Missing required components.");
                yield break;
            }

            var timeout = Time.time + 5f;
            yield return new WaitUntil(() =>
                (passthroughCameraManager.WebCamTexture != null &&
                 passthroughCameraManager.WebCamTexture.isPlaying) ||
                Time.time > timeout);

            if (passthroughCameraManager.WebCamTexture == null || !passthroughCameraManager.WebCamTexture.isPlaying)
            {
                Debug.LogError("WebRTCController: Camera failed to start.");
                yield break;
            }

            _webcamTexture = passthroughCameraManager.WebCamTexture;
            if (canvasRawImage != null)
            {
                canvasRawImage.texture = _webcamTexture;
            }

            webRtcConnection.VideoTransmissionReceived.AddListener(OnVideoReceived);
            webRtcConnection.PromptNameUpdated.AddListener(UpdatePromptName);
            Debug.Log("WebRTCController: Initialized successfully (Voice features disabled).");
        }

        private void OnDestroy()
        {
            if (webRtcConnection == null)
            {
                return;
            }
            webRtcConnection.VideoTransmissionReceived.RemoveListener(OnVideoReceived);
            webRtcConnection.PromptNameUpdated.RemoveListener(UpdatePromptName);
        }

        private void OnVideoReceived()
        {
            _videoReceivedAndReady = true;
        }

        private void UpdatePromptName(string promptKey)
        {
            if (promptNameText != null)
            {
                promptNameText.text = string.IsNullOrEmpty(promptKey) ? "" : promptKey;
            }
        }

        public void QueueCustomPrompt(string prompt)
        {
            if (!string.IsNullOrEmpty(prompt))
            {
                _promptQueue.Enqueue(prompt);
            }
        }

        private void Update()
        {
            if (!_videoReceivedAndReady || !webRtcConnection)
            {
                return;
            }

            // Legacy button handling removed - now handled by MenuManager
            // Old A/B button navigation is replaced with new navigation scheme
            SendQueuedPrompts();
        }

        // Legacy input handling removed - navigation now handled by MenuManager
        // New navigation scheme:
        // - Left Trigger: Back
        // - Right Trigger: Confirm
        // - Joystick Up/Down: Navigate
        // - Hamburger Button: Hide/Show Menu

        private void SendQueuedPrompts()
        {
            while (_promptQueue.Count > 0)
            {
                var prompt = _promptQueue.Dequeue();
                webRtcConnection.SendCustomPrompt(prompt);
            }
        }
    }
}

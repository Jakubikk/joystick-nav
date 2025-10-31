using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Displays control instructions at the bottom of menus
    /// </summary>
    public class ControlsDisplay : MonoBehaviour
    {
        [Header("Text Components")]
        [SerializeField] private TMP_Text instructionsText;
        
        [Header("Control Icons (Optional)")]
        [SerializeField] private Image leftTriggerIcon;
        [SerializeField] private Image rightTriggerIcon;
        [SerializeField] private Image joystickIcon;
        [SerializeField] private Image startButtonIcon;
        
        [Header("Display Settings")]
        [SerializeField] private bool showOnStart = true;
        [SerializeField] private float fadeInDuration = 0.5f;
        
        private CanvasGroup canvasGroup;
        
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            
            if (!showOnStart)
            {
                canvasGroup.alpha = 0f;
            }
        }
        
        private void Start()
        {
            UpdateInstructions(MenuContext.MainMenu);
        }
        
        public void UpdateInstructions(MenuContext context)
        {
            if (instructionsText == null) return;
            
            switch (context)
            {
                case MenuContext.MainMenu:
                    instructionsText.text = "⬆️⬇️ Navigate  |  🎯 Right Trigger: Select  |  ☰ Menu: Hide/Show";
                    break;
                    
                case MenuContext.TimeTravel:
                    instructionsText.text = "⬅️➡️ Adjust Year  |  🎯 Right Trigger: Apply  |  ⬅ Left Trigger: Back";
                    break;
                    
                case MenuContext.ClothingList:
                    instructionsText.text = "⬆️⬇️ Browse Outfits  |  🎯 Right Trigger: Try On  |  ⬅ Left Trigger: Back";
                    break;
                    
                case MenuContext.BiomeList:
                    instructionsText.text = "⬆️⬇️ Browse Biomes  |  🎯 Right Trigger: Transform  |  ⬅ Left Trigger: Back";
                    break;
                    
                case MenuContext.VideoGameList:
                    instructionsText.text = "⬆️⬇️ Browse Games  |  🎯 Right Trigger: Apply Style  |  ⬅ Left Trigger: Back";
                    break;
                    
                case MenuContext.CustomPrompt:
                    instructionsText.text = "🎯 Right Trigger: Open Keyboard  |  ⬅ Left Trigger: Back";
                    break;
                    
                default:
                    instructionsText.text = "⬆️⬇️ Navigate  |  🎯 Select  |  ⬅ Back  |  ☰ Menu";
                    break;
            }
        }
        
        public void Show()
        {
            if (canvasGroup != null)
            {
                StopAllCoroutines();
                StartCoroutine(FadeIn());
            }
        }
        
        public void Hide()
        {
            if (canvasGroup != null)
            {
                StopAllCoroutines();
                StartCoroutine(FadeOut());
            }
        }
        
        private System.Collections.IEnumerator FadeIn()
        {
            float elapsed = 0f;
            while (elapsed < fadeInDuration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeInDuration);
                yield return null;
            }
            canvasGroup.alpha = 1f;
        }
        
        private System.Collections.IEnumerator FadeOut()
        {
            float elapsed = 0f;
            float startAlpha = canvasGroup.alpha;
            while (elapsed < fadeInDuration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeInDuration);
                yield return null;
            }
            canvasGroup.alpha = 0f;
        }
    }
    
    public enum MenuContext
    {
        MainMenu,
        TimeTravel,
        ClothingList,
        BiomeList,
        VideoGameList,
        CustomPrompt
    }
}

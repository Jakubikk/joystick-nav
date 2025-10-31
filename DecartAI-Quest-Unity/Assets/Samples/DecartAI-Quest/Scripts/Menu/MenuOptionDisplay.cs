using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Helper component to display menu options with nice formatting
    /// </summary>
    public class MenuOptionDisplay : MonoBehaviour
    {
        [Header("Display Components")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text navigationHintText;
        [SerializeField] private Image backgroundPanel;
        
        [Header("Visual Settings")]
        [SerializeField] private Color selectedBackgroundColor = new Color(0.2f, 0.6f, 1f, 0.8f);
        [SerializeField] private Color normalBackgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.6f);
        
        private bool isSelected = false;
        
        public void SetOption(string title, string description = "", bool selected = false)
        {
            if (titleText != null)
            {
                titleText.text = title;
            }
            
            if (descriptionText != null)
            {
                descriptionText.text = description;
                descriptionText.gameObject.SetActive(!string.IsNullOrEmpty(description));
            }
            
            SetSelected(selected);
        }
        
        public void SetSelected(bool selected)
        {
            isSelected = selected;
            
            if (backgroundPanel != null)
            {
                backgroundPanel.color = selected ? selectedBackgroundColor : normalBackgroundColor;
            }
            
            if (titleText != null)
            {
                titleText.fontStyle = selected ? FontStyles.Bold : FontStyles.Normal;
            }
        }
        
        public void SetNavigationHint(string hint)
        {
            if (navigationHintText != null)
            {
                navigationHintText.text = hint;
                navigationHintText.gameObject.SetActive(!string.IsNullOrEmpty(hint));
            }
        }
        
        public void ShowNavigationControls(bool show)
        {
            if (navigationHintText != null)
            {
                navigationHintText.gameObject.SetActive(show);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace QuestCameraKit.Menu
{
    /// <summary>
    /// Displays a list of selectable options with joystick navigation
    /// Used for Clothing, Biome, and Video Game menus
    /// </summary>
    public class OptionListDisplay : MonoBehaviour
    {
        [Header("Display Components")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text currentOptionNameText;
        [SerializeField] private TMP_Text currentOptionDescriptionText;
        [SerializeField] private TMP_Text indexText; // Shows "3 / 15"
        
        [Header("Visual Components")]
        [SerializeField] private Image previewImage; // Optional preview image
        [SerializeField] private GameObject leftArrow;
        [SerializeField] private GameObject rightArrow;
        
        [Header("List Settings")]
        [SerializeField] private List<OptionData> options = new List<OptionData>();
        [SerializeField] private int currentIndex = 0;
        
        [Header("Colors")]
        [SerializeField] private Color activeArrowColor = Color.white;
        [SerializeField] private Color inactiveArrowColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        
        private bool isActive = false;
        
        public void SetActive(bool active)
        {
            isActive = active;
            if (active)
            {
                UpdateDisplay();
            }
        }
        
        public void SetOptions(List<OptionData> newOptions, string title = "")
        {
            options = newOptions;
            currentIndex = 0;
            
            if (titleText != null && !string.IsNullOrEmpty(title))
            {
                titleText.text = title;
            }
            
            UpdateDisplay();
        }
        
        public void NavigateNext()
        {
            if (options.Count == 0) return;
            
            currentIndex = (currentIndex + 1) % options.Count;
            UpdateDisplay();
        }
        
        public void NavigatePrevious()
        {
            if (options.Count == 0) return;
            
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = options.Count - 1;
            }
            UpdateDisplay();
        }
        
        public void SetIndex(int index)
        {
            if (options.Count == 0) return;
            
            currentIndex = Mathf.Clamp(index, 0, options.Count - 1);
            UpdateDisplay();
        }
        
        public OptionData GetCurrentOption()
        {
            if (options.Count == 0) return null;
            return options[currentIndex];
        }
        
        public int GetCurrentIndex()
        {
            return currentIndex;
        }
        
        private void UpdateDisplay()
        {
            if (options.Count == 0)
            {
                if (currentOptionNameText != null) currentOptionNameText.text = "No options available";
                if (currentOptionDescriptionText != null) currentOptionDescriptionText.text = "";
                if (indexText != null) indexText.text = "0 / 0";
                return;
            }
            
            OptionData current = options[currentIndex];
            
            // Update name
            if (currentOptionNameText != null)
            {
                currentOptionNameText.text = current.name;
            }
            
            // Update description
            if (currentOptionDescriptionText != null)
            {
                currentOptionDescriptionText.text = current.GetShortDescription();
            }
            
            // Update index counter
            if (indexText != null)
            {
                indexText.text = $"{currentIndex + 1} / {options.Count}";
            }
            
            // Update preview image if available
            if (previewImage != null && current.previewSprite != null)
            {
                previewImage.sprite = current.previewSprite;
                previewImage.gameObject.SetActive(true);
            }
            else if (previewImage != null)
            {
                previewImage.gameObject.SetActive(false);
            }
            
            // Update arrows
            UpdateArrows();
        }
        
        private void UpdateArrows()
        {
            if (options.Count <= 1)
            {
                // Hide arrows if only one option
                if (leftArrow != null) leftArrow.SetActive(false);
                if (rightArrow != null) rightArrow.SetActive(false);
                return;
            }
            
            // Show arrows
            if (leftArrow != null)
            {
                leftArrow.SetActive(true);
                var image = leftArrow.GetComponent<Image>();
                if (image != null)
                {
                    image.color = currentIndex > 0 ? activeArrowColor : inactiveArrowColor;
                }
            }
            
            if (rightArrow != null)
            {
                rightArrow.SetActive(true);
                var image = rightArrow.GetComponent<Image>();
                if (image != null)
                {
                    image.color = currentIndex < options.Count - 1 ? activeArrowColor : inactiveArrowColor;
                }
            }
        }
    }
    
    [System.Serializable]
    public class OptionData
    {
        public string name;
        public string fullPrompt;
        public Sprite previewSprite; // Optional
        
        public OptionData(string name, string prompt, Sprite preview = null)
        {
            this.name = name;
            this.fullPrompt = prompt;
            this.previewSprite = preview;
        }
        
        public string GetShortDescription()
        {
            // Extract a short description from the full prompt
            if (string.IsNullOrEmpty(fullPrompt)) return "";
            
            // Take first part before comma, or truncate if too long
            string[] parts = fullPrompt.Split(',');
            string firstPart = parts[0];
            
            if (firstPart.Length > 80)
            {
                return firstPart.Substring(0, 77) + "...";
            }
            
            return firstPart;
        }
    }
}

using UnityEngine;

namespace QuestCameraKit.WebRTC
{
    /// <summary>
    /// Defines a menu option type for the Quest app
    /// </summary>
    public enum MenuOptionType
    {
        TimeTravel,
        VirtualTryOn,
        BiomeTransformation,
        VideoGameStyle,
        CustomPrompt
    }

    /// <summary>
    /// Represents a menu option with its display name and type
    /// </summary>
    [System.Serializable]
    public class MenuOption
    {
        public MenuOptionType Type;
        public string DisplayName;
        public string Description;
        public Sprite Icon;

        public MenuOption(MenuOptionType type, string displayName, string description)
        {
            Type = type;
            DisplayName = displayName;
            Description = description;
        }
    }
}

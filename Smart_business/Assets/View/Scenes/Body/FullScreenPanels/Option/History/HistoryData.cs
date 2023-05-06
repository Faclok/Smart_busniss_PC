
using UnityEngine;

namespace Assets.View.Body.FullScreen.OptionsWindow.History
{
    public class HistoryData
    {
        public readonly Sprite Icon;

        public readonly string Title;

        public readonly string Description;

        public HistoryData(Sprite icon, string title, string description)
        {
            Icon = icon;
            Title = title;
            Description = description;
        }
    }
}

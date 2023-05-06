using Assets.View.Body.FullScreen.EditWindow;
using Assets.View.Body.FullScreen.OptionsWindow.History;
using Assets.View.Body.FullScreen.OptionsWindow.Review;
using System;
using System.Threading.Tasks;

namespace Assets.View.Body.FullScreen.OptionsWindow
{
    public class OptionProperty
    {
        public readonly string Name;
        public readonly string DescriptionFull;
        public readonly ReviewProperty ReviewProperty;
        public readonly Func<DateTime,DateTime,Task<HistoryData[]>> ValuesHistory;

        public readonly EditProperty EditProperty;

        public OptionProperty(string name, EditProperty property, ReviewProperty reviewProperty,string description, Func<DateTime,DateTime,Task<HistoryData[]>> valueHistory) 
        {
            Name = name;
            EditProperty = property;
            ReviewProperty = reviewProperty;
            DescriptionFull = description;
            ValuesHistory = valueHistory;
        }
    }
}

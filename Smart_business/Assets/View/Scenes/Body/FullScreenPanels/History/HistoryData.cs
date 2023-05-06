using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.View.Body.FullScreen.HistoryWindow
{
    public class HistoryData
    {

        public readonly string Title;

        public readonly string Description;

        public readonly Sprite Icon;

        public HistoryData(string title, string description, Sprite icon)
        {
            Title = title;
            Description = description;
            Icon = icon;
        }
    }
}

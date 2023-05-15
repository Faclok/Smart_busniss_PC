using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.View.Body.FullScreen.OptionsWindow.Review
{
    public class ReviewProperty
    {
        public readonly Sprite IconDate;

        public readonly Func<DateTime, DateTime,Task<float[]>> FuncLoadGraphic;
        public readonly Func<Task<(string LastActive, string TimeLastActive)>> FuncLastActive; 

        public ReviewProperty(Sprite icon, Func<DateTime, DateTime,Task<float[]>> funcLoadGraphic, Func<Task<(string, string)>> funcLastActive)
        {
            IconDate = icon;
            FuncLoadGraphic = funcLoadGraphic;
            FuncLastActive = funcLastActive;
        }
    }
}

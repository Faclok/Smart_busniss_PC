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
        public readonly string ValueLastActive;
        public readonly string TimeLastActive;

        public readonly Sprite IconDate;

        public readonly Func<DateTime, DateTime,Task< float[]>> FuncLoadGraphic;

        public ReviewProperty(Sprite icon, Func<DateTime, DateTime,Task<float[]>> funcLoadGraphic, string valueLastActive, string timeLastActive)
        {
            IconDate = icon;
            FuncLoadGraphic = funcLoadGraphic;
            ValueLastActive = valueLastActive;
            TimeLastActive = timeLastActive;
        }
    }
}

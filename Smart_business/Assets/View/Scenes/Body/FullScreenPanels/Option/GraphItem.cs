using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.View.Body.FullScreen.OptionsWindow
{
    public class GraphItem
    {
        public readonly DateTime Active;
        public readonly float Precent;

        public GraphItem(DateTime active, float precent)
        {
            Active = active;
            Precent = precent;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.View.Body.FullScreen.OptionsWindow
{
    public class FuncOptionProperty
    {
        public readonly Func<DateTime, DateTime, Task<GraphItem[]>> Graph;
        public readonly Func<GraphItem[], string, string> AIHelper;

        public FuncOptionProperty(Func<DateTime, DateTime, Task<GraphItem[]>> graph,Func<GraphItem[], string, string> aiHelper)
        {
            Graph= graph;
            AIHelper = aiHelper;
        }
    }
}

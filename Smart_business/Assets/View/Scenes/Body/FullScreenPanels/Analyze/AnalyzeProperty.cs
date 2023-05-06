using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.View.Body.FullScreen.AnalyzeWindow
{
    public class AnalyzeProperty
    {

        public readonly string Name;
        public readonly string NameItems;
        public readonly Func<DateTime, DateTime, Task<ItemData[]>> FuncLoad;

        public AnalyzeProperty(string name, string nameItems, Func<DateTime, DateTime, Task<ItemData[]>> funcLoad)
        {
            Name = name;
            NameItems = nameItems;
            FuncLoad = funcLoad;
        }
    }
}

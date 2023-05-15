using Assets.View.Body.FullScreen.OptionsWindow.History;
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
        public readonly Func<DateTime, DateTime, Task<PackData>> FuncLoad;

        public AnalyzeProperty(string name, string nameItems, Func<DateTime, DateTime, Task<PackData>> funcLoad)
        {
            Name = name;
            NameItems = nameItems;
            FuncLoad = funcLoad;
        }
    }

    public class PackData
    {
        public readonly ItemData[] Items;
        public readonly HistoryData[] Histories;

        public PackData(ItemData[] items, HistoryData[] histories)
        {
            Items = items;
            Histories = histories;
        }
    }
}

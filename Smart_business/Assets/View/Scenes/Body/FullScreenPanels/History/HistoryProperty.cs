using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.View.Body.FullScreen.HistoryWindow
{
    public class HistoryProperty
    {

        public readonly string Title;

        public readonly Func<Task<HistoryData[]>> LoadHistory;

        public HistoryProperty(string title, Func<Task<HistoryData[]>> loadHistory)
        {
            Title = title;
            LoadHistory = loadHistory;
        }
    }
}

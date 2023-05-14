using Assets.MultiSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.View.Body.FullScreen.Fields;

namespace Assets.View.Body.FullScreen.EditWindow
{
    public class EditProperty
    {
        public readonly IItemDatabase Item;
        public readonly Action UpdateOnChanger;
        public readonly bool IsDelete;
        public string Question => _funcQuestion();

        private Func<string> _funcQuestion;

        public readonly ElementData[] Elements;

        public EditProperty(IItemDatabase item, ElementData[] datas, Action updateOnChanger, bool isDelete, Func<string> funcQuestion)
        {
            Item = item;
            Elements = datas;
            IsDelete = isDelete;
            UpdateOnChanger = updateOnChanger;
            _funcQuestion = funcQuestion;
        }
    }
}

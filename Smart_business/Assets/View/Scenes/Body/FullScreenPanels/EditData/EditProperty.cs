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

        public readonly ElementData[] Elements;

        public EditProperty(IItemDatabase item, ElementData[] datas, Action updateOnChanger, bool isDelete)
        {
            Item = item;
            Elements = datas;
            IsDelete = isDelete;
            UpdateOnChanger = updateOnChanger;
        }
    }
}

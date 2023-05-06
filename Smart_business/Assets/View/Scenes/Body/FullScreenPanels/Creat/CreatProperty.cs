using Assets.MultiSetting;
using Assets.View.Body.FullScreen.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.View.Body.FullScreen.CreatWindow
{
    public class CreatProperty
    {
        public readonly IItemDatabase ItemCreat;
        public readonly ElementData[] ElementDatas;
        public readonly Action UpdateOnChanger;

        public CreatProperty(IItemDatabase itemCreat ,ElementData[] elementDatas, Action updateOnChanger)
        {
            ItemCreat = itemCreat;
            ElementDatas = elementDatas;
            UpdateOnChanger = updateOnChanger;
        }
    }
}

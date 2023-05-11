using UnityEngine.UI;
using UnityEngine;
using Assets.MultiSetting;
using System.Linq;

namespace Assets.View.Body.FullScreen.Fields
{
    public class ControllField : MonoBehaviour
    {

        [Header("Instantiate")]
        [SerializeField]
        private Transform _content;

        [SerializeField]
        private TextFieldBehavior _prefab;

        private TextFieldBehavior[] _behaviors = new TextFieldBehavior[0];
        private ElementData[] _datas;

        public void UpdateData(ElementData[] datas)
        {
            _datas = datas;
            InstantiateElements(datas);
        }

        public ElementData[] SaveProperty()
            => _datas;

        public void Replace()
        {
            for (int i = 0; i < _behaviors.Length; i++)
                _behaviors[i].Replace();
        }

        private void InstantiateElements(ElementData[] datas)
        {
            var newDatas = InstantiateExtensions.GetOverwriteInstantiate(_prefab,_content, _behaviors,datas);

            for (int i = 0; i < newDatas.Length; i++)
                newDatas[i].UpdateData(datas[i]);

            _behaviors = newDatas;
        }
    }
}

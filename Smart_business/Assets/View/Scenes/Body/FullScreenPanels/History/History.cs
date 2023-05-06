using Assets.MultiSetting;
using Assets.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.HistoryWindow
{

    public class History : FullScreenPanel<HistoryProperty>
    {
        [Header("Instantiate")]
        [SerializeField]
        private HistoryBehaviour _prefab;

        [SerializeField]
        private Transform _content;

        [Header("UI")]
        [SerializeField]
        private Text _title;

        [HideInInspector]
        public new GameObject gameObject;

        private HistoryBehaviour[] _instantiateArray = new HistoryBehaviour[0];

        private void Awake()
        {
            gameObject = base.gameObject;
        }

        public override  void OpenWindow()
        {
            _title.text = _property.Title;
            _property.LoadHistory().GetTaskCompleted(UpdateData);
        }

        private void UpdateData(HistoryData[] datas)
        {
            var newArray = InstantiateExtensions.GetOverwriteInstantiate(_prefab,_content, _instantiateArray, datas);

            for (int i = 0; i < newArray.Length; i++)
                newArray[i].UpdateData(datas[i].Title, datas[i].Description, datas[i].Icon);

            _instantiateArray = newArray;
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}

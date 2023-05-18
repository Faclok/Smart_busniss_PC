using Assets.View;
using Assets.View.Body.Menu;
using Assets.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProductData = Assets.ViewModel.Datas.Product;
using System.Threading.Tasks;
using Assets.MultiSetting;

namespace Assets.View.Body.Analyst
{

    public class AnalystChanger : PanelContent
    {

        [Header("Prefab item")]
        [SerializeField]
        private AnalystItemGraphic _prefab;

        [Header("Content")]
        [SerializeField]
        private Transform _content;

        [SerializeField]
        private GameObject _contentBody;

        [SerializeField]
        private AnalystControllChangerPrice _controllPrice;

        private AnalystItemGraphic[] _items = new AnalystItemGraphic[0];

        private void Start()
        {
            OnPanelOpen += UpdateOpen;
            OnPanelClose += UpdateClose;
            _controllPrice.Replace();
            _controllPrice.UpdateData(UpdateItems);
        }

        private void UpdateClose()
        {
            _contentBody.SetActive(false);
        }

        private  void UpdateOpen()
        {
            _contentBody.SetActive(true);
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<ProductData>(ProductData.TABLE); }).GetTaskCompleted(UpdateItems);
        }

        private void UpdateItems()
        {
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<ProductData>(ProductData.TABLE); }).GetTaskCompleted(UpdateItems);
        }

        private void UpdateItems(ProductData[] data)
        {
            var newItems = InstantiateExtensions.GetOverwriteInstantiate(_prefab, _content, _items, data);

            for (int i = 0; i < data.Length; i++)
                newItems[i].UpdateData(data[i], _controllPrice);

            _items = newItems;
        }

        private void OnDestroy()
        {
            OnPanelOpen -= UpdateOpen;
            OnPanelClose -= UpdateClose;
        }
    }
}
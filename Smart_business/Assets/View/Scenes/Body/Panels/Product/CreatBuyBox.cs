using Assets.View.Body.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Assets.ViewModel;
using ProductData = Assets.ViewModel.Datas.Product;
using Assets.MultiSetting;
using Assets.View.Body.FullScreen.Fields;
using System;
using Assets.ViewModel.PullDatas;
using Assets.View.Body.FullScreen.MessageTask;
using System.Linq;

namespace Assets.View.Body.Product
{

    public class CreatBuyBox : PanelContent
    {

        [Header("Prefab")]
        [SerializeField]
        private ProductBehaviourShop _prefabBox;

        [Header("Content")]
        [SerializeField]
        private Transform _content;

        [Header("Link")]
        [SerializeField]
        private ControllField _fields;

        private BuyHistoryPull _buyShop;

        private ProductBehaviourShop[] _productBoxs = new ProductBehaviourShop[0];

        private string PriceBox => _productBoxs.Where(o => o.Count > 0).Select(o => decimal.Parse(o.ProductData.Price) * o.Count).Sum().ToString();

        private void Start()
        {
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<ProductData>(ProductData.TABLE); }).GetTaskCompleted(OnDatasLoad);

            var newMachine = _buyShop = new BuyHistoryPull();

            newMachine["id"] = "auto-ganerate";
            newMachine["idClient"] = "1"; //FIX
            newMachine["readingTime"] = $"{DateTime.Now:yyyy.MM.dd}";
            newMachine["state"] = "cancel";
            newMachine["priceConst"] = "0";

            var elements = new ElementData[]
            {
                new ElementData("ID","id",newMachine["id"], isEdit: false, countSimbols: 7, isNumber:true),
                new ElementData("Client", "idClient", "-1", true, 7, true),
                new ElementData("readingTime","readingTime",newMachine["readingTime"],isEdit:false,countSimbols: 1000, isNumber:false),
                new ElementData("State", "state", "cancel", true, 7, false),
            };

            _fields.UpdateData(elements);

            OnPanelOpen += UpdateOpen;
        }

        private void UpdateOpen()
        {
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<ProductData>(ProductData.TABLE); }).GetTaskCompleted(OnDatasLoad);
        }

        public void Create()
        {
            MessageView.ShowTask($"create shop on price = '{PriceBox}'?", CreateServer, Replace);
        }

        public void Replace()
        {
            _fields.Replace();

            foreach (var item in _productBoxs)
                item.Replace();
        }

        private async Task CreateServer()
        {
            var updateColumns = _buyShop.Columns;
            var updateLocal = _fields.SaveProperty();

            foreach (var item in updateLocal)
                updateColumns[item.Key] = item.Value;

            _buyShop.Columns["id"] = null;
            _buyShop["priceConst"] = PriceBox;
            _buyShop["idProducts"] = "$" + string.Join('$', _productBoxs.Where(o => o.Count > 0).Select(o => $"{o.ProductData["id"]}")) + "$";

            await Task.Run(() => ModelDatabase.CreatObject(_buyShop));
        }

        private void OnDatasLoad(ProductData[] products)
        {
            var productBehaviours = InstantiateExtensions.GetOverwriteInstantiate(_prefabBox, _content, _productBoxs, products);

            for (int i = 0; i < products.Length; i++)
                productBehaviours[i].UpdateData(products[i]);

            _productBoxs = productBehaviours;
        }

        private void OnDestroy()
        {
            OnPanelOpen -= UpdateOpen;
        }
    }
}
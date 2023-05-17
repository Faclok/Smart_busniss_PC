using System.Collections.Generic;
using UnityEngine;
using Assets.View.Body.Menu;
using Assets.ViewModel;
using ProductData = Assets.ViewModel.Datas.Product;
using Assets.View.Body.FullScreen.AnalyzeWindow;
using System;
using System.Threading.Tasks;
using Assets.ViewModel.PullDatas;
using System.Linq;
using Assets.View.Body.FullScreen.OptionsWindow;
using TMPro;
using Assets.View.Body.FullScreen.OptionsWindow.History;

namespace Assets.View.Body.Analyst
{

    public class AnalystControll : PanelContent
    {
        [Header("Text Head")]
        [SerializeField]
        private TextMeshProUGUI _titleText;

        [SerializeField]
        private TextMeshProUGUI _descriptionText;

        /// <summary>
        /// Иконки машин
        /// </summary>
        [Header("Pack UI")]
        [SerializeField]
        private List<KeyValueIcon> _icons;

        /// <summary>
        /// Дефолтная иконка, в случае отсутвия нужной
        /// </summary>
        [Space(15)]
        [SerializeField]
        private Sprite _defaultIcon;

        [Header("Panels")]
        [SerializeField]
        private Option _option;

        /// <summary>
        /// Контроль машин в главном меню
        /// </summary>
        [Header("Items")]
        [SerializeField]
        private VerticalAnalyst _verticalProduct;

        [Header("Edit Body")]
        [SerializeField]
        private GameObject _editBody;

        /// <summary>
        /// Имя используется в анализе
        /// </summary>
        public const string NameDepartment = "Курс";

        /// <summary>
        /// Имя используется в анализе
        /// </summary>
        public const string NameItemDepartment = "Цены";

        public static readonly AnalyzeProperty AnalyzeProperty = new(NameDepartment, NameItemDepartment, GetItemDatas);

        /// <summary>
        /// Паттерн Singleton
        /// </summary>
        private static AnalystControll _singleton;

        /// <summary>
        /// Пробуждение
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            _singleton = this;
        }

        private void Start()
        {
            OnPanelOpen += UpdateOpen;
            OnPanelClose += UpdateClose;
        }

        private void UpdateClose()
        {
            _editBody.SetActive(false);
        }

        private void UpdateOpen()
        {
            _verticalProduct.UpdateDatasOnChanger();
        }

        public static void FocusProduct(AnalystBehaviour product, OptionProperty option)
        {
            _singleton._titleText.text = product.Data.Name;
            _singleton._descriptionText.text = product.Data["dataSet"].Remove(product.Data["dataSet"].Length - 7);
            _singleton._option.Open(option);

            _singleton._option.FirstStart();
        }

        public static void UpdateDatasOnChangers()
            => _singleton._verticalProduct.UpdateDatasOnChanger();

        /// <summary>
        /// Загрузка данных о периоде машин
        /// </summary>
        /// <param name="start">С какого времени учитывать</param>
        /// <param name="end">По какое время учитывать</param>
        /// <returns>Список записей</returns>
        private static async Task<PackData> GetItemDatas(DateTime start, DateTime end)
        {
            var data = await Task.Run(() =>
            {
                return ModelDatabase.GetPullObjectAsync<PriceChangePull>(PriceChangePull.TABLE, PriceChangePull.COLUMN_DATE, start, end);
            });

            var products = _singleton._verticalProduct.ProductDatas;

            var productPercent = new Dictionary<ProductData, decimal>(products.Length);
            var productDictionary = new Dictionary<string, ProductData>(products.Length);

            decimal sumCount = 0;
            for (int i = 0; i < products.Length; i++)
            {
                decimal count = 0;
                for (int q = 0; q < data.Length; q++)
                {
                    if (data[q][PriceChangePull.COLUMN_LINK] == products[i]["id"])
                        count += decimal.Parse(data[q][PriceChangePull.COLUMN_LINK]);
                }

                sumCount += count;
                productPercent.Add(products[i], count);
                productDictionary.Add(products[i]["id"], products[i]);
            }

            var returnArray = new ItemData[productPercent.Count];
            int index = 0;

            foreach (var item in productPercent)
            {
                if (item.Value != 0m && sumCount != 0)
                    returnArray[index] = new ItemData(item.Key.Name, (float)(item.Value / sumCount * 100m));
                else returnArray[index] = new ItemData(item.Key.Name, 0f);

                index++;
            }

            var history = new HistoryData[data.Length];

            var productKey = string.Empty;

            history = data.Select(o =>
             new HistoryData
             (
                 GetIcon(o.Columns["state"]),
                 o.PriceChanger.ToString(),
                 $"{DateTime.Parse(o.Columns[BuyHistoryPull.COLUMN_DATE]):HH:mm dd.MM.yy} - product id ({o.Link})"
                  )
             ).ToArray();

            return new PackData(returnArray, history);
        }

        /// <summary>
        /// Получить иконку в соответсвии машине
        /// </summary>
        /// <param name="name">Имя машины</param>
        /// <returns></returns>
        public static Sprite GetIcon(string name)
        {
            foreach (var item in _singleton._icons)
                if (item.Name == name)
                    return item.Icon;

            return _singleton._defaultIcon;
        }

        public static bool IsRoot(string root)
            => ManagementAssistant.AccessAccount["Продукт"].Contains("all") || ManagementAssistant.AccessAccount["Продукт"].Contains(root);


        private void OnDestroy()
        {
            OnPanelOpen -= UpdateOpen;
            OnPanelClose -= UpdateClose;
        }
    }
}

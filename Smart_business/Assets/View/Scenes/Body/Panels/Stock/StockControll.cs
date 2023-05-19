using System.Collections.Generic;
using UnityEngine;
using Assets.View.Body.Menu;
using Assets.ViewModel;
using ObjectStock = Assets.ViewModel.Datas.ObjectInStock;
using Assets.View.Body.FullScreen.AnalyzeWindow;
using System;
using System.Threading.Tasks;
using Assets.ViewModel.PullDatas;
using System.Linq;
using Assets.View.Body.FullScreen.OptionsWindow;
using TMPro;
using Assets.View.Body.FullScreen.OptionsWindow.History;

namespace Assets.View.Body.Stock
{

    /// <summary>
    /// Центр по управлению машин
    /// </summary>

    public class StockControll : PanelContent
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
        private VerticalStock _verticalStock;

        [Header("Edit Body")]
        [SerializeField]
        private GameObject _editBody;

        /// <summary>
        /// Имя используется в анализе
        /// </summary>
        public const string NameDepartment = "Архив";

        /// <summary>
        /// Имя используется в анализе
        /// </summary>
        public const string NameItemDepartment = "Элемененты";


        public static readonly AnalyzeProperty AnalyzeProperty = new(NameDepartment, NameItemDepartment, GetItemDatas);

        /// <summary>
        /// Паттерн Singleton
        /// </summary>
        private static StockControll _singleton;

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
            _verticalStock.UpdateDatasOnChanger();
        }

        public static void FocusStock(StockBehaviour stock, OptionProperty option)
        {
            _singleton._titleText.text = stock.Data.Name;
            _singleton._descriptionText.text = stock.Data["dataSet"].Remove(stock.Data["dataSet"].Length - 7);
            _singleton._option.Open(option);

            _singleton._option.FirstStart();
        }

        public static void UpdateDatasOnChangers()
            => _singleton._verticalStock.UpdateDatasOnChanger();

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
                return ModelDatabase.GetPullObjectAsync<StockPull>(StockPull.TABLE, StockPull.COLUMN_DATE, start, end);
            });

            var machines = _singleton._verticalStock.StockDatas;
            var machinesPercent = new Dictionary<ObjectStock, uint>(machines.Length);
            var machineDictionary = new Dictionary<string, ObjectStock>(machines.Length);

            uint sumCount = 0;
            for (int i = 0; i < machines.Length; i++)
            {
                uint count = 0;
                for (int q = 0; q < data.Length; q++)
                    if (machines[i]["id"] == data[q].Columns[data[q].ColumnLink])
                        count += (uint)data[q].Value;

                sumCount += count;
                machinesPercent.Add(machines[i], count);
                machineDictionary.Add(machines[i]["id"], machines[i]);
            }

            var returnArray = new ItemData[machinesPercent.Count];
            int index = 0;

            foreach (var item in machinesPercent)
            {
                if (item.Value != 0f && sumCount != 0)
                    returnArray[index] = new ItemData(item.Key.Name, (item.Value / (float)sumCount) * 100f);
                else returnArray[index] = new ItemData(item.Key.Name, 0f);

                index++;
            }

            var history = new HistoryData[data.Length];

            history = data.Select(o =>
             new HistoryData
             (
                 GetIcon(o.Columns["state"]),
                 (machineDictionary.ContainsKey(o.Columns[o.ColumnLink]) ? machineDictionary[o.Columns[o.ColumnLink]].Name : "not found") + ": " + o.Value,
                 $"{DateTime.Parse(o.Columns["dateSet"]):HH:mm dd.MM.yy}"
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
            => ManagementAssistant.AccessAccount["Склад"].Contains("all") || ManagementAssistant.AccessAccount["Склад"].Contains(root);


        private void OnDestroy()
        {
            OnPanelOpen -= UpdateOpen;
            OnPanelClose -= UpdateClose;
        }
    }
}

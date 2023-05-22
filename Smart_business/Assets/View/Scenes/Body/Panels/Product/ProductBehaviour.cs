using System.Collections;
using UnityEngine;
using ProductData = Assets.ViewModel.Datas.Product;
using UnityEngine.UI;
using System;
using Assets.View.Body.FullScreen;
using Assets.View.Body.FullScreen.OptionsWindow;
using Assets.View.Body.FullScreen.EditWindow;
using Assets.View.Body.FullScreen.Fields;
using Assets.View.Body.FullScreen.OptionsWindow.Review;
using System.Threading.Tasks;
using Assets.View.Body.FullScreen.OptionsWindow.History;
using Assets.ViewModel;
using Assets.ViewModel.PullDatas;
using System.Linq;
using System.Collections.Generic;

namespace Assets.View.Body.Product
{

    /// <summary>
    /// Машина в панели
    /// </summary>
    public class ProductBehaviour : MonoBehaviour
    {

        /// <summary>
        /// Поле для имени машины
        /// </summary>
        [SerializeField] private Text _title;

        /// <summary>
        /// Поле для информации машины
        /// </summary>
        [SerializeField] private Text _info;

        /// <summary>
        /// Объект текущего тела
        /// </summary>
        [HideInInspector] public new GameObject gameObject;

        /// <summary>
        /// Тело текушего объекта
        /// </summary>
        [HideInInspector] public new Transform transform;

        /// <summary>
        /// Данные
        /// </summary>
        public ProductData Data { get; private set; }

        /// <summary>
        /// Пробуждение
        /// </summary>
        private void Awake()
        {
            gameObject = base.gameObject;
            transform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        /// <param name="data"></param>
        public void UpdateData(ProductData data)
        {
            Data = data;

            _title.text = data.Name;
            _info.text = $"{data.Price} ₽";
        }

        /// <summary>
        /// Нажатие на нее
        /// </summary>
        public void Click()
        {
            var datas = new ElementData[] { new ElementData("ID","id",Data["id"],false,int.MaxValue.ToString().Length,true),
                                            new ElementData("Имя", "name",Data["name"], true,20),
                                            new ElementData("Создан", "dataSet", Data.CreatMachineSQL, false,15),
                                            new ElementData("Описание","description",Data["description"],true, int.MaxValue)};

            var edit = new EditProperty(Data, datas, ProductControll.UpdateDatasOnChangers, ProductControll.IsRoot("delete"), () => $"edit '{datas[1].Value}'?");
            var option = new OptionProperty(Data.Name, ProductControll.IsRoot("edit") ? edit : null, new ReviewProperty(ProductControll.GetIcon("LastActive"), FuncLoadGraphicAsync, GetLastActive), Data["description"], GetHistoryAsync);

            ProductControll.FocusProduct(this, option);
        }

        private async Task<float[]> FuncLoadGraphicAsync(DateTime start, DateTime end)
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<BuyHistoryPull>(BuyHistoryPull.TABLE, "idProducts", Data, BuyHistoryPull.COLUMN_DATE, start, end, true));

            var values = data.Select(o => float.Parse(o.Columns["priceConst"])).ToArray();
            var dates = DateTimeCalculate.GetColumns(start, end);

            var list = new Dictionary<DateTimeCalculate.Range, List<double>>();

            for (int i = 0; i < dates.Length; i++)
                for (int q = 0; q < data.Length; q++)
                {
                    if (dates[i].Start >= start && dates[i].End <= end)
                        if (list.ContainsKey(dates[i]))
                            list[dates[i]].Add(float.Parse(data[q].Columns["priceConst"]));
                        else list.Add(dates[i], new List<double>() { float.Parse(data[q].Columns["priceConst"]) });
                }

            return DiagrammUtility.GetColumns(list.Values.Select(o => o.ToArray()).ToArray());
        }

        private async Task<(string LastActive, string TimeLastActive)> GetLastActive()
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<BuyHistoryPull>(BuyHistoryPull.TABLE, "idProducts", Data, BuyHistoryPull.COLUMN_DATE, DateTime.MinValue, DateTime.Now, true));

            if (data.Length <= 0)
                return ("no history", "no time");

            return (data[^1].Price, $"{DateTime.Parse(data[^1].Columns["readingTime"]):HH:mm dd.MM.yy}");
        }

        private async Task<HistoryData[]> GetHistoryAsync(DateTime start, DateTime end)
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<BuyHistoryPull>(BuyHistoryPull.TABLE, "idProducts", Data, BuyHistoryPull.COLUMN_DATE, start, end, true));
            var result = data.Select(o =>
            new HistoryData
            (
                ProductControll.GetIcon(o.Columns["state"]),
                o.Price,
                $"{DateTime.Parse(o.Columns[BuyHistoryPull.COLUMN_DATE]):HH:mm dd.MM.yy} - user id ({o.Link}")
            ).ToArray();

            return result;
        }

        /// <summary>
        /// Удаление ее
        /// </summary>
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
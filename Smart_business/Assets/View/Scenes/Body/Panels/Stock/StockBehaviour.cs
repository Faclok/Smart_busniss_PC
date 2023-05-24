using System.Collections;
using UnityEngine;
using ObjectStock = Assets.ViewModel.Datas.ObjectInStock;
using UnityEngine.UI;
using System;
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

namespace Assets.View.Body.Stock
{

    /// <summary>
    /// Машина в панели
    /// </summary>
    public class StockBehaviour : MonoBehaviour
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
        public ObjectStock Data { get; private set; }

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
        public void UpdateData(ObjectStock data)
        {
            Data = data;

            _title.text = data.Name;
            _info.text = $"{data.Amount}";
        }

        /// <summary>
        /// Нажатие на нее
        /// </summary>
        public void Click()
        {
            var datas = new ElementData[] { new ElementData("ID","id",Data["id"],false,int.MaxValue.ToString().Length,true),
                                            new ElementData("Name", "name",Data["name"], true,20),
                                            new ElementData("Creat", StockPull.COLUMN_DATE, Data.CreatMachineSQL, false,15),
                                            new ElementData("Icon", "icon", Data["icon"], true, 60),
                                            new ElementData("Описание","description",Data["description"],true, int.MaxValue)};

            var edit = new EditProperty(Data, datas, StockControll.UpdateDatasOnChangers, StockControll.IsRoot("delete"), () => $"edit '{datas[1].Value}'?");
            var option = new OptionProperty(Data.Name, StockControll.IsRoot("edit") ? edit : null, new ReviewProperty(StockControll.GetIcon("LastActive"), FuncLoadGraphicAsync, GetLastActive), Data["description"], GetHistoryAsync);

            StockControll.FocusStock(this, option);
        }

        private async Task<float[]> FuncLoadGraphicAsync(DateTime start, DateTime end)
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<StockPull>(StockPull.TABLE, StockPull.COLUMN_LINK, Data, StockPull.COLUMN_DATE, start, end));

            var dates = DateTimeCalculate.GetColumns(start, end);

            var list = new Dictionary<DateTimeCalculate.Range, List<double>>();

            for (int i = 0; i < dates.Length; i++)
                list.Add(dates[i], new List<double>() { 0.01d });

            for (int q = 0; q < data.Length; q++)
                for (int i = 0; i < dates.Length; i++)
                    if (dates[i].Start <= data[q].ReadingTime)
                        list[dates[i]].Add(data[q].Value);

            return DiagrammUtility.GetColumns(list.Values.Select(o => o.ToArray()).ToArray());
        }

        private async Task<(string LastActive, string TimeLastActive)> GetLastActive()
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<StockPull>(StockPull.TABLE, StockPull.COLUMN_LINK, Data, StockPull.COLUMN_DATE, DateTime.MinValue, DateTime.Now));

            if (data.Length <= 0)
                return ("no history", "no time");

            return (data[^1].Value.ToString(), $"{DateTime.Parse(data[^1].Columns[StockPull.COLUMN_DATE]):HH:mm dd.MM.yy}");
        }

        private async Task<HistoryData[]> GetHistoryAsync(DateTime start, DateTime end)
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<StockPull>(StockPull.TABLE, StockPull.COLUMN_LINK, Data, StockPull.COLUMN_DATE, start, end));

            var result = data.Select(o =>
            new HistoryData
            (
                StockControll.GetIcon(o.Columns["state"]),
                o.Value.ToString(),
                $"{DateTime.Parse(o.Columns[StockPull.COLUMN_DATE]):HH:mm dd.MM.yy}")
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
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
                                            new ElementData("Creat", "dataSet", Data.CreatMachineSQL, false,15),
                                            new ElementData("Icon", "icon", Data["icon"], true, 60),
                                            new ElementData("Описание","description",Data["description"],true, int.MaxValue)};

            var edit = new EditProperty(Data, datas, StockControll.UpdateDatasOnChangers, StockControll.IsRoot("delete"), () => $"edit '{datas[1].Value}'?");
            var option = new OptionProperty(Data.Name, StockControll.IsRoot("edit") ? edit : null, new ReviewProperty(StockControll.GetIcon("LastActive"), FuncLoadGraphicAsync, GetLastActive), Data["description"], GetHistoryAsync);

            StockControll.FocusStock(this, option);
        }

        private async Task<float[]> FuncLoadGraphicAsync(DateTime start, DateTime end)
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<StockPull>(StockPull.TABLE, StockPull.COLUMN_LINK, Data, StockPull.COLUMN_DATE, start, end));

            var values = data.Select(o => (float)o.Value).ToArray();
            var maxValue = values.Length > 0 ? values.Max<float>() : 0f;
            var result = new float[data.Length];

            for (int i = 0; i < data.Length; i++)
                result[i] = values[i] / maxValue;

            return result;
        }

        private async Task<(string LastActive, string TimeLastActive)> GetLastActive()
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<StockPull>(StockPull.TABLE, StockPull.COLUMN_LINK, Data, StockPull.COLUMN_DATE, DateTime.MinValue, DateTime.Now));

            if (data.Length <= 0)
                return ("no history", "no time");

            return (data[^1].Value.ToString(), $"{DateTime.Parse(data[^1].Columns["dateSet"]):HH:mm dd.MM.yy}");
        }

        private async Task<HistoryData[]> GetHistoryAsync(DateTime start, DateTime end)
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<StockPull>(StockPull.TABLE, StockPull.COLUMN_LINK, Data, StockPull.COLUMN_DATE, start, end));

            var result = data.Select(o =>
            new HistoryData
            (
                StockControll.GetIcon(o.Columns["state"]),
                o.Value.ToString(),
                $"{DateTime.Parse(o.Columns["dateSet"]):HH:mm dd.MM.yy}")
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
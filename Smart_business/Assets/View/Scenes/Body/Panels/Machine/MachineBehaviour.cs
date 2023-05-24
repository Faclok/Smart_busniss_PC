using System.Collections;
using UnityEngine;
using MachineData = Assets.ViewModel.Datas.Machine;
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

namespace Assets.View.Body.Machine
{

    /// <summary>
    /// Машина в панели
    /// </summary>
    public class MachineBehaviour : MonoBehaviour
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
        public MachineData Data { get; private set; }

        /// <summary>
        /// Если машина выключена
        /// </summary>
        public const string Inactive = "отключен";

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
        public void UpdateData(MachineData data)
        {
            Data = data;

            _title.text = data.Name;

            if (!data.IsActive)
                _info.text = $"{Inactive}";
            else
            {
                var timeSpan = DateTime.Now.Subtract(data.StartJob);
                _info.text = $"{Math.Round(timeSpan.TotalHours)} ч. {timeSpan.Minutes} мин.";
            }
            _info.text += $" ({data.CountHours} ч.)";
        }

        /// <summary>
        /// Нажатие на нее
        /// </summary>
        public void Click()
        {
            var datas = new ElementData[] { new ElementData("ID","id",Data["id"],false,int.MaxValue.ToString().Length,true), new ElementData("Name", "name",Data["name"], true,20),
                                            new ElementData("Создан", "dataSet", Data.CreatMachineSQL, false,15),new ElementData("Icon", "icon", Data["icon"], true, 60),
                                            new ElementData("Описание","description",Data["description"],true, int.MaxValue)};

            var edit = new EditProperty(Data, datas, MachineControll.UpdateDatasOnChangers, MachineControll.IsRoot("delete"), () => $"edit '{datas[1].Value}'?");
            var option = new OptionProperty(Data.Name,MachineControll.IsRoot("edit") ? edit : null, new ReviewProperty(MachineControll.GetIcon("LastActive"), FuncLoadGraphicAsync, GetLastActive), Data["description"], GetHistoryAsync);
          
            MachineControll.FocusMachine(this ,option);
        }

        private async Task<float[]> FuncLoadGraphicAsync(DateTime start, DateTime end)
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<MachineWorkPull>(MachineWorkPull.TABLE, MachineWorkPull.COLUMN_LINK, Data, "dateStart", MachineWorkPull.COLUMN_DATE, start, end));

            var dates = DateTimeCalculate.GetColumns(start, end);

            var list = new Dictionary<DateTimeCalculate.Range, List<double>>();

            for (int i = 0; i < dates.Length; i++)
                list.Add(dates[i],new List<double>() { 0.01d });

            for (int q = 0; q < data.Length; q++)
                for (int i = 0; i < dates.Length; i++)
                    if (dates[i].Start <= data[q].StartJob)
                            list[dates[i]].Add(data[q].TimeSpan.TotalHours);

            return DiagrammUtility.GetColumns(list.Values.Select(o => o.ToArray()).ToArray());
        }

        private async Task<(string LastActive, string TimeLastActive)> GetLastActive()
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<MachineWorkPull>(MachineWorkPull.TABLE, MachineWorkPull.COLUMN_LINK, Data, MachineWorkPull.COLUMN_DATE, DateTime.MinValue, DateTime.Now));

            if (data.Length <= 0)
                return ("no history", "no time");

            return (GetParseTimeSpan(data[^1].TimeSpan), $"{DateTime.Parse(data[^1].Columns["dateEnd"]):HH:mm dd.MM.yy}");
        }

        private async Task<HistoryData[]> GetHistoryAsync(DateTime start, DateTime end)
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<MachineWorkPull>(MachineWorkPull.TABLE, MachineWorkPull.COLUMN_LINK, Data, "dateStart", MachineWorkPull.COLUMN_DATE,start, end));
            
            var result = data.Select(o =>
            new HistoryData
            (
                MachineControll.GetIcon(o.Columns["state"]), 
                GetParseTimeSpan(o.TimeSpan),
                $"{DateTime.Parse(o.Columns["dateStart"]):HH:mm dd.MM.yy} - { DateTime.Parse(o.Columns["dateEnd"]):HH:mm dd.MM.yy}")
            ).ToArray();

            return result;
        }

        public static string GetParseTimeSpan(TimeSpan span)
        {
            var day = span.Days > 0 ? $"{span.Days}д. ": string.Empty;
            var hours = span.Hours > 0 ? $"{span.Hours}ч. " : string.Empty;
            return $"{day}{hours}{span.Minutes}мин.";
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
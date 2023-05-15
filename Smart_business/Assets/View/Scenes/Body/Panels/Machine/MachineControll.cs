using System.Collections.Generic;
using UnityEngine;
using Assets.View.Body.Menu;
using Assets.ViewModel;
using MachineData = Assets.ViewModel.Datas.Machine;
using Assets.View.Body.FullScreen.AnalyzeWindow;
using System;
using System.Threading.Tasks;
using Assets.ViewModel.PullDatas;
using System.Linq;
using Assets.View.Body.FullScreen.OptionsWindow;
using TMPro;
using Assets.View.Body.FullScreen.OptionsWindow.History;

namespace Assets.View.Body.Machine
{

    /// <summary>
    /// Центр по управлению машин
    /// </summary>

    public class MachineControll : PanelContent
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
        private List<MachineIcon> _icons;

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
        private VerticalMachine _verticalMachine;

        /// <summary>
        /// Имя используется в анализе
        /// </summary>
        public const string NameDepartment = "Нагрузка";

        /// <summary>
        /// Имя используется в анализе
        /// </summary>
        public const string NameItemDepartment = "Приборы";

        public static readonly AnalyzeProperty AnalyzeProperty = new(NameDepartment, NameItemDepartment, GetItemDatas);

        /// <summary>
        /// Паттерн Singleton
        /// </summary>
        private static MachineControll _singleton;

        /// <summary>
        /// Пробуждение
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            _singleton = this;
        }

        public static void FocusMachine(MachineBehaviour machine, OptionProperty option)
        {
            _singleton._titleText.text = machine.Data.Name;
            _singleton._descriptionText.text = machine.Data["dataSet"].Remove(machine.Data["dataSet"].Length - 7);
            _singleton._option.Open(option);

            _singleton._option.FirstStart();
        }

        public static void UpdateDatasOnChangers()
            => _singleton._verticalMachine.UpdateDatasOnChanger();

        public static async Task<HistoryData[]> GetHistoryItems()
        {
            var data = await Task.Run(() =>
             {
                 return ModelDatabase.GetPullObjectAsync<MachineWorkPull>(MachineWorkPull.TABLE, MachineWorkPull.COLUMN_DATE, DateTime.MinValue, DateTime.Now);
             });

            var result = new HistoryData[data.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = new HistoryData(
                   GetIcon(data[i].Columns["state"]),
                    $"{DateTime.Parse(data[i].Columns["dateStart"]):HH:mm dd.MM.yy} - {DateTime.Parse(data[i].Columns["dateEnd"]):HH:mm dd.MM.yy}",
                     MachineBehaviour.GetParseTimeSpan(data[i].TimeSpan));

            return result;
        }


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
               return ModelDatabase.GetPullObjectAsync<MachineWorkPull>(MachineWorkPull.TABLE, MachineWorkPull.COLUMN_DATE, start, end);
            });

            var machines = _singleton._verticalMachine.MachineDatas;
            var machinesPercent = new Dictionary<MachineData, uint>(machines.Length);
            var machineDictionary = new Dictionary<string, MachineData>(machines.Length);

            uint sumCount = 0;
            for (int i = 0; i < machines.Length; i++)
            {
                uint count = 0;
                for (int q = 0; q < data.Length; q++)
                    if (machines[i]["id"] == data[q].Columns[data[q].ColumnLink])
                        count += (uint)data[q].TimeSpan.TotalHours;

                sumCount += count;
                machinesPercent.Add(machines[i], count);
                machineDictionary.Add(machines[i]["id"],machines[i]);
            }

            var returnArray = new ItemData[machinesPercent.Count];
            int index = 0;

            foreach (var item in machinesPercent)
            {
                if(item.Value != 0f && sumCount != 0)
                     returnArray[index] = new ItemData(item.Key.Name, (item.Value / (float)sumCount) * 100f);
                else returnArray[index] = new ItemData(item.Key.Name,0f);

                index++;
            }

            var history = new HistoryData[data.Length];

            history = data.Select(o =>
             new HistoryData
             (
                 GetIcon(o.Columns["state"]),
                 (machineDictionary.ContainsKey(o.Columns[o.ColumnLink]) ? machineDictionary[o.Columns[o.ColumnLink]].Name : "not found") +": "+ MachineBehaviour.GetParseTimeSpan(o.TimeSpan),
                 $"{DateTime.Parse(o.Columns["dateStart"]):HH:mm dd.MM.yy} - {DateTime.Parse(o.Columns["dateEnd"]):HH:mm dd.MM.yy}"
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
            => ManagementAssistant.AccessAccount["Машиное отделение"].Contains("all") || ManagementAssistant.AccessAccount["Машиное отделение"].Contains(root);

    }
}

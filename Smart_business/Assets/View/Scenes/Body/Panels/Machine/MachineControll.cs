using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.View.Body.Menu;
using Assets.ViewModel;
using MachineData = Assets.ViewModel.Datas.Machine;
using ItemsLoad = Assets.View.Body.ItemsAnimationLoad.ControllLoadAnimation;
using Assets.View.Body.FullScreen;
using Assets.View.Body.FullScreen.AnalyzeWindow;
using System;
using System.Threading.Tasks;
using Assets.ViewModel.PullDatas;
using System.Linq;
using Assets.View.Body.FullScreen.CreatWindow;
using Assets.View.Body.FullScreen.Fields;
using Assets.View.Body.FullScreen.HistoryWindow;
using Assets.View.Body.FullScreen.OptionsWindow;
using Assets.View.Body.FullScreen.EditWindow;
using TMPro;

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

        [SerializeField]
        private Edit _edit;

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

        public static readonly HistoryProperty HistoryProperty = new(NameDepartment,GetHistoryItems);

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

        public static void FocusMachine(MachineBehaviour machine, OptionProperty option, EditProperty edit)
        {
            _singleton._titleText.text = machine.Data.Name;
            _singleton._descriptionText.text = machine.Data["dataSet"];
            _singleton._edit.Open(edit);
            _singleton._option.Open(option);
        }

        public static void UpdateDatasOnChangers()
            => _singleton._verticalMachine.UpdateDatasOnChanger();


        public void CreatMachine()
        {
            var newMachine = new MachineData();

            newMachine["id"] = "auto-ganerate";
            newMachine["name"] = "default machine";
            newMachine["dataSet"] = $"{DateTime.Now:yyyy.MM.dd}";
            newMachine["icon"] = "deafult";
            newMachine["isActive"] = "false";
            newMachine["startWorkDate"] = "20141212";
            newMachine["amount"] = "0";

            var property = new CreatProperty(newMachine, new ElementData[] 
            { 
                new ElementData("ID","id",newMachine["id"], isEdit: false, countSimbols: 7, isNumber:true),
                new ElementData("name","name",newMachine["name"],isEdit: true,countSimbols:20,isNumber: false),
                new ElementData("icon","icon",newMachine["icon"],isEdit: true, countSimbols:35, isNumber:false),
                new ElementData("amount","amount",newMachine["amount"],isEdit:false, countSimbols:1,isNumber:true),
                new ElementData("description","description","write description",isEdit:true,countSimbols: 1000, isNumber:false)
            }
            , UpdateDatasOnChangers);
        }

        public static async Task<HistoryData[]> GetHistoryItems()
        {
            var data = await Task.Run(() =>
             {
                 return ModelDatabase.GetPullObjectAsync<MachineWorkPull>(MachineWorkPull.TABLE, MachineWorkPull.COLUMN_DATE, DateTime.MinValue, DateTime.Now);
             });

            var result = new HistoryData[data.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = new HistoryData(
                    MachineBehaviour.GetParseTimeSpan(data[i].TimeSpan),
                    $"{DateTime.Parse(data[i].Columns["dateStart"]):HH:mm dd.MM.yy} - {DateTime.Parse(data[i].Columns["dateEnd"]):HH:mm dd.MM.yy}",
                    GetIcon(data[i].Columns["state"]));

            return result;
        }


        /// <summary>
        /// Загрузка данных о периоде машин
        /// </summary>
        /// <param name="start">С какого времени учитывать</param>
        /// <param name="end">По какое время учитывать</param>
        /// <returns>Список записей</returns>
        private static async Task<ItemData[]> GetItemDatas(DateTime start, DateTime end)
        {
            var data = await Task.Run(() =>
            {
               return ModelDatabase.GetPullObjectAsync<MachineWorkPull>(MachineWorkPull.TABLE, MachineWorkPull.COLUMN_DATE, start, end);
            });

            var machines = _singleton._verticalMachine.MachineDatas;
            var machinesPercent = new Dictionary<MachineData, uint>();

            uint sumCount = 0;
            for (int i = 0; i < machines.Length; i++)
            {
                uint count = 0;
                for (int q = 0; q < data.Length; q++)
                    if (machines[i]["id"] == data[q].Columns["idMachine"])
                        count += (uint)data[q].TimeSpan.TotalHours;

                sumCount += count;
                machinesPercent.Add(machines[i], count);
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

            return returnArray;
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

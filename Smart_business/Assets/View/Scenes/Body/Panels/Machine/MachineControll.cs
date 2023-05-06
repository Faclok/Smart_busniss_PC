using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.View.Body.Menu;
using Assets.ViewModel;
using Assets.View.Body.VioletSearch;
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

namespace Assets.View.Body.Machine
{

    /// <summary>
    /// ����� �� ���������� �����
    /// </summary>

    public class MachineControll : PanelContent
    {

        /// <summary>
        /// ������ �����
        /// </summary>
        [Header("Pack UI")]
        [SerializeField]
        private List<MachineIcon> _icons;

        /// <summary>
        /// ��������� ������, � ������ �������� ������
        /// </summary>
        [Space(15)]
        [SerializeField]
        private Sprite _defaultIcon;

        /// <summary>
        /// ������� Singleton
        /// </summary>
        private static MachineControll _singleton;

        /// <summary>
        /// ������ ������� ����������
        /// </summary>
        [Header("Multi panel")]
        [SerializeField]
        private MultiPanel _multiPanel;

        /// <summary>
        /// ������ �������� ��������
        /// </summary>
        [Header("Links")]
        [SerializeField]
        private ItemsLoad _itemsLoad;

        /// <summary>
        /// �������� ����� � ������� ����
        /// </summary>
        [SerializeField]
        private VerticalMachine _verticalMachine;

        /// <summary>
        /// ��� ������������ � �������
        /// </summary>
        public const string NameDepartment = "��������";

        public static TypeOpenMachine TypeOpen { get; private set; } = TypeOpenMachine.Option;

        /// <summary>
        /// ��� ������������ � �������
        /// </summary>
        public const string NameItemDepartment = "�������";

        public static readonly AnalyzeProperty AnalyzeProperty = new(NameDepartment, NameItemDepartment, GetItemDatas);

        public static readonly HistoryProperty HistoryProperty = new(NameDepartment,GetHistoryItems);

        /// <summary>
        /// �����������
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            _singleton = this;
        }

        /// <summary>
        /// ���������� ������ �������� ����������
        /// </summary>
        private void UpdateData()
        {
            _multiPanel.UpdateData(ManagementAssistant.AccessAccount[MachineData.TableContains]);
        }

        /// <summary>
        /// ���������� � ������ 
        /// </summary>
        public void OnAnalyze() => FullScreenPanels.OpenAnalyzeWindow(AnalyzeProperty);

        public void OnHistory() => FullScreenPanels.OpenHistoryData(HistoryProperty);

        public static void UpdateDatasOnChangers()
            => _singleton._verticalMachine.UpdateDatasOnChanger();

        public void OnEdit()
          =>  TypeOpen = TypeOpen == TypeOpenMachine.Edit ? TypeOpenMachine.Option : TypeOpenMachine.Edit;


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

            FullScreenPanels.OpenCreatData(property);
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
        /// �������� ������ � ������� �����
        /// </summary>
        /// <param name="start">� ������ ������� ���������</param>
        /// <param name="end">�� ����� ����� ���������</param>
        /// <returns>������ �������</returns>
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
        /// �������� ������ � ����������� ������
        /// </summary>
        /// <param name="name">��� ������</param>
        /// <returns></returns>
        public static Sprite GetIcon(string name)
        {
            foreach (var item in _singleton._icons)
                if (item.Name == name)
                    return item.Icon;

            return _singleton._defaultIcon;
        }

        public static bool IsRoot(string root)
            => ManagementAssistant.AccessAccount["machine"].Contains("all") || ManagementAssistant.AccessAccount["machine"].Contains(root);

        private void OnDestroy()
        {
            TypeOpen = TypeOpenMachine.Option;
        }
    }

    public enum TypeOpenMachine
    { Option, Edit }
}

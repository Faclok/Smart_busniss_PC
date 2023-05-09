using Assets.MultiSetting;
using Assets.View.Body.FullScreen;
using Assets.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using animationItems = Assets.View.Body.ItemsAnimationLoad;
using MachineData = Assets.ViewModel.Datas.Machine;

namespace Assets.View.Body.Machine
{

    /// <summary>
    /// ������������ ��� �������� ����� � ������� ����
    /// </summary>
    public class VerticalMachine : MonoBehaviour
    {
        /// <summary>
        /// ������� ����������� ������
        /// </summary>
        [HideInInspector] public new GameObject gameObject;

        /// <summary>
        /// ������� ����������� ������
        /// </summary>
        [HideInInspector] public new Transform transform;

        /// <summary>
        /// ������ ������ � ����
        /// </summary>
        [Header("Prefab")]
        [SerializeField] private MachineBehaviour _prefabMachine;

        /// <summary>
        /// ������� � ������� ��������������� ������
        /// </summary>
        [Header("Content Machine")]
        [SerializeField]
        private Transform _contentMachine;

        /// <summary>
        /// ������ �� ������� ����������� ������
        /// </summary>
        private float _itemHeight;
        
        /// <summary>
        /// ����������� ������
        /// </summary>
        private float _startHeight;

        /// <summary>
        /// ������������ ������
        /// </summary>
        private MachineBehaviour[] _machineBehaviours = new MachineBehaviour[0];

        /// <summary>
        /// ���������� ������
        /// </summary>
        private MachineData[] _machineDatas;

        /// <summary>
        /// �������� ����������� �����
        /// </summary>
        public MachineData[] MachineDatas => _machineDatas;

        /// <summary>
        /// �����������
        /// </summary>
        private void Awake()
        {
            gameObject = base.gameObject;
            transform = base.transform;
        }

        /// <summary>
        /// ������ ������� �����
        /// </summary>
        private void Start()
        {
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<MachineData>(MachineData.TableContains); }).GetTaskCompleted(OnDatasLoad);


            _startHeight = transform.localPosition.y;
            FilterControllMachine.FilterClick += OnSortFilter;
        }

        /// <summary>
        /// ����������� ������ �� �������
        /// </summary>
        /// <param name="comparer">������</param>
        private void OnSortFilter(IComparer<MachineBehaviour> comparer)
        {
            if (_machineBehaviours == null)
                return;

            Array.Sort(_machineBehaviours, comparer);

            for (int i = 1; i <= _machineBehaviours.Length; i++)
                _machineBehaviours[i - 1].transform.SetSiblingIndex(i);
        }

        /// <summary>
        /// ����� �������� ���������
        /// </summary>
        public void OnDatasLoad(MachineData[] machines)
        {
            _machineDatas = machines;

            var machineBehaviours = InstantiateExtensions.GetOverwriteInstantiate(_prefabMachine, _contentMachine, _machineBehaviours, _machineDatas);

            for (int i = 0; i < machines.Length; i++)
                machineBehaviours[i].UpdateData(machines[i]);

            _machineBehaviours = machineBehaviours;
        }

        public void UpdateDatasOnChanger()
        {
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<MachineData>(MachineData.TableContains); }).GetTaskCompleted(OnDatasLoad);
        }
        /// <summary>
        /// ����� �� ����������
        /// </summary>
        public void SearchClose()
        {
            for (int i = 0; i < _machineBehaviours?.Length; i++)
                _machineBehaviours[i].gameObject.SetActive(true);
        }

        /// <summary>
        /// ����� ������� ������� � �����
        /// </summary>
        /// <param name="input"></param>
        public void SearchChanger(InputField input)
        {
            if (string.IsNullOrWhiteSpace(input.text))
                return;

            for(int i = 0; i < _machineBehaviours?.Length; i++)
                _machineBehaviours[i].gameObject.SetActive(_machineBehaviours[i].Data.Name.Contains(input.text));
        }
    }
}
using Assets.MultiSetting;
using Assets.View.Body.FullScreen;
using Assets.View.SceneMove;
using Assets.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using ObjectStock = Assets.ViewModel.Datas.ObjectInStock;

namespace Assets.View.Body.Stock
{

    /// <summary>
    /// ������������ ��� �������� ����� � ������� ����
    /// </summary>
    public class VerticalStock : MonoBehaviour
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
        [SerializeField] private StockBehaviour _prefabStock;

        /// <summary>
        /// ������� � ������� ��������������� ������
        /// </summary>
        [Header("Content Machine")]
        [SerializeField]
        private Transform _contentStock;

        /// <summary>
        /// ������������ ������
        /// </summary>
        private StockBehaviour[] _stockBehaviours = new StockBehaviour[0];

        /// <summary>
        /// ���������� ������
        /// </summary>
        private ObjectStock[] _stockDatas = new ObjectStock[0];

        /// <summary>
        /// �������� ����������� �����
        /// </summary>
        public ObjectStock[] StockDatas => _stockDatas;

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
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<ObjectStock>(ObjectStock.TABLE); }).GetTaskCompleted(OnDatasLoad);
        }

        /// <summary>
        /// ����� �������� ���������
        /// </summary>
        public void OnDatasLoad(ObjectStock[] stocks)
        {
            _stockDatas = stocks;

            var stockBehaviours = InstantiateExtensions.GetOverwriteInstantiate(_prefabStock, _contentStock, _stockBehaviours, _stockDatas);

            for (int i = 0; i < stocks.Length; i++)
                stockBehaviours[i].UpdateData(stocks[i]);

            _stockBehaviours = stockBehaviours;

            if (_stockBehaviours.Length > 0)
                _stockBehaviours[0].Click();
        }

        public void UpdateDatasOnChanger()
        {
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<ObjectStock>(ObjectStock.TABLE); }).GetTaskCompleted(OnDatasLoad);
        }

        /// <summary>
        /// ����� �� ����������
        /// </summary>
        public void SearchClose()
        {
            for (int i = 0; i < _stockBehaviours?.Length; i++)
                _stockBehaviours[i].gameObject.SetActive(true);
        }

        /// <summary>
        /// ����� ������� ������� � �����
        /// </summary>
        /// <param name="input"></param>
        public void SearchChanger(InputField input)
        {
            if (string.IsNullOrWhiteSpace(input.text))
                return;

            for (int i = 0; i < _stockBehaviours?.Length; i++)
                _stockBehaviours[i].gameObject.SetActive(_stockBehaviours[i].Data.Name.Contains(input.text));
        }
    }
}
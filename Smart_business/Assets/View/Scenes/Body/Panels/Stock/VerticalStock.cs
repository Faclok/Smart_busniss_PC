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
    /// Используется для контроля машин в главном меню
    /// </summary>
    public class VerticalStock : MonoBehaviour
    {
        /// <summary>
        /// Заранее сохранненый объект
        /// </summary>
        [HideInInspector] public new GameObject gameObject;

        /// <summary>
        /// Заранее сохранненый объект
        /// </summary>
        [HideInInspector] public new Transform transform;

        /// <summary>
        /// Префаб машины в меню
        /// </summary>
        [Header("Prefab")]
        [SerializeField] private StockBehaviour _prefabStock;

        /// <summary>
        /// Контент в который устанавливаются машины
        /// </summary>
        [Header("Content Machine")]
        [SerializeField]
        private Transform _contentStock;

        /// <summary>
        /// Установление машины
        /// </summary>
        private StockBehaviour[] _stockBehaviours = new StockBehaviour[0];

        /// <summary>
        /// Загруженые машины
        /// </summary>
        private ObjectStock[] _stockDatas = new ObjectStock[0];

        /// <summary>
        /// Свойтсво загруженных машин
        /// </summary>
        public ObjectStock[] StockDatas => _stockDatas;

        /// <summary>
        /// Пробуждение
        /// </summary>
        private void Awake()
        {
            gameObject = base.gameObject;
            transform = base.transform;
        }

        /// <summary>
        /// Рендер первого кадра
        /// </summary>
        private void Start()
        {
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<ObjectStock>(ObjectStock.TABLE); }).GetTaskCompleted(OnDatasLoad);
        }

        /// <summary>
        /// Когда загрузка закончена
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
        /// Выход из поисковика
        /// </summary>
        public void SearchClose()
        {
            for (int i = 0; i < _stockBehaviours?.Length; i++)
                _stockBehaviours[i].gameObject.SetActive(true);
        }

        /// <summary>
        /// Когда вводите сымволы в поиск
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
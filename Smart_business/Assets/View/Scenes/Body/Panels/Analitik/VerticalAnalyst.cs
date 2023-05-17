using Assets.MultiSetting;
using Assets.View.Body.FullScreen;
using Assets.View.SceneMove;
using Assets.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using ProductData = Assets.ViewModel.Datas.Product;

namespace Assets.View.Body.Analyst
{
    public class VerticalAnalyst : MonoBehaviour
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
        [SerializeField] private AnalystBehaviour _prefabProduct;

        /// <summary>
        /// ������� � ������� ��������������� ������
        /// </summary>
        [Header("Content Machine")]
        [SerializeField]
        private Transform _contentProduct;

        /// <summary>
        /// ������������ ������
        /// </summary>
        private AnalystBehaviour[] _productBehaviours = new AnalystBehaviour[0];

        /// <summary>
        /// ���������� ������
        /// </summary>
        private ProductData[] _productDatas = new ProductData[0];

        /// <summary>
        /// �������� ����������� �����
        /// </summary>
        public ProductData[] ProductDatas => _productDatas;

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
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<ProductData>(ProductData.TABLE); }).GetTaskCompleted(OnDatasLoad);
        }

        /// <summary>
        /// ����� �������� ���������
        /// </summary>
        public void OnDatasLoad(ProductData[] products)
        {
            _productDatas = products;

            var productBehaviours = InstantiateExtensions.GetOverwriteInstantiate(_prefabProduct, _contentProduct, _productBehaviours, _productDatas);

            for (int i = 0; i < products.Length; i++)
                productBehaviours[i].UpdateData(products[i]);

            _productBehaviours = productBehaviours;

            if (_productBehaviours.Length > 0)
                _productBehaviours[0].Click();
        }

        public void UpdateDatasOnChanger()
        {
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<ProductData>(ProductData.TABLE); }).GetTaskCompleted(OnDatasLoad);
        }
        /// <summary>
        /// ����� �� ����������
        /// </summary>
        public void SearchClose()
        {
            for (int i = 0; i < _productBehaviours?.Length; i++)
                _productBehaviours[i].gameObject.SetActive(true);
        }

        /// <summary>
        /// ����� ������� ������� � �����
        /// </summary>
        /// <param name="input"></param>
        public void SearchChanger(InputField input)
        {
            if (string.IsNullOrWhiteSpace(input.text))
                return;

            for (int i = 0; i < _productBehaviours?.Length; i++)
                _productBehaviours[i].gameObject.SetActive(_productBehaviours[i].Data.Name.Contains(input.text));
        }
    }
}

﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Body.FullScreen.AnalyzeWindow
{

    /// <summary>
    /// Контроль данных
    /// </summary>
    public class ControllItems : MonoBehaviour
    {

        /// <summary>
        /// Префад элемента
        /// </summary>
        [Header("Prefab item")]
        [SerializeField]
        private ItemAnalyze _prefab;

        [Header("Link")]
        /// <summary>
        /// Тело в котором размещаются все элементы
        /// </summary>
        [SerializeField]
        private Transform _content;

        /// <summary>
        /// Все установленные элементы
        /// </summary>
        private ItemAnalyze[] _instantiateItems = new ItemAnalyze[0];

        /// <summary>
        /// обновление новых элементов
        /// </summary>
        /// <param name="datas">Данные</param>
        /// <param name="colors">Цвета</param>
        public ItemAnalyze[] UpdateData(Analyze analyze,ItemData[] datas, Color[] colors)
            => InstantiateItems(analyze,datas, colors);

        /// <summary>
        /// Установка данных
        /// </summary>
        /// <param name="datas">Данные</param>
        /// <param name="colors">Цвета</param>
        private ItemAnalyze[] InstantiateItems(Analyze analyze,ItemData[] datas, Color[] colors)
        {
            var list = InstantiateExtensions.GetOverwriteInstantiate(_prefab, _content, _instantiateItems, datas);

            for (int i = 0; i < datas.Length; i++)
                list[i].UpadteData(analyze, datas[i].Name, datas[i].Precent, colors[i]);

            return _instantiateItems = list;
        }
    }
}
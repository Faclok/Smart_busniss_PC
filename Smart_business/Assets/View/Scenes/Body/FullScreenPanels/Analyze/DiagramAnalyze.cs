using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.AnalyzeWindow
{

    /// <summary>
    /// Отрисовка данных 
    /// </summary>
    public class DiagramAnalyze : MonoBehaviour
    {

        /// <summary>
        /// Префаб для отрисовки
        /// </summary>
        [SerializeField]
        private ItemDraw _prefab;

        /// <summary>
        /// Тело в котором все элементы отрисовываются
        /// </summary>
        [SerializeField]
        private Transform _content;

        /// <summary>
        /// Установленные элементы
        /// </summary>
        private ItemDraw[] _itemsFill = new ItemDraw[0];

        /// <summary>
        /// Обновление данных
        /// </summary>
        /// <param name="datas">Данные</param>
        /// <param name="colors">Цвета</param>
        public ItemDraw[] UpdateData(ItemData[] datas, Color[] colors)
            => InstantiateItems(datas, colors);

        /// <summary>
        /// Установка новых элементов
        /// </summary>
        /// <param name="countAdd">Кол-во элементов которое нужно установить</param>
        /// <param name="datas">Данные</param>
        /// <param name="colors">Цвета</param>
        private ItemDraw[] InstantiateItems(ItemData[] datas, Color[] colors)
        {
            var list = InstantiateExtensions.GetOverwriteInstantiate(_prefab, _content, _itemsFill, datas);

            var currentSum = 0f;
            for (int i = 0; i < datas.Length; i++)
            {
                list[i].UpdateData(currentSum, datas[i].Precent, colors[i]);
                currentSum += datas[i].Precent;
            }

            return _itemsFill = list;
        }
    }
}
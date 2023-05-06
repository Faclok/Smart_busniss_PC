using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Body.Machine
{

    /// <summary>
    /// Выльтр для машин
    /// </summary>
    public class FilterControllMachine : MonoBehaviour
    {
        /// <summary>
        /// Объект фильтров
        /// </summary>
        [SerializeField]
        private GameObject _content;

        /// <summary>
        /// Событие при выбора фильтра
        /// </summary>
        public static Action<IComparer<MachineBehaviour>> FilterClick;
     
        /// <summary>
        /// Нажатие на фильтр
        /// </summary>
        /// <param name="filterItems">Фильтр</param>
        public void ClickFilter(FilterItemsMachine filterItems)
        {
            FilterClick?.Invoke(filterItems);
            CloseWindow();
        }

        /// <summary>
        /// Скрыть окно фильтров
        /// </summary>
        public void CloseWindow()
        {
            _content.SetActive(false);
        }

        /// <summary>
        /// Открыть окно фильтвов
        /// </summary>
        public void ShowWindow()
        {
            _content.SetActive(true);
        }
    }
}
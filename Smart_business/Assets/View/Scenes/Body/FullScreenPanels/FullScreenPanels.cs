using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.View.Body.FullScreen.AnalyzeWindow;
using System.Threading.Tasks;
using Assets.View.Body.FullScreen.OptionsWindow;
using Assets.View.Body.FullScreen.EditWindow;
using Assets.View.Body.FullScreen.CreatWindow;
using Assets.View.Body.FullScreen.HistoryWindow;

namespace Assets.View.Body.FullScreen
{

    /// <summary>
    /// Класс для открытия и закрытия окон
    /// </summary>
    public class FullScreenPanels : MonoBehaviour
    {

        /// <summary>
        /// Окно анализа
        /// </summary>
        [Header("Analyze")]
        [SerializeField]
        private Analyze _analyze;

        [Header("Options")]
        [SerializeField]
        private Option _optionsWindow;

        [Header("EditData")]
        [SerializeField]
        private Edit _editData;

        [Header("CreatData")]
        [SerializeField]
        private Creat _creat;

        [Header("HistoryData")]
        [SerializeField]
        private History _history;

        /// <summary>
        /// Паттерн singleton
        /// </summary>
        private static FullScreenPanels _singleton;

        /// <summary>
        /// Пробуждение
        /// </summary>
        private void Awake()
        {
            _singleton = this;
        }

        /// <summary>
        /// Открытие окна Анализа
        /// </summary>
        /// <param name="name">Имя анализа</param>
        /// <param name="nameItems">Имя объектов</param>
        /// <param name="funcLoad">Функция используемая для выгрузки данных</param>
        public static void OpenAnalyzeWindow(AnalyzeProperty property)
          =>  Open(_singleton._analyze, property);

        public static void OpenOption(OptionProperty property)
            => Open(_singleton._optionsWindow, property);

        public static void OpenEditData(EditProperty property)
            => Open(_singleton._editData, property);

        public static void OpenCreatData(CreatProperty property)
            => Open(_singleton._creat, property);

        public static void OpenHistoryData(HistoryProperty property)
            => Open(_singleton._history,property);
        
        private static void Open<T>(FullScreenPanel<T> panel, T property)
        {
            panel.gameObject.SetActive(true);
            panel.Open(property);
            panel.transform.SetAsLastSibling();
        }
    }
}

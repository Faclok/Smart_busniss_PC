using Assets.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// Управление нижней панелью
    /// </summary>
    public class LeftPanel : MonoBehaviour
    {

        /// <summary>
        /// Префаб кнопки
        /// </summary>
        [Header("Базовые настройки")]
        [SerializeField] private DivPanel _divPanel;

        /// <summary>
        /// Пак панелей 
        /// </summary>
        [SerializeField] private PackScriptableObject _scriptableObject;

        /// <summary>
        /// Контент для установки панелей
        /// </summary>
        [SerializeField] private Transform _contentPanels;

        
        /// <summary>
        /// Контент для установки кнопок
        /// </summary>
        [SerializeField] private Transform _contentDivs;

        /// <summary>
        /// Последняя установленная панель
        /// </summary>
        private IPanelContent _lastInstaitePanel;

        /// <summary>
        /// Последняя активная панель
        /// </summary>
        private IPanelContent _lastActivePanel;

        /// <summary>
        /// Пробуждение объекта
        /// </summary>
        void Awake()
        {
            AddButtonsInstaiate(GetButtonsInstaiates(ManagementAssistant.AccessAccount), ManagementAssistant.PropertyAccount["mainPanel"]);
        }

        /// <summary>
        /// Получение отделов которые доступны, согласно доступу пользователя
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        private Dictionary<ItemInPackScriptableObject, string[]> GetButtonsInstaiates(Dictionary<string, string[]> access)
        {
            var buttons = new Dictionary<ItemInPackScriptableObject, string[]>();

            foreach (var item in _scriptableObject.itemsInPackScriptableObject)
                if (access.ContainsKey(item.Title))
                    buttons.Add(item, access[item.Title]);

            return buttons;
        }

        /// <summary>
        /// Установление кнопок и панелей полученных с доступа
        /// </summary>
        /// <param name="buttons">отделы которые разрешенны</param>
        /// <param name="mainPanel">отдел который помечен как первый открывающийся</param>
        private void AddButtonsInstaiate(Dictionary<ItemInPackScriptableObject, string[]> buttons, string mainPanel)
        {
            ButtonPanel keyValue = null;

            foreach (var item in buttons)
            {
                var div = Instantiate(_divPanel, _contentDivs, false);
                var buttonFirst = div.UpdateData(item.Key.Title, item.Key.Content,item.Value);

                if (mainPanel == item.Key.Title)
                    keyValue = buttonFirst;
            }

            if (keyValue != null)
                keyValue.Click();
        }
    }
}
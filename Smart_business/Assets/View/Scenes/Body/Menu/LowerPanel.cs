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
    public class LowerPanel : MonoBehaviour
    {

        /// <summary>
        /// Префаб кнопки
        /// </summary>
        [Header("Базовые настройки")]
        [SerializeField] private ButtonPanel _buttonPrefab;

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
        [SerializeField] private Transform _contentButtons;

        /// <summary>
        /// Последняя установленная панель
        /// </summary>
        private IPanelContent _lastInstaitePanel;

        /// <summary>
        /// последнняя активная кнопка
        /// </summary>
        private ButtonPanel _lastActiveButton;

        /// <summary>
        /// Последняя активная панель
        /// </summary>
        private IPanelContent _lastActivePanel;

        /// <summary>
        /// Заранне установленные панели
        /// </summary>
        private Dictionary<ButtonPanel,IPanelContent> _onAwakePanels;

        /// <summary>
        /// Паттерн singleton используется для кнопок, а так же инкапсулирован
        /// </summary>
        private static LowerPanel _singleton;

        /// <summary>
        /// Пробуждение объекта
        /// </summary>
        void Awake()
        {
            _singleton = this;

            _onAwakePanels = new();

            AddButtonsInstaiate(GetButtonsInstaiates(ManagementAssistant.AccessAccount), ManagementAssistant.PropertyAccount["mainPanel"]);
        }

        /// <summary>
        /// Получение отделов которые доступны, согласно доступу пользователя
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        private ItemInPackScriptableObject[] GetButtonsInstaiates(Dictionary<string, string[]> access)
        {
            var buttons = new List<ItemInPackScriptableObject>();

            foreach (var item in _scriptableObject.itemsInPackScriptableObject)
                if (access.ContainsKey(item.Title))
                    buttons.Add(item);

            return buttons.ToArray();
        }

        /// <summary>
        /// Установление кнопок и панелей полученных с доступа
        /// </summary>
        /// <param name="buttons">отделы которые разрешенны</param>
        /// <param name="mainPanel">отдел который помечен как первый открывающийся</param>
        private void AddButtonsInstaiate(ItemInPackScriptableObject[] buttons, string mainPanel)
        {
            ButtonPanel mainButton = null, lastButton = null;

            foreach (var item in buttons)
            {
                var button = lastButton = Instantiate(_buttonPrefab, _contentButtons, false);
                var panel = Instantiate(item.Content, _contentPanels, false);

                button.Icon = item.Icon;

                _onAwakePanels.Add(button, panel);

                if (item.Title == mainPanel)
                    mainButton = button;
            }

            (mainButton ?? lastButton).Click();
        }

        /// <summary>
        /// Позволяет нажатой кнопке уведомить панель,
        /// используя паттер singleton
        /// </summary>
        /// <param name="button">нажатая кнопка</param>
        public static void ClickButton(ButtonPanel button)
            => _singleton.Click(button);

        /// <summary>
        /// Позволяет нажатой кнопке уведомить панель
        /// </summary>
        /// <param name="button">нажатая кнопка</param>
        public void Click(ButtonPanel button)
        {
            if (!_onAwakePanels.ContainsKey(button))
                return;

            _lastInstaitePanel?.Destroy();
            _lastInstaitePanel = null;
         
            _lastActiveButton?.Disable();
            _lastActiveButton = button;
            _lastActiveButton.Enable();
            
            _lastActivePanel?.Close();
            _lastActivePanel = _onAwakePanels[button];
            _lastActivePanel.Open();
        }

        /// <summary>
        /// Устанавливает новый контент в окне, использует Instantiate,
        /// используя паттер singleton
        /// </summary>
        /// <param name="panel"></param>
        [Obsolete]
        public static void OpenContent<T>(T panel)
            where T: MonoBehaviour, IPanelContent
            => _singleton.Open(panel);


        /// <summary>
        /// Устанавливает новый контент в окне, использует Instantiate
        /// </summary>
        /// <param name="panel"></param>
        [Obsolete]
        private void Open<T>(T panel)
            where T : MonoBehaviour, IPanelContent
        {
            _lastInstaitePanel?.Destroy();

            _lastActivePanel = _lastInstaitePanel = Instantiate(panel, _contentPanels, false);
        }
    }
}
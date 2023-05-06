using Assets.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// ���������� ������ �������
    /// </summary>
    public class LowerPanel : MonoBehaviour
    {

        /// <summary>
        /// ������ ������
        /// </summary>
        [Header("������� ���������")]
        [SerializeField] private ButtonPanel _buttonPrefab;

        /// <summary>
        /// ��� ������� 
        /// </summary>
        [SerializeField] private PackScriptableObject _scriptableObject;

        /// <summary>
        /// ������� ��� ��������� �������
        /// </summary>
        [SerializeField] private Transform _contentPanels;

        
        /// <summary>
        /// ������� ��� ��������� ������
        /// </summary>
        [SerializeField] private Transform _contentButtons;

        /// <summary>
        /// ��������� ������������� ������
        /// </summary>
        private IPanelContent _lastInstaitePanel;

        /// <summary>
        /// ���������� �������� ������
        /// </summary>
        private ButtonPanel _lastActiveButton;

        /// <summary>
        /// ��������� �������� ������
        /// </summary>
        private IPanelContent _lastActivePanel;

        /// <summary>
        /// ������� ������������� ������
        /// </summary>
        private Dictionary<ButtonPanel,IPanelContent> _onAwakePanels;

        /// <summary>
        /// ������� singleton ������������ ��� ������, � ��� �� ��������������
        /// </summary>
        private static LowerPanel _singleton;

        /// <summary>
        /// ����������� �������
        /// </summary>
        void Awake()
        {
            _singleton = this;

            _onAwakePanels = new();

            AddButtonsInstaiate(GetButtonsInstaiates(ManagementAssistant.AccessAccount), ManagementAssistant.PropertyAccount["mainPanel"]);
        }

        /// <summary>
        /// ��������� ������� ������� ��������, �������� ������� ������������
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
        /// ������������ ������ � ������� ���������� � �������
        /// </summary>
        /// <param name="buttons">������ ������� ����������</param>
        /// <param name="mainPanel">����� ������� ������� ��� ������ �������������</param>
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
        /// ��������� ������� ������ ��������� ������,
        /// ��������� ������ singleton
        /// </summary>
        /// <param name="button">������� ������</param>
        public static void ClickButton(ButtonPanel button)
            => _singleton.Click(button);

        /// <summary>
        /// ��������� ������� ������ ��������� ������
        /// </summary>
        /// <param name="button">������� ������</param>
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
        /// ������������� ����� ������� � ����, ���������� Instantiate,
        /// ��������� ������ singleton
        /// </summary>
        /// <param name="panel"></param>
        [Obsolete]
        public static void OpenContent<T>(T panel)
            where T: MonoBehaviour, IPanelContent
            => _singleton.Open(panel);


        /// <summary>
        /// ������������� ����� ������� � ����, ���������� Instantiate
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
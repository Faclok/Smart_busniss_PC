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
    public class LeftPanel : MonoBehaviour
    {

        /// <summary>
        /// ������ ������
        /// </summary>
        [Header("������� ���������")]
        [SerializeField] private DivPanel _divPanel;

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
        [SerializeField] private Transform _contentDivs;

        /// <summary>
        /// ��������� ������������� ������
        /// </summary>
        private IPanelContent _lastInstaitePanel;

        /// <summary>
        /// ��������� �������� ������
        /// </summary>
        private IPanelContent _lastActivePanel;

        /// <summary>
        /// ����������� �������
        /// </summary>
        void Awake()
        {
            AddButtonsInstaiate(GetButtonsInstaiates(ManagementAssistant.AccessAccount), ManagementAssistant.PropertyAccount["mainPanel"]);
        }

        /// <summary>
        /// ��������� ������� ������� ��������, �������� ������� ������������
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
        /// ������������ ������ � ������� ���������� � �������
        /// </summary>
        /// <param name="buttons">������ ������� ����������</param>
        /// <param name="mainPanel">����� ������� ������� ��� ������ �������������</param>
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
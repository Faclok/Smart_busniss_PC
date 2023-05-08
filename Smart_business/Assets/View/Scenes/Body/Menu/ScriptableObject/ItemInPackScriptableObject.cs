using Assets.View.Body.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// ������������ ��� ��������� ������ ������
    /// </summary>
    [CreateAssetMenu(fileName = "Item Pack", menuName = "ScriptableObject/Item in Pack Panel")]
    public class ItemInPackScriptableObject : ScriptableObject
    {

        /// <summary>
        /// ��� ��� ����� ������
        /// </summary>
        [Header("������")]
        [SerializeField] private string _title;

        /// <summary>
        /// �����
        /// </summary>
        [SerializeField] private PanelContent[] _panelConent;

        /// <summary>
        /// �������� � �� ����������, �������� ����� ���� � inspector
        /// </summary>
        public string Title => _title;

        /// <summary>
        /// �������� � �� ����������, �������� ����� ���� � inspector
        /// </summary>
        public PanelContent[] Content => _panelConent;
    }
}
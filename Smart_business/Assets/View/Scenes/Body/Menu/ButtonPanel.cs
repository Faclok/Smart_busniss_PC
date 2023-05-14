using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// ������ ������������� � ������ ������ ����
    /// </summary>
    public class ButtonPanel : MonoBehaviour
    {
        /// <summary>
        /// �������� ����������� transform, �.�. base �� �����������
        /// </summary>
        [HideInInspector] public new Transform transform;

        [Header("Text")]
        [SerializeField]
        private TextMeshProUGUI _textField;

        public PanelContent PanelContent { get; set; }

        public string Title { get => _textField.text; set => _textField.text = value; }


        public static event Action<ButtonPanel> EnableContent;

        private static ButtonPanel _lastActiveButton;

        public static readonly Color EnableColor = Color.black;

        public static readonly Color DisableColor = Color.grey;

        static ButtonPanel()
        {
            EnableContent += ControllActiveButtons;
        }

        private static void ControllActiveButtons(ButtonPanel button)
        {
            _lastActiveButton?.Disable();
            _lastActiveButton?.PanelContent.Close();

            _lastActiveButton = button;
            _lastActiveButton.Enable();
            _lastActiveButton.PanelContent.Open();
        }

        /// <summary>
        /// ����������� �������
        /// </summary>
        void Awake()
        {
            transform = base.transform;
        }

        /// <summary>
        /// ����� ������ ���������� ��������
        /// </summary>
        public void Enable()
        {

            if (_textField != null)
                _textField.color = EnableColor;
        }

        /// <summary>
        /// ����� ������ ����������� �� ��������
        /// </summary>
        public void Disable()
        {

            if (_textField != null)
                _textField.color = DisableColor;
        }

        /// <summary>
        /// ����� �������� �� ������
        /// </summary>
        public void Click()
        {
            EnableContent?.Invoke(this);
        }
    }
}
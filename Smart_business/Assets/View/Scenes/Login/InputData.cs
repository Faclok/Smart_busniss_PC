using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

using Assets.ViewModel;

namespace Assets.View.Scenes.Login
{

    /// <summary>
    /// �������� ����� ������ �� UI
    /// </summary>
    public class InputData : MonoBehaviour
    {

        /// <summary>
        /// ���� ����� ������
        /// </summary>
        [Header("Input Field")]
        [SerializeField]
        private TMP_InputField _login;

        /// <summary>
        /// ���� ����� ������
        /// </summary>
        [SerializeField]
        private TMP_InputField _password;

        /// <summary>
        /// CheckBox ����������� ������������
        /// </summary>
        [SerializeField]
        private Toggle _saveAccount;

        /// <summary>
        /// ���� � �������
        /// </summary>
        [Header("Controll Login")]
        [SerializeField]
        private Button _loginButton;

        /// <summary>
        /// ����������� ������������ �� �������
        /// </summary>
        private List<JsonConfig> _jsonConfigs;

        /// <summary>
        /// �������� ������
        /// </summary>
        public string Login => _login.text;

        /// <summary>
        /// �������� ������
        /// </summary>
        public string Password => _password.text;

        /// <summary>
        /// �������� �����������
        /// </summary>
        public bool SaveAccount => _saveAccount.isOn;

        /// <summary>
        /// ����������� �������
        /// </summary>
        private void Awake()
        {
            _jsonConfigs = ManagementAssistant.GetJsonConfigs().ToList();
            LoginAccount.OnLoginAccount += OnLoginAccount;
        }

        /// <summary>
        /// ���������� � ������ �������� ��������
        /// </summary>
        private void OnLoginAccount()
        {
            ManagementAssistant.UnLoadConfigs();

            if (SaveAccount)
                ManagementAssistant.AddJsonConfig(ManagementAssistant.ActiveLast);
        }

        /// <summary>
        /// ������������ ��������� � �������, �� �������� ������ ����������
        /// </summary>
        public void CheckString()
        {
            _loginButton.interactable = !(string.IsNullOrWhiteSpace(_login.text) || string.IsNullOrWhiteSpace(_password.text));
        }

        /// <summary>
        /// ������������� ������� �������������, ����� ������ Toggle,
        /// ��� �� ������������ � ���� ������
        /// </summary>
        public void ClickToggle()
        {
            _saveAccount.isOn = !_saveAccount.isOn;
        }

        /// <summary>
        /// ������������� ��������� ������ ���� ���� ����������� ������
        /// </summary>
        public void ChangerLogin()
        {
            for(int i= 0; i < _jsonConfigs.Count; i++)
                if (_jsonConfigs[i].Login == _login.text)
                    _password.text = _jsonConfigs[i].Password;
        }
    }
}

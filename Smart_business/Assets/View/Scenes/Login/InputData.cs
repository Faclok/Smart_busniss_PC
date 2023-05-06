using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

using Assets.ViewModel;

namespace Assets.View.Scenes.Login
{

    /// <summary>
    /// Контроль ввода данных из UI
    /// </summary>
    public class InputData : MonoBehaviour
    {

        /// <summary>
        /// Поле ввода логина
        /// </summary>
        [Header("Input Field")]
        [SerializeField]
        private TMP_InputField _login;

        /// <summary>
        /// Поле ввода пароля
        /// </summary>
        [SerializeField]
        private TMP_InputField _password;

        /// <summary>
        /// CheckBox сохраннения пользователя
        /// </summary>
        [SerializeField]
        private Toggle _saveAccount;

        /// <summary>
        /// Вход в аккаунт
        /// </summary>
        [Header("Controll Login")]
        [SerializeField]
        private Button _loginButton;

        /// <summary>
        /// Выгруженные пользователи из системы
        /// </summary>
        private List<JsonConfig> _jsonConfigs;

        /// <summary>
        /// Свойство логина
        /// </summary>
        public string Login => _login.text;

        /// <summary>
        /// Свойство пароля
        /// </summary>
        public string Password => _password.text;

        /// <summary>
        /// Свойство сохраннения
        /// </summary>
        public bool SaveAccount => _saveAccount.isOn;

        /// <summary>
        /// Пробуждение объекта
        /// </summary>
        private void Awake()
        {
            _jsonConfigs = ManagementAssistant.GetJsonConfigs().ToList();
            LoginAccount.OnLoginAccount += OnLoginAccount;
        }

        /// <summary>
        /// Вызывается в момент загрузки аккаунта
        /// </summary>
        private void OnLoginAccount()
        {
            ManagementAssistant.UnLoadConfigs();

            if (SaveAccount)
                ManagementAssistant.AddJsonConfig(ManagementAssistant.ActiveLast);
        }

        /// <summary>
        /// Контралирует вхождение в аккаунт, не допуская пустых параметров
        /// </summary>
        public void CheckString()
        {
            _loginButton.interactable = !(string.IsNullOrWhiteSpace(_login.text) || string.IsNullOrWhiteSpace(_password.text));
        }

        /// <summary>
        /// Устанавливает область прокликивания, кроме самого Toggle,
        /// так же прилигающего к нему текста
        /// </summary>
        public void ClickToggle()
        {
            _saveAccount.isOn = !_saveAccount.isOn;
        }

        /// <summary>
        /// Автоматически вставляет пароль если есть сохранненых конфиг
        /// </summary>
        public void ChangerLogin()
        {
            for(int i= 0; i < _jsonConfigs.Count; i++)
                if (_jsonConfigs[i].Login == _login.text)
                    _password.text = _jsonConfigs[i].Password;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.View.Scenes.Login
{

    /// <summary>
    /// Модуль работы с анимациями UI
    /// </summary>
    [RequireComponent(typeof(Animation))]
    public class AnimationUI : MonoBehaviour
    {

        /// <summary>
        /// Кнопка входа
        /// </summary>
        [Header("UI elements")]
        [SerializeField]
        private Button _buttonLogin;

        /// <summary>
        /// Поле ввода логина
        /// </summary>
        [SerializeField]
        private TMP_InputField _loginInput;

        /// <summary>
        /// Поле ввода пароля
        /// </summary>
        [SerializeField]
        private TMP_InputField _passwordInput;

        /// <summary>
        /// CheckBox сохраннения
        /// </summary>
        [SerializeField]
        private Toggle _toggleSave;

        /// <summary>
        /// Анимация загрузки
        /// </summary>
        [Header("Animation Clips")]
        [SerializeField]
        private AnimationClip _loadAccount;

        /// <summary>
        /// Анимация отсутсвия аккаунта
        /// </summary>
        [SerializeField]
        private AnimationClip _notAccount;

        /// <summary>
        /// Анимация успешного входа в аккаунт
        /// </summary>
        [SerializeField]
        private AnimationClip _loginAccount;

        /// <summary>
        /// Компоннент анимации
        /// </summary>
        private Animation _animation;

        /// <summary>
        /// Пробуждение объекта
        /// </summary>
        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        /// <summary>
        /// Метод для работы с анимациями, на основе входных параметров
        /// </summary>
        /// <param name="typeEvent">тип события для отображения анимации и контроля UI элементов</param>
        public void OnAnimationPlay(TypeEvent typeEvent)
        {
            var isOn = typeEvent == TypeEvent.NotAccount;  

            var nameClip = typeEvent switch
            {
                TypeEvent.Update => _loadAccount.name,
                TypeEvent.LoginAccount => _loginAccount.name,
                TypeEvent.ClickButton => _loadAccount.name,
                TypeEvent.NotAccount => _notAccount.name,
                _ => string.Empty
            };

            _toggleSave.interactable = _loginInput.interactable =
                _passwordInput.interactable = _buttonLogin.interactable = isOn;

            _animation.Play(nameClip);
        }

    }

    /// <summary>
    /// Типы событий
    /// </summary>
    public enum TypeEvent
    { ClickButton, NotAccount, LoginAccount , Update }
}

using System.Threading.Tasks;
using UnityEngine;
using System;

using Assets.MultiSetting;
using Assets.ViewModel;
using Assets.View.SceneMove;
using UnityEngine.UI;

namespace Assets.View.Scenes.Login
{

    /// <summary>
    /// Отвечает за логику входа в аккаунт
    /// </summary>
    [RequireComponent(typeof(InputData),typeof(AnimationUI))]
    public class LoginAccount :MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private Button _button;

        /// <summary>
        /// Система входа
        /// </summary>
        private InputData _input;

        /// <summary>
        /// Система анимаций
        /// </summary>
        private AnimationUI _animationUI;

        /// <summary>
        /// Событие вызываеммое в момент успешного входа в аккаунт
        /// </summary>
        public static event Action OnLoginAccount;

        /// <summary>
        /// Перед перемещением должна сыграть анимация
        /// </summary>
        private bool isAnimation;

        /// <summary>
        /// Пробужденнние объекта
        /// </summary>
        private void Awake()
        {
             _input = GetComponent<InputData>();
            _animationUI = GetComponent<AnimationUI>();
        }

        /// <summary>
        /// Событие кнопки Click
        /// </summary>
        public void OnLoginButton()
        {
            _button.interactable = false;
            Task.Run(() => ManagementAssistant.LoginAsync(_input.Login, _input.Password, _input.SaveAccount)).GetTaskCompleted(OnUpdateLoad);

            _animationUI.OnAnimationPlay(TypeEvent.ClickButton);
        }

        /// <summary>
        /// Проверка загрузки
        /// </summary>
        public void OnUpdateLoad(Result data)
        {
            _button.interactable = true;

            if (!data)
            {
                _animationUI.OnAnimationPlay(TypeEvent.NotAccount);
                return;
            }

            OnLoginAccount?.Invoke();
            _animationUI.OnAnimationPlay(TypeEvent.LoginAccount);
            SceneLoad.Move(ScenesApp.Body);
        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        private void OnDestroy()
        {
            OnLoginAccount = null;
        }
    }
}

using UnityEngine;
using TMPro;

using Assets.View.SceneMove;
using Assets.MultiSetting;
using System.Threading.Tasks;

namespace Assets.View.Load
{

    /// <summary>
    /// Модуль для работы с UI
    /// </summary>
    [RequireComponent(typeof(Animation))]
    public class ModuleUI : MonoBehaviour
    {

        /// <summary>
        /// Кнопка reset
        /// </summary>
        [SerializeField]
        private GameObject _button;

        /// <summary>
        /// Анимация повторения 
        /// </summary>
        [SerializeField]
        private AnimationClip _loadLoop;

        /// <summary>
        /// Анимация ошибки
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _textException;

        /// <summary>
        /// Объект работающий с анимациями
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
        /// Вызов метода когда снова пытаетесь подключится
        /// </summary>
        public void OnUpdate()
        {
            _animation.Play(_loadLoop.name);

            _textException.text = string.Empty;
            _button.SetActive(false);
        }

        /// <summary>
        /// Тип входа в систему
        /// </summary>
        /// <param name="isAccount"></param>
        public void OnLogin(bool isAccount)
        {
            if(isAccount)
                SceneLoad.Move(ScenesApp.Body);
            else SceneLoad.Move(ScenesApp.Login);

            _animation.Stop();
        }

        /// <summary>
        /// Проявление ошибки про загрузке в систему
        /// </summary>
        /// <param name="failed">тип ошибки</param>
        public void OnFailed(TypeException failed)
        {
            _animation.Stop();

            _textException.text = failed switch
            {
                TypeException.DisconnectedServer => "Не удалось подключиться к серверу.\nПопробуйте еще.",
                TypeException.NotNetwork => "Отсутствует интернет, проверте соединение.",
                TypeException.SystemFailed => "Прозошла системная ошибка, видимо приложение имеет некоторые несовместимости с вашим устройством.\nПожалуйста обратитесь к отдел разработки (ссылка).",
               TypeException.LogicApplication => "Видимо у разработчиков еще не совсем прямые руки, раз вы столкнулись с этой ошибкой =)",
                _ =>"Произошла не известная ошибка, перезагрузите приложение."
            };

            _button.SetActive(true);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// Кнопка используеммая в нижней панели меню
    /// </summary>
    [RequireComponent(typeof(Button), typeof(Animation))]
    public class ButtonPanel : MonoBehaviour
    {

        /// <summary>
        /// Компонент в котором будет отображаться иконка отдела
        /// </summary>
        [Header("UI")]
        [SerializeField] private Image _icon;

        /// <summary>
        /// Зараннее сохранненый transform, т.к. base не эффективный
        /// </summary>
        [HideInInspector] public new Transform transform;

        /// <summary>
        /// Свойтсво иконки
        /// </summary>
        public Sprite Icon
        {
            get => _icon.sprite;
            set => _icon.sprite = value;
        }

        /// <summary>
        /// RequireComponent гарантирует нам присутствие этого объекта
        /// </summary>
        private Animation _animation;

        /// <summary>
        /// Пробуждение объекта
        /// </summary>
        void Awake()
        {
            transform = base.transform;
            _animation = GetComponent<Animation>();
        }

        /// <summary>
        /// Когда кнопка становится активной
        /// </summary>
        public void Enable()
        {
            _animation.Play("ActiveButton");
        }

        /// <summary>
        /// Когда кнопка ставновится не активной
        /// </summary>
        public void Disable()
        {
            _animation.Play("DisableButton");
        }

        /// <summary>
        /// Когда нажимают на кнопку
        /// </summary>
        public void Click()
        {
            LowerPanel.ClickButton(this);
        }
    }
}
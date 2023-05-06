using System;
using System.Collections;
using UnityEngine;

namespace Assets.View.Body.ItemsAnimationLoad
{

    /// <summary>
    /// Элемент загрузки
    /// </summary>
    [RequireComponent(typeof(Animation), typeof(RectTransform))]
    public class ItemAnimation : MonoBehaviour
    {
        /// <summary>
        /// Квадрат текущего объекта
        /// </summary>
        private RectTransform _rectTransform;

        /// <summary>
        /// Проигрыватель анимаций
        /// </summary>
        private Animation _animation;

        /// <summary>
        /// Использование анимации, а точнее его имени для проигрывателя
        /// </summary>
        [Header("use name animation POWER")]
        [SerializeField]
        private AnimationClip _nameAnimation;

        [HideInInspector]
        public new GameObject gameObject;

        /// <summary>
        /// Возвращает и задает новые размеры квадрату
        /// </summary>
        public float Height
        {
            get => _rectTransform.sizeDelta.y;
            set => _rectTransform.sizeDelta = new Vector2(_rectTransform.rect.width, value);
        }

        /// <summary>
        /// пробуждение
        /// </summary>
        private void Awake()
        {
            gameObject = base.gameObject;
            _rectTransform = GetComponent<RectTransform>();
             _animation = GetComponent<Animation>();
        }

        /// <summary>
        /// Запуск анимации
        /// </summary>
        public void Play()
        {
            _animation.Play(_nameAnimation.name);
        }

        public void Stop()
        {
            _animation.Stop();
        }
    }
}
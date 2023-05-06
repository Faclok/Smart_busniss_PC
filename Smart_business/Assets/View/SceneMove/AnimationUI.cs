using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.SceneMove
{

    /// <summary>
    /// Для работы с анимациями UI
    /// </summary>
    [RequireComponent(typeof(Animation))]
    public class AnimationUI : MonoBehaviour
    {
        /// <summary>
        /// Анимация появления 
        /// </summary>
        [Header("Clips")]
        [SerializeField]
        private AnimationClip _Show;

        /// <summary>
        /// Анимация скрытия
        /// </summary>
        [SerializeField]
        private AnimationClip _Hide;

        /// <summary>
        /// Компонент Animation
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
        /// Метод запускает анимация показа
        /// </summary>
        public void Show()
        {
            _animation.Play(_Show.name);
        }

        /// <summary>
        /// Метод запускает анимация скрытия
        /// </summary>
        public void Hide()
        {
            _animation.Play(_Hide.name);
        }
    }
}

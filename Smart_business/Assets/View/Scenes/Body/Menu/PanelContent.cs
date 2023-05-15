using System;
using UnityEngine;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// Класс реализующий IPanelContent
    /// </summary>
    public class PanelContent : MonoBehaviour, IPanelContent
    {

        [Header("Name")]
        [SerializeField]
        private string _title;

        public string Title => _title;

        /// <summary>
        /// Зараннее сохранненый объект, ради оптимизации
        /// </summary>
        [HideInInspector] public new GameObject gameObject;

        /// <summary>
        /// Зараннее сохранненый объект, ради оптимизации
        /// </summary>
        [HideInInspector]
        public new Transform transform => _transform ??= base.transform;

        private Transform _transform;

        private static PanelContent _currentContent;


        public static event Action OnPanel;
        /// <summary>
        /// Вызывается когда объект открывается
        /// </summary>
        public event Action OnPanelOpen;

        /// <summary>
        /// Вызывается когда объект закрывается
        /// </summary>
        public event Action OnPanelClose;

        public bool IsFocus { get => _isFocus; private set => _isFocus = value; }

        private bool _isFocus = false;

        /// <summary>
        /// Пробуждение объекта
        /// </summary>
        public virtual void Awake()
        {
            gameObject = base.gameObject;
        }

        /// <summary>
        /// Открывает объект
        /// </summary>
        public virtual void Open()
        {
            _currentContent = this;
            _isFocus = true;

            OnPanel?.Invoke();
            OnPanelOpen?.Invoke();

            transform.SetAsLastSibling();
        }

        public static void EnableCurrent()
        {
            _currentContent.Open();
        }

        public static void DisableCurrent()
        {
            _currentContent.Close();
        }

        /// <summary>
        /// Закрывает объект
        /// </summary>
        public virtual void Close()
        {
            _isFocus = false;
            OnPanelClose?.Invoke();
        }

        /// <summary>
        /// Удаляет объект со сцены
        /// </summary>
        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
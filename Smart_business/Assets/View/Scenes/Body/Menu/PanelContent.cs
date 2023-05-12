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

        /// <summary>
        /// Вызывается когда объект открывается
        /// </summary>
        public event Action OnPanelOpen;

        /// <summary>
        /// Вызывается когда объект закрывается
        /// </summary>
        public event Action OnPanelClose;

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
            OnPanelOpen?.Invoke();

            transform.SetAsLastSibling();
        }

        /// <summary>
        /// Закрывает объект
        /// </summary>
        public virtual void Close()
        {
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
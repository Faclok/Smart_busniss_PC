using System.Collections;
using UnityEngine;

namespace Assets.View.Body.FullScreen
{

    /// <summary>
    /// Используется для открытия окон во весь экран
    /// </summary>
    public abstract class FullScreenPanel<T> : MonoBehaviour
    {
        private protected T _property;

        public virtual void Open(T property)
        {
            _property = property;
            OpenWindow();
        }

        public abstract void OpenWindow();

        /// <summary>
        /// закрытие окна
        /// </summary>
        public abstract void Close();
    }
}
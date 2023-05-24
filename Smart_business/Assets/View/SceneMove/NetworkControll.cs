using UnityEngine;
using System;
using System.Collections;

using Assets.ViewModel;

namespace Assets.View.SceneMove
{

    /// <summary>
    /// Контралирует подключение к интернету
    /// </summary>
    public class NetworkControll : MonoBehaviour, ITimerOneThread
    {
        /// <summary>
        /// Ссылка на таймер
        /// </summary>
        private Coroutine _timer;

        /// <summary>
        /// Пробуждение объекта
        /// </summary>
        private void Awake()
        {
            TimerDispatcher.TimerCreat(this, TimerDispatcher.DeplayRecommended);
        }

        /// <summary>
        /// Первый кадр
        /// </summary>
        private void Start()
        {
            TimerDispatcher.RunTimer();
        }

        /// <summary>
        /// Запускает таймер
        /// </summary>
        /// <param name="deplay">задержка</param>
        /// <param name="tick">событие, вызываемое при tick</param>
        public void Run(float deplay, Action tick)
          => _timer = StartCoroutine(CoroutineTimer(deplay, tick));

        /// <summary>
        /// Останавливает таймер
        /// </summary>
        public void Stop()
            => StopCoroutine(_timer);

        /// <summary>
        /// Реализация таймера с помощью MonoBehaviour
        /// </summary>
        /// <param name="deplayTime">задержка</param>
        /// <param name="tick">событие при tick</param>
        /// <returns></returns>
        private IEnumerator CoroutineTimer(float deplayTime, Action tick)
        {
            var deplay = new WaitForSeconds(deplayTime);

            while (true)
            {
                yield return deplay;

                tick?.Invoke();
            }
        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        private void OnDestroy()
        {
            TimerDispatcher.TimerDelete();
        }
    }
}

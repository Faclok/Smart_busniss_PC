using System;
using UnityEngine;
using Assets.MultiSetting;

namespace Assets.ViewModel
{
    public class TimerDispatcher
    {

        // Отдел для работы с таймером
        #region TimerEvent

        public static event Action Tick;

        /// <summary>
        /// Событие указывающее на тип подключения к интернету
        /// </summary>
        public static event Action<NetworkReachability> NetworkConnecting;

        /// <summary>
        /// Отображение последнего подключения
        /// </summary>
        public static TypeException LastTypeException { get; private set; }

        /// <summary>
        /// Рекомендуемое время для таймера
        /// </summary>
        public const float DeplayRecommended = 0.5f;

        /// <summary>
        /// Таймер основанный на MonoBehavior
        /// </summary>
        private static ITimerOneThread _timerOneThread;

        /// <summary>
        /// Последняя используемая задержка для старта таймера
        /// </summary>
        private static float _deplayTimer;

        /// <summary>
        /// Метод указывающий на вызов события NetworkConnecting
        /// </summary>
        private static void OnTick()
        {
            Tick?.Invoke();
            NetworkConnecting?.Invoke(Application.internetReachability);
        }

        /// <summary>
        /// Метод изменения таймера для события NetworkConnecting
        /// </summary>
        /// <param name="body">Элемент, на котором будет запущена Coroutine</param>
        /// <param name="deplayTimer">Время задержки, используя, по умолчанию используя класс WaitForSeconds</param>
        /// <param name="isRun">Запускает сразу таймер после установки</param>
        public static void TimerCreat(ITimerOneThread timer, float deplayTimer, bool isRun = false)
        {
            _timerOneThread = timer;
            _deplayTimer = deplayTimer;

            if (isRun)
                _timerOneThread.Run(deplayTimer, OnTick);
        }

        /// <summary>
        /// Если таймер установлен, то запускает его
        /// </summary>
        public static void RunTimer()
            => _timerOneThread?.Run(_deplayTimer, OnTick);

        /// <summary>
        /// Если таймер установлен, то останавливает его
        /// </summary>
        public static void StopTimer()
            => _timerOneThread?.Stop();

        /// <summary>
        /// Удаление таймера, что бы объект MonoBehavior смог успешно удалиться,
        /// Перед удалением останавливает таймер
        /// </summary>
        /// <param name="newTimer"></param>
        public static void TimerDelete(ITimerOneThread newTimer = null, float deplayTimer = -1f)
        {
            _timerOneThread.Stop();
            _timerOneThread = newTimer;

            _timerOneThread?.Run(deplayTimer <= 0f ? _deplayTimer : deplayTimer, OnTick);
        }

        #endregion

    }
}

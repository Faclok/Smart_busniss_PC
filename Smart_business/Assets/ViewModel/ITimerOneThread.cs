using System;

namespace Assets.ViewModel
{

    /// <summary>
    /// Таймер используемый для однопоточного вызова, лучшше использования в MonoBehavior
    /// </summary>
    public interface ITimerOneThread
    {

        /// <summary>
        /// Запускает таймер
        /// </summary>
        /// <param name="deplay">Задержка tick</param>
        /// <param name="tick">Событие вызываемое каждый тик</param>
        public void Run(float deplay, Action tick);

        /// <summary>
        /// Оставнавливает таймер
        /// </summary>
        public void Stop();
    }
}

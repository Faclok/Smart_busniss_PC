using System;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;

namespace Assets.Model
{

    /// <summary>
    /// Отвечает за таймер с помощью которого проверяет наличие интернет соедененния
    /// </summary>
    public static partial class Server
    {

        /// <summary>
        /// Объект таймера
        /// </summary>
        private readonly static Timer _timer;

        /// <summary>
        /// Подключение к интернету
        /// </summary>
        public static bool isConnectNetwork
        {
            get
            {
                try
                {
                    return new WebClient().DownloadString("https://www.google.ru/").Length > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        static Server()
        {
            var callback = new TimerCallback(OnTimedEvent);
            var resetEvent = new AutoResetEvent(false);

            _timer = new Timer(callback, resetEvent, 0, 500);
            
            Application.quitting += () => { _timer.Dispose(); };
        }

        /// <summary>
        /// Метод вызываемый каждый tick таймера
        /// </summary>
        /// <param name="sender"></param>
        private static void OnTimedEvent(object sender)
        {
        }
    }
}

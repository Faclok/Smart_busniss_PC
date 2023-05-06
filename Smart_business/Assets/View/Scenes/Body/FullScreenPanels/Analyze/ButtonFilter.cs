using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.AnalyzeWindow
{

    /// <summary>
    /// Кнопка периода
    /// </summary>
    public class ButtonFilter : MonoBehaviour
    {
        
        /// <summary>
        /// Кол-во дней
        /// </summary>
        [SerializeField]
        private int countDay;

        /// <summary>
        /// Задний фон кнопки
        /// </summary>
        [SerializeField]
        private Image _background;

        /// <summary>
        /// Текст кнопки
        /// </summary>
        [SerializeField]
        private Text _text;

        /// <summary>
        /// Свойство кол-ва дней
        /// </summary>
        public int CountDay => countDay;

        /// <summary>
        /// Включение кнопки
        /// </summary>
        public void On()
        {
            _background.color = Color.white;
            _text.color = Color.black;
        }

        /// <summary>
        /// Выключение кнопки
        /// </summary>
        public void Off()
        {
            _background.color = Color.clear;
            _text.color = Color.white;
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.AnalyzeWindow
{

    /// <summary>
    /// Элемент для отрисовки данных
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class ItemDraw : MonoBehaviour
    {
        
        /// <summary>
        /// Тело текущего объекта
        /// </summary>
        [HideInInspector]
        public new Transform transform;

        /// <summary>
        /// Объект текущего тела
        /// </summary>
        [HideInInspector]
        public new GameObject gameObject;

        /// <summary>
        /// Картинка для отрисовки
        /// </summary>
        private Image _image;

        /// <summary>
        /// Отношение одного шага от круга(360)
        /// </summary>
        public const float AROUND_ONE_PRECENT = 3.6f;

        public static bool isFillAmount = false;

        private bool _isDraw = true;

        public bool isDraw
        {
            get => _isDraw;

            set
            {
                _image.enabled = value;
                _isDraw = value;
            }

        }

        /// <summary>
        /// Пробуждение
        /// </summary>
        private void Awake()
        {
            transform = base.transform;
            gameObject = base.gameObject;
            _image = GetComponent<Image>();
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        /// <param name="prevSum">Сколько процентов уже пройдено</param>
        /// <param name="percent">Текущий процент</param>
        /// <param name="color">Цвет</param>
        public void UpdateData(float prevSum,float percent, Color color)
        {
            transform.rotation = Quaternion.Euler(0,0, -prevSum * AROUND_ONE_PRECENT);

            _image.fillAmount = percent * 0.01f;
            _image.color = color;

            if(_image.fillAmount > 0) isFillAmount = true;
        }
    }
}
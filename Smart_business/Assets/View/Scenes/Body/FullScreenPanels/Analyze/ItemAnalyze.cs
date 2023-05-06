using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.AnalyzeWindow
{

    /// <summary>
    /// Элемент данных в панели данных
    /// </summary>
    public class ItemAnalyze : MonoBehaviour
    {

        /// <summary>
        /// Поле для имени элемента
        /// </summary>
        [Header("UI")]
        [SerializeField]
        private Text _title;

        /// <summary>
        /// Поле для вывода процента
        /// </summary>
        [SerializeField]
        private Text _percent;

        /// <summary>
        /// Иконка элемента
        /// </summary>
        [SerializeField]
        private Image _colorIcon;

        public bool isSelect
        {
            get => _colorIcon.color.a != Translucent;

            set
            {
                var alpha = new Color(1f, 1f, 1f, value ? 1f: Translucent);

                _title.color = alpha;
                _percent.color = alpha;
                _colorIcon.color = value ? _color : new Color(_color.r, _color.g, _color.b, Translucent);
            }
        }

        private Color _color;

        public const float Translucent = 0.5f;

        /// <summary>
        /// Обновление данных
        /// </summary>
        /// <param name="title">Имя</param>
        /// <param name="percent">Процент</param>
        /// <param name="color">Цвет</param>
        public void UpadteData(string title, float  percent, Color color)
        {
            _title.text = title;
            _percent.text = percent.ToString("n2") + "%";
            _color = _colorIcon.color = color;
        }

        public void Click()
        {
            Analyze.ClickSelect(this);
        }
    }
}
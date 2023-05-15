using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.MultiSetting;

namespace Assets.View.Body.FullScreen.OptionsWindow.Review {
    public class MoveDate : MonoBehaviour
    {
        [Header("UI elements")]
        [SerializeField]
        private Text _dateField;

        [SerializeField]
        private Button _rightButton;

        [SerializeField]
        private Button _leftButton;

        [Header("Filters days")]
        [SerializeField]
        private FilterDate[] _buttonsFilters;

        public event Action<DateTime, DateTime> OnDateChanged;

        /// <summary>
        /// Обозначает количество дней при перемещении
        /// </summary>
        public const int MOVE_COUNT_DAY = 1;

        /// <summary>
        /// Временый промежутов используя MOVE_COUNT_DAY
        /// </summary>
        public readonly static TimeSpan Move_date = new(MOVE_COUNT_DAY, 0, 0, 0);

        /// <summary>
        /// Текущая временный промежуток
        /// </summary>
        private DateFrame _dateFrameCurrent;

        /// <summary>
        /// Перемещение во временных промежутках
        /// </summary>
        /// <param name="days">Кол-во дней для создания  периода</param>
        public void SetTimeFrame(int days)
        {
            _leftButton.interactable = _rightButton.interactable = false;

            switch (days)
            {
                case 0:

                    OnMoveDays(DateTime.MinValue, DateTime.Now, "За все время");

                    break;

                case 1:

                    OnMoveDays(DateTime.Today, DateTime.Now, $"{DateTime.Now:d}");
                    _leftButton.interactable = true;

                    break;

                default:

                    var start = DateTime.Now.AddDays(-days);
                    OnMoveDays(start, DateTime.Now, $"{start:d} - {DateTime.Now:d}");

                    break;
            }
        }

        public void FirstStart()
        {
            SetTimeFrame(_buttonsFilters[0]);
        }

        /// <summary>
        /// Перемещение в периодах, используя кнопки
        /// </summary>
        /// <param name="days">Нажатая кнопка</param>
        public void SetTimeFrame(FilterDate days)
        {
            for (int i = 0; i < _buttonsFilters.Length; i++)
                _buttonsFilters[i].Off();

            days.On();

            SetTimeFrame(days.CountDays);
        }

        /// <summary>
        /// Перемещение в днях, используя кнопки лево и право
        /// </summary>
        /// <param name="click">Нажатая кнопка</param>
        public void ClickMove(Button click)
        {
            var prevDate = click == _leftButton ? -Move_date : Move_date;
            var start = _dateFrameCurrent.Start.Add(prevDate);
            var end = start.Add(Move_date);
            OnMoveDays(start, end, $"{start:d}");

            _rightButton.interactable = end.DayOfYear <= DateTime.Now.DayOfYear;
        }

        /// <summary>
        /// Перемещение в промежуток времени
        /// </summary>
        /// <param name="start">Начало загрузки</param>
        /// <param name="end">Конец загрузки</param>
        /// <param name="viewText">Имя периода</param>
        private void OnMoveDays(DateTime start, DateTime end, string viewText)
        {
            _dateFrameCurrent = new DateFrame(start, end);

            _dateField.text = viewText;

            OnDateChanged?.Invoke(start, end);
        }

        private void OnDestroy()
        {
            OnDateChanged = null;
        }

        /// <summary>
        /// Временный промежуток
        /// </summary>
        private class DateFrame
        {
            public readonly DateTime Start;
            public readonly DateTime End;

            public DateFrame(DateTime start, DateTime end)
            {
                this.Start = start;
                this.End = end;
            }
        }
    }
}

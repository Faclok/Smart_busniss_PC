using Assets.MultiSetting;
using Assets.View.Body.ItemsAnimationLoad;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.AnalyzeWindow
{

    /// <summary>
    /// Управление окна анализа
    /// </summary>
    public class Analyze : MonoBehaviour
    {

        /// <summary>
        /// Поле для имени окна
        /// </summary>
        [Header("UI elements")]
        [SerializeField]
        private Text _title;

        /// <summary>
        /// Поле для имени объектов
        /// </summary>
        [SerializeField]
        private Text _titleItems;

        /// <summary>
        /// Поле для промежутка времени
        /// </summary>
        [SerializeField]
        private Text _timeFrame;

        [SerializeField]
        private Text _notData;

        /// <summary>
        /// Создание объектов для данных
        /// </summary>
        [Header("Components")]
        [SerializeField]
        private ControllItems _controllItems;

        /// <summary>
        /// Отрисовка данных
        /// </summary>
        [SerializeField]
        private DiagramAnalyze _diagram;

        /// <summary>
        /// Левая кнопка 
        /// </summary>
        [Header("Button")]
        [SerializeField]
        private Button _leftButton;

        /// <summary>
        /// Кнопки временых промежутков
        /// </summary>
        [SerializeField]
        private ButtonFilter[] _buttonsFilters;

        /// <summary>
        /// Правая кнопка
        /// </summary>
        [SerializeField]
        private Button _rightButton;

        /// <summary>
        /// Самый нагруженный
        /// </summary>
        [Header("Colors")]
        [SerializeField]
        private Color _colorMax;

        /// <summary>
        /// Самый не на груженный
        /// </summary>
        [SerializeField]
        private Color _colorMin;

        /// <summary>
        /// Анимация загрузки
        /// </summary>
        [Header("Animation")]
        [SerializeField]
        public Animation _animation;

        /// <summary>
        /// Тело загрузки
        /// </summary>
        [SerializeField]
        private GameObject _animationBody;

        [SerializeField]
        private ControllLoadAnimation _controllAnimation; 

        /// <summary>
        /// Обозначает количество дней при перемещении
        /// </summary>
        public const int MOVE_COUNT_DAY = 1;

        /// <summary>
        /// Временый промежутов используя MOVE_COUNT_DAY
        /// </summary>
        public readonly static TimeSpan Move_date = new(MOVE_COUNT_DAY, 0, 0, 0);

        /// <summary>
        /// Загрузка данных основанных, на удаленом контроле
        /// </summary>
        private Func<DateTime, DateTime, Task<ItemData[]>> _funcLoad;

        /// <summary>
        /// Текущая временный промежуток
        /// </summary>
        private DateFrame _dateFrameCurrent;

        private Dictionary<ItemAnalyze, ItemDraw> _seletItems = new();

        /// <summary>
        /// Открыть окно используя входные параметры
        /// </summary>
        /// <param name="name">Имя окна</param>
        /// <param name="nameItems">Имя объектов</param>
        /// <param name="funcLoad">Функция используемая для выгрузки данных</param>
        public void Open(AnalyzeProperty property)
        {
            _title.text = property.Name;
            _titleItems.text = property.NameItems;
            _funcLoad = property.FuncLoad;

            SetTimeFrame(_buttonsFilters[0]);
        }

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

                    MoveDate(DateTime.MinValue, DateTime.Now, "За все время");

                    break;

                case 1:

                    MoveDate(DateTime.Today, DateTime.Now, $"{DateTime.Now:d}");
                    _leftButton.interactable = true;

                    break;

                default:

                    var start = DateTime.Now.AddDays(-days);
                    MoveDate(start, DateTime.Now, $"{start:d} - {DateTime.Now:d}");

                    break;
            }
        }

        /// <summary>
        /// Перемещение в периодах, используя кнопки
        /// </summary>
        /// <param name="days">Нажатая кнопка</param>
        public void SetTimeFrame(ButtonFilter days)
        {
            for (int i = 0; i < _buttonsFilters.Length; i++)
                _buttonsFilters[i].Off();

            days.On();

            SetTimeFrame(days.CountDay);
        }

        public void OnCompletedTask(ItemData[] data)
        {
            _animation.Stop();
            _controllAnimation.HideItems();
            _animationBody.SetActive(false);

            UpdateData(data);
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
            MoveDate(start, end, $"{start:d}");

            _rightButton.interactable = end.DayOfYear <= DateTime.Now.DayOfYear;
        }

        /// <summary>
        /// Перемещение в промежуток времени
        /// </summary>
        /// <param name="start">Начало загрузки</param>
        /// <param name="end">Конец загрузки</param>
        /// <param name="viewText">Имя периода</param>
        private void MoveDate(DateTime start, DateTime end, string viewText)
        {
            _dateFrameCurrent = new DateFrame(start, end);

            _timeFrame.text = viewText;
            _controllAnimation.ShowItems();
            var task = _funcLoad(start, end).GetTaskCompleted(OnCompletedTask);
            _animationBody.SetActive(true);
            _animation.Play();
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        /// <param name="datas">Данные</param>
        public void UpdateData(ItemData[] datas)
        {
            var colors = GetColors(datas);

            _seletItems.Clear();
            ItemDraw.isFillAmount = false;
            
            var analyzes = _controllItems.UpdateData(this ,datas, colors);
            var draws = _diagram.UpdateData(datas, colors);

            _notData.enabled = !ItemDraw.isFillAmount;

            for (int i = 0; i < datas.Length; i++)
                _seletItems.Add(analyzes[i], draws[i]);
        }

        public void ClickSelect(ItemAnalyze itemAnalyze)
        {
            var selects = _seletItems;

            if (selects?.ContainsKey(itemAnalyze) ?? false)
            {
                var countSelect = 0;

                foreach (var keyParias in selects)
                    if (keyParias.Key.isSelect)
                        countSelect++;

                if (countSelect == selects.Count)
                {
                    foreach (var keyParias in selects)
                    {
                        keyParias.Key.isSelect = false;
                        keyParias.Value.isDraw = false;
                    }
                    itemAnalyze.isSelect = true;
                    selects[itemAnalyze].isDraw = true;
                }
                else if (countSelect == 1)
                {
                    if (itemAnalyze.isSelect)
                        foreach (var keyParias in selects)
                        {
                            keyParias.Key.isSelect = true;
                            keyParias.Value.isDraw = true;
                        }
                    else
                    {
                        itemAnalyze.isSelect = true;
                        selects[itemAnalyze].isDraw = true;
                    }
                }
                else selects[itemAnalyze].isDraw = itemAnalyze.isSelect = !itemAnalyze.isSelect;
            }
        }

        /// <summary>
        /// Получить цвет, с шагом от max до min
        /// </summary>
        /// <param name="datas">На основе данных</param>
        /// <returns>Массив равный входящему</returns>
        public Color[] GetColors(ItemData[] datas)
        {
            Array.Sort(datas);

            var colors = new Color[datas.Length];
            var colorFrame = 1f / datas.Length;

            for (int i = 0; i < datas.Length; i++)
                colors[i] = Color.Lerp(_colorMax, _colorMin, i * colorFrame);

            return colors;
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
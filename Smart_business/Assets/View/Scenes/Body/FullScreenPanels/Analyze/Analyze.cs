using Assets.MultiSetting;
using Assets.View.Body.FullScreen.OptionsWindow.History;
using Assets.View.Body.FullScreen.OptionsWindow.Review;
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
        [Header("Link")]
        [SerializeField]
        private MoveDate _moveDate;

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

        [SerializeField]
        private HistoryOption _history;

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
        private Animation _animation;

        /// <summary>
        /// Тело загрузки
        /// </summary>
        [SerializeField]
        private GameObject _animationBody;

        [SerializeField]
        private ControllLoadAnimation _controllAnimationItems;

        /// <summary>
        /// Загрузка данных основанных, на удаленом контроле
        /// </summary>
        private Func<DateTime, DateTime, Task<PackData>> _funcLoad;

        private Dictionary<ItemAnalyze, ItemDraw> _seletItems = new();


        private void Start()
        {
            _moveDate.OnDateChanged += MoveDate;
        }

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

            _moveDate.FirstStart();
        }

        public void OnCompletedTask(PackData data)
        {
            _animation.Stop();
            _controllAnimationItems.HideItems();
            _animationBody.SetActive(false);

            UpdateData(data.Items);
            _history.OnCompletedTask(data.Histories);
        }

        public void MoveDate(DateTime start, DateTime end)
        {
            _controllAnimationItems.ShowItems();
            _funcLoad(start, end).GetTaskCompleted(OnCompletedTask);
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

        private void OnDestroy()
        {
            _moveDate.OnDateChanged -= MoveDate;
        }
    }
}
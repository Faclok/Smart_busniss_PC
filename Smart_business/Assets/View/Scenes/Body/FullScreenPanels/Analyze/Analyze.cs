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
    /// ���������� ���� �������
    /// </summary>
    public class Analyze : MonoBehaviour
    {

        /// <summary>
        /// ���� ��� ����� ����
        /// </summary>
        [Header("UI elements")]
        [SerializeField]
        private Text _title;

        /// <summary>
        /// ���� ��� ����� ��������
        /// </summary>
        [SerializeField]
        private Text _titleItems;

        /// <summary>
        /// ���� ��� ���������� �������
        /// </summary>
        [SerializeField]
        private Text _timeFrame;

        [SerializeField]
        private Text _notData;

        /// <summary>
        /// �������� �������� ��� ������
        /// </summary>
        [Header("Components")]
        [SerializeField]
        private ControllItems _controllItems;

        /// <summary>
        /// ��������� ������
        /// </summary>
        [SerializeField]
        private DiagramAnalyze _diagram;

        /// <summary>
        /// ����� ������ 
        /// </summary>
        [Header("Button")]
        [SerializeField]
        private Button _leftButton;

        /// <summary>
        /// ������ �������� �����������
        /// </summary>
        [SerializeField]
        private ButtonFilter[] _buttonsFilters;

        /// <summary>
        /// ������ ������
        /// </summary>
        [SerializeField]
        private Button _rightButton;

        /// <summary>
        /// ����� �����������
        /// </summary>
        [Header("Colors")]
        [SerializeField]
        private Color _colorMax;

        /// <summary>
        /// ����� �� �� ���������
        /// </summary>
        [SerializeField]
        private Color _colorMin;

        /// <summary>
        /// �������� ��������
        /// </summary>
        [Header("Animation")]
        [SerializeField]
        public Animation _animation;

        /// <summary>
        /// ���� ��������
        /// </summary>
        [SerializeField]
        private GameObject _animationBody;

        [SerializeField]
        private ControllLoadAnimation _controllAnimation; 

        /// <summary>
        /// ���������� ���������� ���� ��� �����������
        /// </summary>
        public const int MOVE_COUNT_DAY = 1;

        /// <summary>
        /// �������� ���������� ��������� MOVE_COUNT_DAY
        /// </summary>
        public readonly static TimeSpan Move_date = new(MOVE_COUNT_DAY, 0, 0, 0);

        /// <summary>
        /// �������� ������ ����������, �� �������� ��������
        /// </summary>
        private Func<DateTime, DateTime, Task<ItemData[]>> _funcLoad;

        /// <summary>
        /// ������� ��������� ����������
        /// </summary>
        private DateFrame _dateFrameCurrent;

        private Dictionary<ItemAnalyze, ItemDraw> _seletItems = new();

        /// <summary>
        /// ������� ���� ��������� ������� ���������
        /// </summary>
        /// <param name="name">��� ����</param>
        /// <param name="nameItems">��� ��������</param>
        /// <param name="funcLoad">������� ������������ ��� �������� ������</param>
        public void Open(AnalyzeProperty property)
        {
            _title.text = property.Name;
            _titleItems.text = property.NameItems;
            _funcLoad = property.FuncLoad;

            SetTimeFrame(_buttonsFilters[0]);
        }

        /// <summary>
        /// ����������� �� ��������� �����������
        /// </summary>
        /// <param name="days">���-�� ���� ��� ��������  �������</param>
        public void SetTimeFrame(int days)
        {
            _leftButton.interactable = _rightButton.interactable = false;

            switch (days)
            {
                case 0:

                    MoveDate(DateTime.MinValue, DateTime.Now, "�� ��� �����");

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
        /// ����������� � ��������, ��������� ������
        /// </summary>
        /// <param name="days">������� ������</param>
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
        /// ����������� � ����, ��������� ������ ���� � �����
        /// </summary>
        /// <param name="click">������� ������</param>
        public void ClickMove(Button click)
        {
            var prevDate = click == _leftButton ? -Move_date : Move_date;
            var start = _dateFrameCurrent.Start.Add(prevDate);
            var end = start.Add(Move_date);
            MoveDate(start, end, $"{start:d}");

            _rightButton.interactable = end.DayOfYear <= DateTime.Now.DayOfYear;
        }

        /// <summary>
        /// ����������� � ���������� �������
        /// </summary>
        /// <param name="start">������ ��������</param>
        /// <param name="end">����� ��������</param>
        /// <param name="viewText">��� �������</param>
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
        /// ���������� ������
        /// </summary>
        /// <param name="datas">������</param>
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
        /// �������� ����, � ����� �� max �� min
        /// </summary>
        /// <param name="datas">�� ������ ������</param>
        /// <returns>������ ������ ���������</returns>
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
        /// ��������� ����������
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
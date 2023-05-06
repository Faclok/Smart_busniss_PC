using Assets.MultiSetting;
using Assets.View.Body.FullScreen.OptionsWindow.Review;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.View.Body.FullScreen.OptionsWindow.History
{
    public class HistoryOption : BodyOptionBlock
    {
        [Header("Prefab")]
        [SerializeField]
        private HistoryElement _historyElement;

        [SerializeField]
        private Transform _content;

        private Func<DateTime, DateTime,Task<HistoryData[]>> _callLoad;

        private HistoryElement[] _instantiateElements = new HistoryElement[0];

        private void Start()
        {
            MoveDate.OnDateChanged += OnDateMove;
        }

        public void UpdateData(Func<DateTime ,DateTime ,Task<HistoryData[]>> task)
        {
            _callLoad = task;
        }

        private void OnDateMove(DateTime date)
        {
            var loadTask = _callLoad(date, DateTime.Now);
            _callLoad(date,DateTime.Now).GetTaskCompleted(OnCompletedTask);
        }

        private void OnCompletedTask(HistoryData[] data)
        {
            var newArray = InstantiateExtensions.GetOverwriteInstantiate(_historyElement, _content, _instantiateElements, data);

            for (int i = 0; i < data.Length; i++)
                newArray[i].UpdateData(data[i].Title, data[i].Description, data[i].Icon);

            _instantiateElements = newArray;
        }
    }
}
using Assets.MultiSetting;
using Assets.View.Body.FullScreen.OptionsWindow.Review;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.View.Body.FullScreen.OptionsWindow.History
{
    public class HistoryOption : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField]
        private HistoryElement _historyElement;

        [Header("Link")]
        [SerializeField]
        private MoveDate _moveDate;

        [SerializeField]
        private Transform _content;

        [Header("Animation")]
        [SerializeField]
        private ItemsAnimationLoad.ControllLoadAnimation _controllLoadAnimation;

        private Func<DateTime, DateTime,Task<HistoryData[]>> _callLoad;

        private HistoryElement[] _instantiateElements = new HistoryElement[0];

        private void Start()
        {
            _moveDate.OnDateChanged += OnDateMove;
        }

        public void UpdateData(Func<DateTime ,DateTime ,Task<HistoryData[]>> task)
        {
            _callLoad = task;
        }

        private void OnDateMove(DateTime start, DateTime end)
        {

            if(_callLoad != null ) 
            _callLoad(start, end).GetTaskCompleted(OnCompletedTask);

            _controllLoadAnimation.ShowItems();
        }

        public void OnCompletedTask(HistoryData[] data)
        {
            var newArray = InstantiateExtensions.GetOverwriteInstantiate(_historyElement, _content, _instantiateElements, data);

            for (int i = 0; i < data.Length; i++)
                newArray[i].UpdateData(data[i].Title, data[i].Description, data[i].Icon);

            _instantiateElements = newArray;
            _controllLoadAnimation.HideItems();
        }

        private void OnDestroy()
        {
            _moveDate.OnDateChanged -= OnDateMove;
        }
    }
}
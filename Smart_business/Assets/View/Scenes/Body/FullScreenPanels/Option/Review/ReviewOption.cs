using System;
using System.Collections;
using UnityEngine;

namespace Assets.View.Body.FullScreen.OptionsWindow.Review
{
    public class ReviewOption : BodyOptionBlock
    {
        [Header("Components")]
        [SerializeField]
        private LastActive _rowDate;

        [SerializeField]
        private MoveDate _moveDate;

        [Header("Open Links")]
        [SerializeField]
        private FilterDate _filterDate;

        [Header("Animation")]
        [SerializeField]
        private Animation _animation;

        [SerializeField]
        private GameObject _animationGameObject;

        public override void Awake()
        {
            _currentBody = this;
            base.Awake();

        }

        private void Start()
        {
            MoveDate.OnDateChanged += OnMoveDate;
            MoveDate.OnTaskCompleted += OnTaskCompleted;
        }

        public void UpdateData(ReviewProperty property)
        {
            _moveDate.UpdateData(property.FuncLoadGraphic);
            _rowDate.UpdateIcon(property.IconDate);
            _rowDate.UpdateDate(property.ValueLastActive,property.TimeLastActive);

            _moveDate.SetTimeFrame(_filterDate);
        }

        private void OnMoveDate(DateTime time)
        {
            _animationGameObject.SetActive(true);
            _animation.Play();
        }

        private void OnTaskCompleted(float[] values)
        {
            _animationGameObject.SetActive(false);
            _animation.Stop();
        }
    }
}
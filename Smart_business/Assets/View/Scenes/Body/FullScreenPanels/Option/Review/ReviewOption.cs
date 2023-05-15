using Assets.MultiSetting;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.View.Body.FullScreen.OptionsWindow.Review
{
    public class ReviewOption : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private LastActive _rowDate;

        [SerializeField]
        private MoveDate _moveDate;

        [SerializeField]
        private GraphicLine _graphicLine;

        [SerializeField]
        private HelperAI _helperAI;

        [Header("Animation")]
        [SerializeField]
        private Animation _animation;

        [SerializeField]
        private GameObject _animationGameObject;

        private ReviewProperty _property;

        private void Start()
        {
            _moveDate.OnDateChanged += OnMoveDate;
        }

        public async void UpdateData(ReviewProperty property)
        {
            _property = property;
            _rowDate.UpdateIcon(property.IconDate);

            var tuple = await property.FuncLastActive();
            _rowDate.UpdateDate(tuple.LastActive, tuple.TimeLastActive);
        }

        public void FirstStart()
        {
            _moveDate.FirstStart();
        }

        private void OnMoveDate(DateTime start, DateTime end)
        {
            _animationGameObject.SetActive(true);
            _animation.Play();

            _property.FuncLoadGraphic(start, end).GetTaskCompleted(OnTaskCompleted);
        }

        private void OnTaskCompleted(float[] values)
        {
            _animationGameObject.SetActive(false);
            _animation.Stop();

            _helperAI.UpdateText(values);
            _graphicLine.UpdateDraw(values);
        }

        private void OnDestroy()
        {
            _moveDate.OnDateChanged -= OnMoveDate;
        }
    }
}
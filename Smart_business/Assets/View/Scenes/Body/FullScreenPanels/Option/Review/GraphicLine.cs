using System;
using System.Collections;
using UnityEngine;
using Assets.View.Body.Menu;

namespace Assets.View.Body.FullScreen.OptionsWindow.Review
{
    public class GraphicLine : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private LineRenderer _lineRender;

        [SerializeField]
        private UIGradient _gradient;

        [SerializeField]
        private GameObject _bodyLine;

        [SerializeField]
        private GameObject _bodyLineParent;

        [SerializeField]
        private RectTransform _rectLine;

        [SerializeField]
        private MoveDate _moveDate;

        [Header("Colors")]
        [SerializeField]
        private Color _stonksColor;

        [SerializeField]
        private Color _noStonksColor;

        [Header("Setting")]
        [SerializeField]
        private float _distanceZ;

        [SerializeField]
        private PanelContent _panelContent;

        private void Start()
        {
            _moveDate.OnDateChanged += OnMoveDate;

            _panelContent.OnPanelOpen += Enable;
            _panelContent.OnPanelClose += Disable;
        }

        private void Disable()
            => _bodyLineParent.SetActive(false);

        private void Enable()
            => _bodyLineParent.SetActive(true);

        public void OnMoveDate(DateTime start, DateTime end) => _bodyLine.SetActive(false);

        public void UpdateDraw(float[] values)
        {
            if (_panelContent.IsFocus)
                _bodyLineParent.SetActive(true);

            _bodyLine.SetActive(true);

            _gradient.m_color2 = _lineRender.startColor = values.Length > 0 ? _lineRender.endColor = values[0] > values[^1] ? _stonksColor : _noStonksColor : Color.white;

            _gradient.enabled = false; //Не знаю как еще раз вызвать отрисовку, т.к. объект не перерисовывается после изменения данных
            _gradient.enabled = true;

            var width = _rectLine.rect.width;
            var height = _rectLine.rect.height;

            if (values.Length < 2)
            {
                _lineRender.positionCount = 2;
                _lineRender.SetPosition(0, new Vector3(0, height / 2f, _distanceZ));
                _lineRender.SetPosition(1, new Vector3(width, height / 2f, _distanceZ));

                _gradient.m_color2 = _lineRender.endColor = _lineRender.startColor = Color.white;
            }
            else
            {
                float distance = width / (values.Length - 1);
                Debug.Log($"width: {width}");
                Debug.Log($"Disctance:  {distance}");
                Debug.Log($"Count: {values.Length}");
                Debug.Log($"Delenia: {values.Length - 1}");

                _lineRender.positionCount = values.Length;

                _gradient.m_color2 = _lineRender.startColor = _lineRender.endColor = values[0] > values[^1] ? _stonksColor : _noStonksColor;


                for (int i = 0; i < values.Length; i++)
                {
                    _lineRender.SetPosition(i, new Vector3(i * distance, values[i] * height, _distanceZ));
                    Debug.Log($"{i}) x: {i * distance}");
                }
            }
        }

        private void OnDestroy()
        {
            _panelContent.OnPanelClose -= Disable;
            _panelContent.OnPanelOpen -= Enable;
            _moveDate.OnDateChanged -= OnMoveDate;
        }
    }
}
using System;
using System.Collections;
using UnityEngine;
using Assets.View.Body.Menu;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.OptionsWindow.Review
{
    public class GraphicLine : MonoBehaviour
    {
        [Header("Line Render")]
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

        [Space(20f)]
        [Header("Table")]
        [Space(5f)]
        [Header("Prefab")]
        [SerializeField]
        private Image _imageColumn;

        [Header("Content")]
        [SerializeField]
        private Transform _contentColumns;

        [SerializeField]
        private RectTransform _rectTable;

        [Header("Setting")]
        [SerializeField]
        private float _fixedWidth;

        [SerializeField]
        private GameObject _bodyTable;

        private Image[] _columnsInstantiante = new Image[0];

        private State _state = State.Line;

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
            if (_state == State.Line)
            {
                if (_panelContent.IsFocus)
                    _bodyLineParent.SetActive(true);

                _bodyLine.SetActive(true);
            }

            UpdateDrawLine(values);
            UpdateDrawTable(values);
        }

        private void UpdateDrawLine(float[] values)
        {
            _gradient.m_color2 = _lineRender.startColor = values.Length > 0 ? _lineRender.endColor = values[0] > values[^1] ? _stonksColor : _noStonksColor : Color.white;

            _gradient.enabled = false; //Не знаю как еще раз вызвать отрисовку, т.к. объект не перерисовывается после изменения данных
            _gradient.enabled = true;

            var width = _rectLine.rect.width;
            var height = _rectLine.rect.height;

            float distance = width / (values.Length - 1);
            _lineRender.positionCount = values.Length;

            switch (values[0] - values[^1])
            {
                case > 0: _gradient.m_color2 = _lineRender.startColor = _lineRender.endColor = _stonksColor; break;
                case < 0: _gradient.m_color2 = _lineRender.startColor = _lineRender.endColor = _noStonksColor; break;
                case 0: _gradient.m_color2 = _lineRender.endColor = _lineRender.startColor = Color.white; break;
            }

            for (int i = 0; i < values.Length; i++)
                _lineRender.SetPosition(i, new Vector3(i * distance, values[i] * height, _distanceZ));
        }

        private void UpdateDrawTable(float[] values)
        {
            var width = _rectTable.rect.width;
            var height = _rectTable.rect.height;
            float distance = width / (values.Length - 1);
            var newArray = InstantiateExtensions.GetOverwriteInstantiate(_imageColumn, _contentColumns, _columnsInstantiante, values);

            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i].rectTransform.sizeDelta = new Vector2(i * distance, values[i] * height);
                newArray[i].color = UnityEngine.Random.ColorHSV(0f,1f, 0f, 1f, 0f, 1f, 1f, 1f);
            }
        }

        public void TableFocus()
        {
            if (_state == State.Table)
                return;

            _bodyTable.SetActive(true);
            _bodyLine.SetActive(false);

            _state = State.Table;
        }

        public void LineFocus()
        {
            if (_state == State.Line)
                return;

            _bodyTable.SetActive(false);
            _bodyLine.SetActive(true);

            _state = State.Line;
        }

        public void EnableImage(Image image)
             => image.color = Color.white;

        public void DisableImage(Image image)
            => image.color = Color.gray;

        private void OnDestroy()
        {
            _panelContent.OnPanelClose -= Disable;
            _panelContent.OnPanelOpen -= Enable;
            _moveDate.OnDateChanged -= OnMoveDate;
        }

        private enum State
        { Line, Table }
    }
}
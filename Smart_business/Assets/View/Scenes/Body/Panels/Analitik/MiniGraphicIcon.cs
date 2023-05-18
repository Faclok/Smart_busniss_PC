using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.View.Body.Analyst
{
    public class MiniGraphicIcon : MonoBehaviour
    {

        [Header("Link")]
        [SerializeField]
        private LineRenderer _lineRender;

        [SerializeField]
        private RectTransform _rectLine;

        [Header("Colors")]
        [SerializeField]
        private Color _stonksColor;

        [SerializeField]
        private Color _noStonksColor;

        [Header("Setting")]
        [SerializeField]
        private float _distanceZ;

        public void UpdateData(float[] data)
        {
            _lineRender.startColor = data.Length > 0 ? _lineRender.endColor = data[0] > data[^1] ? _stonksColor : _noStonksColor : Color.white;

            var width = _rectLine.rect.width;
            var height = _rectLine.rect.height;

            if (data.Length < 2)
            {
                _lineRender.positionCount = 2;
                _lineRender.SetPosition(0, new Vector3(0, height / 2f, _distanceZ));
                _lineRender.SetPosition(1, new Vector3(width, height / 2f, _distanceZ));

                 _lineRender.endColor = _lineRender.startColor = Color.white;
            }
            else
            {
                float distance = width / (data.Length - 1);
                _lineRender.positionCount = data.Length;

                 _lineRender.startColor = _lineRender.endColor = data[0] > data[^1] ? _stonksColor : _noStonksColor;


                for (int i = 0; i < data.Length; i++)
                    _lineRender.SetPosition(i, new Vector3(i * distance, data[i] * height, _distanceZ));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.Analyst
{

    [RequireComponent(typeof(Button))]
    public class CalculateElement : MonoBehaviour
    {

        [Header("Texts")]
        [SerializeField]
        private TextMeshProUGUI _title;

        [SerializeField]
        private Image _background;

        [Header("Setting")]
        [SerializeField]
        private Sprite _enableBackground;

        [SerializeField]
        private Sprite _disableBackground;

        [Header("Data")]
        [SerializeField]
        private CalculateFieldData[] _fieldDatas = new CalculateFieldData[0];

        [SerializeField]
        private int _keyMethod;

        [SerializeField]
        private string _option;

        public CalculateFieldData[] FieldDatas => _fieldDatas;

        public int Key => _keyMethod;

        public string Option => _option;

        public static readonly Color _enableText = new(0.12f, 0.13f, 0.13f);

        public void Enable()
        {
            _title.color = _enableText;
            _background.sprite = _enableBackground;
        }

        public void Disable()
        { 
            _title.color = Color.white;
            _background.sprite = _disableBackground;
        }
    }
}

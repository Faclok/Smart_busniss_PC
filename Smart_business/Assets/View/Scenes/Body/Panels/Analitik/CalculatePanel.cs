using Assets.View.Body.Menu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.View.Body.Analyst
{
    public class CalculatePanel : PanelContent
    {

        [Header("Text Title")]
        [SerializeField]
        private TextMeshProUGUI _optionContent;

        [SerializeField]
        private CalculateElement _elementActive;

        [Header("Preab")]
        [SerializeField]
        private CalculateField _prefabField;

        [SerializeField]
        private Transform _content;

        [SerializeField]
        private CalculateField[] _fieldInstantiate = new CalculateField[0];

        public readonly Dictionary<int, Action<CalculateField[]>> _calculateMethods = new()
        {
            [0] = GrossProfitAndMarkup,
            [1] = CalculationOfThePriceByProfitability,
            [2] = CalculationOfThePriceByMarkup
        };

        public void ClickCalculateElement(CalculateElement element)
        {
            _elementActive.Disable();
            _elementActive = element;
            _elementActive.Enable();

            _optionContent.text = element.Option;

            _fieldInstantiate = InstantiateExtensions.GetOverwriteInstantiate(_prefabField, _content, _fieldInstantiate, element.FieldDatas.Length);

            for (int i = 0; i < _fieldInstantiate.Length; i++)
                _fieldInstantiate[i].UpdateData(element.FieldDatas[i]);
        }

        public void  ClickMath()
           => _calculateMethods[_elementActive.Key](_fieldInstantiate);

        private static void GrossProfitAndMarkup(CalculateField[] field)
        {
            var value1 = field[0].Value;
            var value2 = field[1].Value;

            field[2].ValueView = Math.Round(value2 - value1, 2).ToString();
            field[3].ValueView = Math.Round((value2 - value1) / value2 * 100m).ToString();
            field[4].ValueView = Math.Round(100m - (value2 - value1) * 100m, 2).ToString();
        }

        private static void CalculationOfThePriceByProfitability(CalculateField[] field)
        {
            var value1 = field[0].Value;
            var value2 = field[1].Value;
            
            field[2].ValueView = Math.Round(value1 / (1-(value2 / 100m)), 2).ToString();
            field[3].ValueView = Math.Round(value1 * (value2 / 100m),2).ToString();
            field[4].ValueView = Math.Round((value2 / (100m-value2))* 100m,2).ToString();
        }

        private static void CalculationOfThePriceByMarkup(CalculateField[] field)
        {
            var value1 = field[0].Value;
            var value2 = field[1].Value;

            field[2].ValueView = Math.Round(value1 * (1 + (value2 / 100m)), 2).ToString();
            field[3].ValueView = Math.Round(value1 * (value2 / 100m), 2).ToString();
            field[4].ValueView = Math.Round((((value1 * (1 + (value2 / 100m))) - value1) / (value1 * (1 + (value2 / 100m)))) * 100m, 2).ToString();
        }
    }
}

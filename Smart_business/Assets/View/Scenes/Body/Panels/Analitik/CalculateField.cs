using UnityEngine;
using TMPro;

namespace Assets.View.Body.Analyst
{
    public class CalculateField : MonoBehaviour
    {

        [Header("Body")]
        [SerializeField]
        private GameObject _inputBody;

        [SerializeField]
        private GameObject _outputBody;

        [Header("UI")]
        [SerializeField]
        private TMP_InputField _inputField;

        [SerializeField]
        private TextMeshProUGUI _textView;

        [SerializeField]
        private TextMeshProUGUI _title;

        public decimal Value => _value;

        private decimal _value;

        public string ValueView
        {
            get => _textView.text;
            set => _textView.text = value;
        }

        public void OnTextChanged(TMP_InputField input)
        {
            if (decimal.TryParse(input.text, out decimal value))
                _value = value;
            else input.text = _value.ToString();
        }

        public void UpdateData(CalculateFieldData data)
        {
            _title.text = data.Title;

            if(data.Type == TypeCalculateField.Input)
            {
                _inputBody.SetActive(true);
                _outputBody.SetActive(false);

                _inputField.text = data.DefaultValue;
            }
            else
            {
                _inputBody.SetActive(false);
                _outputBody.SetActive(true);

                _textView.text = data.DefaultValue;
            }
        }
    }

    public enum TypeCalculateField
    { View, Input }
}

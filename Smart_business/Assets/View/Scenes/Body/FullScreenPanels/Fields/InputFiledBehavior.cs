using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.Fields
{

    public class InputFiledBehavior : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private InputField _inputField;

        [SerializeField]
        private Text _title;

        [SerializeField]
        private Text _description;

        [SerializeField]
        private Text _viewCountSymbols;

        [SerializeField]
        private Button _replaceButton;

        [SerializeField]
        private GameObject _contentBody;

        private TextFieldBehavior _textView;
        private ElementData _data;
        private string _startValue;
        private RectTransform _inputFieldRect;

        private void Awake()
        {
            _inputFieldRect = _inputField.GetComponent<RectTransform>();
        }

        public void UpdateData(ElementData data, TextFieldBehavior textView)
        {
            _data = data;
            _textView = textView;
            _startValue = _data.Value;
            _contentBody.SetActive(true);

            _title.text = _data.Title;
            _description.enabled = _data.IsNumber;

            _replaceButton.interactable = _inputField.interactable = data.IsEdit;
            _inputField.text = data.Value;

            if(data.IsEdit)
                _inputField.ActivateInputField();
        }

        public void ChangerValue()
        {
            _data.Value = _inputField.text;
            _inputField.text = _data.Value;
            _viewCountSymbols.text = $"{_data.Value.Length}/{_data.CountSymbols}";
            _inputFieldRect.localScale = _inputFieldRect.localScale;
        }

        public void Replace()
        {
            _inputField.text = _data.Value = _startValue;
        }

        public void Copy()
            => GUIUtility.systemCopyBuffer = _data.Value;

        public void Save()
        {
            _textView.Draw();
            _contentBody.SetActive(false);
        }
    }
}

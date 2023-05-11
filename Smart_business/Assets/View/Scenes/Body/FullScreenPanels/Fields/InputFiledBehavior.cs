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
        private RectTransform _inputFieldRect;

        private ElementData _data;
        private string _startValue;

        public void UpdateData(ElementData data)
        {
            _data = data;
            _startValue = _data.Value;
            _inputField.interactable = data.IsEdit;
            _inputField.text = data.Value;

            if(data.IsEdit)
                _inputField.ActivateInputField();
        }

        public void ChangerValue()
        {
            Debug.Log("changer set");
            _data.Value = _inputField.text;
            _inputField.text = _data.Value;
            _inputFieldRect.localScale = _inputFieldRect.localScale;
        }

        public void Replace()
        {
            _inputField.text = _data.Value = _startValue;
        }
    }
}

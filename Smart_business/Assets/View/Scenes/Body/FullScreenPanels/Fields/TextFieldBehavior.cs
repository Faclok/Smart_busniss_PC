using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.Fields
{

    [RequireComponent(typeof(RectTransform))]
    public class TextFieldBehavior : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private Text _title;

        [SerializeField]
        private Text _viewText;

        [SerializeField]
        private Button _button;

        [HideInInspector]
        public new GameObject gameObject;

        [HideInInspector]
        public new Transform transform;

        private ElementData _data;

        private InputFiledBehavior _inputField;

         
        private void Awake()
        {
            gameObject = base.gameObject;
            transform = GetComponent<RectTransform>();
        }

        public void UpdateData(ElementData data, InputFiledBehavior inputFiled)
        {
            _inputField = inputFiled;
            _data = data;
            _title.text = data.Title;
            Draw();
        }

        public void Draw()
          =>  _viewText.text = _data.Value;

        public void Click()
            => _inputField.UpdateData(_data, this);
    }
}
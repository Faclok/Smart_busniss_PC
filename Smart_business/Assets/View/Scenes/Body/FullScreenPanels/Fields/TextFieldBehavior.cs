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
        private InputField _viewText;

        [SerializeField]
        private InputFiledBehavior _inputField;

        [HideInInspector]
        public new GameObject gameObject;

        [HideInInspector]
        public new Transform transform;

        private ElementData _data;
         
        private void Awake()
        {
            gameObject = base.gameObject;
            transform = GetComponent<RectTransform>();
        }

        public void UpdateData(ElementData data)
        {
            _inputField.UpdateData(data);
            _data = data;
            _title.text = data.Title;
            Draw();
        }

        public void Replace()
        {
            if (!_data.IsEdit)
                return;

            _inputField.Replace();
        }

        public void Draw()
          =>  _viewText.text = _data.Value;
    }
}
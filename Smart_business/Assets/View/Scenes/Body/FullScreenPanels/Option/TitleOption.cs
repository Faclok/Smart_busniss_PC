using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.OptionsWindow
{
    public class TitleOption : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private Text _fieldText;

        [SerializeField]
        private Image _focus;

        public static event Action<TitleOption> Show;

        public bool isFocus { get => _focus.enabled; set { _focus.enabled = value;} }

        public void Click()
            => Show?.Invoke(this);

        private void OnDestroy()
        {
            Show = null;
        }
    }
}
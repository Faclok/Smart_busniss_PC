using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.OptionsWindow.Review
{
    public class LastActive : MonoBehaviour
    {

        [Header("UI")]
        [SerializeField]
        private Text _value;

        [SerializeField]
        private Text _date;

        [SerializeField]
        private Image _icon;

        public void UpdateIcon(Sprite icon)
            => _icon.sprite = icon;

        public void UpdateDate(string value, string date)
        {
            _value.text = value;
            _date.text = date;
        }
    }
}
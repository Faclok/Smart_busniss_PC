using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.OptionsWindow.Review
{

    public class FilterDate : MonoBehaviour
    {
        [Header("Days")]
        [SerializeField]
        private int _days;

        [SerializeField]
        private Image _background;

        [SerializeField]
        private Text _textField;



        public int CountDays => _days;

        public void On()
        {
            _background.color = Color.white;
            _textField.color = Color.black;
        }

        public void Off()
        {
            _background.color = Color.clear;
            _textField.color = Color.white;
        }
    }
}
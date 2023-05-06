using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Body.FullScreen.OptionsWindow
{
    [Serializable]
    public class OptionBlock
    {
        [Header("Data")]
        [SerializeField]
        private TitleOption _title;

        [SerializeField]
        private BodyOptionBlock _body;

        public TitleOption Title => _title;

        public BodyOptionBlock Body => _body;
    }
}
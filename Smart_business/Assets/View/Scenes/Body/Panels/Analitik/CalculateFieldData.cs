using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.View.Body.Analyst
{

    [Serializable]
    public class CalculateFieldData
    {

        [Header("Data")]
        [SerializeField]
        private string _title;

        [SerializeField]
        private TypeCalculateField _typeField;

        [SerializeField]
        private string _defaultValue = "0.00";

        public string Title => _title;

        public TypeCalculateField Type => _typeField;

        public string DefaultValue => _defaultValue;
    }
}

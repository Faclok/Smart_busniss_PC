using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.VioletSearch
{

    /// <summary>
    /// Кнопка в верхнем управлении
    /// </summary>
    [Serializable]
    public class ButtonMultiPanel
    {

        /// <summary>
        /// Имя кнопки
        /// </summary>
        [Header("Parametrs")]
        [SerializeField]
        private string _nameButton;

        /// <summary>
        /// Сама кнопка
        /// </summary>
        [SerializeField]
        private Button _button;

        /// <summary>
        /// Свойсвто имени
        /// </summary>
        public string NameButton => _nameButton;

        /// <summary>
        /// Свойтсво кнопки
        /// </summary>
        public Button Button => _button;
    }
}

using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.View.Body
{

    /// <summary>
    /// Используется для устновки или получения иконки
    /// </summary>
    [Serializable]
    public class KeyValueIcon
    {
        /// <summary>
        /// Имя иконки
        /// </summary>
        [SerializeField] private string _name;

        /// <summary>
        /// Иконка
        /// </summary>
        [SerializeField] private Sprite _icon;

        /// <summary>
        /// Свойтсво имени
        /// </summary>
        public string Name => _name;
        
        /// <summary>
        /// Свойсвто иконки
        /// </summary>
        public Sprite Icon => _icon;
    }
}

using Assets.View.Body.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// Используется для храннения данных отдела
    /// </summary>
    [CreateAssetMenu(fileName = "Item Pack", menuName = "ScriptableObject/Item in Pack Panel")]
    public class ItemInPackScriptableObject : ScriptableObject
    {

        /// <summary>
        /// Имя или титул отдела
        /// </summary>
        [Header("Данные")]
        [SerializeField] private string _title;

        /// <summary>
        /// Отдел
        /// </summary>
        [SerializeField] private PanelContent[] _panelConent;

        /// <summary>
        /// Свойство и не изменяемое, изменить можно лишь в inspector
        /// </summary>
        public string Title => _title;

        /// <summary>
        /// Свойство и не изменяемое, изменить можно лишь в inspector
        /// </summary>
        public PanelContent[] Content => _panelConent;
    }
}
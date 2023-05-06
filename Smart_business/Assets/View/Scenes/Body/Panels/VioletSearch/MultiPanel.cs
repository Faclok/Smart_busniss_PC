using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.ViewModel;
using System.Linq;

namespace Assets.View.Body.VioletSearch
{

    /// <summary>
    /// Верхняя панель управления
    /// </summary>
    public class MultiPanel : MonoBehaviour
    {
        
        /// <summary>
        /// Установленые кнопки
        /// </summary>
        [Header("Buttons")]
        [SerializeField]
        private ButtonMultiPanel[] _buttons;

        /// <summary>
        /// Обновление уровня доступа
        /// </summary>
        /// <param name="access">параметры доступа</param>
        public void UpdateData(string[] access)
        {
            var list = access.ToList();

            if (list.Contains(ManagementAssistant.AccessAll))
                return;

            foreach (var button in _buttons)
                button.Button.interactable = access.Contains(button.NameButton);
        }
    }
}
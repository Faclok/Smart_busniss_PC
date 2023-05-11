using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.View.Body.Menu
{

    public class DivPanel : MonoBehaviour 
    {

        [Header("Prefab")]
        [SerializeField]
        private ButtonPanel _prefab;

        [SerializeField]
        private Transform _content;

        [Header("Text")]
        [SerializeField]
        private TextMeshProUGUI _textField;

        public ButtonPanel UpdateData(string title, PanelContent[] contents, Transform contentPanel, string[] accessUser)
        {
            _textField.text = title;
            var array = new List<ButtonPanel>();

            for (int i = 0; i < accessUser.Length; i++)
                if (accessUser.Contains(contents[i].Title))
                {
                    var button = Instantiate(_prefab, _content, false);
                    button.PanelContent = contents[i];
                    Instantiate(contents[i], contentPanel, false);
                    array.Add(button);
                }

            return array[0];
        }
    }
}

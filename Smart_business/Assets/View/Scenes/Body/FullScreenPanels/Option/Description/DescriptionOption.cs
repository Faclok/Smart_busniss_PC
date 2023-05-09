using System.Collections.Generic;
using UnityEngine;
using Assets.View;

namespace Assets.View.Body.FullScreen.OptionsWindow.Description
{
    public class DescriptionOption : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField]
        private DescriptionElement _descriptionElement;

        [SerializeField]
        private Transform _content;

        private DescriptionElement[] _instantiateElements = new DescriptionElement[0];

        public void UpateData(string descriptionFull)
        {
            var elements = descriptionFull.Split('$');

            var items = InstantiateExtensions.GetOverwriteInstantiate(_descriptionElement, _content, _instantiateElements, elements);

            for (int i = 0; i < elements.Length; i++)
            {
                var data = elements[i].Split(';');

                if (data.Length < 3)
                {
                    items[i].UpdateData(string.Empty, string.Empty, false);
                    continue;
                }

                var title = data[0];
                var description = data[1];
                var isCopy = data[2] == bool.TrueString;
                items[i].UpdateData(title, description, isCopy);
            }

            _instantiateElements = items;
        }
    }
}
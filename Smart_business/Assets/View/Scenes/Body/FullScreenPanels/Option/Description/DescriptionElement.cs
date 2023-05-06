using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.OptionsWindow.Description
{

    public class DescriptionElement : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private Text _title;

        [SerializeField]
        private Text _description;

        [SerializeField]
        private GameObject _copyButton;

        [HideInInspector]
        public new GameObject gameObject;

        private void Awake()
        {
            gameObject = base.gameObject;
        }

        public void UpdateData(string title, string description, bool isCopy)
        {
            _title.text = title;
            _description.text = description;
            _copyButton.SetActive(isCopy);
        }

        public void Copy()
        {
            GUIUtility.systemCopyBuffer = _description.text;

            // FIX message up
        }
    }
}

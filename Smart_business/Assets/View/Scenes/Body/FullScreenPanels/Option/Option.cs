using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.View.Body.FullScreen.OptionsWindow.Review;
using Assets.View.Body.FullScreen.OptionsWindow.Description;
using Assets.View.Body.FullScreen.OptionsWindow.History;
using TMPro;
using UnityEngine.UI;
using Assets.View.Body.FullScreen.EditWindow;

namespace Assets.View.Body.FullScreen.OptionsWindow
{

    public class Option : MonoBehaviour
    {
        [Header("Title")]
        [SerializeField]
        private TextMeshProUGUI _titleField;

        [Header("Edit button")]
        [SerializeField]
        private Button _buttonEdit;

        [SerializeField]
        private GameObject _editWindow;

        [SerializeField]
        private ReviewOption _reviewOption;

        [Header("Description")]
        [SerializeField]
        private DescriptionOption _descriptionOption;

        [Header("History")]
        [SerializeField]
        private HistoryOption _historyOption;

        public static event Action OnShow;

        private OptionProperty _property;

        public void Open(OptionProperty property)
        {
            _property = property;
            _titleField.text = _property.Name;
            _reviewOption.UpdateData(_property.ReviewProperty);
            _descriptionOption.UpateData(_property.DescriptionFull);
            _historyOption.UpdateData(_property.ValuesHistory);

            OnShow?.Invoke();

            _buttonEdit.interactable = _property.EditProperty != null;
        }

        public void ClickEdit()
        {
            _editWindow.SetActive(true);
        }

        private void OnDestroy()
        {
            OnShow = null;
        }
    }
}

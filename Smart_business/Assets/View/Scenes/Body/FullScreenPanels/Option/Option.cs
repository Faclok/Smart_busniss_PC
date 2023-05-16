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
using Assets.View.Body.FullScreen.MessageTask;
using System.Threading.Tasks;

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
        private GameObject _lineRender;

        [Header("Edit")]
        [SerializeField]
        private GameObject _editWindow;

        [SerializeField]
        private Edit _edit;

        [SerializeField]
        private ReviewOption _reviewOption;

        [Header("Description")]
        [SerializeField]
        private DescriptionOption _descriptionOption;

        [Header("History")]
        [SerializeField]
        private HistoryOption _historyOption;

        private OptionProperty _property;

        public void Open(OptionProperty property)
        {
            _property = property;
            _titleField.text = _property.Name;

            _reviewOption.UpdateData(_property.ReviewProperty);
            _descriptionOption.UpateData(_property.DescriptionFull);
            _historyOption.UpdateData(_property.ValuesHistory);

            _buttonEdit.interactable = _property.EditProperty != null;
        }

        public void FirstStart()
        {
            _reviewOption.FirstStart();
        }

        public void ClickEdit()
        {
            _editWindow.SetActive(true);
            _edit.Open(_property.EditProperty, _lineRender);
        }
    }
}

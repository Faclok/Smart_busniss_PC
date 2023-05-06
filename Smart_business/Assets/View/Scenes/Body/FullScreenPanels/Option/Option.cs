using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.View.Body.FullScreen.OptionsWindow.Review;
using Assets.View.Body.FullScreen.OptionsWindow.Description;
using Assets.View.Body.FullScreen.OptionsWindow.History;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.OptionsWindow
{

    public class Option : FullScreenPanel<OptionProperty>
    {
        [Header("Title")]
        [SerializeField]
        private Text _titleField;

        [Header("Edit button")]
        [SerializeField]
        private GameObject _buttonEdit;

        [Header("Instaitiate")]
        [SerializeField]
        private TitleOption _titleOptionStart;

        [Header("Review")]
        [SerializeField]
        private TitleOption _titleReview;

        [SerializeField]
        private ReviewOption _reviewOption;

        [Header("Description")]
        [SerializeField]
        private TitleOption _titleDescription;

        [SerializeField]
        private DescriptionOption _descriptionOption;

        [Header("History")]
        [SerializeField]
        private TitleOption _titleHistory;

        [SerializeField]
        private HistoryOption _historyOption;

        public static event Action OnClose;
        public static event Action OnShow;

        private Dictionary<TitleOption, BodyOptionBlock> _blocksDictionary = new();

        private TitleOption _current;


        private void Awake()
        {
            TitleOption.Show += ClickTitle;

            _current = _titleOptionStart;

            _blocksDictionary.Add(_titleReview, _reviewOption);
            _blocksDictionary.Add(_titleDescription, _descriptionOption);
            _blocksDictionary.Add(_titleHistory, _historyOption);
        }


        public override void OpenWindow()
        {
            _titleField.text = _property.Name;
            _reviewOption.UpdateData(_property.ReviewProperty);
            _descriptionOption.UpateData(_property.DescriptionFull);
            _historyOption.UpdateData(_property.ValuesHistory);

            OnShow?.Invoke();
            _titleOptionStart.Click();

            _buttonEdit.SetActive(_property.EditProperty != null);
        }

        public void ClickEdit()
        {
            if(_property.EditProperty != null) 
                FullScreenPanels.OpenEditData(_property.EditProperty);
        }

        private void ClickTitle(TitleOption title)
        { 
           title.isFocus = !(_current.isFocus = false);
            _current = title;

            _blocksDictionary[title].Focus();
        }

        public override void Close()
        {
            OnClose?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            OnClose = null;
            OnShow = null;
        }
    }
}

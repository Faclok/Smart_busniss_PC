using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.ViewModel.Datas;
using Assets.ViewModel;

namespace Assets.View.Body.Profile.Version
{

    public class VersionControll : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private Text _textVersionCurrent;

        [SerializeField]
        private Text _textVersionNext;

        [Header("Bug fix")]
        [SerializeField]
        private GameObject _bugFix;

        [SerializeField]
        private Text _textBugFix;

        [Header("Description")]
        [SerializeField]
        private GameObject _description;

        [SerializeField]
        private Text _textDescription;

        private async void Awake()
        {
            var datas = await ModelDatabase.GetUniqueObjectAsync<VersionApplication>(VersionApplication.TABLE);

            var data = datas[^1];
            _textVersionCurrent.text = Application.version;
            _bugFix.SetActive(false);
            _description.SetActive(false);

            if (Application.version == data.Columns["version"])
                _textVersionNext.text = "thank you for using the latest version";
            else
            {
                _textVersionNext.text ="update version to "+ data.Columns["version"];
                if (data.Columns["bugFix"] != string.Empty)
                {
                    _bugFix.SetActive(true);
                    _textBugFix.text = data.Columns["bugFix"];
                }
                if (data.Columns["description"] != string.Empty)
                {
                    _description.SetActive(true);
                    _textDescription.text = data.Columns["description"];
                }
            }
        }
    }
}
using Assets.View.Body.ItemsAnimationLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.OptionsWindow.Review
{
    public class HelperAI : MonoBehaviour
    {

        [Header("Tet Field")]
        [SerializeField]
        private Text _prognozField;

        [SerializeField]
        private Text _offerField;

        [Header("Requests")]
        [TextArea(1,3)]
        [SerializeField]
        private string _prognozRequest;

        [Space(15)]
        [TextArea(1, 3)]
        [SerializeField]
        private string _offerRequest;

        [Header("Words")]
        [SerializeField]
        private string _dateTimeNowReplace;

        [SerializeField]
        private string _dateStartTimeReplace;

        [SerializeField]
        private string _valueTaskReplace;

        [Header("Animations")]
        [SerializeField]
        private ItemAnimation _upperAnimation;

        public string Prognoz { get; private set; } = string.Empty;

        public string Offer { get; private set; } = string.Empty;

        private DateTime _activeTime;

        private void Start()
        {
            MoveDate.OnDateChanged += UpdateDate;
            MoveDate.OnTaskCompleted += UpdateText;
        }

        private void UpdateDate(DateTime start)
        {
            _activeTime = start;

            _upperAnimation.gameObject.SetActive(true);
            _upperAnimation.Play();
        }

        private async void UpdateText(float[] data)
        {
            if (false)
            {
                var prognoz = _prognozRequest.Replace(_valueTaskReplace, string.Join("U+002C", data));
                prognoz = prognoz.Replace(_dateTimeNowReplace, $"{DateTime.Today:d}");
                prognoz = prognoz.Replace(_dateStartTimeReplace, $"{_activeTime: d}");

                var offer = _offerRequest.Replace(_valueTaskReplace, string.Join("U+002C", data));
                offer = offer.Replace(_dateTimeNowReplace, $"{DateTime.Today:d}");
                offer = offer.Replace(_dateStartTimeReplace, $"{_activeTime: d}");

                var answerPrognoz = ChatGPT.GetAnswer(prognoz);
                var answerOffer = ChatGPT.GetAnswer(offer);

                await Task.WhenAll(answerPrognoz, answerOffer);

                _prognozField.text = answerPrognoz.Result;
                _offerField.text = answerOffer.Result;

            } //FIX
            _upperAnimation.gameObject.SetActive(false);
        }
    }
}

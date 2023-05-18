using Assets.View.Body.FullScreen.MessageTask;
using Assets.ViewModel;
using Assets.ViewModel.PullDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.Analyst
{
    public class AnalystControllChangerPrice : MonoBehaviour
    {

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI _titleActive;

        [SerializeField]
        private TMP_InputField _inputField;

        [SerializeField]
        private TextMeshProUGUI _buttonText;

        [SerializeField]
        private TextMeshProUGUI _titleId;

        [SerializeField]
        private TextMeshProUGUI _priceChanger;

        [SerializeField]
        private TextMeshProUGUI _priceChangerProcent;

        [Header("Body")]
        [SerializeField]
        private GameObject _inputBody;

        [SerializeField]
        private GameObject _changerBody;

        [SerializeField]
        private Button _button;

        [Header("Setting")]
        [SerializeField]
        private string _completed;

        [SerializeField]
        private string _back;

        [SerializeField]
        private string _focusItem;

        private decimal _priceValue = 0;

        private AnalystItemGraphic _itemFocus;

        private Action _updateAction;

        public void Replace()
        {
            _changerBody.SetActive(false);
            _inputBody.SetActive(true);
            _titleActive.text = _focusItem;
            _inputField.text = "0";
            _priceValue = 0;
            _buttonText.text = _completed;
            _button.interactable = false;
            _itemFocus = null;
        }

        public void UpdateData(Action update)
        {
            _updateAction = update;
        }

        public void Focus(AnalystItemGraphic item)
        {
            Replace();
            _itemFocus = item;
            _titleActive.text = string.Join(" ", item.Title,item.Description);
        }

        public void UpdateText(TMP_InputField inputField)
        {
            if (decimal.TryParse(inputField.text, out decimal parse)) 
                _priceValue = parse;
            else inputField.text = _priceValue.ToString();

            _button.interactable = _itemFocus != null && _priceValue > 0M;
        }

        public void Click()
        {
            if(_buttonText.text ==  _completed)
            {
                _inputBody.SetActive(false);
                _changerBody.SetActive(true);  

                _titleId.text = $"{_itemFocus.Data["id"]} (id)";
                _priceChanger.text = _priceValue.ToString();

                var priceCurrent = decimal.Parse(_itemFocus.Data.Price);
                var priceNew = priceCurrent + _priceValue;

                var spacingPrice = priceNew - priceCurrent;

                _priceChangerProcent.text = (Math.Round((spacingPrice / priceCurrent) * 100M),2).ToString();

                _buttonText.text = _back;
            }
            else
            {
                _changerBody.SetActive(false);
                _inputBody.SetActive(true);

                _buttonText.text = _completed;
            }
        }

        public void SetPrice(bool isUp)
        {
            MessageView.ShowTask("changer price object?", () => CreatChangerServer(isUp), UpdateChanger);
        }

        private void UpdateChanger()
        {
            AnalystControll.UpdateDatasOnChangers();
            _updateAction();
            Replace();
        }

        private async Task CreatChangerServer(bool isUp)
        {
            var changer = new PriceChangePull();

            changer["id"] = null;
            changer[PriceChangePull.COLUMN_LINK] = _itemFocus.Data["id"];
            changer[PriceChangePull.COLUMN_DATE] = $"{DateTime.Now:yyyy.MM.dd HH.mm.ss}";
            changer["state"] = isUp ? "up" : "down";
            changer["priceNew"] = (decimal.Parse(_itemFocus.Data.Price) + (isUp ? _priceValue : -_priceValue)).ToString();
            changer["pricePrev"] = decimal.Parse(_itemFocus.Data.Price).ToString();

            await Task.Run(async () => await ModelDatabase.UpdateObject(_itemFocus.Data, new() { ["price"] = changer["priceNew"] }));
            await Task.Run(async() =>  await ModelDatabase.CreatObject(changer));
        }
    }
}

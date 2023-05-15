using Assets.View.Body.FullScreen.CreatWindow;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.View.Body.FullScreen.Fields;
using UnityEngine;
using ProductData = Assets.ViewModel.Datas.Product;
using Assets.View.Body.Menu;

namespace Assets.View.Body.Product
{

    public class CreatProduct : PanelContent
    {

        [Header("Link")]
        [SerializeField]
        private Creat _creat;

        private ElementData _dataNameMachine;

        private void Start()
        {
            var newMachine = new ProductData();

            newMachine["id"] = "auto-ganerate";
            newMachine["name"] = "default product";
            newMachine["dataSet"] = $"{DateTime.Now:yyyy.MM.dd}";
            newMachine["amount"] = "0";

            var property = new CreatProperty(newMachine, new ElementData[]
            {
                new ElementData("ID","id",newMachine["id"], isEdit: false, countSimbols: 7, isNumber:true),
                _dataNameMachine = new ElementData("name","name",newMachine["name"],isEdit: true,countSimbols:20,isNumber: false),
                new ElementData("Price", "price", "0", true, 7, true),
                new ElementData("description","description","write description",isEdit:true,countSimbols: 1000, isNumber:false)
            }
            , ProductControll.UpdateDatasOnChangers, GetQuestion);

            _creat.Open(property);
        }

        private string GetQuestion()
            => $"creat machine '{_dataNameMachine.Value}'?";
    }

}

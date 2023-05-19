using Assets.View.Body.FullScreen.CreatWindow;
using Assets.View.Body.Menu;
using System;
using UnityEngine;
using Assets.View.Body.FullScreen.Fields;
using ObjectStock = Assets.ViewModel.Datas.ObjectInStock;

namespace Assets.View.Body.Stock
{

    public class CreatStock : PanelContent
    {

        [Header("Link")]
        [SerializeField]
        private Creat _creat;

        private ElementData _dataNameStock;

        private void Start()
        {
            var newMachine = new ObjectStock();

            newMachine["id"] = "auto-ganerate";
            newMachine["name"] = "default machine";
            newMachine["dataSet"] = $"{DateTime.Now:yyyy.MM.dd}";
            newMachine["icon"] = "deafult";
            newMachine["typeMeasure"] = "elements";
            newMachine["valueMeasure"] = "1";
            newMachine["amount"] = "0";

            var property = new CreatProperty(newMachine, new ElementData[]
            {
                new ElementData("ID","id",newMachine["id"], isEdit: false, countSimbols: 7, isNumber:true),
                _dataNameStock = new ElementData("name","name",newMachine["name"],isEdit: true,countSimbols:20,isNumber: false),
                new ElementData("icon","icon",newMachine["icon"],isEdit: true, countSimbols:35, isNumber:false),
                new ElementData("amount","amount",newMachine["amount"],isEdit:false, countSimbols:1,isNumber:true),
                new ElementData("description","description","write description",isEdit:true,countSimbols: 1000, isNumber:false)
            }
            , StockControll.UpdateDatasOnChangers, GetQuestion);

            _creat.Open(property);
        }

        private string GetQuestion()
            => $"creat stock '{_dataNameStock.Value}'?";
    }
}

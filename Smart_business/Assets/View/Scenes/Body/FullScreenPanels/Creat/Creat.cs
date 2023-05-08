using Assets.MultiSetting;
using Assets.View.Body.FullScreen.Fields;
using Assets.ViewModel;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Assets.View.Body.FullScreen.MessageTask;

namespace Assets.View.Body.FullScreen.CreatWindow
{
    public class Creat : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private ControllField _controllField;

        [HideInInspector]
        public new GameObject gameObject;

        private CreatProperty _property;

        private void Awake()
        {
            gameObject = base.gameObject;
        }

        public void Open(CreatProperty property)
        {
            _property = property;
            _controllField.UpdateData(_property.ElementDatas);
        }

        public void SaveAndCreat()
        {
        }

        private Task ServerRequest()
        {
            var updateColumns = _property.ItemCreat.Columns;
            var updateLocal = _controllField.SaveProperty();

            foreach (var item in updateLocal)
                updateColumns[item.Key] = item.Value;

            _property.ItemCreat.Columns["id"] = null;
            _property.UpdateOnChanger();

            return Task.Run(()=> ModelDatabase.CreatObject(_property.ItemCreat));
        }
    }
}

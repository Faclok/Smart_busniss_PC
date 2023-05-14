using Assets.View.Body.FullScreen.Fields;
using Assets.ViewModel;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Assets.View.Body.Menu;
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

        public void Open(CreatProperty property)
        {
            _property = property;
            _controllField.UpdateData(_property.ElementDatas);
        }

        public void SaveAndCreat()
        {
            MessageView.ShowTask(_property.Question, ServerRequest, Replace);
        }

        public void Replace()
        {
            _controllField.Replace();
        }

        private async Task ServerRequest()
        {
            var updateColumns = _property.ItemCreat.Columns;
            var updateLocal = _controllField.SaveProperty();

            foreach (var item in updateLocal)
                updateColumns[item.Key] = item.Value;

            _property.ItemCreat.Columns["id"] = null;

            await Task.Run(()=> ModelDatabase.CreatObject(_property.ItemCreat));

            _property.UpdateOnChanger();
        }
    }
}

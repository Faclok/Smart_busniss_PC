using Assets.MultiSetting;
using Assets.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.View.Body.FullScreen.Fields;
using System.Threading.Tasks;
using Assets.View.Body.FullScreen.MessageTask;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.EditWindow
{
    public class Edit : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private ControllField _controllField;

        [SerializeField]
        private Button _buttonDelete;

        private EditProperty _property;

        private GameObject _lineRender;

        public void Open(EditProperty property, GameObject linerender = null)
        {
            _lineRender = linerender;
            linerender?.SetActive(false);

            _buttonDelete.interactable = property.IsDelete;

            _property = property;
            _controllField.UpdateData(_property.Elements);
        }

        public void Save()
        {
            MessageView.ShowTask(_property.Question, SaveServer, Close);
        }

        public void Close()
        {
            _lineRender?.SetActive(true);
            gameObject.SetActive(false);
        }

        public void Delete()
        {
            MessageView.ShowTask($"delete object to ID = {_property.Item.Id}?", DeleteServer, Close);
        }

        private async Task DeleteServer()
        {
            await ModelDatabase.DeleteObject(_property.Item);

            _property.UpdateOnChanger();
        }

        private async Task SaveServer()
        {
            var updateColumns = new Dictionary<string, string>();
            var updateLocal = _controllField.SaveProperty();

            foreach (var item in updateLocal)
                updateColumns.Add(item.Key, item.Value);
         
            await Task.Run(()=> ModelDatabase.UpdateObject(_property.Item, updateColumns));

            foreach (var item in updateColumns)
                _property.Item.Columns[item.Key] = item.Value;

            _property.UpdateOnChanger();
        }
    }
}

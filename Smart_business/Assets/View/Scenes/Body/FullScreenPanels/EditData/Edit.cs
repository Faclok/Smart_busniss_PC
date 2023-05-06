using Assets.MultiSetting;
using Assets.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.View.Body.FullScreen.Fields;
using System.Threading.Tasks;
using Assets.View.Body.FullScreen.MessageTask;

namespace Assets.View.Body.FullScreen.EditWindow
{
    public class Edit : FullScreenPanel<EditProperty>
    {
        [Header("Links")]
        [SerializeField]
        private ControllField _controllField;

        [SerializeField]
        private GameObject _deleteButton;

        [SerializeField]
        private MessageView _messageView;

        public override void OpenWindow()
        {
            _controllField.UpdateData(_property.Elements);
            _deleteButton.SetActive(_property.IsDelete);
        }

        public void Save()
        {
            _messageView.Show("Changer save?", SaveServer, Close);
        }

        private Task SaveServer()
        {
            var updateColumns = new Dictionary<string, string>();
            var updateLocal = _controllField.SaveProperty();

            foreach (var item in updateLocal)
                updateColumns.Add(item.Key, item.Value);

            _property.UpdateOnChanger();
            return Task.Run(()=> ModelDatabase.UpdateObject(_property.Item, updateColumns));
        }

        public void Delete()
        {
            _messageView.Show("delete object?", DeleteServer, Close);
        }

        private Task DeleteServer()
        {
            _property.UpdateOnChanger();
             return ModelDatabase.DeleteObject(_property.Item);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}

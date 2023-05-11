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
    public class Edit : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private ControllField _controllField;

        private EditProperty _property;

        public void Open(EditProperty property)
        {
            _property = property;
            _controllField.UpdateData(_property.Elements);
        }

        public void Save()
        {
            SaveServer();
        }

        public void Close()
        {
            _controllField.Replace();
            gameObject.SetActive(false);
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
    }
}

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
    public class Creat : FullScreenPanel<CreatProperty>
    {
        [Header("Links")]
        [SerializeField]
        private ControllField _controllField;

        [HideInInspector]
        public new GameObject gameObject;

        [SerializeField]
        private MessageView _messageView;

        private void Awake()
        {
            gameObject = base.gameObject;
        }

        public override void OpenWindow()
        {
            _controllField.UpdateData(_property.ElementDatas);
        }

        public void SaveAndCreat()
        {
            _messageView.Show("creat object?",ServerRequest,Close);
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

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}

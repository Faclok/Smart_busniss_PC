using Assets.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.ViewModel.PullDatas;
using System;
using UnityEngine.UI;
using System.Threading.Tasks;
using Assets.MultiSetting;
using Assets.View.Body.FullScreen.MessageTask;

namespace Assets.View.Body.Profile.Login
{

    public class ControllLogins : MonoBehaviour
    {

        [Header("Links")]
        [SerializeField]
        private LoginBehaviour _currentLogin;

        [SerializeField]
        private LoginBehaviour _prefab;

        [SerializeField]
        private Transform _content;

        [SerializeField]
        private Button _updateButton;

        private static ControllLogins _singleton;

        private LoginBehaviour[] _instantiateBehaviour = new LoginBehaviour[0];

        private LoginPull _deleteCurrent;

        private void Awake()
        {
            _singleton = this;
        }

        private void Start()
        {
            ClickUpdate();
        }

        public void Open()
        {
            gameObject.SetActive(true);

            ClickUpdate();
        }

        public void ClickUpdate()
        {
            _updateButton.interactable = false;

            Task.Run(() =>ModelDatabase.GetObjecyWhere<LoginPull>(LoginPull.TABLE, new Dictionary<string, string>() { ["id"] = ManagementAssistant.ActiveLast.CodeLogin}).GetTaskCompleted(UpdateDataCurrent));

           var task = Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<LoginPull>(LoginPull.TABLE, LoginPull.COLUMN_LINK, ManagementAssistant.Profile, LoginPull.COLUMN_DATE, DateTime.MinValue, DateTime.Now));
            task.GetTaskCompleted(UpdateData);
        }

        private void UpdateDataCurrent(LoginPull[] current)
        {
            _currentLogin.UpdateData(current[0]);
        }

        private  void UpdateData(LoginPull[] data)
        {
            _updateButton.interactable = true;

            var tempData = new List<LoginPull>();

            for (int i = 0; i < data.Length; i++)
                if (data[i]["id"] != ManagementAssistant.ActiveLast.CodeLogin)
                    tempData.Add(data[i]);

            data = tempData.ToArray();

            var newArray = InstantiateExtensions.GetOverwriteInstantiate(_prefab, _content, _instantiateBehaviour, data);

            for (int i = 0; i < newArray.Length; i++)
                newArray[i].UpdateData(data[i]);

            _instantiateBehaviour = newArray;
        }

        public static void DeleteLogin(LoginPull loginPull)
            => _singleton.Delete(loginPull);

        public void Delete(LoginPull loginPull)
        {
            _deleteCurrent = loginPull;
            MessageView.ShowTask("disable?",DeleteServer,Close);
        }

        private  Task DeleteServer()
           =>  LoginPull.DisableLogin(_deleteCurrent);

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _singleton = null;
        }
    }
}

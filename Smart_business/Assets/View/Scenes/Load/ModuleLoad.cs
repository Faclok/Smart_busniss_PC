using UnityEngine;
using System.Threading.Tasks;

using Assets.MultiSetting;
using Assets.ViewModel;


namespace Assets.View.Load
{

    /// <summary>
    /// ������ �������� ����������
    /// </summary>
    [RequireComponent(typeof(ModuleUI))]
    public class ModuleLoad : MonoBehaviour
    {

        /// <summary>
        /// ��� �� �� �������� �������� � UI
        /// </summary>
        private ModuleUI _moduleUI;

        /// <summary>
        /// ������ �����������
        /// </summary>
        private Task<Result> _connectingDB;

        /// <summary>
        /// ������ �������� �����������
        /// </summary>
        private Task<Result> _loadAccount;

        /// <summary>
        /// ����������� �������
        /// </summary>
        private void Awake()
        {
            _moduleUI = GetComponent<ModuleUI>();
        }

        /// <summary>
        /// ������ ����
        /// </summary>
        private void Start()
        {
            Load();
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        private void Load()
        {
            _connectingDB = Task.Run(() => { return ModelDatabase.ConnectingAsync(); });
            ManagementAssistant.LoadConfigs();
        }

        /// <summary>
        /// ������ �� ������ Reset
        /// </summary>
        public void ClickButtonReset()
        {
            _moduleUI.OnUpdate();

            _connectingDB = Task.Run(() => { return ModelDatabase.ConnectingAsync(); });
        }

        /// <summary>
        /// ������������ � Animation Event
        /// </summary>
        private void CheckLoad()
        {
            if (_connectingDB == null || !_connectingDB.IsCompleted)
                return;

            var data = _connectingDB.Result;

            if (!data)
            {
                _moduleUI.OnFailed(data.TypeException ?? TypeException.LogicApplication);
                return;
            }

            if (ManagementAssistant.ActiveLast == null || string.IsNullOrWhiteSpace(ManagementAssistant.ActiveLast.Login))
            {
                _moduleUI.OnLogin(isAccount: false);
                return;
            }

            if (_loadAccount == null)
            {
                var last = ManagementAssistant.ActiveLast;
                _loadAccount = Task.Run(() => { return ManagementAssistant.LoginJsonAsync(last); });
            }
            else if (_loadAccount.IsCompleted)
                _moduleUI.OnLogin(_loadAccount.Result);
            
        }

    }
}

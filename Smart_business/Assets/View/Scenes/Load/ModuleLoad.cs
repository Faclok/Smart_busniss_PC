using UnityEngine;
using System.Threading.Tasks;

using Assets.MultiSetting;
using Assets.ViewModel;


namespace Assets.View.Load
{

    /// <summary>
    /// Модуль загрузки приложения
    /// </summary>
    [RequireComponent(typeof(ModuleUI))]
    public class ModuleLoad : MonoBehaviour
    {

        /// <summary>
        /// Что бы не работать напрямую с UI
        /// </summary>
        private ModuleUI _moduleUI;

        /// <summary>
        /// Задача подключения
        /// </summary>
        private Task<Result> _connectingDB;

        /// <summary>
        /// Задача загрузки пользвателя
        /// </summary>
        private Task<Result> _loadAccount;

        /// <summary>
        /// Пробуждение объекта
        /// </summary>
        private void Awake()
        {
            _moduleUI = GetComponent<ModuleUI>();
        }

        /// <summary>
        /// Первый кадр
        /// </summary>
        private void Start()
        {
            Load();
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        private void Load()
        {
            _connectingDB = Task.Run(() => { return ModelDatabase.ConnectingAsync(); });
            ManagementAssistant.LoadConfigs();
        }

        /// <summary>
        /// Нажаие на кнопку Reset
        /// </summary>
        public void ClickButtonReset()
        {
            _moduleUI.OnUpdate();

            _connectingDB = Task.Run(() => { return ModelDatabase.ConnectingAsync(); });
        }

        /// <summary>
        /// Используется в Animation Event
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

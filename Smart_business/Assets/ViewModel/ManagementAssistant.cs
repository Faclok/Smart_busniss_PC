using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Assets.Model;
using Assets.MultiSetting;
using Assets.Model.RequestData;
using Assets.ViewModel.Datas;
using Assets.ViewModel.PullDatas;
using UnityEngine;

namespace Assets.ViewModel
{
    
    /// <summary>
    /// Помощник с работой сервера и приложения
    /// </summary>
    public static class ManagementAssistant
    {
        // Отдел для профиля
        #region Profile

        /// <summary>
        /// Активный профиль
        /// </summary>
        public static Account Profile { get; private set; }

        /// <summary>
        /// Означает что у пользователя есть все права
        /// </summary>
        public const string AccessAll = "all";

        /// <summary>
        /// Доступ текущего аккаунт
        /// </summary>
        public static Dictionary<string, string[]> AccessAccount { get; private set; }

        /// <summary>
        /// параметры текущего аккаунта
        /// </summary>
        public static Dictionary<string, string> PropertyAccount { get; private set; }

        /// <summary>
        /// Вход в аккаунт, после удачного входа, активый аккаунт автоматически перейдет в свойство Profile
        /// </summary>
        /// <param name="login">Логин для входа</param>
        /// <param name="password">Пароль для входа</param>
        /// <returns></returns>
        public static async Task<Result> LoginAsync(string login, string password, bool isSave)
        {
            var keyParias = Account.ColumnsCreat;
            keyParias["Login"] = login;
            keyParias["password"] = password;

            var requestProperty = new RequestWhereProperty<Account>("login", keyParias, Account.TableContains);
            var data = await Server.ActionResultOfData<RequestWhereProperty<Account>, Account>(requestProperty);

            if (!data)
                return new(exception: "not account", TypeException.LogicApplication);

            Profile = data.Value[0];

            foreach (var item in _configPack.JsonConfigs)
                if (item.Login == login)
                {
                    item.Password = password;
                    _configPack.ActiveLast = item;

                    break;
                }

            AccessAccount = Profile.Access;
            PropertyAccount = Profile.Property;

            var newLoginDevice = new LoginPull();

            newLoginDevice["id"] = null;
            newLoginDevice["codeLogin"] = LoginPull.ACTIVE;
            newLoginDevice["idUser"] = Profile["id"];
            newLoginDevice["readingTime"] = $"{DateTime.Now:yyyy.MM.dd HH:mm}";
            newLoginDevice["nameMachine"] = $"MachineName: {Environment.MachineName}\nUserName {Environment.UserName}";
            newLoginDevice["os"] = $"OS: {Environment.OSVersion}";

            newLoginDevice = await ModelDatabase.CreatObjectUpdateLocalData(newLoginDevice);
            _configPack.ActiveLast = new JsonConfig(login, password, newLoginDevice["id"]);

            return new Result();
        }

        public static async Task<Result> LoginJsonAsync(JsonConfig config)
        {
            var keyParias = Account.ColumnsCreat;
            keyParias["Login"] = config.Login;
            keyParias["password"] = config.Password;

            var requestProperty = new RequestWhereProperty<Account>("login", keyParias, Account.TableContains);
            var data = await Server.ActionResultOfData<RequestWhereProperty<Account>, Account>(requestProperty);

            if (!data)
                return new(exception: "not account", TypeException.LogicApplication);

            var dataLogin = await ModelDatabase.GetObjecyWhere<LoginPull>(LoginPull.TABLE, new Dictionary<string, string>() { ["id"] = config.CodeLogin});

            if (dataLogin.Length <= 0)
                return new(exception: "not found active account",TypeException.LogicApplication);

            if (dataLogin[0]["codeLogin"] == LoginPull.DISABLED)
                return new(exception: "not active account login", TypeException.LogicApplication);

            Profile = data.Value[0];

            foreach (var item in _configPack.JsonConfigs)
                if (item.Login ==config.Login)
                {
                    item.Password = config.Password;
                    _configPack.ActiveLast = item;

                    break;
                }

            AccessAccount = Profile.Access;
            PropertyAccount = Profile.Property;

           _configPack.ActiveLast = config;

            return new Result();
        }

        /// <summary>
        /// Выход с аккаунта
        /// </summary>
        /// <returns></returns>
        public static async Task<Result> QuitAccount()
        {
            await LoginPull.DisableLogin((await ModelDatabase.GetObjecyWhere<LoginPull>(LoginPull.TABLE, new Dictionary<string, string>() { ["id"] = ActiveLast.CodeLogin }))[0]);
            Profile = null;
            AccessAccount = null;
            PropertyAccount = null;
            _configPack.ActiveLast = null;

            _configPack.ToJson();

            return new Result();
        }

        #endregion

     
        // Отдел для работы с пакетом данных 
        #region Config

        /// <summary>
        /// Имя файл config
        /// </summary>
        private const string _nameConfigFile = "ConfigSmart";

        /// <summary>
        /// Пак пользователей для сохранения
        /// </summary>
        private static ConfigPack _configPack = new();

        public static JsonConfig ActiveLast => _configPack.ActiveLast; 

        /// <summary>
        /// Загрузка пользователей из устройства
        /// </summary>
        public static void LoadConfigs()
        {
            _configPack = ApplicationJson.FromJson<ConfigPack>(_nameConfigFile);

            _configPack.JsonConfigs ??= new();
        }

        public static void UnLoadConfigs()
        {
            _configPack.ToJson();
        }

        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns></returns>
        public static JsonConfig[] GetJsonConfigs()
           => _configPack.JsonConfigs.ToArray();

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="config">Перед тем как добавить проверяет, существует ли он</param>
        public static void AddJsonConfig(JsonConfig config)
        {
            var list = _configPack.JsonConfigs;
            var isUpdate = false;

            foreach (var item in list)
                if (item.Login == config.Login)
                {
                    item.Login = config.Login;
                    isUpdate = true;

                    break;
                }

            if(!isUpdate)
               list.Add(config);

            _configPack.ToJson();
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="config"></param>
        public static void RemoveJsonConfig(JsonConfig config)
        {
            var list = _configPack.JsonConfigs;

            if (!list.Contains(config))
                return;

            list.Remove(config);

            _configPack.ToJson();
        }

        /// <summary>
        /// Пак используемый для сохраннения данных
        /// </summary>
        [Serializable]
        private class ConfigPack : IJsonData
        {
            public string NameKey => _nameConfigFile;

            /// <summary>
            /// Последний зайденный аккаунт
            /// </summary>
            public JsonConfig ActiveLast;

            /// <summary>
            /// Сохраненные данные пользоватлей
            /// </summary>
            public List<JsonConfig> JsonConfigs;
        }

        #endregion
    }
}

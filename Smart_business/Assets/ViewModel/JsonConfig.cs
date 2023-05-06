using Assets.ViewModel.PullDatas;
using System;
using UnityEngine;

namespace Assets.ViewModel
{
    /// <summary>
    /// Класс для сохраннения данных пользователя
    /// </summary>
    [Serializable]
    public class JsonConfig
    {
        public string Login;

        public string Password;

        public string CodeLogin;

        public JsonConfig() { }

        public JsonConfig(string login, string password, string codeLogin)
        {
            Login = login;
            Password = password;
            CodeLogin = codeLogin;
        }
    }
}

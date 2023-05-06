using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

using Assets.MultiSetting;

namespace Assets.Model
{

    /// <summary>
    /// Отвечает за соеденнение с сервером
    /// </summary>
    public static partial class Server
    {

        /// <summary>
        /// Подключение к sql
        /// </summary>
        private static MySqlConnection _connection;

        /// <summary>
        /// Сторка с параметрами подключения
        /// </summary>
        private const string _connectionProperties = "server=rc1b-n4kvp3yklxkjhnvz.mdb.yandexcloud.net;port=3306;user=Kvantorianec;database=SmartBusinessDB;password=uzer1pass;CharSet=utf8";

        /// <summary>
        /// Подключение к серверу
        /// </summary>
        /// <returns>Задача о подключении</returns>
        public static async Task<Result> ConnectedAsync()
        {
            if (!isConnectNetwork)
                return new(exception: "not internet", TypeException.NotNetwork);

            _connection = new MySqlConnection(_connectionProperties);

            try
            {
                await _connection.OpenAsync();
            }
            catch (MySqlException sql)
            {
                return new(exception: "not connected sql: "+ sql.Message, TypeException.DisconnectedServer);
            }
            catch (Exception ex) 
            {
                return new(exception: ex.Message, TypeException.SystemFailed);
            }

            return new();
        }
    }

  
}


using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using System.Linq;

using Assets.MultiSetting;
using Assets.Model.RequestData;
using System;
using UnityEngine;

namespace Assets.Model
{

    /// <summary>
    /// Часть класса для запросов sql
    /// </summary>
    public static partial class Server
    {
        public static async Task<ResultOf<TResult[]>> ActionResultOfData<TProperty, TResult>(TProperty property)
            where TProperty : PropertyRequest, IActionResultOf<TResult>
            where TResult : class, IItemDatabase, new()
        {
            if (_connection.State != ConnectionState.Open)
                return new(exception: "connected close server", TypeException.DisconnectedServer);

            try
            {
                using var connection = new MySqlConnection(_connectionProperties);
                await connection.OpenAsync();

                using var command = new MySqlCommand(property.Request, connection);

                using var read = await command.ExecuteReaderAsync();

                var datas = new List<TResult>();

                while (read.Read())
                {
                    var data = new TResult();
                    datas.Add(data);

                    var keys = data.Columns.Keys.ToArray();

                    for (int i = 0; i < keys.Length; i++)
                        data.Columns[keys[i]] = read.GetString(keys[i]);
                }

                return new ResultOf<TResult[]>(datas.ToArray());
            }
            catch (Exception exp)
            {
                return new ResultOf<TResult[]>(exception: $"{property.Name} [{property.Type.Name}]:{exp.Message}", TypeException.DisconnectedServer);
            }
        }

        public static async Task<Result> ActionResultAsync<T>(T property)
            where T: PropertyRequest, IActionResult
        {

            if (_connection.State != ConnectionState.Open)
                return new(exception: "connected close server", TypeException.DisconnectedServer);

            try
            {
                using var connection = new MySqlConnection(_connectionProperties);
                await connection.OpenAsync();
                using var command = new MySqlCommand(property.Request, connection);
                using var read = await command.ExecuteReaderAsync();
            }
            catch (Exception exp)
            {
                return new Result(exception: $"{property.Name}: {exp.Message}", TypeException.DisconnectedServer);
            }

            return new Result();
        }

        public static async ValueTask<string> GetValue(string request)
        {
            if (_connection.State != ConnectionState.Open)
                return string.Empty;

            try
            {
                using var connection = new MySqlConnection(_connectionProperties);
                await connection.OpenAsync();

                using var command = new MySqlCommand(request, connection);

                using var read = await command.ExecuteReaderAsync();

                return read.HasRows && read.Read() ? read.GetString(0) : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}

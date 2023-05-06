using System;
using System.Collections.Generic;
using Assets.MultiSetting;

namespace Assets.Model.RequestData
{

    /// <summary>
    /// Запрос к серверу, для параметров выгрузки
    /// </summary>
    /// <typeparam name="T">Тип возращаемого объекта</typeparam>
    public class RequestWhereProperty<T> : PropertyRequest, IActionResultOf<T>
        where T : class, IItemDatabase, new()
    {
        public string Name { get; private set; }

        public Type Type { get; private set; }

        /// <summary>
        /// Столбики данных из sql
        /// </summary>
        public readonly Dictionary<string, string> Columns;

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public readonly string Table;

        public RequestWhereProperty(string name,Dictionary<string, string> columns, string table)
            :base($"SELECT * FROM {table} WHERE {GetWhere(columns)}")
        {
            Name = name;
            Type = typeof(T);
            Columns = columns;
            Table = table;
        }

        private static string GetWhere(Dictionary<string,string> columns)
        {
            var columnsContains = new Dictionary<string, string>();

            foreach (var item in columns)
                if (item.Value != "")
                    columnsContains.Add(item.Key, item.Value);

            string where = string.Empty;

            foreach (var item in columnsContains)
                where += $"{item.Key} = '{item.Value}' AND ";

            return where.Remove(where.Length - 4);
        }
    }
}

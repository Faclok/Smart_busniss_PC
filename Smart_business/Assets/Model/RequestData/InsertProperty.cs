using UnityEngine;
using Assets.MultiSetting;

namespace Assets.Model.RequestData
{

    /// <summary>
    /// Класс для добавление элементов в sql
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InsertProperty<T> : PropertyRequest, IActionResult
        where T: class, IItemDatabase
    {
        public const string NULL = "null";

        public string Name { get; private set; }

        /// <summary>
        /// Элементы которые будут добавлены в sql, и с помощью интерфейса IItemDatabase,
        /// будет присвоено значение Id
        /// </summary>
        public readonly T Value;

        public InsertProperty(string name,T value)
            : base ($"INSERT INTO {value.Table} ({GetColumns(value)}) VALUES ({GetValues(value)});")
        {
            Name = name;
            Value = value;
        }

        private static string GetColumns(T value)
        {
            string columns = string.Empty;

            foreach (var keyValuePair in value.Columns)
                columns += keyValuePair.Key + ", ";

            columns = columns.Remove(columns.Length - 2);
            return columns;
        }

        private static string GetValues(T value)
        {
            string values = string.Empty;

            foreach (var keyValuePair in value.Columns)
                values += keyValuePair.Value != null? "'" + keyValuePair.Value + "', ": NULL +", ";

            values = values.Remove(values.Length - 2);
            return values;
        }
    }
}

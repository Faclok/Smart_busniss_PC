using Assets.MultiSetting;
using System.Collections.Generic;

namespace Assets.Model.RequestData
{

    /// <summary>
    /// Использутся для обновления данных в sql
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UpdateProperty<T> : PropertyRequest, IActionResult
        where T : class, IItemDatabase
    {
        public string Name { get; private set; }

        /// <summary>
        /// Элементы которые подлежат обновлению на sql
        /// </summary>
        public readonly T Value;

        public UpdateProperty(string name,T value, Dictionary<string,string> update)
            : base($"UPDATE {value.Table} SET {GetPropertySet(update)} WHERE id = {value.Id};")
        {
            Name = name;
            Value = value;
        }

        private static string GetPropertySet(Dictionary<string, string> update)
        {
            string result = string.Empty;

            foreach (var item in update)
               result += $"{item.Key} = '{item.Value}', ";

            result = result.Remove(result.Length - 2);
            return result;
        }
    }
}

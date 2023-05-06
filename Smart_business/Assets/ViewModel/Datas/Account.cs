using System;
using System.Collections.Generic;
using Assets.Model;
using Assets.MultiSetting;

namespace Assets.ViewModel.Datas
{

    /// <summary>
    /// Аккаунт с даными
    /// </summary>
    public class Account : IItemDatabase
    {

        /// <summary>
        /// Создает новый элемент
        /// </summary>
        public static Dictionary<string, string> ColumnsCreat => new(StringComparer.InvariantCultureIgnoreCase)
        {
            ["id"] = string.Empty,
            ["login"] = string.Empty,
            ["password"] = string.Empty,
            ["name"] = string.Empty,
            ["access"] = string.Empty,
            ["property"] =string.Empty,
            ["dataSet"] =string.Empty,
            ["lastTimeOnline"] = string.Empty,
        };

        /// <summary>
        /// Свойство для вытягивания данных о параметрах, вызывать лишь при обновление
        /// т.к. проходить парсировку строк, так же можно получить строку исходнную =>
        /// Columns["Property"]
        /// </summary>
        public Dictionary<string, string> Property
        {
            get
            {
                var data = Parse(Columns[nameof(Property).ToLower()]);

                var result = new Dictionary<string, string>();
                foreach (var item in data)
                    result.Add(item.Key, item.Value[0]);

                return result;
            }
        }

        /// <summary>
        /// Свойство для вытягивания данных оо параметрах, вызывать лишь при обновление,
        /// т.к. проходить парсировку строк, так же можно получить строку исходнную =>
        /// Columns["Access"]
        /// </summary>
        public Dictionary<string, string[]> Access
             => Parse(Columns[nameof(Access).ToLower()]);

        /// <summary>
        /// Парсирует данные в свойства
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Dictionary<string, string[]> Parse(string data)
        {
            var access = new Dictionary<string, string[]>();

            string[] values = data.Split('$');

            for (int i = 0; i < values.Length; i++)
            {
                var group = values[i].Split('=');

                var title = group[0];
                var groupValues = group[1].Split(',');

                access.Add(title, groupValues);
            }

            return access;
        }

        /// <summary>
        /// Ключ сохраннения
        /// </summary>
        public const string TableContains = "users";

        /// <summary>
        /// У каждого пользователя свой набор данных
        /// </summary>
        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;

        /// <summary>
        /// Реализация интерфейса IItemDatabase
        /// </summary>
        public string Table => TableContains;

        /// <summary>
        /// Индексатор для упращения доступа к параметрам
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public string this[string column]
        {
            get
            {
                if (Columns.ContainsKey(column))
                    return Columns[column];

                new Result(exception: $"no instaite column! message: {column}",TypeException.LogicApplication);
                return column;
            }
            set
            {
                if (Columns.ContainsKey(column)) Columns[column] = value;
                else new Result(exception: $"no instaite column! message: {column}", TypeException.LogicApplication);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.MultiSetting;

namespace Assets.Model.RequestData
{

    /// <summary>
    /// Параметры выгрузки данных, а именно pull objects
    /// </summary>
    /// <typeparam name="T">Тип выгрузки</typeparam>
    public class PullProperty<T> : PropertyRequest, IActionResultOf<T>
        where T : class, IItemDatabase, IPullItem, new()
    {
        public string Name { get; private set; }

        public Type Type { get; private set; }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public readonly string Table;

        /// <summary>
        /// Имя колонки со временем
        /// </summary>
        public readonly string ColumnDate;

        /// <summary>
        /// С какого периода искать
        /// </summary>
        public readonly string StartSearch;

        /// <summary>
        /// По какой период искать
        /// </summary>
        public readonly string EndSearch;

        public PullProperty(string name,string table,string columnDate, DateTime start, DateTime end)
            : base($"SELECT * FROM {table} WHERE {columnDate} BETWEEN '{start:s}' AND '{end:s}'")
        {
            Name = name;
            Type = typeof(T);
            Table = table;
            ColumnDate = columnDate;
            StartSearch = $"{start:s}";
            EndSearch = $"{end:s}";
        }
    }
}

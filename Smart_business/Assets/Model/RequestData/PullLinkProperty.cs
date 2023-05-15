using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.MultiSetting;

namespace Assets.Model.RequestData
{
    public class PullLinkProperty<TResult> : PropertyRequest, IActionResultOf<TResult>
         where TResult : class, IItemDatabase, IPullItem,ILinkToObject, new()
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

        public readonly string ColumnLink;

        public readonly int Link;

        public PullLinkProperty(string name,string table, string columnLink, int link, string columnDate, DateTime start, DateTime end, bool isLike = false)
            : base($"SELECT * FROM {table} WHERE {columnLink} { GetWhereValue(link, isLike)} AND {columnDate} BETWEEN '{start:s}' AND '{end:s}'")
        {
            Name = name;
            Type = typeof(TResult);
            Table = table;
            ColumnDate = columnDate;
            StartSearch = $"{start:s}";
            EndSearch = $"{end:s}";
            ColumnLink = columnLink;
            Link = link;
        }

        public static string GetWhereValue(int link, bool isLike)
        {
            if (!isLike)
                return $"= '{link}'";

            return $" LIKE '{$"%${link}$%"}'";
        }
    }
}

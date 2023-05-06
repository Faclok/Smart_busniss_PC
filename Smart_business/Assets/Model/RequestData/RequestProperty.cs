using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.MultiSetting;

namespace Assets.Model.RequestData
{
    public class RequestProperty<T>: PropertyRequest, IActionResultOf<T>
     where T : class, IItemDatabase, new()
    {
        public string Name { get; private set; }

        public Type Type { get; private set; }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public readonly string Table;

        public RequestProperty(string name,string table)
            : base($"SELECT * FROM {table}")
        {
            Name = name;
            Type = typeof(T);
            Table = table;
        }
    }
}

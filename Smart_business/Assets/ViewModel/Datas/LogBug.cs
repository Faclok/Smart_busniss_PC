using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.MultiSetting;

namespace Assets.ViewModel.Datas
{
    public class LogBug : IItemDatabase
    {
        public static Dictionary<string, string> ColumnsCreat =>
            new()
            {
                ["id"] = string.Empty,
                ["userId"] = string.Empty,
                ["message"] = string.Empty,
                ["requestDate"] = string.Empty,
            };

        public const string TABLE = "logBugs";

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;

        public string Table => TABLE;

        public string this[string columns]
        {
            get => Columns[columns];
            set => Columns[columns] = value;
        }
    }
}

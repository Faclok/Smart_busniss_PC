using Assets.Model;
using System.Collections.Generic;
using Assets.MultiSetting;
using System;

namespace Assets.ViewModel.Datas
{
    public class Product : IItemDatabase
    {
        public static Dictionary<string, string> ColumnsCreat => new(StringComparer.InvariantCultureIgnoreCase)
        {
            ["id"] = string.Empty,
            ["name"] = string.Empty,
            ["description"] =string.Empty,
            ["dataSet"] =string.Empty,
            ["price"] = string.Empty,
            ["amount"] = string.Empty
        };

        public string Name => Columns["name"];

        public string CreatMachineSQL => $"{DateTime.Parse(Columns["dataSet"]):yyyy.MM.dd}";

        public string Price => Columns["price"];

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;

        public string Table => TABLE;

        public const string TABLE = "product";

        public string this[string column]
        {
            get
            {
                if (Columns.ContainsKey(column))
                    return Columns[column];

                new Result(exception: $"no instaite column! message: {column}", TypeException.LogicApplication);
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

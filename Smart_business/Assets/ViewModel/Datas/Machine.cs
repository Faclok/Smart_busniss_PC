using Assets.Model;
using System;
using System.Collections.Generic;
using Assets.MultiSetting;

namespace Assets.ViewModel.Datas
{

    public class Machine : IItemDatabase
    {
        public static Dictionary<string, string> ColumnsCreat => new(StringComparer.InvariantCultureIgnoreCase)
        {
            ["id"] = string.Empty,
            ["name"] = string.Empty,
            ["dataSet"] = string.Empty,
            ["description"] = string.Empty,
            ["icon"] = string.Empty,
            ["isActive"] = string.Empty,
            ["startWorkDate"] = string.Empty,
            ["amount"] = string.Empty,
        };

        public const string TableContains = "machine";

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;

        public string Table => TableContains;
    
        public string Name => this["name"];

        public bool IsActive => this["isActive"] == bool.TrueString;

        public string CreatMachineSQL => $"{DateTime.Parse(Columns["dataSet"]):yyyy.MM.dd}";

        public DateTime StartJob => DateTime.Parse(this["startWorkDate"]);

        public int CountHours => int.Parse(this["amount"]);

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

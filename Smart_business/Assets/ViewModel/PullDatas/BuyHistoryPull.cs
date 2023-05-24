
using Assets.Model;
using System;
using System.Collections.Generic;
using Assets.MultiSetting;

namespace Assets.ViewModel.PullDatas
{
    public class BuyHistoryPull : IItemDatabase, IPullItem , ILinkToObject
    {
        public static Dictionary<string, string> ColumnsCreat => new(StringComparer.InvariantCultureIgnoreCase)
        {

            ["id"] =string.Empty,
            ["idClient"] =string.Empty,
            [COLUMN_DATE] = string.Empty,
            ["state"] =string.Empty,
            ["priceConst"] =string.Empty,
            ["idProducts"] = string.Empty
        };

        public const string COLUMN_DATE = "readingTime";

        public string ColumnDate => COLUMN_DATE;

        public string[] Products => Columns["idProducts"].Split('$');

        public string Price => Columns["priceConst"];

        public DateTime ReadingTime => DateTime.Parse(Columns[COLUMN_DATE]);

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;

        public const string TABLE = "buyHistoryPull";

        public string Table => TABLE;

        public const string COLUMN_LINK = "idClient";

        public string ColumnLink => COLUMN_LINK;

        public int Link => int.Parse(Columns[COLUMN_LINK]);

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

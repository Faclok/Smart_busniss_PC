using Assets.Model;
using System;
using System.Collections.Generic;
using Assets.MultiSetting;

namespace Assets.ViewModel.PullDatas
{
    public class StockPull : IItemDatabase, IPullItem
    {
        public static Dictionary<string, string> ColumnsCreat => new(StringComparer.InvariantCultureIgnoreCase)
        {

            ["id"] = string.Empty,
            ["idObject"] =string.Empty,
            [COLUMN_DATE] = string.Empty,
            ["state"]= string.Empty,
            ["value"] = string.Empty
        };

        public const string COLUMN_DATE = "readingTime";

        public const string TABLE = "stockPull";

        public string Table => TABLE;

        public string ColumnDate => COLUMN_DATE;

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;
    }
}

using Assets.Model;
using System;
using System.Collections.Generic;
using Assets.MultiSetting;

namespace Assets.ViewModel.PullDatas
{
    public class StockPull : IItemDatabase, IPullItem, ILinkToObject
    {
        public static Dictionary<string, string> ColumnsCreat => new(StringComparer.InvariantCultureIgnoreCase)
        {

            ["id"] = string.Empty,
            [COLUMN_LINK] =string.Empty,
            [COLUMN_DATE] = string.Empty,
            ["state"]= string.Empty,
            ["value"] = string.Empty
        };

        public const string COLUMN_DATE = "readingTime";

        public const string COLUMN_LINK = "idObject";

        public const string TABLE = "stockPull";

        public string Table => TABLE;

        public string ColumnDate => COLUMN_DATE;

        public int Value => int.Parse(Columns["values"]);

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;

        public string ColumnLink => COLUMN_LINK;

        public int Link => int.Parse(Columns[COLUMN_LINK]);
    }
}

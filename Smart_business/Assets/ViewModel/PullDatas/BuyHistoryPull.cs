
using Assets.Model;
using System;
using System.Collections.Generic;
using Assets.MultiSetting;

namespace Assets.ViewModel.PullDatas
{
    public class BuyHistoryPull : IItemDatabase, IPullItem 
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

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;

        public const string TABLE = "buyHistoryPull";

        public string Table => TABLE; 
    }
}


using Assets.Model;
using System;
using System.Collections.Generic;
using Assets.MultiSetting;

namespace Assets.ViewModel.PullDatas
{
    public class PriceChangePull : IItemDatabase, IPullItem
    {
        public static Dictionary<string, string> ColumnsCreat => new(StringComparer.InvariantCultureIgnoreCase)
        {

            ["id"] = string.Empty,
            ["idProduct"] = string.Empty,
            [COLUMN_DATE] =string.Empty,
            ["state"] = string.Empty,
            ["priceNew"] = string.Empty,
            ["pricePrev"] = string.Empty
        };

        public const string COLUMN_DATE = "readingTime";

        public string ColumnDate => COLUMN_DATE;

        public const string TABLE = "priceChangePull";

        public string Table => TABLE;

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;
    }
}

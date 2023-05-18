
using Assets.Model;
using System;
using System.Collections.Generic;
using Assets.MultiSetting;
using UnityEngine;

namespace Assets.ViewModel.PullDatas
{
    public class PriceChangePull : IItemDatabase, IPullItem, ILinkToObject
    {
        public static Dictionary<string, string> ColumnsCreat => new(StringComparer.InvariantCultureIgnoreCase)
        {

            ["id"] = string.Empty,
            [COLUMN_LINK] = string.Empty,
            [COLUMN_DATE] =string.Empty,
            ["state"] = string.Empty,
            ["priceNew"] = string.Empty,
            ["pricePrev"] = string.Empty
        };

        public const string COLUMN_DATE = "readingTime";

        public const string COLUMN_LINK = "idProduct";

        public string ColumnDate => COLUMN_DATE;

        public const string TABLE = "priceChangePull";

        public string Table => TABLE;

        public decimal PriceChanger
        {
            get
            {
                var value = decimal.Parse(this["priceNew"]) - decimal.Parse(this["pricePrev"]);

                return value >= 0 ? value : value * -1;
            }
        }

        public decimal PriceChangerProcent
        {
            get
            {
                var pricePrev = decimal.Parse(this["pricePrev"]);
                var valueSpacing = decimal.Parse(this["priceNew"]) - pricePrev;

                return Math.Round((valueSpacing / pricePrev) * 100M, 2);
            }
        }

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;

        public string ColumnLink => COLUMN_LINK;

        public int Link => int.Parse(this[COLUMN_LINK]);

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

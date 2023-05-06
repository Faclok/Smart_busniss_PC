
using Assets.Model;
using System;
using System.Collections.Generic;
using Assets.MultiSetting;

namespace Assets.ViewModel.PullDatas
{
    public class MachineWorkPull : IItemDatabase, IPullItem, ILinkToObject
    {
        public static Dictionary<string, string> ColumnCreat => new(StringComparer.InvariantCultureIgnoreCase)
        {
            ["id"] = string.Empty,
            [COLUMN_LINK] =string.Empty,
            ["dateStart"] = string.Empty,
            [COLUMN_DATE] = string.Empty,
            ["state"] =string.Empty
        };

        public const string COLUMN_DATE = "dateEnd";

        public string ColumnDate => COLUMN_DATE;

        public Dictionary<string, string> Columns { get; set; } = ColumnCreat;

        public const string TABLE = "machineWorkPull";

        public const string COLUMN_LINK = "idMachine";

        public TimeSpan TimeSpan => DateTime.Parse(Columns[COLUMN_DATE]).Subtract(DateTime.Parse(Columns["dateStart"]));

        public string Table => TABLE;

        public string ColumnLink => COLUMN_LINK;

        public int Link => int.Parse(Columns[COLUMN_LINK]);
    }
}

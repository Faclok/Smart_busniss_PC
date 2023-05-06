using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.MultiSetting;

namespace Assets.ViewModel.Datas
{
    public class VersionApplication : IItemDatabase
    {
        public static Dictionary<string, string> ColumnsCreat =>
            new()
            {
                ["id"] = string.Empty,
                ["version"] = string.Empty,
                ["bugFix"] = string.Empty,
                ["description"] = string.Empty,
                ["updateDate"] = string.Empty,
            };

        public const string TABLE = "applicationVersion";

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;

        public string Table => TABLE;
    }
}

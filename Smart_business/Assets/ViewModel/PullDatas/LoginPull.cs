
using Assets.Model;
using System;
using System.Collections.Generic;
using Assets.MultiSetting;
using System.Threading.Tasks;

namespace Assets.ViewModel.PullDatas
{
    public class LoginPull: IItemDatabase, IPullItem ,ILinkToObject
    {
        public static Dictionary<string, string> ColumnsCreat => new(StringComparer.InvariantCultureIgnoreCase)
        {
            ["id"] = string.Empty,
            [COLUMN_LINK] = string.Empty,
            [COLUMN_DATE] =string.Empty,
            ["nameMachine"] = string.Empty,
            ["os"] = string.Empty,
            ["codeLogin"] = string.Empty
        };

        public const string COLUMN_DATE = "readingTime";

        public const string TABLE = "loginPull";

        public const string ACTIVE = "active";

        public const string DISABLED = "disabled";

        public string ColumnDate  => COLUMN_DATE;

        public Dictionary<string, string> Columns { get; set; } = ColumnsCreat;

        public string Table => TABLE;

        public const string COLUMN_LINK = "idUser";

        public string ColumnLink => COLUMN_LINK;

        public int Link => int.Parse(Columns[COLUMN_LINK]);

        public static  Task DisableLogin(LoginPull login)
            => Task.Run(()=> ModelDatabase.UpdateObject(login, new Dictionary<string, string>() { ["codeLogin"] = DISABLED }));

        public string this[string column]
        { 
            get => Columns[column];
            set => Columns[column] = value;
        }
    }
}

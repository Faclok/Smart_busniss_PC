using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Assets.Model;
using Assets.Model.RequestData;
using Assets.MultiSetting;

namespace Assets.ViewModel
{

    /// <summary>
    /// Контроль загрузки классов из сервера
    /// </summary>
    public static class ModelDatabase
    {
        private static Dictionary<string, IItemDatabase> _downkiadDatas = new();

        private static Task<Result> _taskConnecting;

        public static async Task<Result> ConnectingAsync()
        {
            if (!_taskConnecting?.IsCompleted ?? false)
            {
                var data = _taskConnecting.Result;

                return new(exception: "On first close time connecting", data.TypeException ?? TypeException.LogicApplication);
            }

            return await Server.ConnectedAsync();
        }

        /// <summary>
        /// Загрузка обьектов из server
        /// </summary>
        /// <typeparam name="T">Тип данных при загрузке</typeparam>
        /// <param name="columns">Параметры для выгрузки данных данных</param>
        /// <param name="count">Количество данных, елси значение указанно меньше нуля, то происходит выгрузка всех данных</param>
        /// <returns></returns>
        public static Task<TResult[]> GetUniqueObjectAsync<TResult>(string table)
            where TResult : class, IItemDatabase, new()
        => ActionServerResultOf<TResult,RequestProperty<TResult>>(new RequestProperty<TResult>("get unique object", table));

        public static Task<TResult[]> GetObjecyWhere<TResult>(string table, Dictionary<string,string> columnsContains)
            where TResult : class, IItemDatabase, new()
            => ActionServerResultOf<TResult, RequestWhereProperty<TResult>>(new RequestWhereProperty<TResult>("get object in where", columnsContains, table));

        public static Task<TResult[]> GetPullObjectAsync<TResult>(string table, string coloumnDate, DateTime start, DateTime end)
            where TResult : class, IItemDatabase, IPullItem, new()
        => ActionServerResultOf<TResult,PullProperty<TResult>>(new PullProperty<TResult>("get pull object", table, coloumnDate, start, end));

        public static Task<TResult[]> GetPullLinkObjectAsync<TResult>(string table, string columnLink, IItemDatabase link, string coloumnDate, DateTime start, DateTime end)
            where TResult : class, IItemDatabase, IPullItem, ILinkToObject, new()
        => ActionServerResultOf<TResult, PullLinkProperty<TResult>>(new PullLinkProperty<TResult>("get pull link object", table, columnLink, link.Id, coloumnDate, start, end));

        public static Task DeleteObject<IDelete>(IDelete deleteObject)
            where IDelete : class, IItemDatabase
            => ActionServer(new DeleteProperty<IDelete>("delete object", deleteObject));

        public static Task UpdateObject<IUpdate>(IUpdate updateItem,Dictionary<string,string> updateColumns)
            where IUpdate : class, IItemDatabase
            => ActionServer(new UpdateProperty<IUpdate>("update object server", updateItem, updateColumns));

        public static Task CreatObject<ICreatObject>(ICreatObject newObject)
            where ICreatObject: class, IItemDatabase
            => ActionServer(new InsertProperty<ICreatObject>("creat object",newObject));

        public static async Task<ICreatObject> CreatObjectUpdateLocalData<ICreatObject>(ICreatObject newObject)
            where ICreatObject : class, IItemDatabase
        {
            await CreatObject(newObject);
            var data = await Server.GetValue("select LAST_INSERT_ID();");
            newObject.Columns["id"] = data;
            
            return newObject;
        }

        private static async Task ActionServer<TAction>(TAction property)
            where TAction : PropertyRequest, IActionResult
        {
            var data = await Server.ActionResultAsync(property);

            if (!data)
                throw new Exception("not root"); //FIX
        }

        private static async Task<TResult[]> ActionServerResultOf<TResult, TProperty>(TProperty property)
            where TProperty : PropertyRequest, IActionResultOf<TResult>
            where TResult : class, IItemDatabase, new()
        {
            var data = await Server.ActionResultOfData<TProperty, TResult>(property);

            if (data)
                return data.Value;

            return new TResult[0]; //Fix
        }
    }
}

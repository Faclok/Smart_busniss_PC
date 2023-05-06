using System;
using System.Collections.Generic;

namespace Assets.MultiSetting
{

    /// <summary>
    /// Указывает что объект может находится в sql
    /// </summary>
    public interface IItemDatabase
    {

        /// <summary>
        /// Столбики, с помощью которых сервер будет загружать данные
        /// </summary>
        public Dictionary<string, string> Columns { get; set; }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string Table { get; }

        /// <summary>
        /// Свойство которое есть в каждой таблице
        /// </summary>
        public int Id => int.Parse(Columns["id"]);
    }
}

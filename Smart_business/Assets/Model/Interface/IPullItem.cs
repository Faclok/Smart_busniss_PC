using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Model
{

    /// <summary>
    /// Позволяет использовать данные как pull objects,
    /// используя лишь имя таблицы и период времени
    /// </summary>
    public interface IPullItem
    {

        /// <summary>
        /// Имя колонки времеи, во сколько оно было созданно
        /// </summary>
        public string ColumnDate { get; }
    }
}

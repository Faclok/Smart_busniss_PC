using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.View.Body.FullScreen.AnalyzeWindow
{

    /// <summary>
    /// Данные используемые для отрисовки в окне Анализа
    /// </summary>
    public class ItemData : IComparable<ItemData>
    {
        /// <summary>
        /// Имя предмета
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Его процентная зависимость
        /// </summary>
        public readonly float Precent;

        /// <summary>
        /// При ошибке данных использовать это поле
        /// </summary>
        public const string NameDeafult = "not load machine";

        /// <summary>
        /// При ошибке данных использовать это поле
        /// </summary>
        public const float PrecentDeafult = 0f;

        public ItemData(string name, float precent) => (Name, Precent) = (name, precent);

        public ItemData() => (Name, Precent) = (NameDeafult,PrecentDeafult);

        /// <summary>
        /// Метод сравнения
        /// </summary>
        /// <param name="other">С чем сравниваем</param>
        /// <returns></returns>
        public int CompareTo(ItemData other)
            => other.Precent.CompareTo(Precent);
    }
}

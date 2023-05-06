using Assets.MultiSetting;

namespace Assets.Model.RequestData
{

    /// <summary>
    /// Класс для удаления объекта из sql 
    /// </summary>
    /// <typeparam name="T">Класс реализующий интерфейс</typeparam>
    public class DeleteProperty<T> : PropertyRequest, IActionResult
        where T: class, IItemDatabase
    {
        public string Name { get; private set; }

        /// <summary>
        /// Значения которые нужно удалить
        /// </summary>
        public readonly T Value;
        public DeleteProperty(string name,T value)
            :base($"DELETE FROM {value.Table} WHERE id = {value.Id}")
        {
            Name = name;
            Value = value;
        }
    }
}

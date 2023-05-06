
#nullable enable annotations

namespace Assets.MultiSetting
{

    /// <summary>
    /// Класс, отслеживания возможности появления Exception.
    /// Тем самым не производя отлов Exception и экономить производительность
    /// </summary>
    public class Result
    {

        /// <summary>
        /// При отрицательном результате, будет отличаться от null
        /// </summary>
        public readonly string? Exception;

        /// <summary>
        /// Тип ошибки
        /// </summary>
        public readonly TypeException? TypeException;

        /// <summary>
        /// Когда во время процесса возникла ошибка, используйте этот конструктор
        /// </summary>
        /// <param name="exception">Описание ошибки</param>
        public Result(string exception, TypeException typeException)
        {
            Exception = exception;
            TypeException = typeException;
            this.Debugger();
        }

        /// <summary>
        /// Когда результат положительный просто создайте пустой конструктор
        /// </summary>
        public Result()
        { 
            /// Пустой конструктор, для удачного результата
        }

        /// <summary>
        /// Есть ли ошибка при процессе
        /// </summary>
        public bool HasValue => Exception == null;

        /// <summary>
        /// Оператор для использования класса в блоке if
        /// </summary>
        /// <param name="result">Значение из которого берутся данные</param>
        public static implicit operator bool(Result result) => result.HasValue;
    }
}
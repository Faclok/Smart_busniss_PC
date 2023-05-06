
#nullable enable annotations

namespace Assets.MultiSetting
{

    /// <summary>
    /// Гибкий класс, для возращения параметров и возможности появления Exception.
    /// Тем самым не производя отлов Exception и экономить производительность
    /// </summary>
    /// <typeparam name="T">Параметр который будет использоваться для возращения в лучшем случае</typeparam>
    public class ResultOf<TValue>
    {

        /// <summary>
        /// При положительном результате, будет отличаться от null
        /// </summary>
        public readonly TValue? Value;

        /// <summary>
        /// При отрицательном результате, будет отличаться от null
        /// </summary>
        public readonly string? Exception;

        /// <summary>
        /// Тип ошибки
        /// </summary>
        public readonly TypeException? TypeException;

        /// <summary>
        /// Когда результат положительный просто верните значение
        /// </summary>
        /// <param name="value">Значение возращаемого  типа TValue</param>
        public ResultOf(TValue value)
        {
            Value = value;
        }

        /// <summary>
        /// Когда во время процесса возникла ошибка, используйте этот конструктор
        /// </summary>
        /// <param name="exception">Описание ошибки</param>
        public ResultOf(string exception, TypeException typeException)
        {
            Exception = exception;
            TypeException = typeException;
            this.Debugger();
        }

        /// <summary>
        /// Существует ли значение
        /// </summary>
        public bool HasValue => Exception == null;

        /// <summary>
        /// Оператор для использования в блоке if
        /// </summary>
        /// <param name="result">значение из которого берется bool</param>
        public static implicit operator bool(ResultOf<TValue> result) => result.HasValue;
    }
}

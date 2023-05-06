
#nullable enable annotations

namespace Assets.MultiSetting
{

    /// <summary>
    /// ������ �����, ��� ���������� ���������� � ����������� ��������� Exception.
    /// ��� ����� �� ��������� ����� Exception � ��������� ������������������
    /// </summary>
    /// <typeparam name="T">�������� ������� ����� �������������� ��� ���������� � ������ ������</typeparam>
    public class ResultOf<TValue>
    {

        /// <summary>
        /// ��� ������������� ����������, ����� ���������� �� null
        /// </summary>
        public readonly TValue? Value;

        /// <summary>
        /// ��� ������������� ����������, ����� ���������� �� null
        /// </summary>
        public readonly string? Exception;

        /// <summary>
        /// ��� ������
        /// </summary>
        public readonly TypeException? TypeException;

        /// <summary>
        /// ����� ��������� ������������� ������ ������� ��������
        /// </summary>
        /// <param name="value">�������� ������������  ���� TValue</param>
        public ResultOf(TValue value)
        {
            Value = value;
        }

        /// <summary>
        /// ����� �� ����� �������� �������� ������, ����������� ���� �����������
        /// </summary>
        /// <param name="exception">�������� ������</param>
        public ResultOf(string exception, TypeException typeException)
        {
            Exception = exception;
            TypeException = typeException;
            this.Debugger();
        }

        /// <summary>
        /// ���������� �� ��������
        /// </summary>
        public bool HasValue => Exception == null;

        /// <summary>
        /// �������� ��� ������������� � ����� if
        /// </summary>
        /// <param name="result">�������� �� �������� ������� bool</param>
        public static implicit operator bool(ResultOf<TValue> result) => result.HasValue;
    }
}

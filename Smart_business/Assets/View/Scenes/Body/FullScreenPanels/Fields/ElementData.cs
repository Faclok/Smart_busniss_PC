
using System;
using System.Linq;

namespace Assets.View.Body.FullScreen.Fields
{
    public class ElementData
    {
        public readonly string Title;
        public readonly string Key;

        public string Value { get => _value; set => _value = GetFormatString(value, this); }
        private string _value;

        public readonly bool IsEdit;
        public readonly int CountSymbols;
        public readonly bool IsNumber;

        public ElementData(string title,string key, string value, bool isEdit, int countSimbols,bool isNumber = false)
        {
            Title = title;
            Key = key;
            _value = value;
            IsEdit = isEdit;
            CountSymbols = countSimbols;
            IsNumber = isNumber;
        }

        public static string GetFormatString(string newValue, ElementData data)
        {
            if (!data.IsEdit) return data.Value;

            if (data.CountSymbols < newValue.Length)
                return data.Value;

            return data.IsNumber ? GetOfNumbers(newValue) : newValue;
        }

        public static string GetOfNumbers(string value) => new(value.Where(char.IsDigit).ToArray());
    }
}

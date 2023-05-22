using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ViewModel
{
    public static class DateTimeCalculate
    {

        public static Range[] GetColumns(DateTime start, DateTime end)
        {
            var days = end.Subtract(start).TotalDays;

            return days switch
            {
                >= 361d => Parse(() => start.AddDays(days / 29d)),
                >= 360d => Parse(() => start.AddDays(12.41d)),
                >= 160d => Parse(() => start.AddDays(5.517d)),
                >= 30d => Parse(() => start.AddHours(24.88d)),
                >= 7d => Parse(() => start.AddHours(7)),
                >= 1d => Parse(() => start.AddMinutes(49)),
                _ => new Range[0],
            };

            static Range[] Parse(Func<DateTime> func)
            {
                var dates = new DateTime[29, 2];
                var result = new Range[29];

                int rows = dates.GetUpperBound(0) + 1;    // количество строк
                int columns = dates.Length / rows;        // количество столбцов

                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++)
                        dates[i, j] = func();

                for (int i = 0; i < rows; i++)
                    result[i] = new Range(dates[i, 0], dates[i, 1]);

                return result;
            }
        }

        public class Range
        {
            public readonly DateTime Start;
            public readonly DateTime End;

            public Range(DateTime start, DateTime end)
            {
                Start = start;
                End = end;
            }
        }
    }
}

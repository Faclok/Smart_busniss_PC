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
                >= 361d => Parse(start, start.AddDays(days / 29d).Subtract(start)),
                >= 360d => Parse(start, start.AddDays(12.41d).Subtract(start)),
                >= 160d => Parse(start, start.AddDays(5.517d).Subtract(start)),
                >= 30d => Parse(start, start.AddHours(24.88d).Subtract(start)),
                >= 7d => Parse(start, start.AddHours(7).Subtract(start)),
                >= 1d => Parse(start, start.AddMinutes(49).Subtract(start)),
                _ => Parse(start, start.AddMinutes(49).Subtract(start))
            };

            static Range[] Parse(DateTime start ,TimeSpan tap)
            {
                var dates = new DateTime[29, 2];
                var result = new Range[29];

                int rows = dates.GetUpperBound(0) + 1;    // количество строк
                int columns = dates.Length / rows;        // количество столбцов

                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++)
                        dates[i, j] = start += tap;

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

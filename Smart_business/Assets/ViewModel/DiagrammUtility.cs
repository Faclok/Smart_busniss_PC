using System;
using System.Collections.Generic;
using System.Linq;

public static class DiagrammUtility
{
    public static float[] GetColumns(int[][] columnsData)
    {
        var columnsReturn = new float[columnsData.Length];

        for (int i = 0; i < columnsData.Length; i++)
            columnsReturn[i] = (float)columnsData[i].Sum() / (float)columnsData[i].Length;

        return GetPoints(columnsReturn);
    }

    public static float[] GetColumns(double[][] columnsData)
    {
        if (!columnsData.Select(o => o.Select(b => b == 0.01d)).Any(o=> o.Any(q => q == false)))
        {
            var list = new List<float>(29);
            for (int i = 0; i < list.Capacity; i++)
                list.Add(0f);

            return list.ToArray();
        }

        var columnsReturn = new float[columnsData.Length];

        for (int i = 0; i < columnsData.Length; i++)
            columnsReturn[i] = (float)columnsData[i].Sum() / (float)columnsData[i].Length;

        return GetPoints(columnsReturn);
    }

    public static float[] GetColumns(decimal[][] columnsData)
    {
        var columnsReturn = new float[columnsData.Length];

        for (int i = 0; i < columnsData.Length; i++)
            columnsReturn[i] = (float)columnsData[i].Sum() / (float)columnsData[i].Length;

        return GetPoints(columnsReturn);
    }

    public static float[] GetPoints(int[] array)
    {
        float[] returnArray = new float[array.Length];

        float max = (float)array.Max();

        for (int i = 0; i < array.Length; i++)
            returnArray[i] = (float)array[i] / max;

        return returnArray;
    }

    public static float[] GetPoints(float[] array)
    {
        if(array.Length == 0)
            return new float[0];

        float[] returnArray = new float[array.Length];

        float max = array.Max();

        for (int i = 0; i < array.Length; i++)
            returnArray[i] = array[i] / max;

        return returnArray;
    }

    public static (float[] points, float procent) GetPointsAndProcent(int[] array)
    {
        float[] returnArray = new float[array.Length];

        float max = array.Max();

        if (max == 0)
            return (returnArray, 0f);

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 0f)
                continue;

            returnArray[i] = array[i] / max;
        }

        return (returnArray, (returnArray[returnArray.Length - 1] - returnArray[0]) / returnArray[0] * 100f);
    }
}

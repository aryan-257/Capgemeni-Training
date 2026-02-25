using System;

public class MultiplicationTable
{
    public static int[] GetMultiplicationRow(int n, int upto)
    {
        int[] row = new int[upto];

        for (int i = 1; i <= upto; i++)
        {
            row[i - 1] = n * i;
        }

        return row;
    }
}

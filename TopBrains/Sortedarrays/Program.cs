using System;

namespace SortedArraysApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example input
            int[] a = { 1, 3, 5, 7 };
            int[] b = { 2, 4, 6, 8, 10 };

            ArrayMerger merger = new ArrayMerger();
            int[] merged = merger.MergeSortedArrays(a, b);

            Console.WriteLine("Merged array:");
            Console.WriteLine(string.Join(" ", merged));
        }
    }
}

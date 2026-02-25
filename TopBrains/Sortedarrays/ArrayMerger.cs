using System;

namespace SortedArraysApp
{
    public class ArrayMerger
    {
        // Generic method to merge two sorted arrays
        public T[] MergeSortedArrays<T>(T[] a, T[] b) where T : IComparable<T>
        {
            int n = a.Length;
            int m = b.Length;
            T[] merged = new T[n + m];

            int i = 0, j = 0, k = 0;

            // Merge using two pointers
            while (i < n && j < m)
            {
                if (a[i].CompareTo(b[j]) <= 0)
                {
                    merged[k++] = a[i++];
                }
                else
                {
                    merged[k++] = b[j++];
                }
            }

            // Copy remaining elements
            while (i < n) merged[k++] = a[i++];
            while (j < m) merged[k++] = b[j++];

            return merged;
        }
    }
}

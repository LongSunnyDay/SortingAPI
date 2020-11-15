using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sorting.Helpers
{
    public class SortingHelper
    {
        public SortingHelper() { }

        public int[] SortNumbers(int[] pNumbers)
        {
            QuickSort(pNumbers, 0, pNumbers.Length - 1);
            return pNumbers;
        }

        // Recursively Quick Sort array
        private void QuickSort(int[] pArr, int pStart, int pEnd)
        {
            int i;
            if (pStart < pEnd)
            {
                i = Partition(pArr, pStart, pEnd);
                // Sort one side
                QuickSort(pArr, pStart, i - 1);
                // Sort other side
                QuickSort(pArr, i + 1, pEnd);
            }
        }

        private int Partition(int[] pArr, int pStart, int pEnd)
        {
            int vTemp;
            int vPivot = pArr[pEnd]; // Select Pivot
            int i = pStart - 1;

            for (int j = pStart; j <= pEnd - 1; j++)
            {
                // If Pivot is higher than passed value, swap them
                if (pArr[j] <= vPivot)
                {
                    i++;
                    vTemp = pArr[i];
                    pArr[i] = pArr[j];
                    pArr[j] = vTemp;
                }
            }

            // Swap first and last
            vTemp = pArr[i + 1];
            pArr[i + 1] = pArr[pEnd];
            pArr[pEnd] = vTemp;
            return i + 1;
        }
    }
}

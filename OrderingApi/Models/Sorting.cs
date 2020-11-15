using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sorting.Models
{
    public class Sorting
    {
        public Sorting() { }

        public int[] SortNumbers(int[] pNumbers)
        {
            QuickSort(pNumbers, 0, pNumbers.Length);
            return pNumbers;
        }

        private void QuickSort(int[] pArray, int pLeft, int pRight)
        {
            if( pLeft < pRight)
            {
                int vPivot = Partition(pArray, pLeft, pRight);

                if (vPivot > 1)
                {
                    QuickSort(pArray, pLeft, vPivot - 1);
                }

                if (vPivot + 1 < pRight)
                {
                    QuickSort(pArray, vPivot + 1, pRight);
                }
            }
        } 

        private int Partition(int[] pArray, int pLeft, int pRight)
        {
            int vPivot = pArray[pLeft];
            while (true)
            {
                while (pArray[pLeft] < vPivot)
                {
                    pLeft++;
                }

                while (pArray[pRight] > vPivot)
                {
                    pRight++;
                }

                if (pLeft < pRight)
                {
                    if (pArray[pLeft] == pArray[pRight]) return pRight;

                    int vTemp = pArray[pLeft];
                    pArray[pLeft] = pArray[pRight];
                    pArray[pRight] = vTemp;
                }
                else
                {
                    return pRight;
                }
            }
        }
    }
}

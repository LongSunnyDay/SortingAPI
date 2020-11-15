using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sorting.Models
{
    public class SortedData
    {  
        public int ID { get; set; }
        public DateTime SortDate { get; set; }
        public int[] Result { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + "; " + "Sorted Date: " + SortDate.ToString("yyyy/MM/dd HH:mm:ss") + ";";
        }
    }

    public class DataToSort
    {
        public int[] InputData { get; set; }
    }
}

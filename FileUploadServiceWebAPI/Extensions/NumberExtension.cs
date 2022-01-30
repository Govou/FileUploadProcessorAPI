using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadServiceWebAPI.Extensions
{
    public static class NumberExtension
    {
        public static double Median(this List<int> myNumArr)
        {
            double median;
            int numCount = myNumArr.Count;
            int halfIndex = numCount / 2;
            var sortedNumArr = myNumArr.OrderBy(n => n).ToArray();

            if ((numCount % 2) == 0)
                median = (sortedNumArr[halfIndex] + sortedNumArr[halfIndex - 1]) / 2;

            else
                median = sortedNumArr[halfIndex];

            return median;
        }

        public static double Mode(this List<int> myNumArr)
        {
            double mode = myNumArr.GroupBy(n => n).OrderByDescending(d => d.Count()).Select(f => f.Key).FirstOrDefault();

            return mode;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Varldsklass.Web.ExtensionMethods
{
    public static class Extensions
    {
        public static double Median(this List<int> list)
        {
            // Clone list, we don't want input list to get sorted
            List<int> sortedList = new List<int>();
            list.ForEach(delegate(int i)
            {
                sortedList.Add(i);
            });
            sortedList.Sort();

            if (sortedList.Count % 2 == 0) // Average of two middle values if there are two
            {
                int position = sortedList.Count / 2;
                return (double)( (sortedList[position] + sortedList[position-1]) / 2.0 );
            }
            else // or just middle value if there's only one
            {
                return sortedList[sortedList.Count / 2];
            }
        }
    }
}
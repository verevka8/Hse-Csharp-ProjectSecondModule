using System;
using System.Collections.Generic;

namespace Project
{
    public class QuickSort
    {
        // public static void QuickSortRecursive<T>(List<T> array, Func<T,T, int> compare, int low, int high)
        // {
        //     if (low < high)
        //     {
        //         int index = Pivot(array, compare, low, high);
        //         QuickSortRecursive(array, compare, low, index- 1);
        //         QuickSortRecursive(array, compare, index + 1, high);
        //     }
        // }
        //
        // private static int Pivot<T>(List<T> array, Func<T, T, int> compare, int low, int high)
        // {
        //     int ind = low-1;
        //     for (int i = low+1; i <= high; i++)
        //     {
        //         if (compare(array[i], array[high]) <= 0) // i-й элемент меньше или равен чем high-й элемент
        //         {
        //             ind += 1;
        //             (array[i], array[ind]) = (array[ind], array[i]);
        //         }
        //     }
        //
        //     (array[ind + 1], array[high]) = (array[high], array[ind + 1]);
        //     return ind + 1;
        // }
        
       
           public static void QuickSortRecursive<T>(List<T> list, Func<T,T, int> compare, int left, int right)
           {
               if (left < right)
               {
                   int pivotIndex = Pivot(list, compare, left, right);
                   QuickSortRecursive(list, compare, left, pivotIndex - 1);
                   QuickSortRecursive(list, compare, pivotIndex + 1, right);
               }
           }

           private static int Pivot<T>(List<T> list, Func<T,T, int> compare, int left, int right)
           {
               T pivot = list[right];
               int low = left - 1;
               for (int i = left; i < right; i++)
               {
                   if (compare(list[i], pivot) <= 0)
                   {
                       low++;
                       (list[i], list[low]) = (list[low], list[i]);
                   }
               }
               (list[low+1], list[right]) = (list[right], list[low+1]);
               
               return low + 1;
           }
    }
}
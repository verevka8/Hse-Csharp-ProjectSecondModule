﻿namespace Project
{
    public class St
    {
        private static void QuickSortRecursive(int[] array, int low, int high)
        {
            if (low < high)
            {
                // Разделяем массив и получаем индекс опорного элемента
                int pivotIndex = Partition(array, low, high);

                // Рекурсивно сортируем левую и правую части
                QuickSortRecursive(array, low, pivotIndex - 1);
                QuickSortRecursive(array, pivotIndex + 1, high);
            }
        }

        private static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = low - 1; 

            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    (array[i], array[j]) = (array[j], array[i]);
                }
            }
            
            (array[i+1], array[high]) = (array[high], array[i+1]);
            return i + 1;
        }
        
    }
}
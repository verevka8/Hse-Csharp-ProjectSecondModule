namespace Project
{
    public class Sort
    {
        public static void QuickSortRecursive<T>(List<T> array, Func<T,T, int> compare, int low, int high)
        {
            if (low < high)
            {
                int index = Pivot(array, compare, low, high);
                QuickSortRecursive(array, compare, low, index- 1);
                QuickSortRecursive(array, compare, index + 1, high);
            }
            
        }

        private static int Pivot<T>(List<T> array, Func<T, T, int> compare, int low, int high)
        {
            int ind = low-1;
            for (int i = low+1; i <= high; i++)
            {
                if (compare(array[i], array[high]) == -1)
                {
                    ind += 1;
                    (array[i], array[ind]) = (array[ind], array[i]);
                }
            }

            (array[ind + 1], array[high]) = (array[high], array[ind + 1]);
            return ind + 1;
        }
    }
}
namespace Project.Utils
{
    /// <summary>
    /// Класс, реализующий алгоритм быстрой сортировки (QuickSort) с использованием рекурсии.
    /// </summary>
    public class QuickSort
    {
        /// <summary>
        /// Выполняет сортировку списка с использованием алгоритма QuickSort.
        /// </summary>
        /// <typeparam name="T">Тип элементов списка.</typeparam>
        /// <param name="list">Список, который необходимо отсортировать.</param>
        /// <param name="compare">Функция сравнения двух элементов списка.</param>
        /// <param name="left">Индекс начала диапазона сортировки.</param>
        /// <param name="right">Индекс конца диапазона сортировки.</param>
        public static void QuickSortRecursive<T>(List<T> list, Func<T, T, int> compare, int left, int right)
        {
            if (left < right)
            {
                // Находим индекс опорного элемента после разделения
                int pivotIndex = Pivot(list, compare, left, right);

                // Рекурсивно сортируем левую и правую части списка
                QuickSortRecursive(list, compare, left, pivotIndex - 1);
                QuickSortRecursive(list, compare, pivotIndex + 1, right);
            }
        }

        /// <summary>
        /// Определяет опорный элемент и перемещает элементы меньше опорного влево, а большие вправо
        /// </summary>
        /// <typeparam name="T">Тип элементов списка.</typeparam>
        /// <param name="list">Список, который сортируется.</param>
        /// <param name="compare">Функция сравнения двух элементов списка.</param>
        /// <param name="left">Индекс начала диапазона.</param>
        /// <param name="right">Индекс конца диапазона.</param>
        /// <returns>Индекс нового положения опорного элемента.</returns>
        private static int Pivot<T>(List<T> list, Func<T, T, int> compare, int left, int right)
        {
            // Выбираем последний элемент как опорный
            T pivot = list[right];
            int low = left - 1;

            // Перемещаем элементы меньше опорного влево
            for (int i = left; i < right; i++)
            {
                if (compare(list[i], pivot) <= 0)
                {
                    low++;
                    // Меняем местами текущий элемент и элемент с индексом low
                    (list[i], list[low]) = (list[low], list[i]);
                }
            }

            // Перемещаем опорный элемент на правильную позицию
            (list[low + 1], list[right]) = (list[right], list[low + 1]);

            return low + 1;
        }
    }
}

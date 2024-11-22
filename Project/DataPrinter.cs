using Project.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static Project.Utils.Const;

namespace Project
{
    /// <summary>
    /// Класс содержающий логику работы с консолью
    /// </summary>
    public static class DataPrinter
    {
        /// <summary>
        /// Выводит текстовое сообщение в консоль.
        /// </summary>
        /// <param name="message">Сообщение для отображения.</param>
        public static void PrintData(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Выводит список студентов. Опционально выводит список студентов 
        /// </summary>
        /// <param name="students">Список студентов для вывода.</param>
        /// <param name="headers">Заголовки столбцов.</param>
        /// <param name="includeAverage">Флаг, указывающий на необходимость включения среднего балла в вывод.</param>
        public static void PrintData(List<Student> students, bool includeAverage = false)
        {
            string[] headers = includeAverage ? DefaultHeadersWithAverage : DefaultHeaders;

            // Рассчитываем размеры колонок
            DataFormatter.СalculateСolumnSize(out int[] lenOfColumns, students, headers);

            // Формируем строку заголовков
            Console.WriteLine(DataFormatter.GenerateHeaders(headers, lenOfColumns));
            
            foreach (Student student in students)
            {
                Console.WriteLine(DataFormatter.GetStringFormatOfStudent(lenOfColumns, student.GetStudentFields(includeAverage))); // Выводим отформатированные данные каждого студента
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Выводит список студентов с расчетом разницы рез-ов для каждой группы <c>LunchType</c>.
        /// </summary>
        /// <param name="sortedStudents">Список студентов, отсортированный по <c>LunchType</c>.</param>
        /// <param name="dictionaryWithDeltaOfEachGroup">Словарь, где ключ — <c>LunchType</c>, а значение — разница между максимальным и минимальным результатами по математике в одной группе.</param>
        public static void PrintDataWithDelta(List<Student> sortedStudents, Dictionary<string, long> dictionaryWithDeltaOfEachGroup)
        {
            // Рассчитываем размеры колонок
            DataFormatter.СalculateСolumnSize(out int[] lenOfColumns, sortedStudents, DefaultHeaders);

            // Формируем строку заголовков
            Console.WriteLine(DataFormatter.GenerateHeaders(DefaultHeaders, lenOfColumns));

            string currentLunchType = "\n"; // Инициализируем значение, отличное от возможных значений <c>LunchType</c>
            for (int i = 0; i < sortedStudents.Count; i++)
            {
                // Если <c>LunchType</c> изменился, выводим дельту
                if (sortedStudents[i].LunchType != currentLunchType)
                {
                    Console.WriteLine($"\nВ выборке с LunchType: \"{sortedStudents[i].LunchType}\" - разница между максимальным и минимальным результатом по математике составляет: {dictionaryWithDeltaOfEachGroup[sortedStudents[i].LunchType]}.");
                    currentLunchType = sortedStudents[i].LunchType;
                }

                // Выводим данные студента
                Console.WriteLine(DataFormatter.GetStringFormatOfStudent(lenOfColumns, sortedStudents[i].GetStudentFields()));
            }
            Console.WriteLine();
        }
    }
}

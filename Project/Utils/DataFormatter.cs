using System.Text;
using static Project.Utils.Const;

namespace Project.Utils
{
    /// <summary>
    /// Класс, содержащий вспомогательные методы для форматирования данных.
    /// </summary>
    public static class DataFormatter
    {
        /// <summary>
        /// Форматирует строку с данными о студенте для вывода в консоль с учетом ширины колонок.
        /// </summary>
        /// <param name="lenOfColumns">Массив ширины каждой колонки.</param>
        /// <param name="studentFields">Массив строковых значений полей студента.</param>
        /// <returns>Отформатированная строка для вывода.</returns>
        public static string GetStringFormatOfStudent(int[] lenOfColumns, string[] studentFields)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
                result.Append(TextCentering(studentFields[i], lenOfColumns[i]) + "  ");
            }

            return result.ToString();
        }

        /// <summary>
        /// Центрирует текст внутри строки заданной длины.
        /// </summary>
        /// <param name="s">Текст для выравнивания.</param>
        /// <param name="count">Общая длина результирующей строки.</param>
        /// <returns>Центрированная строка.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если длина текста превышает указанную максимальную длину строки.</exception>
        private static string TextCentering(string s, int count)
        {
            count -= s.Length;
            if (count < 0)
            {
                throw new ArgumentException("Длина текста превышает указанную длину строки.");
            }
            return new string(' ', count / 2) + s + new string(' ', (count + 1) / 2);
        }

        /// <summary>
        /// Рассчитывает ширину каждой колонки для корректного форматирования таблицы.
        /// </summary>
        /// <param name="lenOfColumns">Массив для сохранения ширины каждой колонки, который нужно инициализовать.</param>
        /// <param name="students">Список студентов.</param>
        /// <param name="headers">Массив заголовков колонок.</param>
        public static void СalculateСolumnSize(out int[] lenOfColumns, List<Student> students, string[] headers)
        {
            lenOfColumns = new int[headers.Length];

            // Устанавливаем минимальную ширину колонок на основе длины заголовков
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
                lenOfColumns[i] = headers[i].Length;
            }

            // Обновляем максимальную длину колокни на лоснове данных каждого студента
            for (int i = 0; i < students.Count; i++)
            {
                string[] studentFields = students[i].GetStudentFields(headers.Length == DefaultHeadersWithAverage.Length);
                for (int j = 0; j < lenOfColumns.Length; j++)
                {
                    lenOfColumns[j] = Math.Max(studentFields[j].Length, lenOfColumns[j]);
                }
            }
        }

        /// <summary>
        /// Генерирует строку заголовков таблицы с учетом ширины колонок.
        /// </summary>
        /// <param name="headers">Массив заголовков колонок.</param>
        /// <param name="lenOfColumns">Массив ширины каждой колонки.</param>
        /// <returns>Отформатированная строка заголовков.</returns>
        public static string GenerateHeaders(string[] headers, int[] lenOfColumns)
        {
            StringBuilder header = new StringBuilder();
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
                header.Append(TextCentering(headers[i], lenOfColumns[i]) + "  ");
            }
            return "\n" + header;
        }
    }
}

using System.Text;
using static Project.Utils.Const;

namespace Project
{
    /// <summary>
    /// Класс для вывода данных студентов в файл формата CSV.
    /// </summary>
    public class StudentDataOutput
    {
        /// <summary>
        /// Сохраняет список данных студентов в файл CSV.
        /// </summary>
        /// <param name="fileName">Имя выходного файла.</param>
        /// <param name="data">Список объектов <see cref="Student"/>, которые нужно сохранить.</param>
        /// <param name="includeAverage">необезательый флаг, указывающий, следует ли добавлять средние баллы в данные.</param>
        public void SaveDataToCsv(string fileName, List<Student> data, bool includeAverage = false)
        {
     
               StringBuilder csvLines = new StringBuilder();
               // Добавляем заголовки (средние значения включаются при необходимости)
               csvLines.Append(ConvertDataToString(includeAverage ? DefaultHeadersWithAverage : DefaultHeaders));
               // Перебираем данные студентов и добавляем их в CSV
               string[] fields;
               foreach (Student item in data)
               {
                   fields = item.GetStudentFields(includeAverage); // Получаем поля объекта
                   csvLines.Append(ConvertDataToString(fields)); // Конвертируем их в строку CSV
               }
               File.WriteAllText("../../../../" + fileName, csvLines.ToString(), Encoding.UTF8);
               
            DataPrinter.PrintData("Файл успешно сохранен");
        }

        /// <summary>
        /// Конвертирует массив строк в формат строки CSV.
        /// </summary>
        /// <param name="data">Массив строк для конвертации.</param>
        /// <returns>Строка, отформатированная в соответствии с CSV.</returns>
        private string ConvertDataToString(string[] data)
        {
            StringBuilder output = new StringBuilder();

            // Форматируем данные в виде строки CSV с разделением значений запятыми
            for (int i = 0; i < data.Length; i++)
            {
                output.Append("\"" + data[i] + "\"" + (i != data.Length - 1 ? "," : "\n")); // если элемент последний, то не добавляем разделитель,
                                                                                            // а добавляем переход на новую строку
            }

            return output.ToString();
        }
    }
}

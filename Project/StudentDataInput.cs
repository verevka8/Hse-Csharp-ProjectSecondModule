using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static Project.Utils.Const;

namespace Project
{
    /// <summary>
    /// Класс, преобразовывающий данные, прочитанные из файла, в массив объекто типа <see cref="Student"/>
    /// </summary>
    public class StudentDataInput
    {
        /// <summary>
        /// Путь к файлу с данными студентов.
        /// </summary>
        private string _filePath;

        /// <summary>
        /// Свойство для изменения пути к файлу
        /// </summary>
        public string FilePath
        {
            set => _filePath = value;
        }

        /// <summary>
        /// Создает экземпляр класса <see cref="StudentDataInput"/>.
        /// </summary>
        /// <param name="filePath">Путь к файлу, содержащему данные студентов.</param>
        public StudentDataInput(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Читает данные из файла, проверяет их корректность и преобразует в список объектов <see cref="Student"/>.
        /// </summary>
        /// <returns>Список объектов <see cref="Student"/>.</returns>
        /// <exception cref="ArgumentException">Если структура файла неверная или отсутствуют корректные данные.</exception>
        public List<Student> ReadFile()
        {
            List<Student> students = new List<Student>();

            // Читаем содержимое файла построчно
            using StreamReader reader = new StreamReader(_filePath); // испозую ключевое слово using, для автоматического закрытия потока
            string[] lines = reader.ReadToEnd().Split("\n");

            // Проверяем корректность структуры файла
            IsCorrectFileStructure(lines);

            // Преобразуем строки файла в объекты Student
            for (int i = 1; i < lines.Length; i++) // Пропускаем заголовок
            {
                if (ConvertStringToStudent(lines[i], out Student temp))
                {
                    students.Add(temp);
                }
            }
            
            if (students.Count == 0)
            {
                throw new ArgumentException("В файле нет корректных данных. Введите, пожалуйста, другой.");
            }

            return students;
        }

        /// <summary>
        /// Преобразует строку файла в объект <see cref="Student"/>.
        /// </summary>
        /// <param name="line">Строка с данными студента.</param>
        /// <param name="student">Ссылка на объект <see cref="Student"/>, который нужно инициализировать.</param>
        /// <returns>True, если строка успешно преобразована, иначе False.</returns>
        private bool ConvertStringToStudent(string line, out Student student)
        {
            string[] data = Regex.Split(line, "[,;]\""); // сплитим по ," или ;"

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i].Replace('\"', ' ').Trim(); // очищаем поля от ненужных символов 
            }

            if (!IsValidLine(data))
            {
                student = new Student(0,0,0,"","","","","");
                return false;
            }
            // Заполняем свойства объекта Student
            long tempMathScore, tempReadingScore, tempWritingScore;
            ConvertStringToLong(data[5], out tempMathScore);
            ConvertStringToLong(data[6], out tempReadingScore);
            ConvertStringToLong(data[7], out tempWritingScore);

            student =  new(tempMathScore, tempReadingScore, tempWritingScore, data[0], data[1], data[2], data[3],
                data[4]);
            return true;
        }

        /// <summary>
        /// Проверяет корректность структуры файла
        /// </summary>
        /// <param name="lines">Массив строк файла.</param>
        /// <exception cref="ArgumentException">Если структура файла некорректная.</exception>
        private void IsCorrectFileStructure(string[] lines)
        {
            bool isCorrect = true;

            string[] headers = lines[0].Split(new[] { ',', ';' });

            try
            {
                for (int i = 0; i < DefaultHeaders.Length; i++)
                {
                    isCorrect &= headers[i].Replace('\"', ' ').Trim().Equals(DefaultHeaders[i]); // сверяет заголоки, если заголоки в этом файле не совпадают,
                                                                                                 // то файл явно не корректна
                    if (!isCorrect) { break; }
                }
            }
            catch (IndexOutOfRangeException)
            {
                isCorrect = false;
            }

            if (!isCorrect)
            {
                throw new ArgumentException("Введен файл неверной структуры.");
            }
        }

        /// <summary>
        /// Проверяет корректность строк данных
        /// </summary>
        /// <param name="line">Массив данных.</param>
        /// <returns>True, если строка корректна, иначе False.</returns>
        private bool IsValidLine(string[] line)
        {
            if (line.Length != CountOfColumns) { return false; }

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].Equals(DefaultHeaders[i])) // сверяет заголоки, если поле строчки данных в этом файле совпадает с заголоком,то строка явно не корректна
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Преобразует строку в число типа <see cref="long"/>. 
        /// Если строка некорректна, устанавливает значение <see cref="long.MinValue"/>.
        /// </summary>
        /// <param name="input">Входная строка.</param>
        /// <param name="temp">Результат преобразования.</param>
        private void ConvertStringToLong(string input, out long temp)
        {
            if (!long.TryParse(input, out temp))
            {
                temp = long.MinValue; // Специальное значение для пустого или некорректного ввода
            }
        }
    }
}
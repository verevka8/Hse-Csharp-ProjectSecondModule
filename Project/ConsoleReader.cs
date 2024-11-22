using System.Text.RegularExpressions;
using static Project.Utils.Const;

namespace Project
{
    /// <summary>
    /// Класс для чтения корректных данных из консоли
    /// </summary>
    public static class ConsoleReader
    {
        /// <summary>
        /// Считывает число из консоли, проверяет его корректность и принадлежность к списку допустимых команд.
        /// </summary>
        /// <returns>Корректное число типа <see cref="double"/>, введенное пользователем.</returns>
        public static double ReadNumber()
        {
            double input;

            // Продолжаем запрашивать ввод, пока пользователь не введет допустимое число
            while (!double.TryParse(Console.ReadLine(), out input) || !Commands.Contains(input))
            {
                Console.WriteLine(IncorrectNumberMessage); // Выводим сообщение об ошибке
            }

            return input;
        }

        /// <summary>
        /// Считывает имя файла из консоли и проверяет его на соответствие допустимому формату.
        /// </summary>
        /// <returns>Корректное имя файла.</returns>
        public static string ReadFileName()
        {
            Console.WriteLine("Введите название файла:");
            string? fileName = Console.ReadLine();

            // Запрашиваем ввод, пока имя файла не пройдет проверку на валидность
            while (fileName == null || !Regex.IsMatch(fileName, "^[0-9a-zA-Zа-яА-Я.,!@%&*]+$"))
            {
                Console.WriteLine("Введите корректное название файла, используя русские и латинские буквы, цифры и знаки: !,.@%&*");
                fileName = Console.ReadLine();
            }

            return fileName;
        }

        /// <summary>
        /// Считывает путь до файла из консоли
        /// </summary>
        /// <returns>Не пустой путь до файла.</returns>
        public static string ReadFilePath()
        {
            string? filePath = Console.ReadLine();

            // Запрашиваем ввод, пока путь будет не пустой
            while (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Введите корректный путь до файла:");
                filePath = Console.ReadLine();
            }

            return filePath;
        }
    }
}

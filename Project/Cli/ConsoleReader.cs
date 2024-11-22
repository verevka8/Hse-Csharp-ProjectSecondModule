using System.Text.RegularExpressions;
using static Project.Const;
namespace Project.Cli
{
    public static class ConsoleReader
    {
        public static double ReadNumber()
        {
            double input;
            while (!double.TryParse(Console.ReadLine(), out input) || input <= 0 || input > CountOfCommands)
            {
                Console.WriteLine(IncorrectNumberMessage);
            }

            return input;
        }
        
        public static string ReadFileName()
        {
            Console.WriteLine("Введите название файла:");
            string? fileName = Console.ReadLine();
            while (fileName == null || !Regex.IsMatch(fileName, "^[0-9a-zA-Zа-яА-Я.,!@%&*]+$"))
            {
                Console.WriteLine("Введите корректное название файла, используя русские и латинские буквы, цифры и знаки: !,.@%&*");
                fileName = Console.ReadLine();
            }

            return fileName;
        }

        public static string ReadFilePath()
        {
            string? filePath = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Введите корректный путь до файла:");
                filePath = Console.ReadLine();
            }
            return filePath;
        }
    }
}
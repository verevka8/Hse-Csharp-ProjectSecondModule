using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using static Project.Const;

namespace Project
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-Us");
            StudentDataIo reader;
            Console.WriteLine(StartMessage);
            ReadFile(out reader);
            while (true)
            {
                Console.WriteLine(MenuMessage);
                Data data = new Data(reader.ReadFile());
                double n = ReadNumber();
                switch (n)
                {
                    case 1d:
                        Console.Clear();
                        Console.WriteLine(StartMessage);
                        ReadFile(out reader);
                        break;
                    case 2d:
                        Cli.PrintData(data.GetStudentsWithCompletedCourse());
                        break;
                    case 2.1d:
                        reader.SaveDataToCsv("Test_Preparation.csv",data.GetStudentsWithCompletedCourse());
                        break;
                    case 3d:
                        Cli.PrintData(data.GetStudentsWithStandardLunch());
                        break;
                    case 4.1d:
                        data.PrintCountOfLine();
                        break;
                    case 4.2d:
                        data.PrintInfoAboutStudentsRace();
                        break;
                    case 4.3d:
                        data.PrintInfoAboutStudentsExamResult();
                        break;
                    case 5d:
                        Cli.PrintData(data.GetFemaleStudents(),true);
                        break;
                    case 5.1d:
                        reader.SaveDataToCsv(ReadFileName() + ".csv",data.GetFemaleStudents(),true);
                        break;
                    case 6d:
                        return;
                }
            }
        }

        private static double ReadNumber()
        {
            double input;
            while (!double.TryParse(Console.ReadLine(), out input) || input <= 0 || input > CountOfCommands)
            {
                Console.WriteLine(IncorrectNumberMessage);
            }

            return input;
        }

        private static void ReadFile(out StudentDataIo reader)
        {
            while (true)
            {
                try
                {
                    string filePath = Console.ReadLine();
                    reader = new StudentDataIo(filePath);
                    break;
                }
                catch (IOException)
                {
                    Console.WriteLine("Введите путь до корректного файла.");
                }
            }
        }

        private static string ReadFileName()
        {
            Console.WriteLine("Введите название файла:");
            string fileName = Console.ReadLine();
            while (!Regex.IsMatch(fileName, "^[0-9a-zA-Zа-яА-Я.,!@%&*]+$"))
            {
                Console.WriteLine("Введите корректное название файла, используя русские и латинские буквы, цифры и знаки: !,.@%&*");
                fileName = Console.ReadLine();
            }

            return fileName;
        }
    }
}
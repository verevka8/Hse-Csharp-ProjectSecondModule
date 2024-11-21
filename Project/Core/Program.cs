using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
                DataAnalyzer dataAnalyzer = new DataAnalyzer(reader.ReadFile());
                double n = ReadNumber();
                switch (n)
                {
                    case 1d:
                        Console.Clear();
                        Console.WriteLine(StartMessage);
                        ReadFile(out reader);
                        break;
                    case 2d:
                        Cli.PrintData(dataAnalyzer.GetStudentsWithCompletedCourse());
                        break;
                    case 2.1d:
                        reader.SaveDataToCsv("Test_Preparation.csv",dataAnalyzer.GetStudentsWithCompletedCourse());
                        break;
                    case 3d:
                        Cli.PrintData(dataAnalyzer.GetStudentsWithStandardLunch());
                        break;
                    case 4.1d:
                        Cli.PrintData(dataAnalyzer.GetInfoOfCountCorrectLines());
                        break;
                    case 4.2d:
                        Cli.PrintData(dataAnalyzer.GetInfoAboutStudentsRace());
                        break;
                    case 4.3d:
                        Cli.PrintData(dataAnalyzer.GetInfoAboutStudentsExamResult());
                        break;
                    case 5d:
                        Cli.PrintData(dataAnalyzer.GetFemaleStudents(),true);
                        break;
                    case 5.1d:
                        reader.SaveDataToCsv(ReadFileName() + ".csv",dataAnalyzer.GetFemaleStudents(),true);
                        break;
                    case 6d:
                        Cli.PrintDataWithDelta(dataAnalyzer.GetSortedData());
                        break;
                    case 6.1d:
                        reader.SaveDataToCsv("Sorted_Students.csv",dataAnalyzer.GetSortedData());
                        break;
                    case 7d:
                        return;
                    case 10d:
                        Cli.PrintData(dataAnalyzer.Students); // убрать
                        break;
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
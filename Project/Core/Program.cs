using Project.Cli;
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
            Console.WriteLine(StartMessage);
            
            StudentDataOutput dataOutput = new StudentDataOutput();
            DataAnalyzer dataAnalyzer = new DataAnalyzer(ReadFile());
            while (true)
            {
                Console.WriteLine(MenuMessage);
                
                double n = ConsoleReader.ReadNumber();
                switch (n)
                {
                    case 1d:
                        Console.Clear();
                        Console.WriteLine(StartMessage);
                        dataAnalyzer = new DataAnalyzer(ReadFile()); // изменить на set
                        break;
                    case 2d:
                        DataPrinter.PrintData(dataAnalyzer.GetStudentsWithCompletedCourse());
                        break;
                    case 2.1d:
                        dataOutput.SaveDataToCsv("Test_Preparation.csv",dataAnalyzer.GetStudentsWithCompletedCourse());
                        break;
                    case 3d:
                        DataPrinter.PrintData(dataAnalyzer.GetStudentsWithStandardLunch());
                        break;
                    case 4.1d:
                        DataPrinter.PrintData(dataAnalyzer.GetInfoOfCountCorrectLines());
                        break;
                    case 4.2d:
                        DataPrinter.PrintData(dataAnalyzer.GetInfoAboutStudentsRace());
                        break;
                    case 4.3d:
                        DataPrinter.PrintData(dataAnalyzer.GetInfoAboutStudentsExamResult());
                        break;
                    case 5d:
                        DataPrinter.PrintData(dataAnalyzer.GetFemaleStudents(),true);
                        break;
                    case 5.1d:
                        dataOutput.SaveDataToCsv(ConsoleReader.ReadFileName() + ".csv",dataAnalyzer.GetFemaleStudents(),true);
                        break;
                    case 6d:
                        DataPrinter.PrintDataWithDelta(dataAnalyzer.GetSortedData());
                        break;
                    case 6.1d:
                        dataOutput.SaveDataToCsv("Sorted_Students.csv",dataAnalyzer.GetSortedData());
                        break;
                    case 7d:
                        return;
                    case 10d:
                        DataPrinter.PrintData(dataAnalyzer.Students);
                        break;
                }
            }
        }
        
        public static List<Student> ReadFile()
        {
            StudentDataInput dataInput;
            while (true)
            {
                try
                {
                    dataInput = new StudentDataInput(ConsoleReader.ReadFilePath()); // TODO: исправить
                    List<Student> students = dataInput.ReadFile();
                    return students;
                }
                catch (ArgumentException e)
                {
                    DataPrinter.PrintData(e.Message);

                }
                catch (FileNotFoundException)
                {
                    DataPrinter.PrintData("Введите путь до существующего файла");

                }
                catch (Exception ex) when (ex is DirectoryNotFoundException or IOException
                                               or UnauthorizedAccessException or PathTooLongException
                                               or FileLoadException)
                {
                    DataPrinter.PrintData("Введите существующий путь до корректного файла.");

                }
                catch (Exception ex)
                {
                    DataPrinter.PrintData("Ошибка, повторите снова!");
                }
            }
        }
    }
}
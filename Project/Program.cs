/*
 * Лизунов Даниил Кириллоич
 * Бпи 248
 * Вариант 8
 * 22.11.2024
 */

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static Project.Utils.Const; 

namespace Project
{
    /// <summary>
    ///  Основной класс программы, выполняющий логику взаимодействия с пользователем
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Глобальная точка входа в программу, содержит логику взаимодействия с пользователем
        /// </summary>
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-Us"); // утанавливаем культуру en-Us, чтобы дробные числа представлялись с точкой
            DataPrinter.PrintData(StartMessage);
            
            StudentDataOutput dataOutput = new StudentDataOutput();
            DataAnalyzer dataAnalyzer = new DataAnalyzer(ReadFile());
            while (true)
            {
                try
                {
                    Console.WriteLine(MenuMessage);

                    double n = ConsoleReader.ReadNumber(); // Читаем выбор пользователя 
                    switch (n)
                    {
                        case 1d:
                            // Загрузка данных из нового файла
                            Console.Clear();
                            Console.WriteLine(StartMessage);
                            dataAnalyzer.Students = ReadFile();
                            break;
                        case 2d:
                            // Вывод студентов, завершивших курс
                            DataPrinter.PrintData(dataAnalyzer.GetStudentsWithCompletedCourse());
                            break;
                        case 2.1d:
                            // Сохранение данных о студентах, завершивших курс, в CSV
                            dataOutput.SaveDataToCsv("Test_Preparation.csv",
                                dataAnalyzer.GetStudentsWithCompletedCourse());
                            break;
                        case 3d:
                            // Вывод выборки студентов,5. имеющих стандартный ланч
                            DataPrinter.PrintData(dataAnalyzer.GetStudentsWithStandardLunch());
                            break;
                        case 4.1d:
                            // Вывести колличество корректных строк с данными о студентах
                            DataPrinter.PrintData(dataAnalyzer.GetInfoOfCountCorrectLines());
                            break;
                        case 4.2d:
                            // Вывести информацию о колличество групп и о их численности
                            DataPrinter.PrintData(dataAnalyzer.GetInfoAboutStudentsRace());
                            break;
                        case 4.3d:
                            // Вывести информацию о каждом экзамене: сколько человек написано на более чем 50 баллов
                            DataPrinter.PrintData(dataAnalyzer.GetInfoAboutStudentsExamResult());
                            break;
                        case 5d:
                            // Вывести информацию о студентках и их средний балл по все экзаменам
                            DataPrinter.PrintData(dataAnalyzer.GetFemaleStudents(), true);
                            break;
                        case 5.1d:
                            // Сохранить в файл с пользоввательским назанием информацию о студентках и о их среднем баллом по все экзаменам
                            dataOutput.SaveDataToCsv(ConsoleReader.ReadFileName() + ".csv",
                                dataAnalyzer.GetFemaleStudents(), true);
                            break;
                        case 6d:
                            // Вывести выборку студентов, сгруппированную по полю Lunch и отсортированную по баллам за экзамен по математике
                            // с информацией о разнице между максимальным и минимальным баллом в группе 
                            (List<Student> sortedStudents, Dictionary<string, long> deltaInfo) =
                                dataAnalyzer.GetSortedDataWithDelta();
                            DataPrinter.PrintDataWithDelta(sortedStudents, deltaInfo);
                            break;
                        case 6.1d:
                            // Сохранить в файл выборку студентов, сгруппированную по полю Lunch и отсортированную по баллам за экзамен по математике
                            // с информацией о разнице между максимальным и минимальным баллом в группе 
                            dataOutput.SaveDataToCsv("Sorted_Students.csv", dataAnalyzer.GetSortedData());
                            break;
                        case 7d:
                            // Выход из программы 
                            return;
                    }
                }
                catch (IOException)
                {
                    DataPrinter.PrintData("Возникла ошибка с файлами, попробуйте снова!");
                }
                catch (Exception)
                {
                    DataPrinter.PrintData("Возникла ошибка, попробуйте снова!");
                }
                
            }
        }
        
        /// <summary>
        ///  Чтение файла с данными студентов. Обрабатывает исключения, связанные с проблемами чтения/преобразования файла
        /// </summary>
        /// <returns>непустой массив обьектов типа Student, считанных из файла</returns>
        private static List<Student> ReadFile()
        {
            StudentDataInput dataInput;
            while (true)
            {
                try
                {
                    // Инициализируем ввод данных и читаем студентов из указанного файла
                    dataInput = new StudentDataInput(ConsoleReader.ReadFilePath()); 
                    List<Student> students = dataInput.ReadFile(); // пробуем прочитать файл
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
                catch (Exception)
                {
                    DataPrinter.PrintData("Ошибка, повторите снова!");
                }
            }
        }
    }
}
using Project.Cli;
using System;
using System.Collections.Generic;
using System.Text;
using static Project.Const; 
namespace Project
{
    /// <summary>
    /// Command Line Interface
    /// </summary>
    public static class DataPrinter
    {
        public static void PrintData(List<Student> students, bool includeAverage = false)
        {
            if (includeAverage)
            {
                string[] headers = new string[DefaultHeaders.Length+1];
                Array.Copy(DefaultHeaders,headers,DefaultHeaders.Length); // TODO: подумать, можно ли оптимизировать
                headers[^1] = "Average";
                PrintData(students,headers,true);
            }
            else
            {
                PrintData(students,DefaultHeaders);
            }
        }

        public static void PrintData(string message)
        {
            Console.WriteLine(message);
        }

        private static void PrintData(List<Student> students, string[] headers,  bool includeAverage = false)
        {
            DataFormatter.СalculateСolumnSize(out int[] lenOfColumns, students,headers);
            StringBuilder header = new StringBuilder();
            
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
                header.Append(DataFormatter.TextCentering(headers[i], lenOfColumns[i]) + "  ");
            }
            Console.WriteLine("\n" + header);
            
            foreach (Student student in students)
            {
                Console.WriteLine(DataFormatter.GetStringFormatOfStudent(lenOfColumns, student.GetStudentFields(includeAverage)));
            }
            Console.WriteLine();
        }
        
        public static void PrintDataWithDelta(List<Student> sortedStudents)
        {
            DataFormatter.СalculateСolumnSize(out int[] lenOfColumns, sortedStudents,DefaultHeaders);
            int saveIndexOfMinimum = 0;
            
            Dictionary<string, long> dictionary = new Dictionary<string, long>();
            dictionary.Add(sortedStudents[0].LunchType,0);
            
            for (int i = 0; i < sortedStudents.Count-1; i++)
            {
                if (!dictionary.ContainsKey(sortedStudents[i+1].LunchType))
                {
                    dictionary[sortedStudents[i].LunchType] = sortedStudents[i].MathScore - sortedStudents[saveIndexOfMinimum].MathScore; //TODO: вынести в другой класс
                    saveIndexOfMinimum = i+1;
                    dictionary.Add(sortedStudents[i+1].LunchType,0);
                }
            }
            dictionary[sortedStudents[^1].LunchType] = sortedStudents[^1].MathScore - sortedStudents[saveIndexOfMinimum].MathScore;
            
            StringBuilder header = new StringBuilder();
            
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
                header.Append(DataFormatter.TextCentering(DefaultHeaders[i], lenOfColumns[i]) + "  ");
            }
            Console.WriteLine("\n" + header);

            string currentLucnhType = "\n"; // присваиваем служебный символ, который невозможно ввести с консоли, чтобы изначальный currentLucnhType не был равен любому введенному LucnhType
            for (int i = 0; i < sortedStudents.Count;i++)
            {
                if (sortedStudents[i].LunchType != currentLucnhType)
                {
                    Console.WriteLine($"\nВ выборке с LunchType: \"{sortedStudents[i].LunchType}\" - разница между максимальным и минимальным результатом по математике составляет: {dictionary[sortedStudents[i].LunchType]}.");
                    currentLucnhType = sortedStudents[i].LunchType;
                }
                Console.WriteLine(DataFormatter.GetStringFormatOfStudent(lenOfColumns, sortedStudents[i].GetStudentFields()));
            }
            Console.WriteLine();
        }
    }
} 
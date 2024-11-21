using System;
using System.Collections.Generic;
using System.Text;
using static Project.Const; 
namespace Project
{
    /// <summary>
    /// Command Line Interface
    /// </summary>
    public static class Cli
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
            СalculateСolumnSize(out int[] lenOfColumns, students,headers);
            StringBuilder header = new StringBuilder();
            
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
                header.Append(AddSpaces(headers[i], lenOfColumns[i]) + "  ");
            }
            Console.WriteLine("\n" + header);
            
            foreach (Student student in students)
            {
                Console.WriteLine(GetStringFormatOfStudent(lenOfColumns, student.GetStudentFields(includeAverage)));
            }
            Console.WriteLine();
        }
        
        public static void PrintDataWithDelta(List<Student> sortedStudents)
        {
            СalculateСolumnSize(out int[] lenOfColumns, sortedStudents,DefaultHeaders);
            int saveIndexOfMinimum = 0;
            
            Dictionary<string, long> dictionary = new Dictionary<string, long>();
            dictionary.Add(sortedStudents[0].LunchType,0);
            
            for (int i = 0; i < sortedStudents.Count; i++)
            {
                if (i == sortedStudents.Count-1 || !dictionary.ContainsKey(sortedStudents[i+1].LunchType))
                {
                    dictionary[sortedStudents[i].LunchType] = sortedStudents[i].MathScore - sortedStudents[saveIndexOfMinimum].MathScore;
                    saveIndexOfMinimum = i;
                }
            }
            
            StringBuilder header = new StringBuilder();
            
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
                header.Append(AddSpaces(DefaultHeaders[i], lenOfColumns[i]) + "  ");
            }
            Console.WriteLine("\n" + header);

            string currentLucnhType = "";
            for (int i = 0; i < sortedStudents.Count;i++)
            {
                if (sortedStudents[i].LunchType != currentLucnhType)
                {
                    Console.WriteLine($"\nВ выборке с LunchType: {sortedStudents[i].LunchType} - разница между максимальным и минимальным результатом по математике составляет: {dictionary[sortedStudents[i].LunchType]}.");
                    currentLucnhType = sortedStudents[i].LunchType;
                }
                Console.WriteLine(GetStringFormatOfStudent(lenOfColumns, sortedStudents[i].GetStudentFields()));
            }
            Console.WriteLine();
        }

        private static string GetStringFormatOfStudent(int[] lenOfColumns, string[] studentFields)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
               result.Append(AddSpaces(studentFields[i],lenOfColumns[i]) + "  ");
            }

            return result.ToString();
        }

        private static string AddSpaces(string s, int count)
        {
            count -= s.Length;
            return new string(' ', count / 2) + s + new string(' ', (count+1)/2);
        }

        private static void СalculateСolumnSize(out int[] lenOfColumns, List<Student> students, string[] headers)
        {
            lenOfColumns = new int[headers.Length];
            
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
                lenOfColumns[i] = headers[i].Length;
            }
            
            for (int i = 0; i < students.Count; i++)
            {
                string[] studentFields = students[i].GetStudentFields();
                for (int j = 0; j < lenOfColumns.Length; j++)
                {
                    lenOfColumns[j] = Math.Max(studentFields[j].Length, lenOfColumns[j]);
                }
            }
        }
    }
} 
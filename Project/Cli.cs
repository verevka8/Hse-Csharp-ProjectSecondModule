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
            for (int i = 0; i < students.Count; i++)
            {
                string[] studentFields = students[i].GetStudentFields();
                for (int j = 0; j < lenOfColumns.Length; j++)
                {
                    lenOfColumns[j] =
                        Math.Max(studentFields[j].Length, headers[i].Length); //TODO: вынести в другой метод
                }
            }
        }
    }
} 
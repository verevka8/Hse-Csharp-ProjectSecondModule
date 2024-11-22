using System.Text;
using static Project.Const;
namespace Project.Cli
{
    /// <summary>
    /// Класс содержащий вспомогательные методы форматирования данных
    /// </summary>
    public static class DataFormatter
    {
        public static string GetStringFormatOfStudent(int[] lenOfColumns, string[] studentFields)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
                result.Append(TextCentering(studentFields[i],lenOfColumns[i]) + "  ");
            }

            return result.ToString();
        }

        public static string TextCentering(string s, int count)
        {
            count -= s.Length;
            if (count < 0)
            {
                throw new ArgumentException();
            }
            return new string(' ', count / 2) + s + new string(' ', (count+1)/2);
        }

        public static void СalculateСolumnSize(out int[] lenOfColumns, List<Student> students, string[] headers)
        {
            lenOfColumns = new int[headers.Length];
            
            for (int i = 0; i < lenOfColumns.Length; i++)
            {
                lenOfColumns[i] = headers[i].Length;
            }
            
            for (int i = 0; i < students.Count; i++)
            {
                string[] studentFields = students[i].GetStudentFields(headers.Length==DefaultHeadersWithAverage.Length);
                for (int j = 0; j < lenOfColumns.Length; j++)
                {
                    lenOfColumns[j] = Math.Max(studentFields[j].Length, lenOfColumns[j]);
                }
            }
        }
    }
}
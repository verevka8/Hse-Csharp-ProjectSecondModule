using System.Text;
using static Project.Const;
namespace Project
{
    public class StudentDataOutput
    {
        public void SaveDataToCsv(string fileName, List<Student> data, bool includeAverage = false)
        {
            StringBuilder csvLines = new StringBuilder();
            csvLines.Append(ConvertDataToString(includeAverage ? DefaultHeadersWithAverage : DefaultHeaders));
            string[] fields;
            foreach (Student item in data)
            {
                fields = item.GetStudentFields(includeAverage);
                csvLines.Append(ConvertDataToString(fields));
            }

            File.WriteAllText(fileName, csvLines.ToString(), Encoding.UTF8);
        }

        private string ConvertDataToString(string[] data)
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                output.Append("\"" + data[i] + "\"" + (i != data.Length - 1 ? "," : "\n"));
            }

            return output.ToString();
        }
    }
}
using System.Text;
using System.Text.RegularExpressions;
using static Project.Const;

namespace Project
{
    public class StudentDataIo
    {
        private string _filePath;
        
        public StudentDataIo(string filePath)
        {
            _filePath = filePath;
        }
        
        public List<Student> ReadFile()
        {
            List<Student> students = new List<Student>();
            string[] lines = File.ReadAllLines(_filePath);
            IsCorrectFileStructure(lines);
            for (int i = 1; i < lines.Length; i++)
            {
                if (ConvertStringToStudent(lines[i], out Student temp))
                {
                    students.Add(temp);
                }
            }

            return students;
        }

        private bool ConvertStringToStudent(string line, out Student student)
        {
            student = new Student();
            string[] data = line.Split([',',';']);
            if (!IsVaildLine(data)) { return false; }
            
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i].Replace('\"', ' ').Trim();
            }

            student.Gender = data[0];
            student.Race = data[1];
            student.LevelOfEducation = data[2];
            student.LunchType = data[3];
            student.TestPreparationCourse = data[4];

            long temp;
            ConvertStringToLong(data[5], out temp);
            student.MathScore = temp;
            ConvertStringToLong(data[6], out temp);
            student.ReadingScore = temp;
            ConvertStringToLong(data[7], out temp);
            student.WritingScore = temp;

            return true;
        }

        private void IsCorrectFileStructure(string[] lines)
        {
            bool isCorrect = true;
            string[] headers = lines[0].Split([',', ';']);
            try
            {
                for (int i = 0; i < DefaultHeaders.Length; i++)
                {
                    isCorrect &= headers[i].Replace('\"',' ').Trim().Equals(DefaultHeaders[i]);
                    if (!isCorrect){break;}
                }
            }
            catch (IndexOutOfRangeException)
            {
                isCorrect = false;
            }

            if (!isCorrect) { throw new ArgumentException("Введен файл неверной структуры"); }
        }

        private bool IsVaildLine(string[] line)
        {
            if (line.Length != CountOfColumns) { return false;}

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].Replace('\"', ' ').Trim().Equals(DefaultHeaders[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private void ConvertStringToLong(string input, out long output)
        {
            if (!long.TryParse(input, out output))
            {
                output = long.MinValue; // служебное значение означающее, что на вход мы получили некорректное значение и далее будем считать, что это поле пустое
            }
        }

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
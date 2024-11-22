using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static Project.Const;

namespace Project
{
    public class StudentDataInput
    {
        private string _filePath;

        public string FilePath
        {
            set => _filePath = value; //TODO: как-то реализовать 
        }
        
        public StudentDataInput(string filePath)
        {
            _filePath = filePath;
        }
        
        public List<Student> ReadFile()
        {
            List<Student> students = new List<Student>();
            // string[] lines = File.ReadAllLines(_filePath);
            StreamReader reader = new StreamReader(_filePath);
            string[] lines = reader.ReadToEnd().Split("\n");
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
            bool isNumbersCorrect = true;
            isNumbersCorrect&=long.TryParse(data[5], out temp);
            student.MathScore = temp;
            isNumbersCorrect&=long.TryParse(data[6], out temp);
            student.ReadingScore = temp;
            isNumbersCorrect&=long.TryParse(data[7], out temp); // TODO: доделать, пустота = minvalue, потом ее учитывать как 0 в фильтрах
            student.WritingScore = temp;

            return isNumbersCorrect;
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
    }
}
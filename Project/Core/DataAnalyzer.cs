using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class DataAnalyzer
    {
        private List<Student> _students;

        public List<Student> Students => _students; //TODO: убрать

        public DataAnalyzer(List<Student> students)
        {
            _students = students;
        }
        
        public List<Student> GetStudentsWithCompletedCourse()
        {
            return GetStudentsByParameter(student => student.TestPreparationCourse, "completed");
        }

        public List<Student> GetStudentsWithStandardLunch()
        {
            return GetStudentsByParameter(student => student.LunchType, "standard");
        }

        public string GetInfoOfCountCorrectLines()
        {
            return "Количество строк с данными о студентах: " + _students.Count + "\n";
        }

        public string GetInfoAboutStudentsRace()
        {
            Dictionary<string, long> dictionaryOfRace = new Dictionary<string, long>();
            foreach (Student item in _students)
            {
                if (!dictionaryOfRace.TryAdd(item.Race, 1))
                {
                    dictionaryOfRace[item.Race] += 1;
                }
            }

            StringBuilder output = new StringBuilder($"Всего существует {dictionaryOfRace.Count} разных этнических групп:\n");
            foreach (string race in dictionaryOfRace.Keys)
            {
                output.Append($"\t - Группе {race} принадлежит {dictionaryOfRace[race]} студентов\n");
            }

            return output.ToString();
        }

        public string GetInfoAboutStudentsExamResult()
        {
            
            Dictionary<string, long> dictionaryOfExam = new Dictionary<string, long>();
            dictionaryOfExam.Add("math",0);
            dictionaryOfExam.Add("reading",0);
            dictionaryOfExam.Add("writing",0);
            object[][] examsField;
            foreach (Student item in _students)
            {
                examsField= new[] {
                    new object[] {"math", "reading", "writing"},
                    new object[] { item.MathScore, item.ReadingScore, item.WritingScore }
                };
                for (int i = 0; i < examsField[0].Length; i++)
                {
                    dictionaryOfExam[(string)examsField[0][i]] += (long)examsField[1][i] > 50 ? 1 : 0;
                }
            }

            StringBuilder output = new StringBuilder();
            foreach (string nameOfExam in dictionaryOfExam.Keys)
            {
                output.Append(
                    $"Экзамен по {nameOfExam} написало {dictionaryOfExam[nameOfExam]} студентов на более чем 50 баллов\n");
            }

            return output.ToString();
        }
        public List<Student> GetFemaleStudents()
        {
            return GetStudentsByParameter(student => student.Gender, "female");
        }

        public List<Student> GetSortedData()
        {
            List<Student> students = DeepCopy(_students);
            QuickSort.QuickSortRecursive(students, (student1,student2) =>
            {
                int firstComparison  = string.CompareOrdinal(student1.LunchType, student2.LunchType);
                return firstComparison!=0? firstComparison:student1.MathScore.CompareTo(student2.MathScore);
                
            }, 0,students.Count -1);
            return students;
        }
        
        private List<Student> GetStudentsByParameter(Func<Student,string> func, string desiredValue)
        {
            List<Student> outputList = new List<Student>();
            foreach (Student item in _students)
            {
                if (func(item).Equals(desiredValue))
                {
                    outputList.Add(item.Clone());
                }
            }

            return outputList;
        }

        private List<Student> DeepCopy(List<Student> students)
        {
            List<Student> output = new List<Student>();
            foreach (Student item in students)
            {
                output.Add(item.Clone());
            }

            return output;
        }
    }
}
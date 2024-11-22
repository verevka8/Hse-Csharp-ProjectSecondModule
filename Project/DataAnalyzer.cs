using Project.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    /// <summary>
    /// Класс для анализа данных о студентах.
    /// Содержит методы для фильтрации, сортировки и получения статистики по данным.
    /// </summary>
    public class DataAnalyzer
    {
        private List<Student> _students;

        /// <summary>
        /// Свойство для доступа к списку студентов. Устанавливает глубокую копию входного списка.
        /// </summary>
        public List<Student> Students
        {
            set => _students = DeepCopy(value);
        }

        /// <summary>
        /// Конструктор для инициализации DataAnalyzer с данными о студентах.
        /// </summary>
        /// <param name="students">Список студентов.</param>
        public DataAnalyzer(List<Student> students)
        {
            _students = students;
        }

        /// <summary>
        /// Получает студентов, прошедших курс подготовки к тестированию.
        /// </summary>
        /// <returns>Список студентов с курсом "completed".</returns>
        public List<Student> GetStudentsWithCompletedCourse()
        {
            return GetStudentsByParameter(student => student.TestPreparationCourse, "completed");
        }

        /// <summary>
        /// Получает студентов с "standard" типом обеда.
        /// </summary>
        /// <returns>Список студентов с типом обеда "standard".</returns>
        public List<Student> GetStudentsWithStandardLunch()
        {
            return GetStudentsByParameter(student => student.LunchType, "standard");
        }

        /// <summary>
        /// Возвращает информацию о количестве строк с корректными данными.
        /// </summary>
        /// <returns>Строка с количеством студентов.</returns>
        public string GetInfoOfCountCorrectLines()
        {
            return "Количество строк с данными о студентах: " + _students.Count + "\n";
        }

        /// <summary>
        /// Получает информацию о студентах, сгруппированных по расе.
        /// </summary>
        /// <returns>Строка с распределением студентов по расам.</returns>
        public string GetInfoAboutStudentsRace()
        {
            Dictionary<string, long> dictionaryOfRace = new();
            foreach (Student item in _students)
            {
                if (!dictionaryOfRace.TryAdd(item.Race, 1))
                {
                    dictionaryOfRace[item.Race] += 1;
                }
            }

            StringBuilder output = new($"Всего существует {dictionaryOfRace.Count} разных этнических групп:\n");
            foreach (string race in dictionaryOfRace.Keys)
            {
                output.Append($"\t - Группе {race} принадлежит {dictionaryOfRace[race]} студентов\n");
            }

            return output.ToString();
        }

        /// <summary>
        /// Получает информацию о результатах экзаменов.
        /// </summary>
        /// <returns>Строка с количеством студентов, набравших более 50 баллов по каждому из экзаменов.</returns>
        public string GetInfoAboutStudentsExamResult()
        {
            Dictionary<string, long> dictionaryOfExam = new()
            {
                { "math", 0 },
                { "reading", 0 },
                { "writing", 0 }
            };

            foreach (Student item in _students)
            {
                dictionaryOfExam["math"] += item.MathScore > 50 ? 1 : 0;
                dictionaryOfExam["reading"] += item.ReadingScore > 50 ? 1 : 0;
                dictionaryOfExam["writing"] += item.WritingScore > 50 ? 1 : 0;
            }

            StringBuilder output = new();
            foreach (string exam in dictionaryOfExam.Keys)
            {
                output.Append($"Экзамен по {exam} написало {dictionaryOfExam[exam]} студентов на более чем 50 баллов\n");
            }

            return output.ToString();
        }

        /// <summary>
        /// Получает список студенток (женского пола).
        /// </summary>
        /// <returns>Список студенток.</returns>
        public List<Student> GetFemaleStudents()
        {
            return GetStudentsByParameter(student => student.Gender, "female");
        }

        /// <summary>
        /// Получает отсортированные данные о студентах и разницу в баллах между группами по типу обеда.
        /// </summary>
        /// <returns>Кортеж: список отсортированных студентов и словарь с дельтой для каждой группы.</returns>
        public (List<Student> sortedStudents, Dictionary<string, long> deltaInfo) GetSortedDataWithDelta()
        {
            List<Student> students = GetSortedData();
            return (students, GetInfoOfDeltaEachGroup(students));
        }

        /// <summary>
        /// Сортирует список студентов по типу обеда, затем по баллам по математике.
        /// </summary>
        /// <returns>Отсортированный список студентов.</returns>
        public List<Student> GetSortedData()
        {
            List<Student> students = DeepCopy(_students);
            QuickSort.QuickSortRecursive(students, (student1, student2) =>
            {
                int firstComparison = string.CompareOrdinal(student1.LunchType, student2.LunchType);
                return firstComparison != 0 ? firstComparison : student1.MathScore.CompareTo(student2.MathScore);
            }, 0, students.Count - 1);
            return students;
        }

        /// <summary>
        /// Вычисляет разницу в баллах по математике между студентами внутри каждой группы по типу обеда.
        /// </summary>
        /// <param name="students">Отсортированный список студентов.</param>
        /// <returns>Словарь с разницей баллов для каждой группы.</returns>
        private Dictionary<string, long> GetInfoOfDeltaEachGroup(List<Student> students)
        {
            int saveIndexOfMinimum = 0;
            Func<Student, long> format = student => student.MathScore == long.MinValue ? 0 : student.MathScore;
            Dictionary<string, long> dictionary = new()
            {
                { students[0].LunchType, 0 }
            };

            for (int i = 0; i < students.Count - 1; i++)
            {
                if (!dictionary.ContainsKey(students[i + 1].LunchType))
                {
                    dictionary[students[i].LunchType] = format(students[i]) - format(students[saveIndexOfMinimum]);
                    saveIndexOfMinimum = i + 1;
                    dictionary.Add(students[i + 1].LunchType, 0);
                }
            }

            dictionary[students[^1].LunchType] = format(students[^1]) - format(students[saveIndexOfMinimum]);
            return dictionary;
        }

        /// <summary>
        /// Фильтрует студентов по заданному параметру.
        /// </summary>
        /// <param name="func">Функция для получения значения параметра.</param>
        /// <param name="desiredValue">Желаемое значение параметра.</param>
        /// <returns>Список студентов, удовлетворяющих условию.</returns>
        private List<Student> GetStudentsByParameter(Func<Student, string> func, string desiredValue)
        {
            List<Student> outputList = new();
            foreach (Student item in _students)
            {
                if (func(item).Equals(desiredValue))
                {
                    outputList.Add(item.Clone());
                }
            }

            return outputList;
        }

        /// <summary>
        /// Создает глубокую копию списка студентов.
        /// </summary>
        /// <param name="students">Исходный список студентов.</param>
        /// <returns>Глубокая копия списка.</returns>
        private List<Student> DeepCopy(List<Student> students)
        {
            List<Student> output = new();
            foreach (Student item in students)
            {
                output.Add(item.Clone());
            }

            return output;
        }
    }
}

using System;

namespace Project
{
    /// <summary>
    /// Класс обертка для представления одной строчки данных файла с информацией о студентах
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Пол студента
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Раса/этническая принадлежность студента.
        /// </summary>
        public string Race { get; set; }

        /// <summary>
        /// Уровень образования студента.
        /// </summary>
        public string LevelOfEducation { get; set; }

        /// <summary>
        /// Тип питания 
        /// </summary>
        public string LunchType { get; set; }

        /// <summary>
        /// Прошел ли студент подготовительный курс перед экзаменом.
        /// </summary>
        public string TestPreparationCourse { get; set; }

        // Приватные поля для хранения оценок
        private long _mathScore;
        private long _readingScore;
        private long _writingScore;

        /// <summary>
        /// Оценка по математике. 
        /// </summary>
        public long MathScore
        {
            get => _mathScore;
            set => _mathScore = value is long.MinValue or > 0L ? value : 0; // Если значение отрицательно (minValue не считается), то будет установлено 0
        }

        /// <summary>
        /// Оценка по reading.
        /// </summary>
        public long ReadingScore
        {
            get => _readingScore;
            set => _readingScore = value is long.MinValue or > 0L ? value : 0; // Если значение отрицательно (minValue не считается), то будет установлено 0
        }

        /// <summary>
        /// Оценка по writing.
        /// </summary>
        public long WritingScore
        {
            get => _writingScore;
            set => _writingScore = value is long.MinValue or > 0L ? value : 0; // Если значение отрицательно (minValue не считается), то будет установлено 0
        }

        /// <summary>
        /// Конструк для инициализации обьекта
        /// </summary>
        /// <param name="mathScore">Балл по математике</param>
        /// <param name="readingScore">Балл по чтению</param>
        /// <param name="writingScore">Балл по writing</param>
        /// <param name="gender">Гендер</param>
        /// <param name="race">Этническая группа</param>
        /// <param name="levelOfEducation">Уровень образования</param>
        /// <param name="lunchType">Тип обеда</param>
        /// <param name="testPreparationCourse">Как выполнен курс подготовки к тестироанию</param>
        public Student(long mathScore, long readingScore, long writingScore, string gender, string race, string levelOfEducation, string lunchType, string testPreparationCourse)
        {
            // Извини за говнокод в виде конструктора с множеством аргументов, не успел написать паттерн билдер, чтобы гарантироать не null значения  полях
            _mathScore = mathScore;
            _readingScore = readingScore;
            _writingScore = writingScore;
            Gender = gender;
            Race = race;
            LevelOfEducation = levelOfEducation;
            LunchType = lunchType;
            TestPreparationCourse = testPreparationCourse;
        }

        /// <summary>
        /// Создает глубокую копию объекта <see cref="Student"/>.
        /// </summary>
        /// <returns>Копия объекта <see cref="Student"/>.</returns>
        public Student Clone()
        {
            Student clone = new(MathScore, ReadingScore, WritingScore, Gender, Race, LevelOfEducation, LunchType,
                TestPreparationCourse);
            return clone;
        }

        /// <summary>
        /// Вычисляет средний балл по трем экзаменам
        /// </summary>
        /// <returns>
        /// Средний балл, если все оценки валидны; 
        /// ставит служебное значение <c>long.MinValue</c>, если хотя бы одна оценка некорректна.
        /// </returns>
        private double GetAverage()
        {
            return Math.Min(_mathScore, Math.Min(_readingScore, _writingScore)) == long.MinValue
                ? long.MinValue
                : (_mathScore + _readingScore + _writingScore) / 3d;
        }

        /// <summary>
        /// Возвращает массив строк, представляющий данные студента. 
        /// Может включать средний балл, если <paramref name="includeAverage"/> равно <c>true</c>.
        /// </summary>
        /// <param name="includeAverage">Указывает, нужно ли включать средний балл в массив данных.</param>
        /// <returns>Массив строк с данными студента.</returns>
        public string[] GetStudentFields(bool includeAverage = false)
        {
            // Формируем массив данных
            string[] fields = new[]
            {
                Gender, 
                Race, 
                LevelOfEducation, 
                LunchType,
                TestPreparationCourse,
                MathScore == long.MinValue ? "" : MathScore.ToString(), 
                ReadingScore == long.MinValue ? "" : ReadingScore.ToString(), // если целочисленные поля равны служебному значению, то заменяем их на пустоту
                WritingScore == long.MinValue ? "" : WritingScore.ToString(),
            };

            // Если включен расчет среднего балла, расширяем массив
            if (includeAverage)
            {
                Array.Resize(ref fields, fields.Length + 1);
                fields[^1] = $"{GetAverage():F2}"; // Добавляем средний балл с двумя знаками после запятой
            }

            return fields;
        }
    }
}
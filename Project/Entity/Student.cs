using System;

namespace Project
{
    public class Student
    {
        public string Gender { get; set;}
        public string Race { get; set;}
        public string LevelOfEducation { get; set;}
        public string LunchType { get; set;}
        public string TestPreparationCourse { get; set; }
        private long _mathScore;
        private long _readingScore;
        private long _writingScore;

        public long MathScore
        {
            get => _mathScore;
            set => _mathScore = value is long.MinValue or > 0L? value : 0;
        }

        public long ReadingScore
        {
            get => _readingScore;
            set => _readingScore = value is long.MinValue or > 0L? value : 0;
        }
        public long WritingScore
        {
            get => _writingScore;
            set => _writingScore = value is long.MinValue or > 0L? value : 0;
        }

        public Student Clone()
        {
            Student clone = new()
            {
                Gender = Gender, Race = Gender, LevelOfEducation = LevelOfEducation, LunchType = LunchType,
                TestPreparationCourse = TestPreparationCourse,
                MathScore = MathScore,
                ReadingScore = ReadingScore,
                WritingScore = WritingScore
            };
            return clone;
        }

        private double GetAverage()
        {
            return Math.Min(_mathScore,Math.Min(_readingScore,_writingScore)) == long.MinValue?long.MinValue:(_mathScore + _readingScore + _writingScore) / 3d; 
        }

        public string[] GetStudentFields(bool includeAverage = false)
        {
            string[] fields  = new[]
            {
                Gender, Race, LevelOfEducation, LunchType,
                TestPreparationCourse,
                MathScore == long.MinValue ? "" : MathScore.ToString(),
                ReadingScore == long.MinValue ? "" : ReadingScore.ToString(),
                WritingScore == long.MinValue ? "" : WritingScore.ToString(),
            };
            if (includeAverage)
            {
                Array.Resize(ref fields, fields.Length+1);
                fields[^1] = $"{GetAverage():F2}";
            }
            return fields;
        }
    }
}
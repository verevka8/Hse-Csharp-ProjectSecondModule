namespace Project.Utils
{
    /// <summary>
    /// Класс констант
    /// </summary>
    public static class Const
    {
        public const string StartMessage = "Для работы со справочной системой, введите путь до файла с данными:";
        public const string MenuMessage = "Введите номер пункта меню для запуска действий: \n" +
                                          "\t 1) Загрузить новые данные из файла\n" +
                                          "\t 2) Вывести информацию о всех студентах, завершивших курс\n" +
                                          "\t 2.1) Сохранить в файл информацию о всех студентах, завершивших курс\n" +
                                          "\t 3) Вывести информацию о всех студентах, имеющих стандартный ланч\n" +
                                          "\t 4.1) Вывести колличество корректных строк с информацией о студентах\n" +
                                          "\t 4.2) Вывести информацию об этнических группах студентов\n" +
                                          "\t 4.3) Вывести информацию о колличестве студентов, сдавщих конкретный экзамен более чем на 50 баллов\n" +
                                          "\t 5) Вывести информацию о всех студентках и о их средней оценке\n" +
                                          "\t 5.1) Сохранить в файл информацию о всех студентках и о их средней оценке\n" +
                                          "\t 6) Вывести информацию о всех студентах сгруппированных в группы по типу \"lunch\" и внутри групп отсортированных по оценке за экзамен по математике\n" +
                                          "\t 6.1) Сохранить информацию о всхе студентах сгруппированных в группы по типу \"lunch\" и внутри групп отсортированных по оценке за экзамен по математике\n" +
                                          "\t 7) Завершить работу программы";
        public const string IncorrectNumberMessage = "Введите корректный пункт!";
        public static readonly string[] DefaultHeaders = [
            "gender", "race/ethnicity", "parental level of education", "lunch",
            "test preparation course", "math score","reading score","writing score"
        ];
        public static readonly string[] DefaultHeadersWithAverage = [
            "gender", "race/Ethnicity", "parental level of education", "lunch",
            "test preparation course", "math score","reading score","writing score", "average"
        ];

        public const int CountOfColumns = 8;
        public static readonly double[] Commands = [1, 2, 2.1, 3, 4.1, 4.2, 4.3, 5, 5.1, 6, 6.1, 7]; // список всех комманд 
        
    }
}
﻿namespace Project
{
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
                                          "\t 6) Завершить работу программы";
        public const string IncorrectNumberMessage = "Введите корректный пункт!";
        public static readonly string[] DefaultHeaders = [
            "gender", "race/ethnicity", "parental level of education", "lunch",
            "test preparation course", "math score","reading score","writing score"
        ];
        public static readonly string[] DefaultHeadersWithAverage = [
            "gender", "race/Ethnicity", "parental level of education", "lunch",
            "test preparation course", "math score","reading score","writing score", "average"
        ];
        public const int CountOfCommands = 1000; //TODO: переделать, так как не совсем корректно работает из-за дробных пунктов
        public const int CountOfColumns = 8; // TODO: подумать над тем, чтобы убрать
    }
}
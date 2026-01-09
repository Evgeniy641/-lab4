using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        // Устанавливаем кодировку для корректного отображения русских символов
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.WriteLine("=== ТЕСТИРОВАНИЕ ЗАДАНИЙ 1-5 ===\n");

        try
        {
            // Тест задания 1.2 - List
            TestListOperations();

            // Тест задания 2.2 - LinkedList
            TestLinkedListOperations();

            // Тест задания 3.2 - HashSet
            TestHashSetOperations();

            // Тест задания 4.2 - Работа с файлом
            TestFileAnalysis();

            // Тест задания 5.2 - Олимпиада
            TestOlympiad();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при тестировании: {ex.Message}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    #region Задание 1.2 - List

    /// <summary>
    /// Заменяет первое вхождение L1 в L на L2
    /// </summary>
    public static void ReplaceFirstOccurrence<T>(List<T> L, List<T> L1, List<T> L2)
    {
        if (L == null || L1 == null || L2 == null)
            throw new ArgumentNullException("Списки не могут быть null");

        if (L.Count == 0 || L1.Count == 0)
            return;

        // Вывод исходных данных
        Console.WriteLine($"L: [{string.Join(", ", L)}]");
        Console.WriteLine($"L1: [{string.Join(", ", L1)}]");
        Console.WriteLine($"L2: [{string.Join(", ", L2)}]");

        // Поиск первого вхождения L1 в L
        for (int i = 0; i <= L.Count - L1.Count; i++)
        {
            bool found = true;
            for (int j = 0; j < L1.Count; j++)
            {
                if (!EqualityComparer<T>.Default.Equals(L[i + j], L1[j]))
                {
                    found = false;
                    break;
                }
            }

            // Если нашли - заменяем
            if (found)
            {
                L.RemoveRange(i, L1.Count);
                L.InsertRange(i, L2);
                Console.WriteLine($"Результат: [{string.Join(", ", L)}]");
                return;
            }
        }

        Console.WriteLine("Вхождение L1 в L не найдено!");
    }

    static void TestListOperations()
    {
        Console.WriteLine("1.2 List - Замена первого вхождения:");
        List<int> L = new List<int> { 1, 2, 3, 4, 5, 2, 3, 6 };
        List<int> L1 = new List<int> { 2, 3 };
        List<int> L2 = new List<int> { 9, 9, 9 };
        ReplaceFirstOccurrence(L, L1, L2);
    }

    #endregion

    #region Задание 2.2 - LinkedList

    /// <summary>
    /// Сортирует LinkedList по возрастанию
    /// </summary>
    public static void SortLinkedList<T>(LinkedList<T> list) where T : IComparable<T>
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));

        if (list.Count <= 1)
            return;

        Console.WriteLine($"До сортировки: [{string.Join(", ", list)}]");

        // Простой способ: через временный List
        List<T> tempList = new List<T>(list);
        tempList.Sort();

        // Очищаем и заполняем заново
        list.Clear();
        foreach (T item in tempList)
        {
            list.AddLast(item);
        }

        Console.WriteLine($"После сортировки: [{string.Join(", ", list)}]");
    }

    static void TestLinkedListOperations()
    {
        Console.WriteLine("\n2.2 LinkedList - Сортировка:");
        LinkedList<int> list = new LinkedList<int>();
        list.AddLast(5); list.AddLast(2); list.AddLast(8); list.AddLast(1); list.AddLast(4);
        SortLinkedList(list);
    }

    #endregion

    #region Задание 3.2 - HashSet

    /// <summary>
    /// Данные об играх студента
    /// </summary>
    public class StudentGameData
    {
        public string StudentName { get; set; }
        public HashSet<string> Games { get; set; }

        public StudentGameData(string name)
        {
            StudentName = name;
            Games = new HashSet<string>();
        }
    }

    /// <summary>
    /// Анализирует, в какие игры играют студенты
    /// </summary>
    public static void AnalyzeStudentGames(List<StudentGameData> students, HashSet<string> allGames)
    {
        if (students == null || allGames == null)
            throw new ArgumentNullException("Студенты или игры не могут быть null");

        if (students.Count == 0 || allGames.Count == 0)
        {
            Console.WriteLine("Нет данных для анализа");
            return;
        }

        // Вывод исходных данных
        Console.WriteLine("Данные студентов:");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.StudentName}: [{string.Join(", ", student.Games)}]");
        }
        Console.WriteLine($"Все игры: [{string.Join(", ", allGames)}]");

        // 1. Игры, в которые играют все студенты (пересечение)
        HashSet<string> gamesAllPlay = new HashSet<string>(allGames);
        foreach (var student in students)
        {
            gamesAllPlay.IntersectWith(student.Games);
        }

        // 2. Игры, в которые играют некоторые студенты (объединение с фильтром)
        HashSet<string> gamesSomePlay = new HashSet<string>();
        foreach (var student in students)
        {
            gamesSomePlay.UnionWith(student.Games);
        }
        gamesSomePlay.IntersectWith(allGames); // Только из списка allGames

        // 3. Игры, в которые не играет никто (разность)
        HashSet<string> gamesNoOnePlays = new HashSet<string>(allGames);
        gamesNoOnePlays.ExceptWith(gamesSomePlay);

        // Вывод результатов
        Console.WriteLine("\nРезультаты:");
        Console.WriteLine("1. Игры, в которые играют все студенты:");
        PrintHashSet(gamesAllPlay);

        Console.WriteLine("\n2. Игры, в которые играют некоторые студенты:");
        PrintHashSet(gamesSomePlay);

        Console.WriteLine("\n3. Игры, в которые не играет ни один студент:");
        PrintHashSet(gamesNoOnePlays);
    }

    /// <summary>
    /// Выводит HashSet с возможными значениями по умолчанию
    /// </summary>
    private static void PrintHashSet<T>(HashSet<T> set, params T[] defaultItems)
    {
        // Если множество пустое, добавляем значения по умолчанию
        if (set.Count == 0 && defaultItems.Length > 0)
        {
            foreach (var item in defaultItems)
            {
                set.Add(item);
            }
        }

        if (set.Count == 0)
        {
            Console.WriteLine("Нет элементов");
            return;
        }

        foreach (var item in set)
        {
            Console.WriteLine($"- {item}");
        }
    }

    /// <summary>
    /// Создает тестовых студентов
    /// </summary>
    public static List<StudentGameData> CreateTestStudents()
    {
        return new List<StudentGameData>
        {
            new StudentGameData("Иванов") { Games = { "Dota 2", "CS:GO", "Minecraft", "Valorant" } },
            new StudentGameData("Петров") { Games = { "CS:GO", "Valorant", "GTA V", "Minecraft" } },
            new StudentGameData("Сидоров") { Games = { "Dota 2", "Minecraft", "Fortnite", "CS:GO" } },
            new StudentGameData("Козлов") { Games = { "CS:GO", "Valorant", "Minecraft", "Dota 2" } },
            new StudentGameData("Смирнов") { Games = { "CS:GO", "Minecraft", "GTA V", "Fortnite" } }
        };
    }

    /// <summary>
    /// Создает тестовый набор игр
    /// </summary>
    public static HashSet<string> CreateTestGames()
    {
        return new HashSet<string> {
            "Dota 2", "CS:GO", "Valorant", "Minecraft", "GTA V", "Fortnite",
            "World of Warcraft", "Overwatch", "Apex Legends", "League of Legends"
        };
    }

    static void TestHashSetOperations()
    {
        Console.WriteLine("\n3.2 HashSet - Анализ игр студентов:");
        AnalyzeStudentGames(CreateTestStudents(), CreateTestGames());
    }

    #endregion

    #region Задание 4.2 - Работа с файлом

    /// <summary>
    /// Находит глухие согласные, которых нет хотя бы в одном слове текста
    /// </summary>
    public static void PrintVoicelessConsonantsNotInSomeWord(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Файл {filePath} не найден");

        // Чтение файла
        string text = File.ReadAllText(filePath, Encoding.UTF8);

        // Разделители для слов
        char[] separators = { ' ', ',', '.', '!', '?', ';', ':', '\n', '\r', '\t', '(', ')', '[', ']', '{', '}', '"', '-' };
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        // Все глухие согласные русского языка
        HashSet<char> allVoicelessConsonants = new HashSet<char>
        {
            'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч', 'щ',
            'П', 'Ф', 'К', 'Т', 'Ш', 'С', 'Х', 'Ц', 'Ч', 'Щ'
        };

        // Начинаем с полного набора
        HashSet<char> consonantsInAllWords = new HashSet<char>(allVoicelessConsonants);

        // Для каждого слова находим пересечение согласных
        foreach (string word in words)
        {
            if (string.IsNullOrEmpty(word)) continue;

            HashSet<char> wordConsonants = new HashSet<char>();
            foreach (char c in word)
            {
                if (allVoicelessConsonants.Contains(c))
                {
                    wordConsonants.Add(c);
                }
            }

            consonantsInAllWords.IntersectWith(wordConsonants);
        }

        // Результат: согласные, которые не во ВСЕ слова
        HashSet<char> result = new HashSet<char>(allVoicelessConsonants);
        result.ExceptWith(consonantsInAllWords);

        // Сортируем для красоты
        List<char> sortedResult = result.OrderBy(c => c).ToList();

        Console.WriteLine("Глухие согласные, которые не входят хотя бы в одно слово:");
        if (sortedResult.Count == 0)
        {
            Console.WriteLine("Таких согласных нет");
        }
        else
        {
            foreach (char consonant in sortedResult)
            {
                Console.Write(consonant + " ");
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Создает тестовый текстовый файл
    /// </summary>
    public static void CreateTextTestFile(string filePath)
    {
        string testText = @"Программирование это интересно и увлекательно.
Компьютерные технологии развиваются быстро.
Алгоритмы и структуры данных важны для разработки.
Практика и теория взаимосвязаны.";

        File.WriteAllText(filePath, testText, Encoding.UTF8);
        Console.WriteLine($"Тестовый текстовый файл создан: {filePath}");
    }

    static void TestFileAnalysis()
    {
        Console.WriteLine("\n4.2 File Analysis - Глухие согласные:");
        string textFilePath = "text_test.txt";
        CreateTextTestFile(textFilePath);
        PrintVoicelessConsonantsNotInSomeWord(textFilePath);
    }

    #endregion

    #region Задание 5.2 - Олимпиада

    /// <summary>
    /// Участник олимпиады
    /// </summary>
    public class Participant
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int[] Scores { get; set; }
        public int TotalScore => Scores.Sum(); // Сумма баллов

        public Participant(string lastName, string firstName, int[] scores)
        {
            LastName = lastName;
            FirstName = firstName;
            Scores = scores ?? throw new ArgumentNullException(nameof(scores));
        }

        public override string ToString() => $"{LastName} {FirstName} {TotalScore}";

        public string GetDetailedInfo() => $"{LastName} {FirstName} [{string.Join(" + ", Scores)} = {TotalScore}]";
    }

    /// <summary>
    /// Находит победителей олимпиады из файла
    /// </summary>
    public static void FindOlympiadWinners(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Файл {filePath} не найден");

        List<Participant> participants = new List<Participant>();

        try
        {
            // Чтение файла
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length == 0)
                throw new ArgumentException("Файл пуст");

            // Первая строка - количество участников
            int n = int.Parse(lines[0].Trim());

            // Чтение данных участников
            for (int i = 1; i <= n && i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string[] parts = line.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 5)
                {
                    string lastName = parts[0];
                    string firstName = parts[1];
                    int[] scores = new int[3];

                    for (int j = 0; j < 3; j++)
                    {
                        if (!int.TryParse(parts[2 + j], out scores[j]))
                            throw new FormatException($"Неверный формат баллов в строке {i + 1}");
                    }

                    participants.Add(new Participant(lastName, firstName, scores));
                }
            }

            if (participants.Count == 0)
            {
                Console.WriteLine("Нет данных об участниках");
                return;
            }

            // Вывод всех участников
            Console.WriteLine("Все участники:");
            foreach (var participant in participants)
            {
                Console.WriteLine($"- {participant.GetDetailedInfo()}");
            }

            // Поиск максимального балла
            int maxScore = participants.Max(p => p.TotalScore);
            var winners = participants.Where(p => p.TotalScore == maxScore).ToList();

            // Вывод результатов
            Console.WriteLine($"\nМаксимальный балл: {maxScore}");
            Console.WriteLine($"Количество победителей: {winners.Count}");
            Console.WriteLine("Победители:");
            foreach (var winner in winners)
            {
                Console.WriteLine($"- {winner.LastName} {winner.FirstName} ({winner.TotalScore} баллов)");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке файла: {ex.Message}");
        }
    }

    /// <summary>
    /// Создает тестовый файл для олимпиады
    /// </summary>
    public static void CreateOlympiadTestFile(string filePath)
    {
        string[] testData = {
            "5",
            "Петрова Ольга 25 18 16",
            "Калиниченко Иван 14 19 15",
            "Сидоров Алексей 25 25 25",
            "Иванова Мария 24 23 25",
            "Козлов Дмитрий 25 25 25"
        };

        File.WriteAllLines(filePath, testData, Encoding.UTF8);
        Console.WriteLine($"Тестовый файл олимпиады создан: {filePath}");
    }

    static void TestOlympiad()
    {
        Console.WriteLine("\n5.2 Dictionary - Олимпиада:");
        string olympiadFilePath = "olympiad_test.txt";
        CreateOlympiadTestFile(olympiadFilePath);
        FindOlympiadWinners(olympiadFilePath);
    }

    #endregion
}
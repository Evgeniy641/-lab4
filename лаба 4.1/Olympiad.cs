using LabProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Olympiad
{
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

    /// <summary>
    /// Тестирование задания 5.2
    /// </summary>
    public static void Test()
    {
        Console.WriteLine("\n5.2 Dictionary - Олимпиада:");
        string olympiadFilePath = "olympiad_test.txt";
        CreateOlympiadTestFile(olympiadFilePath);
        FindOlympiadWinners(olympiadFilePath);
    }
}
using LabProject;
using System;
using System.Collections.Generic;

public static class HashSetOperations
{
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

    /// <summary>
    /// Тестирование задания 3.2
    /// </summary>
    public static void Test()
    {
        Console.WriteLine("\n3.2 HashSet - Анализ игр студентов:");
        AnalyzeStudentGames(CreateTestStudents(), CreateTestGames());
    }
}
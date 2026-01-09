using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class FileAnalysis
{
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

    /// <summary>
    /// Тестирование задания 4.2
    /// </summary>
    public static void Test()
    {
        Console.WriteLine("\n4.2 File Analysis - Глухие согласные:");
        string textFilePath = "text_test.txt";
        CreateTextTestFile(textFilePath);
        PrintVoicelessConsonantsNotInSomeWord(textFilePath);
    }
}
using System;
using System.Collections.Generic;

public static class ListOperations
{
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

    /// <summary>
    /// Тестирование задания 1.2
    /// </summary>
    public static void Test()
    {
        Console.WriteLine("1.2 List - Замена первого вхождения:");
        List<int> L = new List<int> { 1, 2, 3, 4, 5, 2, 3, 6 };
        List<int> L1 = new List<int> { 2, 3 };
        List<int> L2 = new List<int> { 9, 9, 9 };
        ReplaceFirstOccurrence(L, L1, L2);
    }
}
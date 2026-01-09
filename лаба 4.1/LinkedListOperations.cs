using System;
using System.Collections.Generic;

public static class LinkedListOperations
{
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

    /// <summary>
    /// Тестирование задания 2.2
    /// </summary>
    public static void Test()
    {
        Console.WriteLine("\n2.2 LinkedList - Сортировка:");
        LinkedList<int> list = new LinkedList<int>();
        list.AddLast(5); list.AddLast(2); list.AddLast(8); list.AddLast(1); list.AddLast(4);
        SortLinkedList(list);
    }
}
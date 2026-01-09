using System;

namespace MoneyApp
{
    /// <summary>
    /// Основной класс программы для тестирования класса Money
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== ТЕСТИРОВАНИЕ КЛАССА Money ===\n");

            TestAllOperations();

            Console.WriteLine("\nТестирование завершено!");
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        /// <summary>
        /// Тестирование всех операций класса Money
        /// </summary>
        private static void TestAllOperations()
        {
            try
            {
                TestConstructors();
                TestAddKopeksMethod();
                TestUnaryOperators();
                TestTypeConversionOperators();
                TestBinaryOperators();
                TestErrorHandling();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Тестирование конструкторов
        /// </summary>
        private static void TestConstructors()
        {
            Console.WriteLine("1. Тестирование конструкторов:");

            Money defaultMoney = new Money();
            Console.WriteLine($"Конструктор по умолчанию: {defaultMoney}");

            Money customMoney = new Money(150, 75);
            Console.WriteLine($"Конструктор с параметрами (150, 75): {customMoney}");
        }

        /// <summary>
        /// Тестирование метода AddKopeks
        /// </summary>
        private static void TestAddKopeksMethod()
        {
            Console.WriteLine("\n2. Тестирование метода AddKopeks:");

            Money money = new Money(10, 50);
            Console.WriteLine($"Исходная сумма: {money}");

            Money afterAdd1 = money.AddKopeks(75);
            Console.WriteLine($"После добавления 75 копеек: {afterAdd1}");

            Money afterAdd2 = money.AddKopeks(150);
            Console.WriteLine($"После добавления 150 копеек: {afterAdd2}");
        }

        /// <summary>
        /// Тестирование унарных операторов
        /// </summary>
        private static void TestUnaryOperators()
        {
            Console.WriteLine("\n3. Тестирование унарных операторов:");

            Money money = new Money(5, 99);
            Console.WriteLine($"Исходная сумма: {money}");

            money++;
            Console.WriteLine($"После money++: {money}");

            money--;
            Console.WriteLine($"После money--: {money}");
        }

        /// <summary>
        /// Тестирование операторов приведения типа
        /// </summary>
        private static void TestTypeConversionOperators()
        {
            Console.WriteLine("\n4. Тестирование операторов приведения типа:");

            Money money = new Money(123, 45);
            Console.WriteLine($"Сумма: {money}");

            uint rublesOnly = (uint)money;
            Console.WriteLine($"Явное приведение к uint (только рубли): {rublesOnly}");

            double kopeksInRubles = money;
            Console.WriteLine($"Неявное приведение к double (копейки в рублях): {kopeksInRubles:F2}");
        }

        /// <summary>
        /// Тестирование бинарных операторов
        /// </summary>
        private static void TestBinaryOperators()
        {
            Console.WriteLine("\n5. Тестирование бинарных операторов:");

            Money money = new Money(100, 30);
            Console.WriteLine($"Исходная сумма: {money}");

            Money afterAdd1 = money + 50;
            Console.WriteLine($"money + 50 копеек: {afterAdd1}");

            Money afterAdd2 = 80 + money;
            Console.WriteLine($"80 копеек + money: {afterAdd2}");

            Money afterSubtract1 = money - 30;
            Console.WriteLine($"money - 30 копеек: {afterSubtract1}");

            Console.WriteLine($"200 копеек - money:");
            Money afterSubtract2 = 200 - money;
            Console.WriteLine($"{afterSubtract2}");
        }

        /// <summary>
        /// Тестирование обработки ошибок
        /// </summary>
        private static void TestErrorHandling()
        {
            Console.WriteLine("\n6. Тестирование обработки ошибок:");

            try
            {
                Money normalizedMoney = new Money(0, 150);
                Console.WriteLine($"Нормализация 150 копеек: {normalizedMoney}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при нормализации: {ex.Message}");
            }

            try
            {
                Money zeroMoney = new Money(0, 0);
                zeroMoney--;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Попытка уменьшить нулевую сумму: {ex.Message}");
            }
        }
    }
}
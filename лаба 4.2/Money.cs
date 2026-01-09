using System;

namespace MoneyApp
{
    /// <summary>
    /// Класс для работы с денежными суммами в рублях и копейках
    /// </summary>
    public class Money
    {
        private uint _rubles;
        private byte _kopeks;

        /// <summary>
        /// Рубли
        /// </summary>
        public uint Rubles
        {
            get => _rubles;
            private set => _rubles = value;
        }

        /// <summary>
        /// Копейки (0-99)
        /// </summary>
        public byte Kopeks
        {
            get => _kopeks;
            private set
            {
                if (value >= 100)
                    throw new ArgumentException("Копейки должны быть от 0 до 99");
                _kopeks = value;
            }
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Money() : this(0, 0) { }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="rubles">Рубли</param>
        /// <param name="kopeks">Копейки</param>
        public Money(uint rubles, byte kopeks)
        {
            Rubles = rubles;
            Kopeks = kopeks;
            Normalize();
        }

        /// <summary>
        /// Добавляет указанное количество копеек к сумме
        /// </summary>
        /// <param name="kopeksToAdd">Количество копеек для добавления</param>
        /// <returns>Новая сумма</returns>
        public Money AddKopeks(uint kopeksToAdd)
        {
            if (kopeksToAdd > uint.MaxValue - TotalKopeks)
                throw new OverflowException("Слишком большое количество копеек для добавления");

            uint totalKopeks = TotalKopeks + kopeksToAdd;
            uint newRubles = totalKopeks / 100;
            byte newKopeks = (byte)(totalKopeks % 100);

            if (newRubles > uint.MaxValue)
                throw new OverflowException("Превышено максимальное количество рублей");

            return new Money(newRubles, newKopeks);
        }

        /// <summary>
        /// Общее количество копеек
        /// </summary>
        private uint TotalKopeks => Rubles * 100 + Kopeks;

        /// <summary>
        /// Нормализует сумму (переводит копейки в рубли при необходимости)
        /// </summary>
        private void Normalize()
        {
            if (Kopeks >= 100)
            {
                Rubles += (uint)(Kopeks / 100);
                Kopeks = (byte)(Kopeks % 100);
            }
        }

        /// <summary>
        /// Возвращает строковое представление суммы
        /// </summary>
        public override string ToString()
        {
            return $"{Rubles} руб. {Kopeks:D2} коп.";
        }

        // ========== ЗАДАНИЕ 7 ==========

        /// <summary>
        /// Унарный оператор инкремента (добавляет 1 копейку)
        /// </summary>
        public static Money operator ++(Money money)
        {
            return money.AddKopeks(1);
        }

        /// <summary>
        /// Унарный оператор декремента (уменьшает на 1 копейку)
        /// </summary>
        public static Money operator --(Money money)
        {
            if (money.TotalKopeks == 0)
                throw new InvalidOperationException("Нельзя уменьшить нулевую сумму");

            uint totalKopeks = money.TotalKopeks - 1;
            uint newRubles = totalKopeks / 100;
            byte newKopeks = (byte)(totalKopeks % 100);
            return new Money(newRubles, newKopeks);
        }

        /// <summary>
        /// Явное приведение к uint (возвращает только рубли)
        /// </summary>
        public static explicit operator uint(Money money)
        {
            return money.Rubles;
        }

        /// <summary>
        /// Неявное приведение к double (возвращает копейки в рублях)
        /// </summary>
        public static implicit operator double(Money money)
        {
            return money.Kopeks / 100.0;
        }

        /// <summary>
        /// Сложение суммы с копейками
        /// </summary>
        public static Money operator +(Money money, uint kopeks)
        {
            return money.AddKopeks(kopeks);
        }

        /// <summary>
        /// Сложение копеек с суммой
        /// </summary>
        public static Money operator +(uint kopeks, Money money)
        {
            return money + kopeks;
        }

        /// <summary>
        /// Вычитание копеек из суммы
        /// </summary>
        public static Money operator -(Money money, uint kopeks)
        {
            if (kopeks > money.TotalKopeks)
                throw new InvalidOperationException("Результат не может быть отрицательным");

            uint totalKopeks = money.TotalKopeks - kopeks;
            uint newRubles = totalKopeks / 100;
            byte newKopeks = (byte)(totalKopeks % 100);
            return new Money(newRubles, newKopeks);
        }

        /// <summary>
        /// Вычитание суммы из копеек
        /// </summary>
        public static Money operator -(uint kopeks, Money money)
        {
            if (kopeks < money.TotalKopeks)
                throw new InvalidOperationException("Результат не может быть отрицательным");

            uint totalKopeks = kopeks - money.TotalKopeks;
            uint newRubles = totalKopeks / 100;
            byte newKopeks = (byte)(totalKopeks % 100);
            return new Money(newRubles, newKopeks);
        }
    }
}
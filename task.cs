using System;
using System.Linq;
using System.Threading;

namespace task
{
    class Program
    {
        static void Main()
        {
            // Ініціалізація масиву з 10 цілих чисел у проміжку від 0 до 25
            int[] numbers = InitializeArray(10, 0, 25);

            // Створення потоку T0 для виведення чисел, менших за 15
            Thread T0 = new Thread(() =>
            {
                PrintNumbersLessThan(numbers, 15);
            });
            T0.Name = "потiк T0"; // Ім'я потоку T0

            // Створення потоку T1 для обчислення середнього геометричного значення
            Thread T1 = new Thread(() =>
            {
                double geometricMean = CalculateGeometricMean(numbers);
                Console.WriteLine($"Середнє геометричне значення: {geometricMean:F3}");
            });
            T1.Name = "потiк T1"; // Ім'я потоку T1

            // Запуск потоку T0 та очікування його завершення
            Console.WriteLine($"Назва потоку: {T0.Name}");
            T0.Start();
            Console.WriteLine($"Стан «{T0.Name}»: {T0.ThreadState}");

            T0.Join(); // Чекаємо на завершення потоку T0
            Console.WriteLine($"Стан «{T0.Name}»: {T0.ThreadState}");

            // Запуск потоку T1 після завершення T0
            Console.WriteLine($"\nНазва потоку: {T1.Name}");
            T1.Start();
            Console.WriteLine($"Стан «{T1.Name}»: {T1.ThreadState}");

            // Очікування завершення потоку T1
            T1.Join();
            Console.WriteLine($"Стан «{T1.Name}»: {T1.ThreadState}");
        }

        // Метод для ініціалізації масиву випадковими числами у заданому проміжку
        static int[] InitializeArray(int length, int minValue, int maxValue)
        {
            Random random = new Random();
            int[] array = new int[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(minValue, maxValue + 1);
            }

            return array;
        }

        // Метод для виведення чисел, менших за задане значення
        static void PrintNumbersLessThan(int[] numbers, int threshold)
        {
            Console.WriteLine($"Числа, меншi за {threshold}:");
            foreach (int number in numbers)
            {
                if (number < threshold)
                {
                    Console.WriteLine(number);
                }
            }
        }

        // Метод для обчислення середнього геометричного значення
        static double CalculateGeometricMean(int[] numbers)
        {
            double product = 1.0;
            foreach (int number in numbers)
            {
                product *= number;
            }

            return Math.Pow(product, 1.0 / numbers.Length);     
        }        
    }    
}
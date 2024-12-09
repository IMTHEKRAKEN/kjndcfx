using System;
using System.Numerics;

public class FactorialCalculator
{
    public static BigInteger FactorialRecursive(int n)
    {
        if (n < 0)
        {
            throw new ArgumentException("Число должно быть неотрицательным.");
        }
        if (n == 0)
        {
            return 1;
        }
        return n * FactorialRecursive(n - 1);
    }

    public static BigInteger FactorialIterative(int n)
    {
        if (n < 0)
        {
            throw new ArgumentException("Число должно быть неотрицательным.");
        }
        BigInteger result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Введите неотрицательное целое число:");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int number))
        {
            try
            {
                BigInteger recursiveResult = FactorialRecursive(number);
                Console.WriteLine($"Факториал {number} (рекурсивно): {recursiveResult}");

                BigInteger iterativeResult = FactorialIterative(number);
                Console.WriteLine($"Факториал {number} (итеративно): {iterativeResult}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (StackOverflowException ex)
            {
                Console.WriteLine($"Ошибка: Рекурсивный вызов превысил лимит стека. Попробуйте итеративный метод.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите целое число.");
        }
    }
}
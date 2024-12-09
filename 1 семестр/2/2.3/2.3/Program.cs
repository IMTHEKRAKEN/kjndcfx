using System;

public class ExponentialApproximation
{
    public static double CalculateExponential(double x, double epsilon = 1e-6)
    {
        if (x < -1 || x > 1)
        {
            throw new ArgumentException("x должно быть в диапазоне [-1, 1]");
        }

        double term = 1.0;
        double sum = 1.0;
        double factorial = 1.0;
        int i = 1;

        while (Math.Abs(term) >= epsilon)
        {
            factorial *= i;
            term = Math.Pow(x, i) / factorial;
            sum += term;
            i++;
        }

        return sum;
    }

    public static void Main(string[] args)
    {
        Console.Write("Введите вещественное число x (-1 <= x <= 1): ");
        string input = Console.ReadLine();

        if (double.TryParse(input, out double x))
        {
            try
            {
                double approximation = CalculateExponential(x);
                Console.WriteLine($"Приближение e^x: {approximation}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите вещественное число.");
        }
    }
}
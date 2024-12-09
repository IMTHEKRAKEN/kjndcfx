using System;
using System.IO;

public class SinTableGenerator
{
    public static void Main(string[] args)
    {
        string filePath = "f.txt";
        double start = 0.0;
        double end = 1.0;
        double step = 0.1;

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("x sin(x)"); 

                for (double x = start; x <= end; x += step)
                {
                    double sinX = Math.Sin(x);
                    writer.WriteLine($"{x:F1} {sinX:F4}"); 
                }
            }

            Console.WriteLine($"Таблица значений sin(x) успешно записана в файл {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
        }
    }
}
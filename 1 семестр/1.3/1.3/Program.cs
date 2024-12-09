using System;
namespace homework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Коэффициент a: ");
            int a = Convert.ToInt32(Console.ReadLine());
            if (a == 0)
            {
                Console.WriteLine("Коэффициент а не может быть равен нулю");
            }
            else
            {
                Console.Write("Коэффициент b: ");
                int b = Convert.ToInt32(Console.ReadLine());
                Console.Write("Коэффициент c: ");
                int c = Convert.ToInt32(Console.ReadLine());
                double d = 0.0f;
                d = b * b - 4 * a * c;
                int count = 0;
                double root1 = 0;
                double root2 = 0;
                if (d == 0)
                {
                    count = 1;
                    root1 = (-b) / 2 * a;
                    Console.WriteLine($"Количество решений: {count}");
                    Console.WriteLine(root1);
                }
                if (d >= 0)
                {
                    count = 2;
                    root1 = (-b + Math.Sqrt(d)) / (2 * a);
                    root2 = (-b - Math.Sqrt(d)) / (2 * a);
                    Console.WriteLine($"Количество решений: {count}");
                    Console.WriteLine(root1);
                    Console.WriteLine(root2);
                }
                else
                {
                    Console.WriteLine("Нет вещественных корней");
                }
            }
        }
    }
}
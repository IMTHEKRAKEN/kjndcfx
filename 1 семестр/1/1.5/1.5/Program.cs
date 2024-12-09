using System;
namespace homework
{

    class Program
    {
        static void Main()
        {
            Console.Write("Введите длины сторон первого треугольника через запятую (a, b, c): ");
            string[] sides1 = Console.ReadLine().Split(' ');
            double a1 = Convert.ToDouble(sides1[0]);
            double b1 = Convert.ToDouble(sides1[1]);
            double c1 = Convert.ToDouble(sides1[2]);

            Console.Write("Введите длины сторон второго треугольника через запятую (a, b, c): ");
            string[] sides2 = Console.ReadLine().Split(' ');
            double a2 = Convert.ToDouble(sides2[0]);
            double b2 = Convert.ToDouble(sides2[1]);
            double c2 = Convert.ToDouble(sides2[2]);

            bool areSimilar = AreSimilar(a1, b1, c1, a2, b2, c2);
            Console.WriteLine(areSimilar ? "да" : "нет");
        }

        static bool AreSimilar(double a1, double b1, double c1, double a2, double b2, double c2)
        {
            double ratio1 = a1 / a2;
            double ratio2 = b1 / b2;
            double ratio3 = c1 / c2;

            return Math.Abs(ratio1 - ratio2) < 1e-9 && Math.Abs(ratio2 - ratio3) < 1e-9;
        }
    }
}
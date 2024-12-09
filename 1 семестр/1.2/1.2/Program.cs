using System;
namespace homework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите делимое: ");
            int divisible = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите делитель: ");
            int divider = Convert.ToInt32(Console.ReadLine());
            int integer = 0;
            if (divisible >= 0 && divider > 0)
            {
                while (divisible >= divider)
                {
                    divisible -= divider;
                    integer++;
                }
                Console.WriteLine($"Целое: {integer}, остаток: {divisible}");
            }
            else if (divisible < 0 && divider > 0)
            {
                while (divisible < 0)
                {
                    divisible += divider;
                    integer--;
                }
                Console.WriteLine($"Целое: {integer}, остаток: {divisible}");
            }
            else if (divisible < 0 && divider < 0)
            {
                divisible = Math.Abs(divisible);
                while (divisible >= 0)
                {
                    divisible = divisible - Math.Abs(divider);
                    integer++;
                }
                Console.WriteLine($"Целое: {integer}, остаток:{Math.Abs(divisible)}");

            }
            else if (divisible >= 0 && divider < 0)
            {
                while (divisible >= Math.Abs(divider))
                {
                    divisible -= Math.Abs(divider);
                    integer--;
                }
                Console.WriteLine($"Целое: {integer}, остаток: {divisible}");
            }
            else
                Console.WriteLine("На ноль делить нельзя");
        }
    }
}
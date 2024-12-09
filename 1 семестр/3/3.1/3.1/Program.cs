using System;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace homework
{
    class Program
    {
        public static void Main()
        {
            Console.Write("Введите первое число: ");
            int first = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите второе число: ");
            int second = Convert.ToInt32(Console.ReadLine());

            Console.Write("Первое число в двоичной системе: ");
            Console.WriteLine(Binary(first));
            Console.Write("Второе число в двоичной системе: ");
            Console.WriteLine(Binary(second));

            Console.Write("Сумма двух чисел в двоичном представлении: ");
            Console.WriteLine(Summa(Binary(first), Binary(second)));

            Console.Write("Сумма, переведенная в десятичную систему: ");
            Console.WriteLine(Decimal(Summa(Binary(first), Binary(second))));

            Console.Write("Сумма двух чисел в десятичной системе: ");
            Console.WriteLine(first + second);
        }
        public static string Summa(string one, string two)
        {
            int len1 = one.Length;
            int len2 = two.Length;
            int lenmax = Math.Max(len1, len2);

            if (len1 < lenmax)
            {
                while (len1 < lenmax)
                {
                    one = "0" + one;
                    len1 = one.Length;
                }
            }

            if (len2 < lenmax)
            {
                while (len2 < lenmax)
                {
                    two = "0" + two;
                    len2 = two.Length;
                }
            }
            int carry = 0;
            string result = "";
            for (int i = lenmax - 1; i >= 0; i--)
            {
                int sum = (one[i] - '0') + (two[i] - '0') + carry;
                result = (sum % 2) + result;
                carry = sum / 2;
            }

            if (carry > 0)
            {
                result = carry + result;
            }

            return result;
        }
        public static string Binary(int i)
        {
            string str = "";
            while (i > 0)
            {
                str = str + (i % 2);
                i /= 2;
            }
            string answer = "";
            for (int j = str.Length - 1; j >= 0; j--)
            {
                answer = answer + str[j];
            }
            return answer;
        }
        public static double Decimal(string s)
        {

            double total = 0;
            for (int j = 0; j < s.Length; j++)
            {
                int i = Convert.ToInt32(s[j].ToString());
                double a = i * (Math.Pow(2, (s.Length - j - 1)));

                total += a;
            }
            return total;
        }
    }
}
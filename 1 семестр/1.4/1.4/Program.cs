using System;
namespace homework
{

    class Program
    {
        static void Main()
        {
            Console.Write("Введите первое число в двоичной системе: ");
            string first = Console.ReadLine();
            int len1 = first.Length;

            Console.Write("Введите второе число в двоичной системе: ");
            string second = Console.ReadLine();
            int len2 = second.Length;

            Console.Write("Введите третье число в двоичной системе: ");
            string third = Console.ReadLine();
            int len3 = third.Length;

            int lenmax = Math.Max(Math.Max(len1, len2), len3);

            if (len1 < lenmax)
            {
                while (len1 < lenmax)
                {
                    first = "0" + first;
                    len1 = first.Length;
                }
            }

            if (len2 < lenmax)
            {
                while (len2 < lenmax)
                {
                    second = "0" + second;
                    len2 = second.Length;
                }
            }

            if (len3 < lenmax)
            {
                while (len3 < lenmax)
                {
                    third = "0" + third;
                    len3 = third.Length;
                }
            }

            Console.WriteLine(first);
            Console.WriteLine(second);
            Console.WriteLine(third);
            Console.WriteLine(" ");
            string answer = "";

            for (int i = 0, one = 0, two = 0, three = 0; i < lenmax; i++)
            {
                one = Convert.ToInt32(first[i].ToString());
                two = Convert.ToInt32(second[i].ToString());
                three = Convert.ToInt32(third[i].ToString());

                if (one + two + three == 2 || one + two + three == 3)
                {
                    answer = answer + '1';
                }
                else if (one + two + three == 1 || one + two + three == 0)
                {
                    answer = answer + '0';
                }
            }
            Console.Write(answer);
        }
    }
}
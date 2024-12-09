using System;
namespace homework
{

    class Program
    {
        static void Main()
        {
            Console.Write("Введите первую строчку: ");
            string s = Console.ReadLine();
            Console.Write("Введите вторую строчку: ");
            string s1 = Console.ReadLine();

            int count = 0;
            int index = s.IndexOf(s1);

            while (index != -1)
            {
                count++;
                index = s.IndexOf(s1, index + 1);
            }
            Console.WriteLine("Количество вхождений: " + count);
        }
    }
}
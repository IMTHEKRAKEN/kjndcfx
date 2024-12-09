namespace ConsoleApplication1
{
    class Program
    {
        public static bool isSimple(double x)
        {
            double sqrtX = Math.Sqrt(x);
            for (int i = 2; i <= sqrtX; i++)
                if (x % i == 0) return false;
            return true;
        }
        public static void Main()
        {
            Console.Write("Введите число: ");
            int n = int.Parse(Console.ReadLine());
            if (n <= 0) return;
            int Summ = 0;
            Console.Write("Простые числа: ");
            for (int i = 2; i <= n; i++)
                if (isSimple(i))
                {
                    Summ += i;
                    Console.Write("{0} ", i);
                }
        }
    }
}
using System;

public class CircleDrawer
{
    public static void DrawCircle(int radius)
    {
        if (radius <= 0)
        {
            Console.WriteLine("Радиус должен быть больше 0");
            return;
        }
        int centerX = Console.WindowWidth / 2;
        int centerY = Console.WindowHeight / 2;
        int x = 0;
        int y = radius;
        int d = 3 - 2 * radius;
        DrawPoint(centerX + x, centerY + y);
        DrawPoint(centerX - x, centerY + y);
        DrawPoint(centerX + x, centerY - y);
        DrawPoint(centerX - x, centerY - y);
        DrawPoint(centerX + y, centerY + x);
        DrawPoint(centerX - y, centerY + x);
        DrawPoint(centerX + y, centerY - x);
        DrawPoint(centerX - y, centerY - x);
        while (y >= x)
        {
            x++;
            if (d > 0)
            {
                y--;
                d = d + 4 * (x - y) + 10;
            }
            else
            {
                d = d + 4 * x + 6;
            }
            DrawPoint(centerX + x, centerY + y);
            DrawPoint(centerX - x, centerY + y);
            DrawPoint(centerX + x, centerY - y);
            DrawPoint(centerX - x, centerY - y);
            DrawPoint(centerX + y, centerY + x);
            DrawPoint(centerX - y, centerY + x);
            DrawPoint(centerX + y, centerY - x);
            DrawPoint(centerX - y, centerY - x);
        }
    }
    private static void DrawPoint(int x, int y)
    {
        if (x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("*");
        }
    }
    public static void Main(string[] args)
    {
        Console.Write("Введите радиус окружности: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int radius))
        {
            DrawCircle(radius);
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите целое число.");
        }
    }
}
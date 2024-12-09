using System;
using System.Collections.Generic;
using System.Threading;

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Snake
{
    public List<Point> Body { get; set; }
    public Point Direction { get; set; }

    public Snake(int startX, int startY)
    {
        Body = new List<Point> { new Point(startX, startY) };
        Direction = new Point(1, 0); // Начальное направление: вправо
    }

    public void Move()
    {
        Point head = new Point(Body[0].X + Direction.X, Body[0].Y + Direction.Y);
        Body.Insert(0, head);
        Body.RemoveAt(Body.Count - 1);
    }

    public void Grow()
    {
        Point head = new Point(Body[0].X + Direction.X, Body[0].Y + Direction.Y);
        Body.Insert(0, head);
    }

    public bool CheckCollision(int width, int height)
    {
        Point head = Body[0];
        //Проверка на столкновение со стеной
        if (head.X < 0 || head.X >= width || head.Y < 0 || head.Y >= height) return true;
        //Проверка на столкновение с собственным телом
        for (int i = 1; i < Body.Count; i++)
        {
            if (head.X == Body[i].X && head.Y == Body[i].Y) return true;
        }
        return false;
    }
}

public class Game
{
    public int Width { get; set; }
    public int Height { get; set; }
    public Snake Snake { get; set; }
    public Point Food { get; set; }
    public bool GameOver { get; set; }

    public Game(int width, int height)
    {
        Width = width;
        Height = height;
        Snake = new Snake(width / 2, height / 2);
        GenerateFood();
        GameOver = false;
    }


    public void GenerateFood()
    {
        Random random = new Random();
        int x, y;
        do
        {
            x = random.Next(Width);
            y = random.Next(Height);
        } while (IsPointOccupied(x, y));
        Food = new Point(x, y);
    }

    public bool IsPointOccupied(int x, int y)
    {
        foreach (Point point in Snake.Body)
        {
            if (point.X == x && point.Y == y) return true;
        }
        return false;
    }

    public void Draw()
    {
        Console.Clear();
        // Рисуем границы
        for (int i = 0; i < Width; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("#");
            Console.SetCursorPosition(i, Height - 1);
            Console.Write("#");
        }
        for (int i = 0; i < Height; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("#");
            Console.SetCursorPosition(Width - 1, i);
            Console.Write("#");
        }

        // Рисуем змейку
        foreach (Point point in Snake.Body)
        {
            Console.SetCursorPosition(point.X, point.Y);
            Console.Write("s");
        }

        // Рисуем еду
        Console.SetCursorPosition(Food.X, Food.Y);
        Console.Write("@");
    }

    public void Run()
    {
        while (!GameOver)
        {
            Draw();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow: Snake.Direction = new Point(0, -1); break;
                case ConsoleKey.DownArrow: Snake.Direction = new Point(0, 1); break;
                case ConsoleKey.LeftArrow: Snake.Direction = new Point(-1, 0); break;
                case ConsoleKey.RightArrow: Snake.Direction = new Point(1, 0); break;
            }

            Snake.Move();

            if (Snake.CheckCollision(Width, Height))
            {
                GameOver = true;
            }
            else if (Snake.Body[0].X == Food.X && Snake.Body[0].Y == Food.Y)
            {
                Snake.Grow();
                GenerateFood();
            }

            Thread.Sleep(100); // Задержка для регулирования скорости
        }
        Console.WriteLine("\nИгра окончена!");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game(30, 20);
        game.Run();
    }
}
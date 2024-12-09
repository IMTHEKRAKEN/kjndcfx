using System;
using System.Collections.Generic;
using System.Threading;

public struct Point
{
    public int X;
    public int Y;
}

public class Pacman
{
    public Point Position;
    public int Lives;

    public Pacman(int x, int y)
    {
        Position = new Point { X = x, Y = y };
        Lives = 3;
    }

    public void Move(Point direction, char[,] maze)
    {
        Point newPos = new Point { X = Position.X + direction.X, Y = Position.Y + direction.Y };
        if (IsMoveValid(newPos, maze))
        {
            Position = newPos;
        }
    }

    private bool IsMoveValid(Point pos, char[,] maze)
    {
        if (pos.X < 0 || pos.X >= maze.GetLength(0) || pos.Y < 0 || pos.Y >= maze.GetLength(1))
            return false;
        return maze[pos.X, pos.Y] != '#'; 
    }
}

public abstract class Enemy
{
    public Point Position;
    public abstract void Move(char[,] maze, Pacman pacman);
}

public class Ghost : Enemy
{
    public override void Move(char[,] maze, Pacman pacman)
    {
        Random random = new Random();
        Point[] directions = { new Point { X = 0, Y = 1 }, new Point { X = 0, Y = -1 }, new Point { X = 1, Y = 0 }, new Point { X = -1, Y = 0 } };
        Point direction = directions[random.Next(4)];
        Point newPos = new Point { X = Position.X + direction.X, Y = Position.Y + direction.Y };

        if (IsMoveValid(newPos, maze))
        {
            Position = newPos;
        }
    }
    private bool IsMoveValid(Point pos, char[,] maze)
    {
        if (pos.X < 0 || pos.X >= maze.GetLength(0) || pos.Y < 0 || pos.Y >= maze.GetLength(1))
            return false;
        return maze[pos.X, pos.Y] != '#'; 
    }
}


public class Game
{
    private char[,] maze;
    public Pacman Pacman;
    private List<Enemy> enemies;
    private List<Point> coins;
    public bool GameOver;

    public Game(char[,] maze)
    {
        this.maze = maze;
        Pacman = new Pacman(1, 1);
        enemies = new List<Enemy> { new Ghost { Position = new Point { X = 5, Y = 5 } } }; 
        coins = new List<Point>();
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (maze[i, j] == '.') coins.Add(new Point { X = i, Y = j });
            }
        }
        GameOver = false;
    }

    public void Run()
    {
        while (!GameOver)
        {
            Draw();
            ConsoleKeyInfo key = Console.ReadKey(true);
            Point direction = GetDirection(key.Key);
            Pacman.Move(direction, maze);
            foreach (Enemy enemy in enemies)
            {
                enemy.Move(maze, Pacman);
            }
            CheckCollisions();
            Thread.Sleep(200);
        }
    }


    private void CheckCollisions()
    {
        foreach (Enemy enemy in enemies)
        {
            if (Pacman.Position.X == enemy.Position.X && Pacman.Position.Y == enemy.Position.Y)
            {
                Pacman.Lives--;
                if (Pacman.Lives == 0) GameOver = true;
                Pacman.Position = new Point { X = 1, Y = 1 };
            }
        }
        coins.RemoveAll(coin => Pacman.Position.X == coin.X && Pacman.Position.Y == coin.Y);
        if (coins.Count == 0) GameOver = true;

    }

    private Point GetDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow: return new Point { X = -1, Y = 0 };
            case ConsoleKey.DownArrow: return new Point { X = 1, Y = 0 };
            case ConsoleKey.LeftArrow: return new Point { X = 0, Y = -1 };
            case ConsoleKey.RightArrow: return new Point { X = 0, Y = 1 };
            default: return new Point { X = 0, Y = 0 };
        }
    }

    public void Draw()
    {
        Console.Clear();
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (Pacman.Position.X == i && Pacman.Position.Y == j)
                {
                    Console.Write('P');
                }
                else if (maze[i, j] == '.')
                {
                    bool coinExists = coins.Any(coin => coin.X == i && coin.Y == j);
                    Console.Write(coinExists ? '.' : ' ');
                }
                else if (enemies.Any(enemy => enemy.Position.X == i && enemy.Position.Y == j))
                {
                    Console.Write('G'); 
                }
                else
                {
                    Console.Write(maze[i, j]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Жизни: {Pacman.Lives}");
        if (GameOver) Console.WriteLine("Игра окончена!");
    }

    public static void Main(string[] args)
    {
        char[,] maze =
        {
            {'#','#','#','#','#','#','#','#'},
            {'#','.','#','.','.','G','.','#'},
            {'#','.','.','.','#','#','.','#'},
            {'#','.','.','.','.','.','.','#'},
            {'#','#','.','G','.','.','.','#'},
            {'#','.','.','.','.','#','.','#'},
            {'#','#','#','#','#','#','#','#'}
        };
        Game game = new Game(maze);
        game.Run();
    }
}
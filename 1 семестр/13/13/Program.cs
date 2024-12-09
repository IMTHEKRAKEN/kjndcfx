using System;
using System.Collections.Generic;
using System.Linq;

public enum FigureType { R, G, B, Y }

public class Figure
{
    public FigureType Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Figure(FigureType type, int x, int y)
    {
        Type = type;
        X = x;
        Y = y;
    }
}

public class Match3Game
{
    private int width;
    private int height;
    private Figure[,] grid;
    private List<Figure> figures;
    public int score = 0;
    public bool isGameOver = false;

    private Random random = new Random();

    public Match3Game(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new Figure[width, height];
        figures = new List<Figure>();

        GenerateGrid();
    }

    private void GenerateGrid()
    {
        FigureType[] types = { FigureType.R, FigureType.G, FigureType.B, FigureType.Y };
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                FigureType type = types[random.Next(types.Length)];
                grid[i, j] = new Figure(type, i, j);
                figures.Add(grid[i, j]);
            }
        }
    }

    private void DrawGrid()
    {
        Console.Clear();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(grid[x, y].Type + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("Очки: " + score);
        if (isGameOver) Console.WriteLine("Игра окончена!");
    }

    public bool IsMatch3(int x, int y)
    {
        return CheckHorizontal(x, y) || CheckVertical(x, y);
    }

    private bool CheckHorizontal(int x, int y)
    {
        if (x + 2 >= width) return false;
        return grid[x, y].Type == grid[x + 1, y].Type && grid[x, y].Type == grid[x + 2, y].Type;
    }

    private bool CheckVertical(int x, int y)
    {
        if (y + 2 >= height) return false;
        return grid[x, y].Type == grid[x, y + 1].Type && grid[x, y].Type == grid[x, y + 2].Type;
    }


    public void MakeMove(int x1, int y1, int x2, int y2)
    {
        if (x1 < 0 || x1 >= width || y1 < 0 || y1 >= height || x2 < 0 || x2 >= width || y2 < 0 || y2 >= height) return;
        if (Math.Abs(x1 - x2) + Math.Abs(y1 - y2) != 1) return; 
        Figure figure1 = grid[x1, y1];
        Figure figure2 = grid[x2, y2];

        grid[x1, y1] = figure2;
        grid[x2, y2] = figure1;
        figure1.X = x2;
        figure1.Y = y2;
        figure2.X = x1;
        figure2.Y = y1;

        if (IsMatch3(x1, y1) || IsMatch3(x2, y2))
        {
            score += 10;
        }

        isGameOver = true; 
    }

    public static void Main(string[] args)
    {
        Match3Game game = new Match3Game(8, 8);
        game.DrawGrid();
        while (!game.isGameOver)
        {
            Console.WriteLine("Введите координаты двух фигур (x1 y1 x2 y2):");
            string input = Console.ReadLine();
            string[] coords = input.Split(' ');
            if (coords.Length == 4 && int.TryParse(coords[0], out int x1) && int.TryParse(coords[1], out int y1) &&
                int.TryParse(coords[2], out int x2) && int.TryParse(coords[3], out int y2))
            {
                game.MakeMove(x1, y1, x2, y2);
            }
            game.DrawGrid();
        }
    }
}
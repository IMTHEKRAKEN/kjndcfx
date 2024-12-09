using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class Card
{
    public char Symbol { get; set; }
    public bool Revealed { get; set; } = false;

    public Card(char symbol)
    {
        Symbol = symbol;
    }
}

public class MemoryGame
{
    private int size;
    private Card[,] grid;
    private List<char> symbols;
    private Stopwatch stopwatch;
    private int revealedCount = 0;
    private Card firstSelectedCard = null;

    public MemoryGame(int n)
    {
        size = 2 * n;
        grid = new Card[size, size];
        symbols = Enumerable.Range(65, size * size / 2).Select(i => (char)i).ToList(); 

        InitializeGrid();
        stopwatch = new Stopwatch();
    }

    private void InitializeGrid()
    {
        List<char> shuffledSymbols = Shuffle(symbols.Concat(symbols).ToList());
        int index = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                grid[i, j] = new Card(shuffledSymbols[index++]);
            }
        }
    }

    private List<T> Shuffle<T>(List<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    public void StartGame()
    {
        while (revealedCount < size * size)
        {
            DrawGrid();
            SelectCard();
        }

        stopwatch.Stop();
        Console.Clear();
        Console.WriteLine($"Поздравляем! Вы выиграли за {stopwatch.ElapsedMilliseconds / 1000.0} секунд!");
        Console.CursorVisible = true;
    }

    private void SelectCard()
    {
        Console.WriteLine("Выберите карту (стрелки, Enter):");
        int row = size / 2, col = size / 2; 
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow: row = Math.Max(0, row - 1); break;
                case ConsoleKey.DownArrow: row = Math.Min(size - 1, row + 1); break;
                case ConsoleKey.LeftArrow: col = Math.Max(0, col - 1); break;
                case ConsoleKey.RightArrow: col = Math.Min(size - 1, col + 1); break;
                case ConsoleKey.Enter:
                    if (!grid[row, col].Revealed)
                    {
                        if (firstSelectedCard == null)
                        {
                            firstSelectedCard = grid[row, col];
                            firstSelectedCard.Revealed = true;
                        }
                        else
                        {
                            Card secondCard = grid[row, col];
                            secondCard.Revealed = true;
                            DrawGrid();
                            Thread.Sleep(1000);

                            if (firstSelectedCard.Symbol == secondCard.Symbol)
                            {
                                firstSelectedCard.Revealed = false;
                                secondCard.Revealed = false;
                                revealedCount += 2;
                            }
                            else
                            {
                                firstSelectedCard.Revealed = false;
                                secondCard.Revealed = false;
                            }
                            firstSelectedCard = null;
                        }

                    }
                    return;

            }
            DrawGrid();
        }
    }

    private void DrawGrid()
    {
        Console.Clear();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write((grid[i, j].Revealed ? grid[i, j].Symbol : '#') + " ");
            }
            Console.WriteLine();
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Введите размер сетки (n, 2n x 2n):");
        if (int.TryParse(Console.ReadLine(), out int n))
        {
            MemoryGame game = new MemoryGame(n);
            game.StartGame();
        }
        else
        {
            Console.WriteLine("Некорректный ввод.");
        }
    }
}
using System;
using System.Collections.Generic;

public class Calculator
{
    private double currentNumber;
    private double previousNumber;
    private string previousOperation;
    private Stack<double> history = new Stack<double>();
    private Stack<string> operationHistory = new Stack<string>();


    public Calculator()
    {
        currentNumber = 0;
        previousNumber = 0;
        previousOperation = "";
    }

    public void Add(double number)
    {
        currentNumber += number;
    }

    public void Subtract(double number)
    {
        currentNumber -= number;
    }

    public void Multiply(double number)
    {
        currentNumber *= number;
    }

    public void Divide(double number)
    {
        if (number == 0)
        {
            throw new DivideByZeroException("Деление на ноль невозможно!");
        }
        currentNumber /= number;
    }

    public int Div(double number)
    {
        if (number == 0)
        {
            throw new DivideByZeroException("Деление на ноль невозможно!");
        }
        return (int)(currentNumber / number);

    }

    public int Mod(double number)
    {
        if (number == 0)
        {
            throw new DivideByZeroException("Деление на ноль невозможно!");
        }
        return (int)(currentNumber % number);
    }


    public void Clear()
    {
        currentNumber = 0;
    }

    public double GetResult()
    {
        return currentNumber;
    }

    public void RepeatLastOperation(double number)
    {
        if (string.IsNullOrEmpty(previousOperation))
        {
            throw new InvalidOperationException("Предыдущая операция не определена.");
        }

        switch (previousOperation)
        {
            case "+": Add(number); break;
            case "-": Subtract(number); break;
            case "*": Multiply(number); break;
            case "/": Divide(number); break;
            default: throw new InvalidOperationException("Неизвестная операция.");
        }
    }

    public void CopyNumber(double number)
    {
        previousNumber = number;
    }

    public void PasteNumber()
    {
        if (previousNumber == 0)
        {
            throw new InvalidOperationException("Число не скопировано.");
        }
        currentNumber = previousNumber;
    }


    public void SetOperation(string operation)
    {
        previousOperation = operation;
        history.Push(currentNumber);
        operationHistory.Push(operation);
        previousNumber = currentNumber;
        currentNumber = 0;
    }

    public void Undo()
    {
        if (history.Count > 0)
        {
            currentNumber = history.Pop();
            previousOperation = operationHistory.Pop();
        }
        else
        {
            Console.WriteLine("Нет истории для отмены");
        }
    }

    //Дополнительные функции (не обязательные, но полезные)
    public void PrintHistory()
    {
        Console.WriteLine("История операций:");
        Stack<double> tempHistory = new Stack<double>(history);
        Stack<string> tempOpHistory = new Stack<string>(operationHistory);
        while (tempHistory.Count > 0)
        {
            Console.WriteLine($"{tempOpHistory.Pop()}: {tempHistory.Pop()}");
        }
    }


}

public class Program
{
    public static void Main(string[] args)
    {
        Calculator calc = new Calculator();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Введите выражение (или 'exit' для выхода):");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                exit = true;
                continue;
            }

            try
            {
                string[] parts = input.Split(' ');
                double number = double.Parse(parts[0]);
                string operation = parts[1];

                switch (operation)
                {
                    case "+": calc.SetOperation("+"); break;
                    case "-": calc.SetOperation("-"); break;
                    case "*": calc.SetOperation("*"); break;
                    case "/": calc.SetOperation("/"); break;
                    case "div": calc.SetOperation("div"); break;
                    case "mod": calc.SetOperation("mod"); break;
                    case "c": calc.Clear(); break;
                    case "=": Console.WriteLine($"Результат: {calc.GetResult()}"); break;
                    case "repeat": calc.RepeatLastOperation(number); break;
                    case "copy": calc.CopyNumber(number); break;
                    case "paste": calc.PasteNumber(); break;
                    case "undo": calc.Undo(); break;
                    case "history": calc.PrintHistory(); break;
                    default: throw new ArgumentException("Неизвестная операция!");
                }
                if (operation == "+" || operation == "-" || operation == "*" || operation == "/")
                {
                    double nextNumber = double.Parse(parts[2]);
                    switch (operation)
                    {
                        case "+": calc.Add(nextNumber); break;
                        case "-": calc.Subtract(nextNumber); break;
                        case "*": calc.Multiply(nextNumber); break;
                        case "/": calc.Divide(nextNumber); break;
                    }
                }
                else if (operation == "div" || operation == "mod")
                {
                    double nextNumber = double.Parse(parts[2]);
                    if (operation == "div")
                    {
                        Console.WriteLine($"Результат div: {calc.Div(nextNumber)}");
                    }
                    else
                    {
                        Console.WriteLine($"Результат mod: {calc.Mod(nextNumber)}");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
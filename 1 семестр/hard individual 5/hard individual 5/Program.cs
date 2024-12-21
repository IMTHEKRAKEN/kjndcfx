using System;
using System.Collections.Generic;
using System.Linq;

// Абстрактный класс для всех выражений
public abstract class Expression
{
    public abstract Expression Differentiate(string variable);
    public abstract double Evaluate(Dictionary<string, double> variables);
    public abstract override string ToString();
}

// Константа
public class Constant : Expression
{
    public double Value { get; }

    public Constant(double value)
    {
        Value = value;
    }

    public override Expression Differentiate(string variable)
    {
        return new Constant(0);
    }

    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

// Переменная
public class Variable : Expression
{
    public string Name { get; }

    public Variable(string name)
    {
        Name = name;
    }

    public override Expression Differentiate(string variable)
    {
        return Name == variable ? new Constant(1) : new Constant(0);
    }

    public override double Evaluate(Dictionary<string, double> variables)
    {
        if (variables.TryGetValue(Name, out double value))
        {
            return value;
        }
        else
        {
            throw new ArgumentException($"Variable '{Name}' not found in the dictionary.");
        }
    }

    public override string ToString()
    {
        return Name;
    }
}

// Сумма
public class Sum : Expression
{
    public Expression Left { get; }
    public Expression Right { get; }

    public Sum(Expression left, Expression right)
    {
        Left = left;
        Right = right;
    }

    public override Expression Differentiate(string variable)
    {
        return new Sum(Left.Differentiate(variable), Right.Differentiate(variable));
    }

    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Left.Evaluate(variables) + Right.Evaluate(variables);
    }
    public override string ToString()
    {
        return $"({Left} + {Right})";
    }
}

// Разность
public class Difference : Expression
{
    public Expression Left { get; }
    public Expression Right { get; }

    public Difference(Expression left, Expression right)
    {
        Left = left;
        Right = right;
    }

    public override Expression Differentiate(string variable)
    {
        return new Difference(Left.Differentiate(variable), Right.Differentiate(variable));
    }
    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Left.Evaluate(variables) - Right.Evaluate(variables);
    }
    public override string ToString()
    {
        return $"({Left} - {Right})";
    }
}

// Произведение
public class Product : Expression
{
    public Expression Left { get; }
    public Expression Right { get; }

    public Product(Expression left, Expression right)
    {
        Left = left;
        Right = right;
    }

    public override Expression Differentiate(string variable)
    {
        return new Sum(
            new Product(Left.Differentiate(variable), Right),
            new Product(Left, Right.Differentiate(variable)));
    }
    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Left.Evaluate(variables) * Right.Evaluate(variables);
    }
    public override string ToString()
    {
        return $"({Left} * {Right})";
    }
}

// Частное
public class Quotient : Expression
{
    public Expression Numerator { get; }
    public Expression Denominator { get; }

    public Quotient(Expression numerator, Expression denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    public override Expression Differentiate(string variable)
    {
        return new Quotient(
            new Difference(
                new Product(Numerator.Differentiate(variable), Denominator),
                new Product(Numerator, Denominator.Differentiate(variable))
            ),
            new Power(Denominator, new Constant(2))
        );
    }
    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Numerator.Evaluate(variables) / Denominator.Evaluate(variables);
    }
    public override string ToString()
    {
        return $"({Numerator} / {Denominator})";
    }
}


// Возведение в степень
public class Power : Expression
{
    public Expression Base { get; }
    public Expression Exponent { get; }

    public Power(Expression @base, Expression exponent)
    {
        Base = @base;
        Exponent = exponent;
    }

    public override Expression Differentiate(string variable)
    {
        if (Exponent is Constant constExponent)
        {
            if (Base is Variable varBase)
            {
                return new Product(
                    new Product(constExponent, new Power(varBase, new Constant(constExponent.Value - 1))),
                    Base.Differentiate(variable)
                    );
            }

            return new Product(
                    new Product(Exponent, new Power(Base, new Difference(Exponent, new Constant(1)))),
                    Base.Differentiate(variable));

        }

        return new Product(new Power(Base, Exponent),
            new Sum(
            new Product(Exponent.Differentiate(variable), new Ln(Base)),
            new Product(Exponent, new Quotient(Base.Differentiate(variable), Base))
        ));
    }
    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Math.Pow(Base.Evaluate(variables), Exponent.Evaluate(variables));
    }
    public override string ToString()
    {
        return $"({Base} ^ {Exponent})";
    }
}


// Синус
public class Sin : Expression
{
    public Expression Argument { get; }

    public Sin(Expression argument)
    {
        Argument = argument;
    }

    public override Expression Differentiate(string variable)
    {
        return new Product(new Cos(Argument), Argument.Differentiate(variable));
    }
    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Math.Sin(Argument.Evaluate(variables));
    }
    public override string ToString()
    {
        return $"sin({Argument})";
    }
}

// Косинус
public class Cos : Expression
{
    public Expression Argument { get; }

    public Cos(Expression argument)
    {
        Argument = argument;
    }

    public override Expression Differentiate(string variable)
    {
        return new Product(new Product(new Constant(-1), new Sin(Argument)), Argument.Differentiate(variable));
    }
    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Math.Cos(Argument.Evaluate(variables));
    }
    public override string ToString()
    {
        return $"cos({Argument})";
    }
}

// Тангенс
public class Tan : Expression
{
    public Expression Argument { get; }

    public Tan(Expression argument)
    {
        Argument = argument;
    }

    public override Expression Differentiate(string variable)
    {
        return new Product(new Quotient(new Constant(1), new Power(new Cos(Argument), new Constant(2))), Argument.Differentiate(variable));
    }

    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Math.Tan(Argument.Evaluate(variables));
    }
    public override string ToString()
    {
        return $"tan({Argument})";
    }
}

// Экспонента
public class Exp : Expression
{
    public Expression Argument { get; }

    public Exp(Expression argument)
    {
        Argument = argument;
    }

    public override Expression Differentiate(string variable)
    {
        return new Product(new Exp(Argument), Argument.Differentiate(variable));
    }
    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Math.Exp(Argument.Evaluate(variables));
    }
    public override string ToString()
    {
        return $"exp({Argument})";
    }
}

// Натуральный логарифм
public class Ln : Expression
{
    public Expression Argument { get; }

    public Ln(Expression argument)
    {
        Argument = argument;
    }

    public override Expression Differentiate(string variable)
    {
        return new Product(new Quotient(new Constant(1), Argument), Argument.Differentiate(variable));
    }
    public override double Evaluate(Dictionary<string, double> variables)
    {
        return Math.Log(Argument.Evaluate(variables));
    }
    public override string ToString()
    {
        return $"ln({Argument})";
    }
}
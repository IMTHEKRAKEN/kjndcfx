using System;

public abstract class Expression
{
    public abstract double Evaluate(double x);
    public abstract string ToString();
    public abstract Expression Differentiate();
}

public class Constant : Expression
{
    public double Value { get; }
    public Constant(double value) => Value = value;
    public override double Evaluate(double x) => Value;
    public override string ToString() => Value.ToString();
    public override Expression Differentiate() => new Constant(0);
}

public class Variable : Expression
{
    public override double Evaluate(double x) => x;
    public override string ToString() => "x";
    public override Expression Differentiate() => new Constant(1);
}

public class UnaryOperation : Expression
{
    public string Operator { get; }
    public Expression Operand { get; }

    public UnaryOperation(string op, Expression operand)
    {
        Operator = op;
        Operand = operand;
    }

    public override double Evaluate(double x)
    {
        switch (Operator)
        {
            case "sin": return Math.Sin(Operand.Evaluate(x));
            case "cos": return Math.Cos(Operand.Evaluate(x));
            case "exp": return Math.Exp(Operand.Evaluate(x));
            case "ln": return Math.Log(Operand.Evaluate(x));
            default: throw new ArgumentException($"Unknown unary operator: {Operator}");
        }
    }

    public override string ToString() => $"{Operator}({Operand})";

    public override Expression Differentiate()
    {
        switch (Operator)
        {
            case "sin": return new Product(new UnaryOperation("cos", Operand), Operand.Differentiate());
            case "cos": return new Product(new Constant(-1), new Product(new UnaryOperation("sin", Operand), Operand.Differentiate()));
            case "exp": return new Product(this, Operand.Differentiate());
            case "ln": return new Quotient(Operand.Differentiate(), Operand);
            default: throw new ArgumentException($"Derivative of {Operator} not implemented");
        }
    }
}

public class BinaryOperation : Expression
{
    public string Operator { get; }
    public Expression Left { get; }
    public Expression Right { get; }

    public BinaryOperation(string op, Expression left, Expression right)
    {
        Operator = op;
        Left = left;
        Right = right;
    }

    public override double Evaluate(double x)
    {
        switch (Operator)
        {
            case "+": return Left.Evaluate(x) + Right.Evaluate(x);
            case "-": return Left.Evaluate(x) - Right.Evaluate(x);
            case "*": return Left.Evaluate(x) * Right.Evaluate(x);
            case "/": return Left.Evaluate(x) / Right.Evaluate(x);
            case "^": return Math.Pow(Left.Evaluate(x), Right.Evaluate(x));
            default: throw new ArgumentException($"Unknown binary operator: {Operator}");
        }
    }

    public override string ToString() => $"({Left} {Operator} {Right})";

    public override Expression Differentiate()
    {
        switch (Operator)
        {
            case "+": return new Sum(Left.Differentiate(), Right.Differentiate());
            case "-": return new Difference(Left.Differentiate(), Right.Differentiate());
            case "*": return new Sum(new Product(Left.Differentiate(), Right), new Product(Left, Right.Differentiate()));
            case "/": return new Quotient(new Difference(new Product(Left.Differentiate(), Right), new Product(Left, Right.Differentiate())), new Power(Right, new Constant(2)));
            case "^":
                if (Right is Constant c) return new Product(new Constant(c.Value), new Power(Left, new Constant(c.Value - 1)));
                else throw new NotImplementedException("Derivative of x^y not implemented");
            default: throw new ArgumentException($"Derivative of {Operator} not implemented");
        }
    }
}

public class Sum : Expression { public Expression Left, Right; public Sum(Expression left, Expression right) { Left = left; Right = right; } public override double Evaluate(double x) => Left.Evaluate(x) + Right.Evaluate(x); public override string ToString() => $"({Left} + {Right})"; public override Expression Differentiate() => new Sum(Left.Differentiate(), Right.Differentiate()); }
public class Difference : Expression { public Expression Left, Right; public Difference(Expression left, Expression right) { Left = left; Right = right; } public override double Evaluate(double x) => Left.Evaluate(x) - Right.Evaluate(x); public override string ToString() => $"({Left} - {Right})"; public override Expression Differentiate() => new Difference(Left.Differentiate(), Right.Differentiate()); }
public class Product : Expression { public Expression Left, Right; public Product(Expression left, Expression right) { Left = left; Right = right; } public override double Evaluate(double x) => Left.Evaluate(x) * Right.Evaluate(x); public override string ToString() => $"({Left} * {Right})"; public override Expression Differentiate() => new Sum(new Product(Left.Differentiate(), Right), new Product(Left, Right.Differentiate())); }
public class Quotient : Expression { public Expression Left, Right; public Quotient(Expression left, Expression right) { Left = left; Right = right; } public override double Evaluate(double x) => Left.Evaluate(x) / Right.Evaluate(x); public override string ToString() => $"({Left} / {Right})"; public override Expression Differentiate() => new Quotient(new Difference(new Product(Left.Differentiate(), Right), new Product(Left, Right.Differentiate())), new Power(Right, new Constant(2))); }
public class Power : Expression { public Expression Left, Right; public Power(Expression left, Expression right) { Left = left; Right = right; } public override double Evaluate(double x) => Math.Pow(Left.Evaluate(x), Right.Evaluate(x)); public override string ToString() => $"({Left}^{Right})"; public override Expression Differentiate() { if (Right is Constant c) return new Product(new Constant(c.Value), new Power(Left, new Constant(c.Value - 1))); else throw new NotImplementedException("Derivative of x^y not implemented"); } }


public class DifferentiatorTests
{
    [Fact]
    public void TestConstant() => Assert.Equal("0", new Constant(5).Differentiate().ToString());

    [Fact]
    public void TestVariable() => Assert.Equal("1", new Variable().Differentiate().ToString());

    [Fact]
    public void TestSinX() => Assert.Equal("(cos(x) * 1)", new UnaryOperation("sin", new Variable()).Differentiate().ToString());
}
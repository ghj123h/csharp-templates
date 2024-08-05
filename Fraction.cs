public readonly struct Fraction : IComparable<Fraction>
{
    private readonly int num;
    private readonly int den;

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
        }
        num = numerator;
        den = denominator;
    }

    public static Fraction operator +(Fraction a) => a;
    public static Fraction operator -(Fraction a) => new Fraction(-a.num, a.den);

    public static Fraction operator +(Fraction a, Fraction b)
        => new Fraction(a.num * b.den + b.num * a.den, a.den * b.den);
    public static Fraction operator -(Fraction a, Fraction b)
        => a + (-b);
    public static Fraction operator *(Fraction a, Fraction b)
        => new Fraction(a.num * b.num, a.den * b.den);
    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.num == 0)
        {
            throw new DivideByZeroException();
        }
        return new Fraction(a.num * b.den, a.den * b.num);
    }

    public static bool operator <(Fraction a, Fraction b) => a.CompareTo(b) < 0;
    public static bool operator <=(Fraction a, Fraction b) => a.CompareTo(b) <= 0;
    public static bool operator >(Fraction a, Fraction b) => a.CompareTo(b) > 0;
    public static bool operator >=(Fraction a, Fraction b) => a.CompareTo(b) >= 0;
    public static bool operator ==(Fraction a, Fraction b) => a.CompareTo(b) == 0;
    public static bool operator !=(Fraction a, Fraction b) => a.CompareTo(b) != 0;

    public static implicit operator double(Fraction a) => a.num * 1.0 / a.den;

    public Fraction Simplify() => new Fraction(num / Gcd(num, den), den / Gcd(num, den));

    public int CompareTo(Fraction other) => num * other.den - other.num * den;
    public override string ToString() => $"{num} / {den}";
    public override bool Equals(object? o) => o is Fraction fraction && CompareTo(fraction) == 0;
    public override int GetHashCode() => (num, den).GetHashCode();

    private int Gcd(int a, int b) => a == 0 ? b : Gcd(b % a, a);
}
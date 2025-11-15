using System.Numerics;
using System.Text.RegularExpressions;

namespace NerdStore.Core.DomainObjects;

public static class Validations
{
    public static void IfEqual(object object1, object object2, string message)
    {
        if (!object1.Equals(object2))
        {
            throw new DomainException(message);
        }
    }

    public static void IfDifferent(object object1, object object2, string message)
    {
        if (object1.Equals(object2))
        {
            throw new DomainException(message);
        }
    }

    public static void StringSize(string value, int max, string message)
    {
        var length = value.Trim().Length;
        if (length > max)
        {
            throw new DomainException(message);
        }
    }

    public static void StringSize(string value, int min, int max, string message)
    {
        var length = value.Trim().Length;
        if (length < min || length > max)
        {
            throw new DomainException(message);
        }
    }

    public static void Expression(string pattern, string value, string message)
    {
        if (!Regex.IsMatch(value, pattern))
        {
            throw new DomainException(message);
        }
    }

    public static void IfEmpty(string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException(message);
        }
    }

    public static void IfNull(object object1, string message)
    {
        if (object1 is null)
        {
            throw new DomainException(message);
        }
    }

    public static void IfFalse(bool value, string message)
    {
        if (value)
        {
            throw new DomainException(message);
        }
    }

    public static void Min<T>(T value, T min, string message)
        where T : INumber<T>
    {
        if (value < min)
        {
            throw new DomainException(message);
        }
    }

    public static void MinOrEqual<T>(T value, T min, string message)
    where T : INumber<T>
    {
        if (value <= min)
        {
            throw new DomainException(message);
        }
    }

    public static void Max<T>(T value, T min, string message)
        where T : INumber<T>
    {
        if (value > min)
        {
            throw new DomainException(message);
        }
    }

    public static void MinMax<T>(T value, T min, string message)
        where T : INumber<T>
    {
        if (value < min || value > min)
        {
            throw new DomainException(message);
        }
    }
}

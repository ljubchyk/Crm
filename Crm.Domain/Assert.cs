using System.Collections;

namespace Crm.Domain;

public static class Assert
{
    public static void NotEmpty(string input, string paramName)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }

    public static void NotEmpty(Guid input, string paramName)
    {
        if (Guid.Empty == input)
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }

    public static void NotNull(IEnumerable input, string paramName)
    {
        if (input is null)
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }

    public static void GreaterThanZero(double input, string paramName)
    {
        if (input <= 0)
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }
}

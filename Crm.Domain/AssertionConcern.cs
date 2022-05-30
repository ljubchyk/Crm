using System.Collections;

namespace Crm.Domain;

public class AssertionConcernClass
{
    protected static void AssertNotEmpty(string input, string paramName)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }

    protected static void AssertNotEmpty(Guid input, string paramName)
    {
        if (Guid.Empty == input)
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }

    protected static void AssertNotNull(IEnumerable input, string paramName)
    {
        if (input is null)
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }

    protected static void AssertGreaterThanZero(double input, string paramName)
    {
        if (input <= 0)
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }
}

public record AssertionConcernRecord
{
    protected static void AssertNotEmpty(string input, string paramName)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }

    protected static void AssertNotEmpty(Guid input, string paramName)
    {
        if (Guid.Empty == input)
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }

    protected static void AssertGreaterThanZero(double input, string paramName)
    {
        if (input <= 0)
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }
}
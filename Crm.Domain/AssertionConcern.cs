namespace Crm.Domain;

public class AssertionConcern
{
    protected static void AssertNotEmpty(string input, string paramName)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }
}

public static class AssertionConcernExtentions
{
    public static void AssertNotEmpty(this string input, string paramName)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"The {paramName} must be provided.");
        }
    }
}
namespace FinanceApp_Databaser;

public static class ValidationHelper
{
    public static bool IsNullOrEmpty(string? input)
    {
        return string.IsNullOrEmpty(input);
    }

    public static bool IsNullOrWhiteSpace(string? input)
    {
        return string.IsNullOrWhiteSpace(input);
    }

    /// <summary>
    /// Checks if input is empty and returns a bool
    /// </summary>
    /// <param name="input"></param>
    /// <param name="errorMessage"></param>
    /// <returns>Bool</returns>
    public static bool ValidateNotEmpty(string? input, string errorMessage)
    {
        if (IsNullOrEmpty(input) || IsNullOrWhiteSpace(input))
        {
            Utilities.WaitForKeyAny(errorMessage);
            return false;
        }

        return true;
    }
}

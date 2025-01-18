namespace FinanceApp_Databaser;

public class DateSelectionResult
{
    // Indicates if the user chose to exit the menu
    public bool ShouldExit { get; set; }

    // The type of date filter selected (YEAR, MONTH, WEEK, etc.)
    public DateType DateType { get; set; }

    // The actual date parameters (year, month, etc.)
    public List<string> DateInput { get; set; } = new();
}

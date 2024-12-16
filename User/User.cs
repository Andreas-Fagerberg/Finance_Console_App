namespace FinanceApp_Databaser;

public class User
{
    public Guid UserId { get; init; }
    public string? Name { get; set; }
    public string? Password { get; set; }
}

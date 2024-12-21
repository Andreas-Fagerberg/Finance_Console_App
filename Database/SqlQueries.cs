public static class SqlQueries
{
    // Query for transactions by user ID and Year
    public static string GetTransactionsByUserIdAndYear =>
        @"
        SELECT * FROM transactions 
        WHERE user_id = @user_id 
        AND EXTRACT(YEAR FROM transfer_date) = @year";

    // Query for transactions by user ID and Month
    public static string GetTransactionsByUserIdAndMonth =>
        @"
        SELECT * FROM transactions 
        WHERE user_id = @user_id 
        AND EXTRACT(YEAR FROM transfer_date) = @year 
        AND EXTRACT(MONTH FROM transfer_date) = @month";

    // Query for transactions by user ID and Week of the year
    public static string GetTransactionsByUserIdAndWeek =>
        @"
        SELECT * FROM transactions 
        WHERE user_id = @user_id 
        AND EXTRACT(YEAR FROM transfer_date) = @year 
        AND EXTRACT(WEEK FROM transfer_date) = @week";

    // Query for transactions by user ID and specific date
    public static string GetTransactionsByUserIdAndDate =>
        @"
        SELECT * FROM transactions 
        WHERE user_id = @user_id 
        AND transfer_date = @date";

    // Query for transactions by user ID within a date range (Start Date to End Date)
    public static string GetTransactionsByUserIdAndDateRange =>
        @"
        SELECT * FROM transactions 
        WHERE user_id = @user_id 
        AND transfer_date BETWEEN @StartDate AND @EndDate";
    public static string GetTransactionsByUserId =>
        @"
        SELECT * FROM transactions 
        WHERE user_id = @user_id";

    // Query for transactions by user ID and a specific day of the week
    public static string GetTransactionsByUserIdAndDayOfWeek =>
        @"
        SELECT * FROM transactions 
        WHERE user_id = @user_id 
        AND EXTRACT(DOW FROM transfer_date) = @DayOfWeek"; // DOW returns 0 for Sunday, 1 for Monday, etc.
}

namespace FinanceApp_Databaser;

public class SubMenu
{
    public static void Display(SubMenuType choice)
    {
        Console.Clear();
        switch (choice)
        {
            case SubMenuType.AddTransaction:
                Console.Write(
                    """
                    | ADD TRANSACTION |

                    [1] - Add a new transaction
                    [2] - Return to the main menu.

                    Command: 
                    """
                );
                break;

            case SubMenuType.RegisterUser:
                Console.Write(
                    """
                    | REGISTER USER |

                    [1] - Register a new user
                    [2] - Return to the login menu.

                    Command: 
                    """
                );
                break;
            case SubMenuType.LogOut:
                Console.Write(
                    """
                    Are you sure would like to log out?

                    [1] - Log out user.
                    [2] - Return to the main menu.

                    Command: 
                    """
                );
                break;
        }
    }
}

public enum SubMenuType
{
    AddTransaction,
    RegisterUser,
    LogOut,
}

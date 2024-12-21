namespace FinanceApp_Databaser;

public class SubMenu
{
    public static void Display(SubMenuType choice)
    {
        Console.Clear();
        switch (choice)
        {
            case SubMenuType.ADDTRANSACTION:
                Console.Write(
                    """
                    | ADD TRANSACTION |

                    [1] - Add a new transaction
                    [2] - Return to the main menu.

                    Command: 
                    """
                );
                break;
            case SubMenuType.DISPLAYTRANSACTION1:
                Console.Write(
                    """
                    | DISPLAY TRANSACTIONS |

                    [1] - Display transactions
                    [2] - Return to the main menu.

                    Command: 
                    """
                );
                break;

            case SubMenuType.DISPLAYTRANSACTION2:
                Console.Write(
                    """
                    | DISPLAY TRANSACTIONS - FILTER BY |

                    Please choose an option to filter the transactions by:

                    [1] - Year
                    [2] - Month
                    [3] - Week
                    [4] - Specific date
                    [5] - All

                    Command: 
                    """
                );
                break;

            case SubMenuType.REGISTERUSER:
                Console.Write(
                    """
                    | REGISTER USER |

                    [1] - Register a new user
                    [2] - Return to the login menu.

                    Command: 
                    """
                );
                break;
            case SubMenuType.LOGOUT:
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

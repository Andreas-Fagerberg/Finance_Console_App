namespace FinanceApp_Databaser;

// Del 1:
// Krav för G:

//     Följ instruktionerna i beskrivningen
//     Formatera koden: små misstag ignoreras men större fel ger IG (om du blir osäker så kan du fråga läraren)


// Krav för VG (gör minst en av följande punkter):

//     Spara och ladda all data till och från fil
//     Bygg applikationen med ett graphical user-interface (GUI)

// Använd ett bibliotek för detta.

//     Skriv fem unit-tester


// Beskrivning


// Bygg en personal-finance applikation som fungerar i terminalen (eller med UI för VG). I applikationen skall man kunna göra följande:

//     Lägga in transaktioner (manuellt; när du exempelvis har köpt något eller fått lön)
//     Radera transaktioner (manuellt)
//     Se nuvarande kontobalans
//     Se pengar spenderade årsvis, månadsvis, veckovis och dagvis
//     Se inkomst årsvis, månadsvis, veckovis och dagvis


// Om du inte går för VG så behöver ingen data sparas. Det betyder att du måste börja om med att lägga in transaktioner och annat varje gång applikationen startas om.

// Del 2:

// Krav för G:

// - Följ instruktionerna i beskrivningen

// - Använd Git för versionshantering

// - Använd PostgreSQL som databas


// Krav för VG:

// - Uppnå alla krav för G

// - Spara kontoinformation på ett säkert sätt (hashing av lösenord)

// - Använd SQL JOINS för data hämtning när det går

// - Använd minst två SQL TRANSACTIONS

// - Använd alla normalformer (1NF, 2NF, 3NF)

// - Felhantera alla databasoperationer (try/catch, resource releasing, using)


// Beskrivning


// Fortsätt på Personal Finance projektet från förra kursen. Det är okej att skriva om projektet från scratch om det känns enklare med kommande uppgifter.


// Applikationen skall koppla på och använda en databas för att spara och hantera information. Alla transaktioner skall sparas och hämtas och raderas och uppdateras till/från databas. Det skall även finnas ett kontosystem, med vilket man kan logga in på olika användare, byta mellan dem och lägga till transaktioner. All funktionalitet som fanns i uppgiften från förra kursen skall finnas nu, men kan skall kunna göra det per-användare. En användare kan inte se transaktioner för en annan användare.


// Förtydligande krav för kontosystem, följande möjligheter skall finnas:

// - Registrera användare genom namn och lösenord

// - Logga in genom namn och lösenord

// - Logga ut (och kunna byta användare genom att logga in igen)

// - All funktionalitet från tidigare uppgift, kopplat per-användare


// Använd GUI eller terminal-app valfritt.




class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("FINANCE");
    }
}

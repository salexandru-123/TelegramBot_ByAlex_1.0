
class ConfigClass{

    public static string Config(string what)
    {
        switch (what)
        {
            case "api_id": 
                Console.WriteLine("Api Id: ");
                return Console.ReadLine()!; 

            case "api_hash":
                Console.WriteLine("Api Hash: "); 
                return Console.ReadLine()!;

            case "phone_number":
                Console.Write("Your Telegram Phone Number: +39"); 
                return "+PhonePrefix"+Console.ReadLine()!.Trim(' '); 

            case "verification_code": 
                Console.Write("Code: "); 
                return Console.ReadLine()!;

            case "first_name": return "your_first_name";      // if sign-up is required
            case "last_name": return "your_last_name";        // if sign-up is required
            case "password": return "your_2FA_password";     // if user has enabled 2FA
            default: return null!;                  // let WTelegramClient decide the default config
        }
    }
}

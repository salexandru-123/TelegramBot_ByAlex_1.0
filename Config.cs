
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
                return "+39"+Console.ReadLine()!.Trim(' '); 

            case "verification_code": 
                Console.Write("Code: "); 
                return Console.ReadLine()!;

            case "first_name": return "Stefan Alexandru";      // if sign-up is required
            case "last_name": return "Soreanu";        // if sign-up is required
            case "password": return "password123";     // if user has enabled 2FA
            default: return null!;                  // let WTelegramClient decide the default config
        }
    }
}

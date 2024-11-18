
class ConfigClass{


    public static string Config(string what)
    {
        switch (what)
        {
            case "api_id": return "";
            case "api_hash": return "";
            case "phone_number": return "your_phone_number";
            case "verification_code": Console.Write("Code: "); return Console.ReadLine()!;
            case "first_name": return "Stefan Alexandru";      // if sign-up is required
            case "last_name": return "Soreanu";        // if sign-up is required
            case "password": return "add_your_own_password";     // if user has enabled 2FA
            default: return null!;                  // let WTelegramClient decide the default config
        }
    }
}

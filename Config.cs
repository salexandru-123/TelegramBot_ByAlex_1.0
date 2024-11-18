
class ConfigClass{

    public static string api_key= "";
    public static string Config(string what)
    {
        switch (what)
        {
            case "api_id": return "";
            case "api_hash": return "";
            case "phone_number": return "";
            case "verification_code": Console.Write("Code: "); return Console.ReadLine()!;
            case "first_name": return "Stefan Alexandru";      // if sign-up is required
            case "last_name": return "Soreanu";        // if sign-up is required
            case "password": return "";     // if user has enabled 2FA
            default: return null!;                  // let WTelegramClient decide the default config
        }
    }
}

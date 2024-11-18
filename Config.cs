
class ConfigClass{

    public static string api_key= "your_api_key";
    public static string Config(string what)
    {
        switch (what)
        {
            case "api_id": return "your_app_api_id";
            case "api_hash": return "your_app_api_hash";
            case "phone_number": return "your_phone_with_prefix";
            case "verification_code": Console.Write("Code: "); return Console.ReadLine()!;
            case "first_name": return "Stefan Alexandru";      // if sign-up is required
            case "last_name": return "Soreanu";        // if sign-up is required
            case "password": return "your_2FA_password";     // if user has enabled 2FA
            default: return null!;                  // let WTelegramClient decide the default config
        }
    }
}

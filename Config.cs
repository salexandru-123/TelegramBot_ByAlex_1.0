
class ConfigClass{


    public static string Config(string what)
    {
        switch (what)
        {
            case "api_id": return "20587725";
            case "api_hash": return "00fbea4bc3d73d8f3ec935fe76bc8d1f";
            case "phone_number": return "+393515698542";
            case "verification_code": Console.Write("Code: "); return Console.ReadLine()!;
            case "first_name": return "Stefan Alexandru";      // if sign-up is required
            case "last_name": return "Soreanu";        // if sign-up is required
            case "password": return "password123";     // if user has enabled 2FA
            default: return null!;                  // let WTelegramClient decide the default config
        }
    }
}
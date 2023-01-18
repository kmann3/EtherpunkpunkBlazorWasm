namespace EtherpunkBlazorWasm.Server;
public class AppSettings
{
    public const string ValidAudience = "EpunkAud";
    public const string ValidIssuer = "EpunkIss";

    /// <summary>
    /// Secret key. This can be a passphrase / password / guid. Doesn't matter. Something unique. Don't share this.
    /// </summary>
    public const string SecretKey = "4185ac7a3ee64060a3e895562d509c52";

    /// <summary>
    /// Salt used for hashing. This MUST be 22 characters.
    /// </summary>
    public const string PasswordSalt = "k87L/MF28Q673VKh8/cPi.";

    /// <summary>
    /// How many days before a token expires. If you plan on revoking tokens, this is also how long you'll need to maintain a list of active tokens so you can revoke them / blacklist them.
    /// </summary>
    public const int TokenLengthInDays = 1;

    /// <summary>
    /// Maintaining a list of valid user tokens is an option if you'd like the ability to revoke tokens or allow users to logout from other sessions.
    /// The main reason to set this to false would be if you don't care to maintain a revocation list or spend the cycles on it.
    /// You might consider this if you're on a shared host and need to be very considerate of cpu/db cycles or if it's a personal site where expiration doesn't -really- matter to you (e.g. intranet stuff).
    /// For public facing stuff you'll want to consider things like a user forgets to logout at a library or had a device stolen.
    /// </summary>
    public const bool LogUserTokens = true;
}

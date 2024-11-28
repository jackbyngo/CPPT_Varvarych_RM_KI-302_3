namespace lab4.Entity;

public class AuthEntity(string login, string password)
{
    public static int userId { get; set; }
    public string Login { get; set; } = login;
    public string Password { get; set; } = password;

    public AuthEntity() : this(string.Empty, string.Empty) { }
}
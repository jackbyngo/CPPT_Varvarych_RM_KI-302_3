namespace lab4.Entity;

public class RegisterEntity(string login, string password, string confirmationPassword)
{
    public string Login { get; set; } = login;
    public string Password { get; set; } = password;
    public string ConfirmationPassword { get; set; } = confirmationPassword;

    public RegisterEntity() : this(string.Empty, string.Empty, string.Empty) { }
}
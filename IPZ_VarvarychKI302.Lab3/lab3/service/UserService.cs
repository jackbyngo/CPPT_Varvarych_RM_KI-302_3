using Microsoft.EntityFrameworkCore;
using lab3.api;
using lab3.model;

namespace lab3.service;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(AppDbContext context, ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<(bool success, string message)> RegisterUser(string username, string password,
        string passwordConfirm)
    {
        // Перевірка, чи вже існує користувач
        if (await _context.Users.AnyAsync(u => u.Username == username))
        {
            _logger.LogWarning("Реєстрація не вдалася. Користувач {Username} вже існує.", username);
            return (false, "Користувач вже існує.");
        }

        // Перевірка на порожнє ім'я користувача
        if (string.IsNullOrWhiteSpace(username))
        {
            _logger.LogWarning("Реєстрація не вдалася. Ім'я користувача не може бути порожнім.");
            return (false, "Ім'я користувача не може бути порожнім.");
        }

        // Перевірка на мінімальну довжину імені користувача
        if (username.Length < 5)
        {
            _logger.LogWarning("Реєстрація не вдалася. Ім'я користувача повинно містити щонайменше 5 символів.");
            return (false, "Ім'я користувача повинно містити щонайменше 5 символів.");
        }

        // Перевірка на мінімальну довжину пароля
        if (password.Length < 8)
        {
            _logger.LogWarning("Реєстрація не вдалася. Пароль повинен містити щонайменше 8 символів.");
            return (false, "Пароль повинен містити щонайменше 8 символів.");
        }

        // Перевірка на відповідність паролів
        if (password != passwordConfirm)
        {
            _logger.LogWarning("Реєстрація не вдалася. Паролі не співпадають");
            return (false, "Реєстрація не вдалася. Паролі не співпадають");
        }

        // Перевірка на складність пароля (наявність цифр, великих і малих літер)
        if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
        {
            _logger.LogWarning("Реєстрація не вдалася. Пароль повинен містити великі і малі літери, а також цифри.");
            return (false, "Пароль повинен містити великі і малі літери, а також цифри.");
        }

        // Перевірка на недопустимі символи в імені користувача
        if (username.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            _logger.LogWarning("Реєстрація не вдалася. Ім'я користувача може містити лише літери і цифри.");
            return (false, "Ім'я користувача може містити лише літери і цифри.");
        }

        var user = new User
        {
            Username = username,
            Password = password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Користувач {Username} успішно зареєстрований.", username);
        return (true, user.Id.ToString());
    }


    public async Task<(bool success, string message)> LoginUser(string username, string password)
    {
        _logger.LogInformation("Спроба входу для користувача: {Username}", username);

        // Перевірка на порожнє ім'я користувача
        if (string.IsNullOrWhiteSpace(username))
        {
            _logger.LogWarning("Вхід не вдався. Ім'я користувача не може бути порожнім.");
            return (false, "Ім'я користувача не може бути порожнім.");
        }

        // Перевірка на порожній пароль
        if (string.IsNullOrWhiteSpace(password))
        {
            _logger.LogWarning("Вхід не вдався. Пароль не може бути порожнім.");
            return (false, "Пароль не може бути порожнім.");
        }

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);

        if (user == null)
        {
            _logger.LogWarning("Вхід не вдався. Користувача {Username} не знайдено або пароль невірний.", username);
            return (false, "Користувача не знайдено або пароль невірний.");
        }

        _logger.LogInformation("Користувач {Username} успішно увійшов.", username);
        return (true, user.Id.ToString());
    }

}
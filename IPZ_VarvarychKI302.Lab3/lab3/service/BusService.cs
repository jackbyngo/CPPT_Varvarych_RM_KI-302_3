using Microsoft.EntityFrameworkCore;
using lab3.api;
using lab3.model;

namespace lab3.service;

public class BusService
{
    private readonly AppDbContext _context;
    private readonly ILogger<BusService> _logger;

    public BusService(AppDbContext context, ILogger<BusService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<(bool success, string message)> CreateBus(string name, int number, int amountSeats)
    {
        try
        {
            var existingBus = await _context.Buses
                .FirstOrDefaultAsync(t => t.Number == number);

            if (existingBus != null)
            {
                return (false, $"Автобус з номером {number} вже існує");
            }

            var bus = new Bus
            {
                Name = name,
                Number = number,
                AmountSeats = amountSeats
            };

            await _context.Buses.AddAsync(bus);
            await _context.SaveChangesAsync();

            var seats = new List<Seat>();
            for (int i = 1; i <= amountSeats; i++)
            {
                var seat = new Seat
                {
                    Number = i,
                    IsAvailable = true,
                    BusId = bus.Id
                };
                seats.Add(seat);
            }

            await _context.Seats.AddRangeAsync(seats);
            await _context.SaveChangesAsync();


            _logger.LogInformation($"Створено новий автобус: {name} (#{number}) з {seats.Count} місцями");
            return (true, $"Автобус успішно створено з {seats.Count} місцями");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Помилка при створенні автобусу");
            return (false, "Сталася помилка при створенні автобусу");
        }
    }

   public async Task<(bool success, string message)> TakeASeat(long userId, int busId, int seatNumber)
{
    using var transaction = await _context.Database.BeginTransactionAsync();
    try
    {
        // Отримуємо користувача по ID
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return (false, "Користувача не існує");
        }

        // Отримуємо місце за ID автобуса та номером місця
        var seat = await _context.Seats
            .Include(s => s.Bookings) // Завантажуємо бронювання місця
            .FirstOrDefaultAsync(s => s.BusId == busId && s.Number == seatNumber);

        if (seat == null)
        {
            return (false, "Місце не існує в цьому автобусі");
        }

        // Перевіряємо, чи є активне бронювання на це місце
        var hasActiveBooking = seat.Bookings.Any(b => b.IsActive);
        if (hasActiveBooking)
        {
            return (false, "Місце вже заброньоване");
        }

        // Створюємо нове бронювання
        var booking = new Booking
        {
            UserId = userId,
            SeatId = seat.Id,
            BookingDate = DateTime.UtcNow,
            IsActive = true
        };

        // Додаємо бронювання в базу даних
        await _context.Bookings.AddAsync(booking);

        // Оскільки місце заброньоване, робимо його недоступним
        seat.IsAvailable = false;

        // Зберігаємо зміни в базі даних
        await _context.SaveChangesAsync();

        // Завершуємо транзакцію
        await transaction.CommitAsync();

        // Логування успіху
        _logger.LogInformation($"Місце {seatNumber} у автобусі {busId} успішно заброньовано користувачем {userId}");
        return (true, "Місце успішно заброньовано");
    }
    catch (Exception ex)
    {
        // В разі помилки відкатуємо транзакцію
        await transaction.RollbackAsync();
        _logger.LogError(ex, "Помилка при бронюванні місця");
        return (false, "Сталася помилка при бронюванні місця");
    }
}

}
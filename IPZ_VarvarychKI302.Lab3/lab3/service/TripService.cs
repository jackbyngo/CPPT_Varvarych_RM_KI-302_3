using Microsoft.EntityFrameworkCore;
using lab3.api;
using lab3.dto;
using lab3.model;

public class TripService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TripService> _logger;

    public TripService(AppDbContext context, ILogger<TripService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<(bool success, string message)> CreateTrip(string name, int busId, DateTime startDate, DateTime endDate)
    {
        try
        {
            var bus = await _context.Buses
                .FirstOrDefaultAsync(t => t.Id == busId);

            if (bus == null)
            {
                return (false, $"Автобуса з id {busId} не існує");
            }

            if (startDate >= endDate)
            {
                return (false, "Дата початку повинна бути раніше дати закінчення");
            }

            if (startDate < DateTime.Now)
            {
                return (false, "Дата початку не може бути в минулому");
            }
            
            var hasOverlap = await _context.Trips
                .AnyAsync(t => t.BusId == busId && 
                             ((t.StartDate <= startDate && t.EndDate >= startDate) ||
                              (t.StartDate <= endDate && t.EndDate >= endDate) ||
                              (t.StartDate >= startDate && t.EndDate <= endDate)));

            if (hasOverlap)
            {
                return (false, "Цей автобус вже має рейс на вказаний період");
            }
            
            var trip = new Trip
            {
                Name = name,
                BusId = busId,
                StartDate = startDate,
                EndDate = endDate
            };
            
            await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation($"Створено новий рейс: {name}");
            return (true, $"Створено новий рейс: {name}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Помилка при створенні рейсу");
            return (false, "Сталася помилка при створенні рейсу");
        }
    }
    
    
    public async Task<List<TripGetResponse>> GetTrips()
    {
        try
        {
            var trips = await _context.Trips
                .Include(t => t.Bus)
                .Select(t => new TripGetResponse
                {
                    Id = t.Id,
                    TripName = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    BusId = t.Bus.Id,
                    BusName = t.Bus.Name,
                    Number = t.Bus.Number,
                    AmountSeats = t.Bus.AmountSeats
                })
                .ToListAsync();

            return trips;
        }
        catch (Exception ex)
        {
            return new List<TripGetResponse>();
        }
    }
    
    public async Task<List<TripGetResponse>> SearchTripsByName(string searchTerm)
    {
        try
        {
            var trips = await _context.Trips
                .Include(t => t.Bus)
                .Where(t => t.Name.Contains(searchTerm))
                .Select(t => new TripGetResponse
                {
                    Id = t.Id,
                    TripName = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    BusName = t.Bus.Name,
                    Number = t.Bus.Number,
                    AmountSeats = t.Bus.AmountSeats
                })
                .ToListAsync();

            return trips;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching trips");
            return new List<TripGetResponse>();
        }
    }
    
    public async Task<List<TripGetResponse>> SearchTripsByStartDate(DateTime startDate)
    {
        try
        {
            var trips = await _context.Trips
                .Include(t => t.Bus)
                .Where(t => t.StartDate >= startDate) 
                .Select(t => new TripGetResponse
                {
                    Id = t.Id,
                    TripName = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    BusName = t.Bus.Name,
                    Number = t.Bus.Number,
                    AmountSeats = t.Bus.AmountSeats
                })
                .ToListAsync();

            return trips;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching trips by start date");
            return new List<TripGetResponse>();
        }
    }
    
    public async Task<List<BookingGetResponse>> GetBookingsForTrip(int tripId)
    {
        try
        {
            var bookings = await _context.Bookings
                .Include(b => b.Seat)
                .ThenInclude(s => s.Bus)
                .ThenInclude(bus => bus.Trips) // Завантажуємо рейси, пов'язані з автобусом
                .Where(b => b.Seat.Bus.Trips.Any(t => t.Id == tripId)) // Фільтрація по tripId
                .ToListAsync();

            var response = bookings.Select(b => new BookingGetResponse
            {
                BookingId = b.Id,
                UserId = b.UserId,
                SeatNumber = b.Seat.Number,
                BusName = b.Seat.Bus.Name,
                TripName = b.Seat.Bus.Trips.FirstOrDefault(t => t.Id == tripId)?.Name,
                StartDate = b.Seat.Bus.Trips.FirstOrDefault(t => t.Id == tripId)?.StartDate,
                EndDate = b.Seat.Bus.Trips.FirstOrDefault(t => t.Id == tripId)?.EndDate
            }).ToList();

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching bookings for trip");
            return new List<BookingGetResponse>();
        }
    }





}
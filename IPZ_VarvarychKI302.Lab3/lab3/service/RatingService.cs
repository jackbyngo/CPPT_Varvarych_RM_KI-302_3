using Microsoft.EntityFrameworkCore;
using lab3.api;
using lab3.dto;
using lab3.model;

namespace lab3.service;

public class RatingService
{
    private readonly AppDbContext _context;
    private readonly ILogger<BusService> _logger;

    public RatingService(AppDbContext context, ILogger<BusService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<(bool success, string message)> AddRating(double number, int tripId)
    {
        try
        {
            var bus = await _context.Trips.FirstOrDefaultAsync(t => t.Id == tripId);
            if (bus == null)
            {
                return (false, "Trip не знайдено");
            }
            
            if (number < 1 || number > 5)
            {
                return (false, "Рейтинг повинен бути від 1 до 5");
            }
            
            var rating = new Rating
            {
                Number = number,
                TripId = tripId
            };

            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
            
            var averageRating = await _context.Ratings
                .Where(r => r.TripId == tripId)
                .AverageAsync(r => r.Number);

            _logger.LogInformation($"Додано новий рейтинг {number} для trip {tripId}. Середній рейтинг: {averageRating:F1}");
            return (true, $"Рейтинг успішно додано. Середній рейтинг trip: {averageRating:F1}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Помилка при додаванні рейтингу");
            return (false, "Сталася помилка при додаванні рейтингу");
        }
    }
    
    public async Task<List<TripRatingGetResponse>> GetTripsRating()
    {
        try
        {
            var trips = await _context.Trips
                .Include(t => t.Bus)
                .GroupBy(t => new 
                {
                    t.Id,
                    t.Name,
                    t.StartDate,
                    t.EndDate,
                    BusName = t.Bus.Name
                })
                .Select(g => new TripRatingGetResponse
                {
                    Id = g.Key.Id,
                    TripName = g.Key.Name,
                    StartDate = g.Key.StartDate,
                    EndDate = g.Key.EndDate,
                    BusName = g.Key.BusName,
                    Rating = g.SelectMany(t => _context.Ratings.Where(r => r.TripId == t.Id).Select(r => r.Number)).DefaultIfEmpty().Average()  // Calculate average rating
                })
                .ToListAsync();

            return trips;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching trip ratings");
            return new List<TripRatingGetResponse>();
        }
    }
}
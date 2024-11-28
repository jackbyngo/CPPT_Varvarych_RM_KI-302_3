namespace lab3.model;

public class Bus
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public int AmountSeats { get; set; }

    // Додайте цей рядок, якщо його немає
    public ICollection<Trip> Trips { get; set; }
    public IEnumerable<Seat>? Seats { get; set; }
}

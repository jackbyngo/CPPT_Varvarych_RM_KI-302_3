namespace lab4.Entity;

public class Trip
{
    public int Id { get; set; }
    public string TripName { get; set; }
    public int BusId { get; set; }
    public string BusName { get; set; }
    public int Number { get; set; }
    public int AmountSeats { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
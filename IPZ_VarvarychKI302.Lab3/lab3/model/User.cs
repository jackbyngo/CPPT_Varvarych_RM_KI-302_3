namespace lab3.model;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Booking> Bookings { get; set; } = new List<Booking>();
}
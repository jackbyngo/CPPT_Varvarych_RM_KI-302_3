namespace lab3.model;

public class Booking
{
    public int Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public int SeatId { get; set; }
    public Seat Seat { get; set; }
    public DateTime BookingDate { get; set; }
    public bool IsActive { get; set; }
}
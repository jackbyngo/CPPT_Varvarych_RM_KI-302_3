namespace lab3.model;

public class Rating
{
    public int Id { get; set; }
    public double Number { get; set; }
    public int TripId { get; set; }
    public Trip Trip { get; set; }
}
namespace lab4.Entity;

public class TripEntity(string tripName, string tripDescription, string tripTimeFrame, BusEntity? bus)
{
    public string TripName { get; set; } = tripName;
    public string TripDescription { get; set; } = tripDescription;
    public string TripTimeFrame { get; set; } = tripTimeFrame;
    public BusEntity? Bus { get; set; } = bus;

    public TripEntity() : this(string.Empty, string.Empty, string.Empty, null)
    {
    }
}
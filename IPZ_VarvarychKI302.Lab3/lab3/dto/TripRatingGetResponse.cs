namespace lab3.dto;

public class TripRatingGetResponse
{
    public int Id { get; set; }
    public string TripName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string BusName { get; set; }
    public double Rating { get; set; }
}
namespace lab3.dto;

public class TripGetResponse
{
    public int Id { get; set; }
    public string TripName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int BusId { get; set; }
    public string BusName { get; set; }
    public int Number { get; set; }
    public int AmountSeats { get; set; }
}
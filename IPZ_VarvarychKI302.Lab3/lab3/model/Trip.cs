namespace lab3.model;

public class Trip
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int BusId { get; set; }
    public Bus Bus { get; set; }  
}

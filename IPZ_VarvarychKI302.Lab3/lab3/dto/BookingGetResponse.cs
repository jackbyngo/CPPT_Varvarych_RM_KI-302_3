namespace lab3.dto
{
    public class BookingGetResponse
    {
        public int BookingId { get; set; }
        public long UserId { get; set; }
        public int SeatNumber { get; set; }
        public string BusName { get; set; }
        public string TripName { get; set; }
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; } 
    }

}
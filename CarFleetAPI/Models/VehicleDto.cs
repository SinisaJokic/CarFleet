namespace CarFleetAPI.Models
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Model { get; set; }=String.Empty;
        public string RegistrationNumber { get; set; } = String.Empty;
        public int? ProductionYear { get; set; }
        public int? LoadCapacity { get; set; }
        public decimal? CurrentX { get; set; }
        public decimal? CurrentY { get; set; }
        public string? Description { get; set; }
    }
}

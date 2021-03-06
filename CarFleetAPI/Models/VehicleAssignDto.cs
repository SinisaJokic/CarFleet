using CarFleetAPI.Entities;

namespace CarFleetAPI.Models
{
    public class VehicleAssignDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Description { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public string RegistrationNumber { get; set; }

        public string Driver { get; set; }

    }
}

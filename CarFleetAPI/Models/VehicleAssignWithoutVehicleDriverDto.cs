using CarFleetAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarFleetAPI.Models
{
    public class VehicleAssignWithoutVehicleDriverDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FromDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime ToDate { get; set; }
        public string? Description { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }

    }
}

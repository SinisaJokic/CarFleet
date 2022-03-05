using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFleetAPI.Entities
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Model { get; set; }
        [Required]
        [MaxLength(50)]
        public string? RegistrationNumber { get; set; }
        public int? ProductionYear { get; set; }
        public int? LoadCapacity { get; set; }
        public decimal? CurrentX { get; set; }
        public decimal? CurrentY { get; set; }
        public string? Description { get; set; }

        public Vehicle(string model)
        {
            Model = model;
        }
    }
}

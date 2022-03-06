using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFleetAPI.Entities
{
    public class VehicleAssign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Description { get; set; }

        [ForeignKey("VehicleId")]
        //public Vehicle? Vehicle { get; set; }
        public int VehicleId { get; set; }

        [ForeignKey("DriverId")]
        //public Driver? Driver { get; set; }
        public int DriverId { get; set; }

        public VehicleAssign(string name)
        {
            Name = name;
        }


        //public Driver(string firstname, string lastname)
        //{
        //    FirstName = firstname;
        //    LastName = lastname;
        //}
    }
}

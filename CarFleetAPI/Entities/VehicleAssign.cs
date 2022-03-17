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
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FromDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime ToDate { get; set; }
        public string? Description { get; set; }

        [ForeignKey("VehicleId")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        

        [ForeignKey("DriverId")]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        

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

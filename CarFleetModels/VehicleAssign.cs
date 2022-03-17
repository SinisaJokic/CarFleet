using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFleetModels
{
        public class VehicleAssign
    {
            public int Id { get; set; }
            public string Name { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
             public DateTime FromDate { get; set; }
             [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
            public DateTime ToDate { get; set; }
            public string Description { get; set; }
            public int VehicleId { get; set; }
            public int DriverId { get; set; }

            public string? RegistrationNumber { get; set; }
            public string? Driver { get; set; }
    }
    
}

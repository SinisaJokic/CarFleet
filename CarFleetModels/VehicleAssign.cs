using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFleetModels
{
        public class VehicleAssign
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Description { get; set; }
            public int VehicleId { get; set; }
            public int DriverId { get; set; }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFleetModels
{
        public class Vehicle
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public string RegistrationNumber { get; set; }
            public int ProductionYear { get; set; }
            public int LoadCapacity { get; set; }
            public decimal CurrentX { get; set; }
            public decimal CurrentY { get; set; }
            public string Description { get; set; }
        }
}

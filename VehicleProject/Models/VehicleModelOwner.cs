using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleProject.Models
{
    public class VehicleModelOwner
    {
        public int VehicleModel_id { get; set; }

        public int Owner_id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }





    }
}

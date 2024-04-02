using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleProject.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public int MakeId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

    }
}

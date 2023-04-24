using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class Cargo
    {
        public int CargoId { get; set; }
        public string? Name { get; set; }
        // Weight saved in kilograms
        public double  Weight { get; set; }

        // Length, Width, Height saved in centimeters
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public string? Description { get; set; }
    }
}

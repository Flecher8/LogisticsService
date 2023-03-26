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
        public int  Weight { get; set; }

        // Length, Width, Height saved in centimeters
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public string? Description { get; set; }
    }
}

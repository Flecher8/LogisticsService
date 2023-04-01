using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.Dtos
{
    public class CargoDto
    {
        public int CargoId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public double Weight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string WeightMeasureUnit { get; set; } = string.Empty;
        public string SizeMeasureUnit { get; set; } = string.Empty;
    }
}

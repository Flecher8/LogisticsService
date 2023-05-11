using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    public class Cargo
    {
        public int CargoId { get; set; }
        public string Name { get; set; }

        public double Weight { get; set; }

        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public string Description { get; set; }
    }
}

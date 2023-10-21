using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    public class CancelledOrder
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public string CancelledBy { get; set; }
        public int CancelledById { get; set; }
    }
}

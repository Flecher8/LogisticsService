﻿namespace LogisticsService.BLL.Helpers.GoogleMapsApi
{
    public class Geometry
    {
        public Bounds bounds { get; set; }
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }
}

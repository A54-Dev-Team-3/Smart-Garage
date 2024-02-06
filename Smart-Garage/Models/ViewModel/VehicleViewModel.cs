﻿using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public string VIN { get; set; }
        public int CreationYear { get; set; }
        public User? User { get; set; }
        public IList<Visit> Visits { get; set; } = new List<Visit>();
    }
}

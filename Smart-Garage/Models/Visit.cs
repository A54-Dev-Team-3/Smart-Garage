﻿using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public int TotalPrice { get; set; } // in BGN
        public int ServicesTotalPrice { get; set; } // in BGN
        public int PartsTotalPrice { get; set; } // in BGN
        public bool IsDeleted { get; set; }
        public IList<ServiceInstance> ServiceInstances { get; set; } = new List<ServiceInstance>();
    }
}
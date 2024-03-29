﻿using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class CustomerVisitViewModel
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}

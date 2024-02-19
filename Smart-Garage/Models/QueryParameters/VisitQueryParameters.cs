namespace Smart_Garage.Models.QueryParameters
{
    public class VisitQueryParameters
    {
        public string? User { get; set; } // Username
        public string? LicensePlate { get; set; } // Vehicle
        public string? Brand { get; set; } // Vehicle
        public string? Model { get; set; } // Vehicle
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}

namespace Smart_Garage.Models.QueryParameters
{
    public class UserQueryParameters
    {
        // USer
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Vehicle
        public string? LicensePlate { get; set; }
        public string? VIN { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
    }
}

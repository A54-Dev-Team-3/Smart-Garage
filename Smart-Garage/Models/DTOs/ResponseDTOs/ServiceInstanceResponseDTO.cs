namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class ServiceInstanceResponseDTO
    {
        public int PartQuantity { get; set; }
        public double PartUnitPrice { get; set; }
        public PartResponseDTO Part { get; set; }

        public double ServicePrice { get; set; }
        public ServiceReponseDTO Service { get; set; }

        public MechanicResponseDTO Mechanic { get; set; }
    }
}

namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class BrandResponseDTO
    {
        public string Name { get; set; }
        public IList<ModelResponseDTO> Models { get; set; }
    }
}

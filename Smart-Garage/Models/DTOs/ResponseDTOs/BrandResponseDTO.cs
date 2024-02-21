namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class BrandResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ModelResponseDTO> Models { get; set; }
    }
}

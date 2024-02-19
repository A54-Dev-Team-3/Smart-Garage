using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IModelService
    {
        public IList<ModelResponseDTO> GetAll();
    }
}

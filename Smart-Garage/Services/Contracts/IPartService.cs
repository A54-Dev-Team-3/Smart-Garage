using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;

namespace Smart_Garage.Services.Contracts
{
    public interface IPartService
    {
        PartResponseDTO Create(PartRequestDTO newPartDTO, string username);
        IList<PartResponseDTO> GetAll(string username);
        IList<PartResponseDTO> FilterBy(PartQueryParameters filterParameters, string username);
        PartResponseDTO GetById(int id, string username);
        PartResponseDTO Update(int id, PartRequestDTO newPartDTO, string username);
        PartResponseDTO Delete(int id, string username);
        bool PartExists(string name);
    }
}

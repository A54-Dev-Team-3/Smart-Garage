using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;

namespace Smart_Garage.Services.Contracts
{
    public interface IVisitService
    {
        VisitResponseDTO Create(VisitRequestDTO visitRequestDTO);
        IList<VisitResponseDTO> GetAll(string username);
        IList<VisitResponseDTO> FilterBy(VisitQueryParameters filterParameters);
        VisitResponseDTO GetById(int id);
        VisitResponseDTO Update(VisitRequestDTO visitRequestDTO);
        VisitResponseDTO Delete(int id, string username);
        bool VisitExists(int id);
    }
}

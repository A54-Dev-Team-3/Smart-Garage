using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;

namespace Smart_Garage.Services.Contracts
{
    public interface IVisitService
    {
        VisitResponseDTO Create(int VehicleId, string username);
        IList<VisitResponseDTO> GetAll(string username);
        IList<VisitResponseDTO> FilterBy(VisitQueryParameters filterParameters, string username);
        VisitResponseDTO GetById(int id);
        VisitResponseDTO Delete(int id, string username);
        bool VisitExists(int id);
    }
}

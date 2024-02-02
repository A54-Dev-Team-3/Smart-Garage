using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IServiceService
    {
        CreateServiceResponseDTO Create(CreateServiceRequestDTO newService, string username);
        IList<ServiceReponseDTO> GetAll(string username);
        IList<ServiceReponseDTO> FilterBy(ServicesQueryParameters filterParameters, string username);
        ServiceReponseDTO GetById(int id, string username);
        ServiceReponseDTO GetByName(string username);
        UpdateServiceResponseDTO Update(int id, UpdateServiceRequestDTO updatedService, string username);
        DeleteServiceResponseDTO Delete(int id, string username);
        bool ServiceExists(string name, string username);
        int Count(string username);
    }
}

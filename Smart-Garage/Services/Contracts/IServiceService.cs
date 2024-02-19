using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.ViewModel;

namespace Smart_Garage.Services.Contracts
{
    public interface IServiceService
    {
        CreateServiceResponseDTO Create(ServiceRequestDTO newService, string username);
        IList<ServiceReponseDTO> GetAll();
        IList<ServiceReponseDTO> FilterBy(ServicesQueryParameters filterParameters, string username);
        ServiceReponseDTO GetById(int id, string username);
        ServiceReponseDTO GetByName(string username);
        UpdateServiceResponseDTO Update(int id, ServiceRequestDTO updatedService, string username);
        DeleteServiceResponseDTO Delete(int id, string username);
        bool ServiceExists(string name, string username);
        int Count(string username);
    }
}

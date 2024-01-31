using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IServicesService
    {
        CreateServiceResponseDTO Create(CreateServiceRequestDTO newService, string username);
        IList<Service> GetAll(string username);
        Service GetById(int id, string username);
        Service GetByName(string username);
        UpdateServiceResponseDTO Update(int id, UpdateServiceRequestDTO updatedService, string username);
        DeleteServiceResponseDTO Delete(int id, string username);
        IList<Service> FilterBy(ServicesQueryParameters filterParameters, string username);
        bool ServiceExists(string name, string username);
        int Count(string username);
    }
}

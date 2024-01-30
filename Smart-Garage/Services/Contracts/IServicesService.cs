using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IServicesService
    {
        IList<Service> GetAll();
        Service GetById(int id);
        Service GetByName(string username);
        CreateServiceResponseDTO Create(CreateServiceRequestDTO newService);
        UpdateServiceResponseDTO Update(int id, UpdateServiceRequestDTO updatedService, string? username);
        DeleteServiceResponseDTO Delete(int id);
        IList<Service> FilterBy(ServicesQueryParameters filterParameters);
        bool ServiceExists(string name);
        int Count();
    }
}

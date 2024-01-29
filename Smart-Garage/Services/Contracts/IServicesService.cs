using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;

namespace Smart_Garage.Services.Contracts
{
    public interface IServicesService
    {
        IList<Service> GetAll();
        Service GetById(int id);
        Service GetByName(string username);
        Service Create(Service newService, string username);
        Service Update(int id, Service updatedService);
        Service Delete(int id, string username);
        IList<Service> FilterBy(ServicesQueryParameters usersParams);
        bool ServiceExists(string name);
        int Count();
    }
}

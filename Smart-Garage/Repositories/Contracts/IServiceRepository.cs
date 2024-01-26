using Smart_Garage.Models;
using Smart_Garage.Models.QueryParameters;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IServiceRepository
    {
        IList<Service> GetAll();
        Service GetById(int id);
        Service GetByName(string name);
        Service Create(Service newService);
        Service Update(int id, Service updatedService);
        Service Delete(int id);
        IList<Service> FilterBy(ServicesQueryParameters usersParams);
        bool ServiceExists(string name);
        int Count();
    }
}

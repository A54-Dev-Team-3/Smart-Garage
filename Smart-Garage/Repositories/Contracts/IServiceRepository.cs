using Smart_Garage.Models;
using Smart_Garage.Models.QueryParameters;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IServiceRepository
    {
        Service Create(Service newService);
        IList<Service> GetAll();
        IList<Service> FilterBy(ServicesQueryParameters usersParams);
        Service GetById(int id);
        Service GetByName(string name);
        Service Update(int id, Service updatedService);
        Service Delete(int id);
        bool ServiceExists(string name);
        int Count();
    }
}

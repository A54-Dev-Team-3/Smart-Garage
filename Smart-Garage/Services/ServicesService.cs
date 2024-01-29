using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IServiceRepository servicesRepository;
        private readonly IUsersRepository usersRepository;

        public ServicesService(IServiceRepository servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        public IList<Service> GetAll()
        {
            return servicesRepository.GetAll();
        }

        public Service GetById(int id)
        {
            return servicesRepository.GetById(id);
        }

        public Service GetByName(string username)
        {
            return servicesRepository.GetByName(username);
        }

        public Service Create(Service newService, string username)
        {
            User user = usersRepository.GetByName(username);

            if (!user.IsAdmin)
                throw new UnauthorizedOperationException("User is not authorized.");

            if (servicesRepository.ServiceExists(newService.Labour))
                throw new UnauthorizedOperationException($"Service with name {newService.Labour} already exists.");

            return servicesRepository.Create(newService);
        }

        public Service Update(int id, Service updatedService)
        {
            return servicesRepository.Update(id, updatedService);
        }

        public Service Delete(int id, string username)
        {
            User user = usersRepository.GetByName(username);

            if (!user.IsAdmin)
                throw new UnauthorizedOperationException($"User is not authorized.");

            return servicesRepository.Delete(id);
        }

        public IList<Service> FilterBy(ServicesQueryParameters usersParams)
        {
            throw new NotImplementedException();
        }

        public bool ServiceExists(string name)
        {
            return servicesRepository.ServiceExists(name);
        }

        public int Count()
        {
            return servicesRepository.Count();
        }
    }
}

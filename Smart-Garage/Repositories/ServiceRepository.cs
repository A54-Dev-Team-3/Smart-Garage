using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories.Contracts;

namespace Smart_Garage.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly SGContext context;

        public ServiceRepository(SGContext context)
        {
            this.context = context;
        }

        public IList<Service> GetAll()
        {
            return context.Services.ToList();
        }
        public Service GetById(int id)
        {
            return context.Services
                .FirstOrDefault(s => s.Id == id) ??
                throw new EntityNotFoundException($"Service with id:{id} not found.");
        }

        public Service GetByName(string name)
        {
            return context.Services.FirstOrDefault(s => s.Labour == name) ??
               throw new EntityNotFoundException($"Service with name:{name} is not found.");
        }
        public Service Create(Service newService)
        {
            context.Services.Add(newService);
            context.SaveChanges();
            return newService;
        }

        public Service Update(int id, Service updatedService)
        {
            Service newService = context.Services.FirstOrDefault(s => s.Id == id) ??
                throw new EntityNotFoundException($"Service to update with id:{id} not found.");

            // Username should not be able to be updated
            newService.Labour = updatedService.Labour;
            newService.Price = updatedService.Price;

            context.SaveChanges();
            return updatedService;
        }

        public Service Delete(int id)
        {
            Service toDelete = GetById(id);
            context.Remove(toDelete);
            context.SaveChanges();
            return toDelete;
        }
        
        public bool ServiceExists(string name)
        {
            return context.Services.Any(s => s.Labour == name);
        }

        public int Count()
        {
            return context.Services.Count();
        }

        public IList<Service> FilterBy(ServicesQueryParameters usersParams)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}

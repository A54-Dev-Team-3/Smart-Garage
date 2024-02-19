using Microsoft.Data.SqlClient;
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

        public Service Create(Service newService)
        {
            context.Services.Add(newService);
            context.SaveChanges();
            return newService;
        }

        public IList<Service> GetAll()
        {
            return context.Services.Where(s => !s.IsDeleted).ToList();
        }

        public IList<Service> FilterBy(ServicesQueryParameters serviceParams)
        {
            IQueryable<Service> result = context.Services.Where(u => !u.IsDeleted);

            if (!string.IsNullOrEmpty(serviceParams.Name))
            {
                result = result.Where(s => s.Name == serviceParams.Name);
            }

            if (serviceParams.MinPrice.HasValue)
            {
                result = result.Where(s => s.Price >= serviceParams.MinPrice);
            }

            if (serviceParams.MaxPrice.HasValue)
            {
                result = result.Where(s => s.Price <= serviceParams.MaxPrice);
            }

            switch (serviceParams.SortBy)
            {
                case "name":
                    result = result.OrderBy(s => s.Name);
                    break;
                case "price":
                    result = result.OrderBy(s => s.Price);
                    break;
                default:
                    break;
            }

            result = (serviceParams.SortOrder == "desc") ? result.Reverse() : result;

            return result.ToList();
        }

        public Service GetById(int id)
        {
            return context.Services
                .FirstOrDefault(s => s.Id == id && !s.IsDeleted) ??
                throw new EntityNotFoundException($"Service with id:{id} not found.");
        }

        public Service GetByName(string name)
        {
            return context.Services.FirstOrDefault(s => s.Name == name && !s.IsDeleted) ??
               throw new EntityNotFoundException($"Service with name:{name} is not found.");
        }

        public Service Update(int id, Service updatedService)
        {
            Service newService = context.Services.FirstOrDefault(s => s.Id == id && !s.IsDeleted) ??
                throw new EntityNotFoundException($"Service to update with id:{id} not found.");

            newService.Name = updatedService.Name;
            newService.Price = updatedService.Price;

            context.SaveChanges();
            return updatedService;
        }

        public Service Delete(int id)
        {
            Service toDelete = GetById(id);
            toDelete.IsDeleted = true;
            context.SaveChanges();
            return toDelete;
        }
        
        public bool ServiceExists(string name)
        {
            return context.Services.Any(s => s.Name == name);
        }

        public int Count()
        {
            return context.Services.Count();
        }
    }
}

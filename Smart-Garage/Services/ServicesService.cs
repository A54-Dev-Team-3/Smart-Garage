using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IServiceRepository servicesRepository;
        private readonly IUsersService usersService;
        private readonly IMapper autoMapper;

        public ServicesService(IServiceRepository servicesRepository, IUsersService usersService, IMapper autoMapper)
        {
            this.servicesRepository = servicesRepository;
            this.usersService = usersService;
            this.autoMapper = autoMapper;
        }

        public CreateServiceResponseDTO Create(CreateServiceRequestDTO newService, string username)
        {

            if (servicesRepository.ServiceExists(newService.Name))
                throw new DuplicationException($"Service with name {newService.Name} already exists.");

            IsCurrentUserAdmin(username);

            Service service = new Service()
            {
                Name = newService.Name,
                Price = newService.Price,
            };

            return autoMapper.Map<CreateServiceResponseDTO>(servicesRepository.Create(service));
        }

        public IList<Service> GetAll(string username)
        {
            IsCurrentUserAdmin(username);

            return servicesRepository.GetAll();
        }

        public Service GetById(int id, string username)
        {
            IsCurrentUserAdmin(username);
            return servicesRepository.GetById(id);
        }

        public Service GetByName(string username)
        {
            IsCurrentUserAdmin(username);
            return servicesRepository.GetByName(username);
        }

        public UpdateServiceResponseDTO Update(int id, UpdateServiceRequestDTO updatedService, string username)
        {
            IsCurrentUserAdmin(username);

            Service service = new Service()
            {
                Name = updatedService.Name,
                Price = updatedService.Price,
            };

            return autoMapper.Map<UpdateServiceResponseDTO>(servicesRepository.Update(id, service));
        }

        public DeleteServiceResponseDTO Delete(int id, string username)
        {
            IsCurrentUserAdmin(username);

            return autoMapper.Map<DeleteServiceResponseDTO>(servicesRepository.Delete(id));
        }

        public IList<Service> FilterBy(ServicesQueryParameters filterParameters, string username)
        {
            IsCurrentUserAdmin(username);

            return servicesRepository.FilterBy(filterParameters)
                            .Select(u => autoMapper.Map<Service>(u))
                            .ToList();
        }

        public bool ServiceExists(string name, string username)
        {
            IsCurrentUserAdmin(username);

            return servicesRepository.ServiceExists(name);
        }

        public int Count(string username)
        {
            IsCurrentUserAdmin(username);

            return servicesRepository.Count();
        }

        private void IsCurrentUserAdmin(string username)
        {
            if (!usersService.IsCurrentUserAdmin(username))
                throw new UnauthorizedOperationException($"You are not an admin!");
        }
    }
}

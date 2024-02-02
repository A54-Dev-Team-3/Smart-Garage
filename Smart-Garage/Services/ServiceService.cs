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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository servicesRepository;
        private readonly IUserService usersService;
        private readonly IMapper autoMapper;

        public ServiceService(IServiceRepository servicesRepository, IUserService usersService, IMapper autoMapper)
        {
            this.servicesRepository = servicesRepository;
            this.usersService = usersService;
            this.autoMapper = autoMapper;
        }

        public CreateServiceResponseDTO Create(CreateServiceRequestDTO newService, string username)
        {

            if (servicesRepository.ServiceExists(newService.Name))
                throw new DuplicationException($"Service with name {newService.Name} already exists.");

            usersService.IsCurrentUserAdmin(username);

            Service service = new Service()
            {
                Name = newService.Name,
                Price = newService.Price,
            };

            return autoMapper.Map<CreateServiceResponseDTO>(servicesRepository.Create(service));
        }

        public IList<ServiceReponseDTO> GetAll(string username)
        {
            usersService.IsCurrentUserAdmin(username);

            return servicesRepository.GetAll()
                .Select(s => autoMapper.Map<ServiceReponseDTO>(s))
                .ToList();
        }

        public IList<ServiceReponseDTO> FilterBy(ServicesQueryParameters filterParameters, string username)
        {
            usersService.IsCurrentUserAdmin(username);

            return servicesRepository.FilterBy(filterParameters)
                            .Select(f => autoMapper.Map<ServiceReponseDTO>(f))
                            .ToList();
        }

        public ServiceReponseDTO GetById(int id, string username)
        {
            usersService.IsCurrentUserAdmin(username);
            return autoMapper.Map<ServiceReponseDTO>(servicesRepository.GetById(id));
        }

        public ServiceReponseDTO GetByName(string username)
        {
            usersService.IsCurrentUserAdmin(username);
            return autoMapper.Map<ServiceReponseDTO>(servicesRepository.GetByName(username));
        }

        public UpdateServiceResponseDTO Update(int id, UpdateServiceRequestDTO updatedService, string username)
        {
            usersService.IsCurrentUserAdmin(username);

            Service service = new Service()
            {
                Name = updatedService.Name,
                Price = updatedService.Price
            };

            return autoMapper.Map<UpdateServiceResponseDTO>(servicesRepository.Update(id, service));
        }

        public DeleteServiceResponseDTO Delete(int id, string username)
        {
            usersService.IsCurrentUserAdmin(username);

            return autoMapper.Map<DeleteServiceResponseDTO>(servicesRepository.Delete(id));
        }

        public bool ServiceExists(string name, string username)
        {
            usersService.IsCurrentUserAdmin(username);

            return servicesRepository.ServiceExists(name);
        }

        public int Count(string username)
        {
            usersService.IsCurrentUserAdmin(username);

            return servicesRepository.Count();
        }

    }
}

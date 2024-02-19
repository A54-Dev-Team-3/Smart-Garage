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
        private readonly IUserService userService;
        private readonly IMapper autoMapper;

        public ServiceService(IServiceRepository servicesRepository, IUserService usersService, IMapper autoMapper)
        {
            this.servicesRepository = servicesRepository;
            this.userService = usersService;
            this.autoMapper = autoMapper;
        }

        public CreateServiceResponseDTO Create(ServiceRequestDTO newService, string username)
        {

            if (servicesRepository.ServiceExists(newService.Name))
                throw new DuplicationException($"Service with name {newService.Name} already exists.");

            userService.IsCurrentUserAdmin(username);

            Service service = new Service()
            {
                Name = newService.Name,
                Price = newService.Price,
            };

            return autoMapper.Map<CreateServiceResponseDTO>(servicesRepository.Create(service));
        }

        public IList<ServiceReponseDTO> GetAll()
        {
            //userService.IsCurrentUserAdmin(username);

            return servicesRepository.GetAll()
                .Select(s => autoMapper.Map<ServiceReponseDTO>(s))
                .ToList();
        }

        public IList<ServiceReponseDTO> FilterBy(ServicesQueryParameters filterParameters, string username)
        {
            userService.IsCurrentUserAdmin(username);

            return servicesRepository.FilterBy(filterParameters)
                            .Select(f => autoMapper.Map<ServiceReponseDTO>(f))
                            .ToList();
        }

        public ServiceReponseDTO GetById(int id, string username)
        {
            userService.IsCurrentUserAdmin(username);
            return autoMapper.Map<ServiceReponseDTO>(servicesRepository.GetById(id));
        }

        public ServiceReponseDTO GetByName(string name)
        {
            //userService.IsCurrentUserAdmin(username);
            return autoMapper.Map<ServiceReponseDTO>(servicesRepository.GetByName(name));
        }

        public UpdateServiceResponseDTO Update(int id, ServiceRequestDTO updatedService, string username)
        {
            userService.IsCurrentUserAdmin(username);

            Service service = new Service()
            {
                Name = updatedService.Name,
                Price = updatedService.Price
            };

            return autoMapper.Map<UpdateServiceResponseDTO>(servicesRepository.Update(id, service));
        }

        public DeleteServiceResponseDTO Delete(int id, string username)
        {
            userService.IsCurrentUserAdmin(username);

            return autoMapper.Map<DeleteServiceResponseDTO>(servicesRepository.Delete(id));
        }

        public bool ServiceExists(string name, string username)
        {
            userService.IsCurrentUserAdmin(username);

            return servicesRepository.ServiceExists(name);
        }

        public int Count(string username)
        {
            userService.IsCurrentUserAdmin(username);

            return servicesRepository.Count();
        }

    }
}

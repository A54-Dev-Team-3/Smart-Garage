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
        private readonly IUsersRepository usersRepository;
        private readonly IMapper autoMapper;

        public ServicesService(IServiceRepository servicesRepository, IUsersRepository usersRepository, IMapper autoMapper)
        {
            this.servicesRepository = servicesRepository;
            this.usersRepository = usersRepository;
            this.autoMapper = autoMapper;
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

        public CreateServiceResponseDTO Create(CreateServiceRequestDTO newService)
        {

            if (servicesRepository.ServiceExists(newService.Name))
                throw new DuplicationException($"Service with name {newService.Name} already exists.");

            Service service = new Service()
            {
                Name = newService.Name,
                Price = newService.Price,
                VehicleId = newService.VehicleId
            };

            return autoMapper.Map<CreateServiceResponseDTO>(servicesRepository.Create(service));
        }

        public UpdateServiceResponseDTO Update(int id, UpdateServiceRequestDTO updatedService, string? username)
        {
            if (username?.Length == 5)
            {
                throw new NotImplementedException("");
            }

            Service service = new Service()
            {
                Name = updatedService.Name,
                Price = updatedService.Price,
                VehicleId = updatedService.VehicleId
            };

            return autoMapper.Map<UpdateServiceResponseDTO>(servicesRepository.Update(id, service));
        }

        public DeleteServiceResponseDTO Delete(int id)
        {
            return autoMapper.Map<DeleteServiceResponseDTO>(servicesRepository.Delete(id));
        }

        public IList<Service> FilterBy(ServicesQueryParameters filterParameters)
        {
            return servicesRepository.FilterBy(filterParameters)
                            .Select(u => autoMapper.Map<Service>(u))
                            .ToList();
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

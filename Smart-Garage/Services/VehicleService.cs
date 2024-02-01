using Smart_Garage.Models;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Services.Contracts;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Exceptions;
using Smart_Garage.Models.DTOs;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Helpers.Contracts;
using AutoMapper;

namespace Smart_Garage.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IMapper mapper;
        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            this.vehicleRepository = vehicleRepository;
            this.mapper = mapper;
        }

        public Vehicle Create(UserRequestDTO dto, Vehicle vehicle)
        {
            User user = mapper.Map<User>(dto);
            return this.vehicleRepository.Create(user, vehicle);
        }

        public bool Delete(int id)
        {
            return this.vehicleRepository.Delete(id);
        }

        public IList<Vehicle> FilterBy(VehicleQueryParameters vehicleQueryParameters)
        {
            return this.vehicleRepository.FilterBy(vehicleQueryParameters);
        }

        public IList<Vehicle> GetAll()
        {
            return this.vehicleRepository.GetAll();
        }

        public Vehicle GetById(int id)
        {
            return this.vehicleRepository.GetById(id);
        }

        public IList<Vehicle> SearchBy(string filter)
        {
            return this.vehicleRepository.SearchBy(filter);
        }

        public List<Vehicle> SearchByPhoneNumber(User user, string phoneNumber)
        {
            if(!user.IsAdmin)
            {
                throw new UnauthorizedOperationException("You are not an admin!");
            }
            return this.vehicleRepository.SearchByPhoneNumber(phoneNumber);
        }

        public Vehicle Update(int vehicleId, Vehicle updatedVehicle)
        {
            return this.vehicleRepository.Update(vehicleId, updatedVehicle);
        }
    }
}

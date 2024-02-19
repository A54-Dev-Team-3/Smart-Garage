using Smart_Garage.Models;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Services.Contracts;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Exceptions;
using Smart_Garage.Models.DTOs;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Helpers.Contracts;
using AutoMapper;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Helpers;
using Smart_Garage.Models.ViewModel;

namespace Smart_Garage.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper autoMapper;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper autoMapper, IUserRepository userRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.autoMapper = autoMapper;
            this.userRepository = userRepository;
        }

        public VehicleResponseDTO Create(string username, VehicleRequestDTO dto)
        {
            User user = userRepository.GetByName(username);
            if (!user.IsAdmin)
            {
                throw new UnauthorizedOperationException("You are not an admin!");
            }

			Vehicle vehicle = new Vehicle()
			{
				LicensePlate = dto.LicensePlate,
				VIN = dto.VIN,
				CreationYear = dto.CreationYear,
				Model = autoMapper.Map<Model>(dto.Model)
			};

			Vehicle createdVehicle = this.vehicleRepository.Create(user, vehicle);

            VehicleResponseDTO dtoToReturn = new VehicleResponseDTO()
			{
				LicensePlate = createdVehicle.LicensePlate,
				VIN = createdVehicle.VIN,
				CreationYear = createdVehicle.CreationYear,
				Model = autoMapper.Map<string>(createdVehicle.Model)
			};

            return dtoToReturn;
		}
        public IList<VehicleResponseDTO> GetAll()
        {
            IList<Vehicle> vehicles = this.vehicleRepository.GetAll();

            return vehicles
                .Select(v => autoMapper.Map<VehicleResponseDTO>(v))
                .ToList();
        }

        public VehicleResponseDTO GetById(int id)
        {

            Vehicle vehicle = this.vehicleRepository.GetById(id);
            return autoMapper.Map<VehicleResponseDTO>(vehicle);
        }

        public IList<string> GetLicensePlateByUser(string username)
        {
            return vehicleRepository.GetLicensePlateByUser(username);
        }

        public VehicleResponseDTO Update(int vehicleId, VehicleRequestDTO dto)
        {
            Vehicle updatedVehicle = this.vehicleRepository.Update(vehicleId, autoMapper.Map<Vehicle>(dto));
            return autoMapper.Map<VehicleResponseDTO>(updatedVehicle);

        }

        public bool Delete(int id)
        {
            return this.vehicleRepository.Delete(id);
        }

        public IList<VehicleResponseDTO> FilterBy(VehicleQueryParameters vehicleQueryParameters)
        {
            return this.vehicleRepository.FilterBy(vehicleQueryParameters)
                .Select(v => autoMapper.Map<VehicleResponseDTO>(v))
                .ToList();
        }

        public VehicleResponseDTO FilterByLicensePlate(string licensePlate)
        {
            var vehicleResponseDTO = autoMapper.Map<VehicleResponseDTO>(vehicleRepository.FilterByLicensePlate(licensePlate));
            return vehicleResponseDTO;
        }


        public IList<Vehicle> SearchBy(string filter)
        {
            return this.vehicleRepository.SearchBy(filter);
        }

        public List<Vehicle> SearchByPhoneNumber(User user, string phoneNumber)
        {
            if (!user.IsAdmin)
            {
                throw new UnauthorizedOperationException("You are not an admin!");
            }
            return this.vehicleRepository.SearchByPhoneNumber(phoneNumber);
        }

    }
}

using Microsoft.Extensions.Hosting;
using Smart_Garage.Helpers.Contracts;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Helpers
{
    public class ModelMapper : IModelMapper
    {
        public User Map(SignUpUserRequestDTO dto)
        {
            return new User
            {
                Username = dto.Username,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
        }

        public Vehicle Map(VehicleRequestDTO dto, User user)
        {
            return new Vehicle
            {
                LicensePlate = dto.LicensePlate,
                VIN = dto.VIN,
                CreationYear = dto.CreationYear,
                Model = dto.Model,
                Brand = dto.Brand,
                User = user
            };
        }

        public VehicleResponseDTO Map(UserRequestDTO user, Vehicle vehicleModel)
        {
            return new VehicleResponseDTO()
            {
                LicencePlate = vehicleModel.LicensePlate,
                VIN = vehicleModel.VIN,
                CreationYear = vehicleModel.CreationYear,
                Model = vehicleModel.Model,
                Brand = vehicleModel.Brand,
                User = user.Username,
                Services = vehicleModel.Services.ToList()
            };
        }
    }
}
using Smart_Garage.Helpers.Contracts;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;

namespace Smart_Garage.Helpers
{
    public class ModelMapper : IModelMapper
    {
        public User Map(SignUpUserDTO dto)
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
    }
}

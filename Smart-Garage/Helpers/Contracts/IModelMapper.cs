using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;


namespace Smart_Garage.Helpers.Contracts
{
    public interface IModelMapper
    {
        User Map(SignUpUserRequestDTO dto);
        //Vehicle Map(VehicleRequestDTO dto, User user);
        //public VehicleResponseDTO Map(UserRequestDTO user, Vehicle vehicleModel);
    }
}

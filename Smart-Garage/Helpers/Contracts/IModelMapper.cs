using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;

namespace Smart_Garage.Helpers.Contracts
{
    public interface IModelMapper
    {
        User Map(SignUpUserRequestDTO dto);
    }
}

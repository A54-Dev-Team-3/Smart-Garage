using Smart_Garage.Models;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Models.DTOs;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IVehicleService
    {
        VehicleResponseDTO Create(string username, VehicleRequestDTO dto);
        public IList<VehicleResponseDTO> GetAll();
        public VehicleResponseDTO GetById(int id);
        public VehicleResponseDTO Update(int vehicleId, VehicleRequestDTO dto);
        bool Delete(int id);
        public IList<Vehicle> SearchBy(string filter);
        IList<VehicleResponseDTO> FilterBy(VehicleQueryParameters vehicleQueryParameters);
        public List<Vehicle> SearchByPhoneNumber(User user, string phoneNumber);
    }
}

using Smart_Garage.Models;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Models.DTOs;
using Smart_Garage.Models.DTOs.RequestDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IVehicleService
    {
        Vehicle Create(UserRequestDTO user, Vehicle vehicle);
        IList<Vehicle> GetAll();
        Vehicle GetById(int id);
        Vehicle Update(int vehicleId, Vehicle updatedVehicle);
        bool Delete(int id);
        public IList<Vehicle> SearchBy(string filter);
        IList<Vehicle> FilterBy(VehicleQueryParameters vehicleQueryParameters);
        public List<Vehicle> SearchByPhoneNumber(User user, string phoneNumber);
    }
}

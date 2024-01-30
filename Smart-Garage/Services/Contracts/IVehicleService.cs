using Smart_Garage.Models;
using Smart_Garage.Repositories.QueryParameters;

namespace Smart_Garage.Services.Contracts
{
    public interface IVehicleService
    {
        Vehicle Create(User user, Vehicle vehicle);
        IList<Vehicle> GetAll();
        Vehicle GetById(int id);
        Vehicle Update(int vehicleId, Vehicle updatedVehicle);
        bool Delete(int id);
        public IList<Vehicle> SearchBy(string filter);
        IList<Vehicle> FilterBy(VehicleQueryParameters vehicleQueryParameters);
        public List<Vehicle> SearchByPhoneNumber(User user, string phoneNumber);
    }
}

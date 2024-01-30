using Microsoft.EntityFrameworkCore;
using Smart_Garage.Models;
using Smart_Garage.Repositories.QueryParameters;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IVehicleRepository
    {
        Vehicle Create(User user, Vehicle vehicle);
        IList<Vehicle> GetAll();
        Vehicle GetById(int id);
        Vehicle Update(User user, int vehicleId, Vehicle updatedVehicle);
        bool Delete(int id);
        public IList<Vehicle> SearchBy(string filter);
        IList<Vehicle> FilterBy(VehicleQueryParameters vehicleQueryParameters);
    }
}

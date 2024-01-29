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
        Vehicle GetByLP(string licensePlate);
        Vehicle GetByVIN(string VIN);
        Vehicle FilterBy(VehicleQueryParameters vehicleQueryParameters);
        List<Vehicle> SearchByPhoneNumber(string phoneNumber);
    }
}

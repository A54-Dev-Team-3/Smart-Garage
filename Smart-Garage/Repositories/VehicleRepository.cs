using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Repositories.QueryParameters;

namespace Smart_Garage.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly SGContext context;
        private string NotFoundMessage = "Vehicle with {0}:{1} doesn't exists.";
        public VehicleRepository( SGContext context )
        {
            this.context = context;
        }

        public Vehicle Create(User user, Vehicle vehicle)
        {
            vehicle.User = user;
            vehicle.CreationYear = DateTime.Now.Year;
            context.Vehicles.Add(vehicle);
            vehicle.User.Vehicles.Add(vehicle);
            context.SaveChanges();

            return vehicle;
        }

        public bool Delete(int id)
        {
            Vehicle vehicle = GetById(id);
            vehicle.IsDeleted = true;

            return context.SaveChanges() > 0;
        }

        public Vehicle FilterBy(VehicleQueryParameters vehicleQueryParameters)
        {
            throw new NotImplementedException();
        }

        public IList<Vehicle> GetAll()
        {
            return context.Vehicles.Where(v => !v.IsDeleted).ToList();
        }

        public Vehicle GetById(int id)
        {
            return context.Vehicles.FirstOrDefault(v => v.Id == id && !v.IsDeleted) ??
                throw new EntityNotFoundException(string.Format(NotFoundMessage,"id", id));
        }

        public Vehicle GetByLP(string licensePlate)
        {
            return context.Vehicles.FirstOrDefault(v => v.LicensePlate == licensePlate && !v.IsDeleted) ??
                throw new EntityNotFoundException(string.Format(NotFoundMessage, "licensePlate", licensePlate));
        }

        public Vehicle GetByVIN(string VIN)
        {
            return context.Vehicles.FirstOrDefault(v => v.VIN == VIN && !v.IsDeleted) ??
                throw new EntityNotFoundException(string.Format(NotFoundMessage, "VIN", VIN));
        }

        public List<Vehicle> SearchByPhoneNumber(string phoneNumber)
        {
            return context.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber && !u.IsDeleted).Vehicles.ToList() ??
                throw new EntityNotFoundException($"User with phone number: {phoneNumber} doesn't exists");
        }

        public Vehicle Update(User user, int vehicleId, Vehicle updatedVehicle)
        {
            Vehicle vehicleToUpdate = GetById(vehicleId);

            vehicleToUpdate.LicensePlate = updatedVehicle.LicensePlate;
            
            context.Update(vehicleToUpdate);
            context.SaveChanges();

            return vehicleToUpdate;
        }
    }
}

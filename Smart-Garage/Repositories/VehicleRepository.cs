using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        public VehicleRepository(SGContext context)
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

        public IList<Vehicle> FilterBy(VehicleQueryParameters filterParameters)
        {
            IList<Vehicle> vehicles = GetAll();

            vehicles = FilterByModel(vehicles, filterParameters.Model);
            vehicles = FilterByBrand(vehicles, filterParameters.Brand);
            vehicles = FilterByYearOfCreation(vehicles, filterParameters.YearOfCreation);
            vehicles = SortBy(vehicles, filterParameters.SortBy);

            return vehicles.ToList();
        }

        public IList<Vehicle> GetAll()
        {
            return context.Vehicles.Where(v => !v.IsDeleted).ToList();
        }

        public Vehicle GetById(int id)
        {
            return context.Vehicles.FirstOrDefault(v => v.Id == id && !v.IsDeleted) ??
                throw new EntityNotFoundException(string.Format(NotFoundMessage, "id", id));
        }

        //public Vehicle GetByLP(string licensePlate)
        //{
        //    return context.Vehicles.FirstOrDefault(v => v.LicensePlate == licensePlate && !v.IsDeleted) ??
        //        throw new EntityNotFoundException(string.Format(NotFoundMessage, "licensePlate", licensePlate));
        //}

        //public Vehicle GetByVIN(string VIN)
        //{
        //    return context.Vehicles.FirstOrDefault(v => v.VIN == VIN && !v.IsDeleted) ??
        //        throw new EntityNotFoundException(string.Format(NotFoundMessage, "VIN", VIN));
        //}

        //public List<Vehicle> SearchByPhoneNumber(string phoneNumber)
        //{
        //    return context.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber && !u.IsDeleted).Vehicles.ToList() ??
        //        throw new EntityNotFoundException($"User with phone number: {phoneNumber} doesn't exists");
        //}
        public IList<Vehicle> SearchBy(string filter)
        {
            var vehicles = context.Vehicles
            .Where(v => v.LicensePlate.Contains(filter) ||
                        v.VIN.Contains(filter))
            .Include(v => v.Model)
            .Include(v => v.Brand)
            .ToList();

            return vehicles;
        }
        public Vehicle Update(User user, int vehicleId, Vehicle updatedVehicle)
        {
            Vehicle vehicleToUpdate = GetById(vehicleId);

            vehicleToUpdate.LicensePlate = updatedVehicle.LicensePlate;

            context.Update(vehicleToUpdate);
            context.SaveChanges();

            return vehicleToUpdate;
        }

        private IList<Vehicle> FilterByModel(IList<Vehicle> vehicles, string model)
        {
            if (!string.IsNullOrEmpty(model))
            {
                return vehicles.Where(v => v.Model.Contains(model)).ToList();
            }

            return vehicles;
        }

        private IList<Vehicle> FilterByBrand(IList<Vehicle> vehicles, string brand)
        {
            if (!string.IsNullOrEmpty(brand))
            {
                return vehicles.Where(v => v.Brand.Contains(brand)).ToList();
            }

            return vehicles;
        }

        private IList<Vehicle> FilterByYearOfCreation(IList<Vehicle> vehicles, string year)
        {
            if (!string.IsNullOrEmpty(year))
            {
                int _year = int.Parse(year);
                return vehicles.Where(v => v.CreationYear.Equals(_year)).ToList();
            }

            return vehicles;
        }

        private IList<Vehicle> SortBy(IList<Vehicle> vehicles, string criteria)
        {
            switch (criteria)
            {
                case "model":
                    return vehicles.OrderBy(v => v.Model).ToList();
                case "brand":
                    return vehicles.OrderBy(v => v.Brand).ToList();
                case "year":
                    return vehicles.OrderBy(v => v.CreationYear).ToList();
                default:
                    return vehicles;
            }
        }
    }
}

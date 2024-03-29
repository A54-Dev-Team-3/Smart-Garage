﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Models.DTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly SGContext context;
        public VehicleRepository(SGContext context)
        {
            this.context = context;
        }

        public Vehicle Create(User user, Vehicle vehicle)
        {
            if (context.Vehicles.Any(v => v.LicensePlate == vehicle.LicensePlate && !v.IsDeleted))
            {
                throw new DuplicationException($"Vehicle with license plate: {vehicle.LicensePlate} already exists!");
            }

            if (context.Vehicles.Any(v => v.VIN == vehicle.VIN && !v.IsDeleted))
            {
                throw new DuplicationException($"Vehicle with VIN: {vehicle.VIN} already exists!");
            }

            vehicle.UserId = user.Id;

            if (context.Models.FirstOrDefault(m => m.Name == vehicle.Model.Name) == null)
            {
                context.Models.Add(vehicle.Model);
            }
            else
            {
                vehicle.Model.Id = context.Models.FirstOrDefault(m => m.Name == vehicle.Model.Name).Id;
			}

            if (context.Brands.FirstOrDefault(b => b.Name == vehicle.Model.Brand.Name) == null)
            {
                context.Brands.Add(vehicle.Model.Brand);
            }
			else
			{
				vehicle.Model.Brand.Id = context.Models.FirstOrDefault(b => b.Name == vehicle.Model.Brand.Name).Id;
			}

			context.Vehicles.Add(vehicle);
            vehicle.User.Vehicles.Add(vehicle);

            context.SaveChanges();

            return vehicle;
        }
        public IList<Vehicle> GetAll()
        {
            return context.Vehicles
                .Where(v => !v.IsDeleted)
                .Include(v => v.User)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Brand)
                .ToList();
        }

        public Vehicle GetById(int id)
        {
            return context.Vehicles
                .Include(v => v.User)
                            .Include(v => v.Model)
                                .ThenInclude(m => m.Brand)
                                .FirstOrDefault(v => v.Id == id && !v.IsDeleted) ??
                throw new EntityNotFoundException($"Vehicle with id: {id} doesn't exists.");
        }

        public IList<string> GetLicensePlateByUser(string username)
        {
            return context.Vehicles
                .Where(v => !v.IsDeleted && v.User.Username == username)
                .Select(v => v.LicensePlate)
                .ToList();
        }

        public Vehicle Update(int vehicleId, Vehicle updatedVehicle)
        {
            Vehicle vehicleToUpdate = GetById(vehicleId);

            vehicleToUpdate.LicensePlate = updatedVehicle.LicensePlate;
            vehicleToUpdate.Model = updatedVehicle.Model;
            vehicleToUpdate.Model.Brand = updatedVehicle.Model.Brand;
            vehicleToUpdate.CreationYear = updatedVehicle.CreationYear;
            vehicleToUpdate.VIN = updatedVehicle.VIN;
            vehicleToUpdate.User = updatedVehicle.User;

            context.Update(vehicleToUpdate);
            context.SaveChanges();

            return vehicleToUpdate;
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
            vehicles = FilterByLicensePlateQ(vehicles, filterParameters.LicensePlate);
            vehicles = FilterByVIN(vehicles, filterParameters.VIN);
            vehicles = SortBy(vehicles, filterParameters.SortBy);

            return vehicles.ToList();
        }

        public List<Vehicle> SearchByPhoneNumber(string phoneNumber)
        {
            return GetAll().Where(v => v.User.PhoneNumber == phoneNumber).ToList() ??
            throw new EntityNotFoundException($"User with phone number: {phoneNumber} doesn't exists");
        }
        public IList<Vehicle> SearchBy(string filter)
        {
            var vehicles = context.Vehicles
            .Where(v => v.LicensePlate.Contains(filter) ||
                        v.VIN.Contains(filter))
            .Include(v => v.Model)
            .Include(v => v.Model.Brand)
            .ToList();

            return vehicles;
        }

        public Vehicle FilterByLicensePlate(string licensePlate)
        {
            var vehicles = context.Vehicles
                .Where(v => !v.IsDeleted)
                .Include(v => v.User)
                .Include(v => v.Model)
                .ThenInclude(m => m.Brand)
                .ToList();

            if (!string.IsNullOrEmpty(licensePlate))
                return vehicles.FirstOrDefault(v => v.LicensePlate == licensePlate);
            else
                throw new EntityNotFoundException($"Car with license plate \"{licensePlate}\" not found");
        }

        private IList<Vehicle> FilterByLicensePlateQ(IList<Vehicle> vehicles, string model)
        {
            if (!string.IsNullOrEmpty(model))
            {
                return vehicles.Where(v => v.LicensePlate.Contains(model)).ToList();
            }

            return vehicles;
        }

        private IList<Vehicle> FilterByOwner(IList<Vehicle> vehicles, string model)
        {
            if (!string.IsNullOrEmpty(model))
            {
                return vehicles.Where(v => v.User.Username.Contains(model)).ToList();
            }

            return vehicles;
        }

        private IList<Vehicle> FilterByVIN(IList<Vehicle> vehicles, string model)
        {
            if (!string.IsNullOrEmpty(model))
            {
                return vehicles.Where(v => v.VIN.Contains(model)).ToList();
            }

            return vehicles;
        }

        private IList<Vehicle> FilterByModel(IList<Vehicle> vehicles, string model)
        {
            if (!string.IsNullOrEmpty(model))
            {
                return vehicles.Where(v => v.Model.Name.Contains(model)).ToList();
            }

            return vehicles;
        }

        private IList<Vehicle> FilterByBrand(IList<Vehicle> vehicles, string brand)
        {
            if (!string.IsNullOrEmpty(brand))
            {
                return vehicles.Where(v => v.Model.Brand.Name.Contains(brand)).ToList();
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
                    return vehicles.OrderBy(v => v.Model.Brand).ToList();
                case "year":
                    return vehicles.OrderBy(v => v.CreationYear).ToList();
                default:
                    return vehicles;
            }
        }
    }
}

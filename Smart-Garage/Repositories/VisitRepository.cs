using Microsoft.EntityFrameworkCore;
using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories.Contracts;
using System.Globalization;

namespace Smart_Garage.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        private readonly SGContext context;

        public VisitRepository(SGContext context)
        {
            this.context = context;
        }

        public Visit Create(Visit newVisit)
        {
            context.Visits.Add(newVisit);
            context.SaveChanges();
            return newVisit;
        }

        public IList<Visit> GetAll()
        {
            return context.Visits
                .Where(v => !v.IsDeleted)
                .Include(v => v.Vehicle)
                    .ThenInclude(v => v.User)
                .Include(v => v.Vehicle)
                    .ThenInclude(v => v.Model)
                        .ThenInclude(m => m.Brand)
                .ToList();
        }

        public IList<Visit> FilterBy(VisitQueryParameters visitsParams)
        {
            IQueryable<Visit> result = context.Visits
                .Where(v => !v.IsDeleted)
                .Include(v => v.Vehicle.User)
                .Include(v => v.Vehicle)
                    .ThenInclude(v => v.Model)
                        .ThenInclude(m => m.Brand);

            if (!string.IsNullOrEmpty(visitsParams.User))
            {
                result = result.Where(v => v.Vehicle.User.Username.Contains(visitsParams.User));
            }

            if (!string.IsNullOrEmpty(visitsParams.LicensePlate))
            {
                result = result.Where(v => v.Vehicle.LicensePlate.Contains(visitsParams.LicensePlate));
            }

            if (!string.IsNullOrEmpty(visitsParams.Model))
            {
                result = result.Where(v => v.Vehicle.Model.Name.Contains(visitsParams.Model));
            }

            if (!string.IsNullOrEmpty(visitsParams.Brand))
            {
                result = result.Where(v => v.Vehicle.Model.Brand.Name.Contains(visitsParams.Brand));
            }


            if (!string.IsNullOrEmpty(visitsParams.StartDate))
            {
                DateTime startDate;
                if (DateTime.TryParseExact(visitsParams.StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                    result = result.Where(v => v.Date >= startDate);
                else
                {
                    // Handle invalid date format or value
                    // You may throw an exception, log the error, or take other appropriate action
                }
            }

            if (!string.IsNullOrEmpty(visitsParams.EndDate))
            {
                DateTime endDate;
                if (DateTime.TryParseExact(visitsParams.EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                {
                    endDate = endDate.AddDays(1);
                    result = result.Where(v => v.Date < endDate);
                }
                else
                {
                    // Handle invalid date format or value
                    // You may throw an exception, log the error, or take other appropriate action
                }
            }

            return result.ToList();
        }

        public Visit GetById(int id)
        {
            return context.Visits
                .Include(v => v.Vehicle.User)
                .Include(v => v.ServiceInstances.Where(si => !si.IsDeleted)) // where to put .Where(si => !si.IsDeleted)
                    .ThenInclude(si => si.Mechanic)
                .Include(v => v.ServiceInstances.Where(si => !si.IsDeleted))
                            .ThenInclude(sis => sis.Service)
                .Include(v => v.ServiceInstances.Where(si => !si.IsDeleted))
                            .ThenInclude(sip => sip.Part)
                .Include(v => v.Vehicle)
                    .ThenInclude(v => v.Model)
                        .ThenInclude(m => m.Brand)
                .FirstOrDefault(v => v.Id == id && !v.IsDeleted) ??
                throw new EntityNotFoundException($"Visit with id:{id} not found.");
        }

        public Visit Update(Visit visitToUpdate)
        {
            var visit = GetById(visitToUpdate.Id);


            foreach (var serviceInstance in visit.ServiceInstances)
            {
                serviceInstance.IsDeleted = true;
            }

            visit.PartsTotalPrice = visitToUpdate.PartsTotalPrice;
            visit.ServicesTotalPrice = visitToUpdate.ServicesTotalPrice;
            visit.TotalPrice = visitToUpdate.TotalPrice;

            foreach (var serviceInstance in visitToUpdate.ServiceInstances)
            {

                var newServiceInstance = new ServiceInstance
                {
                    MechanicId = serviceInstance.MechanicId,
                    ServiceId = serviceInstance.ServiceId,
                    PartId = serviceInstance.PartId,
                    PartQuantity = serviceInstance.PartQuantity,
                    PartUnitPrice = serviceInstance.PartUnitPrice,
                    ServicePrice = serviceInstance.ServicePrice
                };

                visit.ServiceInstances.Add(newServiceInstance);
            }

            context.SaveChanges();
            return visit;
        }

        public Visit Delete(int id)
        {
            var toDelete = GetById(id);
            toDelete.IsDeleted = true;
            context.SaveChanges();
            return toDelete;
        }

        public bool VisitExists(int id)
        {
            return context.Visits.Any(v => v.Id == id);
        }
    }
}

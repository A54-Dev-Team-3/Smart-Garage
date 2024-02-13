using Microsoft.EntityFrameworkCore;
using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories.Contracts;

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
                    .ThenInclude(v => v.Model)
                        .ThenInclude(m => m.Brand)
                .ToList();
        }

        public IList<Visit> FilterBy(VisitQueryParameters visitsParams)
        {
            IQueryable<Visit> result = context.Visits
                .Where(v => !v.IsDeleted)
                .Include(v => v.Vehicle)
                    .ThenInclude(v => v.Model)
                        .ThenInclude(m => m.Brand);

            if (!string.IsNullOrEmpty(visitsParams.User))
            {
                result = result.Where(v => v.Vehicle.User.Username == visitsParams.User);
            }

            return result.ToList();
        }

        public Visit GetById(int id)
        {
            return context.Visits
                .Include(v => v.Vehicle.User)
                .Include(v => v.Vehicle)
                    .ThenInclude(v => v.Model)
                        .ThenInclude(m => m.Brand)
                .FirstOrDefault(v => v.Id == id && !v.IsDeleted) ??
                throw new EntityNotFoundException($"Visit with id:{id} not found.");
        }

        public Visit Delete(int id)
        {
            Visit toDelete = GetById(id);
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

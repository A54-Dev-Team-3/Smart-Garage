using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Repositories.Contracts;
using System.Xml.Linq;

namespace Smart_Garage.Repositories
{
    public class MechanicRepository : IMechanicRepository
    {
        private readonly SGContext context;

        public MechanicRepository(SGContext context)
        {
            this.context = context;
        }

        public Mechanic Create(Mechanic newMechanic)
        {
            context.Mechanics.Add(newMechanic);
            context.SaveChanges();
            return newMechanic;
        }

        public IList<Mechanic> GetAll()
        {
            return context.Mechanics.ToList();
        }

        public Mechanic GetById(int id)
        {
            return context.Mechanics
                .FirstOrDefault(m => m.Id == id && !m.IsDeleted) ??
                throw new EntityNotFoundException($"Mechanic with id:{id} not found.");
        }

        public Mechanic GetByName(string name)
        {
            return context.Mechanics.FirstOrDefault(m => m.FirstName + " " + m.LastName == name && !m.IsDeleted) ??
               throw new EntityNotFoundException($"Mechanic with name:{name} is not found.");
        }

        public Mechanic Delete(int id)
        {
            Mechanic toDelete = GetById(id);
            toDelete.IsDeleted = true;
            context.SaveChanges();
            return toDelete;
        }

        public bool VisitExists(string name)
        {
            return context.Mechanics.Any(m => m.FirstName + " " + m.LastName == name);
        }
    }
}

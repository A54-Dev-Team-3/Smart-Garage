using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories.Contracts;
using System.Xml.Linq;

namespace Smart_Garage.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly SGContext context;

        public PartRepository(SGContext context)
        {
            this.context = context;
        }

        public Part Create(Part newPartDTO)
        {
            context.Parts.Add(newPartDTO);
            context.SaveChanges();
            return newPartDTO;
        }

        public IList<Part> GetAll()
        {
            return context.Parts.ToList();
        }

        public IList<Part> FilterBy(PartQueryParameters filterParameters)
        {
            throw new NotImplementedException();
        }

        public Part GetById(int id)
        {
            return context.Parts
                .FirstOrDefault(p => p.Id == id && !p.IsDeleted) ??
                throw new EntityNotFoundException($"Part with id:{id} not found.");
        }

        public Part GetByName(string name)
        {
            return context.Parts
                .FirstOrDefault(p => p.Name == name && !p.IsDeleted) ??
               throw new EntityNotFoundException($"Part with name:{name} is not found.");
        }

        public Part Update(int id, Part newPartDTO)
        {
            var newPart = context.Parts.FirstOrDefault(s => s.Id == id && !s.IsDeleted) ??
                throw new EntityNotFoundException($"Parts to update with id:{id} not found.");

            newPart.Name = newPartDTO.Name;
            newPart.UnitPrice = newPartDTO.UnitPrice;

            context.SaveChanges();
            return newPart;
        }

        public Part Delete(int id)
        {
            var toDelete = GetById(id);
            toDelete.IsDeleted = true;
            context.SaveChanges();
            return toDelete;
        }

        public bool PartExists(string name)
        {
            return context.Parts.Any(p => p.Name == name);
        }
    }
}

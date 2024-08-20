using Microsoft.EntityFrameworkCore;
using Smart_Garage.Models;
using Smart_Garage.Repositories.Contracts;

namespace Smart_Garage.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly SGContext context;

        public BrandRepository(SGContext context)
        {
            this.context = context;
        }

        public IList<VehicleBrand> GetAll()
        {
            return context.Brands
                .Include(b => b.Models)
                .Where(b => !b.IsDeleted).ToList();
        }
        

    }
}

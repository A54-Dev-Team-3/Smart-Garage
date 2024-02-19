using Smart_Garage.Models;
using Smart_Garage.Repositories.Contracts;

namespace Smart_Garage.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly SGContext context;

        public ModelRepository(SGContext context)
        {
            this.context = context;
        }

        public IList<Model> GetAll()
        {
            return context.Models.Where(u => !u.IsDeleted).ToList();
        }
    }
}

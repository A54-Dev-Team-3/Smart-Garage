using Smart_Garage.Models;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IModelRepository
    {
        public IList<Model> GetAll();
    }
}

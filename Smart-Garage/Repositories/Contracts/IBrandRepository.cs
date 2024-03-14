using Smart_Garage.Models;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IBrandRepository
    {
        public IList<VehicleBrand> GetAll();
    }
}

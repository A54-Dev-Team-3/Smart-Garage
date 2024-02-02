using Smart_Garage.Models;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IMechanicRepository
    {
        Mechanic Create(Mechanic newMechanic);
        IList<Mechanic> GetAll();
        Mechanic GetById(int id);
        Mechanic GetByName(string name);
        Mechanic Delete(int id);
        bool VisitExists(string name);
    }
}

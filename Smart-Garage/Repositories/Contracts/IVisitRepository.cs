using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IVisitRepository
    {
        Visit Create(Visit newVisit);
        IList<Visit> GetAll();
        IList<Visit> FilterBy(VisitQueryParameters visitsParams);
        Visit GetById(int id);
        Visit Delete(int id);
        bool VisitExists(int id);
    }
}

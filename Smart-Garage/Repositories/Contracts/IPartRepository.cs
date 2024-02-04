using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IPartRepository
    {
        Part Create(Part newPartDTO);
        IList<Part> GetAll();
        IList<Part> FilterBy(PartQueryParameters filterParameters);
        Part GetById(int id);
        Part GetByName(string name);
        Part Update(int id, Part newPartDTO);
        Part Delete(int id);
        bool PartExists(string name);
    }
}

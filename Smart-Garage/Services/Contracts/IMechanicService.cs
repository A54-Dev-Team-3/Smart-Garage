using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IMechanicService
    {
        MechanicResponseDTO Create(MechanicRequestDTO newMechanic, string username);
        IList<MechanicResponseDTO> GetAll(string username);
        MechanicResponseDTO GetById(int id, string username);
        MechanicResponseDTO GetByName(string name, string username);
        MechanicResponseDTO Delete(int id, string username);
        bool MechanicExists(string name);
    }
}

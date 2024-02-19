using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IMechanicService
    {
        MechanicResponseDTO Create(MechanicRequestDTO newMechanic);
        IList<MechanicResponseDTO> GetAll();
        MechanicResponseDTO GetById(int id);
        MechanicResponseDTO GetByName(string name);
        MechanicResponseDTO Delete(int id, string username);
        bool MechanicExists(string name);
    }
}

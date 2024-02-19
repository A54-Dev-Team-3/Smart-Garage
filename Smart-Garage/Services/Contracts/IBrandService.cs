using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IBrandService
    {
        public IList<BrandResponseDTO> GetAll();
    }
}
